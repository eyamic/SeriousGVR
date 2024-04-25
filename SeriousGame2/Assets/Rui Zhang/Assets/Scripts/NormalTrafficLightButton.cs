using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalTrafficLightButton : MonoBehaviour
{
    public AudioSource TrifficSound;
    public GameObject trifficPanel;
    private bool playerInRange_Traffic = false;
    private bool trifficP = false;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&& trifficP == false)
        {
            playerInRange_Traffic = true;
            //Debug.Log("inRange");
            trifficPanel.SetActive(true);
            trifficP = true;
        }
       
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")&&trifficP == true)
        {
            playerInRange_Traffic = false;
            trifficPanel.SetActive(false);
            trifficP = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange_Traffic && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("play sound");
            TrifficSound.PlayDelayed(5.0f);
        }
    }
}
