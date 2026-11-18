using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SocialPlatforms.GameCenter;

public class Enemy2Script : MonoBehaviour
{

    public Transform player;
    public Transform center;
    private float orbitRadius = 9.5f;
    private float radiusOffset = 0f;
    public float orbitSpeed = 30f;

    public float angleValue;
    private float radius;
    private Rigidbody rb;

    Vector3 Center => center != null ? center.position : Vector3.zero;

    void Awake()
    {

        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;

        if(player == null)
        {
            GameObject p = GameObject.Find("Player");
            if (p != null) player = p.transform;
        }
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vector3 offset = rb.position - Center;
        angleValue = Mathf.Atan2(rb.position.z, rb.position.x) * Mathf.Rad2Deg;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (player != null)
            radius = (player.position - Center).magnitude + radiusOffset;
        else
            orbitRadius = orbitRadius + radiusOffset;
        
        angleValue += orbitSpeed * Time.fixedDeltaTime;
        float rad = angleValue * Mathf.Deg2Rad;


        Vector3 newPos = Center + new Vector3(
            radius * Mathf.Cos(rad),
            rb.position.y - Center.y,
            radius * Mathf.Sin(rad));

        rb.MovePosition(newPos);

        Vector3 toCenter = (Center - newPos).normalized;
        rb.MoveRotation(Quaternion.LookRotation(toCenter, Vector3.up));

   }
}

