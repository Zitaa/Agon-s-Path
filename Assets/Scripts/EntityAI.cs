using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAI : OrderInLayerBehaviour
{
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
    private Color normalColor;
    private Vector2 movement;
    private Vector2 direction;
    protected int health;
    protected int mana;

	#region UNITY FUNCTIONS
	
	protected virtual new void Start ()
	{
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        game = GameBehaviour.instance.GetGame();
        input = game.GetInput();
        target = game.GetPlayer();
        normalColor = sr.color;

        game.IncreaseEntities();
        ID = game.GetEntites();

        if (!CompareTag("Player")) health = settings.GetHealth();
	}
	
	protected virtual new void Update () 
	{
        base.Update();
	}
	
    private IEnumerator MovementAIDelay(Vector2 movement)
    {
        yield return new WaitForSeconds(.2f);

        if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y)) movement.y = 0;
        else movement.x = 0;

        if (movement.x > 0) sr.flipX = false;
        else if (movement.x < 0) sr.flipX = true;

        rb2d.velocity = movement.normalized * settings.GetSpeed();
    }

    protected IEnumerator Attack()
    {
        yield return new WaitForSeconds(.15f);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, settings.GetMeleeRange(), targetMask);
        if (hit.collider != null)
        {
            Transform t = hit.collider.transform;
            EnemyEntity ai = t.GetComponent<EnemyEntity>();

            if (ai != null) ai.DecreaseHealth(settings.GetDamage());

            if (direction == (Vector2)transform.right) t.position = new Vector2(t.position.x + force, t.position.y);
            else if (direction == -(Vector2)transform.right) t.position = new Vector2(t.position.x - force, t.position.y);
            else if (direction == (Vector2)transform.up) t.position = new Vector2(t.position.x, t.position.y + force);
            else if (direction == -(Vector2)transform.up) t.position = new Vector2(t.position.x, t.position.y - force);
        }
    }

    protected IEnumerator DamageEffect()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(.1f);
        sr.color = normalColor;
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

        if (!game.GetCombatSystem().InCombat())
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
            else if (movement.y > 0) direction = transform.up;
            else if (movement.y < 0) direction = -transform.up;
        }
        else if (game.GetCombatSystem().InCombat())
        {
            Vector2 midPoint = game.GetCameraSettings().GetCentroid(game.GetCombatSystem().GetPositions().ToArray());
            if (transform.position.x > midPoint.x)
            {
                sr.flipX = true;
                if (movement.y == 0) direction = -transform.right;
            }
            else if (transform.position.x < midPoint.x)
            {
                sr.flipX = false;
                if (movement.y == 0) direction = transform.right;
            }

            if (movement.y > 0) direction = transform.up;
            else if (movement.y < 0) direction = -transform.up;
        }

        movement = new Vector2(x, y);
        rb2d.velocity = movement.normalized * settings.GetSpeed();
    }

    protected virtual void HandleAIMovement()
    {
        Vector2 targetMovement = target.position - transform.position;
        StartCoroutine(MovementAIDelay(targetMovement));
    }

    protected virtual void EnemyAttack()
    {
        float distance = Vector2.Distance(transform.position, target.position);
        if (distance < settings.GetMeleeRange())
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                anim.Play("Attack");
                float hitChance = Random.value;
                if (hitChance > .4f)
                {
                    PlayerEntity player = game.GetPlayer().GetComponent<PlayerEntity>();
                    player.DecreaseHealth(settings.GetDamage());
                }
            }
        }
    }

    protected virtual void KillEntity()
    {
        Destroy(this.gameObject);
    }

    #endregion

    #region PUBLIC FUNCTIONS

    public virtual void DecreaseHealth(int amount)
    {
        StartCoroutine(DamageEffect());
        health -= amount;
        print(string.Format("{0} took {1} points of damage. {2} / {3}", name, amount, health, settings.GetHealth()));
    }

    public int GetID() { return ID; }

    public int GetHealth() { return health; }

    public int GetMana() { return mana; }

    public EntitySettings GetSettings() { return settings; }

	#endregion
}
