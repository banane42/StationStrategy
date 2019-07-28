using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float maxCameraSpeed;
    public float maxCameraSize = 20f;
    public float minCameraSize = 1f;
    public float zoomSensitivity;
    public bool useMouseToMove = true;
    Camera Camera;

    void Awake()
    {
        Camera = GetComponent<Camera>();
    }

    void FixedUpdate()
    {

        Camera.orthographicSize += -Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity;

        //Speed based on how zoomed in the camera is. More zoomed in, lower speed.
        float cameraSpeed = maxCameraSpeed * (Camera.orthographicSize / maxCameraSize);

        if (Camera.orthographicSize <= 1f) {
            Camera.orthographicSize = 1f;
        }

        if (Camera.orthographicSize >= 20) {
            Camera.orthographicSize = 20;
        }

        if (Input.GetKey(KeyCode.W) || (Input.mousePosition.y >= Screen.height - 3 && useMouseToMove)) {

            transform.position = new Vector3(transform.position.x, transform.position.y + cameraSpeed, -10f);

        }

        if (Input.GetKey(KeyCode.S) || (Input.mousePosition.y <= 3 && useMouseToMove)) {

            transform.position = new Vector3(transform.position.x , transform.position.y - cameraSpeed , -10f);

        }

        if (Input.GetKey(KeyCode.A) || (Input.mousePosition.x <= 3 && useMouseToMove)) {

            transform.position = new Vector3(transform.position.x - cameraSpeed, transform.position.y, -10f);

        }

        if (Input.GetKey(KeyCode.D) || (Input.mousePosition.x >= Screen.width - 3 && useMouseToMove)) {

            transform.position = new Vector3(transform.position.x + cameraSpeed , transform.position.y , -10f);

        }

    }
}
