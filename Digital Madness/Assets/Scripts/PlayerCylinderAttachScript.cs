using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
public class PlayerCylinderAttachScript : MonoBehaviour
{
    private InputSystem_Actions controls;
    private Rigidbody rb;
    public Rigidbody surface;
    public float speed = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
        Vector3 distance = surface.transform.position - rb.transform.position;
        distance.x = 0;

        float pitchAngle = moveInput.y * speed * Time.fixedDeltaTime;

        Quaternion stepRotation = Quaternion.AngleAxis(pitchAngle, -surface.transform.up);


        Quaternion targetRotation = stepRotation * rb.rotation;
        rb.MoveRotation(targetRotation);

        float offsetX = (rb.position.x - surface.position.x) + moveInput.x * speed * Time.fixedDeltaTime / 2.0f;
        offsetX = Mathf.Clamp(offsetX, -25.0f, 25.0f);

        Vector3 newUp = targetRotation * Vector3.up;
        Vector3 targetPosition = surface.position + (newUp * distance.magnitude) + new Vector3( offsetX, 0f, 0f); // then move

  
        rb.MovePosition(targetPosition);


    }
}
