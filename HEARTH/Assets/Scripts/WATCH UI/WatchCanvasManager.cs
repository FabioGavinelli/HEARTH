using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchCanvasManager : MonoBehaviour {

    public GameObject ticketCanvas;
    public GameObject UICanvas;
    private bool firstTime = true;
    

	// Use this for initialization
	void Start () {
        ticketCanvas.SetActive(false);
        UICanvas.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            //se UI chiusa la apre 
            if (!ticketCanvas.activeSelf && !UICanvas.activeSelf)
            {
                //animazione polso???
                showUI();
            }

            //se UI già aperta la chiude
            else
            {
                hideUI();
            }

        }
	}



    public void showUI()
    {
        //se è la prima volta che clicca tab mostra il biglietto in grande
        if (firstTime)
        {
            firstTime = false;
            ticketCanvas.SetActive(true);
        }
        //altrimenti mostra UI normale
        else
        {
            UICanvas.SetActive(true);
        }
    }

    public void hideUI()
    {
        //se è aperto il biglietto lo chiude e mostra ui normale
        if (ticketCanvas.activeSelf)
        {
            ticketCanvas.SetActive(false);
            UICanvas.SetActive(true);
        }
        //se è aperta UI normale la chiude 
        else
        {
            UICanvas.SetActive(false);
        }
    }

   

}
