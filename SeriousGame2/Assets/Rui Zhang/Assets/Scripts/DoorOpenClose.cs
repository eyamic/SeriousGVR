using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenClose : MonoBehaviour
{
    private Animator anim;
    private bool inRange = false;//门的状态
    private bool isOpen = false;
    public GameObject doorP;
    private float delayTime = 2f;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    /* void Update()
     {
         if (Input.GetKeyDown(KeyCode.E))
         {
             if (!isOpen)//门是关的
             {
                 anim.SetTrigger("Open");
                 isOpen = true;//设为“打开的门”
             }
             else//门是开的
             {
                 anim.SetTrigger("Close");
                 isOpen = false;//设为“关上的门”
             }
         }
     }*/

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&& Input.GetKeyDown(KeyCode.E))
        {
            if (!isOpen)//门是关的
            {
                anim.SetTrigger("Open");
                isOpen = true;//设为“打开的门”
            }
            else//门是开的
            {
                anim.SetTrigger("Close");
                isOpen = false;//设为“关上的门”
            }
        }
    }*/


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&&inRange==false)
        {
            inRange = true;
            doorP.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")&&inRange)
        {
            inRange = false;
            doorP.SetActive(false);
        }
    }
    private void Update()
    {
        if (inRange ==true&&Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("play");
            anim.SetTrigger("Open");
            isOpen = true;
        }

        if (inRange == false && isOpen == true)
        {
            Invoke("CloseDoor", delayTime);
            isOpen = false;
        }
    }

    void CloseDoor()
    {
        anim.SetTrigger("Close");
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
