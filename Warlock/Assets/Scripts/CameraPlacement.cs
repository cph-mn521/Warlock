using UnityEngine;

public class CameraPlacement : MonoBehaviour
{

    public Transform player;
    public Vector3 cameraOffset;

    void Update()
    {
        transform.position = player.position + cameraOffset;
        transform.LookAt(player.position);
    }
}
