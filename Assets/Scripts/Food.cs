using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{

    int points;

    private bool isEaten = false;

    GameManager gm;


    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool GetIsEaten()
    {
        return isEaten;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;

            GameObject[] skrs = GameObject.FindGameObjectsWithTag("Enemy");
            print(skrs.Length);
            foreach (GameObject seeker in skrs)
            {
                seeker.GetComponent<mySeeker>().Flee();
            }

            points = Random.Range(1, 5) * 5;
            GameData.Score += points;

            isEaten = true;
            gm.checkEaten++;
        }

        print(GameData.Score);
    }
}
