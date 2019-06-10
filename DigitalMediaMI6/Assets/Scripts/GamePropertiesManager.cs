using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum InputType
{
	VR,
	Mouse
}

enum ClientMode
{
	OnePlayer,
	TwoPlayers
}

public class GamePropertiesManager
{
	private static GamePropertiesManager instance;

	private ClientMode clientMode;
	private InputType inputType;
	private bool allowClickOnPiece;

	public static GamePropertiesManager Instance
	{
		get
		{
			if (instance == null)
				instance = new GamePropertiesManager();

			return instance;
		}
	}

	public GamePropertiesManager()
	{
		clientMode = ClientMode.OnePlayer;
		inputType = InputType.Mouse;
		allowClickOnPiece = false;
	}

	#region Code
	public bool CheckIfInputIsPressed()
	{
		bool IsInputPressed = false;

		if( inputType == InputType.Mouse )
			IsInputPressed = Input.GetMouseButtonDown( 0 );

		else if( inputType == InputType.VR )
			IsInputPressed = OVRInput.Get( OVRInput.Button.One );

		return IsInputPressed;
	}

	public bool CheckIfFieldIsHitted( ChessPieceSpawner spawner, out RaycastHit hittedField )
	{
		bool isFieldHitted = false;
		Ray ray;
		float maxDistance;

		if( inputType == InputType.Mouse )
		{
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			maxDistance = 25.0f;
		}
		else //if( inputType == InputType.VR )
		{
			ray = VrPointer.GetRay();
			maxDistance = 0.0f;
		}

		isFieldHitted = Physics.Raycast( ray, out hittedField, maxDistance, LayerMask.GetMask("ChessPlane") );
		
		if( allowClickOnPiece )
		{
			DebugLogger.LogValue( "CheckIfFieldIsHitted", "X", hittedField.point.x );
			DebugLogger.LogValue( "CheckIfFieldIsHitted", "Y", hittedField.point.z );

			if( isFieldHitted == false
			 && hittedField.collider.gameObject != null )
			{
				for( int iX = 0; iX < 7; iX++ )
				{
					for( int iY = 0; iY < 7; iY++ )
					{
						ChessPiece piece = spawner.ChessPieces[iX,iY];

						if( piece != null 
						 && piece == hittedField.collider.gameObject )
						{
							hittedField.point = new Vector3( iX, 0, iY );
							isFieldHitted = true;
							break;
						}
					}

					if( isFieldHitted )
						break;
				}
			}
		}

		return isFieldHitted;
	}

	public int Players
	{
		get
		{
			return clientMode == ClientMode.OnePlayer ? 1 : 2;
		}
	}

	private Pointer VrPointer
	{
		get
		{
			return GameObject.Find("PR_Pointer").GetComponent<Pointer>();
		}
	}
	#endregion
}

public static class DebugLogger
{
	private static List<string> loggingFunctions = new List<string>();
	private static bool logFunctionNames = true;

	public static List<string> Functions
	{
		get
		{
			return loggingFunctions;
		}
	}

	public static bool LogFunctionNames
	{
		get
		{
			return logFunctionNames;
		}

		set
		{
			logFunctionNames = value;
		}
	}

	public static void Log( string function, string message )
	{
		string finalMessage = "";

		if( !loggingFunctions.Contains( function ) )
			return;

		if( logFunctionNames )
			finalMessage += function + ": ";

		finalMessage += message;

		Debug.Log( finalMessage );
	}

	public static void LogValue( string function, string valueName, object value )
	{
		Log( function, valueName + " = " + value.ToString() );
	}
}