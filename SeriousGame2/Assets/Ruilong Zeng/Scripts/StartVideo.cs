using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class StartVideo : MonoBehaviour
{
   public GameObject blackPanel;
   public GameObject Rawi;
   public VideoPlayer video; 
   private float timecount;

   private void Awake()
   {
      video = GetComponentInChildren<VideoPlayer>();
      blackPanel.SetActive(true);
   }

   private void Update()
   {
      timecount += Time.deltaTime;
      
      if (timecount>=13f)
      {
         blackPanel.SetActive(false);
         video.Play();
         if (timecount>=18)
         {
            Rawi.SetActive(false);
            video.Stop();
         }
      }
   }
}
