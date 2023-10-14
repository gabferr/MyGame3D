using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UiFillUpdate : MonoBehaviour
{
    public Image uiImage;

    [Header("Animation")]
    public float duration =.1f;
    public Ease ease = Ease.OutBack;

    private Tween _currTWeen;
    private void OnValidate()
    {
       if (uiImage == null) uiImage = GetComponent<Image>();
    }

    public void UpdateValue(float f)
    {
        uiImage.fillAmount = f;
    }
    public void UpdateValue(float max, float current)
    {
        if(_currTWeen != null) _currTWeen.Kill();
        uiImage.DOFillAmount(1 - (current / max),duration).SetEase(ease);
    }
}
    