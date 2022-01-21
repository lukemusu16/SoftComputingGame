using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private float playerSpeed = 5f;

    private bool isLockHorizontal;
    private bool isLockVertical;

    [SerializeField]
    Rigidbody2D rb;
    Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        
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
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            ReduceHealth(25);

            if (GameData.Health <= 0)
            {
                GameData.Score = 0;
                SceneManager.LoadScene("Highscores");
            }
        }
    }
}
