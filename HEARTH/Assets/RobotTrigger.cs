using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotTrigger : MonoBehaviour
{

    public GameObject robot;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            robot.SetActive(true);
            this.enabled = false;
        }
    }
}
