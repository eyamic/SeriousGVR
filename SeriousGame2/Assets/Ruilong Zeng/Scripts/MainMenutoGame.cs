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
    public GameObject ChoseCnavas;
    public Button start;
    public Button Quit;
    public Button About;
    public Button closeAboutCanvansButton;
    public Button CloseChoseCanvasButton;
    public Button keyboradButton;
    public Button ContorllerButton;


    private void Start()
    {
        start.onClick.AddListener(StartGame);
        Quit.onClick.AddListener(QuitGame);
        About.onClick.AddListener(AboutGame);
        closeAboutCanvansButton.onClick.AddListener(CloseAboutCanvans);
        CloseChoseCanvasButton.onClick.AddListener(CloseChoseCanvans);
        keyboradButton.onClick.AddListener(Keyboard);
        ContorllerButton.onClick.AddListener(controller);
    }

    void StartGame()
    {
        ChoseCnavas.SetActive(true);
    }

    void QuitGame()
    {
        Application.Quit();
    }

    void AboutGame()
    {
        AboutCanvans.SetActive(true);
    }

    void Keyboard()
    {
        SceneManager.LoadScene("Total4_Rui");
    }

    void controller()
    {
        SceneManager.LoadScene("Total5");
    }

    void CloseAboutCanvans()
    {
        AboutCanvans.SetActive(false);
    }
    void CloseChoseCanvans()
    {
        ChoseCnavas.SetActive(false);
    }
}
