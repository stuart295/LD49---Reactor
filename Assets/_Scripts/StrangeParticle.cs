using System.Collections.Generic;
using UnityEngine;

public class StrangeParticle : MonoBehaviour
{

    private List<Magnet> magnets;
    private Rigidbody2D rb;
    private GameManager gm;


    // Start is called before the first frame update
    void Start() {
        GameObject[] magGos = GameObject.FindGameObjectsWithTag("Magnet");
        foreach (GameObject magGo in magGos) {
            magnets.Add(magGo.GetComponent<Magnet>());
        }

        rb = GetComponent<Rigidbody2D>();
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (!gm.Paused)
            UpdateForces();
    }

    private void UpdateForces() {
        Vector3 netForce = Vector3.zero;

        foreach (Magnet magnet in magnets) {
            netForce += magnet.GetForceVector(transform.position);
        }

        rb.AddForce(netForce);
    }
}
