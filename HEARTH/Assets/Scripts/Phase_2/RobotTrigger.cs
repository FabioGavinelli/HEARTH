using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class RobotTrigger : MonoBehaviour
{

    [SerializeField] private GameObject robot;
    [SerializeField] private Light robotLight;
    [SerializeField] private GameObject rightEye;
    [SerializeField] private GameObject leftEye;



    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            robot.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            //robot.SetActive(true);
            StartCoroutine(bootRobot());
        }
    }

    private IEnumerator bootRobot()
    {
        robotLight.enabled = true;
        rightEye.SetActive(true);
        leftEye.SetActive(true);
        yield return new WaitForSeconds(2f);
        robot.GetComponent<AudioSource>().Play();
        StartCoroutine(StartFollowPLayer());
    }

    private IEnumerator StartFollowPLayer()
    {
        robot.GetComponent<RobotAnimationController>().TriggerAnimation((int)RobotAnimationController.robotAnimations.walk);
        yield return new WaitForSeconds(1f);
        robot.GetComponent<RobotNavController>().SetFollowing(true);
        //robot.GetComponent<NavMeshAgent>().isStopped = false;
        this.transform.gameObject.SetActive(false);
    }
}
