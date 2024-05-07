using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class TalkDoctor : MonoBehaviour
{
    // Declares multiple AudioSource variables for different segments of the conversation.
    public AudioSource player_dialogue1;
    public AudioSource doctor_dialogue1;
    public AudioSource player_dialogue2;
    public AudioSource doctor_dialogue2;
    public AudioSource thankYou;
///UI panels for displaying hints or information.
    public GameObject hintPanel;
    public GameObject hintPanel2;
    public GameObject hintPanel3;
// Counter for how many times the player has initiated conversation.
    private bool inRange;
    private bool dialogue1Over;
    private bool dialogue2Over;
    public bool talkOver;
   
    private int numOfTalk;// Counter for how many times the player has initiated conversation.

    private PlayerControls controls; // Reference to the custom input action setup.


   //private PlayerControls controls;  // 控制器引用

    void Awake() {
        controls = new PlayerControls(); // Instantiate the Input Actions.
    }

    void OnEnable() {
        controls.Enable();// Enable the input system's control scheme.
        controls.Gameplay.Doctor.performed += HandleTalkControl;  
    }

    void OnDisable() {
        controls.Gameplay.Doctor.performed -= HandleTalkControl;
        controls.Disable();  
    }

    void Start() {
        // Initializes all conversation state flags and the talk counter.
        inRange = false;
        dialogue1Over = false;
        dialogue2Over = false;
        talkOver = false;
        numOfTalk = 0;
    }

    private void HandleTalkControl(InputAction.CallbackContext context) {
        // Handles the input for talking when the player is in range.
        if (inRange) {
            numOfTalk++;
            BeginTalk();
            Debug.Log("对话增加");
            hintPanel.SetActive(false);  // 隐藏提示面板 Hide the initial hint panel.  
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            inRange = true;
            hintPanel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other) {
        inRange = false;
        hintPanel.SetActive(false);
        hintPanel2.SetActive(false);
        hintPanel3.SetActive(false);
    }

    private void BeginTalk() {
        // 对话逻辑，根据对话次数控制不同阶段的播放Dialogue logic to control different stages of playback based on number of dialogues
        if (numOfTalk == 1 && inRange) {
            Debug.Log("开始对话");
            player_dialogue1.Play();
            doctor_dialogue1.PlayDelayed(5.0f);
            dialogue1Over = true;
            Invoke("ShowPanel", 18.0f);
            Invoke("HidePanel3", 20.0f);
        }
        if (dialogue1Over && numOfTalk == 2) {
            player_dialogue2.Play();
            doctor_dialogue2.PlayDelayed(3.0f);
            dialogue2Over = true;
            Invoke("ShowPanel", 9.0f);
            Invoke("HidePanel3", 10.0f);
        }
        if (dialogue2Over && numOfTalk == 3) {
            thankYou.Play();
            talkOver = true;
        }
        if (talkOver && inRange) {
            hintPanel3.SetActive(true);
            Invoke("HidePanel3", 2.0f);
        }
    }
    
    private void ShowPanel() {
        hintPanel2.SetActive(true);
    }
    private void HidePanel3() {
        hintPanel2.SetActive(false);
        hintPanel3.SetActive(false);
    }
}