﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.Utility;

public class My_FPSInteractionManager : MonoBehaviour
{
    [SerializeField] private Transform _fpsCameraT;
    [SerializeField] private bool _debugRay;
    [SerializeField] private float _interactionDistance;
    [SerializeField] private float _pushDistance;
    [SerializeField] private float _pushForce;
    [SerializeField] private float _grabDistance;
    [SerializeField] private Transform _grabbedObjPosition;
    //[SerializeField] private Transform _leftHand;
    //[SerializeField] private Transform _rightHand;
    [SerializeField] private GameObject _oar;

    private bool _pointingGrabbable;
    private bool _pointingOutlinable;
    private bool droppable;              
    private bool enteredShipZone = false;
    private bool showable = false;
    private bool owningHammer = false;      
    private bool owningNails = false;
    private bool finishLifting = false;
    public int repairState = 0;

    private Collider entered;
    private CharacterController fpsController;
    private Vector3 rayOrigin;
    private Vector3 OutlineRay;
    private OutlineObject pointed = null;

    private Grabbable _grabbedObject = null;     
    private GameObject interactingObject;
    private PlayerBehaviour pb;

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
        pb = GetComponent<PlayerBehaviour>();
    }

    void Update()
    {
        rayOrigin = _fpsCameraT.position + fpsController.radius * _fpsCameraT.forward;
        //OutlineRay = _fpsCameraT.position + fpsController.radius * _fpsCameraT.forward;
        //CheckInteractionOutlinable();

        if (_grabbedObject == null || droppable == false)
            CheckInteraction();


        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_grabbedObject != null && droppable)
            {
                Drop();
                droppable = false;
            }
        }

        if (_debugRay)
            DebugRaycast();
    }

    
    private void OnTriggerEnter(Collider other)
    {
        entered = other;
    }

    private void CheckInteractionOutlinable()
    {
        Ray ray = new Ray(OutlineRay, _fpsCameraT.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, InteractionDistance))
        {
            //Check if is outlinable
            OutlineObject outlinedObj = hit.transform.GetComponentInChildren<OutlineObject>();
            //GameObject hittedObj = hit.transform.gameObject;
           
            _pointingOutlinable = outlinedObj != null ? true : false;

            if (_pointingOutlinable)
            {
                outlinedObj.OutlineObj(true);
            }
            else if(pointed != null && (pointed != outlinedObj))
            {
                pointed.OutlineObj(false);
            }

            pointed = outlinedObj;
        }
    }

    private void CheckInteraction()
    {

        Ray ray = new Ray(rayOrigin, _fpsCameraT.forward);
        RaycastHit hit;
        

        if (Physics.Raycast(ray, out hit, InteractionDistance))
        {
            //Check if is grabbable
            Grabbable grabbableObject = hit.transform.GetComponent<Grabbable>();
            _pointingGrabbable = grabbableObject != null ? true : false;
   
            if (_pointingGrabbable && _grabbedObject == null)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Transform grabbableTag = grabbableObject.transform;
                    grabbableObject.Grab(gameObject);
                    pb.TriggerAnimation((int)PlayerBehaviour.animations.Lift);
                    StartCoroutine(WaitingLifting(grabbableObject, 1.3f));
                    //Grab(grabbableObject);
                    StartCoroutine(WaitingDroppable(grabbableTag));
                }
            }
        }
        else
        {
            _pointingGrabbable = false;
        }
    }

    private void Drop()
    {
        if (_grabbedObject == null)
            return;

        pb.setGrabbedState(false);

        if (_grabbedObject.transform.tag == "MetalPlate" && enteredShipZone && owningHammer && owningNails) {

            _grabbedObject.gameObject.SetActive(false);
            repairState++;
            showable = true;
            StartCoroutine(DisableCollider());

        } else if (_grabbedObject.transform.tag == "Oar" && enteredShipZone) {
            _oar.SetActive(true);
            _grabbedObject.gameObject.SetActive(false);
            repairState++;
            SetGrabbing();

        } else {
            _grabbedObject.transform.parent = _grabbedObject.OriginalParent;
            _grabbedObject.Drop();
            SetGrabbing();
        }
    }

    private void Grab(Grabbable grabbable)
    {
        _grabbedObject = grabbable;

        if(grabbable.transform.tag == "Hammer")
        {
            owningHammer = true;
            _grabbedObject.gameObject.SetActive(false);
            _grabbedObject = null;
        }
        else if(grabbable.transform.tag == "Nails")
        {
            owningNails = true;
            _grabbedObject.gameObject.SetActive(false);
            _grabbedObject = null;
        }
        else
        {
            Debug.Log("No Martello, No Chiodi");
            pb.setGrabbedState(true);
            StartCoroutine(ShowObj(grabbable, 2));
            grabbable.transform.SetParent(_grabbedObjPosition);
            grabbable.transform.localPosition = Vector3.zero;
        }
    }

    private void DebugRaycast()
    {
        Debug.DrawRay(rayOrigin, _fpsCameraT.forward * InteractionDistance, Color.red);
    }

    public void SetEnteredZone(bool entered)
    {
        enteredShipZone = entered;
    }

    public IEnumerator DisableCollider()
    {
        yield return new WaitForSeconds(2f);
        entered.enabled = false;
    }

    public IEnumerator WaitingDroppable(Transform grabbableTag)
    {
        yield return new WaitForSeconds(0.5f);
        droppable = true;

        if (grabbableTag.tag == "Hammer")
            droppable = false;
        if (grabbableTag.tag == "Nails")
            droppable = false;
    }

    public IEnumerator WaitingLifting(Grabbable grab, float time)
    {
        yield return new WaitForSeconds(time);
        Grab(grab);
        grab.gameObject.SetActive(false);
        
    }

    private IEnumerator ShowObj(Grabbable grab, float time)
    {
        yield return new WaitForSeconds(time);
        grab.gameObject.SetActive(true);
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

    public bool GetOwningHammer()
    {
        return owningHammer;
    }
    public bool GetOwningNails()
    {
        return owningNails;
    }

    public int GetRepairState()
    {
        return repairState;
    }
}
