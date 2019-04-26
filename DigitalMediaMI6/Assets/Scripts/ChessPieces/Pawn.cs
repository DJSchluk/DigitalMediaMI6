using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : ChessPiece
{
  

    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];

        

        ChessPiece c, c2;

        //weiß
       if (isWhite)
        {
            //diagonal links
            if (CurrentX != 0 && CurrentY != 7)
            {
                c = spawner.ChessPieces[CurrentX - 1, CurrentY + 1];
                if (c != null && !c.isWhite)
                {
                    r[CurrentX - 1, CurrentY + 1] = true;
                }
            }
            //diagonal rechts
            if (CurrentX != 7 && CurrentY != 7)
            {
                c = spawner.ChessPieces[CurrentX + 1, CurrentY + 1];
                if (c != null && !c.isWhite)
                {
                    r[CurrentX + 1, CurrentY + 1] = true;
                }
            }
            //gerade
            if (CurrentY != 7)
            {
                c = spawner.ChessPieces[CurrentX, CurrentY + 1];
                if (c == null)
                {
                    r[CurrentX, CurrentY + 1] = true;
                }
            }
            //gerade erster Zug
            if (CurrentY == 1)
            {
                c = spawner.ChessPieces[CurrentX, CurrentY + 1];
                c2 = spawner.ChessPieces[CurrentX, CurrentY + 2];
                if (c == null && c2 == null)
                {
                    r[CurrentX, CurrentY + 2] = true;
                }
            }
            //en passant
        }
        else
        {
            //diagonal links
            if (CurrentX != 0 && CurrentY != 0)
            {
               c = spawner.ChessPieces[CurrentX - 1, CurrentY - 1];
                if (c != null && c.isWhite)
                {
                    r[CurrentX - 1, CurrentY - 1] = true;
                }
            }
            //diagonal rechts
            if (CurrentX != 7 && CurrentY != 0)
            {
                c = spawner.ChessPieces[CurrentX + 1, CurrentY - 1];
                if (c != null && c.isWhite)
                {
                    r[CurrentX + 1, CurrentY + 1] = true;
                }
            }
            //gerade
            if (CurrentY != 0)
            {
                c = spawner.ChessPieces[CurrentX, CurrentY -1];
                if (c == null)
                {
                    r[CurrentX, CurrentY - 1] = true;
                }
            }
            //gerade erster Zug
            if (CurrentY == 6)
            {
                c = spawner.ChessPieces[CurrentX, CurrentY - 1];
                c2 = spawner.ChessPieces[CurrentX, CurrentY - 2];
                if (c == null && c2 == null)
                {
                    r[CurrentX, CurrentY - 2] = true;
                }
            }

        }
        return r;
    }

}
