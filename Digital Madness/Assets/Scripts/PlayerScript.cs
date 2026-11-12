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
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 moveInput = controls.Player.Move.ReadValue<Vector2>();
        Vector3 distance = surface.transform.position - rb.transform.position;

        float pitchAngle = moveInput.y * speed * Time.fixedDeltaTime;
        float rollAngle = -moveInput.x * speed * Time.fixedDeltaTime;

        // 2. Combine the rotations around the player's local Right and Forward axes
        Quaternion stepRotation = Quaternion.AngleAxis(pitchAngle, transform.right) *
                                  Quaternion.AngleAxis(rollAngle, transform.forward);

        // 3. Apply the new rotation to the Rigidbody
        Quaternion targetRotation = stepRotation * rb.rotation;
        rb.MoveRotation(targetRotation);

        // 4. Calculate the new position on the sphere's surface using the new "Up" direction
        Vector3 newUp = targetRotation * Vector3.up;
        Vector3 targetPosition = surface.position + (newUp * distance.magnitude);

        // 5. Move the Rigidbody cleanly to the new position
        rb.MovePosition(targetPosition);
    }
}
