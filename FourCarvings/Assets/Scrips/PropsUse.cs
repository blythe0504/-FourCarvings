using FourCarvings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PropsUse : MonoBehaviour
{

    //private Item item;

    public string buttonName;

    public Slot slot;

    public TextAsset propsTextAssetV2;

    public string[] propsRowsV2;

    public TextMeshProUGUI currentText;

    //public GameObject slotPfb;

    private void Start()
    {
        
    }

    public void ReadPropsText(TextAsset _textAsset)
    {
        propsRowsV2 = _textAsset.text.Split('\n');
        Debug.Log("�D��ĥ�Ū�����\");
        Debug.Log(currentText.text);
        

    }
    /*
    public void UpdataButtonName(string name)
    {
       // name = slot.slotName;
        Debug.Log(name);
    }
    */
    
    public void UseProps()
    {
        ReadPropsText(propsTextAssetV2);
        //UpdataButtonName(buttonName);
        
        
        for (int i = 0; i < propsRowsV2.Length; i++)
        {
            string[] cells = propsRowsV2[i].Split(',');
            if (cells[0] == currentText.text)
            {
                Debug.Log("�D��W�r�ǰt���\");
                if (cells[1] == "�q��")
                {
                    GameObject.Find("�ʦL�B").gameObject.SetActive(false);
                }
            }
        }
    }
}
