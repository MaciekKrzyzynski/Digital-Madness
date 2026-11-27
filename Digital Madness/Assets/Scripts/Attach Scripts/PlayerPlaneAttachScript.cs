using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
public class PlayerPlaneAttachScript : MonoBehaviour
{
    private InputSystem_Actions controls;
    private Rigidbody rb;
    public Rigidbody planeSurface;
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
        moveInput = moveInput * speed * Time.fixedDeltaTime;
        Vector3 pos = rb.position + new Vector3(moveInput.x, 0.0f, moveInput.y);
        pos.x = Mathf.Clamp(pos.x, -planeSurface.transform.localScale.x / 2, planeSurface.transform.localScale.x /2 );
        pos.z = Mathf.Clamp(pos.z, -planeSurface.transform.localScale.z / 2, planeSurface.transform.localScale.z / 2);
        rb.MovePosition(pos);
    }
}