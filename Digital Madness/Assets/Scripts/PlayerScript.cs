using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using System.Security.Cryptography;

public class PlayerScript : MonoBehaviour
{
    private InputSystem_Actions controls;
    private Camera camera;
    public GameObject projectilePrefab;
    public bool powerUp;
    private bool firing;
    private int maxLives = 5;
    public GameText heartsUI;
    private int lives;
    private float invincibleUntil;
    public float invincibleTime = 1f;

    void Awake()
    {
        controls = new InputSystem_Actions();
        GameObject cameraObject = GameObject.FindWithTag("MainCamera");
        camera = cameraObject.GetComponent<Camera>();
        if (heartsUI == null) heartsUI = FindFirstObjectByType<GameText>();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lives = maxLives;
        heartsUI.SetLives(lives);
        invincibleUntil = Time.time + 2f;
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

    public void TakeDamage()
    {
        lives--;
        heartsUI.SetLives(lives);
        if (lives <= 0)
        {
            Debug.Log("Player hit" + lives);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player trigger with " + other.name);
        if (other.GetComponentInParent<Enemy1Script>() != null &&
            other.GetComponentInParent<Enemy2Script>() != null) return;
        if (Time.time < invincibleUntil) return;
        invincibleUntil = Time.time + invincibleTime;
        
        TakeDamage();
        
    }

    


}
