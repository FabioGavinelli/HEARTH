using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine;

public class Phase2Conroller : MonoBehaviour
{
    [SerializeField] private GameObject robot;
    [SerializeField] private GameObject gameoverController;
    [SerializeField] private GameObject robotController;
    [SerializeField] private GameObject obstaclesController;
    private bool gameover = false;

    private void Update()
    {
        //RESPAWN
        if ((Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKey("enter")))
        {
            gameoverController.GetComponent<GameOver_Controller>().Respawn();
        }

        /*
        if (robot.GetComponent<RobotNavController>().IsTargetReached() == true)
        {
            robot.GetComponent<RobotNavController>().SetReachedState(false);
            gameoverController.GetComponent<GameOver_Controller>().GameOver();
            StartCoroutine(robotController.GetComponent<RobotController>().ResetOnGameOver());
            obstaclesController.GetComponent<ObstaclesController>().ResetCubes();
        }
        */
    }

}
