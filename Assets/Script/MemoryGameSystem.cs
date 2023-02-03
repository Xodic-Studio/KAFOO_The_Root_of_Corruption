using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;

public class MemoryGameSystem : MonoBehaviour
{
    public Image[] memorySprites;
    //0 = w
    //1 = a
    //2 = s
    //3 = d
    [SerializeField] private List<int> sequenceIndices = new List<int>();
    private bool allowInput = false;

    // Start is called before the first frame update
    void Start()
    {
        RandomSequence(sequenceIndices, 4);
        StartCoroutine(PlaySequence(sequenceIndices));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PlaySequence(List<int> sequence)
    {
        allowInput = false;
        foreach (int i in sequence)
        {
            Color originalColor = memorySprites[i].color;
            Vector2 originalSize = memorySprites[i].rectTransform.sizeDelta;
            memorySprites[i].color = Color.green;
            memorySprites[i].rectTransform.sizeDelta *= 1.25f;
            yield return new WaitForSeconds(1f);
            memorySprites[i].color = originalColor;
            memorySprites[i].rectTransform.sizeDelta = originalSize;
            yield return new WaitForSeconds(1f);
        }
        allowInput = true;
        StartCoroutine(InputCheck(sequenceIndices));
    }

    void RandomSequence(List<int> sequence, int sequenceLength)
    {
        for (int i = 0; i < sequenceLength; i++)
        {
            sequence.Add(Random.Range(0, 3));
        }
    }

    IEnumerator InputCheck(List<int> sequence)
    {
        bool wrongInput = false;
        List<int> userSequence = new List<int>();
        while (true)
        {
            while (true)
            {
                Debug.Log("Input NOW!!!");
                if (Input.GetKeyDown(KeyCode.W))
                {
                    userSequence.Add(0);
                    break;
                }
                else if (Input.GetKeyDown(KeyCode.A))
                {
                    userSequence.Add(1);
                    break;
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    userSequence.Add(2);
                    break;
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    userSequence.Add(3);
                    break;
                }

                yield return null;
            }
            while (true)
            {
                if (CompareSequence(sequence, userSequence) == 0)
                {
                    Debug.Log("wrong!!");
                    userSequence.Clear();
                    StartCoroutine(PlaySequence(sequence));
                    yield return null;
                    //StartCoroutine(InputCheck(sequence));
                }
                else if (CompareSequence(sequence, userSequence) == 1)
                {
                    Debug.Log("correct but input more!");
                    break;
                }
                else
                {
                    Debug.Log("All correct");
                    yield break;
                }
                yield return null;
            }
            yield return null;
        }
    }

    //0 = wrong
    //1 = need more input
    //2 = correct
    int CompareSequence(List<int> sequence, List<int> userSequence)
    {
        for (int i = 0; i < userSequence.Count; i++)
        {
            if (userSequence[i] != sequence[i])
            {
                return 0;
            }
            else
            {
                return 1;
            }
            if (userSequence.Count == sequence.Count)
            {
                return 2;
            }
        }
        return 1;
    }
}
