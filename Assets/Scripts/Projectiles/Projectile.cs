using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    /// <summary>
    /// Direction the projectile is flying.
    /// </summary>
    protected Vector2 direction;

    /// <summary>
    /// Speed of the projectile.
    /// </summary>
    public FloatData speedData;

    /// <summary>
    /// The time a projectile has in seconds before it is removed from the scene.
    /// </summary>
    public FloatData timeToLiveData;

    public void Move(Vector2 newDirection)
    {
        direction = newDirection;
        gameObject.GetComponent<Rigidbody2D>().velocity = direction * speedData.value;
        StartCoroutine(TimeToLiveEnds());
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        // since Tank is wrapped inside another GameObject, has to check parent
        Tank tank = collision.gameObject.GetComponent<Tank>();
        if (tank != null)
        {
            tank.GotHit();
            Remove();
        }
    }

    protected IEnumerator TimeToLiveEnds()
    {
        yield return new WaitForSeconds(timeToLiveData.value);
        Remove();
    }

    /// <summary>
    /// Remove this Projectile from the game.
    /// </summary>
    protected void Remove()
    {
        // return projectile to its pool
        gameObject.SetActive(false);
    }
}
