using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyCollector : BeamEmitter
{
    

    private EnergyManager energy;

    protected override void Awake() {
        base.Awake();
        energy = GameObject.FindGameObjectWithTag("GameController").GetComponent<EnergyManager>();
    }


    protected override void OnBeamCollisionStart(GameObject particleGo) {
        Rigidbody2D rb = particleGo.GetComponent<Rigidbody2D>();
        energy.AddEnergyFromVelocity(rb.velocity);
    }
}
