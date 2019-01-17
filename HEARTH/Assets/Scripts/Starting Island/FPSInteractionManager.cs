using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.Utility;

public class FPSInteractionManager : MonoBehaviour
{
    [SerializeField] private Transform _fpsCameraT;
    [SerializeField] private bool _debugRay;
    [SerializeField] private float _interactionDistance;
    [SerializeField] private float _pushDistance;
    [SerializeField] private float _pushForce;
    [SerializeField] private float _grabDistance;
    [SerializeField] private GameObject _leftArm;
    [SerializeField] private GameObject _rightArm;
    [SerializeField] private GameObject _rotationPointLeft;
    [SerializeField] private GameObject _rotationPointRight;
    //[SerializeField] private GameObject _metalPlateR;   //R = right
    [SerializeField] private GameObject _oar;
    //[SerializeField] private GameObject _metalPlateLF;  //L = left; F = front
    //[SerializeField] private GameObject _metalPlateLB;  //L = left; B = back



    //[SerializeField] private Image _target;

    //private bool _pointingInteractable;
    private bool _pointingGrabbable;
    public bool droppable;              //has to be private
    private bool enteredShipZone = false;
    private bool showable = false;

    private CharacterController fpsController;
    private Vector3 rayOrigin;

    private Grabbable _grabbedObject = null;
    private GameObject interactingObject;


    public float InteractionDistance
    {
        get { return _interactionDistance; }
        set { _interactionDistance = value; }
    }

    public Grabbable GrabbedObject
    {
        set { _grabbedObject = value; }
    }

    void Start()
    {
        fpsController = GetComponent<CharacterController>();
    }

    void Update()
    {
        rayOrigin = _fpsCameraT.position + fpsController.radius * _fpsCameraT.forward;

        if (_grabbedObject == null || droppable == false)
            CheckInteraction();


        if (/*Input.GetMouseButtonDown(0)*/Input.GetKeyDown(KeyCode.E))
        {
            if (_grabbedObject != null && droppable)
            {
                Drop();
                droppable = false;
                SetGrabbing();      //set _grabbedObject to null
            }
            /*else
                Push();*/
        }

        //UpdateUITarget();

        if (_debugRay)
            DebugRaycast();
    }

    private void CheckInteraction()
    {

        Ray ray = new Ray(rayOrigin, _fpsCameraT.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, InteractionDistance))
        {
            //Check if the controller collides with the ship collider

            if (hit.transform.childCount != 0)
                interactingObject = hit.transform.GetChild(0).gameObject;

           /* if (hit.transform.tag == "Ship")
            {
                //Debug.Log("colliding with ship");
                //droppable = true;
            }*/

            //Check if is interactable
            /*Interactable interactable = hit.transform.GetComponent<Interactable>();
            _pointingInteractable = interactable != null ? true : false;
            if (_pointingInteractable)
            {
               
                if(Input.GetMouseButtonDown(1))
                    interactable.Interact(gameObject);
            }*/

            //Check if is grabbable
            Grabbable grabbableObject = hit.transform.GetComponent<Grabbable>();
            _pointingGrabbable = grabbableObject != null ? true : false;
            if (_pointingGrabbable && _grabbedObject == null)
            {

                if (Input.GetKeyDown(KeyCode.E))
                {
                    grabbableObject.Grab(gameObject);
                    Grab(grabbableObject);
                    //_leftArm.transform.RotateAround(this.transform.position, Vector3.right, -90);
                    //_rightArm.transform.RotateAround(this.transform.position, Vector3.right, -90);
                    StartCoroutine(WaitingDroppable());
                    //droppable = true;
                }

            }
        }
        else
        {
            //_pointingInteractable = false;
            _pointingGrabbable = false;
        }

    }

    /* private void UpdateUITarget()
     {
         if (_pointingInteractable)
             _target.color = Color.green;
         else if (_pointingGrabbable)
             _target.color = Color.yellow;
         else
             _target.color = Color.red;
     }*/
    /*private void Push()
    {
        Ray ray = new Ray(rayOrigin, _fpsCameraT.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _pushDistance))
        {
            Rigidbody otherRigidbody = hit.rigidbody;
            if (otherRigidbody != null)
            {
                otherRigidbody.AddForce(-hit.normal * _pushForce);
            }
        }
    }*/

    private void Drop()
    {
        if (_grabbedObject == null)
            return;

        if (_grabbedObject.transform.tag == "MetalPlate" && enteredShipZone)
        {
            //Debug.Log(interactingObject.name);
            //interactingObject.SetActive(true);
            //_wreckedBoat.SetActive(false);
            //_metalPlateR.SetActive(true);
            //_grabbedObject.transform.parent = _grabbedObject.OriginalParent;
            //_grabbedObject.Drop();
            _grabbedObject.gameObject.SetActive(false);
            showable = true;

        }else if (_grabbedObject.transform.tag == "Oar" && enteredShipZone)
        {
            _oar.SetActive(true);
            //_grabbedObject.transform.position = _grabbedObject.OriginalParent.transform.position;
            _grabbedObject.gameObject.SetActive(false);
            //_grabbedObject.Drop();

        }else{
            _grabbedObject.transform.parent = _grabbedObject.OriginalParent;
        //_grabbedObject.gameObject.SetActive(false);
            _grabbedObject.Drop();
        //_target.enabled = true;
            
        }

        //_grabbedObject = null;

    }

    private void Grab(Grabbable grabbable)
    {
        _grabbedObject = grabbable;
        grabbable.transform.SetParent(_fpsCameraT);
        Vector3 grabPosition = _fpsCameraT.position + transform.forward * _grabDistance;

        //_target.enabled = false;
    }

    private void DebugRaycast()
    {
        Debug.DrawRay(rayOrigin, _fpsCameraT.forward * InteractionDistance, Color.red);
    }

    public void SetEnteredZone(bool entered)
    {
        enteredShipZone = entered;
    }

    public IEnumerator WaitingDroppable()
    {
        yield return new WaitForSeconds(0.5f);
        droppable = true;
    }

    public Transform GetGrabbedObjectTransform()
    {
        if (_grabbedObject == null)
            return null;
        else
            return _grabbedObject.transform;
    }

    public bool GetShowable()
    {
        bool ret = showable;
        return ret;
    }

    public void SetShowable(bool var)
    {
        showable = var;
    }

    public void SetGrabbing()
    {
        _grabbedObject = null;
    }
}
