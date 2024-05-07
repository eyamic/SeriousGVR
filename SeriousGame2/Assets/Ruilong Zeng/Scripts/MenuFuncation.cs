using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuFuncation : MonoBehaviour
{
    public GameObject MenuFunctionCanvas; // Reference to the menu function canvas
    public GameObject OptionMenuCanvas; // Reference to the option menu canvas
    public GameObject UICanvas; // Reference to the UI canvas
    
    public Button PauseButton; // Button to pause the game
    public Button BacktoMenuButton; // Button to go back to the main menu
    public Button PauseBacktoGameButton; // Button to resume game from pause menu
    public Button OptionBacktoGameButton; // Button to return to game from option menu
    public Button QuitButton; // Button to quit the game
    public Button OptionButton; // Button to open option menu

    private void Start()
    {
        if (Time.timeScale == 0) // If game is paused when scene starts
        {
            Time.timeScale = 1; // Resume game
        }
    }

    private void Awake()
    {
        // Assigning event listeners to buttons
        BacktoMenuButton.onClick.AddListener(BacktoMenu);
        PauseBacktoGameButton.onClick.AddListener(PauseBacktoGame);
        OptionBacktoGameButton.onClick.AddListener(OptionBacktoGame);
        PauseButton.onClick.AddListener(PauseGame);
        QuitButton.onClick.AddListener(QuitGame);
        OptionButton.onClick.AddListener(Option);
    }

    // Method to open option menu
    void Option()
    {
        Time.timeScale = 0; // Pause game
        OptionMenuCanvas.SetActive(true); // Activate option menu canvas
        UICanvas.SetActive(false); // Deactivate UI canvas
    }

    // Method to quit the game
    void QuitGame()
    {
        Application.Quit(); // Quit the application
    }

    // Method to pause the game
    void PauseGame()
    {
        Time.timeScale = 0; // Pause game
        UICanvas.SetActive(false); // Deactivate UI canvas
        MenuFunctionCanvas.SetActive(true); // Activate menu function canvas
    }

    // Method to resume game from pause menu
    void PauseBacktoGame()
    {
        Time.timeScale = 1; // Resume game
        UICanvas.SetActive(true); // Activate UI canvas
        MenuFunctionCanvas.SetActive(false); // Deactivate menu function canvas
    }

    // Method to return to game from option menu
    void OptionBacktoGame()
    {
        Time.timeScale = 1; // Resume game
        UICanvas.SetActive(true); // Activate UI canvas
        OptionMenuCanvas.SetActive(false); // Deactivate option menu canvas
    }

    // Method to go back to the main menu
    void BacktoMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Load the main menu scene
    }
}
