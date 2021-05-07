using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tank : MonoBehaviour
{
    // tank parts
    public GameObject hull;
    protected Renderer hullRenderer;
    public GameObject barrelEntrance;
    public Rigidbody2D rigidBody;

    // stats
    public const int movementSpeed = 5;
    public const int rotationSpeed = 2;
    public const int reloadTime = 3;

    // shell
    public RoundShellPool roundShellPool;

    // states
    protected bool isReloading;

    protected void Start()
    {
        hullRenderer = hull.GetComponent<Renderer>();
    }


    public abstract void MoveTankForward();

    public abstract void MoveTankBackward();

    public abstract void RotateTurretClockwise();

    public abstract void RotateTurretCounterClockwise();

    /// <summary>
    /// Fire the projectile.
    /// </summary>
    public abstract void Fire();

    /// <summary>
    /// Let the tank knows that it has been hit.
    /// </summary>
    public void GotHit()
    {
        Destroy(gameObject);
    }

    protected abstract IEnumerator Reload();
}
