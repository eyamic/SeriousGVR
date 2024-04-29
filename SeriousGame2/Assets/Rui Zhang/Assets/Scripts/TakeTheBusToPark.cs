using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeTheBusToPark : MonoBehaviour
{
    public float busSpeed = 10.0f;
    public GameObject endPoint;
    public GameObject busPlayer;
    private Rigidbody endPointRigidbody;
    private bool busMoving = false;
    private bool hasTrigger = false;
    private Vector3 origianlScale;

    public Collider talkArea;
    private bool talkOver = false;
    private int numOfTalk = 0;

    private Collider myCollider;
    //public Collider stopCollider;

    public AudioSource busArrive;
    public AudioSource busInside;
    public AudioSource busStart;
    public AudioSource busPass;
    public AudioSource busArrivePark;
    public AudioSource driverAnswer;
    public AudioSource driverAnswer_ok;

    public GameObject hintPanel;
    public GameObject askPanel;
    public GameObject hintPanel2;


    void Start()
    {
        endPointRigidbody = endPoint.GetComponent<Rigidbody>();
        //origianlScale = busPlayer.transform.localScale;
        myCollider = GetComponent<Collider>();

    }

    void Update()
    {
        if (busMoving == true)
        {
            this.transform.Translate(Vector3.right* -busSpeed * Time.deltaTime);
            //busPlayer.transform.localPosition = new Vector3(0, 0, 0);
        }
        if(talkArea)
        {
            Invoke("ShowHintPanel",1.0f);
            Invoke("HideHintPanel", 2.0f);

        }

        if (Input.GetKeyDown(KeyCode.E)&&talkArea)
        {
            askPanel.SetActive(true);
            numOfTalk++;
            TalkToBus();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (talkOver && other.CompareTag("Player") && hasTrigger == false)
        {
            StartBusMove();
            busStart.Play();
            busInside.PlayDelayed(0.5f);
            //stopCollider.enabled = true;
        }
        else if (other.CompareTag("EndPoint"))
        {
            StopBusMove();
            myCollider.enabled = false;
            busInside.enabled = false;
            busArrivePark.Play();

        }

       

    }

    public void StartBusMove()
    {
        busMoving = true;
        //busPlayer.transform.localScale = origianlScale;
        busPlayer.transform.parent = transform;
    }

    public void StopBusMove()
    {
        busMoving = false;
       // origianlScale = busPlayer.transform.localScale;
        busPlayer.transform.parent = null;
        Destroy(endPointRigidbody);
    }

    public void TalkToBus()
    {
        busArrive.PlayDelayed(1.0f);

        if (numOfTalk == 1)
        {
            driverAnswer.PlayDelayed(1.5f);
            busPass.Play();
        }

        if (numOfTalk == 2)
        {
            Invoke("ShowHintPanel2",1.0f);
            Invoke("HideHintPanel2", 2.0f);
            busPass.Play();
        }

        if (numOfTalk ==3 )
        {
            talkOver = true;
            driverAnswer_ok.PlayDelayed(1.0f);
        }
    }

    public void ShowHintPanel()
    {
        hintPanel.SetActive(true);
    }

    public void HideHintPanel()
    {
        hintPanel.SetActive(false);
    }

    public void ShowHintPanel2()
    {
        hintPanel2.SetActive(true);

    }

    public void HideHintPanel2()
    {
        hintPanel2.SetActive(false);
    }
}

