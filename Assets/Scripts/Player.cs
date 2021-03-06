using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private float playerSpeed = 7.5f;

    private bool isLockHorizontal;
    private bool isLockVertical;

    [SerializeField]
    Rigidbody2D rb;
    Vector2 movement;

    GameManager manager;

    

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();

        
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.x != 0 && !isLockHorizontal)
        {
            isLockVertical = true;
            rb.MovePosition(rb.position + new Vector2(movement.x,0) * playerSpeed * Time.deltaTime);
        }
        else if (movement.y != 0 && !isLockVertical)
        {
            isLockHorizontal = true;
            rb.MovePosition(rb.position + new Vector2(0, movement.y) * playerSpeed * Time.deltaTime);
        }

        if (movement.x == 0)
        {
            isLockVertical = false;
        }
        else if (movement.y == 0)
        {
            isLockHorizontal = false;
        }
    }

    private void ReduceHealth(int value)
    {
        GameData.Health -= value;
        Text health = GameObject.Find("Canvas").transform.GetChild(1).GetComponentInChildren<Text>();

        health.text = GameData.Health.ToString();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            ReduceHealth(1);
            
            if (GameData.Health <= 0)
            {
                GameData gd = new GameData();
                SaveSystem.SaveScore(gd);
                SceneManager.LoadScene("Highscores");
            }

            Destroy(col.gameObject);

            Vector3 newPos = manager._gm.getSpawnLocation(gameObject);

            print(newPos);

            Instantiate(manager._seekerPrefab, newPos, Quaternion.identity);
        }
    }
}
