using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
public class ProjectileSphereScript : MonoBehaviour
{

    private Rigidbody rb;
    private GameObject surface;
    public float speed = 10;
    private float radius;
    private Vector3 lastNormal;

    void Awake()
    {
      
        rb = GetComponent<Rigidbody>();
        surface = GameObject.FindWithTag("SphereMap");
        StartCoroutine(Expire()); 
        
    }
    IEnumerator Expire()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject); 
    }




    void Start()
    {
        Vector3 offset = rb.position - surface.transform.position;
        radius = offset.magnitude;
        lastNormal = offset.normalized;
    }

    void FixedUpdate()
    {

        Vector3 position = rb.position - (rb.rotation * Vector3.up) * speed * Time.fixedDeltaTime;

        Vector3 normal = (position - surface.transform.position).normalized;

        position =  surface.transform.position + normal * radius;

        rb.MovePosition(position);

        Quaternion rotation = Quaternion.FromToRotation(lastNormal, normal) * rb.rotation; 
        rb.MoveRotation(rotation);
        lastNormal = normal;

       
    }

    void OnTriggerEnter(Collider other)
    {
        Enemy1Script e1 = other.GetComponentInParent<Enemy1Script>();
        if(e1 != null)
        {
            e1.TakeHit();
            Destroy(gameObject);
            return;
        }
    
        Enemy2Script e2 = other.GetComponentInParent<Enemy2Script>();
        if(e2 != null)
        {
            e2.TakeHit();
            Destroy(gameObject);
        }
    }

}
