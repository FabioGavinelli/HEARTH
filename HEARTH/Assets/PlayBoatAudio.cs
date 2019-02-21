using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBoatAudio : MonoBehaviour
{
    private GameObject sound;
    // Start is called before the first frame update
    void Start()
    {
        sound = GameObject.FindGameObjectWithTag("Consciousness");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player"){
            sound.GetComponent<ConsciousnessController>().PlayAudioClip(2);


        }
    }
}
