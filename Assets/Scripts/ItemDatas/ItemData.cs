using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    public Sprite ItemSprite;
    public string ItemName;
    public int ItemRecoverValue;

    [TextArea]
    public string Explain; //Ό³Έν
}
