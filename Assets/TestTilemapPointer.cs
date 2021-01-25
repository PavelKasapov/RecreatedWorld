using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestTilemapPointer : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log(eventData);

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log(eventData);
        //Debug.Log(eventData.pointerCurrentRaycast);
        //Debug.Log(eventData.pointerCurrentRaycast.worldPosition);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
