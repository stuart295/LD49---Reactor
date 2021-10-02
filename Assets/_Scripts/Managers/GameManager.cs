using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Physics")]
    public float magnetStrength = 1000f;

    [Header("Debug")]
    public bool debugMode = true;

    private bool playing = false;
    private UIManager ui;
    private BuildManager build;
    private EnergyManager energy;

    private List<Placable> placables;
    

    public bool Playing { get => playing; }

    // Start is called before the first frame update
    void Awake()
    {
        ui = GetComponent<UIManager>();
        build = GetComponent<BuildManager>();
        energy = GetComponent<EnergyManager>();
        placables = new List<Placable>();
    }

    internal void DeregisterPlacable(Placable placable) {
        placables.Remove(placable);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RegisterPlacable(Placable placable) {
        if (!placables.Contains(placable))
            placables.Add(placable);
    }


    public void Play() {
        playing = true;
        ui.OnPlay();
        build.OnPlay();
        energy.OnPlay();
    }

    public void Reset() {
        playing = false;
        ui.OnReset();
        build.OnReset();
        energy.OnReset();

        foreach (Placable placable in placables) {
            placable.ResetState();
        }
    }

}
