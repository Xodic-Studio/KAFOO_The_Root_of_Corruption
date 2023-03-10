using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelection : MonoBehaviour
{
    [Required] [SceneObjectsOnly] public List<Button> buttons;
    [Required] [SerializeField] SoundController soundController;
    [Required] [SerializeField] private Animator[] transitions;
    public int selectedButton;
    public bool isSelecting;
    public bool isVertical;

    private void Start()
    {
        buttons[selectedButton].Select();
    }

    private void Update()
    {
        if (MasterScript.Instance.isEnded) return;
        if (isVertical)
        {
            if (Input.GetAxis("Vertical") != 0 && !isSelecting) StartCoroutine(SelectButton());
        }
        else
        {
            if (Input.GetAxisRaw("Horizontal") != 0 && !isSelecting) StartCoroutine(SelectButton());
        }





        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.RightControl))
        {
            soundController.PlaySound("transitions");
            foreach (var VARIABLE in transitions)
            {
                VARIABLE.SetTrigger("Exit");
            }
            buttons[selectedButton].onClick.Invoke();
        }
    }
    private IEnumerator SelectButton()
    {
        isSelecting = true;
        if (isVertical)
        {
            Vertical();
        }
        else
        {
            Horizontal();
        }
        //Change color of selected button
        buttons[selectedButton].gameObject.GetComponent<Image>().color = Color.red;
        for (var i = 0; i < buttons.Count; i++)
            if (i != selectedButton)
                buttons[i].gameObject.GetComponent<Image>().color = Color.white;
        buttons[selectedButton].Select();
        yield return new WaitForSeconds(0.2f);
        isSelecting = false;

        void Horizontal()
        {
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                selectedButton++;
                if (selectedButton >= buttons.Count) selectedButton = 0;
                soundController.PlaySound("ui");
            }
            else if (Input.GetAxisRaw("Horizontal") < 0)
            {
                selectedButton--;
                if (selectedButton < 0) selectedButton = buttons.Count - 1;
                soundController.PlaySound("ui");
            }
        }

        void Vertical()
        {
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                selectedButton--;
                if (selectedButton < 0) selectedButton = buttons.Count - 1;
                soundController.PlaySound("ui");
            }
            else if (Input.GetAxisRaw("Vertical") < 0)
            {
                selectedButton++;
                if (selectedButton >= buttons.Count) selectedButton = 0;
                soundController.PlaySound("ui");
            }
        }
    }
}