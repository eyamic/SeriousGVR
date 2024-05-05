using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Gameover : MonoBehaviour
{
    public TextMeshProUGUI TextLabel;
    public TextAsset textFile;
    public int index;
    public float TextSpeed;
    List<string> textList = new List<string>();
    private bool textFished;
    public Button BackTomenuButton;


    void Awake()
    {
        GetTextFromFile(textFile);
        BackTomenuButton.onClick.AddListener(BackTomenu);
    }

    void BackTomenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void OnEnable()
    {
        textFished = true;
        StartCoroutine(SetTextUI());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && index == textList.Count)
        {
            index = 0;
            return;
        }

        if (Input.GetMouseButtonDown(0) && textFished)
        {
            StartCoroutine(SetTextUI());
        }
    }

    void GetTextFromFile(TextAsset file)
    {
        textList.Clear();
        index = 0;
        var linedate = file.text.Split('\n');

        foreach (var line in linedate)
        {
            textList.Add(line);
        }
    }

    IEnumerator SetTextUI()
    {
        textFished = false;
        TextLabel.text = "";

        for (int i = 0; i < textList[index].Length; i++)
        {
            TextLabel.text += textList[index][i];
            yield return new WaitForSeconds(TextSpeed);
        }

        textFished = true;
        index++;
    }
}
