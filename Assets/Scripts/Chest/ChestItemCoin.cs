using DG.Tweening;
using Itens;
using System.Collections.Generic;
using UnityEngine;

public class ChestItemCoin : ChestItemBase
{
    public Vector2 randomRange = new Vector2(-2f, 2f);
    public float tweenEndTime = .5f;

    public int coinNumber = 5;
    public GameObject coinObject;

    private List<GameObject> _itens = new List<GameObject>();
    public override void ShowItem()
    {
        base.ShowItem();
        createItems();
    }
    [NaughtyAttributes.Button]
    public void createItems()
    {
        for (int i = 0; i < coinNumber; i++)
        {
            var item = Instantiate(coinObject);
            item.transform.position = transform.position + Vector3.forward * Random.Range(randomRange.x,randomRange.y) + Vector3.right * Random.Range(randomRange.x, randomRange.y);
            item.transform.DOScale(0, .2f).SetEase(Ease.OutBack).From();
            _itens.Add(item);
            
        }
    }
    [NaughtyAttributes.Button]
    public override void Collect()
    {
        base.Collect();
        foreach (var item in _itens)
        {
            item.transform.DOMoveY(2f, tweenEndTime).SetRelative();
            item.transform.DOScale(0,tweenEndTime /2).SetDelay(tweenEndTime /2);
            itemManager.Instance.AddByType(ItemsType.COIN);
        }
    }
}

