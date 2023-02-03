using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]
public class LoadSceneButton : MonoBehaviour
{
    private Button button;
    [SerializeField] private SceneName SceneName;
    
    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(delegate
        {
            LoadSceneManager.Instance.LoadScene(this.SceneName);
        });
    }
}
