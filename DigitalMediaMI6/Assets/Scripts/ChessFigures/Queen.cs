﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : ChessPiece {

    public override bool[, ] PossibleMove () {
        bool[, ] r = new bool[8, 8];

        ChessPiece c;
        int i, j;

        //rechts
        i = X;
        while (true) {
            i++;
            if (i >= 8) {
                break;
            }

            c = spawner.ChessPieces[i, Y];
            if (c == null) {
                r[i, Y] = true;
            } else {
                if (c.isWhite != isWhite) {
                    r[i, Y] = true;
                }
                break;
            }

        }

        //links
        i = X;
        while (true) {
            i--;
            if (i < 0) {
                break;
            }

            c = spawner.ChessPieces[i, Y];
            if (c == null) {
                r[i, Y] = true;
            } else {
                if (c.isWhite != isWhite) {
                    r[i, Y] = true;
                }
                break;
            }

        }

        //hoch
        i = Y;
        while (true) {
            i++;
            if (i >= 8) {
                break;
            }

            c = spawner.ChessPieces[X, i];
            if (c == null) {
                r[X, i] = true;
            } else {
                if (c.isWhite != isWhite) {
                    r[X, i] = true;
                }
                break;
            }

        }

        //runter
        i = Y;
        while (true) {
            i--;
            if (i < 0) {
                break;
            }

            c = spawner.ChessPieces[X, i];
            if (c == null) {
                r[X, i] = true;
            } else {
                if (c.isWhite != isWhite) {
                    r[X, i] = true;
                }
                break;
            }

        }

        //Top Left
        i = X;
        j = Y;
        while (true) {
            i--;
            j++;
            if (i < 0 || j >= 8)
                break;

            c = spawner.ChessPieces[i, j];
            if (c == null)
                r[i, j] = true;

            else {
                if (isWhite != c.isWhite)
                    r[i, j] = true;

                break;
            }
        }

        //Top Right
        i = X;
        j = Y;
        while (true) {
            i++;
            j++;
            if (i >= 8 || j >= 8)
                break;

            c = spawner.ChessPieces[i, j];
            if (c == null)
                r[i, j] = true;

            else {
                if (isWhite != c.isWhite)
                    r[i, j] = true;

                break;
            }
        }

        //Down Left
        i = X;
        j = Y;
        while (true) {
            i--;
            j--;
            if (i < 0 || j < 0)
                break;

            c = spawner.ChessPieces[i, j];
            if (c == null)
                r[i, j] = true;

            else {
                if (isWhite != c.isWhite)
                    r[i, j] = true;

                break;
            }
        }

        //Down Right
        i = X;
        j = Y;
        while (true) {
            i++;
            j--;
            if (i >= 8 || j < 0)
                break;

            c = spawner.ChessPieces[i, j];
            if (c == null)
                r[i, j] = true;

            else {
                if (isWhite != c.isWhite)
                    r[i, j] = true;

                break;
            }
        }

        return r;
    }
}