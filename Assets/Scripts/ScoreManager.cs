using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    
    Text hs1;
    Text hs2;
    Text hs3;
    Text hs4;
    Text hs5;

    // Start is called before the first frame update
    void Start()
    {
        hs1 = GameObject.Find("Canvas").transform.GetChild(5).GetComponent<Text>();
        hs2 = GameObject.Find("Canvas").transform.GetChild(6).GetComponent<Text>();
        hs3 = GameObject.Find("Canvas").transform.GetChild(7).GetComponent<Text>();
        hs4 = GameObject.Find("Canvas").transform.GetChild(8).GetComponent<Text>();
        hs5 = GameObject.Find("Canvas").transform.GetChild(9).GetComponent<Text>();

        if (GameData.Score > GameData.Highscore1)
        {
            GameData.Highscore2 = GameData.Highscore1;
            GameData.Highscore3 = GameData.Highscore2;
            GameData.Highscore4 = GameData.Highscore3;
            GameData.Highscore5 = GameData.Highscore4;

            GameData.Highscore1 = GameData.Score;
        }
        else if (GameData.Highscore1 > GameData.Score && GameData.Score > GameData.Highscore2)
        {
            GameData.Highscore3 = GameData.Highscore2;
            GameData.Highscore4 = GameData.Highscore3;
            GameData.Highscore5 = GameData.Highscore4;

            GameData.Highscore2 = GameData.Score;

        }
        else if (GameData.Highscore2 > GameData.Score && GameData.Score > GameData.Highscore3)
        {
            GameData.Highscore4 = GameData.Highscore3;
            GameData.Highscore5 = GameData.Highscore4;

            GameData.Highscore3 = GameData.Score;

        }
        else if (GameData.Highscore3 > GameData.Score && GameData.Score > GameData.Highscore4)
        {
            GameData.Highscore5 = GameData.Highscore4;

            GameData.Highscore4 = GameData.Score;

        }
        else if (GameData.Highscore4 > GameData.Score && GameData.Score > GameData.Highscore5)
        {
            GameData.Highscore5 = GameData.Score;
        }

        GameData.Score = 0;

        hs1.text = GameData.Highscore1.ToString();
        hs2.text = GameData.Highscore2.ToString();
        hs3.text = GameData.Highscore3.ToString();
        hs4.text = GameData.Highscore4.ToString();
        hs5.text = GameData.Highscore5.ToString();
    }
}
