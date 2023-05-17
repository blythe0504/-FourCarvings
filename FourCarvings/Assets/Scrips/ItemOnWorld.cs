using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourCarvings
{
    public class ItemOnWorld : MonoBehaviour
    {
        public Item thisItem;
        public Inventory playerInventory;

        private void OnMouseDown()
        {
            AddNewItem();
            InventoryManager.CreatNewItem(thisItem);
        }

        public void AddNewItem()
        {
            if(!playerInventory.itemList.Contains(thisItem))
            {
                playerInventory.itemList.Add(thisItem);
                Destroy(gameObject);
            }
            else
            {
                thisItem.itemHeld += 1;
            }
        }
    }
}
