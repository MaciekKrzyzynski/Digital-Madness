using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class ProjectileScript : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject planeSurface;
    private GameObject player;
    private GameObject CylinderSurface;
    private GameObject sphereSurface;

    public float speed = 10;
    public Level level;

    // Plane
    Quaternion rotation = Quaternion.identity;

    // Cylinder
    private float CylinderRadius;
    private Vector3 CylinderLastNormal;

    // Sphere
    private float SphereRadius;
    private Vector3 SphereLastNormal;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (level.currentLevel == 1)
        {
            planeSurface = GameObject.FindWithTag("PlaneMap");
            player = GameObject.FindWithTag("Player");
            rotation = player.transform.rotation * Quaternion.Euler(90.0f, 0.0f, 90.0f);
        }
        else if (level.currentLevel == 2)
        {
            CylinderSurface = GameObject.FindWithTag("CylinderMap");
        }
        else if (level.currentLevel == 3)
        {
            sphereSurface = GameObject.FindWithTag("SphereMap");
        }

        StartCoroutine(Expire());
    }

    IEnumerator Expire()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    void Start()
    {
        if (level.currentLevel == 2)
        {
            Vector3 CylinderOffset = rb.position - CylinderSurface.transform.position;
            CylinderOffset.x = 0f;
            CylinderRadius = CylinderOffset.magnitude;
            CylinderLastNormal = CylinderOffset.normalized;
        }
        else if (level.currentLevel == 3)
        {
            Vector3 SphereOffset = rb.position - sphereSurface.transform.position;
            SphereRadius = SphereOffset.magnitude;
            SphereLastNormal = SphereOffset.normalized;
        }
    }

    void FixedUpdate()
    {
        if (level.currentLevel == 1)
        {
            if (!Physics.Raycast(transform.position, Vector3.down, 10, LayerMask.GetMask("Default")))// check if plane is below the projectile 
            {
                Destroy(gameObject);
            }

            rb.linearVelocity = -transform.up * speed;
            rb.MoveRotation(rotation);
        }
        else if (level.currentLevel == 2)
        {
            Vector3 position = rb.position - (rb.rotation * Vector3.up) * speed * Time.fixedDeltaTime;

            // no rotation on the x
            Vector3 normal = new Vector3(0f, position.y - CylinderSurface.transform.position.y, position.z - CylinderSurface.transform.position.z).normalized;

            position.y = CylinderSurface.transform.position.y + normal.y * CylinderRadius;
            position.z = CylinderSurface.transform.position.z + normal.z * CylinderRadius;

            if (position.x > 23.5f || position.x < -23.5f)
            {
                Destroy(gameObject);
            }
            rb.MovePosition(position);

            Quaternion rot = Quaternion.FromToRotation(CylinderLastNormal, normal) * rb.rotation;
            rb.MoveRotation(rot);
            CylinderLastNormal = normal;
        }
        else if (level.currentLevel == 3)
        {
            Vector3 position = rb.position - (rb.rotation * Vector3.up) * speed * Time.fixedDeltaTime;

            Vector3 normal = (position - sphereSurface.transform.position).normalized;

            position = sphereSurface.transform.position + normal * SphereRadius;

            rb.MovePosition(position);

            Quaternion rot = Quaternion.FromToRotation(SphereLastNormal, normal) * rb.rotation;
            rb.MoveRotation(rot);
            SphereLastNormal = normal;
        }
    }
}
