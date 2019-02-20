using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{

    public GameObject ticketAlarmPanel;
    public GameObject hourPanel;
    private Vector3 finalSize = new Vector3(0.00647068f, 0.004290778f, 0.005296017f);
    private Vector3 initialSize = new Vector3(0f, 0f, 0f);
    private int minutes = 3;

    private void OnEnable()
    {
        this.transform.localScale = initialSize;
        minutes += (int)Time.realtimeSinceStartup/60;
        hourPanel.GetComponent<Text>().text = "17:" + minutes; 
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
