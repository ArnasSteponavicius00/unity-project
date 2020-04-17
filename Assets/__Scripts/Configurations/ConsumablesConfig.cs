using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ConsumablesConfig : ScriptableObject
{
    // private fields
    [SerializeField] private GameObject healthPrefab;
    [SerializeField] private Transform enemy;

    // public methods
    public GameObject GetHealthPotion()
    { 
        GameObject health = Instantiate(healthPrefab, enemy.position, enemy.rotation);
        return health; 
    }

}
