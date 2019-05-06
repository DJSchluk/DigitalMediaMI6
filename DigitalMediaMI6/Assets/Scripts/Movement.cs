using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : ChessPiece {
    bool[, ] r = new bool[8, 8];
    ChessPiece c, c2;
    int i, j;

    //Läufer
    public void topLeftMove () {
        i = CurrentX;
        j = CurrentY;
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

    }
    public void topRightMove () {
        i = CurrentX;
        j = CurrentY;
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
    }

    public void downLeftMove () {
        i = CurrentX;
        j = CurrentY;
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
    }

    public void downRightMove () {
        i = CurrentX;
        j = CurrentY;
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
    }

    //König
    public void topSideMove () {
        i = CurrentX - 1;
        j = CurrentY + 1;
        if (CurrentY != 7) {
            for (int k = 0; k < 3; k++) {
                if (i >= 0 && i < 8) {
                    c = spawner.ChessPieces[i, j];
                    if (c == null)
                        r[i, j] = true;
                    else if (isWhite != c.isWhite)
                        r[i, j] = true;
                }

                i++;
            }
        }
    }

    public void downSideMove () {
        i = CurrentX - 1;
        j = CurrentY - 1;
        if (CurrentY != 0) {
            for (int k = 0; k < 3; k++) {
                if (i >= 0 && i < 8) {
                    c = spawner.ChessPieces[i, j];
                    if (c == null)
                        r[i, j] = true;
                    else if (isWhite != c.isWhite)
                        r[i, j] = true;
                }

                i++;
            }
        }
    }

    public void middleLeftMove () {
        if (CurrentX != 0) {
            c = spawner.ChessPieces[CurrentX - 1, CurrentY];
            if (c == null)
                r[CurrentX - 1, CurrentY] = true;
            else if (isWhite != c.isWhite)
                r[CurrentX - 1, CurrentY] = true;

        }
    }

    public void middleRightMove () {
        if (CurrentX != 7) {
            c = spawner.ChessPieces[CurrentX + 1, CurrentY];
            if (c == null)
                r[CurrentX + 1, CurrentY] = true;
            else if (isWhite != c.isWhite)
                r[CurrentX + 1, CurrentY] = true;

        }
    }

    //Königin
    public void rightMove () {
        i = CurrentX;
        while (true) {
            i++;
            if (i >= 8) {
                break;
            }

            c = spawner.ChessPieces[i, CurrentY];
            if (c == null) {
                r[i, CurrentY] = true;
            } else {
                if (c.isWhite != isWhite) {
                    r[i, CurrentY] = true;
                }
                break;
            }

        }
    }

    public void leftMove () {
        i = CurrentX;
        while (true) {
            i--;
            if (i < 0) {
                break;
            }

            c = spawner.ChessPieces[i, CurrentY];
            if (c == null) {
                r[i, CurrentY] = true;
            } else {
                if (c.isWhite != isWhite) {
                    r[i, CurrentY] = true;
                }
                break;
            }

        }
    }

    public void upMove () {
        i = CurrentY;
        while (true) {
            i++;
            if (i >= 8) {
                break;
            }

            c = spawner.ChessPieces[CurrentX, i];
            if (c == null) {
                r[CurrentX, i] = true;
            } else {
                if (c.isWhite != isWhite) {
                    r[CurrentX, i] = true;
                }
                break;
            }

        }
    }

    public void downMove () {
        i = CurrentY;
        while (true) {
            i--;
            if (i < 0) {
                break;
            }

            c = spawner.ChessPieces[CurrentX, i];
            if (c == null) {
                r[CurrentX, i] = true;
            } else {
                if (c.isWhite != isWhite) {
                    r[CurrentX, i] = true;
                }
                break;
            }

        }
    }

    //Bauer
    public void tempBauerMovement () {
        if (isWhite) {
            //diagonal links
            if (CurrentX != 0 && CurrentY != 7) {
                c = spawner.ChessPieces[CurrentX - 1, CurrentY + 1];
                if (c != null && !c.isWhite) {
                    r[CurrentX - 1, CurrentY + 1] = true;
                }
            }
            //diagonal rechts
            if (CurrentX != 7 && CurrentY != 7) {
                c = spawner.ChessPieces[CurrentX + 1, CurrentY + 1];
                if (c != null && !c.isWhite) {
                    r[CurrentX + 1, CurrentY + 1] = true;
                }
            }
            //gerade
            if (CurrentY != 7) {
                c = spawner.ChessPieces[CurrentX, CurrentY + 1];
                if (c == null) {
                    r[CurrentX, CurrentY + 1] = true;
                }
            }
            //gerade erster Zug
            if (CurrentY == 1) {
                c = spawner.ChessPieces[CurrentX, CurrentY + 1];
                c2 = spawner.ChessPieces[CurrentX, CurrentY + 2];
                if (c == null && c2 == null) {
                    r[CurrentX, CurrentY + 2] = true;
                }
            }
            //en passant
        } else {
            //diagonal links
            if (CurrentX != 0 && CurrentY != 0) {
                c = spawner.ChessPieces[CurrentX - 1, CurrentY - 1];
                if (c != null && c.isWhite) {
                    r[CurrentX - 1, CurrentY - 1] = true;
                }
            }
            //diagonal rechts
            if (CurrentX != 7 && CurrentY != 0) {
                c = spawner.ChessPieces[CurrentX + 1, CurrentY - 1];
                if (c != null && c.isWhite) {
                    r[CurrentX + 1, CurrentY + 1] = true;
                }
            }
            //gerade
            if (CurrentY != 0) {
                c = spawner.ChessPieces[CurrentX, CurrentY - 1];
                if (c == null) {
                    r[CurrentX, CurrentY - 1] = true;
                }
            }
            //gerade erster Zug
            if (CurrentY == 6) {
                c = spawner.ChessPieces[CurrentX, CurrentY - 1];
                c2 = spawner.ChessPieces[CurrentX, CurrentY - 2];
                if (c == null && c2 == null) {
                    r[CurrentX, CurrentY - 2] = true;
                }
            }

        }

    }

    //Pferd TODO

}