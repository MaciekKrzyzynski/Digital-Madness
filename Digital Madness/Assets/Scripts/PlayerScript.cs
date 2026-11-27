using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class PlayerScript : MonoBehaviour
{
    private InputSystem_Actions controls;
    private Camera camera;
    public GameObject projectilePrefab;
    public bool powerUp;
    private bool firing;
    private bool invincible = false;
    private bool flickerTransparant = false;
    private float invincibleTime = 2.0f;
    private float flickerTime = 0.2f;
    private int lives = 5;

    void Awake()
    {
        controls = new InputSystem_Actions();
        GameObject cameraObject = GameObject.FindWithTag("MainCamera");
        camera = cameraObject.GetComponent<Camera>();
    }

    private void OnEnable()
    {
        controls.Enable();
        if (!invincible)
        {
            StartCoroutine(playerInvincibility());
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }
    private void Update()
    {
        controls.Player.Attack.performed += ctx =>
        {
            if (!firing)
            {
                StartCoroutine(ProjectileInterval());
            }
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
        Vector3 spawnPosition = transform.position + transform.right * 1f;         
        Instantiate(projectilePrefab, spawnPosition, transform.rotation * spawnRotation);
        if (powerUp)
        {
            spawnRotation = Quaternion.Euler(-90f, 0f, 60f);
            spawnPosition = transform.position + transform.right * 1f + transform.forward * .2f;
            Instantiate(projectilePrefab, spawnPosition, transform.rotation * spawnRotation);
            spawnPosition = transform.position + transform.right * 1f - transform.forward * .2f;
            spawnRotation = Quaternion.Euler(-90f, 0f, 120f);
            Instantiate(projectilePrefab, spawnPosition, transform.rotation * spawnRotation);
        }
    }

    private IEnumerator ProjectileInterval()
    {

        firing = true; 
        while (controls.Player.Attack.IsPressed())
        {
            FireProjectile(); 
            yield return new WaitForSeconds(0.5f);
        }
        firing = false;
    }

    IEnumerator playerInvincibility()
    {
        float timePassed = 0f;
        invincible = true;
        Renderer renderer = GetComponent<Renderer>();
        Material material = renderer.material;

        // Cache original values
        Color color = material.color;
        float originalMetallic = material.GetFloat("_Metallic");
        float originalSmoothness = material.GetFloat("_Smoothness");


        while (timePassed <= invincibleTime)
        {
            if (!flickerTransparant)
            {
                color.a = 0.2f;
                material.SetFloat("_Metallic", 0f);
                material.SetFloat("_Smoothness", 0f);
                flickerTransparant = true;
            }
            else
            {
                color.a = 1.0f;
                material.SetFloat("_Metallic", originalMetallic);
                material.SetFloat("_Smoothness", originalSmoothness);
                flickerTransparant = false;
            }
            GetComponent<Renderer>().material.color = color;
            timePassed += flickerTime;
            yield return new WaitForSeconds(flickerTime);

        }
        color.a = 1.0f;
        material.color = color;
        material.SetFloat("_Metallic", originalMetallic);
        material.SetFloat("_Smoothness", originalSmoothness);
        invincible = false;

    }
}
