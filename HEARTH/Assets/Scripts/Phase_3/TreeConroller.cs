using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeConroller : MonoBehaviour
{
    private GameObject player;
    private PlayerBehaviour pb;
    private AudioSource audio;
    [SerializeField] AudioClip[] consciousness;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pb = player.GetComponent<PlayerBehaviour>();
        //audio = this.GetComponent<AudioSource>();
        audio = GameObject.FindGameObjectWithTag("Consciousness").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            pb.setSafe(true);
            StopCoroutine(AirDamage());
            StartCoroutine(HealPlayer());
            //audio.Stop();
            audio.clip = consciousness[1];
            audio.Play();

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            pb.setSafe(true);
            //StopCoroutine(AirDamage());
            //StartCoroutine(HealPlayer());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            pb.setSafe(false); 
            StopCoroutine(HealPlayer());
            StartCoroutine(AirDamage());
            //audio.Stop();
            audio.clip = consciousness[0];
            audio.Play();
        } 
    }

    private IEnumerator AirDamage()
    {
        while (pb.lifePoints > 0 && (pb.getSafe() == false))
        {
            pb.Damage(1f);
            yield return new WaitForSeconds(0.05f);
        }
    }

    private IEnumerator HealPlayer()
    {
        while (pb.lifePoints < 100 && (pb.getSafe() == true))
        {
            pb.Heal(1f);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
