using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerator2 : Magnet
{

    private Collider2D col;

    protected override void Awake() {
        base.Awake();
        col = GetComponent<Collider2D>();
    }

    //public override Vector3 GetForceVector(Vector3 particlePos) {
    //    Vector3 closestPoint = col.ClosestPoint(particlePos);

    //    //if (gm.debugMode)
    //    //    Debug.DrawLine(particlePos, closestPoint, Color.red);

    //    return GetRadialForce(particlePos, closestPoint);
    //}

    public override Vector3 GetForceVector(Vector3 particlePos) {
        Vector3 closestPoint = col.ClosestPoint(particlePos);

        Vector3 dispVec = (closestPoint - particlePos);

        RaycastHit2D[] hits = Physics2D.RaycastAll(particlePos, dispVec.normalized, dispVec.magnitude + 1f);


        foreach (RaycastHit2D hit in hits) {
            if (hit.collider.gameObject == gameObject) {

                Vector3 perpDir = Vector3.Cross(hit.normal, Vector3.forward).normalized;

                return GetRadialForce(particlePos, closestPoint).magnitude * perpDir;



            }
        }

        return Vector3.zero;

    }
}
