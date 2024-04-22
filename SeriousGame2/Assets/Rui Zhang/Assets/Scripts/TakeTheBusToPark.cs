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

    //private Collider myCollider;
    //public Collider stopCollider;


    void Start()
    {
        endPointRigidbody = endPoint.GetComponent<Rigidbody>();
        //origianlScale = busPlayer.transform.localScale;
        //myCollider = GetComponent<Collider>();

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
            //myCollider.enabled = false;
            //stopCollider.enabled = true;
        }
        else if (other.CompareTag("EndPoint"))
        {
            StopBusMove();
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
        if (numOfTalk == 1)
        {
            Debug.Log("1");
        }

        if (numOfTalk == 2)
        {
            Debug.Log("2");
        }

        if (numOfTalk ==3 )
        {
            talkOver = true;
        }
    }
}

