using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeScene : MonoBehaviour {

    public void StartMain()
    {
        SceneManager.LoadScene("mainScene");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
