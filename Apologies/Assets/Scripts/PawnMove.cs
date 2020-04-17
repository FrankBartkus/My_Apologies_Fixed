﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnMove : MonoBehaviour
{
    public int currentID;
    public int pawnNumber;
    static List<int> movedPawnNumberTest = new List<int>();
    static List<int> movedPawnNumberReal = new List<int>();
    static bool selectionMade = false;
    static bool goThroughAll = false;
    static int sevenMove = 7;
    GameManager manager;
    int moveBy;
    GameObject moveTo;
    bool start = true;
    public char color;
    static float timer = 0.0f;
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
                    bool ourPawn = (manager.turn == 0 && color == 'y') || (manager.turn == 1 && color == 'g') || (manager.turn == 2 && color == 'r') || (manager.turn == 3 && color == 'b');
                    if (ourPawn || goThroughAll)
                    {
                        if (moveBy != 0 && !selectionMade || goThroughAll)
                        {
                            if (hit.transform.gameObject.GetComponent<PawnMove>().pawnNumber == pawnNumber)
                            {
                                Debug.Log(manager.turn + ", " + color + ", " + pawnNumber);
                                ClearList(movedPawnNumberTest);
                                movedPawnNumberTest.Add(pawnNumber);
                                for (int i = 1; i < 60 + 6 * 4; i++)
                                {
                                    if(manager.board_[i].GetComponent<Square>().selectionStatus != ' ')
                                        manager.board_[i].GetComponent<Square>().selectionStatus = ' ';
                                }
                                switch (moveBy)
                                {
                                    case 2:

                                        break;
                                    case 4:
                                        manager.board_[findId(-4)].GetComponent<Square>().selectionStatus = 'g';
                                        break;
                                    case 7:
                                        if(movedPawnNumberReal.Count > 0)
                                        {
                                            manager.board_[findId(sevenMove)].GetComponent<Square>().selectionStatus = 'g';
                                        }
                                        else
                                        {
                                            for (int i = 1; i < 60 + 6*4; i++)
                                            {
                                                manager.board_[i].GetComponent<Square>().selectionStatus = ' ';
                                            }
                                            for (int i = 1; i <= sevenMove; i++)
                                            {
                                                manager.board_[findId(i)].GetComponent<Square>().selectionStatus = 'y';
                                            }
                                        }
                                        break;
                                    default:
                                        if(goThroughAll)
                                        {
                                            int realMove = -1;
                                            GameObject[] pawns = GameObject.FindGameObjectsWithTag("Player");
                                            foreach (GameObject pawn in pawns)
                                            {
                                                if (pawn.GetComponent<PawnMove>().moveBy != 0)
                                                {
                                                    realMove = pawn.GetComponent<PawnMove>().moveBy;
                                                    break;
                                                }
                                            }
                                            switch(realMove)
                                            {
                                                case 3:
                                                    if (ourPawn)
                                                        manager.board_[findId(3)].GetComponent<Square>().selectionStatus = 'g';
                                                    else
                                                        manager.board_[findId(-3)].GetComponent<Square>().selectionStatus = 'g';
                                                    break;
                                            }
                                        }
                                        else
                                            manager.board_[findId(moveBy)].GetComponent<Square>().selectionStatus = 'g';
                                        break;
                                }
                            }
                        }
                    }
                }
                if (hit.transform.gameObject.GetComponent<Square>() != null)
                {
                    //  if(manager.board_[hit.transform.gameObject.GetComponent<Square>().squareID] == null)
                    //      Debug.Log(false);
                    //  else
                    //      Debug.Log(manager.board_[hit.transform.gameObject.GetComponent<Square>().squareID].GetComponent<Square>().squareID);
                    if (moveBy == 7)
                    {
                        if (movedPawnNumberTest.Count > 0)
                        {
                            bool on = false;
                            for (int i = 1; i <= sevenMove; i++)
                            {
                                if (manager.board_[findId(i)] == hit.transform.gameObject)
                                    on = true;
                            }
                            if (on)
                            {
                                for (int i = 1; i <= 7; i++)
                                {
                                    if(manager.board_[findId(i)].GetComponent<Square>().squareID == hit.transform.gameObject.GetComponent<Square>().squareID)
                                        manager.board_[findId(i)].GetComponent<Square>().selectionStatus = 'g';
                                    else if(movedPawnNumberReal.Count == 0)
                                       manager.board_[findId(i)].GetComponent<Square>().selectionStatus = 'y';
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    int findId(int moveAmount)
    {
        int id = currentID;
        int number = 0;
        switch (color)
        {
            case 'y':
                number = 0;
                break;
            case 'g':
                number = 1;
                break;
            case 'r':
                number = 2;
                break;
            case 'b':
                number = 3;
                break;
        }
        if (moveAmount < 0)
        {
            Debug.Log(pawnNumber);
            if (start)
                return 84 + number;
            if(id >= 60 && id + moveAmount < 60 + 6 * number)
            {
                Debug.Log("LeaveSafeZone");
                return (id + moveAmount + 63 + 9 * number) % 60;
            }
            else if(id + moveAmount < 0)
                return (id + moveAmount + 60);
        }
        if (start)
            id = number * 15 + 3;
        if (id < 60 + 6 * number)
        {
            if (id - 15 * number < 3)
            {
                if (id + moveAmount - 15 * number < 3)
                {
                    Debug.Log("Normal");
                    return id + moveAmount;
                }
                else if (id + moveAmount + 60 - 3 - 9 * number < 66 + number * 6)
                {
                    Debug.Log("EnterSafeZone" + " " + id + moveAmount);
                    return id + moveAmount + 60 - 3 - 9 * number;
                }
                Debug.Log("TooFar");
                return id;
            }
            else if (60 + 6 * number <= id + moveAmount - 3)
            {
                Debug.Log("AroundToSafeZone");
                return id + moveAmount - 3;
            }
        }
        else if (id + moveAmount < 66 + number * 6)
        {
            Debug.Log("SafeZone");
            return id + moveAmount;
        }
        else
        {
            Debug.Log("TooFar");
            return id;
        }
        return (id + moveAmount) % 60;
    }
    // Update is called once per frame
    void Update()
    {
        MyOnMouseDown();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int count = (moveBy == 7) ? 1 : 0;
            if (movedPawnNumberTest.Count > 0)
            {
                if (movedPawnNumberTest[movedPawnNumberTest.Count - 1] == pawnNumber)
                {
                    if (Selected() == 1)
                    {
                        movedPawnNumberReal.Add(movedPawnNumberTest[0]);
                        ClearList(movedPawnNumberTest);
                        for (int j = 0; j < 60 + 4 * 7; j++)
                        {
                            if (manager.board_[j] != null)
                            {
                                if (manager.board_[j].GetComponent<Square>().selectionStatus != ' ')
                                {
                                    if (manager.board_[j].GetComponent<Square>().selectionStatus == 'g')
                                    {
                                        if (moveBy == 7)
                                        {
                                            for (int i = 1; i <= 7; i++)
                                            {
                                                if (findId(i) == j)
                                                    sevenMove -= i;
                                            }
                                        }
                                        moveTo = manager.board_[j];
                                    }
                                    manager.board_[j].GetComponent<Square>().selectionStatus = ' ';
                                }
                            }
                        }
                        if (start) start = false;
                        if (movedPawnNumberReal.Count > count || sevenMove == 0)
                        {
                            moveBy = 0;
                            timer = 1.0f;
                            selectionMade = true;
                        }
                    }
                }
            }
        }
        if (manager.turn == 0 && color == 'y' || manager.turn == 1 && color == 'g' || manager.turn == 2 && color == 'r' || manager.turn == 3 && color == 'b')
        {
            if (moveBy == 0)
            {
                if (Input.GetKeyDown(KeyCode.Alpha2))
                    moveBy = 1;
                if (Input.GetKeyDown(KeyCode.Alpha2))
                    moveBy = 2;
                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    goThroughAll = true;
                    moveBy = 3;
                }
                if (Input.GetKeyDown(KeyCode.Alpha4))
                    moveBy = 4;
                if (Input.GetKeyDown(KeyCode.Alpha5))
                    moveBy = 5;
                if (Input.GetKeyDown(KeyCode.Alpha6))
                    moveBy = 6;
                if (Input.GetKeyDown(KeyCode.Alpha7))
                    moveBy = 7;
                if (Input.GetKeyDown(KeyCode.Alpha8))
                    moveBy = 8;
                if (Input.GetKeyDown(KeyCode.Alpha9))
                    moveBy = 9;
                if (Input.GetKeyDown(KeyCode.Alpha0))
                    moveBy = 10;
                if (Input.GetKeyDown(KeyCode.Q))
                    moveBy = 11;
                if (Input.GetKeyDown(KeyCode.E))
                    moveBy = 12;
            }
        }
        if (moveTo != null && selectionMade)
        {
            //  Debug.Log(pawnNumber + " " + moveTo.GetComponent<Square>().squareID);
            if (timer > 0.9f)
            {
                LeanTween.moveLocalY(gameObject, moveTo.transform.position.y + 1.25f, 0.3f);
            }
            else if (timer > 0.6f)
            {
                LeanTween.moveLocalZ(gameObject, moveTo.transform.position.z + (Random.value - 0.5f) / 4, 0.3f);
                LeanTween.moveLocalX(gameObject, moveTo.transform.position.x + (Random.value - 0.5f) / 4, 0.3f);
            }
            else if (timer > 0.0f)
            {
                LeanTween.moveLocalY(gameObject, moveTo.transform.position.y + 0.25f, 0.3f);
            }
            else
            {
                GameObject[] pawns = GameObject.FindGameObjectsWithTag("Player");
                foreach (GameObject pawn in pawns)
                {
                    
                    if(pawn.GetComponent<PawnMove>().moveTo != null)
                    {
                        pawn.GetComponent<PawnMove>().currentID = pawn.GetComponent<PawnMove>().moveTo.GetComponent<Square>().squareID;
                        pawn.GetComponent<PawnMove>().moveTo = null;
                    }
                    pawn.GetComponent<PawnMove>().moveBy = 0;
                    pawn.GetComponent<PawnMove>().ClearList(movedPawnNumberReal);
                }
                goThroughAll = false;
                sevenMove = 7;
                timer = 0.0f;
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
                if (manager.board_[j].GetComponent<Square>().selectionStatus == 'g')
                    numberOfPiecesSelected++;
            }
        }
        return numberOfPiecesSelected;
    }
    void ClearList(List<int> listToClear)
    {
        int count = listToClear.Count;
        for (int i = 0; i < count; i++)
            listToClear.RemoveAt(0);
    }
}