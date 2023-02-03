using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class FooPressSystem : MonoBehaviour
{
    public int pressCount;
    private Random rnd;
    private List<KeyCode> keys;
    [SerializeField] private float changeKeyRate;
    [SerializeField] private int keyLimit;
    [SerializeField] private List<Sprite> keyImage;

    [SerializeField] private KeyCode key;
    private KeyCode keyBefore;

    void Start()
    {
        keys = new List<KeyCode>() { KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow };
        rnd = new Random();
    }


    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            GetComponent<Image>().color = Color.black;
            
            // change key & key sprite
            if (rnd.Next(0, 100) < changeKeyRate)
            {
                while (true)
                {
                    Mathf.Lerp(0f,3f, keyLimit);
           
                    int randomKey = rnd.Next(0, keys.Count - keyLimit);

                    if (randomKey != keys.IndexOf(key) || keyLimit == keys.Count - 1)
                    {
                        keyBefore = key;
                        key = keys[randomKey];
                        GetComponent<Image>().sprite = keyImage[randomKey];
                        break;
                    }
                }
            }
            pressCount++;
        }

        if (Input.GetKeyUp(key) || Input.GetKeyUp(keyBefore))
        {
            GetComponent<Image>().color = Color.white;
        }
    }
}
