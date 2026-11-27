using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
public class ProjectilePlaneScript : MonoBehaviour
{

    private Rigidbody rb;
    private GameObject surface;
    private GameObject player;
    public float speed = 10;
    Quaternion rotation = Quaternion.identity;


    void Awake()
    {

        rb = GetComponent<Rigidbody>();
        surface = GameObject.FindWithTag("PlaneMap");
        player = GameObject.FindWithTag("Player");
        rotation = player.transform.rotation * Quaternion.Euler(90.0f, 0.0f, 90.0f); 
        StartCoroutine(Expire());

    }
    IEnumerator Expire()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }


    void FixedUpdate()
    {

        if (!Physics.Raycast(transform.position, Vector3.down, 10, LayerMask.GetMask("Default")))// check if plane is below the projectile 
        {
            Destroy(gameObject);
        }

        rb.linearVelocity = -transform.up * speed;

        rb.MoveRotation(rotation);


    }
}
