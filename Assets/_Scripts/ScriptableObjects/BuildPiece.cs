using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildPiece", menuName = "Data")]
public class BuildPiece : ScriptableObject
{
    public string pieceName = "";
    public Sprite icon;
    public GameObject prefab;



}
