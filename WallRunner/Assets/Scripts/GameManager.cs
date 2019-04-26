using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject playerObj;
    Player player;
    public Camera cam;
    public Transform spawn;
    public GameObject score;
    TextMeshProUGUI scoreText;
    Animator scoreAnimator;
    int prevScore = 0;
    int currScore = 0;

    bool playerOffScreen = false;
    bool gameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        player = playerObj.GetComponent<Player>();
        scoreText = score.GetComponent<TextMeshProUGUI>();
        scoreAnimator = score.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerDead();
        gameOver = playerOffScreen;
        currScore = player.numCoins;
        if(prevScore != currScore) {
            scoreAnimator.SetTrigger("Scored");
            scoreText.text = currScore.ToString();
            prevScore = currScore;
        }
        if (gameOver)
        {
            GameOver();
        }
    }

    void CheckPlayerDead()
    {
        Vector3 playerPoint = cam.WorldToViewportPoint(playerObj.transform.position);
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
