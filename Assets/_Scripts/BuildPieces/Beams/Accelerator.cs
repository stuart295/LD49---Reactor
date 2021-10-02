using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerator : BeamEmitter
{

    public float forceAmount = 1f;

    protected override void OnBeamCollisionStart(GameObject particleGo) {
        Rigidbody2D rb = particleGo.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * forceAmount, ForceMode2D.Impulse);
    }
}
