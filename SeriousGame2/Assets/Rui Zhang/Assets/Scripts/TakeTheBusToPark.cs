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
    // Start is called before the first frame update
    void Start()
    {
        endPointRigidbody = endPoint.GetComponent<Rigidbody>();
        origianlScale = busPlayer.transform.localScale;

    }
    // Update is called once per frame
    void Update()
    {
        if (busMoving == true)
        {

            this.transform.Translate(Vector3.forward * busSpeed * Time.deltaTime);
            //busPlayer.transform.localPosition = new Vector3(0, 0, 0);

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && hasTrigger == false)
        {
            busMoving = true;
            busPlayer.transform.localScale = origianlScale;
            busPlayer.transform.parent = transform;
            hasTrigger = true;
        }
        else if (other.CompareTag("EndPoint"))
        {
            busMoving = false;
            origianlScale = busPlayer.transform.localScale;
            busPlayer.transform.parent = null;
            Destroy(endPointRigidbody);

        }
    }




}
