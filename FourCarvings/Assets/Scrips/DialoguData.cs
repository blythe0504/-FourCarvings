using UnityEngine;

namespace FourCarvings
{
    /// <summary>
    /// ��ܸ��
    /// </summary>
    [CreateAssetMenu(menuName = "FourCarvings/Dialogue Data", fileName = "New Dialogue Data")]
    public class DialogueData : ScriptableObject
    {
        [Header("��ܪ̦W��")]
        public string diaName;

        [Header("��ܪ̤��e"), TextArea(2, 10)]
        public string[] diaContents;
    }
}
