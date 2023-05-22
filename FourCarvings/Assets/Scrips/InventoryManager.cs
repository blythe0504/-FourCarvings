using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace FourCarvings
{

    public class InventoryManager : MonoBehaviour
    {
        static InventoryManager instance;

        public Item item;

        public Inventory playerBag;

        public GameObject slotGrid;

        public Slot slotPrefab;

        public TextMeshProUGUI itemInformation;

        public Image UI_image;

        public TextMeshProUGUI item_Name;

        

        private void Awake()
        {
            if (instance != null)
                Destroy(this);
            instance = this;
        }

        private void Start()
        {
           
        }

        public void OnEnable()
        {
            RefeshItem();
            instance.itemInformation.text = "";
        }
        public static void UpdataItemInfo(string itemDescription)
        {
            instance.itemInformation.text = itemDescription;
        }
        
        public static void UpdataUI_Image(Sprite _itemImage)
        {
            instance.UI_image.gameObject.SetActive(true);
            instance.UI_image.sprite = _itemImage;
        }

        public static void Updata_ItemName(string _itemName)
        {
            instance.item_Name.text = _itemName;
        }
        

        public static void CreatNewItem(Item _item)
        {
            Slot newItem = Instantiate(instance.slotPrefab, instance.slotGrid.transform.position, Quaternion.identity);

            newItem.gameObject.transform.SetParent(instance.slotGrid.transform);

            newItem.sloyItem = _item;

            newItem.slotImage.sprite = _item.itemImage;

            newItem.slotNum.text = _item.itemHeld.ToString();

            

        }
        
        public static void RefeshItem()
        {
            for (int i = 0; i < instance.slotGrid.transform.childCount; i++)
            {
                if (instance.slotGrid.transform.childCount == 0)
                    break;
                Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
            }

            for (int i = 0; i < instance.playerBag.itemList.Count; i++)
            {
                CreatNewItem(instance.playerBag.itemList[i]);
            }
        }

       


    }
  
}
