using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnMove : MonoBehaviour
{
    public int currentID;
    public Color selected;
    public Color unSelected;
    public int pawnNumber;
    static int movedPawnNumber = -1;
    static bool selectionMade = false;
    GameObject manager;
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
        manager = GameObject.FindWithTag("Manager");
    }
    private void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.GetComponent<PawnMove>() != null)
            {
                if (manager != null)
                {
                    if (manager.GetComponent<GameManager>().turn == 0 && color == 'y' || manager.GetComponent<GameManager>().turn == 1 && color == 'g' || manager.GetComponent<GameManager>().turn == 2 && color == 'r' || manager.GetComponent<GameManager>().turn == 3 && color == 'b')
                    {
                        if (moveBy != 0 && !selectionMade)
                        {
                            if (hit.transform.gameObject.GetComponent<PawnMove>().pawnNumber == pawnNumber)
                            {
                                movedPawnNumber = pawnNumber;
                                switch(moveBy)
                                {
                                    case 4:
                                        manager.GetComponent<GameManager>().board_[findId(-moveBy)].GetComponent<Square>().selected = true;
                                        break;
                                    case 7:
                                        break;
                                    default:
                                        manager.GetComponent<GameManager>().board_[findId(moveBy)].GetComponent<Square>().selected = true;
                                        break;
                                }
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
        switch (color)
        {
            case 'y':
                if (id > 60 - 1 + yellow[1])
                {
                    // Don't have home yet
                    // return (id + i < 60 + 7 - 1 + yellow[1]) ? id + i : id;
                    return (id + i < 60 + 7 - 1 - 1 + yellow[1]) ? id + i : id;
                }
                else if ((id + i + 60 - yellow[0] - 1) % 60 < (id + 60 - yellow[0] - 1) % 60)
                {
                    return ((id + i) % 60 + 60 - yellow[0] + yellow[1] - 1);
                }
                break;
            case 'g':
                if (id > 60 - 1 + green[1])
                {
                    // Don't have home yet
                    // return (id + i < 60 + 7 - 1 + green[1]) ? id + i : id;
                    return (id + i < 60 + 7 - 1 - 1 + green[1]) ? id + i : id;
                }
                else if ((id + i + 60 - green[0] - 1) % 60 < (id + 60 - green[0] - 1) % 60)
                {
                    return ((id + i) % 60 + 60 - green[0] + green[1] - 1);
                }
                break;
            case 'r':
                if (id > 60 - 1 + red[1])
                {
                    // Don't have home yet
                    // return (id + i < 60 + 7 - 1 + red[1]) ? id + i : id;
                    return (id + i < 60 + 7 - 1 - 1 + red[1]) ? id + i : id;
                }
                else if ((id + i + 60 - red[0] - 1) % 60 < (id + 60 - red[0] - 1) % 60)
                {
                    return ((id + i) % 60 + 60 - red[0] + red[1] - 1);
                }
                break;
            case 'b':
                if (id > 60 - 1 + blue[1])
                {
                    // Don't have home yet
                    // return (id + i < 60 + 7 - 1 + blue[1]) ? id + i : id;
                    return (id + i < 60 + 7 - 1 - 1 + blue[1]) ? id + i : id;
                }
                else if ((id + i + 60 - blue[0] - 1) % 60 < (id + 60 - blue[0] - 1) % 60)
                {
                    return ((id + i) % 60 + 60 - blue[0] + blue[1] - 1);
                }
                break;
        }
        return (id + i) % 60;
    }
    void lightBoard()
    {
        for (int j = 0; j < 60 + 4 * 7; j++)
        {
            if (manager.GetComponent<GameManager>().board_[j] != null)
            {
                if (manager.GetComponent<GameManager>().board_[j].GetComponent<Square>().selected)
                    manager.GetComponent<GameManager>().board_[j].GetComponent<SpriteRenderer>().color = selected;
                else
                    manager.GetComponent<GameManager>().board_[j].GetComponent<SpriteRenderer>().color = unSelected;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (movedPawnNumber == pawnNumber)
            {
                if(Selected() == 1)
                {
                    for (int j = 0; j < 60 + 4 * 7; j++)
                    {
                        if (manager.GetComponent<GameManager>().board_[j] != null)
                        {
                            if (manager.GetComponent<GameManager>().board_[j].GetComponent<Square>().selected)
                            {
                                moveTo = manager.GetComponent<GameManager>().board_[j];
                                manager.GetComponent<GameManager>().board_[j].GetComponent<Square>().selected = false;
                            }
                        }
                    }
                    timer = 1.5f;
                    if (start) start = false;
                    selectionMade = true;
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
            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                moveBy = 8;
            }
            if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                moveBy = 9;
            }
        }
        if (moveTo != null && movedPawnNumber > -1 && selectionMade)
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
                movedPawnNumber = -1;
                selectionMade = false;
                manager.GetComponent<GameManager>().turn = ++manager.GetComponent<GameManager>().turn % 2;
            }
            timer -= Time.deltaTime;
        }
    }
    int Selected()
    {
        int numberOfPiecesSelected = 0;
        for (int j = 0; j < 60 + 4 * 7; j++)
        {
            if (manager.GetComponent<GameManager>().board_[j] != null)
            {
                if (manager.GetComponent<GameManager>().board_[j].GetComponent<Square>().selected)
                    numberOfPiecesSelected++;
            }
        }
        return numberOfPiecesSelected;
    }
}