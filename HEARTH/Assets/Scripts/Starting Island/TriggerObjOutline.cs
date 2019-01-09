﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObjOutline : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            this.GetComponent<OutlineObject>().OutlineObj(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            this.GetComponent<OutlineObject>().OutlineObj(false);
        }
    }
}
