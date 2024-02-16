using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHight : MonoBehaviour
{
    private GameObject highlightedObject;
    private void Update()
    {
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
                }
            }
            else
            {
                // 如果物体上没有 Outline 组件，则清除高亮
                ClearHighlight();
            }
        }
        else
        {
            // 如果没有命中任何物体，则清除高亮
            ClearHighlight();
        }
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