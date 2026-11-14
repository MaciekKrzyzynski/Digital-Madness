using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private InputSystem_Actions controls;
    private Camera camera;
    public GameObject projectilePrefab;

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
    private void Update()
    {
        controls.Player.Attack.performed += ctx =>
        {
            FireProjectile();
        };
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        // get player and camera position in camera space. 
        Vector2 mousePos = controls.UI.Point.ReadValue<Vector2>();
        Vector2 screenPos = camera.WorldToScreenPoint(transform.position);
        Vector2 distance = mousePos - screenPos;

        //get the angle between them
        float rotation = Mathf.Atan2(distance.y, distance.x);
        rotation = Mathf.Rad2Deg * rotation;
        //apply rotation
        transform.localRotation = Quaternion.Euler(0, -rotation, 0);
    }

    void FireProjectile()
    {
        //Create Projectile 
        Quaternion spawnRotation = Quaternion.Euler(-90f,0f,90f);  
        Vector3 spawnPosition = transform.position + transform.right * 2f;         
        Instantiate(projectilePrefab, spawnPosition, transform.rotation * spawnRotation);
    }
}
