using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ChessPieceFactory))]

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance { set; get; }

    private bool[,] allowedMoves { set; get; }

    public ChessPiece[,] ChessPieces { set; get; }

    public bool isWhiteTurn = true;

    ChessPieceSpawner spawner;
    DrawBoard drawBoard;

    SelectionManager selection;

    private void Start()
    {
        Instance = this;
        selection = new SelectionManager();
        drawBoard = new DrawBoard();
        spawner = new ChessPieceSpawner(this.transform);
        spawner.SpawnAllPieces();
    }

    private void Update()
    {
        drawBoard.UpdateDrawBoard();
        selection.UpdateSelection();

        if (Input.GetMouseButtonDown(0))
        {
            if (selection.GetSelectionX() >= 0 && selection.GetSelectionY() >= 0)
            {
                if (spawner.selectedChessPiece == null)
                {
                    SelectChessPiece(selection.GetSelectionX(), selection.GetSelectionY());
                }
                else
                {
                    MoveChessPiece(selection.GetSelectionX(), selection.GetSelectionY());
                }
            }
        }
    }

    private void SelectChessPiece(int x, int y)
    {
        if (spawner.ChessPieces[x, y] == null)
            return;

        if (spawner.ChessPieces[x, y].isWhite != isWhiteTurn)
            return;

        spawner.selectedChessPiece = spawner.ChessPieces[x, y];
        BoardHighlights.Instance.HighLightAllowedMoves(allowedMoves);
    }

    private void MoveChessPiece(int x, int y)
    {
        if (allowedMoves[x, y])
        {
            ChessPiece c = spawner.ChessPieces[x, y];
            if (c != null && c.isWhite != isWhiteTurn)
            {

                //Figur schlagen
                //Falls König
                if (c.GetType() == typeof(King))
                {
                    //end
                    EndGame();
                    return;
                }
                spawner.activeChessPieces.Remove(c.gameObject);
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

    private void EndGame()
    {
        if (isWhiteTurn)
            Debug.Log("White team wins");
        else
            Debug.Log("Black team wins");

        foreach (GameObject go in spawner.activeChessPieces)
            Destroy(go);

        isWhiteTurn = true;
        BoardHighlights.Instance.HideHighlights();
        spawner.SpawnAllPieces();

    }

}
