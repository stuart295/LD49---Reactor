using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestParticle : MonoBehaviour
{

    public float forceMag = 1f;
    public float randomForce = 1f;
    public float initialVel = 1f;


    private List<GameObject> magnets;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        magnets = new List<GameObject>(GameObject.FindGameObjectsWithTag("Magnet"));
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.up * initialVel;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 netForce = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), 0).normalized * randomForce;


        foreach (GameObject magnet in magnets) {
            if (magnet.name.StartsWith("FlatMag")){
                Vector3 closestPoint = magnet.GetComponent<Collider2D>().ClosestPoint(transform.position);
                Debug.DrawLine(transform.position, closestPoint, Color.red);
                netForce += AddForceRadial(closestPoint);
            }
            else {
                Debug.DrawLine(transform.position, magnet.transform.position, Color.red);
                netForce += AddForceRadial(magnet.transform.position);
            }

            Vector3 dispVec = transform.position - magnet.transform.position;
            Vector3 dir = dispVec.normalized;
            float magnitude = forceMag / Mathf.Pow(dispVec.magnitude, 2);
            netForce += dir * magnitude;

        }

        rb.AddForce(netForce);
    }


    private Vector3 AddForceRadial(Vector3 source) {
        Vector3 dispVec = transform.position - source;
        Vector3 dir = dispVec.normalized;
        float magnitude = forceMag / Mathf.Pow(dispVec.magnitude, 2);
        return dir * magnitude;
    }

}
