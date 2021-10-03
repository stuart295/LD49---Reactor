using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Physics")]
    public float magnetStrength = 1000f;
    public float distanceModifier = 1f;

    [Header("Debug")]
    public bool debugMode = true;
    public Vector2 fieldTestArea = Vector2.zero;
    public float fieldSpacing = 1f;
    public float maxDispForce = 1f;

    private bool playing = false;
    private UIManager ui;
    private BuildManager build;
    private EnergyManager energy;

    private List<Placable> placables;

    private bool levelCompleted = false;


    public bool Playing { get => playing; }

    // Start is called before the first frame update
    void Awake() {
        ui = GetComponent<UIManager>();
        build = GetComponent<BuildManager>();
        energy = GetComponent<EnergyManager>();
        placables = new List<Placable>();
    }

    internal void DeregisterPlacable(Placable placable) {
        placables.Remove(placable);
    }

    // Update is called once per frame
    void Update() {

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
        ui.HideBanner();
        ui.OnReset();
        build.OnReset();
        energy.OnReset();

        foreach (Placable placable in placables) {
            placable.ResetState();
        }
    }

    public void Lose() {
        if (levelCompleted) return;
        //TODO
        ui.ShowBanner("Failed!");
        ui.SetPlayPanelEnabled(false);
        StartCoroutine(DelayedReset());
    }

    public void Win() {
        if (levelCompleted) return;

        levelCompleted = true;
        ui.SetPlayPanelEnabled(false);
        ui.ShowBanner("Level Complete!");
        StartCoroutine(DelayedNextLevel());
    }

    private IEnumerator DelayedReset() {
        yield return new WaitForSeconds(3f);
        Reset();
    }

    private IEnumerator DelayedNextLevel() {
        yield return new WaitForSeconds(3f);
        NextLevel();
    }

    private void OnDrawGizmos() {
        if (!debugMode || placables == null || fieldSpacing == 0) return;

        for (float x = -fieldTestArea.x / 2f; x < fieldTestArea.x / 2f; x += fieldSpacing) {
            for (float y = -fieldTestArea.y / 2f; y < fieldTestArea.y / 2f; y += fieldSpacing) {
                Vector3 pos = new Vector3(x, y, 0);
                float fieldStrength = GetFieldStrength(pos).magnitude;
                if (fieldStrength < 1f) continue;

                fieldStrength = Mathf.Clamp(fieldStrength, 0f, maxDispForce)/maxDispForce;
                Gizmos.color = Color.Lerp(Color.blue, Color.red, fieldStrength);

                Gizmos.DrawSphere(pos, 0.1f);
            }
        }


    }

    private Vector3 GetFieldStrength(Vector3 pos) {
        
        Vector3 netForce = Vector3.zero; 

        foreach (Placable p in placables) {
            if (p is Magnet) {
                netForce += ((Magnet)p).GetForceVector(pos);
            }
        }

        return netForce;
    }

    private void NextLevel() {
        int curScene = SceneManager.GetActiveScene().buildIndex;
        if (curScene < SceneManager.sceneCount - 1) {
            SceneManager.LoadScene(curScene + 1);
        }
        else {
            //Main Menu
            SceneManager.LoadScene(0);
        }

        
    }
}
