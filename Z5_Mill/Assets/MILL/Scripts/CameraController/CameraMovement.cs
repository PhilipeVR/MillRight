using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform cameraFocusPt;

    public float dragSpeed = 6f;
    public Vector3 maxBounds;
    private Vector3 maxDistance;
    public Vector3 minBounds;
    private Vector3 minDistance;
    private Vector3 moveXY;

    // Minimum and maximum values in world space
    private float MIN_X = -3f;
    private float MAX_X = 3f;
    private float MIN_Y = -4f;
    private float MAX_Y = 2f;
    private float MIN_Z = 0f;
    private float MAX_Z = 10f;

    // Variables for CameraOrbit Script
    protected Transform _XForm_Camera;
    protected Transform _XForm_Parent; // CameraController

    protected Vector3 _LocalRotation; // final target rotation
    protected float _CameraDistance = 5f; // controls zoom

    public float MouseSensitivity = 4f;
    public float ScrollSensitvity = 4f;
    public float OrbitDampening = 25f;
    public float ScrollDampening = 6f;

    void Start(){
        //previousPosition2 = cam.transform.position;
        Vector3 maxBounds = this.transform.position;
        maxBounds.x = 5f;
        maxBounds.y = 2f;
        Vector3 minBounds = this.transform.position;
        minBounds.x = -5f;
        minBounds.y = -2f;

        // Variables for CameraOrbit Script
        this._XForm_Camera = this.transform;
        this._XForm_Parent = this.transform.parent;
    }

    void Update()
    {
        if(!EventSystem.current.IsPointerOverGameObject())
        {
            // Move Camera in front-facing (XY) plane
            if (Input.GetMouseButton(0))
            {
                moveXY = Vector3.zero;
                moveXY.x += (-Input.GetAxis("Mouse X"));
                moveXY.y += (-Input.GetAxis("Mouse Y"));
                MoveFocusPointXY(moveXY);            
            }

            // Rotate camera relative to cameraPivot
            if (Input.GetMouseButton(1))
            {
                //Rotation of the Camera based on Mouse Coordinates
                //Only triggers when mouse is not stationary
                if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
                {
                    _LocalRotation.x += Input.GetAxis("Mouse X") * MouseSensitivity;
                    _LocalRotation.y -= Input.GetAxis("Mouse Y") * MouseSensitivity; // Prevent inverted controls

                    //Clamp the y Rotation to horizon and not flipping over at the top
                    _LocalRotation.y = Mathf.Clamp(_LocalRotation.y, 0f, 90f);
                    _LocalRotation.x = Mathf.Clamp(_LocalRotation.x, -40f, 40f);
                }
            }        

            //Zooming Input from our Mouse Scroll Wheel

            if ((Input.GetAxis("Mouse ScrollWheel") != 0f) && Input.GetKey(KeyCode.LeftShift))
            {
                float ScrollAmount = Input.GetAxis("Mouse ScrollWheel") * ScrollSensitvity;
                ScrollAmount *= (this._CameraDistance * 0.3f);
                this._CameraDistance += ScrollAmount * -1f;
                //This makes camera go no closer than 1.5 meters from target, and no further than 10 meters.
                this._CameraDistance = Mathf.Clamp(this._CameraDistance, -4f, 13f);
            }
        }
        
    }

    void LateUpdate() {
        //Actual Camera Rig Transformations
        //This must be in LastUpdate()
        Quaternion QT = Quaternion.Euler(_LocalRotation.y, _LocalRotation.x, 0);
        this._XForm_Parent.rotation = Quaternion.Lerp(this._XForm_Parent.rotation, QT, Time.deltaTime * OrbitDampening);

        if ( this._XForm_Camera.localPosition.z != this._CameraDistance * -1f ) // Camera Scroll / Zooming update
        {
            this._XForm_Camera.localPosition = new Vector3(0f, 0f, Mathf.Lerp(this._XForm_Camera.localPosition.z, this._CameraDistance * -1f, Time.deltaTime * ScrollDampening));
        } 
    }

    private void MoveFocusPointXY(Vector3 movement){
        cameraFocusPt.transform.Translate(movement * dragSpeed * Time.deltaTime, Space.Self);
        cameraFocusPt.transform.position = new Vector3(
            Mathf.Clamp(cameraFocusPt.transform.position.x, MIN_X, MAX_X),
            Mathf.Clamp(cameraFocusPt.transform.position.y, MIN_Y, MAX_Y),
            Mathf.Clamp(cameraFocusPt.transform.position.z, MIN_Z, MAX_Z)
        );
    }
}