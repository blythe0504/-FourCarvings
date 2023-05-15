using UnityEngine;

namespace FourCarvings
{
    [CreateAssetMenu(menuName ="FourCarvings/Dialogue Data",fileName ="New Dialogue Data")]
    public class DialogueData : ScriptableObject
    {
        [Header("��ܪ̦W��_��")]
        public string dialogueName_Left;

        [Header("��ܪ̦W��_�k")]
        public string dialogueName_Right;

        [Header("��ܪ̤��e"),TextArea(2,10)]
        public string[] dialogueContents;

    }
}
