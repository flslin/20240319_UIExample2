using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Countable, Useable
{
    public bool Use()
    {
        // 포션의 개수가 충분할 경우 개수 1감소
        return true;
    }
}
