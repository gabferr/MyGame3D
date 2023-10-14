using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SOUIIntUpdate : MonoBehaviour
{
    public SOint soInt;
    public TextMeshProUGUI uiText;

    private void Start()
    {
        uiText.text = soInt.value.ToString();

    }

    private void Update()
    {
        uiText.text = soInt.value.ToString();
    }
}
