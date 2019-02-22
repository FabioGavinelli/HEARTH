using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewGame : MonoBehaviour
{
    public void Load()
    {
        SceneManager.LoadScene(0);
    }
}
