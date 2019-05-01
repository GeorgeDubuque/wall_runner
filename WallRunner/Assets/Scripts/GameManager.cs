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
    public float totalCoins;
    public Animator finishAnimator;
    public ScoreAnimator scoreAnimator;
    int animScore = 0;
    public GameObject coinGrid;
    float prevScore = 0;
    public float currScore = 0;
    bool playerOffScreen = false;
    bool playerWin = false;
    bool gameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        player = playerObj.GetComponent<Player>();
        totalCoins = coinGrid.transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            if (playerWin)
            {
                if (!finishAnimator.enabled) {
                    scoreAnimator.finalScore = currScore;
                    Debug.Log(currScore / totalCoins);
                    scoreAnimator.gradeScore = LetterGrade((currScore / totalCoins)*100f);
                    scoreAnimator.totalNumCoins = (int)totalCoins;
                    finishAnimator.enabled = true;
                }
            }
            else
            {
                GameOver();
            }
        } else {
            CheckPlayerDead();
            CheckPlayerWin();
            gameOver = playerOffScreen || playerWin;
            currScore = player.numCoins;
            
        }
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

    void NextLevel ( ) {
        PlayerData.currLevelInd++;
        SceneManager.LoadScene(PlayerData.levels[PlayerData.currLevelInd].name);
        playerWin = false;
        playerOffScreen = false;
    }
    
    void GameOver()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
