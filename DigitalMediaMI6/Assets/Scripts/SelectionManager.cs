using ControllerSelection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


public class SelectionManager {

	private int selectionX = -1;
    private int selectionY = -1;

    [HideInInspector]
    public OVRInput.Controller activeController = OVRInput.Controller.None;

    Transform trackingSpace = null;



    public void UpdateSelection()
    {
        if (!Camera.main)
            return;

        

        RaycastHit hit;

        Ray selectionRay = OVRInputHelpers.GetSelectionRay(activeController, trackingSpace);
        selectionRay = Camera.main.ScreenPointToRay(new Vector2(Screen.height / 2, Screen.width / 2));
        if (Physics.Raycast(selectionRay, out hit))
        //if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25.0f, LayerMask.GetMask("ChessPlane")))
        {
            
            //Debug.Log("Raycast hit result " + (int)hit.point.x + ", " + (int)hit.point.z);
            selectionX = (int)hit.point.x;
            selectionY = (int)hit.point.z;
            //Debug.Log("Selection variable content " + selectionX + ", " + selectionY);
        }
        else
        {
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
