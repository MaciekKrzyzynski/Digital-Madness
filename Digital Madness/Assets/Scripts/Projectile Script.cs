using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
public class ProjectileScript : MonoBehaviour
{

    private Rigidbody rb;
    private GameObject surface;
    public float speed = 10;
   
    void Awake()
    {
      
        rb = GetComponent<Rigidbody>();
        surface = GameObject.FindWithTag("PlayArea");
    }


    void FixedUpdate()
    {
      
        Vector3 distance = surface.transform.position - rb.transform.position;

        float pitchAngle =  speed * Time.fixedDeltaTime;


        Quaternion stepRotation = Quaternion.AngleAxis(pitchAngle, transform.forward);  // Z = tangent → correct spin axis
        Quaternion targetRotation = stepRotation * rb.rotation;
        rb.MoveRotation(targetRotation);

        Vector3 newUp = targetRotation * Vector3.right;                                // X = normal (radial) → keeps bullet on sphere
        Vector3 targetPosition = surface.transform.position + (newUp * distance.magnitude);
        rb.MovePosition(targetPosition);

    }
}
