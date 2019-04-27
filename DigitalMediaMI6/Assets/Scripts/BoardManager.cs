using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (ChessPieceFactory))]

public class BoardManager : MonoBehaviour {
    public static BoardManager Instance { set; get; }

    private bool[, ] allowedMoves { set; get; }

<<<<<<< HEAD
    public ChessPiece[, ] ChessPieces { set; get; }

=======
>>>>>>> 8673b91f952f76853e90e950749f5c8ca1f3e496
    private ChessPiece selectedChessPiece;

    public bool isWhiteTurn = true;

    ChessPieceSpawner spawner;
    DrawBoard drawBoard;

    SelectionManager selection;

    private void Start () {
        Instance = this;
        selection = new SelectionManager ();
        drawBoard = new DrawBoard ();
        spawner = new ChessPieceSpawner (this.transform);
        spawner.SpawnAllPieces ();
    }

<<<<<<< HEAD
    private void Update () {
        drawBoard.UpdateDrawBoard ();
        selection.UpdateSelection ();

        if (Input.GetMouseButtonDown (0)) {
            //    Debug.Log("Klick!");
            if (selection.GetSelectionX () >= 0 && selection.GetSelectionY () >= 0) {
                //        Debug.Log("x >= 0 && y >= 0");
                if (selectedChessPiece == null) {
                    SelectChessPiece (selection.GetSelectionX (), selection.GetSelectionY ());
                    //            Debug.Log("Ausgewähltes Feld: " + selection.GetSelectionX() + ", " + selection.GetSelectionY());
                } else {
                    MoveChessPiece (selection.GetSelectionX (), selection.GetSelectionY ());
=======
    private void Update()
    {
        drawBoard.UpdateDrawBoard();
        selection.UpdateSelection();

        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("Klick!");
            if (selection.GetSelectionX() >= 0 && selection.GetSelectionY() >= 0)
            {
                //Debug.Log("x >= 0 && y >= 0");
                if (selectedChessPiece == null)
                {
                    SelectChessPiece(selection.GetSelectionX(), selection.GetSelectionY());
                    Debug.Log("Ausgewähltes Feld: " + selection.GetSelectionX() + ", " + selection.GetSelectionY());
                }
                else
                {
                    MoveChessPiece(selection.GetSelectionX(), selection.GetSelectionY());
>>>>>>> 8673b91f952f76853e90e950749f5c8ca1f3e496

                }
            }
        }
    }

    private void SelectChessPiece (int x, int y) {
        if (spawner.ChessPieces[x, y] == null) {
            Debug.Log ("Keine Figur");
            return;
        }

        if (spawner.ChessPieces[x, y].isWhite != isWhiteTurn) {
            Debug.Log ("Nicht am Zug");
            return;
        }
<<<<<<< HEAD

        allowedMoves = ChessPieces[x, y].PossibleMove ();
=======
        
        allowedMoves = spawner.ChessPieces[x,y].PossibleMove();
>>>>>>> 8673b91f952f76853e90e950749f5c8ca1f3e496
        selectedChessPiece = spawner.ChessPieces[x, y];
        Debug.Log (selectedChessPiece.CurrentX + ", " + selectedChessPiece.CurrentY);
        BoardHighlights.Instance.HighLightAllowedMoves (allowedMoves);
    }

    private void MoveChessPiece (int x, int y) {
        //ALLOWED MOVES MUSS GEFIXT WERDEN
<<<<<<< HEAD
        if (allowedMoves[x, y] == true) {
            ChessPiece c = spawner.ChessPieces[x, y];
            if (c != null && c.isWhite != isWhiteTurn) {
=======
        if (allowedMoves[x, y] == true)
        {
            ChessPiece c = spawner.ChessPieces[x, y];
            if (c != null && c.isWhite != isWhiteTurn)
            {
>>>>>>> 8673b91f952f76853e90e950749f5c8ca1f3e496

                //Figur schlagen
                //Falls König
                if (c.GetType () == typeof (King)) {
                    //end
                    EndGame ();
                    return;
                }
<<<<<<< HEAD
                spawner.activeChessPieces.Remove (c.gameObject);
                Destroy (c.gameObject);
=======
                spawner.activeChessPieces.Remove(c.gameObject);
                Destroy(c.gameObject);
>>>>>>> 8673b91f952f76853e90e950749f5c8ca1f3e496
            }
            spawner.ChessPieces[selectedChessPiece.CurrentX, selectedChessPiece.CurrentY] = null;
            selectedChessPiece.transform.position = spawner.GetTileCenter (x, y);
            selectedChessPiece.setPosition (x, y);
            spawner.ChessPieces[x, y] = selectedChessPiece;
            isWhiteTurn = !isWhiteTurn;
        }
        BoardHighlights.Instance.HideHighlights ();
        selectedChessPiece = null;
    }

    private void EndGame () {
        if (isWhiteTurn)
            Debug.Log ("White team wins");
        else
            Debug.Log ("Black team wins");

        foreach (GameObject go in spawner.activeChessPieces)
            Destroy (go);

        isWhiteTurn = true;
        BoardHighlights.Instance.HideHighlights ();
        spawner.SpawnAllPieces ();

    }
<<<<<<< HEAD

}
=======
}
>>>>>>> 8673b91f952f76853e90e950749f5c8ca1f3e496
