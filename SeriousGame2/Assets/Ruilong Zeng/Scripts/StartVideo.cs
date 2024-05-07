using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class StartVideo : MonoBehaviour
{
    public GameObject blackPanel; // Reference to the black panel
    public GameObject Rawi; // Reference to the Rawi game object
    public VideoPlayer video; // Reference to the VideoPlayer component
    public GameObject playerStart; // Reference to the player start game object
    private float timeCount; // Time counter

    private void Awake()
    {
        video = GetComponentInChildren<VideoPlayer>(); // Get the VideoPlayer component
        blackPanel.SetActive(true); // Activate the black panel
        playerStart.SetActive(false); // Deactivate the player start game object
    }

    private void Update()
    {
        timeCount += Time.deltaTime; // Update time counter

        // Check if it's time to start the video
        if (timeCount >= 13f)
        {
            blackPanel.SetActive(false); // Deactivate the black panel
            video.Play(); // Start playing the video
            if (playerStart != null)
            {
                playerStart.SetActive(true); // Activate the player start game object
            }

            // Check if it's time to hide Rawi and stop the video
            if (timeCount >= 18)
            {
                Rawi.SetActive(false); // Deactivate Rawi
                video.Stop(); // Stop playing the video
            }
        }
    }
}