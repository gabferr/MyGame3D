using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Itens;

public class itemCollactableCoin : itemCollactableBase
{
    public Collider2D colider;
    protected override void OnCollect()
    {
        base.OnCollect();
        itemManager.Instance.AddByType(ItemsType.COIN);
        colider.enabled = false;
    }
}
