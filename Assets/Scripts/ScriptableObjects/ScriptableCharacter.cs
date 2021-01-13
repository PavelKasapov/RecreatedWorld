using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Characters/Player")]
public class ScriptableCharacter : ScriptableObject
{
    public new string name;
    public Sprite portraitSprite;
}
