using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FooProgressBar : MonoBehaviour
{
    private FooPressSystem fooPressSystem;
    [SerializeField] private int pressGoal;
    private Slider slider;
    void Start()
    {
        fooPressSystem = GameObject.Find("FooButton").GetComponent<FooPressSystem>();
        slider = GetComponent<Slider>();
        slider.maxValue = pressGoal;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = fooPressSystem.pressCount;
    }
}
