using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public Camera cam;
    public Transform spawn;

    bool playerOffScreen = false;
    bool gameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerDead();
        gameOver = playerOffScreen;
        if (gameOver)
        {
            GameOver();
        }
    }

    void CheckPlayerDead()
    {
        Vector3 playerPoint = cam.WorldToViewportPoint(player.transform.position);
        if(playerPoint.z < 0)
        {
            playerOffScreen = true;
        }
    }

    void GameOver()
    {
        Scene currScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currScene.buildIndex);
    }
}
