using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : ChessPiece {
    public override bool[,] PossibleMove(){
        bool[,] r = new bool[8,8];

        ChessPiece c;
        int i;

        //rechts
        i = CurrentX;
        while(true){
            i++;
            if(i>=8){
                break;
            }

            c = BoardManager.Instance.ChessPieces[i, CurrentY];
            if(c==null){
                r[i, CurrentY] = true;
            }
            else {
                if(c.isWhite != isWhite){
                    r[i,CurrentY] = true;
                }
                break;
            }
            
        }

        //links
        i = CurrentX;
        while(true){
            i--;
            if(i<0){
                break;
            }

            c = BoardManager.Instance.ChessPieces[i, CurrentY];
            if(c==null){
                r[i, CurrentY] = true;
            }
            else {
                if(c.isWhite != isWhite){
                    r[i,CurrentY] = true;
                }
                break;
            }
            
        }

        //hoch
        i = CurrentY;
        while(true){
            i++;
            if(i>=8){
                break;
            }

            c = BoardManager.Instance.ChessPieces[CurrentX, i];
            if(c==null){
                r[CurrentX,i] = true;
            }
            else {
                if(c.isWhite != isWhite){
                    r[CurrentX,i] = true;
                }
                break;
            }
            
        }

        //hoch
        i = CurrentY;
        while(true){
            i--;
            if(i<0){
                break;
            }

            c = BoardManager.Instance.ChessPieces[CurrentX, i];
            if(c==null){
                r[CurrentX,i] = true;
            }
            else {
                if(c.isWhite != isWhite){
                    r[CurrentX,i] = true;
                }
                break;
            }
            
        }

        return r;

    }
}
