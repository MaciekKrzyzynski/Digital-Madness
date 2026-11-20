using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class Enemy1Script : MonoBehaviour
{
    public GameObject projectilePrefab;
    public bool powerUp;

    public float orbitRadius = 9.5f;
    public float orbitDrift = 20f;
    public float turnSpeed = 60.0f;
    private float angleValue;

    private Rigidbody rb;
    private Transform player;

    void Awake()
    {

        rb = GetComponent<Rigidbody>();
        GameObject p = GameObject.Find("Player");
        if (p != null) player = p.transform;
    }

  

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        angleValue = Mathf.Atan2(rb.position.z, rb.position.x) * Mathf.Rad2Deg;
    }
   
    // Update is called once per frame
    void FixedUpdate()
    {
        if (player == null) return;


        float playerAngle = Mathf.Atan2(player.position.z, player.position.x) * Mathf.Rad2Deg;
        float delta = Mathf.DeltaAngle(angleValue, playerAngle);
        float maxStep = turnSpeed * Time.fixedDeltaTime;
        angleValue += Mathf.Clamp(delta, -maxStep, maxStep)+ orbitDrift * Time.fixedDeltaTime;

        float rad = angleValue * Mathf.Deg2Rad;
        Vector3 newPos = new Vector3(
            orbitRadius * Mathf.Cos(rad),
            rb.position.y,
            orbitRadius* Mathf.Sin(rad));

        rb.MovePosition(newPos);

        Vector3 toCenter = (Vector3.zero - newPos).normalized;
        rb.MoveRotation(Quaternion.LookRotation(toCenter, Vector3.up));
    }
}
