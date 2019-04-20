using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawBoard
{

    private int selectionX = -1;
    private int selectionY = -1;

    public void UpdateDrawBoard()
    {
        DrawChessboard();
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

        //Auswahl darstellen
        if (selectionX >= 0 && selectionY >= 0)
        {
            Debug.DrawLine(
                Vector3.forward * selectionY + Vector3.right * selectionX,
                Vector3.forward * (selectionY + 1) + Vector3.right * (selectionX + 1), Color.red);

            Debug.DrawLine(
                Vector3.forward * (selectionY + 1) + Vector3.right * selectionX,
                Vector3.forward * selectionY + Vector3.right * (selectionX + 1), Color.red);
        }
        //Debug.Log("x = " + selectionX + ", y = " + selectionY);
    }
    public void SetSelectionX(int selectionX)
    {
        this.selectionX = selectionX;
    }


    public void SetSelectionY(int selectionY)
    {
        this.selectionY = selectionY;
    }
}
