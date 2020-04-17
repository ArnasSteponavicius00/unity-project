using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Arrow Type Config")]
public class ArrowTypes : ScriptableObject
{
    // private fields
    [SerializeField] private GameObject startArrowPrefab;
    [SerializeField] private GameObject fireArrowPrefab;
    [SerializeField] private GameObject poisonArrowPrefab;

    // public methods
    public GameObject GetStartArrow(){ return startArrowPrefab; }
    public GameObject GetFireArrow(){ return fireArrowPrefab; }
    public GameObject GetPoisonArrow(){ return poisonArrowPrefab; }

}
