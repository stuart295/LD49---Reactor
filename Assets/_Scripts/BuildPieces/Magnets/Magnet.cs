using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Magnet : Placable
{

    public float strengthModifier = 1f;

    private bool active = true;

    protected bool Active { get => active; set => active = value; }


 

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    protected float GetMagnetForce() {
        return gm.magnetStrength * strengthModifier;
    }

    protected Vector3 GetRadialForce(Vector3 particlePos, Vector3 magnetPos) {
        Vector3 dispVec = particlePos - magnetPos;
        Vector3 dir = dispVec.normalized;
        float magnitude = GetMagnetForce() / Mathf.Pow(dispVec.magnitude * gm.distanceModifier, 2);
        return dir * magnitude;
    }


    public abstract Vector3 GetForceVector(Vector3 particlePos);

}
