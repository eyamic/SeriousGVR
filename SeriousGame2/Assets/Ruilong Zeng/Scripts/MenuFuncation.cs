using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuFuncation : MonoBehaviour
{
    public GameObject MenuFunctionCanvas;
    public GameObject OptionMenuCanvans;
    public GameObject UICanvas;
    
    public Button PauseButton;
    public Button BacktomenuButton;
    public Button PauseBacktoGameButton;
    public Button OptionBacktoGameButton;
    public Button QuitButton;
    public Button OptionButton;


    private void Awake()
    {
        BacktomenuButton.onClick.AddListener(BacktoMenu);
        PauseBacktoGameButton.onClick.AddListener(PasueBacktoGame);
        OptionBacktoGameButton.onClick.AddListener(OptionBacktoGame);
        PauseButton.onClick.AddListener(PasueGame);
        QuitButton.onClick.AddListener(QuitGame);
        OptionButton.onClick.AddListener(Option);
    }

    void Option()
    {
        OptionMenuCanvans.SetActive(true);
        UICanvas.SetActive(false);
    }
    void QuitGame()
    {
        Application.Quit();
    }

    void PasueGame()
    
    {
        Time.timeScale = 0;
        UICanvas.SetActive(false);
        MenuFunctionCanvas.SetActive(true);
    }

    void PasueBacktoGame()
    {
        Time.timeScale = 1;
        UICanvas.SetActive(true);
        MenuFunctionCanvas.SetActive(false);
    }
    void OptionBacktoGame()
    {
        Time.timeScale = 1;
        UICanvas.SetActive(true);
        OptionMenuCanvans.SetActive(false);
    }

    void BacktoMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
