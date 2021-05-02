using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundShell : Projectile
{
    private const float SPEED = 5;
    private const float TIME_TO_LIVE_IN_SEC = 1;

    // Start is called before the first frame update
    protected override void InitDetails()
    {
        speed = SPEED;
        timeToLive = TIME_TO_LIVE_IN_SEC;
    }
}
