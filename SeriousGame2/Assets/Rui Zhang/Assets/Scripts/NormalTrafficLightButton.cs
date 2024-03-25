using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalTrafficLightButton : MonoBehaviour
{
    public AudioSource TrifficSound;
    private bool playerInRange_Traffic = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange_Traffic = true;
            Debug.Log("inRange");
        }
       
    }


    // Update is called once per frame
    void Update()
    {
        if (playerInRange_Traffic == true && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("play sound");
            //TrifficSound.PlayDelayed(5.0f);
        }
    }
}
