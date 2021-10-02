using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlatMagnet : Magnet
{

    private Collider2D col;

    protected override void Awake() {
        base.Awake();
        col = GetComponent<Collider2D>();
    }

    public override Vector3 GetForceVector(Vector3 particlePos) {
        Vector3 closestPoint = col.ClosestPoint(particlePos);

        if (gm.debugMode)
            Debug.DrawLine(particlePos, closestPoint, Color.red);

        return GetRadialForce(particlePos, closestPoint);
    }
}
