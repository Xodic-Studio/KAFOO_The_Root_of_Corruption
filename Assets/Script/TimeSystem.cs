using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeSystem : MonoBehaviour
{
     [SerializeField] private float timeSpan = 30f;
     [SerializeField] public float timeLeft;
     private TextMeshProUGUI timeText;

     void Start()
     {
         timeText = GetComponent<TextMeshProUGUI>();
     }

     void Update()
     {
         timeLeft = timeSpan - Time.time;
         Mathf.Lerp(0, timeSpan, timeLeft);

         timeText.text = $"Time Left : {timeLeft:F0}";
     }
}
