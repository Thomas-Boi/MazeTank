using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tank : MonoBehaviour
{
    // tank parts
    public GameObject hull;
    private Renderer hullRenderer;
    public GameObject barrelEntrance;
    private Rigidbody2D rb;

    // stats
    public const int movementSpeed = 5;
    public const int rotationSpeed = 2;
    private const int reloadTime = 3; 

    // shell
    public GameObject roundShell;

    // ui
    public PressedDownObject forwardBtn;
    public PressedDownObject backwardBtn;
    public PressedDownObject leftRotateBtn;
    public PressedDownObject rightRotateBtn;
    public Button fireBtn;

    // states
    private bool isReloading;

    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hullRenderer = hull.GetComponent<Renderer>();
        fireBtn.onClick.AddListener(Fire);
    }

    private void FixedUpdate()
    {
        if (forwardBtn.IsPressedDown)
        {
            MoveTankForward();
        }

        else if (backwardBtn.IsPressedDown)
        {
            MoveTankBackward();
        }

        if (leftRotateBtn.IsPressedDown)
        {
            RotateTurretCounterClockwise();
        }

        if (rightRotateBtn.IsPressedDown)
        {
            RotateTurretClockwise();
        }

    }

    public void MoveTankForward()
    {
        Vector2 distance = transform.right * movementSpeed * Time.fixedDeltaTime;
        rb.MovePosition((Vector2) transform.position + distance);
    }

    public void MoveTankBackward()
    {
        Vector2 distance = -transform.right * movementSpeed * Time.fixedDeltaTime;
        rb.MovePosition((Vector2)transform.position + distance);
    }

    void RotateTurretClockwise()
    {
        transform.RotateAround(hullRenderer.bounds.center, Vector3.back, rotationSpeed);
    }

    void RotateTurretCounterClockwise()
    {
        transform.RotateAround(hullRenderer.bounds.center, Vector3.back, -rotationSpeed);
    }

    /// <summary>
    /// Fire the projectile.
    /// </summary>
    public void Fire()
    {
        if (isReloading) return;

        GameObject shell = Instantiate(roundShell, barrelEntrance.transform.position, hull.transform.rotation);
        // recall that the direction the turret faces is the right side of the prefab
        shell.GetComponent<Projectile>().Move(transform.right.normalized);

        StartCoroutine(Reload());
    }

    /// <summary>
    /// Let the tank knows that it has been hit.
    /// </summary>
    public void GotHit()
    {
        Destroy(gameObject);
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        isReloading = false;
    }
}
