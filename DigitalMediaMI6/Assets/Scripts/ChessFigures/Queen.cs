using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Movement {

    public void move () {
        rightMove();
        leftMove();    
        upMove();
        downMove();

        topLeftMove();
        topRightMove();
        downLeftMove();
        downRightMove();
    }

}