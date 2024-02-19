using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogSystem : MonoBehaviour
{
    [Header("UI")] public TextMeshProUGUI TextLabel;
    [Header("TextFile")] public TextAsset myTextFile;
    public int index;
    public float TextShowSpeed;

    private List<string> textlist = new List<string>();
    [Header("game Variable")]
    public GameObject talkCanvas;

    private bool TextFinished;
    

    private void Awake()
    {
        GetTextFromFile(myTextFile);
        index = 0;
    }

    public void OnEnable()
    {
        TextFinished = true;
        StartCoroutine(SetText());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)&&index==textlist.Count)
        {
            talkCanvas.SetActive(false);
            index = 0;
            return;

        }
        if (Input.GetMouseButtonDown(0)&&TextFinished)
        {
            StartCoroutine(SetText());
        }
    }

    void GetTextFromFile(TextAsset file)
    {
        textlist.Clear();
        index = 0;

        var linedata=file.text.Split('\n');
        foreach (var line in linedata)
        {
            textlist.Add(line);
        }
    }

    IEnumerator SetText()
    {
        TextFinished = false;
        TextLabel.text = "";
        for (int i = 0; i < textlist[index].Length; i++)
        {
            TextLabel.text += textlist[index][i];

            yield return new WaitForSeconds(TextShowSpeed);
        }

        TextFinished = true;

        index++;
    }
}