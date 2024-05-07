using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenutoGame : MonoBehaviour
{
    // References to UI elements and canvases
    public GameObject Canvas;
    public GameObject AboutCanvas;
    public GameObject ChooseCanvas;
    public Button start;
    public Button Quit;
    public Button About;
    public Button closeAboutCanvasButton;
    public Button CloseChooseCanvasButton;
    public Button keyboardButton;
    public Button controllerButton;

    // Start is called before the first frame update
    private void Start()
    {
        // Assigning event listeners to buttons
        start.onClick.AddListener(StartGame);
        Quit.onClick.AddListener(QuitGame);
        About.onClick.AddListener(AboutGame);
        closeAboutCanvasButton.onClick.AddListener(CloseAboutCanvas);
        CloseChooseCanvasButton.onClick.AddListener(CloseChooseCanvas);
        keyboardButton.onClick.AddListener(Keyboard);
        controllerButton.onClick.AddListener(Controller);
    }

    // Method to start the game
    void StartGame()
    {
        ChooseCanvas.SetActive(true); // Activate the choose canvas
    }

    // Method to quit the game
    void QuitGame()
    {
        Application.Quit(); // Quit the application
    }

    // Method to show information about the game
    void AboutGame()
    {
        AboutCanvas.SetActive(true); // Activate the about canvas
    }

    // Method to load the game scene with keyboard controls
    void Keyboard()
    {
        SceneManager.LoadScene("Total4_Rui"); // Load the scene with keyboard controls
    }

    // Method to load the game scene with controller controls
    void Controller()
    {
        SceneManager.LoadScene("Total5"); // Load the scene with controller controls
    }

    // Method to close the about canvas
    void CloseAboutCanvas()
    {
        AboutCanvas.SetActive(false); // Deactivate the about canvas
    }

    // Method to close the choose canvas
    void CloseChooseCanvas()
    {
        ChooseCanvas.SetActive(false); // Deactivate the choose canvas
    }
}
