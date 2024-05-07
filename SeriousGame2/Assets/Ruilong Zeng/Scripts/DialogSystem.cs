using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogSystem : MonoBehaviour
{
   [Header("UI")] public TextMeshProUGUI TextLabel; // Text label for displaying dialog
    [Header("TextFile")] public TextAsset myTextFile; // Text asset containing dialog
    public int index; // Index to keep track of current dialog
    public float TextShowSpeed; // Speed at which text is displayed

    private List<string> textlist = new List<string>(); // List to store dialog lines
    [Header("game Variable")]
    public GameObject talkCanvas; // Canvas for displaying dialog

    private bool TextFinished; // Flag to indicate if text display is finished

    private void Awake()
    {
        GetTextFromFile(myTextFile); // Load dialog from text file
        index = 0; // Set initial dialog index
    }

    public void OnEnable()
    {
        TextFinished = true; // Set text finished flag to true
        StartCoroutine(SetText()); // Start displaying text
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && index == textlist.Count) // If left mouse button clicked and reached end of dialog
        {
            talkCanvas.SetActive(false); // Disable dialog canvas
            index = 0; // Reset dialog index
            return;
        }

        if (Input.GetMouseButtonDown(0) && TextFinished) // If left mouse button clicked and text display finished
        {
            StartCoroutine(SetText()); // Start displaying next text
        }
    }

    void GetTextFromFile(TextAsset file)
    {
        textlist.Clear(); // Clear existing dialog text
        index = 0; // Reset dialog index

        var linedata = file.text.Split('\n'); // Split text asset into lines
        foreach (var line in linedata)
        {
            textlist.Add(line); // Add each line of dialog to the list
        }
    }

    IEnumerator SetText()
    {
        TextFinished = false; // Set text finished flag to false
        TextLabel.text = ""; // Clear text label
        for (int i = 0; i < textlist[index].Length; i++)
        {
            TextLabel.text += textlist[index][i]; // Display each character of dialog text

            yield return new WaitForSeconds(TextShowSpeed); // Wait for text show speed
        }

        TextFinished = true; // Set text finished flag to true

        index++; // Move to next dialog line
    }
}