using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;

    public float height = 10f;
    public float distance = 0.001f;

    void LateUpdate()
    {
        if (player == null) return;
        // position = above + behind, in the player's local frame
        Vector3 targetPos = player.parent.position + player.up * height - player.parent.forward * distance;

        transform.position = targetPos;
        transform.LookAt(player.position, player.up);
    }
}