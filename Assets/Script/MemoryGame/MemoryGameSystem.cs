using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;

public class MemoryGameSystem : MonoBehaviour
{
    //0 = w
    //1 = a
    //2 = s
    //3 = d
    public MemoryScoreSystem memoryScoreSystem;
    public TimeSystem timeSystem;
    public Image[] memorySprites;
    private Vector3[] memoryOriginalPositions;
    private Animator[] memoryAnimators;
    public int minSequenceLength, maxSequenceLength;
    private int currentSequenceLength;
    public int countToIncreseLength = 2;
    private int playerCombo;
    public float sequenceInterval = 1f;
    public int playerNum = 1;
    public bool swapAfterSequence = false;
    [SerializeField] private List<int> sequenceIndices = new List<int>();
    private List<KeyCode> playerControl = new List<KeyCode>();
    private List<int> userSequence = new List<int>();
    [SerializeField] private bool allowInput = false;
    [Required]
    public Animator fooAnimator;
    [Required]
    public Animator fhanafhonAnimator;
    private static readonly int Correct = Animator.StringToHash("Correct");
    private static readonly int Initial = Animator.StringToHash("Initial");
    [Required]
    public SoundData soundData;

    // Start is called before the first frame update
    void Start()
    {
        playerControl = playerNum == 1
            ? new List<KeyCode>() { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D }
            : new List<KeyCode>() { KeyCode.UpArrow, KeyCode.LeftArrow, KeyCode.DownArrow, KeyCode.RightArrow };
    }

    public void SetUpSystem()
    {
        memoryAnimators = new Animator[memorySprites.Length];
        memoryOriginalPositions = new Vector3[memorySprites.Length];
        for (int i = 0; i < memorySprites.Length; i++)
        {
            memoryAnimators[i] = memorySprites[i].gameObject.GetComponent<Animator>();
            memoryOriginalPositions[i] = memorySprites[i].transform.position;
        }
        currentSequenceLength = minSequenceLength;
        RandomSequence(sequenceIndices, currentSequenceLength);
        StartCoroutine(PlaySequence(sequenceIndices));
    }

    // Update is called once per frame
    void Update()
    {
        if (!timeSystem.gameStart)
        {
            return;
        }
        UserInput();
        UserInputUp();
        CheckCombo();
    }

    void UserInput()
    {
        if (allowInput)
        {
            Debug.Log("Input NOW!!!");
            if (Input.GetKeyDown(playerControl[0]))
            {
                if(playerNum == 1)
                    fooAnimator.SetTrigger("Up");
                else
                    fhanafhonAnimator.SetTrigger("Up");
                userSequence.Add(0);
                InputCheck(0, sequenceIndices);
            }
            else if (Input.GetKeyDown(playerControl[1]))
            {
                if(playerNum == 1)
                    fooAnimator.SetTrigger("Left");
                else
                    fhanafhonAnimator.SetTrigger("Left");
                userSequence.Add(1);
                InputCheck(1, sequenceIndices);
            }
            else if (Input.GetKeyDown(playerControl[2]))
            {
                if (playerNum == 1)
                    fooAnimator.SetTrigger("Down");
                else
                    fhanafhonAnimator.SetTrigger("Down");
                userSequence.Add(2);
                InputCheck(2, sequenceIndices);
            }
            else if (Input.GetKeyDown(playerControl[3]))
            {
                if (playerNum == 1)
                    fooAnimator.SetTrigger("Right");
                else
                    fhanafhonAnimator.SetTrigger("Right");
                userSequence.Add(3);
                InputCheck(3, sequenceIndices);
            }
        }
    }

    private void UserInputUp()
    {
        if (Input.GetKeyUp(playerControl[0]))
        {
            InputCheck(0, sequenceIndices);
            ButtonFeedback(0, "Correct", false);
            ButtonFeedback(0, "Wrong", false);
        }
        else if (Input.GetKeyUp(playerControl[1]))
        {
            InputCheck(1, sequenceIndices);
            ButtonFeedback(1, "Correct", false);
            ButtonFeedback(1, "Wrong", false);
        }
        else if (Input.GetKeyUp(playerControl[2]))
        {
            InputCheck(2, sequenceIndices);
            ButtonFeedback(2, "Correct", false);
            ButtonFeedback(2, "Wrong", false);
        }
        else if (Input.GetKeyUp(playerControl[3]))
        {
            InputCheck(3, sequenceIndices);
            ButtonFeedback(3, "Correct", false);
            ButtonFeedback(3, "Wrong", false);
        }
    }

