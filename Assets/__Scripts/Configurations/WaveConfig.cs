using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Wave Type Config")]
public class WaveConfig : ScriptableObject
{
    // private data fields
    [SerializeField] private GameObject enemy;
    [SerializeField] private int enemiesPerWave = 10;
    [SerializeField] private float enemySpeed = 3.0f;
    [SerializeField] private float spawnInterval = 0.35f;
    [SerializeField] private int gemValue = 20;
    [SerializeField] private int damageValue = 5;

    // public methods
    public GameObject GetEnemy() { return enemy; }
    public int GetEnemiesPerWave() { return enemiesPerWave; }
    public float GetEnemySpeed() { return enemySpeed; }
    public float GetSpawnInterval() { return spawnInterval; }
    public int GetGemValue() { return gemValue; }
    public int GetDamageValue() { return damageValue; }
}
