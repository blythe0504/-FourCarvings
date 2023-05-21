using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Net.Http.Headers;
using Unity.VisualScripting;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;

namespace FourCarvings
{
    public class DialogueManger : MonoBehaviour
    {
        static DialogueManger instance;

        /// <summary>
        /// 遊戲對話文本
        /// </summary>
        public TextAsset dialogDataFile;
        /// <summary>
        /// UI_角色名字
        /// </summary>
        public TextMeshProUGUI nameText;
        /// <summary>
        /// UI_對話內容
        /// </summary>
        public TextMeshProUGUI dialogText;
        /// <summary>
        /// 角色圖片列表
        /// </summary>
        public List<Sprite> sprites = new List<Sprite>();
        /// <summary>
        /// 角色名字對應字典
        /// </summary>
        Dictionary<string, Sprite> ImageDic = new Dictionary<string, Sprite>();
        /// <summary>
        /// 當前對話索引值
        /// </summary>
        public int dialogIndex;
        /// <summary>
        /// 對話文本_以行分割
        /// </summary>
        public string[] dialogRows;

        //public Button nextButton;
        /// <summary>
        /// 選項按鈕預置物
        /// </summary>
        public GameObject optionButton;
        /// <summary>
        /// 選項按鈕父節點
        /// </summary>
        public Transform buttonGroup;

        public KeyCode dialogKey = KeyCode.Space;

        public bool OnSpace;

        public CanvasGroup dialogGroup;

        public static bool present;

        public Image ch_image_Left;


        public Image ch_image_Right;

        //public PlayerMovement _playerMovement;

        public BoxCollider2D detectionPoint;

        public static bool speed=false;

        

        private void Awake()
        {
            ImageDic["小精靈"] = sprites[0];
            ImageDic["守護者"] = sprites[1];
            
        }

        private void Start()
        {
            
            //_playerMovement = GameObject.Find("守護者").GetComponent<PlayerMovement>();
            //ReadText(dialogDataFile);
            //ShowDialogue();

            //OnSpace = true;
            //spreiteRight.gameObject.SetActive(false);
            //spriteLeft.gameObject.SetActive(false);
            //OnClickNext();
            //UpdataText("小精靈", "守護者");
            //UpadataImage("小精靈", false);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                StartCoroutine(CanvasFade());
                Debug.Log("偵測成功");
                ReadText(dialogDataFile);
                ShowDialogue();
                present = true;

                Debug.Log(PlayerMovement._switch);
                OnSpace = true;

            }
        }



        private void Update()
        {
            SwitchForPlayerMovement();
            OnClickNext();
        }

        public void UpdataText(string _name, string _text)
        {
            nameText.text = _name;
            dialogText.text = _text;
        }

        public void UpadataImage(string _name, string _position)
        {
            if (present == true)
            {
                //spreiteRight.gameObject.SetActive(true);
                //spriteLeft.gameObject.SetActive(true);
                if (_position == "左")
                {
                    // spriteLeft.sprite = ImageDic[_name];

                    ch_image_Left.sprite = ImageDic[_name];

                    ch_image_Right.color = new Color32(94, 94, 94, 255);

                    ch_image_Left.color = new Color32(255, 255, 255, 255);
                }
                else if (_position == "右")
                {
                    //spreiteRight.sprite = ImageDic[_name];

                    ch_image_Right.sprite = ImageDic[_name];

                    ch_image_Left.color = new Color32(94, 94, 94, 255);

                    ch_image_Right.color = new Color32(255, 255, 255, 255);

                }
                else if (_position == null)
                {
                    ch_image_Left.color = new Color32(94, 94, 94, 255);
                    ch_image_Right.color = new Color32(94, 94, 94, 255);

                    //ch_image_Left.gameObject.SetActive(false);
                    //ch_image_Right.gameObject.SetActive(false);
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
            for (int i = 0; i < dialogRows.Length; i++)
            {
                string[] cells = dialogRows[i].Split(',');
                if (cells[0] == "#" && int.Parse(cells[1]) == dialogIndex)
                {
                    UpdataText(cells[2], cells[4]);
                    UpadataImage(cells[2], cells[3]);

                    //StartCoroutine(Verbatim());

                    dialogIndex = int.Parse(cells[5]);
                    //nextButton.gameObject.SetActive(true);
                    break;
                }
                else if (cells[0] == "&" && int.Parse(cells[1]) == dialogIndex)
                {
                    //nextButton.gameObject.SetActive(false);
                    GenerateOption(i);
                }
                else if (cells[0] == "END" && int.Parse(cells[1]) == dialogIndex)
                {
                    Debug.Log("劇情結束");
                    StartCoroutine(CanvasFade(false));
                    Debug.Log(dialogGroup.alpha);
                    present = false;
                    OnSpace = false;

                    detectionPoint.enabled = false;

                    
                    //Destroy(this.gameObject);
                    //spreiteRight.gameObject.SetActive(false);
                    //spriteLeft.gameObject.SetActive(false);
                    //this.gameObject.SetActive(false);
                }
            }
        }

        public void OnClickNext()
        {


            if (Input.GetKeyDown(dialogKey) && OnSpace == true)
            {
                Debug.Log("按下空白鍵");
                ShowDialogue();
            }
        }

        public void GenerateOption(int _index)
        {
            string[] cells = dialogRows[_index].Split(',');
            if (cells[0] == "&")
            {
                GameObject button = Instantiate(optionButton, buttonGroup);

                button.GetComponentInChildren<TextMeshProUGUI>().text = cells[4];
                button.GetComponent<Button>().onClick.AddListener(delegate { OnOptionClick(int.Parse(cells[5]));
                    if (cells[6] != "") { cells[7]=Regex.Replace(cells[7],@"[\r\n]","");
                        OptionEffect(cells[6], cells[7]); } });
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

        private IEnumerator CanvasFade(bool _fade = true)
        {
            float increase = _fade ? +0.1f : -0.1f;
            //present = _fade;
            //print(present);
            for (int i = 0; i < 10; i++)
            {
                dialogGroup.alpha += increase;
                yield return new WaitForSeconds(0.04f);

            }
        }

        public void SwitchForPlayerMovement()
        {
            if(present==false)
            {
                //GameObject.Find("守護者").GetComponent<>
                 PlayerMovement._switch=false;
            }
            else
            {
                PlayerMovement._switch = true;
            }
        }

        public void OptionEffect(string _effect, string _target)
        {
           
            if(_effect=="消失")
            {
                if(_target=="小精靈")
                {
                    GameObject.Find("小精靈").gameObject.SetActive(false);
                    speed = true;
                    Debug.Log("傳送守護者加速度");
                }
            }
        }


    }
}