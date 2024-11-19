using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscButtons : MonoBehaviour
{
    public void Lobby()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void ResetButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
