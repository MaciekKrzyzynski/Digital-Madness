using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SocialPlatforms.GameCenter;

public class Enemy2Script : MonoBehaviour
{

    public Transform player;
    public Transform center;
    private float fallbackRadius = 9.5f;
    private float radiusOffset = 0f;
    public float orbitSpeed = 30f;
    public float axisSpinSpeed = 10f;
    public float spriteAngleOffset = 0f;

    public float angleValue;
    private float radius;
    private Rigidbody rb;
    private Vector3 dir;
    private Vector3 axis;
    private Vector3 heading;
    public int lives = 2;

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

        dir = (rb.position - Center).normalized;
        if (dir == Vector3.zero) dir = Vector3.right;

        axis = Vector3.Cross(dir, Vector3.up);
        if (axis.sqrMagnitude < 0.001f) axis = Vector3.Cross(dir, Vector3.right);
        axis.Normalize();

        axis = Quaternion.AngleAxis(Random.Range(0f, 360f), dir) * axis;
        }

    // Update is called once per frame
    void FixedUpdate()
    {
        float radius = (player != null)
            ? (player.position - Center).magnitude + radiusOffset
            : fallbackRadius + radiusOffset;

        dir = (Quaternion.AngleAxis(orbitSpeed * Time.fixedDeltaTime, axis) * dir).normalized;


        axis = Quaternion.AngleAxis(axisSpinSpeed * Time.fixedDeltaTime, Vector3.up) * axis;
        axis = Vector3.ProjectOnPlane(axis, dir).normalized;

        rb.MovePosition(Center + dir *radius);

        //angleValue += orbitSpeed * Time.fixedDeltaTime;
        //float rad = angleValue * Mathf.Deg2Rad;


        heading = Vector3.ProjectOnPlane(heading, dir).normalized;
        Quaternion rot = Quaternion.LookRotation(-dir, heading) * Quaternion.Euler(0f, 0f, spriteAngleOffset);
        rb.MoveRotation(rot);
   }

    public void TakeHit()
    {
        lives--;
        Debug.Log("Enemy2 hit by" + lives);
        if (lives == 0) Destroy(gameObject);
    }
}

