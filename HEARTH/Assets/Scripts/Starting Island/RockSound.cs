using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSound : MonoBehaviour {

    [SerializeField] private GameObject player;
    private My_FPSController controller;

	void Start () {
        controller = player.GetComponent<My_FPSController>();	
	}
	
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            controller.SetStepSound(1);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            controller.SetStepSound(0);
        }
    }
}
