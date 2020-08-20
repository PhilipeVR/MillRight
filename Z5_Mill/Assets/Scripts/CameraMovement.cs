using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform mill;
    [SerializeField] private Transform target;
    

    private Vector3 previousPosition;

    private float DistanceToTarget = 10f;

    public float lookSpeedH = 2f;
    public float lookSpeedV = 2f;
    public float zoomSpeed = 0.2f;
    public float dragSpeed = 0.2f;
    public Vector2 panlLimit;
    public Vector3 maxBounds;
    public Vector3 minBounds;

    void Start(){
        // Vector3 maxBounds = this.transform.position;
        // maxBounds.x = 5f;
        // maxBounds.y = 2f;
        // Vector3 minBounds = this.transform.position;
        // minBounds.x = -5f;
        // minBounds.y = -2f;
    }

    private void Update()
    {

        Vector3 pos = cam.transform.position;
        
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

            cam.transform.position = target.position; // Reset position of camera to target.position
            
            cam.transform.Rotate(new Vector3(1, 0, 0), rotationAroundXAxis); //rotation around xaxis
            cam.transform.Rotate(new Vector3(0, 1, 0), rotationAroundYAxis, Space.World); //roatation around y-axis
            
            cam.transform.Translate(new Vector3(0, 0, -DistanceToTarget)); // move camera position backward so it is looking at target

            previousPosition = newPosition;
        }

        //Pan camera
        if (Input.GetMouseButton(0))
        {
            float x = -Input.GetAxis("Mouse X");
            float y = -Input.GetAxis("Mouse Y");
            float z = 0;

            //float xclamp = Mathf.Clamp(x, -mill.position.x * 2f, mill.position.x * 2f);
            //float yclamp = Mathf.Clamp(y, -mill.position.y * 2f, mill.position.y * 2f);

            cam.transform.Translate(x, y, z);
            pos = cam.transform.position;
            //Debug.Log(maxBounds.x);
            //Debug.Log(minBounds.x);

            if(pos.x > maxBounds.x){
                pos.x = maxBounds.x;                
            }
            if(pos.x < minBounds.x){
                pos.x = minBounds.x;
            }

            if(pos.y > maxBounds.y){
                pos.y = maxBounds.y;
            }
            if(pos.y < minBounds.y){
                pos.y = minBounds.y;
            }
            cam.transform.position = pos; //update position as a whole

            

            //Debug.Log(xclamp);

            //cam.transform.position = new Vector3(x,y,-38f);
        }

        // Zoom scroll
        if (Input.GetMouseButton(2))
        {
            cam.transform.Translate(0,0,Input.GetAxis("Mouse ScrollWheel") * zoomSpeed, Space.Self);
        }

    }
}