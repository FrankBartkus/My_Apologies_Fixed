using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int turn = 0;
    public GameObject[] board_ = new GameObject[4 * 23];
    public GameObject[] pawns = new GameObject[4 * 3];
    public GameObject choice1;
    public GameObject choice2;
    public Text text1;
    public Text text2;
    public Color selected;
    public Color highlighted;
    public Color unSelected;
    public int players;
    // Start is called before the first frame update
    void Start()
    {
        text1 = choice1.GetComponent<Text>();
        text2 = choice2.GetComponent<Text>();
        text1.text = "";
        text2.text = "";
        GameObject[] boards = GameObject.FindGameObjectsWithTag("Board");
        pawns = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < 60; i++)
        {
            foreach (GameObject board in boards)
            {
                if (board.GetComponent<Square>().squareID == i)
                {
                    board_[i] = board;
                }
            }
        }
        foreach (GameObject board in boards)
        {
            switch (board.GetComponent<Square>().safeZone)
            {
                case 'y':
                    for (int j = 0; j < 6; j++)
                    {
                        if (board.GetComponent<Square>().squareID == j + 60)
                            board_[60 + j] = board;
                    }
                    if(board.GetComponent<Square>().squareID > 83)
                    {
                        board_[84] = board;
                    }
                    break;
                case 'g':
                    for (int j = 0; j <= 6; j++)
                    {
                        if (board.GetComponent<Square>().squareID == j + 60 + 6)
                            board_[60 + j + 6] = board;
                    }
                    if (board.GetComponent<Square>().squareID > 83)
                    {
                        board_[85] = board;
                    }
                    break;
                case 'r':
                    for (int j = 0; j <= 6; j++)
                    {
                        if (board.GetComponent<Square>().squareID == j + 60 + 12)
                            board_[60 + j + 12] = board;
                    }
                    if (board.GetComponent<Square>().squareID > 83)
                    {
                        board_[86] = board;
                    }
                    break;
                case 'b':
                    for (int j = 0; j <= 6; j++)
                    {
                        if (board.GetComponent<Square>().squareID == j + 60 + 18)
                            board_[60 + j + 18] = board;
                    }
                    if (board.GetComponent<Square>().squareID > 83)
                    {
                        board_[87] = board;
                    }
                    break;
            }
        }
        int id = 0;
        foreach (GameObject pawn in pawns)
        {
            pawn.GetComponent<PawnMove>().pawnNumber = id++;
        }
        LightBoard();
    }
    public void LightBoard()
    {
        for (int j = 0; j < 60 + 4 * 7; j++)
        {
            if (board_[j] != null)
            {
                switch (board_[j].GetComponent<Square>().selectionStatus)
                {
                    case 'g':
                        board_[j].GetComponent<SpriteRenderer>().color = selected;
                        break;
                    case 'y':
                        board_[j].GetComponent<SpriteRenderer>().color = highlighted;
                        break;
                    default:
                        board_[j].GetComponent<SpriteRenderer>().color = unSelected;
                        break;
                }
            }
        }
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
}
