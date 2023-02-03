using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeSystem : MonoBehaviour
{
     public float timeSpan = 30f;
     private float timeWhenStart;
     [SerializeField] public float timeLeft;
     private TextMeshProUGUI timeText;

     void Start()
     {
         timeText = GetComponent<TextMeshProUGUI>();
         timeWhenStart = Time.time;
     }

     void Update()
     {
         timeLeft = timeSpan - (Time.time - timeWhenStart);
         timeLeft = Mathf.Clamp(timeLeft, 0, timeSpan);

         timeText.text = $"Time Left : {timeLeft:F0}";
     }
     
     
}
