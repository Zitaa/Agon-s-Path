using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellProjectile : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private int cost;

	#region UNITY FUNCTIONS
	
    protected virtual void Awake()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Spell"), LayerMask.NameToLayer("Player"));
    }

	protected virtual void Start ()
	{
        Destroy(this.gameObject, lifeTime);
	}
	
	protected virtual void Update () 
	{
        transform.position += transform.right * speed * Time.deltaTime;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            EntityAI ai = collision.gameObject.GetComponent<EntityAI>();
            if (ai != null) ai.DecreaseHealth(damage);
        }
        Destroy(this.gameObject);
    }

    #endregion

    #region PRIVATE FUNCTIONS



    #endregion

    #region PUBLIC FUNCTIONS

    public int GetDamage() { return damage; }

    public int GetCost() { return cost; }

    #endregion
}
