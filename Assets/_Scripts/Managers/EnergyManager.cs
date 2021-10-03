using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyManager : MonoBehaviour
{

    public float velConversionFactor = 1f;
    public float collisionEnergyFactor = 1f;
    public float required = 10f;
    public float demand = 0f;
    public string demandDescription = "Low";

    private float currentEnergy = 0f;

    private GameManager gm;

    public float CurrentEnergy { get => currentEnergy; }

    private void Awake() {
        gm = GetComponent<GameManager>();
    }

    private void Update() {
        if (gm.Playing)
            currentEnergy = Mathf.Max(0f, currentEnergy - demand * Time.deltaTime);
    }

    public void AddEnergy(float amount) {
        if (gm.Playing) {
            currentEnergy += amount;
            if (currentEnergy > required && gm.enabled) {
                gm.Win();
            }
        }
    }

    public void AddEnergyFromVelocity(Vector3 vel) {
        if (!gm.Playing) return;

        AddEnergy(vel.magnitude * velConversionFactor);
    }

    public void AddEnergyFromCollision(Vector3 vel) {
        if (!gm.Playing) return;

        AddEnergy(vel.magnitude * collisionEnergyFactor);
    }

    public float GetPercentAmount() {
        return currentEnergy / required;
    }

    public void OnPlay() {
        currentEnergy = 0f;
    }

    public void OnReset() {
        currentEnergy = 0f;
    }

}
