using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Movement {
    public void move () {
        topSideMove ();
        downSideMove ();
        middleLeftMove ();
        middleRightMove ();
    }

}