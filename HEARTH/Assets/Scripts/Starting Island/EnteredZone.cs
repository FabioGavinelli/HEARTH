using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnteredZone : MonoBehaviour {

    private bool sameObj = false;
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

            if(script.GetGrabbedObjectTransform() != null && script.GetGrabbedObjectTransform().tag == this.gameObject.tag)
            {
                sameObj = true;
                script.SetEnteredZone(true);

               if ((script.GetGrabbedObjectTransform().tag == "Oar") || ((script.GetGrabbedObjectTransform().tag == "MetalPlate") && (script.GetOwningHammer()) && (script.GetOwningNails())))
               {
                    GetComponentInParent<OutlineObj>().enabled = true;
               }
                
            }
            else{
                sameObj = false;
            }
            
        }  
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.GetComponent<My_FPSInteractionManager>() != null)
        {
            My_FPSInteractionManager script = other.transform.GetComponent<My_FPSInteractionManager>();
            script.SetEnteredZone(false);

            if(sameObj == true)
            {
                GetComponentInParent<OutlineObj>().enabled = false;
                //this.GetComponent<Collider>().enabled = false;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.GetComponent<My_FPSInteractionManager>() != null)
        {
            My_FPSInteractionManager script = other.transform.GetComponent<My_FPSInteractionManager>();

            if (script.GetGrabbedObjectTransform() == null && GetComponentInParent<OutlineObj>().enabled == true && sameObj)
            {
                GetComponentInParent<OutlineObj>().enabled = false;
                this.GetComponent<Collider>().enabled = false;
                sameObj = false;
            }
        }
    }
}
