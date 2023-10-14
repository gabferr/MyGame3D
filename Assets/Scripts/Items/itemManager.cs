
using System.Collections.Generic;
using TMPro;
using GABFERR.Core.Singleton;
using UnityEngine;

namespace Itens { 
    public enum ItemsType
    {
        COIN,
        LIFEPACK
    }
public class itemManager : Singleton<itemManager> 
{
    public List<ItemSetup> itemSetups;   
    private void Start()
    {
        Reset();
       
    }
  
    public void Reset()
    {
           foreach (var i in itemSetups)
           {
               i.soInt.value = 0;
           }
    }
        
    public ItemSetup GetItemByType(ItemsType item )
    {  
      return itemSetups.Find(i => i.itemType == item);
    }
        public void AddByType(ItemsType item ,int amount = 1)
    {   if (amount < 0) return;
       itemSetups.Find(i => i.itemType == item).soInt.value += amount;
    }
    public void RemoveByType(ItemsType item, int amount = 1)
    {
            var iten = itemSetups.Find(i => i.itemType == item);
            iten.soInt.value -= amount;
            if (iten.soInt.value < 0) iten.soInt.value = 0;
    }

    [NaughtyAttributes.Button]
    private void AddCoin()
        {
            AddByType(ItemsType.COIN);
        }

     [NaughtyAttributes.Button]
         private void AddLifePack()
         {
            AddByType(ItemsType.LIFEPACK);
         }
    }
    [System.Serializable]
    public class ItemSetup
    {
        public ItemsType itemType;
        public SOint soInt;
        public Sprite icon;
    }
}