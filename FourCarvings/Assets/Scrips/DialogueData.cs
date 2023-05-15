using UnityEngine;

namespace FourCarvings
{
    [CreateAssetMenu(menuName ="FourCarvings/Dialogue Data",fileName ="New Dialogue Data")]
    public class DialogueData : ScriptableObject
    {
        [Header("對話者名稱_左")]
        public string dialogueName_Left;

        [Header("對話者名稱_右")]
        public string dialogueName_Right;

        [Header("對話者內容"),TextArea(2,10)]
        public string[] dialogueContents;

    }
}
