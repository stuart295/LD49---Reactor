using System.Collections.Generic;
using UnityEngine;

public class StrangeParticle : Placable
{

    public float maxVel = 100f;

    public GameObject deathEffectPref;

    private List<Magnet> magnets;
    private Rigidbody2D rb;
    private EnergyManager energy;



   


    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        energy = gm.GetComponent<EnergyManager>();
        OnPlaced();
        Time.fixedDeltaTime = 0.005f;
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

            SetLayer("Particle");

            if (rb.isKinematic) rb.isKinematic = false;


        }
        else {
            magnets = null;
            SetLayer("ParticlePlacement");
        }
    }


    // Update is called once per frame
    void FixedUpdate() {
        if (gm.Playing) {
            UpdateForces();
            energy.AddEnergyFromVelocity(rb.velocity);

            if (!IsOnScreen()){
                Kill();
            }
        }   
    }

    private void SetLayer(string name) {
        int layer = LayerMask.NameToLayer(name);
        if (gameObject.layer != layer)
            gameObject.layer = layer;
    }


    private void UpdateForces() {
        if (magnets == null) return;

        Vector3 netForce = Vector3.zero;

        foreach (Magnet magnet in magnets) {
            netForce += magnet.GetForceVector(transform.position);
        }

        rb.AddForce(netForce);

        if (rb.velocity.magnitude > maxVel) {
            rb.velocity = rb.velocity.normalized * maxVel;
        }
    }

    public override void ResetState() {
        base.ResetState();
        rb.velocity = Vector3.zero ;
        gameObject.SetActive(true);
    }


    private void OnCollisionEnter2D(Collision2D collision) {

        if (!gm.Playing) return;
        
        if (collision.collider.tag.Equals("Particle")) {
            //TODO
        }
        else {
            Debug.Log("Particle collided!");
            Kill();
        }
    }

    private bool IsOnScreen() {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        return screenPos.x > 0f && screenPos.x < Screen.width && screenPos.y > 0f && screenPos.y < Screen.height;
    }

    public void Kill() {
        Instantiate(deathEffectPref, transform.position, transform.rotation);

        gameObject.SetActive(false);

        gm.Lose();
    }
}
