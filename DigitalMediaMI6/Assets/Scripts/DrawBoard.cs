using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawBoard
{
    SelectionManager selection = new SelectionManager();

    public void UpdateDrawBoard()
    {
        selection.UpdateSelection();
        DrawChessboard();
        DrawSelection(selection.GetSelectionX(), selection.GetSelectionY());
//        Debug.Log("UpdateDrawBoard: " + selection.GetSelectionX() + ", " + selection.GetSelectionY());
    }

    private void DrawChessboard()
    {
        Vector3 widthLine = Vector3.right * 8;
        Vector3 heigthLine = Vector3.forward * 8;

        for (int i = 0; i <= 8; i++)
        {
            Vector3 start = Vector3.forward * i;
            Debug.DrawLine(start, start + widthLine, Color.green);
            for (int j = 0; j <= 8; j++)
            {
                start = Vector3.right * j;
                Debug.DrawLine(start, start + heigthLine, Color.green);

            }
        }

    }

    private void DrawSelection(int x, int y)
    {
        if (x >= 0 && y >= 0)
        {
//            Debug.Log("DrawSelection: " + x + ", " + y);
            Debug.DrawLine(
                Vector3.forward * y + Vector3.right * x,
                Vector3.forward * (y + 1) + Vector3.right * (x + 1), Color.red);

            Debug.DrawLine(
                Vector3.forward * (y + 1) + Vector3.right * x,
                Vector3.forward * y + Vector3.right * (x + 1), Color.red);
        }
    }
}
