using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LetterController : MonoBehaviour
{
    [SerializeField] private Image letter;
    [SerializeField] private Sprite instructions;
    [SerializeField] private GameObject closeLetter;
    private GameObject player;
    private bool front = true;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void FlipLetter()
    {
        letter.sprite = instructions;
        closeLetter.SetActive(true);
    }

    public void CloseLetter()
    {
        player.GetComponent<My_FPSController>().enabled = true;
        player.GetComponent<CharacterController>().enabled = true;
        player.GetComponent<PlayerBehaviour>().enabled = true;
        this.gameObject.SetActive(false);
        this.transform.parent.gameObject.SetActive(false);
    }
}
