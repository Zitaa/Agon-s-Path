using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : EntityAI
{
    [SerializeField] private GameObject loot;

    private bool inCombat = false;
    private bool isDead = false;

	#region UNITY FUNCTIONS
	
	protected override void Start ()
	{
        base.Start();
        this.name = settings.name;
	}
	
	protected override void Update () 
	{
        base.Update();
        float distance = Vector2.Distance(transform.position, target.position);
        if (distance <= settings.GetViewRange())
        {
            EnterCombat();
            base.HandleAIMovement();
            base.EnemyAttack();
        }
        else ExitCombat();
	}
	
	#endregion
	
	#region PRIVATE FUNCTIONS
	
	private void EnterCombat()
    {
        if (!inCombat) game.GetCombatSystem().EnterCombat(this);
        inCombat = true;
    }

    private void ExitCombat()
    {
        if (inCombat) game.GetCombatSystem().RemoveEntity(this);
        inCombat = false;
        rb2d.velocity = Vector2.zero;
    }

    protected override void KillEntity()
    {
        isDead = true;
        game.GetCombatSystem().RemoveEntity(this);
        Instantiate(loot, transform.position, Quaternion.identity);
        base.KillEntity();
    }

    #endregion

    #region PUBLIC FUNCTIONS

    public override void DecreaseHealth(int amount)
    {
        base.DecreaseHealth(amount);
        if (health <= 0 && !isDead) KillEntity();
    }

    #endregion
}
