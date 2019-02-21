using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeConroller : MonoBehaviour
{
    private GameObject player;
    private PlayerBehaviour pb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pb = player.GetComponent<PlayerBehaviour>();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            pb.setSafe(true);
            StopCoroutine(AirDamage());
            StartCoroutine(HealPlayer());
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
        } 
    }

    private IEnumerator AirDamage()
    {
        while (pb.lifePoints > 0 && (pb.getSafe() == false))
        {
            Debug.Log("damaging");
            pb.Damage(5f);
            yield return new WaitForSeconds(0.25f);
        }
    }

    private IEnumerator HealPlayer()
    {
        while (pb.lifePoints < 100 && (pb.getSafe() == true))
        {
            Debug.Log("healing");
            pb.Heal(1f);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
