using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{

    public GameObject apple;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public int score;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(apple, new Vector3(7, 0, 0), transform.rotation);
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateApple()
    {
        Instantiate(apple, new Vector3(Random.Range(-20, 20), Random.Range(-12, 13), 0), transform.rotation);
    }

    public void addScore()
    {
        score++;
        scoreText.text = "Score: " + score;

        if(score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScoreText.text = "High Score: " + score;
        }

    }

}
