using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAI : GameObjectBehaviour
{
    [SerializeField] protected EntitySettings settings;
    [SerializeField] protected uint ID;

    private SpriteRenderer sr;
    private Rigidbody2D rb2d;
    private Animator anim;
    private InputManager input;
    private Vector2 movement;
    private Vector2 direction;
    private Color normalColor;
    private bool up, right, down, left;
    private float difference;

    #region UNITY FUNCTIONS

    private void Awake()
    {
        settings.Init(this);
    }

    protected virtual void Start()
    {
        GetGame().IncreaseEntities();
        ID = GetGame().GetEntities();

        sr = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        input = GetGame().GetInputSettings();

        normalColor = sr.color;
    }

    protected virtual void Update()
    {
        
    }

    protected virtual IEnumerator Attack()
    {
        anim.Play("Attack");
        yield return new WaitForSeconds(.15f);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, settings.meleeRange.GetValue(), settings.targetMask);
        if (hit.collider != null)
        {
            Transform t = hit.collider.GetComponent<Transform>();
            EntityAI ai = t.GetComponent<EntityAI>();

            if (ai != null)
            {
                ai.DamageEffect(direction, settings.force.GetValue());
                ai.settings.DecreaseHealth(settings.damage.GetValue());
            }
        }
    }

    protected virtual IEnumerator MovementAIDelay(Vector2 targetMovement)
    {
        yield return new WaitForSeconds(.2f);
        rb2d.velocity = PhysicsMotor.Movement(targetMovement, settings.speed * EntitySettings.SPEED_MULTIPLIER);
    }

    protected virtual IEnumerator DamageColour()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(.1f);
        if (sr != null) sr.color = normalColor;
    }

    #endregion

    #region PRIVATE FUNCTIONS

    protected virtual void HandlePlayerMovement()
    {
        rb2d.velocity = PhysicsMotor.Movement(movement, settings.speed * EntitySettings.SPEED_MULTIPLIER);

        if (movement != Vector2.zero) anim.SetBool("walking", true);
        else anim.SetBool("walking", false);

        if (GetGame().GetMisc().GetState() == Game.GameStates.COMBAT)
        {
            Vector2 entityPos = GetGame().GetCombatSystem().GetEntityTransform().position;
            if (entityPos.x < transform.position.x)
            {
                sr.flipX = true;
                direction = -transform.right;
            }
            else if (entityPos.x > transform.position.x)
            {
                sr.flipX = false;
                direction = transform.right;
            }
        }
        else
        {
            if (movement.x < 0)
            {
                sr.flipX = true;
                direction = -transform.right;
            }
            else if (movement.x > 0)
            {
                sr.flipX = false;
                direction = transform.right;
            }
        }

        if (movement.y < 0) direction = -transform.up;
        else if (movement.y > 0) direction = transform.up;
    }

    protected virtual void HandleAIMovement(Transform entity, Transform target)
    {
        Vector2 targetMovement = target.position - transform.position;
        StartCoroutine(MovementAIDelay(targetMovement));
    }

    #endregion

    #region PUBLIC FUNCTIONS

    public void DamageEffect(Vector2 dir, float force)
    {
        Transform t = GetComponent<Transform>();
        StartCoroutine(DamageColour());

        if (dir == Vector2.right) t.position = new Vector2(t.position.x + force, t.position.y);
        else if (dir == -Vector2.right) t.position = new Vector2(t.position.x - force, t.position.y);
        else if (dir == Vector2.up) t.position = new Vector2(t.position.x, t.position.y + force);
        else if (dir == -Vector2.up) t.position = new Vector2(t.position.x, t.position.y - force);
    }

    public EntitySettings GetEntitySettings()
    {
        return settings;
    }

    #endregion

    #region USERINPUT 

    protected virtual void UserInput()
    {
        MovementInput();
        MeleeInput();
        SpellInput();
    }

    private void MovementInput()
    {
        float x = input.GetMovementInput("Horizontal");
        float y = input.GetMovementInput("Vertical");

        movement = new Vector2(x, y);
    }

    private void MeleeInput()
    {
        if (input.GetKeyUp(input.meleeAttack))
        {
            StartCoroutine(Attack());
        }
    }

    private void SpellInput()
    {
        if (input.GetKeyUp(input.spellActivator))
        {
            if (!GetGame().GetSpellSystem().IsActive()) GetGame().GetSpellSystem().Start();
            else if (GetGame().GetSpellSystem().IsActive()) GetGame().GetSpellSystem().End();
        }
        if (GetGame().GetSpellSystem().IsActive())
        {
            if (input.GetKeyUp(input.spellAttack))
            {
                Vector2 spawnPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                GameObject clone = Instantiate(GetGame().GetSpellSystem().GetSpellIcon(), spawnPos, Quaternion.identity);
                GetGame().GetSpellSystem().AddSpellIcon(clone);
            }
        }
    }

    #endregion
}
