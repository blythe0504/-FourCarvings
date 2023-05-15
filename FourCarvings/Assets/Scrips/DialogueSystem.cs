using UnityEngine;
using TMPro;
using System.Collections;

namespace FourCarvings
{

    public class DialogueSystem : MonoBehaviour
    {
        #region ��ưϰ�
        [SerializeField, Header("��ܶ��j"), Range(0, 0.5f)]
        private float dialogueIntervalTime = 0.1f;

        [SerializeField, Header("�}�Y���")]
        private DialogueData dialogueOpening;

        [SerializeField,Header("��ܫ���")]
        private KeyCode dialogueKey = KeyCode.Space;

        private WaitForSeconds dialogueInterval => new WaitForSeconds(dialogueIntervalTime);
        private CanvasGroup groupDialogue;
        private TextMeshProUGUI textName_Left;
        private TextMeshProUGUI textName_Right;
        private TextMeshProUGUI textContent; 
        #endregion

        private void Awake()
        {
            groupDialogue = GameObject.Find("��ܤ���").GetComponent<CanvasGroup>();
            textName_Left = GameObject.Find("��ܪ�_��").GetComponent<TextMeshProUGUI>();
            
            textContent = GameObject.Find("��ܤ��e").GetComponent<TextMeshProUGUI>();

            StartCoroutine(FadeGroup());
            StartCoroutine(TypeEffect());
        }
        /// <summary>
        /// �H�J�H�X�s�ժ���
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
                print("���a���U����");

                
            }

            StartCoroutine(FadeGroup(false));
        }

    }
}
