using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
public class PlayerScript : MonoBehaviour
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

        float pitchAngle = moveInput.y * speed * Time.fixedDeltaTime;
        float rollAngle = -moveInput.x * speed * Time.fixedDeltaTime;

        Quaternion stepRotation = Quaternion.AngleAxis(pitchAngle, transform.right) *
                                  Quaternion.AngleAxis(rollAngle, transform.forward);


        Quaternion targetRotation = stepRotation * rb.rotation;
        rb.MoveRotation(targetRotation);

        Vector3 newUp = targetRotation * Vector3.up;
        Vector3 targetPosition = surface.position + (newUp * distance.magnitude);

        rb.MovePosition(targetPosition);
    }
}
