using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    /// <summary>
    /// Direction the projectile is flying.
    /// </summary>
    protected Vector2 direction;

    /// <summary>
    /// The GameObject's rigid body.
    /// </summary>
    protected Rigidbody2D rigidBody;

    /// <summary>
    /// Speed of the projectile.
    /// </summary>
    protected float speed;

    /// <summary>
    /// The time a projectile has in seconds before it is removed from the scene.
    /// </summary>
    protected float timeToLive;

    // Start is called before the first frame update
    void Awake()
    {
        // only set if there's nothing
        if (direction == null)
        {
            direction = new Vector2(0, 0);
        }
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        InitDetails();
        StartCoroutine(TimeToLiveEnds());
    }

    /// <summary>
    /// Set the speed and timer here.
    /// </summary>
    protected abstract void InitDetails();

    public void Move(Vector2 newDirection)
    {
        direction = newDirection;
        rigidBody.velocity = direction * speed;
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        Tank tank = collision.gameObject.GetComponent<Tank>();
        if (tank != null)
        {
            tank.GotHit();
            Remove();
        }
    }

    protected IEnumerator TimeToLiveEnds()
    {
        yield return new WaitForSeconds(timeToLive);
        Remove();
    }

    /// <summary>
    /// Remove this Projectile from the game.
    /// </summary>
    protected void Remove()
    {
        // so we don't have null pointer if we call remove multiple times
        if (gameObject)
        {
            Destroy(gameObject);
        }
    }
}
