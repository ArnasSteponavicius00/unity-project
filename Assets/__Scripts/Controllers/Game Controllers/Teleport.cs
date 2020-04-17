using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Utils;

public class Teleport : MonoBehaviour
{
    
    private Scene scene;
    public GameController game;

    void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    // Handle where to teleport
    void OnTriggerEnter2D(Collider2D playerEnter)
    {
        var player = playerEnter.GetComponent<PlayerMovement>();

        if(player)
        {
            if(scene.name == Utils.SceneNames.TUTORIAL)
            {
                SceneManager.LoadScene(Utils.SceneNames.LEVEL_ONE);
            }

            if(scene.name == Utils.SceneNames.LEVEL_ONE)
            {
                SceneManager.LoadScene(Utils.SceneNames.LEVEL_TWO);
            }

            if(scene.name == Utils.SceneNames.LEVEL_TWO)
            {
                SceneManager.LoadScene(Utils.SceneNames.FINAL_LEVEL);
            }

            if(scene.name == Utils.SceneNames.FINAL_LEVEL)
            {
                SceneManager.LoadScene(Utils.SceneNames.GAME_OVER);
            }
        }
    }
}
