using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : Movement {
    
    public void move() {
        upMove();
        downMove();
        leftMove();
        rightMove();

    }
}