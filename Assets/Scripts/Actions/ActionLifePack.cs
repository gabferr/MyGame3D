using Itens;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionLifePack : MonoBehaviour
{
    public SOint soInt;
    public KeyCode keyCode = KeyCode.L;
    private void Start()
    {
       soInt=  itemManager.Instance.GetItemByType(ItemsType.LIFEPACK).soInt;
    }
     private void RecoveryLife()
    {
        if(soInt.value > 0)
        {
            itemManager.Instance.RemoveByType(ItemsType.LIFEPACK);
            Player.Instance.healthBase.ResetLife();
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(keyCode)) {
            RecoveryLife();
        }
    }
}
