using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PressSystem : MonoBehaviour
{
    [SerializeField] private float changeKeyRate;
    [SerializeField] private int keyLimit;
    [SerializeField] private KeyCode key;
    [SerializeField] private List<Sprite> keyImage;
    [SerializeField] private List<KeyCode> keys;
    [SerializeField] public int pressGoal;
    [HideInInspector] public int pressCount;
    public TimeSystem timeSystem;
    
    private KeyCode keyBefore;
    [SerializeField] private List<KeyCode> otherKey;
    [SerializeField] bool pressAble = true;

    private void Start()
    {
        otherKey = keys.ToList();
        otherKey.Remove(key);
    }

    public void SetKeyLimit(int limit)
    {
        math.clamp(limit, 0, keys.Count - 1);
        keyLimit = limit;
    }

    void Update()
    {
        if (!timeSystem.gameStart) return;
        if (!pressAble) return;
        foreach (var k in otherKey)
        {
            if (Input.GetKeyDown(k))
            {
                pressAble = false;
                Invoke(nameof(TurnOnPress), 1f);
                Debug.Log("Wrong Key");
                GetComponent<Image>().color = Color.red;
                return;
            }
        }
        if (Input.GetKeyDown(key))
        {
            GetComponent<Image>().color = Color.black;
            
            // change key & key sprite
            if (Random.Range(0, 100) < changeKeyRate)
            {
                Mathf.Lerp(0f,3f, keyLimit);

                while (CheckSameKey())
                {
                    Debug.Log("Random Same Key");
                }
                Debug.Log("Change Key");
                    
            }
            pressCount++;
        }

        if (Input.GetKeyUp(key) || Input.GetKeyUp(keyBefore))
        {
            GetComponent<Image>().color = Color.white;
        }
    }

    private void TurnOnPress()
    {
        pressAble = true;
        GetComponent<Image>().color = Color.white;
    }

    private bool CheckSameKey()
    {
        int randomKeyIndex = Random.Range(0, keys.Count - keyLimit);
        if (randomKeyIndex != keys.IndexOf(key) || keyLimit == keys.Count - 1)
        {
            keyBefore = key;
            otherKey.Add(keyBefore);
            key = keys[randomKeyIndex];
            otherKey.Remove(key);
            GetComponent<Image>().sprite = keyImage[randomKeyIndex];
            return false;
        }
        return true;
    }
}
