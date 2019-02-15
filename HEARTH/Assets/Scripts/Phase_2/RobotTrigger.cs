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
            //robot.SetActive(true);
            StartCoroutine(StartFollowPLayer());
            this.enabled = false;
        }
    }

    private IEnumerator StartFollowPLayer()
    {
        robot.GetComponent<RobotAnimationController>().TriggerAnimation((int)RobotAnimationController.robotAnimations.walk);
        yield return new WaitForSeconds(1f);
        robot.GetComponent<RobotNavController>().enabled = true;
    }
}
