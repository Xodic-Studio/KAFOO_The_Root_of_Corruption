using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;

public class MemoryGameSystem : MonoBehaviour
{
    public Image[] memorySprites;
    private Animator[] memoryAnimators;
    public float sequenceInterval = 1f;
    public int playerNum = 1;
    //0 = w
    //1 = a
    //2 = s
    //3 = d
    [SerializeField] private List<int> sequenceIndices = new List<int>();
    private List<KeyCode> playerControl = new List<KeyCode>();
    private List<int> userSequence = new List<int>();
    [SerializeField] private bool allowInput = false;
    private static readonly int PlayFeedback = Animator.StringToHash("PlayFeedback");
    private static readonly int PlayFeedbackTrigger = Animator.StringToHash("PlayFeedbackTrigger");

    // Start is called before the first frame update
    void Start()
    {
        memoryAnimators = new Animator[memorySprites.Length];
        for (int i = 0; i < memorySprites.Length; i++)
        {
            memoryAnimators[i] = memorySprites[i].gameObject.GetComponent<Animator>();
        }
        RandomSequence(sequenceIndices, 4);
        StartCoroutine(PlaySequence(sequenceIndices));
        playerControl = playerNum == 1
            ? new List<KeyCode>() { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D }
            : new List<KeyCode>() { KeyCode.UpArrow, KeyCode.LeftArrow, KeyCode.DownArrow, KeyCode.RightArrow };
    }

    // Update is called once per frame
    void Update()
    {
        UserInput();
        UserInputUp();
    }

    void UserInput()
    {
        if (allowInput)
        {
            Debug.Log("Input NOW!!!");
            if (Input.GetKeyDown(playerControl[0]))
            {
                userSequence.Add(0);
                InputCheck(sequenceIndices);
                ButtonFeedback(0, true);
            }
            else if (Input.GetKeyDown(playerControl[1]))
            {
                userSequence.Add(1);
                InputCheck(sequenceIndices);
                ButtonFeedback(1, true);
            }
            else if (Input.GetKeyDown(playerControl[2]))
            {
                userSequence.Add(2);
                InputCheck(sequenceIndices);
                ButtonFeedback(2, true);
            }
            else if (Input.GetKeyDown(playerControl[3]))
            {
                userSequence.Add(3);
                InputCheck(sequenceIndices);
                ButtonFeedback(3, true);
            }
        }
    }

    void UserInputUp()
    {
        if (Input.GetKeyUp(playerControl[0]))
        {
            InputCheck(sequenceIndices);
            ButtonFeedback(0, false);
        }
        else if (Input.GetKeyUp(playerControl[1]))
        {
            InputCheck(sequenceIndices);
            ButtonFeedback(1, false);
        }
        else if (Input.GetKeyUp(playerControl[2]))
        {
            InputCheck(sequenceIndices);
            ButtonFeedback(2, false);
        }
        else if (Input.GetKeyUp(playerControl[3]))
        {
            InputCheck(sequenceIndices);
            ButtonFeedback(3, false);
        }
    }

    void ButtonFeedback(int index, bool state)
    {
        memoryAnimators[index].SetBool(PlayFeedback, state);
    }

    void ButtonFeedback(int index)
    {
        memoryAnimators[index].SetTrigger(PlayFeedbackTrigger);
    }

    IEnumerator PlaySequence(List<int> sequence)
    {
        allowInput = false;
        foreach (int i in sequence)
        {
            yield return new WaitForSeconds(sequenceInterval);
            ButtonFeedback(i);
        }
        allowInput = true;
    }

    void RandomSequence(List<int> sequence, int sequenceLength)
    {
        sequence.Clear();
        for (int i = 0; i < sequenceLength; i++)
        {
            sequence.Add(Random.Range(0, 4));
        }
    }

    void InputCheck(List<int> sequence)
    {
        if (CompareSequence(sequence, userSequence) == 0)
        {
            Debug.Log("wrong!!");
            userSequence.Clear();
            StartCoroutine(PlaySequence(sequence));
        }
        else if (CompareSequence(sequence, userSequence) == 1)
        {
            Debug.Log("correct but input more!");
        }
        else
        {
            Debug.Log("All correct");
            userSequence.Clear();
            RandomSequence(sequenceIndices, 4);
            StartCoroutine(PlaySequence(sequenceIndices));
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
            if (userSequence[i] == sequence[i])
            {
                if (userSequence.Count != sequence.Count)
                {
                    continue;
                }
                return 2;
            }
        }
        return 1;
    }
}
