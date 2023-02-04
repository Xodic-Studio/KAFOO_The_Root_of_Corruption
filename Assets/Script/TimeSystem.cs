using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeSystem : MonoBehaviour
{
     public float timeSpan = 30f;
     [SerializeField] public float timeLeft;
     private TextMeshProUGUI timeText;
     public bool gameStart;

     void Start()
     {
         timeText = GetComponent<TextMeshProUGUI>();
         timeText.text = String.Empty;
         timeLeft = timeSpan;
     }

     void Update()
     {
         if (!gameStart)
         {
             return;
         }
         timeLeft -= Time.deltaTime;
         timeLeft = Mathf.Clamp(timeLeft, 0, timeSpan);

         timeText.text = $"Time Left : {timeLeft:F0}";
         if (timeLeft <= 10)
         {
             timeText.color = (int)timeLeft % 2 == 0 ? Color.white : Color.red;
         }
         else
         {
             timeText.color = Color.white;
         }
     }
     
     
}
