using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourCarvings
{
    [CreateAssetMenu(fileName ="New Hint Data",menuName =("FourCarvings/Hint Data"))]
    public class HintData : ScriptableObject
    {
        [Header("���ܪ�")]
        public string hintName;

        [Header("���ܤ��e" +
            "")]
        public string[] hintContent;
    }
}
