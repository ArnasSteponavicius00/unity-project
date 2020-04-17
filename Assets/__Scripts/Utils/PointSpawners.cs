using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Utilities;

public class PointSpawners : MonoBehaviour
{
    // private fields
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private float spawnDelay = 0.25f;
    [SerializeField] private float spawnInterval = 0.35f;

    private const string SPAWN_ENEMY_METHOD = "SpawnOneEnemy";
    private IList<SpawnPoint> spawnPoints;
    private Stack<SpawnPoint> spawnStack;
    private GameObject enemyParent;

    // delegate variables for events
    public delegate void EnemySpawned();
    public static event EnemySpawned EnemySpawnedEvent;

    // private methods 
    private void Start()
    {
        enemyParent = GameObject.Find("EnemyParent");
        if(!enemyParent)
        {
            enemyParent = new GameObject("EnemyParent");
        }
        // Get the spawn points
        spawnPoints = GetComponentsInChildren<SpawnPoint>();
        // Create the stack of points
        spawnStack = ListUtils.CreateShuffledStack(spawnPoints);
    }

    private void SpawnEnemyWaves()
    {
        // create the stack of points
        spawnStack = ListUtils.CreateShuffledStack(spawnPoints);
    }

    // stack version
    private void SpawnOneEnemy()
    {
        if(spawnStack.Count == 0)
        {
            spawnStack = ListUtils.CreateShuffledStack(spawnPoints);
        }

        var enemy = Instantiate(enemyPrefab, enemyParent.transform);
        var sp = spawnStack.Pop();

        enemy.transform.position = sp.transform.position;
        // Tell system enemy spawned
        PublishOnEnemySpawnedEvent();
    }

    // public methods
    // create event to publish that enemy spawned
    public void PublishOnEnemySpawnedEvent()
    {
        //
        EnemySpawnedEvent?.Invoke();
    }

    // public methods
    public void EnableSpawning()
    {
        InvokeRepeating(SPAWN_ENEMY_METHOD, spawnDelay, spawnInterval);
    }

    public void DisableSpawning()
    {
        CancelInvoke(SPAWN_ENEMY_METHOD);
    }
}
