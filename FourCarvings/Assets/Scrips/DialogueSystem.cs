using UnityEngine;
using TMPro;
using System.Collections;

namespace FourCarvings
{

    public class DialogueSystem : MonoBehaviour
    {
        #region 資料區域
        [SerializeField, Header("對話間隔"), Range(0, 0.5f)]
        private float dialogueIntervalTime = 0.1f;

        [SerializeField, Header("開頭對話")]
        private DialogueData dialogueOpening;

        [SerializeField,Header("對話按鍵")]
        private KeyCode dialogueKey = KeyCode.Space;

        private WaitForSeconds dialogueInterval => new WaitForSeconds(dialogueIntervalTime);
        private CanvasGroup groupDialogue;
        private TextMeshProUGUI textName_Left;
        private TextMeshProUGUI textName_Right;
        private TextMeshProUGUI textContent; 
        #endregion

        private void Awake()
        {
            groupDialogue = GameObject.Find("對話介面").GetComponent<CanvasGroup>();
            textName_Left = GameObject.Find("對話者_左").GetComponent<TextMeshProUGUI>();
            
            textContent = GameObject.Find("對話內容").GetComponent<TextMeshProUGUI>();

            StartCoroutine(FadeGroup());
            StartCoroutine(TypeEffect());
        }
        /// <summary>
        /// 淡入淡出群組物件
        /// </summary>       
        private IEnumerator FadeGroup(bool fadeIn=true)
        {
            float increase = fadeIn ? +0.1f : -0.1f;
            for (int i = 0; i < 10; i++)
            {
                groupDialogue.alpha += increase;
                yield return new WaitForSeconds(0.04f);
            }
        }

        private IEnumerator TypeEffect()
        {
            textName_Left.text = dialogueOpening.dialogueName_Left;
            

            for (int j = 0; j < dialogueOpening.dialogueContents.Length; j++)
            {

                textContent.text = "";

                string dialogue = dialogueOpening.dialogueContents[j];

                for (int i = 0; i < dialogue.Length; i++)
                {
                    textContent.text += dialogue[i];
                    yield return dialogueInterval;
                }

                while (!Input.GetKeyDown(dialogueKey))
                {
                    yield return null;
                }
                print("玩家按下按鍵");

                
            }

            StartCoroutine(FadeGroup(false));
        }

    }
}
