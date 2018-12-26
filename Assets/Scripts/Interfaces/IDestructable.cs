using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal interface IDestructable
{
    int Health { get; }

    void DecreaseHealth(int amount);
}
