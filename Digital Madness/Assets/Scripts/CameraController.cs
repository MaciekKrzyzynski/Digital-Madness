using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Rigidbody player;
    private Vector3 offset;

    void Start()
    {
        
        offset = player.transform.position - transform.position;
    }

    void LateUpdate()
    {

       // transform.position = player.transform.position + offset; 

    }
}
