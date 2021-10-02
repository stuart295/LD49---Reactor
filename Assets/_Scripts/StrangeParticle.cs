using System.Collections.Generic;
using UnityEngine;

public class StrangeParticle : Placable
{

    private List<Magnet> magnets;
    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        OnPlaced();
    }


    private void FindMagnets() {
        magnets = new List<Magnet>();
        GameObject[] magGos = GameObject.FindGameObjectsWithTag("Magnet");
        foreach (GameObject magGo in magGos) {
            magnets.Add(magGo.GetComponent<Magnet>());
        }
    }

    private void Update() {
        if (gm.Playing) {
            if (magnets == null) {
                FindMagnets();
            }
        }
        else {
            magnets = null;
        }
    }


    // Update is called once per frame
    void FixedUpdate() {
        if (gm.Playing)
            UpdateForces();
    }

    private void UpdateForces() {
        if (magnets == null) return;

        Vector3 netForce = Vector3.zero;

        foreach (Magnet magnet in magnets) {
            netForce += magnet.GetForceVector(transform.position);
        }

        rb.AddForce(netForce);
    }

    public override void ResetState() {
        base.ResetState();
        rb.velocity = Vector3.zero ;
    }
}
