using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

/// <summary>
/// Should act on the rigidBody property 
/// rather than the object transform for best result
/// </summary>
public class Tank : NetworkBehaviour
{    // tank parts
    public GameObject hull;
    private Renderer hullRenderer;
    public GameObject barrelEntrance;
    public Rigidbody2D rigidBody;

    // stats
    public FloatDataSO movementSpeed;
    public FloatDataSO rotationSpeed;
    public FloatDataSO reloadTime;

    // shell
    public RoundShellPool roundShellPool;

    // states
    private bool isReloading;

    // ui objects
    private PressedDownObject forwardBtn;
    private PressedDownObject backwardBtn;
    private PressedDownObject leftRotateBtn;
    private PressedDownObject rightRotateBtn;
    private Button fireBtn;
    private Text fireBtnText;

    // Start is called before the first frame update
    void Awake()
    {
        hullRenderer = hull.GetComponent<Renderer>();
    }

    /// <summary>
    /// Called when the local player object has been set up.
    /// <para>This happens after OnStartClient(), as it is triggered 
    /// by an ownership message from the server. This is an appropriate place 
    /// to activate components or functionality that should only be active 
    /// for the local player, such as cameras and input.</para>
    /// </summary>
    public override void OnStartLocalPlayer()
    {
        forwardBtn = GameObject.Find("ForwardBtn").GetComponent<PressedDownObject>();
        backwardBtn = GameObject.Find("BackwardBtn").GetComponent<PressedDownObject>();
        leftRotateBtn = GameObject.Find("LeftRotateBtn").GetComponent<PressedDownObject>();
        rightRotateBtn = GameObject.Find("RightRotateBtn").GetComponent<PressedDownObject>();
        fireBtn = GameObject.Find("FireBtn").GetComponent<Button>();
        fireBtn.onClick.AddListener(Fire);
        fireBtnText = fireBtn.GetComponentInChildren<Text>();
    }

    private void FixedUpdate()
    {
        if (!isLocalPlayer) return;

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

        else if (rightRotateBtn.IsPressedDown)
        {
            RotateTurretClockwise();
        }

    }

    public void MoveTankForward()
    {
        Vector2 distance = rigidBody.transform.right * movementSpeed.Value * Time.fixedDeltaTime;
        rigidBody.MovePosition((Vector2)rigidBody.transform.position + distance);
    }

    public void MoveTankBackward()
    {
        Vector2 distance = -rigidBody.transform.right * movementSpeed.Value * Time.fixedDeltaTime;
        rigidBody.MovePosition((Vector2)rigidBody.transform.position + distance);
    }

    public void RotateTurretClockwise()
    {
        rigidBody.transform.RotateAround(hullRenderer.bounds.center, Vector3.back, rotationSpeed.Value);
    }

    public void RotateTurretCounterClockwise()
    {
        rigidBody.transform.RotateAround(hullRenderer.bounds.center, Vector3.back, -rotationSpeed.Value);
    }

    /// <summary>
    /// Fire the projectile.
    /// </summary>
    public void Fire()
    {
        if (isReloading) return;

        GameObject shell = roundShellPool.GetPoolObj(
            barrelEntrance.transform.position, hull.transform.rotation);
        // recall that the direction the turret faces is the right side of the prefab
        shell.GetComponent<Projectile>().Fire(rigidBody.transform.right.normalized);

        StartCoroutine(Reload());
    }

    protected IEnumerator Reload()
    {
        isReloading = true;
        fireBtnText.text = "Reloading";
        yield return new WaitForSeconds(reloadTime.Value);
        isReloading = false;
        fireBtnText.text = "Fire";
    }


    /// <summary>
    /// Let the tank knows that it has been hit.
    /// </summary>
    public void GotHit()
    {
        // later on will play destruction anim
        Destroy(gameObject);
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log($"{name} touches {collision.gameObject}");
    //}
}
