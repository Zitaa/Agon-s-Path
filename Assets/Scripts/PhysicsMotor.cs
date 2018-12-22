using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsMotor : SingletonPattern
{
	#region PRIVATE FUNCTIONS
	
	
	
	#endregion
	
	#region PUBLIC FUNCTIONS
	
	public static Vector2 Movement(float x, float y, float speed)
    {
        if (Mathf.Abs(x) > Mathf.Abs(y)) y = 0;
        else x = 0;

        return new Vector2(x, y).normalized * speed * Time.deltaTime;
    }

    public static Vector2 Movement(Vector2 movement, float speed)
    {
        return Movement(movement.x, movement.y, speed);
    }
	
	#endregion
}
