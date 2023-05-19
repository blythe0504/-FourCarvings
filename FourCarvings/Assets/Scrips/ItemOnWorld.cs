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
            
            Destroy(gameObject);
        }

        public void AddNewItem()
        {
            if(!playerInventory.itemList.Contains(thisItem))
            {
                playerInventory.itemList.Add(thisItem);
                //InventoryManager.CreatNewItem(thisItem);

            }
            else
            {
                thisItem.itemHeld += 1;
            }
            InventoryManager.RefeshItem();
        }
    }
}
