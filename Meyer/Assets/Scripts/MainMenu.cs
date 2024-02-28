using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Gamemode1()
    {
        SceneManager.LoadScene("GameNormal");
    }
    public void Gamemode2()
    {
        SceneManager.LoadScene("GameAI");
    }
}
