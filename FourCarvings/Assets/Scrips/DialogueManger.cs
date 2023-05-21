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
        /// �C����ܤ奻
        /// </summary>
        public TextAsset dialogDataFile;
        /// <summary>
        /// UI_����W�r
        /// </summary>
        public TextMeshProUGUI nameText;
        /// <summary>
        /// UI_��ܤ��e
        /// </summary>
        public TextMeshProUGUI dialogText;
        /// <summary>
        /// ����Ϥ��C��
        /// </summary>
        public List<Sprite> sprites = new List<Sprite>();
        /// <summary>
        /// ����W�r�����r��
        /// </summary>
        Dictionary<string, Sprite> ImageDic = new Dictionary<string, Sprite>();
        /// <summary>
        /// ��e��ܯ��ޭ�
        /// </summary>
        public int dialogIndex;
        /// <summary>
        /// ��ܤ奻_�H�����
        /// </summary>
        public string[] dialogRows;

        //public Button nextButton;
        /// <summary>
        /// �ﶵ���s�w�m��
        /// </summary>
        public GameObject optionButton;
        /// <summary>
        /// �ﶵ���s���`�I
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
            ImageDic["�p���F"] = sprites[0];
            ImageDic["�u�@��"] = sprites[1];
            
        }

        private void Start()
        {
            
            //_playerMovement = GameObject.Find("�u�@��").GetComponent<PlayerMovement>();
            //ReadText(dialogDataFile);
            //ShowDialogue();

            //OnSpace = true;
            //spreiteRight.gameObject.SetActive(false);
            //spriteLeft.gameObject.SetActive(false);
            //OnClickNext();
            //UpdataText("�p���F", "�u�@��");
            //UpadataImage("�p���F", false);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                StartCoroutine(CanvasFade());
                Debug.Log("�������\");
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
                if (_position == "��")
                {
                    // spriteLeft.sprite = ImageDic[_name];

                    ch_image_Left.sprite = ImageDic[_name];

                    ch_image_Right.color = new Color32(94, 94, 94, 255);

                    ch_image_Left.color = new Color32(255, 255, 255, 255);
                }
                else if (_position == "�k")
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
            Debug.Log("Ū�����\");
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
                    Debug.Log("�@������");
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
                Debug.Log("���U�ť���");
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
                //GameObject.Find("�u�@��").GetComponent<>
                 PlayerMovement._switch=false;
            }
            else
            {
                PlayerMovement._switch = true;
            }
        }

        public void OptionEffect(string _effect, string _target)
        {
           
            if(_effect=="����")
            {
                if(_target=="�p���F")
                {
                    GameObject.Find("�p���F").gameObject.SetActive(false);
                    speed = true;
                    Debug.Log("�ǰe�u�@�̥[�t��");
                }
            }
        }


    }
}