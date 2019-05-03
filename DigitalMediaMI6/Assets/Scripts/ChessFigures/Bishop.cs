using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Movement {
    public void move () {

        topLeftMove ();
        topRightMove ();
        downLeftMove ();
        downRightMove ();
    }
}