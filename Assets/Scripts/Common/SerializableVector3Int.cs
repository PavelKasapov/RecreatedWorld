using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SerializableVector3Int
{
    public int x;
    public int y;
    public int z;
    public SerializableVector3Int(int rX, int rY, int rZ)
    {
        x = rX;
        y = rY;
        z = rZ;
    }
    public override string ToString()
    {
        return String.Format("[{0}, {1}, {2}]", x, y, z);
    }

    public static implicit operator Vector3Int(SerializableVector3Int rValue)
    {
        return new Vector3Int(rValue.x, rValue.y, rValue.z);
    }

    public static implicit operator SerializableVector3Int(Vector3Int rValue)
    {
        return new SerializableVector3Int(rValue.x, rValue.y, rValue.z);
    }
}

