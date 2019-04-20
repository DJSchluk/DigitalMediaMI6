using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager {

	private int selectionX = -1;
    private int selectionY = -1;

	public void UpdateSelection()
    {
        if (!Camera.main)
            return;


        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25.0f, LayerMask.GetMask("ChessPlane")))
        {
            Debug.Log("Raycast hit result " + (int)hit.point.x + ", " + (int)hit.point.z);
            //drawBoard.SetSelectionX((int)hit.point.x);
            //drawBoard.SetSelectionY((int)hit.point.z);
            selectionX = (int)hit.point.x;
            selectionY = (int)hit.point.z;
            Debug.Log("Selection variable content " + selectionX + ", " + selectionY);
        }
        else
        {
            //drawBoard.SetSelectionX(-1);
            //drawBoard.SetSelectionY(-1);
            selectionX = -1;
            selectionY = -1;
        }

    }

	public int GetSelectionX(){
		return selectionX;
	}

	public int GetSelectionY(){
		return selectionY;
	}
}
