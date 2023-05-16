using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Net.Http.Headers;
using Unity.VisualScripting;
using UnityEngine.UI;

namespace FourCarvings
{
    public class DialogManger : MonoBehaviour
    {
        public TextAsset dialogDataFile;

        public SpriteRenderer spriteLeft;

        public SpriteRenderer spreiteRight;

        public TextMeshProUGUI nameText;

        public TextMeshProUGUI dialogText;

        public List<Sprite> sprites = new List<Sprite>();

        Dictionary<string, Sprite> ImageDic = new Dictionary<string, Sprite>();

        public int dialogIndex;

        public string[] dialogRows;

        //public Button nextButton;

        public GameObject optionButton;

        public Transform buttonGroup;

        public KeyCode dialogKey = KeyCode.Space;

        public bool OnSpace;

        public CanvasGroup dialogGroup;

        public bool present;

       
        private void Awake()
        {
            ImageDic["小精靈"] = sprites[0];
            ImageDic["守護者"] = sprites[1];
            StartCoroutine(CanvasFade());
        }

        private void Start()
        {
            ReadText(dialogDataFile);
            ShowDialogue();

            OnSpace = true;
            spreiteRight.gameObject.SetActive(false);
            spriteLeft.gameObject.SetActive(false);
            //OnClickNext();
            //UpdataText("小精靈", "守護者");
            //UpadataImage("小精靈", false);
        }

        private void Update()
        {
            OnClickNext();
        }

        public void UpdataText(string _name,string _text)
        {
            nameText.text = _name;
            dialogText.text = _text;
        }

        public void UpadataImage(string _name,string _position)
        {
            if (present == true)
            {
                spreiteRight.gameObject.SetActive(true);
                spriteLeft.gameObject.SetActive(true);
                if (_position == "左")
                {
                    spriteLeft.sprite = ImageDic[_name];
                    

                }
                else if (_position == "右")
                {
                    spreiteRight.sprite = ImageDic[_name];

                }
                else if (_position == null)
                {

                }
            }
        }

        public void ReadText(TextAsset _textAsset)
        {
            dialogRows = _textAsset.text.Split('\n');
            Debug.Log("讀取成功");
        }

        public void ShowDialogue()
        {
            for(int i=0;i<dialogRows.Length;i++)
            {
                string[] cells = dialogRows[i].Split(',');
                if (cells[0] == "#" && int.Parse(cells[1])==dialogIndex)
                {
                    UpdataText(cells[2], cells[4]);
                    UpadataImage(cells[2], cells[3]);

                    //StartCoroutine(Verbatim());

                    dialogIndex = int.Parse(cells[5]);
                    //nextButton.gameObject.SetActive(true);
                    break;
                }
                else if(cells[0]=="&" && int.Parse(cells[1]) == dialogIndex)
                {
                    //nextButton.gameObject.SetActive(false);
                    GenerateOption(i);
                }
                else if(cells[0]=="END"&& int.Parse(cells[1]) == dialogIndex)
                {
                    Debug.Log("劇情結束");
                    StartCoroutine(CanvasFade(false));
                    present = false;
                    OnSpace = false;
                    spreiteRight.gameObject.SetActive(false);
                    spriteLeft.gameObject.SetActive(false);
                }
            }
        }

        public void OnClickNext()
        {
            
            
            if (Input.GetKeyDown(dialogKey)&&OnSpace==true)
            {
                Debug.Log("按下空白鍵");
                ShowDialogue();
            }
        }

        public void GenerateOption(int _index)
        {
            string[] cells = dialogRows[_index].Split(',');
            if(cells[0]=="&")
            {
                GameObject button = Instantiate(optionButton, buttonGroup);

                button.GetComponentInChildren<TextMeshProUGUI>().text = cells[4];
                button.GetComponent<Button>().onClick.AddListener(delegate { OnOptionClick(int.Parse(cells[5])); });
                OnSpace = false;
                
                GenerateOption(_index + 1);
            }
            
        }

        public void OnOptionClick(int _id)
        {
            dialogIndex = _id;
            ShowDialogue();

            for (int i = 0; i < buttonGroup.childCount; i++)
            {
                Destroy(buttonGroup.GetChild(i).gameObject);
            }
            OnSpace = true;
        }

        private IEnumerator CanvasFade(bool _fade=true)
        {
            float increase = _fade ? +0.1f : -0.1f;
            present = _fade;
            print(present);
            for (int i = 0; i < 10; i++)
            {
                dialogGroup.alpha += increase;
                yield return new WaitForSeconds(0.04f);

            }
        }
        
    }

    
}
