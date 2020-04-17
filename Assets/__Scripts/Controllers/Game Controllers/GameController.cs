using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

using Utils;

public class GameController : MonoBehaviour
{

    // private fields
    [SerializeField] private TextMeshProUGUI gemText;
    [SerializeField] private TextMeshProUGUI totalGemText;
    [SerializeField] private TextMeshProUGUI timeAlive;
    [SerializeField] private GameObject teleporter;
    [SerializeField] private Transform teleporterPos;
    [SerializeField] private int gemsToTeleport;

    private int enemiesRemaining; 
    private int enemyWaveCount = 20;
    private int startWave = 0;
    private int waveNumber = 0;
    private int playerGems = 0; 
    private bool gameOver = false;
    private bool instatiateOnce = true;

    // public fields
    public GameObject gameOverUI;

    void Start()
    {
        playerGems = 0;
        UpdateGems();
        DisableSpawning();
        TeleportCondition(gemsToTeleport);
    }

    void Update()
    {
        UpdateGems();
        timeAlive.text = Time.time.ToString("F2");
        totalGemText.text = playerGems.ToString();

        if(playerGems >= gemsToTeleport && instatiateOnce)
        {
            TeleportCondition(gemsToTeleport);
            instatiateOnce = false;
        }

    }

    // Collider used to enable enemy spawning
    // player must walk past a certain point for enemies to
    // start spawning
    void OnTriggerEnter2D(Collider2D start)
    {
        var player = start.GetComponent<Player>();

        if(player)
        {
            EnableSpawning();
        }
    }

    //public methods
    // Display the game over canvas when the player dies
    // and freeze the time
    public void GameOver()
    {
        if(gameOver == false)
        {
            gameOver = true;
            gameOverUI.SetActive(true);
            Time.timeScale = 0f;   
        }
    }

    // If the player clicks the return button execute this method
    // hide the game over canvas, set the time back to normal
    // and reload the current scene/level
    public void Restart()
    {
        gameOverUI.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    // Use to freeze the game when the player clicks on the pause
    // button
    public void FreezeGame()
    {
        Time.timeScale = 0f;
    }
    
    // private methods
    #region OnEnable, OnDisable Methods

    private void OnEnable()
    {
        Enemy.EnemyKilledEvent += OnEnemyKilledEvent;
        PointSpawners.EnemySpawnedEvent += PointSpawners_OnEnemySpawnedEvent;
    }

    private void OnDisable()
    {
        Enemy.EnemyKilledEvent -= OnEnemyKilledEvent;
        PointSpawners.EnemySpawnedEvent -= PointSpawners_OnEnemySpawnedEvent;
    }
    #endregion

    // Used to set the amount of gems a player needs to gather
    // in the level to be able to complete it and advance to the 
    // next level
    private void TeleportCondition(int gems)
    {
        //if the player has more gems than the required gems to advance
        if(playerGems >= gems)
        {
            // Instatiate the teleporter at the Teleporter Position object
            GameObject teleport = Instantiate(teleporter, teleporterPos.position, teleporterPos.rotation);
        }
    }

    private void DisableSpawning()
    {
        // Get the spawners and call their method to disable them.
        foreach(var spawner in FindObjectsOfType<PointSpawners>())
        {
            spawner.DisableSpawning();
        }
    }

    private void EnableSpawning()
    {
        // Get the spawners and call their method to enable them.
        foreach(var spawner in FindObjectsOfType<PointSpawners>())
        {
            spawner.EnableSpawning();
        }
    }

    private void OnEnemyKilledEvent(Enemy enemy)
    {
        // add the gem value for the enemy to the player
        playerGems += enemy.GemValue;
        UpdateGems();
    }
    
    // Used to keep track of the amount of enemies spawned
    // to manage the waves
    private void PointSpawners_OnEnemySpawnedEvent()
    {
        enemiesRemaining--;

        if(enemiesRemaining == 0)
        {
            DisableSpawning();
            StartCoroutine(SetupNextWave());
        }
    }

    private IEnumerator SetupNextWave()
    {
        yield return new WaitForSeconds(5.0f);
        waveNumber++;
        enemiesRemaining = enemyWaveCount;
        //FindObjectOfType<PointSpawners>().EnableSpawning();
        EnableSpawning();
    }
    
    // Update the UI text for the amount of gems the player has
    private void UpdateGems()
    {
        gemText.text = playerGems.ToString();
    }
}

