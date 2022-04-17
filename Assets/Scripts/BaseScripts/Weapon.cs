using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public abstract IEnumerator Attack();
    public abstract bool GetAttackState();
}
