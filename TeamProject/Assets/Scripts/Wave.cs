using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Wave"))]
public class Wave : ScriptableObject
{
    public Path path;
    public bool reverse, reverseLoop;
    public float coolDown, startDelay, endDelay;
    public GameObject[] enemies;
    
}
