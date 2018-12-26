using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton
{
    private static readonly Singleton instance = new Singleton();
    public static Singleton Instance { get { return instance; } }

    public GameManager GetGame() { return GameBehaviour.instance.GetGame(); }
}
