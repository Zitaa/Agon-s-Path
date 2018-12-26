using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAI : MonoBehaviour, IDestructable {

    [SerializeField] protected EntitySettings settings;

    private Rigidbody2D rb2d;
    private SpriteRenderer sr;
    private Animator anim;
    protected GameManager game;
    private InputManager input;
    protected Transform target;
    private Vector2 movement;
    private Vector2 direction;

    public int Health { get; private set; }

	#region UNITY FUNCTIONS
	
	protected virtual void Start ()
	{
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        game = GameBehaviour.instance.GetGame();
        input = game.GetInput();
        target = game.GetPlayer();
        
        Health = settings.GetHealth();
	}
	
	protected virtual void Update () 
	{

	}
	
    private IEnumerator MovementAIDelay(Vector2 movement)
    {
        yield return new WaitForSeconds(.2f);

        if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y)) movement.y = 0;
        else movement .x = 0;

        rb2d.velocity = movement.normalized * settings.GetSpeed() * Time.deltaTime;
    }

	#endregion
	
	#region PRIVATE FUNCTIONS
	
	protected virtual void HandlePlayerMovement()
    {
        float x = input.GetMovementInput("Horizontal");
        float y = input.GetMovementInput("Vertical");

        if (Mathf.Abs(x) > Mathf.Abs(y)) y = 0;
        else x = 0;

        if (x != 0 || y != 0) anim.SetBool("walking", true);
        else anim.SetBool("walking", false);

        if (game.GetGameState().Equals(GameManager.GameStates.IDLE))
        {
            if (movement.x > 0)
            {
                sr.flipX = false;
                direction = transform.right;
            }
            else if (movement.x < 0)
            {
                sr.flipX = true;
                direction = -transform.right;
            }
        }
        else if (game.GetGameState().Equals(GameManager.GameStates.COMBAT))
        {
            Vector2 cameraPos = game.GetCameraSettings().GetCamera().transform.position;
            if (transform.position.x > cameraPos.x)
            {
                sr.flipX = true;
                direction = -transform.right;
            }
            else if (transform.position.x < cameraPos.x)
            {
                sr.flipX = false;
                direction = transform.right;
            }
        }

        movement = new Vector2(x, y);
        rb2d.velocity = movement.normalized * settings.GetSpeed() * Time.deltaTime;
    }

    protected virtual void HandleAIMovement()
    {
        Vector2 targetMovement = target.position - transform.position;
        StartCoroutine(MovementAIDelay(targetMovement));
    }
	
	#endregion
	
	#region PUBLIC FUNCTIONS
	
	public void DecreaseHealth(int amount)
    {
        Health -= amount;
        if (Health <= 0) KillEntity();
    }

    public void KillEntity()
    {
        game.GetCombatSystem().RemoveEntity(this);
        Destroy(this);
    }
	
	#endregion
}