    private void CheckCombo()
    {
        if (playerCombo >= countToIncreseLength)
        {
            playerCombo = 0;
            currentSequenceLength += 1;
            currentSequenceLength = Mathf.Clamp(currentSequenceLength, minSequenceLength, maxSequenceLength);
        }
    }

    private void ButtonFeedback(int index, string name, bool state)
    {
        memoryAnimators[index].SetBool(name, state);
    }

    private void ButtonFeedback(int index)
    {
        memoryAnimators[index].SetTrigger(Initial);
    }

    private IEnumerator PlaySequence(List<int> sequence)
    {
        foreach (int i in sequence)
        {
            ButtonFeedback(i);
            SoundManager.Instance.PlaySound(soundData.GetSoundClip("Guide"));
            yield return new WaitForSeconds(sequenceInterval);
        }
        if (swapAfterSequence)
        {
            StartCoroutine(SwapPosition());
        }
        allowInput = true;
    }

    private IEnumerator SwapPosition()
    {
        List<int> randomList = new List<int> { 0, 1, 2, 3 };
        for (int i = 0; i < memorySprites.Length; i++)
        {
            float maxTime = 0.1f;
            float lerpTime = 0f;
            int randomNum = randomList[Random.Range(0, randomList.Count)];
            randomList.Remove(randomNum);
            while (lerpTime <= maxTime)
            {
                memorySprites[i].transform.position = Vector3.Lerp(memorySprites[i].transform.position,
                    memoryOriginalPositions[randomNum], lerpTime / maxTime);
                lerpTime += Time.deltaTime;
                yield return null;
            }
        } 
    }

    private void ReturnToOriginal()
    {
        for (int i = 0; i < memorySprites.Length; i++)
        {
            memorySprites[i].transform.position = memoryOriginalPositions[i];
        }
    }
    
    void RandomSequence(List<int> sequence, int sequenceLength)
    {
        sequence.Clear();
        for (int i = 0; i < sequenceLength; i++)
        {
            sequence.Add(Random.Range(0, 4));
        }
    }

    private void WaitBeforePlay()
    {
        StartCoroutine(PlaySequence(sequenceIndices));
    }
    void InputCheck(int buttonIndex, List<int> sequence)
    {
        SoundManager.Instance.PlaySound(soundData.GetSoundClip("Click"));
        if (CompareSequence(sequence, userSequence) == 0)
        {
            allowInput = false;
            Debug.Log("wrong!!");
            SoundManager.Instance.PlaySound(soundData.GetSoundClip("Fail"));
            if (playerNum == 1)
                fooAnimator.SetTrigger("Fail");
            else
                fhanafhonAnimator.SetTrigger("Fail");
            userSequence.Clear();
            ButtonFeedback(buttonIndex, "Wrong", true);
            if (swapAfterSequence)
            { 
                ReturnToOriginal();
            }
            Invoke(nameof(WaitBeforePlay), 1f);
        }
        else if (CompareSequence(sequence, userSequence) == 1)
        {
            ButtonFeedback(buttonIndex, "Correct", true);
            Debug.Log("correct but input more!");
        }
        else
        {
            allowInput = false;
            Debug.Log("All correct");
            SoundManager.Instance.PlaySound(soundData.GetSoundClip("Succes2"));
            memoryScoreSystem.IncreaseScore(playerNum, 1);
            userSequence.Clear();
            ButtonFeedback(buttonIndex, "Correct", true);
            playerCombo += 1;
            RandomSequence(sequenceIndices, currentSequenceLength);
            if (swapAfterSequence)
            { 
                ReturnToOriginal();
            }
            Invoke(nameof(WaitBeforePlay), 1f);
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
