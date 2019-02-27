using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Phase3Controller : MonoBehaviour
{
    [SerializeField] private GameObject gameoverController;
    [SerializeField] private GameObject player;

    private bool exiting = false;

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

        if (Input.GetKey(KeyCode.Escape))
        {
            exiting = true;
            StartCoroutine(ExitingLevel());
        }
        else
        {
            exiting = false;
            StopCoroutine(ExitingLevel());
        }
    }

    private IEnumerator ExitingLevel()
    {
        yield return new WaitForSeconds(4f);
        Debug.Log(exiting);
        if (exiting == true)
            SceneManager.LoadScene(0);
    }


}
