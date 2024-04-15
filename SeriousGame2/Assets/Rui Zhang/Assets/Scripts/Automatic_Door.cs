using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Automatic_Door : MonoBehaviour
{
    private Animator anim;

    private bool inRange = false;

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
            
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange && isOpen == false)
        {
            anim.SetTrigger("AutomaticOpen");
            isOpen = true;
        }
    }


}
