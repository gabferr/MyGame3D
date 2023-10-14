using Itens;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class ItemLayoutManager : MonoBehaviour
    {
        public ItemLayout prefabLayout;
        public Transform container;

        public List<ItemLayout> itemLayouts;

        private void Start()
        {
            createItems();
        }
        private void createItems()
        {
            foreach(var setup in itemManager.Instance.itemSetups)
            {
                var item = Instantiate(prefabLayout, container);
                item.LoadSetup(setup);
                itemLayouts.Add(item);
            }
        }
    }

}
