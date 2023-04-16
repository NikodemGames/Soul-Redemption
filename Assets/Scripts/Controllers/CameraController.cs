using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;

    public Vector3 offset;

    
    [SerializeField] private float currentZoom = 6f;
    public float zoomSpeed = 60f;
    public float minZoom = 5f;
    public float maxZoom = 15f;

    public float pitch = 2;
    public float yawSpeed = 100f;
    private float yawInput = 0f;

    private void Update()
    {
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom,minZoom, maxZoom);

        yawInput -= Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = target.position-offset*currentZoom;
        transform.LookAt(target.position+Vector3.up*pitch);
        transform.RotateAround(target.position, Vector3.up, yawInput);
    }
}
