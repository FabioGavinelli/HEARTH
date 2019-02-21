using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseTicket : MonoBehaviour
{
    [SerializeField] private GameController gc;

    public void close()
    {
        Debug.Log("chiudi biglietto");
        gc.QuitFromTicket();
    }
}
