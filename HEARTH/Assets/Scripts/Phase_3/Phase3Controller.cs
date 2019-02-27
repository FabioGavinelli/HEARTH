using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using UnityEngine;

public class Phase3Controller : MonoBehaviour
{
    [SerializeField] private GameObject gameoverController;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject airPostProcess;
    [SerializeField] private Canvas infoText;
    private PostProcessVolume ppVolume;
    private bool exiting = false;
    private bool cheat = false;

    void Start()
    {
        ResetInfoText();
        player.GetComponent<My_FPSController>().SetStepSound(1);
        ppVolume = airPostProcess.GetComponent<PostProcessVolume>();
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
            SetInfoText("exiting");
            StartCoroutine(ExitingLevel());
        }
        else
        {
            exiting = false;
            ResetInfoText();
            StopCoroutine(ExitingLevel());
        }

        if (Input.GetKey(KeyCode.Backslash))
        {
            cheat = true;
            SetInfoText("activating cheat");
            StartCoroutine(ActivateLifeCheat());
        }
        else
        {
            cheat = false;
            ResetInfoText();
            StopCoroutine(ActivateLifeCheat());
        }
    }

    private IEnumerator ExitingLevel()
    {
        yield return new WaitForSeconds(4f);
        if (exiting == true)
            SceneManager.LoadScene(0);
    }

    private IEnumerator ActivateLifeCheat()
    {
        yield return new WaitForSeconds(2f);
        if (cheat == true)
        {
            player.GetComponent<PlayerBehaviour>().setLifePoints(99999999999999);
            ppVolume.weight = 0;
            airPostProcess.SetActive(false);
            ResetInfoText();
        }
            
    }

    private void SetInfoText(string info)
    {
        infoText.GetComponentInChildren<Text>().text = info;
    }

    private void ResetInfoText()
    {
        SetInfoText("");
    }


}
