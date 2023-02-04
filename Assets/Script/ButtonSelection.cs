using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelection : MonoBehaviour
{
    [Required] [SceneObjectsOnly] public List<Button> buttons;
    public int selectedButton;
    public bool isSelecting;

    private void Start()
    {
        buttons[selectedButton].Select();
    }

    private void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 && !isSelecting) StartCoroutine(SelectButton());
        

        if (Input.GetKeyDown(KeyCode.Space)|| Input.GetKeyDown(KeyCode.RightControl)) buttons[selectedButton].onClick.Invoke();
    }
    private IEnumerator SelectButton()
    {
        isSelecting = true;
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            selectedButton++;
            if (selectedButton >= buttons.Count) selectedButton = 0;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            selectedButton--;
            if (selectedButton < 0) selectedButton = buttons.Count - 1;
        }

        //Change color of selected button
        buttons[selectedButton].gameObject.GetComponent<Image>().color = Color.red;
        for (var i = 0; i < buttons.Count; i++)
            if (i != selectedButton)
                buttons[i].gameObject.GetComponent<Image>().color = Color.white;
        buttons[selectedButton].Select();
        yield return new WaitForSeconds(0.2f);
        isSelecting = false;
    }
}