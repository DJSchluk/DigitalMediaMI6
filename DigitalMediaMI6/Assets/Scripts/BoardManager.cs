using ControllerSelection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (ChessPieceFactory))]

public class BoardManager : MonoBehaviour
{
	[HideInInspector]
	public OVRInput.Controller activeController = OVRInput.Controller.RTouch;

	public static BoardManager Instance { set; get; }
	
	public bool isWhiteTurn = true;
	private Client client;
	private ChessPieceSpawner spawner;
	private SelectionManager selection;
	
	private bool IsMyTurn
	{
		get
		{
			return client.isHost == isWhiteTurn;
		}
	}
	
	private void Start() 
	{
        Instance = this;

        spawner = new ChessPieceSpawner( this.transform );
		selection = new SelectionManager( this.spawner );
		client = FindObjectOfType<Client>();

		spawner.SpawnAllPieces();

		isWhiteTurn = true;
	}

	private void Update()
	{
		bool IsInputPressed = false;
		SelectionAction action = SelectionAction.None;

		(new DrawBoard()).UpdateDrawBoard();

		//IsInputPressed = OVRInput.Get( OVRInput.Button.One );
		IsInputPressed = Input.GetMouseButtonDown(0);

		if (IsInputPressed && IsMyTurn)
		{
			action = selection.CheckAction();

			switch( action )
			{
				case SelectionAction.Select:
					HighlightPiece( selection.Piece );
					break;

				case SelectionAction.DeSelect:
					RemoveHighlights();
					break;

				case SelectionAction.Move:
					Debug.Log( "Update" );
					SendMoveSelectedChessPieceMessage();
					MoveSelectedChessPiece();
					break;
			}

			selection.FinishAction( action );
		}
	}

	private void RemoveHighlights()
	{
		BoardHighlights.Instance.HideHighlights();
	}

	private void HighlightPiece(ChessPiece piece)
	{
		RemoveHighlights();

		if ( piece != null )
			BoardHighlights.Instance.HighLightAllowedMoves( piece );
	}


	public void ProcessEnemiesTurn( int Xs, int Ys, int Xd, int Yd )
	{
		selection.Select( Xs, Ys );
		selection.Select( Xd, Yd );

		Debug.Log( "ProcessEnemiesTurn" );
		MoveSelectedChessPiece();
	}

	private void MoveSelectedChessPiece()
	{
		Debug.Log( "Move Chess Piece" );

		ChessPiece victimPiece = spawner.ChessPieces[(int)selection.ClickedField.x, (int)selection.ClickedField.y];
		
		if (victimPiece != null
		 && victimPiece.isWhite != selection.Piece.isWhite)
		{
			if (victimPiece is King)
			{
				EndGame();
				return;
			}

			spawner.RemoveChessPiece(victimPiece);
			DestroyImmediate( victimPiece.gameObject );
		}

		ToggleTurn();
	}

	private void ToggleTurn()
	{
		Debug.Log( "BeforeToggle: " + isWhiteTurn.ToString() );

		if (isWhiteTurn)
			isWhiteTurn = false;
		else
			isWhiteTurn = true;

		Debug.Log( "AfterToggle: " + isWhiteTurn.ToString() );
		Debug.Log( "IsMyTurn: " + IsMyTurn.ToString() );
	}

	private void SendMoveSelectedChessPieceMessage()
	{
		string message = "CMOV|";
		message += BuildCoordinateString( selection.SelectedField ) + "|";
		message += BuildCoordinateString( selection.ClickedField );

		client.Send( message );
	}

	private string BuildCoordinateString( Vector2 coords )
	{
		return ( (int)coords.x ).ToString() + "|" + ( (int)coords.y ).ToString();
	}

	private void EndGame()
	{
		if (isWhiteTurn)
			Debug.Log("White team wins");
		else
			Debug.Log("Black team wins");

		foreach (GameObject go in spawner.activeChessPieces)
			DestroyImmediate(go);

		isWhiteTurn = true;
		RemoveHighlights();

		spawner.SpawnAllPieces();
	}
}
