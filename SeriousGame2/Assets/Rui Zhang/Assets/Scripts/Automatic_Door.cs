using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Automatic_Door : MonoBehaviour
{
    private Animator anim;

    private bool isOpen = false;//�ŵ�״̬
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
            if (!isOpen)//���ǹص�
            {
                anim.SetTrigger("AutomaticOpen");
                isOpen = true;//��Ϊ���򿪵��š�
            }
            else//���ǿ���
            {
                anim.SetTrigger("AutomaticClose");
                isOpen = false;//��Ϊ�����ϵ��š�
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
