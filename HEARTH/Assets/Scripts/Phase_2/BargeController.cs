using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BargeController : MonoBehaviour
{
    private float angle = 1f;
    private bool level2finished = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        //wave effect
        transform.position = new Vector3(Mathf.PingPong(Time.time, 3), 3f + Mathf.PingPong(Time.time, 1), 204f + Mathf.PingPong(Time.time, 0.5f));
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            level2finished = true;
        }
    }

    public bool IsLevel2Finshed()
    {
        return level2finished;
    }
}

