using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOver_Controller : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private GameObject audioController;
    [SerializeField] private GameObject loadingScreen;
    private bool respawnable = false;
    private Vector3 respawnLocation;
    private bool respawning = false;
    private Quaternion respawnRotation;

    // Start is called before the first frame update
    void Start()
    {
        respawnLocation = player.transform.position;
        respawnRotation = player.transform.rotation;
    }


    public void Respawn()
    {
        if (respawnable == true)
        {
            //respawn player
            respawnable = false;
            player.GetComponent<My_FPSController>().enabled = true;
            gameOverCanvas.SetActive(false);
            //play music
            audioController.GetComponent<Sound_Controller>().SetVolume(0, 0);
            audioController.GetComponent<Sound_Controller>().SetVolume(1, 0);
            StartCoroutine(audioController.GetComponent<Sound_Controller>().volumeUp(0, 0.2f, 2f, true)); //0 = wave
            StartCoroutine(audioController.GetComponent<Sound_Controller>().volumeUp(1, 0.2f, 2f, true)); //1 = piano
            //waveSound.volume = 0;
            //pianoSound.volume = 0;
            //StartCoroutine(volumeUp(waveSound, 0.2f, 2f, true));
            //StartCoroutine(volumeUp(pianoSound, 0.2f, 2f, true));
            //reset step sound
            player.GetComponent<My_FPSController>().SetStepSound(0);
            if (SceneManager.GetActiveScene().buildIndex == 2 || SceneManager.GetActiveScene().buildIndex == 3)
                StartCoroutine(ReloadAsync());
        }
    }

    IEnumerator ReloadAsync()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        loadingScreen.SetActive(true);
        respawning = true;
        while (!async.isDone)
        {
            yield return null;
        }
    }

    /* ---- GAMEOVER FUNCTIONS ---- */
    public IEnumerator FadeTextToFullAlpha(float f, Text t)
    {
        t.color = new Color(t.color.r, t.color.g, t.color.b, 0);
        while (t.color.a < 1.0f)
        {
            t.color = new Color(t.color.r, t.color.g, t.color.b, t.color.a + (Time.deltaTime / f));
            if (t.color.a >= 0.5)
            {
                respawnable = true;
            }
            yield return null;
        }
    }

    public void GameOver()
    {
        respawnable = false;
        player.GetComponent<PlayerBehaviour>().setLifePoints(100f);
        player.transform.position = respawnLocation;
        player.transform.rotation = respawnRotation;
        player.GetComponent<My_FPSController>().enabled = false;
        gameOverCanvas.SetActive(true);
        StartCoroutine(FadeTextToFullAlpha(10f, gameOverCanvas.transform.GetChild(1).gameObject.GetComponent<Text>()));
        //stop music
        audioController.GetComponent<Sound_Controller>().StopAllAudioSources();
    }

    public bool GetRespawnState()
    {
        return respawning;
    }
}
