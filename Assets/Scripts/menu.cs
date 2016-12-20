using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour {
    void start()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
    // Use this for initialization
    public void LoadScene()
    {
        SceneManager.LoadScene("scenepieter", LoadSceneMode.Single);

    }
}
