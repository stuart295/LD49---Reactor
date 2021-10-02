using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placable : MonoBehaviour
{

    public bool destroyable = true;

    protected Vector3 initialPos;
    protected Quaternion initialRot;

    protected GameManager gm;

    protected virtual void Awake() {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }


    public virtual void OnPlaced() {
        initialPos = transform.position;
        initialRot = transform.rotation;
        gm.RegisterPlacable(this);
    }

    public void OnRemoved() {
        gm.DeregisterPlacable(this);
    }


    public virtual void ResetState() {
        transform.position = initialPos;
        transform.rotation = initialRot;
    }


}
