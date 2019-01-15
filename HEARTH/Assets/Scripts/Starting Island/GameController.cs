using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameController : MonoBehaviour {

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject wakeUpPPEffect;
    [SerializeField] private GameObject damagePPEffect;
    [SerializeField] private AudioClip alarmClock;
    [SerializeField] private AudioSource waveSound;
    [SerializeField] private AudioSource pianoSound;
    [SerializeField] private VideoClip endLevelVideo;
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private GameObject tutorialCanvas;
    [SerializeField] private GameObject blackScreenCanvas;
    [SerializeField] private GameObject menuCanvas;
    private PlayerBehaviour pb;
    private Animator playerAnimator;
    private bool ended = false;

    private string[] tutorialMessages = {"PREMI IL TASTO AZIONE [E] PER ALZARTI",
                                         "PREMI IL TASTO AZIONE [E] PER SPEGNERE LA SVEGLIA" };

    private AudioSource speaker;
    private Text tutorialText;


    public int phase;

    private bool respawnable = false;
    private Vector3 respawnLocation;
    
    private PostProcessVolume eye;
    private bool awake = false;


    private void Start()
    {
        //Cursor.visible = false;
        
        //Set Up Variables
        respawnLocation = player.transform.position;
        speaker = GetComponent<AudioSource>();
        tutorialText = tutorialCanvas.GetComponentInChildren<Text>();
        pb = player.GetComponent<PlayerBehaviour>();
        playerAnimator = player.GetComponentInChildren<Animator>();
        //Start Game
        if (phase == 0) PhaseOne();
    }

    void Update()
    {

        //RESPAWN
        if ((Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKey("enter")) && respawnable == true)
        {
            //respawn player
            respawnable = false;
            player.GetComponent<My_FPSController>().enabled = true;
            gameOverCanvas.SetActive(false);
            //play music
            waveSound.volume = 0;
            pianoSound.volume = 0;
            StartCoroutine(volumeUp(waveSound, 0.2f, 2f, true));
            StartCoroutine(volumeUp(pianoSound, 0.2f, 2f, true));
            //reset step sound
            player.GetComponent<My_FPSController>().SetStepSound(0);
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
                    }
                break;

            case 1:
                phase++;
                speaker.Stop();
                tutorialCanvas.SetActive(false);
                playerAnimator.SetBool("Watch", true);
                StartCoroutine(ShowMenuCanvas());
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
    public IEnumerator FadeTextToFullAlpha(float f, Text t)
    {
        t.color = new Color(t.color.r, t.color.g, t.color.b, 0);
        while (t.color.a < 1.0f)
        {
            t.color = new Color(t.color.r, t.color.g, t.color.b, t.color.a + (Time.deltaTime / f));
            if(t.color.a >= 0.5)
            {
                respawnable = true;
            }
            yield return null;
        }
    }

    public void GameOver()
    {
        respawnable = false;
        damagePPEffect.GetComponent<PostProcessVolume>().weight = 0;
        player.GetComponent<PlayerBehaviour>().setLifePoints(100f);
        player.transform.position = respawnLocation;
        player.GetComponent<My_FPSController>().enabled = false;
        gameOverCanvas.SetActive(true);
        StartCoroutine(FadeTextToFullAlpha(10f, gameOverCanvas.transform.GetChild(1).gameObject.GetComponent<Text>()));
        //stop music
        waveSound.Stop();
        pianoSound.Stop();
    }

    /* ---- AWAKE FUNCTIONS ---- */

    private IEnumerator EyeOpen()
    {
        for(int i = 0; i < 2; i++)
        {
            float targetWeight = (0.9f - (i/10f));
            while (eye.weight > targetWeight)
            {
                eye.weight -= 0.005f;
                yield return new WaitForSeconds(0.05f);
            }

            yield return new WaitForSeconds(0.05f);
            eye.weight = 1;
            yield return new WaitForSeconds(0.05f);
            eye.weight = (0.9f - (i/10f));
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
        while(speaker.volume < 0.15f)
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

        //alarm clock sound setup
        speaker.clip = alarmClock;
        speaker.Play();
        StartCoroutine(clockVolumeUp());
        //eye effect setup 
        eye = wakeUpPPEffect.GetComponent<PostProcessVolume>();
        eye.weight = 1;
        //text setup
        tutorialText.text = "PREMI IL TASTO AZIONE [E] PER ALZARTI";
    }

    public void QuitFromTicket()
    {
        if (phase == 2)
        {
            phase++;
            player.GetComponent<PlayerBehaviour>().TriggerAnimation((int)PlayerBehaviour.animations.Watch);
            StartCoroutine(pb.SwitchCameraParentInAnimations(2f));
            menuCanvas.SetActive(false);
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
        StartCoroutine(FadeOutToBlack(blackScreenCanvas.GetComponentInChildren<Image>(), 3f)); ;

        //start video
        this.GetComponent<VideoPlayer>().clip = endLevelVideo;
        StartCoroutine(StartEndVideo());
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
            t.color = new Color(t.color.r, t.color.g, t.color.b, t.color.a + (Time.deltaTime / time));
            yield return null;
        }
        
    }

    private IEnumerator FadeInByBlack(Image t, float time)
    {
        t.color = new Color(t.color.r, t.color.g, t.color.b, 1);
        while (t.color.a > 0f)
        {
            t.color = new Color(t.color.r, t.color.g, t.color.b, t.color.a - (Time.deltaTime / time));
            yield return null;
        }

    }

    private IEnumerator StartEndVideo()
    {
        yield return new WaitForSeconds(5f);
        speaker.volume = 0.5f;
        this.GetComponent<VideoPlayer>().Play();
        //blackScreenCanvas.SetActive(false);
        StartCoroutine(LoadNewSceneAfterVideo());
    }

    private IEnumerator LoadNewSceneAfterVideo()
    {
        yield return new WaitForSeconds(13f);
        SceneManager.LoadScene(2);
    }

}
