using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float rotationSpeed = 1f;

    private bool isRotating = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isRotating = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            isRotating = false;
        }
    }

    void LateUpdate()
    {
        if (isRotating)
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

            transform.RotateAround(player.position, Vector3.up, mouseX);
            transform.RotateAround(player.position, transform.right, -mouseY);
        }

    }
}
