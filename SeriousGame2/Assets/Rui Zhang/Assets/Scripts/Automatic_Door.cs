using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Automatic_Door : MonoBehaviour
{
    private Animator anim;

    private bool playerAutomaticDoor = false;

    private bool isOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("inRange");
            playerAutomaticDoor = true;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerAutomaticDoor == true )
        {
            if (!isOpen)
            {
                anim.SetTrigger("AutomaticOpen");
                isOpen = true;
            }
            // else if (playerAutomaticDoor != true)
            // {
            //     anim.SetTrigger("AutomaticClose");
            //     isOpen = false;
            // }
        }
    }
    //private void OnTriggerStay
    //{
    //if(other.CompareTag("Player"))
    // {
    //isOpen = true;
    // }

    //private void OnTriggerExit
    //{
    //if(other.CompareTag("Player"))
    //{
    //isOpen = false;
    //}    

    //}


}
