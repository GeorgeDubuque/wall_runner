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
    public Transform win;
    public GameObject score;
    public float totalCoins;
    TextMeshProUGUI scoreText;
    public TextMeshProUGUI finishText;
    public TextMeshProUGUI finalScoreText;
    Animator scoreAnimator;
    public GameObject coinGrid;
    float prevScore = 0;
    float currScore = 0;

    bool playerOffScreen = false;
    bool playerWin = false;
    bool gameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        player = playerObj.GetComponent<Player>();
        scoreText = score.GetComponent<TextMeshProUGUI>();
        scoreAnimator = score.GetComponent<Animator>();
        totalCoins = coinGrid.transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerDead();
        CheckPlayerWin();
        gameOver = playerOffScreen  || playerWin;
        currScore = player.numCoins;
        if(prevScore != currScore) {
            scoreAnimator.SetTrigger("Scored");
            scoreText.text = currScore.ToString();
            prevScore = currScore;
        }
        if (gameOver)
        {
            if (playerWin)
            {
                DisplayWin();
            }
            else
            {
                GameOver();
            }
        }
    }

    void DisplayWin()
    {
        finishText.enabled = true;
        float scorePercent =(currScore / totalCoins) * 100f;
        Debug.Log(scorePercent);
        finalScoreText.text = "Score: " + LetterGrade(scorePercent);
        finalScoreText.enabled = true;
    }

    string LetterGrade(float scorePercent)
    {
        string letterGrade = "";


        if (scorePercent >= 94)
        {
            letterGrade = "A";
        }else if(scorePercent < 94 && scorePercent >= 90)
        {
            letterGrade = "A-";
        }
        else if (scorePercent < 90 && scorePercent >= 87)
        {
            letterGrade = "B+";
        }
        else if (scorePercent < 87 && scorePercent >= 83)
        {
            letterGrade = "B";
        }
        else if (scorePercent < 83 && scorePercent >= 80)
        {
            letterGrade = "B-";
        }
        else if (scorePercent < 80 && scorePercent >= 77)
        {
            letterGrade = "C+";
        }
        else if (scorePercent < 77 && scorePercent >= 73)
        {
            letterGrade = "C";
        }
        else if (scorePercent < 73 && scorePercent >= 70)
        {
            letterGrade = "C-";
        }
        else if (scorePercent < 70 && scorePercent >= 67)
        {
            letterGrade = "D+";
        }
        else if (scorePercent < 67 && scorePercent >= 63)
        {
            letterGrade = "D";
        }
        else if (scorePercent < 63 && scorePercent >= 60)
        {
            letterGrade = "D-";
        }
        else
        {
            letterGrade = "F";
        }

        return letterGrade;
    }

    void CheckPlayerDead()
    {
        Vector3 playerPoint = cam.WorldToViewportPoint(playerObj.transform.position);
        if(playerPoint.z < 0)
        {
            playerOffScreen = true;
        }
    }

    void CheckPlayerWin()
    {
        if (player.transform.position.z > win.position.z)
        {
            playerWin = true;
        }
    }

    void GameOver()
    {
        Scene currScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currScene.buildIndex);
    }
}
