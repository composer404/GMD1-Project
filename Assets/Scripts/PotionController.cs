using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionController : Collectable {

    [SerializeField]
    private int health = 10;

    public int GetHealth() {
        return health;
    }
}
