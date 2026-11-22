using UnityEngine;

public class CompanionScript : MonoBehaviour
{
    public float speed = 0.1f;
    private Vector3 originalPos;
    private Quaternion playerRotation;

    void Start()
    {
        originalPos = transform.localPosition * transform.parent.localScale.x;
        playerRotation = transform.parent.rotation;
    }

    void FixedUpdate()
    {
        playerRotation = Quaternion.Slerp(playerRotation, transform.parent.rotation, speed * Time.deltaTime);

        transform.position = Vector3.MoveTowards( transform.position,transform.parent.position + (playerRotation * originalPos), speed);

        transform.rotation = playerRotation;
    }
}
