using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Wrapper for a Tank Prefab.
/// Should act on the rigidBody property 
/// rather than the object transform for best result
/// </summary>
public class UIControlTank : Tank
{
    // ui
    public PressedDownObject forwardBtn;
    public PressedDownObject backwardBtn;
    public PressedDownObject leftRotateBtn;
    public PressedDownObject rightRotateBtn;
    public Button fireBtn;
    private Text fireBtnText;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        fireBtn.onClick.AddListener(Fire);
        fireBtnText = fireBtn.GetComponentInChildren<Text>();
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

    public override void MoveTankForward()
    {
        Vector2 distance = rigidBody.transform.right * movementSpeed * Time.fixedDeltaTime;
        rigidBody.MovePosition((Vector2)rigidBody.transform.position + distance);
    }

    public override void MoveTankBackward()
    {
        Vector2 distance = -rigidBody.transform.right * movementSpeed * Time.fixedDeltaTime;
        rigidBody.MovePosition((Vector2)rigidBody.transform.position + distance);
    }

    public override void RotateTurretClockwise()
    {
        rigidBody.transform.RotateAround(hullRenderer.bounds.center, Vector3.back, rotationSpeed);
    }

    public override void RotateTurretCounterClockwise()
    {
        rigidBody.transform.RotateAround(hullRenderer.bounds.center, Vector3.back, -rotationSpeed);
    }

    /// <summary>
    /// Fire the projectile.
    /// </summary>
    public override void Fire()
    {
        if (isReloading) return;

        GameObject shell = roundShellPool.GetPoolObj(
            barrelEntrance.transform.position, hull.transform.rotation);
        // recall that the direction the turret faces is the right side of the prefab
        shell.GetComponent<Projectile>().Move(rigidBody.transform.right.normalized);

        StartCoroutine(Reload());
    }

    protected override IEnumerator Reload()
    {
        isReloading = true;
        fireBtnText.text = "Reloading";
        yield return new WaitForSeconds(reloadTime);
        isReloading = false;
        fireBtnText.text = "Fire";
    }
}
