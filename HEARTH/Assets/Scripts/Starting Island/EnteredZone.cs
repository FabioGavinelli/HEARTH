using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnteredZone : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<My_FPSInteractionManager>() != null)
        {

            My_FPSInteractionManager script = other.transform.GetComponent<My_FPSInteractionManager>();
            script.SetEnteredZone(true);

            if (script.GetGrabbedObjectTransform() != null) { 
                if ((script.GetGrabbedObjectTransform().tag == "Oar") || ((script.GetGrabbedObjectTransform().tag == "MetalPlate") && (script.GetOwningHammer()) && (script.GetOwningNails())))
                {
                    GetComponentInParent<OutlineObject>().OutlineObj(true);
                }
            }
        }  
    }

   /* private void OnTriggerStay(Collider other)
    {
        if (other.transform.GetComponent<My_FPSInteractionManager>() != null)
        {

            My_FPSInteractionManager script = other.transform.GetComponent<My_FPSInteractionManager>();
            script.SetEnteredZone(true);

            if (script.GetGrabbedObjectTransform() != null)
            {
                if ((script.GetGrabbedObjectTransform().tag == "Oar") || ((script.GetGrabbedObjectTransform().tag == "MetalPlate") && (script.GetOwningHammer()) && (script.GetOwningNails())))
                {
                    GetComponentInParent<OutlineObject>().OutlineObj(true);
                }
                else
                {
                    GetComponentInParent<OutlineObject>().OutlineObj(false);
                }
            }
        }
    }
    */

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.GetComponent<My_FPSInteractionManager>() != null)
        {
            My_FPSInteractionManager script = other.transform.GetComponent<My_FPSInteractionManager>();
            script.SetEnteredZone(false);

            GetComponentInParent<OutlineObject>().OutlineObj(false); 
        }
        

    }
}
