using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnMove : MonoBehaviour
{
    public int currentID;
    public Color selected;
    public Color unSelected;
    public int pawnNumber;
    static List<int> movedPawnNumber = new List<int>();
    static bool selectionMade = false;
    static int sevenMove = 0;
    GameManager manager;
    int moveBy;
    GameObject moveTo;
    bool start = true;
    public char color;
    float timer = 0.0f;
    int[] yellow = { 2, 0 };
    int[] green = { 17, 7 };
    int[] red = { 32, 14 };
    int[] blue = { 47, 21 };
    // Start is called before the first frame update
    void Start()
    {
        currentID = -1;
        manager = GameObject.FindWithTag("Manager").GetComponent<GameManager>();
    }
    private void MyOnMouseDown()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (manager != null)
            {
                if (hit.transform.gameObject.GetComponent<PawnMove>() != null)
                {
                    if (manager.turn == 0 && color == 'y' || manager.turn == 1 && color == 'g' || manager.turn == 2 && color == 'r' || manager.turn == 3 && color == 'b')
                    {
                        if (moveBy != 0 && !selectionMade)
                        {
                            if (hit.transform.gameObject.GetComponent<PawnMove>().pawnNumber == pawnNumber)
                            {
                                if(movedPawnNumber.Count > 0)
                                    movedPawnNumber.RemoveAt(movedPawnNumber.Count - 1);
                                movedPawnNumber.Add(pawnNumber);
                                switch(moveBy)
                                {
                                    case 4:
                                        manager.board_[findId(-4)].GetComponent<Square>().selected = true;
                                        break;
                                    case 7:
                                        if(movedPawnNumber.Count > 1)
                                        {


                                        }
                                        else
                                        {
                                            for (int i = 1; i <= 7; i++)
                                            {
                                                manager.board_[findId(i)].GetComponent<Square>().selected = true;
                                            }
                                        }
                                        break;
                                    default:
                                        manager.board_[findId(moveBy)].GetComponent<Square>().selected = true;
                                        break;
                                }
                                lightBoard();
                            }
                        }
                    }
                }
                if (hit.transform.gameObject.GetComponent<Square>() != null)
                {
                    if(moveBy == 7)
                    {
                        if (movedPawnNumber.Count > 0)
                        {
                            bool on = false;
                            for (int i = 1; i <= 7; i++)
                            {
                                if (manager.board_[findId(i)] == hit.transform.gameObject)
                                {
                                    on = true;
                                }
                            }
                            if (on)
                            {
                                for (int i = 1; i <= 7; i++)
                                {
                                    manager.board_[findId(moveBy)].GetComponent<Square>().selected = false;
                                }
                                // hit.transform.GetComponent<Square>().selected = true;
                                lightBoard();
                            }
                        }
                    }
                }
            }
        }
    }
    int findId(int i)
    {
        int id = currentID;
        if (start)
        {
            id++;
            switch (color)
            {
                case 'y':
                    id = yellow[0] + 1;
                    break;
                case 'g':
                    id = green[0] + 1;
                    break;
                case 'r':
                    id = red[0] + 1;
                    break;
                case 'b':
                    id = blue[0] + 1;
                    break;
            }
        }
        if (i < 0)
        {
            if (start)
                id += 2;
            return (id + i + 60) % 60;
        }
        int[] number = new int[2];
        switch (color)
        {
            case 'y':
                number = yellow;
                break;
            case 'g':
                number = green;
                break;
            case 'r':
                number = red;
                break;
            case 'b':
                number = blue;
                break;
        }
        if (id > 60 - 1 + number[1])
        {
            return (id + i < 60 + 7 - 1 - 1 + number[1]) ? id + i : id;
        }
        else if ((id + i + 60 - number[0] - 1) % 60 < (id + 60 - number[0] - 1) % 60)
        {
            return ((id + i) % 60 + 60 - number[0] + number[1] - 1);
        }
        return (id + i) % 60;
    }
    void lightBoard()
    {
        for (int j = 0; j < 60 + 4 * 7; j++)
        {
            if (manager.board_[j] != null)
            {
                if (manager.board_[j].GetComponent<Square>().selected)
                    manager.board_[j].GetComponent<SpriteRenderer>().color = selected;
                else
                    manager.board_[j].GetComponent<SpriteRenderer>().color = unSelected;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        MyOnMouseDown();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(movedPawnNumber.Count > 0)
            {
                if (movedPawnNumber[movedPawnNumber.Count - 1] == pawnNumber)
                {
                    if (Selected() == 1)
                    {
                        for (int j = 0; j < 60 + 4 * 7; j++)
                        {
                            if (manager.board_[j] != null)
                            {
                                if (manager.board_[j].GetComponent<Square>().selected)
                                {
                                    moveTo = manager.board_[j];
                                    manager.board_[j].GetComponent<Square>().selected = false;
                                }
                            }
                        }
                        if (movedPawnNumber.Count > ((moveBy == 7) ? 1 : 0))
                        {
                            if (start) start = false;
                            timer = 1.5f;
                            selectionMade = true;
                        }
                    }
                }
                else
                {
                    moveBy = 0;
                }
            }
            else
            {
                moveBy = 0;
            }
        }
        if (moveBy == 0)
        {
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                moveBy = 4;
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                moveBy = 5;
            }
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                moveBy = 6;
            }
            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                moveBy = 7;
            }
            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                moveBy = 8;
            }
            if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                moveBy = 9;
            }
        }
        if (moveTo != null && selectionMade)
        {
            moveTo.GetComponent<SpriteRenderer>().color = unSelected;
            if (timer > 1.2f)
            {
                LeanTween.moveLocalY(gameObject, moveTo.transform.position.y + 1.25f, 0.3f);
            }
            else if (timer > 0.9f)
            {
                LeanTween.moveLocalZ(gameObject, moveTo.transform.position.z + (Random.value - 0.5f) / 4, 0.3f);
                LeanTween.moveLocalX(gameObject, moveTo.transform.position.x + (Random.value - 0.5f) / 4, 0.3f);
            }
            else if (timer > 0.6f)
            {
                LeanTween.moveLocalY(gameObject, moveTo.transform.position.y + 0.25f, 0.3f);
            }
            else
            {
                timer = 0.0f;
                currentID = moveTo.GetComponent<Square>().squareID;
                moveBy = 0;
                moveTo = null;
                movedPawnNumber.Clear();
                selectionMade = false;
                manager.turn = ++manager.turn % 2;
            }
            timer -= Time.deltaTime;
        }
    }
    int Selected()
    {
        int numberOfPiecesSelected = 0;
        for (int j = 0; j < 60 + 4 * 7; j++)
        {
            if (manager.board_[j] != null)
            {
                if (manager.board_[j].GetComponent<Square>().selected)
                    numberOfPiecesSelected++;
            }
        }
        return numberOfPiecesSelected;
    }
}