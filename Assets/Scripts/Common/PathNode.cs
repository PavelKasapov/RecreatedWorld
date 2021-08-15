using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    public PathNode(Vector3Int coords, int gCost, int hCost, int selfCost, PathNode prevNode)
    {
        Coords = coords;
        GCost = gCost;
        PrevNode = prevNode;
        FCost = hCost + gCost;
        SelfCost = selfCost;
    }
    public Vector3Int Coords {get; set;}
    public int GCost { get; set; }
    public int FCost { get; set; }
    public int SelfCost { get; set; }
    public PathNode PrevNode { get; set; }

    //PathNode startNode = new PathNode()
    //{
    //    Coords = fromOffsetCoords,
    //    GCost = 0,
    //    prevNode = null,
    //    FCost = newHCost + newSelfCost,
    //    SelfCost = newSelfCost
    //};
}
