using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    public Vector3Int Coords {get; set;}
    public int GCost { get; set; }
    public int FCost { get; set; }
    public int SelfCost { get; set; }
    public PathNode prevNode { get; set; }
}
