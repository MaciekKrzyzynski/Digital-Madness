using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;

    public float height = 10f;
    public float distance = 0.001f;

    void LateUpdate()
    {
        // position = above + behind, in the player's local frame
        Vector3 targetPos = player.position + player.up * height - player.forward * distance;

        transform.position = targetPos;
        transform.LookAt(player.position, player.up);
    }
}