using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenutoGame : MonoBehaviour
{
    public GameObject Canvans;
    public GameObject AboutCanvans;
    public Button start;
    public Button Quit;
    public Button About;
    public Button closeAboutCanvansButton;


    private void Start()
    {
        start.onClick.AddListener(StartGame);
        Quit.onClick.AddListener(QuitGame);
        About.onClick.AddListener(AboutGame);
        closeAboutCanvansButton.onClick.AddListener(CloseAboutCanvans);
    }

    void StartGame()
    {
        SceneManager.LoadScene("Ruilong TestScene");
    }

    void QuitGame()
    {
        Application.Quit();
    }

    void AboutGame()
    {
        AboutCanvans.SetActive(true);
    }

    void CloseAboutCanvans()
    {
        AboutCanvans.SetActive(false);
    }
}
