using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateRobotLight : MonoBehaviour
{
    [SerializeField] private Light robotLight;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            robotLight.enabled = false;
            Debug.Log("lumos!");
            this.enabled = false;
        }
    }
}
