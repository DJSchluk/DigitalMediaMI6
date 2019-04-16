using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance{set; get;}
    private bool[,] allowedMoves{set; get;}

    public Chessman[,] Chessmans { set; get; }

    public bool isWhiteTurn = true;

    private int selectionX = -1;
    private int selectionY = -1;

    SpawnFigures sf;

    private void Start()
    {
        Instance  = this;
    }

    private void Update()
    {
        UpdateSelection();

        if (Input.GetMouseButtonDown(0))
        {
            if (selectionX >= 0 && selectionY >= 0)
            {
                if (sf.selectedChessman == null)
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
        if (sf.Chessmans[x, y] == null)
            return;

        if (sf.Chessmans[x, y].isWhite != isWhiteTurn)
            return;
            
        sf.selectedChessman = sf.Chessmans[x, y];
        BoardHighlights.Instance.HighLightAllowedMoves(allowedMoves);
    }

    private void MoveChessman(int x, int y)
    {
        if (allowedMoves[x,y])
        {
            Chessman c = sf.Chessmans[x,y];
            if(c != null && c.isWhite != isWhiteTurn){

                //Figur schlagen
                //Falls König
                if(c.GetType()==typeof(Koenig)){
                    //end
                    EndGame();
                    return;
                }
                sf.activeChessman.Remove(c.gameObject);
                Destroy(c.gameObject);
            }
            sf.Chessmans[sf.selectedChessman.CurrentX, sf.selectedChessman.CurrentY] = null;
            sf.selectedChessman.transform.position = sf.GetTileCenter(x, y);
            sf.selectedChessman.setPosition(x,y);
            sf.Chessmans[x, y] = sf.selectedChessman;
            isWhiteTurn = !isWhiteTurn;
        }
        BoardHighlights.Instance.HideHighlights();
        sf.selectedChessman = null;
    }


    private void UpdateSelection()
    {
        if (!Camera.main)
            return;

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25.0f, LayerMask.GetMask("ChessPlane")))
        {
            selectionX = (int)hit.point.x;
            selectionY = (int)hit.point.z;
        }
        else
        {
            selectionX = -1;
            selectionY = -1;
        }
    }


    private void EndGame()
    {
        if (isWhiteTurn)
            Debug.Log("White team wins");
        else
            Debug.Log("Black team wins");

        foreach (GameObject go in sf.activeChessman)
            Destroy(go);

        isWhiteTurn = true;
        BoardHighlights.Instance.HideHighlights();
        sf.SpawnAllChessmans(); 



    }

}
