using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace FourCarvings
{
    public class HintSystem : MonoBehaviour
    {
        [SerializeField, Header("提示間隔"), Range(0, 0.5f)]
         float hintIntervalTime = 0.1f;

        public CanvasGroup groupHint;

        //public TextMeshProUGUI hintNane_text;

        public TextMeshProUGUI hintContent_text;

        [SerializeField,Header("提示對話")]
        private HintData hint;

        //private KeyCode hintKey = KeyCode.Space;

        private WaitForSeconds hintInterval => new WaitForSeconds(hintIntervalTime);

        private void Start()
        {
            hintContent_text.text = "";
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Debug.Log("偵測提示成功");
                StartCoroutine(FadeGroup());
                StartCoroutine(ShowHint());

               // ShowHint();
            }
            
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if(collision.gameObject.CompareTag("Player"))
            {
                StartCoroutine(FadeGroup(false));
            }
        }
        /*
      public void ShowHint()
      {


          for (int i = 0; i < hint.hintContent.Length; i++)
          {

              string hintText = hint.hintContent[i];

          }
      }
        */


        private IEnumerator ShowHint()
        {
            //hintContent_text.text = hint.hintName;

            hintContent_text.text = "";

            for (int i = 0; i < hint.hintContent.Length; i++)
            {
                string hintText = hint.hintContent[i];
                hintContent_text.text += hintText;

                yield return hintInterval;
            }
        }

        private IEnumerator FadeGroup(bool fadeIn = true)
        {
            float increase = fadeIn ? +0.1f : -0.1f;
            for (int i = 0; i < 10; i++)
            {
                groupHint.alpha += increase;
                yield return new WaitForSeconds(0.04f);
            }
        }



    }


}
