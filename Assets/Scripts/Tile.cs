using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private bool _isObstacle = false;
    private bool _isFood = false;

    public bool isObstacle()
    {
        return _isObstacle;
    }

    public bool isFood()
    {
        return _isFood;
    }

    public void setObstacle(bool isObstacle)
    {
        _isObstacle = isObstacle;
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = isObstacle;
        this.gameObject.transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = isObstacle;
    }

    public void setFood(bool isFood)
    {
        _isFood = isFood;
        this.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = isFood;
        this.gameObject.transform.GetChild(1).GetComponent<BoxCollider2D>().enabled = isFood;
    }
}
