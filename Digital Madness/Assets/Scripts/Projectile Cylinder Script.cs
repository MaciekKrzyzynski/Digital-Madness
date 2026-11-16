using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
public class ProjectileCylinderScript : MonoBehaviour
{

    private Rigidbody rb;
    private GameObject surface;
    public float speed = 10;
    private float radius;
    private Vector3 lastNormal;

    void Awake()
    {
      
        rb = GetComponent<Rigidbody>();
        surface = GameObject.FindWithTag("CylinderMap");
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
        offset.x = 0f;
        radius = offset.magnitude;
        lastNormal = offset.normalized;
    }

    void FixedUpdate()
    {

        Vector3 position = rb.position - (rb.rotation * Vector3.up) * speed * Time.fixedDeltaTime;

        // no rotation on the x
        Vector3 normal = new Vector3(0f, position.y - surface.transform.position.y, position.z - surface.transform.position.z).normalized;

        position.y = surface.transform.position.y + normal.y * radius;
        position.z = surface.transform.position.z + normal.z * radius;
        rb.MovePosition(position);

        Quaternion rotation = Quaternion.FromToRotation(lastNormal, normal) * rb.rotation; 
        rb.MoveRotation(rotation);
        lastNormal = normal;

       
    }
}
