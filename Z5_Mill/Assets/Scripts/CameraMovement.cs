using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform target;
    [SerializeField] private float distanceToTarget = 10;

    private Vector3 previousPosition;

    public float lookSpeedH = 2f;
    public float lookSpeedV = 2f;
    public float zoomSpeed = 2f;
    public float dragSpeed = 6f;
    public Vector2 panlLimit;
    private void Update()
    {

        Vector3 pos = cam.transform.position;

        if (Input.GetMouseButtonDown(1)) // 0 is left button, movement has started
        {
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButton(1)) // true if LEFT mouse button is down
        {
            Vector3 newPosition = cam.ScreenToViewportPoint(Input.mousePosition); // what does this do?
            Vector3 direction = previousPosition - newPosition;

            

            float rotationAroundYAxis = -direction.x * 180; // camera moves horizontally
            float rotationAroundXAxis = direction.y * 180; // camera moves vertically

            cam.transform.position = target.position; // Reset position of camera to target.position
            
            cam.transform.Rotate(new Vector3(1, 0, 0), rotationAroundXAxis); //rotation around xaxis
            cam.transform.Rotate(new Vector3(0, 1, 0), rotationAroundYAxis, Space.World); //roatation around y-axis
            
            cam.transform.Translate(new Vector3(0, 0, -distanceToTarget)); // move camera position backward so it is looking at target

            previousPosition = newPosition;
        }

        //Pan camera
        if (Input.GetMouseButton(0))
        {
            float x = -Input.GetAxisRaw("Mouse X") * Time.deltaTime * dragSpeed;
            float y = -Input.GetAxisRaw("Mouse Y")*Time.deltaTime * dragSpeed;
            float z = 0;

            // Clamp camera
            x = Mathf.Clamp(-Input.GetAxisRaw("Mouse X"), -panlLimit.x, panlLimit.x)* Time.deltaTime * dragSpeed;
            y = Mathf.Clamp(-Input.GetAxisRaw("Mouse Y"), -panlLimit.y, panlLimit.y)* Time.deltaTime * dragSpeed;
            cam.transform.Translate(x, y, 0);
        }

        // Zoom scroll
        if (Input.GetMouseButton(2))
        {
            cam.transform.Translate(0,0,Input.GetAxis("Mouse ScrollWheel") * zoomSpeed, Space.Self);
        }

    }
}