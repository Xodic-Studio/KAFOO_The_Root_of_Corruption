using System.Collections;
using System.Collections.Generic;
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
    
    private KeyCode keyBefore;
    
    public void SetKeyLimit(int limit)
    {
        math.clamp(limit, 0, keys.Count - 1);
        keyLimit = limit;
    }

    void Update()
    {
        
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

    private bool CheckSameKey()
    {
        int randomKeyIndex = Random.Range(0, keys.Count - keyLimit);
        if (randomKeyIndex != keys.IndexOf(key) || keyLimit == keys.Count - 1)
        {
            keyBefore = key;
            key = keys[randomKeyIndex];
            GetComponent<Image>().sprite = keyImage[randomKeyIndex];
            return false;
        }
        return true;
    }
}
