using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FahnafonProgressBar : MonoBehaviour
{
    [SerializeField] private FahnafonPressSystem fahnafonPressSystem;
    [SerializeField] private int pressGoal;
    private Slider slider;
    void Start()
    {
        fahnafonPressSystem = GameObject.Find("FahnafonButton").GetComponent<FahnafonPressSystem>();
        slider = GetComponent<Slider>();
        slider.maxValue = pressGoal;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = fahnafonPressSystem.pressCount;
    }
}
