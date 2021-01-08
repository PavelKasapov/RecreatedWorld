﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestExecutor : MonoBehaviour
{
    public GameObject selector;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Testing");

        Debug.Log("Test Map Generate Start");
        WorldMapMaganer.Instance.GenerateMap(40, 40);
        Debug.Log("Test Map Generate Complete");

        Vector3 testClick = new Vector2(150, 300);
        Debug.Log("Test trying to click on (" + testClick.x + ", " + testClick.y + ") mouse coords");
        TileSelector tileSelector = selector.GetComponent<TileSelector>();
        tileSelector.SelectTile(testClick);
        Debug.Log("Success click");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}