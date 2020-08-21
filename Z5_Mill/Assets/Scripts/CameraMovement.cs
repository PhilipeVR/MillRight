using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform mill;
    [SerializeField] private Transform target;
    

    private Vector3 previousPosition;
    private Vector3 previousPosition2;

    private float DistanceToTarget = 10f;

    public float lookSpeedH = 2f;
    public float lookSpeedV = 2f;
    public float zoomSpeed = 0.2f;
    public float dragSpeed = 0.2f;
    public Vector2 panlLimit;
    public Vector3 maxBounds;
    private Vector3 maxDistance;
    public Vector3 minBounds;
    private Vector3 minDistance;

    void Start(){
        previousPosition2 = cam.transform.position;
        UpdateBounds(previousPosition2);
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
            //Debug.Log("Cam Position Before Translate: " + pos);

            cam.transform.Translate(x, y, z);
            pos = cam.transform.position;
            //Debug.Log("Cam Position: " + pos);

            if(pos.x > maxDistance.x){
                pos.x = maxDistance.x;                
            }
            else if(pos.x < minDistance.x){
                pos.x = minDistance.x;
            }

            if(pos.y > maxDistance.y){
                pos.y = maxDistance.y;
            }
            else if(pos.y < minDistance.y){
                pos.y = minDistance.y;
            }

            if (pos.z > maxDistance.z)
            {
                pos.z = maxDistance.z;
            }
            else if (pos.z < minDistance.z)
            {
                pos.z = minDistance.z;
            }
            cam.transform.position = pos; //update position as a whole

            previousPosition2 = pos;
            UpdateBounds(previousPosition2);


            //Debug.Log(xclamp);

            //cam.transform.position = new Vector3(x,y,-38f);
        }

        // Zoom scroll
        if (Input.GetMouseButton(2))
        {
            cam.transform.Translate(0,0,Input.GetAxis("Mouse ScrollWheel") * zoomSpeed, Space.Self);
        }

    }

    private void UpdateBounds(Vector3 pos)
    {
        maxDistance = pos + maxBounds;
        minDistance = pos + minBounds;
    }
}