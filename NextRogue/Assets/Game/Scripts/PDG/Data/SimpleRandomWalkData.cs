using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SRW_",menuName = "PDG/RandomWalkData")]
public class SimpleRandomWalkData : ScriptableObject
{
    public int Iterations = 10;
    public int WalkLength = 10;
    public bool StartRandomly = true;
}
