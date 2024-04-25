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
        if (Input.GetKeyDown(KeyCode.E)&&talkArea)
        {
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
            Debug.Log("1");
            busPass.Play();
        }

        if (numOfTalk == 2)
        {
            Debug.Log("2");
            busPass.Play();
        }

        if (numOfTalk ==3 )
        {
            talkOver = true;
        }
    }
}

