using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    public float timer = 5.0f;
    float currentTimer = 0.0f;
    Text text;
    public int die1 = 0;
    public int die2 = 0;
    int dieNum = 0;
    public int rollStage = 0;
    System.Random rand = new System.Random();
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0f)
        {
            die1 = rand.Next(1, 7);
            switch(dieNum)
            {
                case 1:
                    text.text = die1.ToString();
                    break;
                case 2:
                    die2 = rand.Next(1, 7);
                    text.text = die1.ToString() + " " + die2.ToString();
                    break;
            }
            currentTimer -= Time.deltaTime;
        }
        else if(die1 != 0)
        {
            rollStage = 2;
            timer = 0f;
        }
    }
    public void roll(bool both)
    {
        dieNum = 1;
        if(both)
            dieNum++;
        currentTimer = timer;
        rollStage = 1;
    }
}
