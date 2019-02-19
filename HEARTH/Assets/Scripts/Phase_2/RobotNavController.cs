using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class RobotNavController : MonoBehaviour {

    
    [SerializeField] public GameObject target;
    [SerializeField] private GameObject gameoverController;
    [SerializeField] private GameObject robotController;
    [SerializeField] private Transform robotSpawn;
    private NavMeshAgent navMeshAgent;
    private bool following = false;
    private bool reached = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.isStopped = true;
    }


    void Update()
    {
        navMeshAgent.enabled = following;

        //se il robot sta già inseguendo il player
        if (following)
        {
            navMeshAgent.SetDestination(target.transform.position);
            //se ha raggiunto il target lo fa scomparire TOFIX
            if (reached)
            {
                following = false;
                reached = false;
                //trigger.SetActive(true);
                gameoverController.GetComponent<GameOver_Controller>().GameOver();
                StartCoroutine(robotController.GetComponent<RobotController>().ResetOnGameOver());
                //StartCoroutine(respawnRobot());
            }
        }

    }

    private IEnumerator respawnRobot()
    {
        Debug.Log(following);
        Instantiate(this.transform.gameObject, robotSpawn.position, robotSpawn.rotation);
        Destroy(this.transform.gameObject);
        yield return new WaitForSeconds(2f);
        navMeshAgent.enabled = true;
        following = true;
    }

    public void SetFollowing(bool state)
    {
        following = state;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Player")
        {
            reached = true;
        }
    }
}
