using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class WaterDamage : MonoBehaviour {

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject damagePPEffect;
    [SerializeField] private GameObject gameOverController;
    [SerializeField] private float damage;
    [SerializeField] private float heal;
    [SerializeField] private float damageDelay;
    [SerializeField] private float healingDelay;
    private PostProcessVolume ppVolume;
    private bool invicible = false; //true = the player can't take damage, false = the player can take damage

    private void Start()
    {
        ppVolume = damagePPEffect.GetComponent<PostProcessVolume>();
    }

    private IEnumerator DamagePlayer()
    {
        while (player.GetComponent<PlayerBehaviour>().getLifePoints() > 0 && invicible == false)
        {
            player.GetComponent<PlayerBehaviour>().Damage(damage);
            ppVolume.weight += (1f / (125/damage));
            yield return new WaitForSeconds(damageDelay);
        }
        invicible = true;
        if (player.GetComponent<PlayerBehaviour>().getLifePoints() <= 0)
        {
            damagePPEffect.GetComponent<PostProcessVolume>().weight = 0;
            //gameOverController.GetComponent<GameOver_Controller>().GameOver();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            invicible = false;
            if (ppVolume.weight < 0.5) ppVolume.weight = 0.5f;
            StartCoroutine(DamagePlayer());
        }
       
    }

    private IEnumerator WaitForHeal()
    {
        yield return new WaitForSeconds(4f);
        if (invicible) StartCoroutine(HealPlayer());
    }

    private IEnumerator HealPlayer()
    {
        while (player.GetComponent<PlayerBehaviour>().getLifePoints() <= 100 && invicible == true)
        {
            player.GetComponent<PlayerBehaviour>().Heal(heal);
            ppVolume.weight -= (1f / (100 / heal));
            yield return new WaitForSeconds(healingDelay);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            invicible = true;
            StartCoroutine(WaitForHeal());
        } 
    }
}
