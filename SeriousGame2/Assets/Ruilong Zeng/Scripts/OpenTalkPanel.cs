using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenTalkPanel : MonoBehaviour
{
    public GameObject talkCanvas;
    public Button CloseButton;

    public void Update()
    {
        CloseButton.onClick.AddListener(CloseCanvas);
    }

    public void OnMouseDown()
    {
        talkCanvas.SetActive(true);
    }

    void CloseCanvas()
    {
        talkCanvas.SetActive(false);
    }
    
}
