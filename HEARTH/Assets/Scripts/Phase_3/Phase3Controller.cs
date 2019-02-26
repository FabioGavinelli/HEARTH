using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Phase3Controller : MonoBehaviour
{
    [SerializeField] private GameObject gameoverController;
    [SerializeField] private GameObject player;

    void Start()
    {
        player.GetComponent<My_FPSController>().SetStepSound(1);    
    }

    void Update()
    {
        //RESPAWN
        if ((Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKey("enter")))
        {
            gameoverController.GetComponent<GameOver_Controller>().Respawn();

        }
    }


}
