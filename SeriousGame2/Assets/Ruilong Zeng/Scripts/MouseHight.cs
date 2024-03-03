using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseHight : MonoBehaviour
{
    [Header("UI Part")] public RectTransform uiTansform;
    public GameObject talkcanvs;
    public Button closeButton;

    [Header("Interaction Part")] public Transform handTransform;
    public PlayerCamera playerCamera;
    public GameObject ClickEffect;
    public GameObject ClickEffect2;

    private GameObject highlightedObject;
    private bool canRotateVertically = true;

    private bool haveItem;


    private void Update()
    {
        closeButton.onClick.AddListener(Close);
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.collider.gameObject;

            // 确保物体上有 Outline 组件
            Outline outlineComponent = hitObject.GetComponent<Outline>();
            if (outlineComponent != null)
            {
                // 如果鼠标当前悬停的物体不是上一个悬停的物体
                if (hitObject != highlightedObject)
                {
                    ClearHighlight();
                    highlightedObject = hitObject;
                    outlineComponent.enabled = true;

                    //uiTansform.gameObject.SetActive(true);
                }

                if (hitObject.CompareTag("CanPickup"))
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        PickUpItem(hitObject);
                    }
                }

                if (hitObject.CompareTag("CanTalk"))
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        talkcanvs.SetActive(true);
                    }
                }
            }
            else
            {
                // 如果物体上没有 Outline 组件，则清除高亮
                ClearHighlight();

               // uiTansform.gameObject.SetActive(false);
            }
        }
        else
        {
            // 如果没有命中任何物体，则清除高亮
            ClearHighlight();
            uiTansform.gameObject.SetActive(false);
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (haveItem)
            {
                // 获取点击位置
                RaycastHit floorHit;
                if (Physics.Raycast(ray, out floorHit))
                {
                    if (floorHit.collider.CompareTag("Floor"))
                    {
                        // 触发粒子效果
                        ClickEffectControl(floorHit.point);
                    }else if (floorHit.collider.CompareTag("Dangerous"))
                    {
                        ClickEffectControl2(floorHit.point);
                    }
                }
            }
        }
    }

    void Close()
    {
        talkcanvs.SetActive(false);
    }

    private void PickUpItem(GameObject gamobject)
    {
        Collider itemCollider = gamobject.GetComponent<Collider>();
        if (itemCollider != null)
        {
            itemCollider.enabled = false;
        }


        gamobject.transform.position = handTransform.position;
        gamobject.transform.rotation = handTransform.rotation;
        gamobject.gameObject.transform.SetParent(handTransform);
        haveItem = true;
        
        playerCamera.SetMoveUpDownEnabled(false);
    }

    private void ClickEffectControl(Vector3 position)
    {
        // 产生粒子效果
        GameObject ripple = Instantiate(ClickEffect, position, Quaternion.identity);
        // 设置粒子效果销毁时间
        Destroy(ripple, 1.5f);
    }

    private void ClickEffectControl2(Vector3 position)
    {
        GameObject ripper2 = Instantiate(ClickEffect2, position, Quaternion.identity);
        Destroy(ripper2,1.5f);
    }

    private void ClearHighlight()
    {
        if (highlightedObject != null)
        {
            Outline outlineComponent = highlightedObject.GetComponent<Outline>();
            if (outlineComponent != null)
            {
                outlineComponent.enabled = false;
            }

            highlightedObject = null;
        }
    }
}