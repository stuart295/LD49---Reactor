using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyCollector : Placable
{
    public Transform collectionZone;
    public float maxLength = 1000f;

    private EnergyManager energy;

    protected override void Awake() {
        base.Awake();
        collectionZone.gameObject.SetActive(false);
        energy = GameObject.FindGameObjectWithTag("GameController").GetComponent<EnergyManager>();
    }

    public override void OnPlaced() {
        base.OnPlaced();
        SetupCollectionZone();
    }

    public override void OnMove() {
        base.OnMove();
        collectionZone.gameObject.SetActive(false);
    }

    private void SetupCollectionZone() {
        collectionZone.gameObject.SetActive(true);

        RaycastHit2D hit = Physics2D.Raycast(collectionZone.position, collectionZone.up);

        float scale = maxLength;

        if (hit.collider != null) {
            Debug.Log(hit.collider);
            scale = Vector3.Distance(collectionZone.position, hit.point);
        }

        collectionZone.localScale = new Vector3(collectionZone.localScale.x, scale, 1);

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        

        if (collision.tag.Equals("Particle")) {
            Debug.Log("Particle caught: " + collision.name);

            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

            energy.AddEnergyFromVelocity(rb.velocity);
        }
    }

}
