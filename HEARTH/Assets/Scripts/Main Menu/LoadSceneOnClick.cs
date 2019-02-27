using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadSceneOnClick : MonoBehaviour
{
    public GameObject loadingScreen;

    public void LoadByIndex(int sceneIndex)
    {
        //SceneManager.LoadScene(sceneIndex);
        StartCoroutine(LoadSceneAsyncronous(sceneIndex));
    }

    private IEnumerator LoadSceneAsyncronous(int sceneIndex)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while (!async.isDone)
        {
            yield return null;
        }
    }
}
