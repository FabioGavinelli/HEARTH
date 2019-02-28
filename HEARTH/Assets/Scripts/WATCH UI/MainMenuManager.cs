using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{

    public GameObject ticketAlarmPanel;
    public GameObject hourPanel;
    public GameObject taskPanel;
    private Vector3 finalSize = new Vector3(0.00647068f, 0.004290778f, 0.005296017f);
    private Vector3 initialSize = new Vector3(0f, 0f, 0f);
    private string task;
    private int minutes = 3;
    private int hour;

    private void OnEnable()
    {
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 1:
                hour = 15;
                minutes = 3;
                task = "find somenthing to repair that boat and leave this island";
                break;
            case 2:
                hour = 18;
                minutes = 24;
                task = "find a way out from this maze on the plastic island";
                break;
            case 3:
                hour = 20;
                minutes = 11;
                task = "reach the spaceship located on the other side of the city";
                break;
            default:
                break;
        }


        this.transform.localScale = initialSize;
        minutes += (int)Time.realtimeSinceStartup/60;
        if(minutes >= 60)
        {
            hour++;
            minutes = 0;
        }
        if (minutes / 10 < 1)
        {
            hourPanel.GetComponent<Text>().text = hour + ":0" + minutes;
        }
        else
        {
            hourPanel.GetComponent<Text>().text = hour +  ":" + minutes;
        }

        taskPanel.GetComponentInChildren<Text>().text = task;

        StartCoroutine(TurnOnOlograms());
    }

    private IEnumerator TurnOnOlograms()
    {
        yield return new WaitForSeconds(1f);
        this.transform.DOScale(finalSize, 1f);
    }

    public void DisableTicketReminder()
    {
        ticketAlarmPanel.SetActive(false);
    }
}
