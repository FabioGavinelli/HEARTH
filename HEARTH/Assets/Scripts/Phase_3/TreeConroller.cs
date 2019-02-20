using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeConroller : MonoBehaviour
{
    [SerializeField] private bool contaminatedAir = true;
    [SerializeField] private GameObject player;
    private PlayerBehaviour pb;

    void Start()
    {
        pb = player.GetComponent<PlayerBehaviour>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            ContaminatedAir(false);
            StopCoroutine(AirDamage());
            StartCoroutine(HealPlayer());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            ContaminatedAir(true);
            StopCoroutine(HealPlayer());
            StartCoroutine(AirDamage());
        } 
    }

    private IEnumerator AirDamage()
    {
        while (pb.lifePoints > 0)
        {
            Debug.Log("damaging");
            pb.Damage(5f);
            yield return new WaitForSeconds(0.25f);
        }
    }

    private IEnumerator HealPlayer()
    {
        while (pb.lifePoints < 100)
        {
            Debug.Log("healing");
            pb.Heal(1f);
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void ContaminatedAir(bool state)
    {
        contaminatedAir = state;
    }
}
