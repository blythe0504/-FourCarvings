using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourCarvings
{
    [CreateAssetMenu(fileName ="New Inentory",menuName = "FourCarvings/New Inentory")]
    public class Inventory : ScriptableObject
    {
        public List<Item> itemList = new List<Item>();
    }
}
