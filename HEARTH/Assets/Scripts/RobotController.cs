using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    [SerializeField] private GameObject robot;
    [SerializeField] private GameObject rightEye;
    [SerializeField] private GameObject leftEye;
    [SerializeField] private GameObject trigger;
    [SerializeField] private Transform robotSpawn;
    private Vector3 robotStartingPosition;
    private Quaternion robotStartingRotation;

    public GameObject GetRobotReference()
    {
        return robot;
    }

    public IEnumerator ResetOnGameOver()
    {
        robot.SetActive(false);
        //robot.GetComponent<Rigidbody>().isKinematic = true;
        //robot.GetComponent<Rigidbody>().detectCollisions = false;
        Debug.Log("Disattivato");
        //transform
        robot.transform.position = robotSpawn.position;
        //robot.transform.localRotation = robotSpawn.localRotation;
        //animations
        robot.GetComponent<RobotAnimationController>().TriggerAnimation((int)RobotAnimationController.robotAnimations.walk);
        //lights
        robot.GetComponentInChildren<Light>().enabled = false;
        rightEye.SetActive(false);
        leftEye.SetActive(false);
        trigger.SetActive(true);
        //audio
        robot.GetComponent<AudioSource>().Stop();
        //trigger
        trigger.SetActive(true);
        //robot.GetComponent<Rigidbody>().detectCollisions = true;
        yield return new WaitForSeconds(2f);
        //robot.GetComponent<Rigidbody>().isKinematic = false;
        robot.SetActive(true);
    }
}
