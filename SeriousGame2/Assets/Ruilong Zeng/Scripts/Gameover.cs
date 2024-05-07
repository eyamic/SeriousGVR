using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Gameover : MonoBehaviour
{
   public TextMeshProUGUI TextLabel; // Text label for displaying game over message
    public TextAsset textFile; // Text asset containing game over message
    public int index; // Index to keep track of current message
    public float TextSpeed; // Speed at which text is displayed
    List<string> textList = new List<string>(); // List to store game over messages
    private bool textFished; // Flag to indicate if text display is finished
    public Button BackTomenuButton; // Button to return to the main menu

    void Awake()
    {
        GetTextFromFile(textFile); // Load game over messages from text file
        BackTomenuButton.onClick.AddListener(BackTomenu); // Add listener to back to menu button
    }

    void BackTomenu()
    {
        SceneManager.LoadScene("MainMenu"); // Load the main menu scene
    }

    private void OnEnable()
    {
        textFished = true; // Set text finished flag to true
        StartCoroutine(SetTextUI()); // Start displaying game over message
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && index == textList.Count) // If left mouse button clicked and reached end of messages
        {
            index = 0; // Reset message index
            return;
        }

        if (Input.GetMouseButtonDown(0) && textFished) // If left mouse button clicked and text display finished
        {
            StartCoroutine(SetTextUI()); // Start displaying next message
        }
    }

    void GetTextFromFile(TextAsset file)
    {
        textList.Clear(); // Clear existing messages
        index = 0; // Reset message index
        var linedate = file.text.Split('\n'); // Split text asset into lines

        foreach (var line in linedate)
        {
            textList.Add(line); // Add each line of text to the list
        }
    }

    IEnumerator SetTextUI()
    {
        textFished = false; // Set text finished flag to false
        TextLabel.text = ""; // Clear text label

        for (int i = 0; i < textList[index].Length; i++)
        {
            TextLabel.text += textList[index][i]; // Display each character of message
            yield return new WaitForSeconds(TextSpeed); // Wait for text speed
        }

        textFished = true; // Set text finished flag to true
        index++; // Move to next message
    }
}
