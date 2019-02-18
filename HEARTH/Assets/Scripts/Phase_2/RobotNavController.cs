using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class RobotNavController : MonoBehaviour {

    
    [SerializeField] public GameObject target;
    [SerializeField] private GameObject gameoverController;
    [SerializeField] private GameObject rightEye;
    [SerializeField] private GameObject leftEye;
    [SerializeField] private GameObject trigger;

    private Vector3 robotStartingPosition;
    private NavMeshAgent navMeshAgent;
    private bool following = true;
    private bool reached = false;

    void Start()
    {
        robotStartingPosition = this.transform.position;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        //se il robot sta già inseguendo il player
        if (following)
        {
            navMeshAgent.SetDestination(target.transform.position);
            
            //se ha raggiunto il target lo fa scomparire TOFIX
            if (reached)
            {
                //GAMEOVER TODO
                Debug.Log("GAMEOVER");
                reached = false;
                this.transform.gameObject.SetActive(false);
                gameoverController.GetComponent<GameOver_Controller>().GameOver();
                
                //resetRobot();
            }
        }

    }

    //restituisce true se il target è raggiunto 
    /*private bool TargetReached()
    {
        if (!navMeshAgent.pathPending)
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
                if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
                    return true;

        return false;
    }*/

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Player")
        {
            reached = true;
        }
    }

    private void resetRobot()
    {
        this.transform.position = robotStartingPosition;
        this.GetComponentInChildren<Light>().enabled = false;
        rightEye.SetActive(false);
        leftEye.SetActive(false);
        trigger.SetActive(true);
        this.GetComponent<AudioSource>().Stop();
        this.GetComponent<RobotNavController>().enabled = false;
    }

}
