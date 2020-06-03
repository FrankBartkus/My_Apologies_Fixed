using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnUI : MonoBehaviour
{
    GameManager manager;
    Text text;
    string baseText;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindWithTag("Manager").GetComponent<GameManager>();
        text = GetComponent<Text>();
        baseText = "Turn: ";
    }

    // Update is called once per frame
    void Update()
    {
        switch(manager.turn)
        {
            case 0:
                text.text = baseText + "Y";
                break;
            case 1:
                text.text = baseText + "G";
                break;
            case 2:
                text.text = baseText + "R";
                break;
            case 3:
                text.text = baseText + "B";
                break;
        }
    }
}
