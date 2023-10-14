using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DestructableItemBase : MonoBehaviour
{
    public float shakeDuration = .1f;
    public int shakeForce = 1;
    public HealthBase healthBase;

    public int dropCoinsAmount = 10;
    public GameObject coinPrefab;
    public Transform dropPosition;
    private void OnValidate()
    {
        if(healthBase == null) healthBase = GetComponent<HealthBase>();
    }
    private void Awake()
    {
        OnValidate();
        healthBase.onDamage += OnDamage;
    }
    private void OnDamage(HealthBase h)
    {
        transform.DOShakeScale(shakeDuration/2,Vector3.up,shakeForce/2);
        DropCoins();
    }
    [NaughtyAttributes.Button]
    private void DropCoins()
    {
        var i = Instantiate(coinPrefab);
        i.transform.position = dropPosition.position;
        i.transform.DOScale(0, 1f).SetEase(Ease.OutBack).From();
    }
    [NaughtyAttributes.Button]
    private void DropGroupOfCoins()
    {
       StartCoroutine(DropGroupOfCoinsCoroutine());
    }

    IEnumerator DropGroupOfCoinsCoroutine()
    {
         for(var i = 0; i<dropCoinsAmount; i++)
        {
            DropCoins();
            yield return new WaitForSeconds(.1f);
        }           
    }
}
