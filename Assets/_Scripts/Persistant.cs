using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persistant : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Music");
        if (gos.Length > 1) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

    }

 
}
