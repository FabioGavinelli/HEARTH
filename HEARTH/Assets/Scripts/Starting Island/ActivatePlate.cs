using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePlate : MonoBehaviour {

    private bool showObject;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.GetComponent<My_FPSInteractionManager>() != null)
        {
            My_FPSInteractionManager script = other.transform.GetComponent<My_FPSInteractionManager>();
            if (script.GetGrabbedObjectTransform() != null)
            {

                Transform grabbed = script.GetGrabbedObjectTransform();
                showObject = script.GetShowable();

                if (grabbed.tag == "MetalPlate" && showObject)
                {
                    this.transform.GetChild(0).gameObject.SetActive(true);
                    script.SetShowable(false);
                    script.SetGrabbing();
                }
            }
        }
    }
}
