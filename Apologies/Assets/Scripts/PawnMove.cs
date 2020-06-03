using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // 1
using System;
using System.Security.Policy;

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
    Dice die;
    bool start = true;
    public char color;
    static float timer = 0.0f;
    int choice = 0;
    System.Random rand = new System.Random();
    // Start is called before the first frame update
    void Start()
    {
        currentID = 84 + GetColor(color);
        manager = GameObject.FindWithTag("Manager").GetComponent<GameManager>();
        die = GameObject.FindWithTag("Finish").GetComponent<Dice>();
    }
    int GetColor(char c)
    {
        if (c == 'y')
            return 0;
        if (c == 'g')
            return 1;
        if (c == 'r')
            return 2;
        if (c == 'b')
            return 3;
        return -1;
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
                    if (manager.turn == GetColor(color) || goThroughAll)
                    {
                        if (moveBy != 0 && !selectionMade || goThroughAll)
                        {
                            if (hit.transform.gameObject.GetComponent<PawnMove>().pawnNumber == pawnNumber)
                            {
                                ClearList(movedPawnNumberTest);
                                movedPawnNumberTest.Add(pawnNumber);
                                foreach (GameObject square in manager.board_)
                                    square.GetComponent<Square>().selectionStatus = ' ';
                                switch (moveBy)
                                {
                                    case 1:
                                        if (choice == 1)
                                            manager.board_[findId(1)].GetComponent<Square>().selectionStatus = 'g';
                                        if (choice == 2)
                                        {
                                            if (currentID < 84)
                                                manager.board_[GetColor(color) * 6 + 65].GetComponent<Square>().selectionStatus = 'g';
                                            else
                                                manager.board_[currentID].GetComponent<Square>().selectionStatus = 'g';
                                        }
                                        break;
                                    case 2:
                                        if (currentID < 60)
                                            manager.board_[GetColor(color) + 84].GetComponent<Square>().selectionStatus = 'g';
                                        else if (currentID < 84)
                                            manager.board_[GetColor(color) * 6 + 65].GetComponent<Square>().selectionStatus = 'g';
                                        else
                                            manager.board_[GetColor(color) + 84].GetComponent<Square>().selectionStatus = 'g';
                                        break;
                                    case 4:
                                        manager.board_[findId(-4)].GetComponent<Square>().selectionStatus = 'g';
                                        break;
                                    case 7:
                                        if (movedPawnNumberReal.Count > 0)
                                        {
                                            manager.board_[findId(sevenMove)].GetComponent<Square>().selectionStatus = 'g';
                                        }
                                        else
                                        {
                                            for (int i = 1; i < 60 + 6 * 4; i++)
                                            {
                                                manager.board_[i].GetComponent<Square>().selectionStatus = ' ';
                                            }
                                            for (int i = 1; i <= sevenMove; i++)
                                            {
                                                manager.board_[findId(i)].GetComponent<Square>().selectionStatus = 'y';
                                            }
                                        }
                                        break;
                                    case 10:
                                        if (choice == 1)
                                            manager.board_[findId(10)].GetComponent<Square>().selectionStatus = 'g';
                                        if (choice == 2)
                                        {
                                            moveBy = rand.Next(1, 7);
                                            MyOnMouseDown();
                                        }
                                        break;
                                    case 11:
                                        if (choice == 1)
                                            manager.board_[findId(11)].GetComponent<Square>().selectionStatus = 'g';
                                        if (choice == 2)
                                        {
                                            if (currentID < 60)
                                            {
                                                if (countOthers() == 0)
                                                    manager.board_[currentID].GetComponent<Square>().selectionStatus = 'g';
                                            }
                                            else
                                                manager.board_[currentID].GetComponent<Square>().selectionStatus = 'g';
                                        }
                                        break;
                                    case 12:
                                        if (choice == 1)
                                            manager.board_[findId(1)].GetComponent<Square>().selectionStatus = 'g';
                                        if (choice == 2)
                                        {
                                            if(currentID < 84)
                                                manager.board_[currentID].GetComponent<Square>().selectionStatus = 'g';
                                            else if (countOthers() == 0)
                                                manager.board_[currentID].GetComponent<Square>().selectionStatus = 'g';
                                        }
                                        break;
                                    default:
                                        if (goThroughAll)
                                        {
                                            int realMove = -1;
                                            foreach (GameObject pawn in manager.pawns)
                                            {
                                                if (pawn.GetComponent<PawnMove>().moveBy != 0)
                                                {
                                                    realMove = pawn.GetComponent<PawnMove>().moveBy;
                                                    break;
                                                }
                                            }
                                            switch (realMove)
                                            {
                                                case 3:
                                                    if (manager.turn == GetColor(color))
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
                else if (hit.transform.gameObject.GetComponent<Square>() != null)
                {
                    //if(manager.board_[hit.transform.gameObject.GetComponent<Square>().squareID] == null)
                    //    Debug.Log(false);
                    //else
                    //    Debug.Log(manager.board_[hit.transform.gameObject.GetComponent<Square>().squareID].GetComponent<Square>().squareID);
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
                                    if (manager.board_[findId(i)].GetComponent<Square>().squareID == hit.transform.gameObject.GetComponent<Square>().squareID)
                                        manager.board_[findId(i)].GetComponent<Square>().selectionStatus = 'g';
                                    else if (movedPawnNumberReal.Count == 0)
                                        manager.board_[findId(i)].GetComponent<Square>().selectionStatus = 'y';
                                }
                            }
                        }
                    }
                    else if (moveBy == 11 || moveBy == 12)
                    {
                        if(hit.transform.gameObject.GetComponent<Square>().selectionStatus == 'y')
                            hit.transform.gameObject.GetComponent<Square>().selectionStatus = 'g';
                    }
                }
            }
            manager.LightBoard();
        }
        else if (moveBy == 1 || moveBy == 10 || moveBy == 11 || moveBy == 12)
        {
            if (manager.choice1.GetComponent<MouseText>().isHovering() || manager.choice2.GetComponent<MouseText>().isHovering())
            {
                foreach (GameObject square in manager.board_)
                    square.GetComponent<Square>().selectionStatus = ' ';
                if (moveBy != 10 || choice != 2)
                {
                    if (manager.choice1.GetComponent<MouseText>().isHovering())
                    {
                        choice = 1;
                        manager.choice1.GetComponent<MouseText>().setBig(true);
                        manager.choice2.GetComponent<MouseText>().setBig(false);
                    }
                    else if (manager.choice2.GetComponent<MouseText>().isHovering())
                    {
                        choice = 2;
                        manager.choice2.GetComponent<MouseText>().setBig(true);
                        manager.choice1.GetComponent<MouseText>().setBig(false);
                        if (moveBy == 10)
                        {
                            manager.text1.text = "";
                            manager.text2.text = "";
                        }
                    }
                }
                manager.LightBoard();
            }
        }
    }
    int findId(int moveAmount)
    {
        int id = currentID;
        int number = GetColor(color);
        if (moveAmount < 0)
        {
            if (id >= 84)
                return 84 + number;
            if (id >= 60 && id + moveAmount < 60 + 6 * number)
            {
                return (id + moveAmount + 63 + 9 * number) % 60;
            }
            else if (id + moveAmount < 0)
                return (id + moveAmount + 60);
        }
        if (id >= 84)
            id = number * 15 + 3;
        if (id < 60 + 6 * number)
        {
            if (id - 15 * number < 3)
            {
                if (id + moveAmount - 15 * number < 3)
                {
                    return id + moveAmount;
                }
                else if (id + moveAmount + 60 - 3 - 9 * number < 66 + number * 6)
                {
                    return id + moveAmount + 60 - 3 - 9 * number;
                }
                return id;
            }
            else if (60 + 6 * number <= id + moveAmount - 3)
            {
                return id + moveAmount - 3;
            }
        }
        else if (id + moveAmount < 66 + number * 6)
        {
            return id + moveAmount;
        }
        else
        {
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
                        manager.LightBoard();
                        if (start) start = false;
                        if (movedPawnNumberReal.Count > ((moveBy == 7) ? 1 : 0) || sevenMove == 0)
                        {
                            timer = 1.0f;
                            selectionMade = true;
                        }
                    }
                }
            }
        }
        if (manager.turn == GetColor(color))
        {
            if (moveBy == 0)
            {
                if(die.rollStage == 2)
                {
                    moveBy = die.die1 + die.die2;
                    switch (moveBy)
                    {
                        case 1:
                            manager.text1.text = "Move One";
                            manager.text2.text = "Move any pawn in play into Home";
                            break;
                        case 10:
                            manager.text1.text = "Move Ten";
                            manager.text2.text = "Roll One Die";
                            break;
                        case 11:
                            manager.text1.text = "Move Eleven";
                            manager.text2.text = "Change between two pawns";
                            break;
                        case 12:
                            manager.text1.text = "Move One";
                            manager.text2.text = "Take a pawn out of start in substitute for someone else's";
                            break;
                    }
                }
                else if (die.rollStage == 0 && Input.GetKeyDown(KeyCode.Space))
                {
                    die.roll(true);
                }
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    moveBy = 1;
                    manager.text1.text = "Move One";
                    manager.text2.text = "Move any pawn in play into Home";
                }
            }
        }
        if (moveTo != null && selectionMade)
        {
            //  Debug.Log(pawnNumber + " " + moveTo.GetComponent<Square>().squareID);
            if (timer > 0.9f)
            {
                manager.text1.text = "";
                manager.text2.text = "";
                manager.choice1.GetComponent<MouseText>().setBig(false);
                manager.choice2.GetComponent<MouseText>().setBig(false);
                LeanTween.moveLocalY(gameObject, moveTo.transform.position.y + 1.25f, 0.3f);
            }
            else if (timer > 0.6f)
            {
                LeanTween.moveLocalZ(gameObject, moveTo.transform.position.z + ((float)rand.NextDouble() - 0.5f) / 4, 0.3f);
                LeanTween.moveLocalX(gameObject, moveTo.transform.position.x + ((float)rand.NextDouble() - 0.5f) / 4, 0.3f);
            }
            else if (timer > 0.0f)
            {
                LeanTween.moveLocalY(gameObject, moveTo.transform.position.y + 0.25f, 0.3f);
            }
            else
            {
                goThroughAll = false;
                sevenMove = 7;
                timer = 0.0f;
                choice = 0;
                if (CheckWin() > -1)
                {
                    switch (CheckWin())
                    {
                        case 0:
                            manager.text1.text = "Yellow";
                            break;
                        case 1:
                            manager.text1.text = "Green";
                            break;
                        case 2:
                            manager.text1.text = "Red";
                            break;
                        case 3:
                            manager.text1.text = "Blue";
                            break;
                    }
                    manager.text2.text = "Wins!";
                }
                else
                {
                    if (!TakenOut())
                    {
                        selectionMade = false;
                        manager.turn = ++manager.turn % manager.players;
                        foreach (GameObject pawn in manager.pawns)
                        {
                            if (pawn.GetComponent<PawnMove>().moveTo != null)
                            {
                                pawn.GetComponent<PawnMove>().currentID = pawn.GetComponent<PawnMove>().moveTo.GetComponent<Square>().squareID;
                                pawn.GetComponent<PawnMove>().moveTo = null;
                            }
                            pawn.GetComponent<PawnMove>().moveBy = 0;
                            pawn.GetComponent<PawnMove>().ClearList(movedPawnNumberReal);
                        }
                    }
                    manager.LightBoard();
                }
            }
            timer -= Time.deltaTime;
        }
    }
    int CheckWin()
    {
        int[] inEachGoal = { 0, 0, 0, 0 };
        foreach (GameObject pawn in manager.pawns)
        {
            if (pawn.GetComponent<PawnMove>().currentID == GetColor(pawn.GetComponent<PawnMove>().color) * 6 + 65)
            {
                inEachGoal[GetColor(pawn.GetComponent<PawnMove>().color)]++;
            }
            else if(pawn.GetComponent<PawnMove>().moveTo != null)
            {
                if(pawn.GetComponent<PawnMove>().moveTo.GetComponent<Square>().squareID == GetColor(pawn.GetComponent<PawnMove>().color) * 6 + 65)
                    inEachGoal[GetColor(pawn.GetComponent<PawnMove>().color)]++;
            }
            if (inEachGoal[GetColor(pawn.GetComponent<PawnMove>().color)] == 3)
                return GetColor(pawn.GetComponent<PawnMove>().color);
        }
        return -1;
    }
    int countOthers()
    {
        int count = 0;
        PawnMove pawnMove;
        foreach (GameObject pawn in manager.pawns)
        {
            pawnMove = pawn.GetComponent<PawnMove>();
            if (pawnMove.color != color & pawnMove.currentID < 60)
            {
                manager.board_[pawnMove.currentID].GetComponent<Square>().selectionStatus = 'y';
                count++;
            }
        }
        return count;
    }
    bool TakenOut()
    {
       bool taken = false;
       PawnMove pawnMove;
       foreach (GameObject pawn in manager.pawns)
       {
           pawnMove = pawn.GetComponent<PawnMove>();
           if (pawnMove.color != color & pawnMove.currentID < 84)
            {
               if (pawnMove.currentID == moveTo.GetComponent<Square>().squareID)
               {
                    taken = true;
                    timer = 1.0f;
                    UnityEngine.Debug.Log(moveBy);
                    if (moveBy == 11)
                       pawnMove.moveTo = manager.board_[currentID];
                    else
                       pawnMove.moveTo = manager.board_[pawnMove.GetColor(pawnMove.color) + 84];
                    currentID = moveTo.GetComponent<Square>().squareID;
                    moveTo = null;
                }
           }
        }
        return taken;
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