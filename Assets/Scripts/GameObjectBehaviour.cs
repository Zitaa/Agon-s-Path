using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectBehaviour : MonoBehaviour {

	#region UNITY FUNCTIONS
	
	private void Start ()
	{

	}
	
	private void Update () 
	{

    }
	
	#endregion
	
	#region PRIVATE FUNCTIONS
	
	
	
	#endregion
	
	#region PUBLIC FUNCTIONS
    
	public GameManager GetGame() { return GameBehaviour.instance.GetGame(); }
	
	#endregion
}
