using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_Flicker : MonoBehaviour
{
    [SerializeField] private Light light;

    // Start is called before the first frame update
    void Update()
    {
            StartCoroutine(Flick());
    }

    private IEnumerator Flick()
    {
            light.intensity = Random.Range(0.5f, 1.5f);
            yield return null; 
    }
}
