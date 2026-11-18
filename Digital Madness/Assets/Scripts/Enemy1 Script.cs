using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class Enemy1Script : MonoBehaviour
{
    
    private Rigidbody rb;
    public Transform player;
    public Transform center;

    public float speed = 40f;
    public float turnRate = 90f;
    public float radiusOffset = 0f;
    private Vector3 dir;

    private Vector3 heading;
    Vector3 Center => center != null ? center.position : Vector3.zero;

    void Awake()
    {

        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = false;

        if (player == null)
        {
            GameObject p = GameObject.Find("Player");
            if (p != null) player = p.transform;
        }
        if (player == null) Debug.LogWarning("Enemy1Script: no player found on " + name);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dir = (rb.position - Center).normalized;
        if (dir == Vector3.zero) dir = Vector3.right;

        heading = Vector3.ProjectOnPlane(Vector3.forward, dir);
        if (heading.sqrMagnitude < 0.001f) heading = Vector3.ProjectOnPlane(Vector3.right, dir);
           heading.Normalize();
    }
   
    // Update is called once per frame
    void FixedUpdate()
    {
        if (player == null) return;

        Vector3 toPlayer = player.position - Center;
        float radius = toPlayer.magnitude + radiusOffset;
        Vector3 playerDir = toPlayer.normalized;

        Vector3 desired = Vector3.ProjectOnPlane(playerDir, dir);
        if(desired.sqrMagnitude > 0.0001f)
        {
            heading = Vector3.RotateTowards(heading, desired.normalized,
                turnRate * Mathf.Deg2Rad * Time.fixedDeltaTime, 0f);
        }

        heading = Vector3.ProjectOnPlane(heading, dir).normalized;

        Vector3 axis = Vector3.Cross(dir, heading);
        dir = (Quaternion.AngleAxis(speed * Time.fixedDeltaTime, axis) * dir).normalized;
        rb.MovePosition(Center + dir * radius);

        heading = Vector3.ProjectOnPlane(heading, dir).normalized;
        rb.MoveRotation(Quaternion.LookRotation(heading, dir));
    }
}
