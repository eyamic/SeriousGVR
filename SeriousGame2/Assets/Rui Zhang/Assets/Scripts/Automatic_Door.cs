using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Automatic_Door : MonoBehaviour
{
    private Animator anim;

    private bool isOpen = false;//门的状态
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isOpen)//门是关的
            {
                anim.SetTrigger("AutomaticOpen");
                isOpen = true;//设为“打开的门”
            }
            else//门是开的
            {
                anim.SetTrigger("AutomaticClose");
                isOpen = false;//设为“关上的门”
            }
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
