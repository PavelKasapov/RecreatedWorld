using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Characters/Player")]
public class Character : ScriptableObject
{
    public new string name;
    public Sprite portraitSprite;
}
