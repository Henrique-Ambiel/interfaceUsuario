using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Inventary/Item", order = 1)] //Cria um asset 
public class ItemData : ScriptableObject
{
    public string itemName;
    public float price;
    [TextArea]
    public string description;
   
}
