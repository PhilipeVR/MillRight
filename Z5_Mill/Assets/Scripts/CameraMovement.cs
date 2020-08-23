using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform mill;
    [SerializeField] private Transform target;

    private Vector3 previousPosition;
    private Vector3 previousPosition2;

    private float DistanceToTarget = 10f;


    public float zoomSpeed = 0.2f;
    public float dragSpeed = 2f;
 
    public Vector3 maxBounds;
    private Vector3 maxDistance;
    public Vector3 minBounds;
    private Vector3 minDistance;

    // Minimum and maximum values in world space
    private float MIN_X = 6f;
    private float MAX_X = 12f;
    private float MIN_Y = 5f;
    private float MAX_Y = 11f;
    private float MIN_Z = -21f;
    private float MAX_Z = -9f;



    Vector2 input;

    void Start(){
        previousPosition2 = cam.transform.position;
        Vector3 maxBounds = this.transform.position;
        maxBounds.x = 5f;
        maxBounds.y = 2f;
        Vector3 minBounds = this.transform.position;
        minBounds.x = -5f;
        minBounds.y = -2f;
    }

    private void Update()
    {
        // Begin orbit
        if (Input.GetMouseButtonDown(1))
        {
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(1)) // true while button is held down (i.e. not released)
        {
            Vector3 newPosition = cam.ScreenToViewportPoint(Input.mousePosition);
            Vector3 direction = previousPosition - newPosition;

            float rotationAroundYAxis = -direction.x * 180; // camera moves horizontally
            float rotationAroundXAxis = direction.y * 180; // camera moves vertically

            //transform.position = target.position; // Reset position of camera to target.position
            transform.position = target.position;
            
            transform.Rotate(new Vector3(1, 0, 0), rotationAroundXAxis); //rotation around xaxis
            transform.Rotate(new Vector3(0, 1, 0), rotationAroundYAxis, Space.World); //roatation around y-axis
            
            transform.Translate(new Vector3(0, 0, -DistanceToTarget)); // move camera position backward so it is looking at target

            previousPosition = newPosition;
        }

        //Pan camera
        if (Input.GetMouseButton(0))
        {

            float x = -Input.GetAxis("Mouse X");
            float y = -Input.GetAxis("Mouse Y");
            float z = 0;

            Vector3 movement = Vector3.zero;
            movement.x += x;
            movement.y += y;


            transform.Translate(movement * dragSpeed * Time.deltaTime, Space.Self);

            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, MIN_X, MAX_X),
                Mathf.Clamp(transform.position.y, MIN_Y, MAX_Y),
                Mathf.Clamp(transform.position.z, MIN_Z, MAX_Z)
            );

        }

        // Zoom scroll
        if (Input.GetMouseButton(2))
        {
            cam.transform.Translate(0,0,Input.GetAxis("Mouse ScrollWheel") * zoomSpeed, Space.Self);
        }

    }
}