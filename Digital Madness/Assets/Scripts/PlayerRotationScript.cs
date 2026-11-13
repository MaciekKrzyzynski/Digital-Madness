using UnityEngine;

public class PlayerRotationScript : MonoBehaviour
{
    private InputSystem_Actions controls;
    private Camera camera; 

    void Awake()
    {
        controls = new InputSystem_Actions();
        GameObject cameraObject = GameObject.FindWithTag("MainCamera");
        camera = cameraObject.GetComponent<Camera>();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 mousePos = controls.UI.Point.ReadValue<Vector2>();
        Vector2 screenPos = camera.WorldToScreenPoint(transform.position);
        Vector2 distance = mousePos - screenPos;
        float rotation = Mathf.Atan2(distance.y, distance.x);
        rotation = Mathf.Rad2Deg * rotation;
        transform.localRotation = Quaternion.Euler(0, -rotation, 0);
    }
}
