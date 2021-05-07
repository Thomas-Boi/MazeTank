using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITank : Tank
{
    public override void MoveTankBackward()
    {
        Debug.Log("move");
    }

    public override void MoveTankForward()
    {
        Debug.Log("move");
    }

    public override void RotateTurretClockwise()
    {
        Debug.Log("move");
    }

    public override void RotateTurretCounterClockwise()
    {
        Debug.Log("move");
    }

    public override void Fire()
    {
        Debug.Log("fire");
    }

    protected override IEnumerator Reload()
    {
        Debug.Log("reload");
        yield return new WaitForSeconds(1);
    }
}
