using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private PressSystem button;
    [SerializeField] private int pressGoal;
    private Slider slider;
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = pressGoal;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = button.pressCount;
    }
}
