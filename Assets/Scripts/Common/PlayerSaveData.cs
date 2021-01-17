using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerSaveData
{
    public string Name { get; set; }
    public SerializableVector3 WorldCoord { get; set; }
    public SerializableVector3Int GridCoord { get; set; }
    public SerializableVector3Int TargetGridCoord { get; set; }
    public bool DirectControl { get; set; }
    public string SpritePath { get; set; }
}
