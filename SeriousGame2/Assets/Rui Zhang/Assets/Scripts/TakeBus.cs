using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeBus : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public float speed = 1.0f;

    private float startTime;
    private float distanceLength;

    private bool canMove = false;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        distanceLength = Vector3.Distance(startPoint.position, endPoint.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            float distanceCovered = (Time.time - startTime) * speed;
            float fractionJourney = distanceCovered / distanceLength;

            transform.position = Vector3.Lerp(startPoint.position, endPoint.position, fractionJourney);
            Vector3 movementOffset = (endPoint.position - startPoint.position) * fractionJourney;
            player.position = startPoint.position + movementOffset;

            if (fractionJourney >= 1f)
            {
                Debug.Log("arrived");
                canMove = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
        
            startTime = Time.time;
            canMove = true;
        }
    }
}
