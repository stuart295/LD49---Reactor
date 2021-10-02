using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Physics")]
    public float magnetStrength = 1000f;

    [Header("Debug")]
    public bool debugMode = true;

    private bool paused = true;

    public bool Paused { get => paused; set => paused = value; }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
