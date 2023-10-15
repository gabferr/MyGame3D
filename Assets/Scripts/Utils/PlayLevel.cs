using UnityEngine;
using TMPro;

public class PlayLevel : MonoBehaviour
{

    public TextMeshProUGUI uiTextName;
    private void Start()
    {
        SaveManager.Instance.FileLoaded += OnLoad;
    }
    public void OnLoad(SaveSetup setup)
    {
        uiTextName.text = "Play " + (setup.lastLevel + 1);
    } 

    private void OnDestroy()
    {
        SaveManager.Instance.FileLoaded += OnLoad;
    }
}
