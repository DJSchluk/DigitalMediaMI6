using ControllerSelection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (ChessPieceFactory))]

public class BoardManager : MonoBehaviour {
    public static BoardManager Instance { set; get; }

	//private bool[, ] allowedMoves { set; get; }

	public bool isWhiteTurn = true;
	private ChessPiece selectedChessPiece;

    
	private Client client;
	ChessPieceSpawner spawner;
	//DrawBoard drawBoard;
    SelectionManager selection;

    /*
	 [HideInInspector]
    //public OVRInput.Controller activeController = OVRInput.Controller.None;
    public OVRInput.Controller activeController = OVRInput.Controller.RTouch;
	*/

    /*[Header("(Optional) Tracking space")]
    [Tooltip("Tracking space of the OVRCameraRig.\nIf tracking space is not set, the scene will be searched.\nThis search is expensive.")]
    public Transform trackingSpace = null;*/

    [Header("Selection")]
    //[Tooltip("Primary selection button")]
    // OVRInput.Button primaryButton = OVRInput.Button.One;
    //[Tooltip("Secondary selection button")]
    //public OVRInput.Button secondaryButton = OVRInput.Button.SecondaryIndexTrigger;
    //[Tooltip("Maximum raycast distance")]
    public float raycastDistance = 500;

	private bool IsMyTurn
	{
		get
		{
			return client.isHost == isWhiteTurn;
		}
	}


	private void Start () {
        Instance = this;
        selection = new SelectionManager ();
        //drawBoard = new DrawBoard ();
        spawner = new ChessPieceSpawner (this.transform);
		client = FindObjectOfType<Client>();
		isWhiteTurn = true;
		spawner.SpawnAllPieces ();

        
    }

	private void Update()
	{
		bool IsInputPressed,
				IsFieldSelected;
		int X,
				Y;

		(new DrawBoard()).UpdateDrawBoard();

		// IsInputPressed = OVRInput.Get( OVRInput.Button.One );
		IsInputPressed = Input.GetMouseButtonDown(0);

		if (IsInputPressed && IsMyTurn)
		{
			IsFieldSelected = selection.CheckSelection();

			if (IsFieldSelected)
			{
				X = selection.X;
				Y = selection.Y;

				if (selectedChessPiece == null)
				{
					SelectChessPiece(X, Y);
				}
				else
				{
					if (selectedChessPiece.CheckIfMoveIsValid(X, Y))
					{
						SendMoveSelectedChessPieceToMessage(X, Y);
						MoveSelectedChessPieceTo(X, Y);
					}
				}
			}
		}
	}

	public void SelectChessPiece(int x, int y, bool highlight = true)
	{
		selectedChessPiece = spawner.ChessPieces[x, y];

		if (selectedChessPiece != null && highlight)
			BoardHighlights.Instance.HighLightAllowedMoves(selectedChessPiece);
	}

	public void MoveSelectedChessPieceTo(int X, int Y)
	{
		ChessPiece victimPiece = spawner.ChessPieces[X, Y];

		// If no piece is selected, we cannot move it.
		if (selectedChessPiece == null)
			return;

		if (victimPiece != null
		 && victimPiece.isWhite != selectedChessPiece.isWhite)
		{
			if (victimPiece is King)
			{
				EndGame();
				return;
			}

			spawner.RemoveChessPiece(victimPiece);

			//Destroy ersetzt durch immediate um das ende zu bekommen
			DestroyImmediate(victimPiece.gameObject);
		}

		selectedChessPiece.SetPosition(X, Y);

		EndTurn();
	}

	private void EndTurn()
	{
		if (isWhiteTurn)
			isWhiteTurn = false;
		else
			isWhiteTurn = true;

		selectedChessPiece = null;

		BoardHighlights.Instance.HideHighlights();
	}

	private void SendMoveSelectedChessPieceToMessage(int X, int Y)
	{
		string message = "CMOV|";
		message += selectedChessPiece.X + "|" + selectedChessPiece.Y + "|";
		message += X + "|" + Y;

		client.Send(message);
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
		BoardHighlights.Instance.HideHighlights();
		spawner.SpawnAllPieces();
	}
}
