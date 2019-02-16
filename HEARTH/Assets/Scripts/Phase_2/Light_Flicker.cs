using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_Flicker : MonoBehaviour
{
    [SerializeField] private Light light;
    [SerializeField] private GameObject rightEye;
    [SerializeField] private GameObject leftEye;
    [SerializeField][Range(0f, 1f)] private float flickFrequency = 0.2f;
    [SerializeField][Range(0f, 10f)] private float maxIntensityValue = 1.5f;
    private bool flick = true;

    // Start is called before the first frame update
    void Update()
    {
        if (flick)
        {
            flick = false;
            StartCoroutine(Flick());
        }
    }

    private IEnumerator Flick()
    {
        float rnd = Random.Range(0f, maxIntensityValue);
        light.intensity = rnd;

        if(rnd >= maxIntensityValue / 2)
        {
            rightEye.SetActive(true);
            leftEye.SetActive(true);
        }
        else
        {
            rightEye.SetActive(false);
            leftEye.SetActive(false);
        }
        
        yield return new WaitForSeconds(flickFrequency);
        flick = true;
    }
}
