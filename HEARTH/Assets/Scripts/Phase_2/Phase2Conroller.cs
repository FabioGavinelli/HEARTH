using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Phase2Conroller : MonoBehaviour
{
    [SerializeField] private GameObject robot;
    /*[SerializeField] private GameObject player;
    [SerializeField] private GameObject gameOverCanvas;

    private bool respawnable = false;
    private Vector3 respawnLocation;
    */

    // Start is called before the first frame update
    void Start()
    {
        robot.GetComponent<RobotNavController>().enabled = false;
    }

    /*
    // Update is called once per frame
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
    }
    */

    /* ---- GAMEOVER FUNCTIONS ---- */
    /*
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
    }*/
    /*
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
    }*/
}
