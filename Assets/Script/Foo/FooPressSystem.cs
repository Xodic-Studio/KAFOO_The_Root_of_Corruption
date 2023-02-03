using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FooPressSystem : MonoBehaviour
{
    public int pressCount;
    private List<KeyCode> keys;
    [SerializeField] private float changeKeyRate;
    [SerializeField] private int keyLimit;
    [SerializeField] private List<Sprite> keyImage;

    [SerializeField] private KeyCode key;
    private KeyCode keyBefore;

    void Start()
    {
        keys = new List<KeyCode>() { KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow };
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
