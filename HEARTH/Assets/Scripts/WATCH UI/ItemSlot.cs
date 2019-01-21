using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour {
    
    //sostituisce all'immagine precedente la nuova - setta attivo lo slot
    public void UpdateSprite(GameObject objectToAdd) 
    {
        //estraggo l'immagine dell'oggetto che ho appena raccolto
        Image imageToAdd = objectToAdd.GetComponent<Image>();

        if (imageToAdd)
        {
            //salvo immagine nello slot
            this.GetComponent<Image>().sprite = imageToAdd.sprite;
            //abilito visualizzazione slot
            this.GetComponent<Image>().enabled = true;
        }
    }


}
