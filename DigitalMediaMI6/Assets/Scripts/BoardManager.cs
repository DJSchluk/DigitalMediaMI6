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

    private int selectionX = -1;
    private int selectionY = -1;

    ChessPieceSpawner spawner;
    DrawBoard drawBoard;

    private void Start()
    {
        Instance = this;
        drawBoard = new DrawBoard();
        spawner = new ChessPieceSpawner(this.transform);
        spawner.SpawnAllPieces();
    }

    private void Update()
    {
        drawBoard.UpdateDrawBoard();
        UpdateSelection();

        if (Input.GetMouseButtonDown(0))
        {
            if (selectionX >= 0 && selectionY >= 0)
            {
                if (spawner.selectedChessPiece == null)
                {
                    SelectChessPiece(selectionX, selectionY);
                }
                else
                {
                    MoveChessPiece(selectionX, selectionY);
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


    private void UpdateSelection()
    {
        if (!Camera.main)
            return;


        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25.0f, LayerMask.GetMask("ChessPlane")))
        {
            Debug.Log("Raycast hit result " + (int)hit.point.x + ", " + (int)hit.point.z);
            drawBoard.SetSelectionX((int)hit.point.x);
            drawBoard.SetSelectionY((int)hit.point.z);
            selectionX = (int)hit.point.x;
            selectionY = (int)hit.point.z;
            Debug.Log("Selection variable content " + selectionX + ", " + selectionY);
        }
        else
        {
            drawBoard.SetSelectionX(-1);
            drawBoard.SetSelectionY(-1);
            selectionX = -1;
            selectionY = -1;
        }

        //Debug.Log("x = " + GetSelectionX() + ", y = " + GetSelectionY());
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

        foreach (GameObject go in spawner.activeChessPieces)
            Destroy(go);

        isWhiteTurn = true;
        BoardHighlights.Instance.HideHighlights();
        spawner.SpawnAllPieces();

    }

}
