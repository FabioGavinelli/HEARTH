using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Phase2Conroller : MonoBehaviour
{
    [SerializeField] private GameObject robot;
    [SerializeField] private GameObject barge;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject gameoverController;
    [SerializeField] private GameObject robotController;
    [SerializeField] private GameObject obstaclesController;
    [SerializeField] private GameObject blackScreenCanvas;
    [SerializeField] private VideoClip endLevelVideo;
    [SerializeField] private Canvas infoText;
    [SerializeField] private GameObject loadingScreen;
    private bool gameover = false;
    private bool lvl2end = false;
    private AudioSource speaker;
    private ConsciousnessController conscController;
    private bool exiting = false;

    private void Start()
    {
        ResetInfoText();
        speaker = GetComponent<AudioSource>();
        conscController = GameObject.FindGameObjectWithTag("Consciousness").GetComponent<ConsciousnessController>();
        conscController.PlayAudioClip(0);
    }

    private void Update()
    {
        //RESPAWN
        if ((Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKey("enter")))
        {
            gameoverController.GetComponent<GameOver_Controller>().Respawn();
        }

        if (barge.GetComponent<BargeController>().IsLevel2Finshed() && lvl2end == false)
        {
            lvl2end = true;
            EndPhase2();
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

    private void EndPhase2()
    {
        StopAllCoroutines();

        //disable player
        player.GetComponent<My_FPSController>().enabled = false;
        //mute all sounds

        //fade to black
        blackScreenCanvas.SetActive(true);
        StartCoroutine(FadeOutToBlack(blackScreenCanvas.GetComponentInChildren<Image>(), 2f));

        //start video
        this.GetComponent<VideoPlayer>().clip = endLevelVideo;
        StartCoroutine(StartEndVideo());
    }

    private IEnumerator FadeOutToBlack(Image t, float time)
    {
        t.color = new Color(t.color.r, t.color.g, t.color.b, 0);
        while (t.color.a < 1.0f)
        {
            t.color = new Color(t.color.r, t.color.g, t.color.b, Mathf.Clamp(t.color.a + (Time.deltaTime * (1 / time)), 0f, 1f));
            Debug.Log("Fade out to black, alpha: " + t.color.a);
            yield return null;
        }
    }

    private IEnumerator FadeInByBlack(Image t, float time)
    {
        t.color = new Color(t.color.r, t.color.g, t.color.b, 1);
        while (t.color.a > 0f)
        {
            t.color = new Color(t.color.r, t.color.g, t.color.b, Mathf.Clamp(t.color.a - (Time.deltaTime * (1 / time)), 0f, 1f));
            Debug.Log("Fade in to black, alpha: " + t.color.a);
            yield return null;
        }
    }

    private IEnumerator StartEndVideo()
    {
        blackScreenCanvas.transform.GetChild(0).transform.gameObject.SetActive(false);
        yield return new WaitForSeconds(10f);
        speaker.volume = 0.5f;
        this.GetComponent<VideoPlayer>().Play();
        StartCoroutine(FadeInByBlack(blackScreenCanvas.transform.GetChild(1).GetComponent<Image>(), 1f));
        //blackScreenCanvas.SetActive(false);
        //blackScreenCanvas.transform.GetChild(1).transform.gameObject.SetActive(false);
        blackScreenCanvas.transform.GetChild(0).transform.gameObject.SetActive(true);
        StartCoroutine(LoadNewSceneAfterVideo());
    }

    private IEnumerator LoadNewSceneAfterVideo()
    {
        yield return new WaitForSeconds(13f);
        StartCoroutine(LoadSceneAsyncronous());
    }

    private IEnumerator LoadSceneAsyncronous()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(3);
        blackScreenCanvas.SetActive(false);
        loadingScreen.SetActive(true);
        while (!async.isDone)
        {
            loadingScreen.transform.GetChild(2).transform.Rotate(Vector3.forward * 1);
            yield return null;
        }
    }

    private IEnumerator ExitingLevel()
    {
        yield return new WaitForSeconds(4f);
        Debug.Log(exiting);
        if (exiting == true)
            SceneManager.LoadScene(0);
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
