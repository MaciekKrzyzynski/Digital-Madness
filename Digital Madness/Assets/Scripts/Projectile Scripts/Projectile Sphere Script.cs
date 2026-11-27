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
        if (rb == null || surface == null)
        {
            enabled = false;
            return;
        }
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
}
