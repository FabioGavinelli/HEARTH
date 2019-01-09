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
                //Debug.Log("collided with " + other.name);

                Transform grabbed = script.GetGrabbedObjectTransform();
                //Debug.Log("grabbed object : " + grabbed.name);
                showObject = script.GetShowable();
                Debug.Log(showObject);

                if (grabbed.tag == "MetalPlate" && showObject)
                {
                    Debug.Log("showing");
                    this.transform.GetChild(0).gameObject.SetActive(true);
                    //this.transform.GetComponentInChildren<Transform>(true).gameObject.SetActive(true);
                    Debug.Log(this.transform.name);
                    script.SetShowable(false);
                    script.SetGrabbing();
                }
            }
        }
    }
}
