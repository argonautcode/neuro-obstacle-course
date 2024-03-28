using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private float panSpeed;
    [SerializeField]
    private float zoomSpeed;
    
    private Vector3 dragPoint;

    void Update()
    {
        Pan();
        Zoom();
        MousePan();
        MouseZoom();
    }

    void MousePan()
    {
        if (Input.GetMouseButtonDown(2))
        {
            dragPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(2))
        {
            Vector3 deltaPos = dragPoint - cam.ScreenToWorldPoint(Input.mousePosition);
            cam.transform.position += deltaPos;
        }
    }

    void MouseZoom()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            cam.orthographicSize /= zoomSpeed;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            cam.orthographicSize *= zoomSpeed;
        }
    }

    void Pan()
    {
        transform.Translate(
            Input.GetAxis("Horizontal") * Time.deltaTime * cam.orthographicSize * panSpeed,
            Input.GetAxis("Vertical") * Time.deltaTime * cam.orthographicSize * panSpeed,
            0);
    }

    void Zoom()
    {
        if (Input.GetKey("q"))
        {
            cam.orthographicSize /= 0.98f + zoomSpeed / 50;
        }
        else if (Input.GetKey("e"))
        {
            cam.orthographicSize *= 0.98f + zoomSpeed / 50;
        }
    }
}
