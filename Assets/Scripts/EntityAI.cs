using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAI : OrderInLayerBehaviour, IDestructable {

    [SerializeField] protected EntitySettings settings;
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private float force;
    [SerializeField] private int ID;

    protected Rigidbody2D rb2d;
    private SpriteRenderer sr;
    protected Animator anim;
    protected GameManager game;
    protected InputManager input;
    protected Transform target;
    private Vector2 movement;
    private Vector2 direction;

    public int Health { get; private set; }

	#region UNITY FUNCTIONS
	
	protected virtual new void Start ()
	{
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        game = GameBehaviour.instance.GetGame();
        input = game.GetInput();
        target = game.GetPlayer();

        game.IncreaseEntities();
        ID = game.GetEntites();

        Health = settings.GetHealth();
	}
	
	protected virtual new void Update () 
	{
        base.Update();
	}
	
    private IEnumerator MovementAIDelay(Vector2 movement)
    {
        yield return new WaitForSeconds(.2f);

        if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y)) movement.y = 0;
        else movement .x = 0;

        rb2d.velocity = movement.normalized * settings.GetSpeed() * Time.deltaTime;
    }

    protected IEnumerator Attack()
    {
        yield return new WaitForSeconds(.15f);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, settings.GetMeleeRange(), targetMask);
        if (hit.collider != null)
        {
            Transform t = hit.collider.transform;
            EntityAI ai = t.GetComponent<EntityAI>();

            if (ai != null) ai.DecreaseHealth(settings.GetDamage());

            if (direction == (Vector2)transform.right) t.position = new Vector2(t.position.x + force, t.position.y);
            else if (direction == -(Vector2)transform.right) t.position = new Vector2(t.position.x - force, t.position.y);
            else if (direction == (Vector2)transform.up) t.position = new Vector2(t.position.x, t.position.y + force);
            else if (direction == -(Vector2)transform.up) t.position = new Vector2(t.position.x, t.position.y - force);
        }
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

        if (game.GetGameState() != GameManager.GameStates.COMBAT)
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
            Vector2 midPoint = game.GetCameraSettings().GetCentroid(game.GetCombatSystem().GetPositions().ToArray());
            if (transform.position.x > midPoint.x)
            {
                sr.flipX = true;
                direction = -transform.right;
            }
            else if (transform.position.x < midPoint.x)
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
        print(string.Format("{0} took {1} points of damage. {2} / {3}", name, amount, Health, settings.GetHealth()));
        if (Health <= 0) KillEntity();
    }

    public void KillEntity()
    {
        game.GetCombatSystem().RemoveEntity(this);
        Destroy(this.gameObject);
    }

    public int GetID() { return ID; }

	#endregion
}
