using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellProjectile : MonoBehaviour {

    [SerializeField] private float lifeTime;
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private int cost;

    #region UNITY FUNCTIONS

    private void Awake()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Spell"), LayerMask.NameToLayer("Player"));
    }

    private void Start ()
	{
        Destroy(this.gameObject, lifeTime);
	}
	
	private void Update () 
	{
        transform.position += transform.right * speed * Time.deltaTime;
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            EntityAI ai = collision.gameObject.GetComponent<EntityAI>();
            if (ai != null)
            {
                ai.GetEntitySettings().DecreaseHealth(damage);

                Vector2 direction = new Vector2();
                Vector3 rot = transform.localEulerAngles;

                if (rot.z < 45 || rot.z > 315) direction = Vector2.right;
                else if (rot.z < 135 && rot.z > 45) direction = Vector2.up;
                else if (rot.z < 225 && rot.z > 135) direction = -Vector2.right;
                else if (rot.z < 315 && rot.z > 225) direction = -Vector2.up;

                ai.DamageEffect(direction, 1);
            }
            Destroy(this.gameObject);
        }
    }

    #endregion

    #region PRIVATE FUNCTIONS



    #endregion

    #region PUBLIC FUNCTIONS



    #endregion
}
