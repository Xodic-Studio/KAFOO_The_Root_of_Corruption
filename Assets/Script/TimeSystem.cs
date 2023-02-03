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

     void Start()
     {
         timeText = GetComponent<TextMeshProUGUI>();
         timeLeft = timeSpan;
     }

     void Update()
     {
         timeLeft -= Time.deltaTime;
         timeLeft = Mathf.Clamp(timeLeft, 0, timeSpan);

         timeText.text = $"Time Left : {timeLeft:F0}";
     }
     
     
}
