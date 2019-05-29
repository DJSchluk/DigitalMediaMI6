using ControllerSelection;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


public enum SelectionAction
{
	Move,
	Select,
	DeSelect,
	None
}

public class SelectionManager
{
	private Vector2 clickedField;
	private Vector2 selectedField;
	private ChessPiece selectedChessPiece;
	private ChessPieceSpawner spawner;

	public Vector2 ClickedField
	{
		get { return clickedField; }
	}
	public Vector2 SelectedField
	{
		get { return selectedField; }
	}

	public bool IsFieldSelected
	{
		get 
		{ 
			return selectedField.x >= 0 && selectedField.y >= 0;
		}
	}
	public bool IsPieceSelected 
	{ 
		get
		{
			return selectedChessPiece != null;
		}
	}

	public ChessPiece Piece
	{
		get
		{
			selectedChessPiece = spawner.ChessPieces[(int)selectedField.x, (int)selectedField.y];
			return selectedChessPiece;
		}
	}

	public SelectionManager( ChessPieceSpawner spawner )
	{
		this.spawner = spawner;
		this.selectedField = new Vector2();
		this.clickedField = new Vector2();
	}

    public SelectionAction CheckAction()
    {
        try
        {
            if( !Camera.main )
                throw new Exception("Camera not main");

			InitField( clickedField );

			bool isFieldHitted;
			RaycastHit hittedField;

			isFieldHitted = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hittedField, 25.0f, LayerMask.GetMask("ChessPlane"));
			//isFieldHitted = GetLaserPointer().GetHittedField( out hittedField );

			if( isFieldHitted )
			{
				clickedField.x = hittedField.point.x;
                clickedField.y = hittedField.point.z;

				if( IsPieceSelected )
				{
					if( Piece.CheckIfMoveIsValid( clickedField ) )
						return SelectionAction.Move;
					else
						return SelectionAction.DeSelect;
				}
				else
				{
					return SelectionAction.Select;
				}

			}
        }
        catch (Exception xept)
        {
            Debug.Log(xept.Message);
        }

		return SelectionAction.None;
	}
	
	public void FinishAction(SelectionAction action, bool commit = true)
	{
		if( action == SelectionAction.Move )
		{
			if( commit )
			{
				selectedChessPiece.SetPosition( (int)clickedField.x, (int)clickedField.y );
			}
		}
		else
		{
			if( commit )
			{
				selectedField.x = clickedField.x;
				selectedField.y = clickedField.y;
			}
			else
			{
				clickedField.x = selectedField.x;
				clickedField.y = selectedField.y;
			}
		}
	}

	public void Select( int X, int Y, bool select = true )
	{
		if( select )
		{
			selectedField.x = X;
			selectedField.y = Y;
		}
		else
		{
			clickedField.x = X;
			clickedField.y = Y;
		}
	}
	
	private Pointer GetLaserPointer()
    {
        return GameObject.Find("PR_Pointer").GetComponent<Pointer>();
    }

	private void InitField( Vector2 field )
	{
		field.x = -1;
		field.y = -1;
	}
}
