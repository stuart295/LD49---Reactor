using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BeamEmitter : Placable
{
    public Transform beamArea;
    public float maxLength = 1000f;
    public bool activateOnStay = false;


    protected override void Awake() {
        base.Awake();
        beamArea.gameObject.SetActive(false);
    }

    protected override void Update() {
        base.Update();
        UpdateCollectionZone();
    }

    public override void OnPlaced() {
        base.OnPlaced();
        beamArea.gameObject.SetActive(true);
    }

    public override void OnMove() {
        base.OnMove();
        beamArea.gameObject.SetActive(false);
    }

    private void UpdateCollectionZone() {
        int mask = LayerMask.GetMask(new string[] { "Building" });

        RaycastHit2D hit = Physics2D.Raycast(beamArea.position, beamArea.up, maxLength, mask);

        float scale = maxLength;

        if (hit.collider != null) {
            scale = Vector3.Distance(beamArea.position, hit.point);
        }

        beamArea.localScale = new Vector3(beamArea.localScale.x, scale, 1);

    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (!gm.Playing || !activateOnStay) return;

        if (collision.tag.Equals("Particle")) {
            OnBeamCollisionStart(collision.gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        if (!gm.Playing || activateOnStay) return;

        if (collision.tag.Equals("Particle")) {
            OnBeamCollisionStart(collision.gameObject);
        }
    }


    protected abstract void OnBeamCollisionStart(GameObject particleGo);


}
