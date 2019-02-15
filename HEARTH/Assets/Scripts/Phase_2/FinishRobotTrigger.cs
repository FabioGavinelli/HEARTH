using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishRobotTrigger : MonoBehaviour
{
    [SerializeField] private GameObject robot;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            robot.GetComponent<RobotNavController>().enabled = false;
            //robot.GetComponent<RobotAnimationController>().TriggerAnimation((int)RobotAnimationController.robotAnimations.walk);
        }
    }
}
