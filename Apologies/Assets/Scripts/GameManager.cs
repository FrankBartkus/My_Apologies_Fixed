using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int turn = 0;
    public GameObject[] board_ = new GameObject[60+4*7];
    public Color selected;
    public Color highlighted;
    public Color unSelected;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] boards = GameObject.FindGameObjectsWithTag("Board");
        for(int i = 0; i < 60; i++)
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
                    break;
                case 'g':
                    for (int j = 0; j <= 6; j++)
                    {
                        if (board.GetComponent<Square>().squareID == j + 60 + 6)
                            board_[60 + j + 6] = board;
                    }
                    break;
                case 'r':
                    for (int j = 0; j <= 6; j++)
                    {
                        if (board.GetComponent<Square>().squareID == j + 60 + 12)
                            board_[60 + j + 12] = board;
                    }
                    break;
                case 'b':
                    for (int j = 0; j <= 6; j++)
                    {
                        if (board.GetComponent<Square>().squareID == j + 60 + 18)
                            board_[60 + j + 18] = board;
                    }
                    break;
            }
        }
        int id = 0;
        GameObject[] pawns = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject pawn in pawns)
        {
            pawn.GetComponent<PawnMove>().pawnNumber = id++;
        }
    }
    void Update()
    {
        for (int j = 0; j < 60 + 4 * 6; j++)
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
}
