using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Phase2Conroller : MonoBehaviour
{
    [SerializeField] private GameObject robot;
    [SerializeField] private GameObject gameoverController;

    void Start()
    {
        robot.GetComponent<RobotNavController>().enabled = false;
    }

    private void Update()
    {
        //RESPAWN
        if ((Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKey("enter")))
        {
            gameoverController.GetComponent<GameOver_Controller>().Respawn();
        }
    }

}
