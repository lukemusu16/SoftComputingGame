using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Dictionary<Vector2Int, GameObject> _tiles;
    private float originOffset = 0.5f;

    private GameManager gm;

    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public GridManager(int width, int height, GameObject TilePrefab)
    { 
        _tiles = new Dictionary<Vector2Int, GameObject>();

        for (int x = 0; x < GameData.Width; x++)
        {
            for (int y = 0; y < GameData.Height; y++)
            {
                //Position of the tile - taking care of grid offset
                Vector3 pos = new Vector3(x - (GameData.Width / 2) + originOffset, y - (GameData.Height / 2) + originOffset);
                 // Instantiating a new tile
                GameObject tile = Instantiate(TilePrefab, pos , Quaternion.identity);
                //Set is an obstacle or not
                tile.GetComponent<Tile>().setObstacle(false);
                //Storing the tile in the dictionary
                _tiles[new Vector2Int((int)pos.x, (int)pos.y)] = tile;
            }
        }
    }

    public bool isTileAvailable(Vector3 pos)
    {
        //Converting a vector3 to vector2Int
        Vector2Int tilePos = new Vector2Int((int)pos.x, (int)pos.y);
        //Check if the tile exists in our dictionary
        if (_tiles.ContainsKey(tilePos))
        {
            //If it exists, we check if it is an obstacle
            if (_tiles[tilePos].GetComponent<Tile>().isObstacle() || _tiles[tilePos].GetComponent<Tile>().isFood())
            {
                //If it is an obstacle, we return false
                return false;
            }
            else
            {
                //If it is not an obstacle, we return true
                return true;
            }
        }
        else
        {
            //Does not exist
            return false;
        }
    }


    public Vector3 getSpawnLocation()
    {
        while (true)
        {
            Vector3 randomPos = new Vector3(Random.Range(-(GameData.Width / 2), (GameData.Width / 2)), Random.Range(-(GameData.Height / 2)+2, (GameData.Height / 2)-2));
            if (isTileAvailable(randomPos))
            {
                return randomPos;
            }
        }
    }

    public void CreateMaze()
    {
        Vector2[,] Points = new Vector2[GameData.Width, GameData.Height];

        for (int x = 0; x < GameData.Width; x++)
        {
            for (int y = 0; y < GameData.Width; y++)
            {
                Points[x, y] = new Vector3(1 * x, 1 * y);
            }
        }

        for (int x = 0; x < GameData.Width; x++)
        {
            for (int y = 0; y < GameData.Height; y++)
            {
                gm.addObstacle();
            }
        }
    }
}
