using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public void MainMenuBtn()
    {
        SceneManager.LoadScene(0);
    }
    public void PlayAgainBtn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
