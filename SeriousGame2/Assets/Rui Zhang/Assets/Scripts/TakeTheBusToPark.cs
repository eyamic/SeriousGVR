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
    // Start is called before the first frame update
    void Start()
    {
        endPointRigidbody = endPoint.GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        if (busMoving == true)
        {
            this.transform.Translate(Vector3.forward * busSpeed * Time.deltaTime);
            busPlayer.transform.localPosition = new Vector3(0, 0, 0);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && hasTrigger == false)
        {
            busMoving = true;
            busPlayer.transform.parent = transform;
            hasTrigger = true;
        }
        else if (other.CompareTag("EndPoint"))
        {
            busMoving = false;
            busPlayer.transform.parent = null;
            Destroy(endPointRigidbody);

        }
    }




}
