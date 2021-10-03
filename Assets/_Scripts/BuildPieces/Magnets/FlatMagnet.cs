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

        //if (gm.debugMode)
        //    Debug.DrawLine(particlePos, closestPoint, Color.red);

        return GetRadialForce(particlePos, closestPoint);
    }

    //public override Vector3 GetForceVector(Vector3 particlePos) {
    //    Vector3 closestPoint = col.ClosestPoint(particlePos);

    //    Vector3 dispVec = (closestPoint - particlePos);

    //    RaycastHit2D[] hits = Physics2D.RaycastAll(particlePos, dispVec.normalized, dispVec.magnitude + 1f);


    //    foreach (RaycastHit2D hit in hits) {
    //        if (hit.collider.gameObject == gameObject) {

    //            if (gm.debugMode) {
    //                Debug.DrawLine(particlePos, closestPoint, Color.red);
    //                Debug.DrawLine(closestPoint, closestPoint + new Vector3(hit.normal.x, hit.normal.y), Color.green);
    //            }

    //            float dotProduct = Vector3.Dot(-dispVec, hit.normal);

    //            return GetRadialForce(particlePos, closestPoint) * dotProduct;



    //        }
    //    }

    //    return Vector3.zero;

    //}
}
