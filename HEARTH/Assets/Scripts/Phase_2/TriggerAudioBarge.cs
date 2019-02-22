using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAudioBarge : MonoBehaviour
{
    private ConsciousnessController cc;
    // Start is called before the first frame update
    void Start()
    {
        cc = GameObject.FindGameObjectWithTag("Consciousness").GetComponent<ConsciousnessController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            cc.PlayAudioClip(1);
        }
    }
}
