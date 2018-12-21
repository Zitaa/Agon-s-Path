using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : GameObjectBehaviour {

    [SerializeField] private float speed;

    private Vector2 movement;
	
	#region UNITY FUNCTIONS
	
	private void Start ()
	{
        speed *= 250;
	}
	
	private void Update () 
	{
        UserInput();

        GetComponent<Rigidbody2D>().velocity = PhysicsMotor.Movement(movement.x, movement.y, speed);
	}
	
	#endregion
	
	#region PRIVATE FUNCTIONS
	
	private void UserInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        movement = new Vector2(x, y);
    }
	
	#endregion
	
	#region PUBLIC FUNCTIONS
	
	
	
	#endregion
}
