using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{

    int points;

    public bool isEaten = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        }

        print(GameData.Score);
    }
}
