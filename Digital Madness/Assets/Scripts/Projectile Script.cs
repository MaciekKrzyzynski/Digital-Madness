using System.Collections;
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
        StartCoroutine(Expire()); 
        
    }
    IEnumerator Expire()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject); 
    }

    void FixedUpdate()
    {
        
        Vector3 distance = surface.transform.position - rb.transform.position;

        float pitchAngle =  speed * Time.fixedDeltaTime;

        Quaternion stepRotation = Quaternion.AngleAxis(pitchAngle, transform.right);
        Quaternion targetRotation = stepRotation * rb.rotation;
        rb.MoveRotation(targetRotation);
       
        //The projectile is laying down so the forward vector is used for facing away from sphere. 
        Vector3 newUp = targetRotation * Vector3.forward;
        Vector3 targetPosition = surface.transform.position + (newUp * distance.magnitude);
        rb.MovePosition(targetPosition);

    }
}
