using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace FourCarvings
{

    public class InventoryManager : MonoBehaviour
    {
        static InventoryManager instance;

        public Inventory playerBag;

        public GameObject slotGrid;

        public Slot slotPrefab;

        public TextMeshProUGUI itemInformation;

        private void Awake()
        {
            if (instance != null)
                Destroy(this);
            instance = this;
        }

        public static void CreatNewItem(Item _item)
        {
            Slot newItem = Instantiate(instance.slotPrefab, instance.slotGrid.transform.position, Quaternion.identity);

            newItem.gameObject.transform.SetParent(instance.slotGrid.transform);

            newItem.sloyItem = _item;

            newItem.slotImage.sprite = _item.itemImage;

            newItem.slotNum.text = _item.itemHeld.ToString();

        }
        /*
        public static void RefeshItem()
        {
            for (int i = 0; i < instance.slotGrid.transform.childCount; i++)
            {
                if (instance.slotGrid.transform.childCount == 0)
                    break;
                Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
            }

            for()
        }
        */
    }
  
}
