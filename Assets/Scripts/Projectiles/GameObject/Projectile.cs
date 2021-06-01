using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    /// <summary>
    /// Speed of the projectile.
    /// </summary>
    public FloatDataSO speedData;

    /// <summary>
    /// The time a projectile has in seconds before it is removed from the scene.
    /// </summary>
    public FloatDataSO timeToLiveData;

    public void Fire(Vector2 newDirection)
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = newDirection * speedData.Value;
        StartCoroutine(TimeToLiveEnds());
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Tilemap") return;
        Tank tank = collision.gameObject.GetComponent<Tank>();
        if (tank != null)
        {
            tank.GotHit();
            Remove();
        }

        Projectile projectile = collision.gameObject.GetComponent<Projectile>();
        if (projectile != null)
        {
            projectile.Remove();
            Remove();
        }
    }

    protected IEnumerator TimeToLiveEnds()
    {
        yield return new WaitForSeconds(timeToLiveData.Value);
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
