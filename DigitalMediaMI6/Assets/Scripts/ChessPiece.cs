﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChessPiece : MonoBehaviour {

    public int CurrentX { set; get; }
    public int CurrentY { set; get; }

    public bool isWhite;


    public void setPosition(int x, int y)
    {
        CurrentX = x;
        CurrentY = y;

    }

    public virtual bool[,] PossibleMove()
    {
       
        return new bool[8,8];
    }
}

