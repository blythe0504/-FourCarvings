using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace FourCarvings
{

    public class Slot : MonoBehaviour
    {
        public Item sloyItem;

        public Image slotImage;

        public TextMeshProUGUI slotNum;

        public Button useButton;

        

        public void ItemOnClick()
        {
            InventoryManager.UpdataItemInfo(sloyItem.itemInfo);

            InventoryManager.UpdataUI_Image(sloyItem.itemImage) ;

            InventoryManager.Updata_ItemName(sloyItem.itemName);

            useButton.gameObject.SetActive(true);
            
        }

        public void UseOnClick()
        {
            Destroy(this.gameObject);
        }
    }
}
