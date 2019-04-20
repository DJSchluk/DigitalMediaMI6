using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ChessPieceFactory))]

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance { set; get; }

    //public List<GameObject> chessPiecePrefabs;
    private bool[,] allowedMoves { set; get; }

    public Chessman[,] ChessPieces{ set; get; }

    public bool isWhiteTurn = true;
    

    private int selectionX = -1;
    private int selectionY = -1;

    ChessPieceSpawner spawner;
    DrawBoard DrawBoard;

    private void Start()
    {
        Instance = this;
        DrawBoard = new DrawBoard();
        spawner = new ChessPieceSpawner(this.transform);
        spawner.SpawnAllPieces();
    }

    private void Update()
    {
        DrawBoard.UpdateDrawBoard();
        UpdateSelection();

        if (Input.GetMouseButtonDown(0))
        {
            if (selectionX >= 0 && selectionY >= 0)
            {
                if (spawner.selectedChessPiece == null)
                {
                    SelectChessman(selectionX, selectionY);
                }
                else
                {
                    MoveChessman(selectionX, selectionY);
                }
            }
        }
    }

    private void SelectChessman(int x, int y)
    {
        if (spawner.ChessPieces[x, y] == null)
            return;

        if (spawner.ChessPieces[x, y].isWhite != isWhiteTurn)
            return;

        spawner.selectedChessPiece = spawner.ChessPieces[x, y];
        BoardHighlights.Instance.HighLightAllowedMoves(allowedMoves);
    }

    private void MoveChessman(int x, int y)
    {
        if (allowedMoves[x, y])
        {
            Chessman c = spawner.ChessPieces[x, y];
            if (c != null && c.isWhite != isWhiteTurn)
            {

                //Figur schlagen
                //Falls König
                if (c.GetType() == typeof(Koenig))
                {
                    //end
                    EndGame();
                    return;
                }
                spawner.activeChessPiece.Remove(c.gameObject);
                Destroy(c.gameObject);
            }
            spawner.ChessPieces[spawner.selectedChessPiece.CurrentX, spawner.selectedChessPiece.CurrentY] = null;
            spawner.selectedChessPiece.transform.position = spawner.GetTileCenter(x, y);
            spawner.selectedChessPiece.setPosition(x, y);
            spawner.ChessPieces[x, y] = spawner.selectedChessPiece;
            isWhiteTurn = !isWhiteTurn;
        }
        BoardHighlights.Instance.HideHighlights();
        spawner.selectedChessPiece = null;
    }


    private void UpdateSelection()
    {
        if (!Camera.main)
            return;

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25.0f, LayerMask.GetMask("ChessPlane")))
        {
            DrawBoard.SetSelectionX((int)hit.point.x);
            DrawBoard.SetSelectionY((int)hit.point.z);
        }
        else
        {
            DrawBoard.SetSelectionX(-1);
            DrawBoard.SetSelectionY(-1);
        }
    }

    public float GetSelectionX()
    {
        return selectionX;
    }

    public float GetSelectionY()
    {
        return selectionY;
    }


    private void EndGame()
    {
        if (isWhiteTurn)
            Debug.Log("White team wins");
        else
            Debug.Log("Black team wins");

        foreach (GameObject go in spawner.activeChessPiece)
            Destroy(go);

        isWhiteTurn = true;
        BoardHighlights.Instance.HideHighlights();
        spawner.SpawnAllPieces();

    }

}
