using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameController : MonoBehaviour
{

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject wakeUpPPEffect;
    [SerializeField] private AudioClip alarmClock;
    [SerializeField] private AudioSource waveSound;
    [SerializeField] private AudioSource pianoSound;
    [SerializeField] private GameObject consciousnessSounds;
    [SerializeField] private VideoClip endLevelVideo;
    [SerializeField] private GameObject tutorialCanvas;
    [SerializeField] private GameObject blackScreenCanvas;
    [SerializeField] private GameObject menuCanvas;
    [SerializeField] private Canvas infoText;
    [SerializeField] private GameObject loadingScreen;
    private PlayerBehaviour pb;
    private Animator playerAnimator;
    private bool ended = false;
    public bool exiting = false;

    private string[] tutorialMessages = {"PRESS [E] TO STAND UP",
                                         "PRESS [E] TO TURN OFF THE ALARM" };

    private AudioSource speaker;
    private Text tutorialText;

    public int phase;

    [SerializeField] private GameObject gameoverController;
    [SerializeField] private GameObject audioController;

    private PostProcessVolume eye;
    private bool awake = false;


    private void Start()
    {
        //Cursor.visible = false;
        ResetInfoText();
        //Set Up Variables
        //respawnLocation = player.transform.position;
        speaker = GetComponent<AudioSource>();
        tutorialText = tutorialCanvas.GetComponentInChildren<Text>();
        pb = player.GetComponent<PlayerBehaviour>();
        playerAnimator = player.GetComponentInChildren<Animator>();
        //Start Game
        if (phase == 0) PhaseOne();
    }

    void Update()
    {
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

        //RESPAWN
        if ((Input.GetKeyDown(KeyCode.Mouse0)) && gameoverController.GetComponent<GameOver_Controller>().GetRespawnState() == false)
        {
            gameoverController.GetComponent<GameOver_Controller>().Respawn();
        }


        //GAME EVENTS
        if (Input.GetKeyDown(KeyCode.E) && pb.IsKeyboardActive() == true)
        {
            switch (phase)
            {
                case 0:
                    if (awake == true)
                    {
                        phase++;
                        playerAnimator.SetTrigger("Wake");
                        pb.TriggerAnimation((int)PlayerBehaviour.animations.StandUp);
                        tutorialCanvas.SetActive(false);
                        pb.SetKeyboardInput(false);
                        StartCoroutine(timedTutorialText(tutorialCanvas, phase, 11f));
                        consciousnessSounds.GetComponent<ConsciousnessController>().PlayAudioClip(0);
                    }
                    break;

                case 1:
                    phase++;
                    speaker.Stop();
                    tutorialCanvas.SetActive(false);
                    playerAnimator.SetBool("Watch", true);
                    StartCoroutine(ShowMenuCanvas());
                    consciousnessSounds.GetComponent<ConsciousnessController>().PlayAudioClip(1);
                    break;

                /*
                 *  Phase 2 gestita da chiusura biglietto
                 */

                default:
                    break;

            }
        }

        if (player.GetComponent<My_FPSInteractionManager>().GetRepairState() >= 4 && ended == false)
        {
            EndPhaseOne();
        }
    }

    /* ---- GAMEOVER FUNCTIONS ---- */
    public void GameOver()
    {
        gameoverController.GetComponent<GameOver_Controller>().GameOver();
    }

    /* ---- AWAKE FUNCTIONS ---- */

    private IEnumerator EyeOpen()
    {
        for (int i = 0; i < 2; i++)
        {
            float targetWeight = (0.9f - (i / 10f));
            while (eye.weight > targetWeight)
            {
                eye.weight -= 0.005f;
                yield return new WaitForSeconds(0.05f);
            }

            yield return new WaitForSeconds(0.05f);
            eye.weight = 1;
            yield return new WaitForSeconds(0.05f);
            eye.weight = (0.9f - (i / 10f));
        }


        while (eye.weight > 0)
        {
            eye.weight -= 0.01f;
            yield return new WaitForSeconds(0.05f);
        }
        wakeUpPPEffect.SetActive(false);
        tutorialCanvas.SetActive(true);
        pb.SetKeyboardInput(true);
        awake = true;
    }

    private IEnumerator clockVolumeUp()
    {
        while (speaker.volume < 0.15f)
        {
            speaker.volume += 0.0015f;
            yield return new WaitForSeconds(0.03f);
        }
        StartCoroutine(EyeOpen());
    }

    private IEnumerator timedTutorialText(GameObject canvas, int textIndex, float time)
    {
        Text canvasText = canvas.GetComponentInChildren<Text>();

        yield return new WaitForSeconds(time);
        canvasText.text = tutorialMessages[textIndex];
        canvas.SetActive(true);
        pb.SetKeyboardInput(true);
    }

    private IEnumerator ShowMenuCanvas()
    {
        yield return new WaitForSeconds(2f);
        menuCanvas.SetActive(true);
    }

    private void PhaseOne()
    {
        //Disable character controller
        player.GetComponent<My_FPSController>().enabled = false;
        //Set player to sleep 
        playerAnimator.SetTrigger("Sleep");
        player.GetComponent<PlayerBehaviour>().SetCameraToAnimation(true);
        player.GetComponent<PlayerBehaviour>().SetPlayerToActive(false);
        //alarm clock sound setup
        speaker.clip = alarmClock;
        speaker.Play();
        StartCoroutine(clockVolumeUp());
        //eye effect setup 
        eye = wakeUpPPEffect.GetComponent<PostProcessVolume>();
        eye.weight = 1;
        //text setup
        tutorialText.text = tutorialMessages[phase];
    }

    public void QuitFromTicket()
    {
        if (phase == 2)
        {
            phase++;
            player.GetComponent<PlayerBehaviour>().TriggerAnimation((int)PlayerBehaviour.animations.Watch);
            StartCoroutine(pb.SwitchCameraParentInAnimations(2f));
            menuCanvas.SetActive(false);
            menuCanvas.GetComponent<MainMenuManager>().DisableTicketReminder();
            pianoSound.Play();
        }
    }

    /* ---- END LEVEL FUNCTIONS ---- */

    private void EndPhaseOne()
    {
        ended = true;
        StopAllCoroutines();

        //disable player
        player.GetComponent<My_FPSController>().enabled = false;
        //mute all sounds
        if (speaker.volume > 0) StartCoroutine(volumeDown(speaker, 0f, 2.5f, true));
        if (waveSound.volume > 0) StartCoroutine(volumeDown(waveSound, 0f, 2.5f, true));
        if (pianoSound.volume > 0) StartCoroutine(volumeDown(pianoSound, 0f, 2.5f, true));
        //fade to black
        blackScreenCanvas.SetActive(true);
        StartCoroutine(FadeOutToBlack(blackScreenCanvas.GetComponentInChildren<Image>(), 5f));

        //start video
        this.GetComponent<VideoPlayer>().clip = endLevelVideo;
        this.GetComponent<VideoPlayer>().Prepare();
        
    }

    private IEnumerator volumeUp(AudioSource audio, float maxVolume, float time, bool forceStart)
    {
        if (maxVolume > 1 && maxVolume <= 0) yield break;
        if (forceStart) audio.Play();

        while (audio.volume < maxVolume)
        {
            audio.volume += Time.deltaTime / (time / maxVolume);
            yield return null;
        }
    }

    private IEnumerator volumeDown(AudioSource audio, float minVolume, float time, bool forceStop)
    {
        if (minVolume < 0 && minVolume > 1) yield break;

        while (audio.volume > minVolume)
        {
            audio.volume -= Time.deltaTime / (time);
            yield return null;
        }

        if (forceStop) audio.Stop();
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
        StartCoroutine(StartEndVideo());
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
        while (!this.GetComponent<VideoPlayer>().isPrepared)
        {
            yield return null;
        }
        speaker.volume = 0.5f;
        this.GetComponent<VideoPlayer>().Play();
        StartCoroutine(WaitToDisableBlackScreen());
        StartCoroutine(LoadNewSceneAfterVideo());
    }

    IEnumerator WaitToDisableBlackScreen()
    {
        yield return new WaitForSeconds(1f);
        blackScreenCanvas.SetActive(false);
    }

    private IEnumerator LoadNewSceneAfterVideo()
    {
        yield return new WaitForSeconds(11f);
        StartCoroutine(LoadSceneAsyncronous());
    }

    private IEnumerator LoadSceneAsyncronous()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(2);
        loadingScreen.SetActive(true);
        while (!async.isDone)
        {
            yield return null;
        }
    }

    private IEnumerator ExitingLevel()
    {
        yield return new WaitForSeconds(4f);
        Debug.Log(exiting);
        if(exiting == true)
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
