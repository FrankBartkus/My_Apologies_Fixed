using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseText : MonoBehaviour
{
    Text text;
    int start;
    int big;
    public int number;
    bool selected = false;
    void Start()
    {
        text = GetComponent<Text>();
        start = text.fontSize;
        big = (int)(text.fontSize * 1.25f);
    }
    void Update()
    {
        if(selected)
            text.fontSize = big;
        else if (isHovering())
            text.fontSize = big;
        else
            text.fontSize = start;
    }
    public bool isHovering()
    {
        if(EventSystem.current.IsPointerOverGameObject())
            return (Input.mousePosition.x * number > 600 * number);
        return false;
    }
    public void setBig(bool size_)
    {
        selected = size_;
        if(selected)
            text.fontSize = big;
        else
            text.fontSize = start;
    }
}