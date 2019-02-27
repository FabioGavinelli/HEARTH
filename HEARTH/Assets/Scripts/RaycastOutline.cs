using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastOutline : MonoBehaviour
{
    [SerializeField] private Transform _fpsCameraT;
    [SerializeField] private float _interactionDistance;
    [SerializeField] private bool _debugRay;

    private CharacterController fpsController;
    private Vector3 rayOrigin;
    private bool _pointingOutlinable;
    private OutlineObj pointed = null;

    // Start is called before the first frame update
    void Start()
    {
        fpsController = GetComponent<CharacterController>();
    }

    public float InteractionDistance
    {
        get { return _interactionDistance; }
        set { _interactionDistance = value; }
    }

    // Update is called once per frame
    void Update()
    {
        rayOrigin = _fpsCameraT.position + fpsController.radius * _fpsCameraT.forward;
        CheckInteractionOutlinable();

        if (_debugRay)
            DebugRaycast();
    }

    private void CheckInteractionOutlinable()
    {
        Ray ray = new Ray(rayOrigin, _fpsCameraT.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, InteractionDistance))
        {
            //Check if is outlinable
            OutlineObj outlinedObj = hit.transform.GetComponent<OutlineObj>();
            _pointingOutlinable = outlinedObj != null ? true : false;

            if (_pointingOutlinable)
            {
                outlinedObj.enabled = true ;
            }
            else if (pointed != null && (pointed != outlinedObj))
            {
                pointed.enabled = false;
            }

            pointed = outlinedObj;
        }
        else
        {
            if (pointed != null/* && _pointingOutlinable == false*/)
                pointed.enabled = false;
        }

        //if(pointed != null && _pointingOutlinable == false)
               //pointed.enabled = false;
    }

    private void DebugRaycast()
    {
        Debug.DrawRay(rayOrigin, _fpsCameraT.forward * InteractionDistance, Color.red);
    }
}
