using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : GameObjectBehaviour {

    [SerializeField] private float speed;

    private SpriteRenderer sr;
    private Rigidbody2D rb2d;
    private Animator anim;

    private Vector2 movement;
	
	#region UNITY FUNCTIONS
	
	private void Start ()
	{
        speed *= 250;

        sr = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
	}
	
	private void Update () 
	{
        UserInput();
        HandleMovement();
	}

    private IEnumerator Attack()
    {
        anim.SetTrigger("attack");
        yield return new WaitForSeconds(.002f);
        anim.ResetTrigger("attack");
    }
	
	#endregion
	
	#region PRIVATE FUNCTIONS
	
	private void UserInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        movement = new Vector2(x, y);

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            StartCoroutine(Attack());
        }
    }

    private void HandleMovement()
    {
        rb2d.velocity = PhysicsMotor.Movement(movement, speed);

        if (movement != Vector2.zero) anim.SetBool("walking", true);
        else anim.SetBool("walking", false);

        if (movement.x < 0) sr.flipX = true;
        else if (movement.x > 0) sr.flipX = false;
    }
	
	#endregion
	
	#region PUBLIC FUNCTIONS
	
	
	
	#endregion
}
