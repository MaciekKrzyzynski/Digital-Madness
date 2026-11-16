using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
public class PlayerCylinderAttachScript : MonoBehaviour
{
    private InputSystem_Actions controls;
    private Rigidbody rb;
    public Rigidbody cylinderSurface;
    public float speed = 10;
    

    void Awake()
    {
        controls = new InputSystem_Actions();
        rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        controls.Player.Enable();
    }

    void FixedUpdate()
    {
        Vector2 moveInput = controls.Player.Move.ReadValue<Vector2>();
        Vector3 distance =  rb.transform.position - cylinderSurface.transform.position;
        
        float pitchAngle = moveInput.y * speed * 1.5f * Time.fixedDeltaTime;

        Quaternion stepRotation = Quaternion.AngleAxis(pitchAngle, -cylinderSurface.transform.up);

        Quaternion targetRotation = stepRotation * rb.rotation;
        rb.MoveRotation(targetRotation);

        // offset for straight x movement and clamp
        float addOffset = distance.x + moveInput.x * speed * Time.fixedDeltaTime / 2.0f;
        addOffset = Mathf.Clamp(addOffset, -23.5f, 23.5f);
        distance.x = 0;

        Vector3 newUp = targetRotation * Vector3.up;
        Vector3 targetPosition = cylinderSurface.position + (newUp * distance.magnitude) + new Vector3( addOffset, 0f, 0f); // add the offset
        rb.MovePosition(targetPosition);
    }
}
