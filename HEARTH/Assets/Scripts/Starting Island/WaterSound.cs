using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSound : MonoBehaviour {

    [SerializeField] private GameObject player;
    private My_FPSController controller;
    private GameObject conscSound;

    void Start()
    {
        controller = player.GetComponent<My_FPSController>();
        conscSound = GameObject.FindGameObjectWithTag("Consciousness");
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            controller.SetStepSound(2);
            conscSound.GetComponent<ConsciousnessController>().PlayAudioClip(3);
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
