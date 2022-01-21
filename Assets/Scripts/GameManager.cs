using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System.Linq;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private Camera _cam;
    
	[SerializeField] 
	private GameObject _tilePrefab;

	[SerializeField]
	private GameObject _seekerPrefab;

	[SerializeField]
	private GameObject _playerPrefab;

	private GridManager _gm;

	List<GameObject> skrs = new List<GameObject>();

	List<GameObject> obst = new List<GameObject>();

	List<GameObject> food = new List<GameObject>();



	int width, height;


	public void Start()
	{
		//Setting the _cam to main camera
		_cam = Camera.main;
		//Loading the path finding
		
		//Creating a new grid
		_gm = new GridManager(30	, 30, _tilePrefab);

		CreateMaze();

		
		for (int i = 0; i < 2; i++)
		{
			addSeeker();
		}

		for (int i = 0; i < Random.Range(0, 10); i++)
		{
			addFood();
		}

		addPlayer();

		startSeeking();
		
	}



	private void Update()
	{
		AstarPath.active.Scan();
	}

	public void CreateMaze()
	{
		Vector2[,] Points = new Vector2[GameData.Width, GameData.Height];

		for (int x = 0; x < GameData.Width; x++)
		{
			for (int y = 0; y < GameData.Width; y++)
			{
				Points[x, y] = new Vector3((1 * x), (1 * y));
				//print(Points[x, y]);
			}
		}

		for (int x = 0; x < GameData.Width; x++)
		{
			for (int y = 0; y < GameData.Width; y++)
			{
				GameObject delSquare;

				if (_gm._tiles.TryGetValue(Points[x, y], out delSquare))
				{
					delSquare.GetComponent<Tile>().setObstacle(true);
					print(delSquare);
				}
			}
		}


		for (int x = 0; x < GameData.Width; x++)
		{
			for (int y = 0; y < GameData.Width; y++)
			{
				if (x == 0 || y == 0 || x == GameData.Width - 1 || y == GameData.Height - 1)
				{
					GameObject delSquare;

					if (_gm._tiles.TryGetValue(Points[x, y], out delSquare))
					{
						delSquare.GetComponent<Tile>().isVisited = true;
						//print(delSquare);
					}
				}
				else if (y == 1 || y == GameData.Height - 2)
				{
					GameObject delSquare;

					if (_gm._tiles.TryGetValue(Points[x, y], out delSquare))
					{
						delSquare.GetComponent<Tile>().isVisited = true;
						delSquare.GetComponent<Tile>().setObstacle(false);
						print(delSquare.transform.position + " " + x + " " + y);
					}
				}

			}
		}

		GameObject outSquare;

		//GameData.Width / 2, GameData.Height / 2

		if (_gm._tiles.TryGetValue(Points[GameData.Width / 2, GameData.Height / 2], out outSquare))
		{
			MazeAlgorithm(outSquare, Points);
		}
	}

	private void MazeAlgorithm(GameObject tile, Vector2[,] points)
	{

		tile.GetComponent<Tile>().isVisited = true;

		Vector3 pos = tile.transform.position;


		GameObject[] tiles = new GameObject[4];


		if (_gm._tiles.TryGetValue(points[(int)pos.x+1, (int)pos.y], out tiles[0]) &&
			_gm._tiles.TryGetValue(points[(int)pos.x, (int)pos.y+1], out tiles[1]) &&
			_gm._tiles.TryGetValue(points[(int)pos.x-1, (int)pos.y], out tiles[2]) &&
			_gm._tiles.TryGetValue(points[(int)pos.x, (int)pos.y-1], out tiles[3]))
		{
			int ranNum = Random.Range(0, 4);

			while (tiles[0].GetComponent<Tile>().isVisited == false ||
				  tiles[1].GetComponent<Tile>().isVisited == false ||
				  tiles[2].GetComponent<Tile>().isVisited == false ||
				  tiles[3].GetComponent<Tile>().isVisited == false)
			{

				print(tiles[ranNum].transform.position + " " + points);

				if (tiles[ranNum].GetComponent<Tile>().isVisited == false)
				{
					switch (ranNum)
					{
						case 0:
							tiles[0].GetComponent<Tile>().isVisited = true;
							tiles[1].GetComponent<Tile>().isVisited = true;
							tiles[3].GetComponent<Tile>().isVisited = true;

							tiles[ranNum].GetComponent<Tile>().setObstacle(false);

							MazeAlgorithm(tiles[ranNum], points);
							break;

						case 1:
							tiles[1].GetComponent<Tile>().isVisited = true;
							tiles[0].GetComponent<Tile>().isVisited = true;
							tiles[2].GetComponent<Tile>().isVisited = true;

							tiles[ranNum].GetComponent<Tile>().setObstacle(false);

							MazeAlgorithm(tiles[ranNum], points);
							break;

						case 2:
							tiles[2].GetComponent<Tile>().isVisited = true;
							tiles[1].GetComponent<Tile>().isVisited = true;
							tiles[3].GetComponent<Tile>().isVisited = true;

							tiles[ranNum].GetComponent<Tile>().setObstacle(false);

							MazeAlgorithm(tiles[ranNum], points);
							break;

						case 3:
							tiles[3].GetComponent<Tile>().isVisited = true;
							tiles[0].GetComponent<Tile>().isVisited = true;
							tiles[2].GetComponent<Tile>().isVisited = true;

							tiles[ranNum].GetComponent<Tile>().setObstacle(false);

							MazeAlgorithm(tiles[ranNum], points);
							break;
					}
				}
				else if (ranNum == 3)
				{
					ranNum = 0;
				}
				else
				{
					ranNum++;
				}
			}
		}


		return;



	}

	private void addSeeker()
	{
		Vector3 spawnLoc = _gm.getSpawnLocation();
		GameObject seek = Instantiate(_seekerPrefab, spawnLoc, Quaternion.identity);
		skrs.Add(seek);
	}

	private void addPlayer()
	{
		Vector3 spawnLoc = _gm.getSpawnLocation();
		GameObject seek = Instantiate(_playerPrefab, spawnLoc, Quaternion.identity);
	}

	public void addObstacle(Vector2 spawnLoc)
    {
		GameObject tile = _tilePrefab;

        GameObject bl = Instantiate(tile, spawnLoc, Quaternion.identity);

		print("gay");

        bl.GetComponent<Tile>().setObstacle(true);

		print("ass");

		obst.Add(bl);

    }

	private void addFood()
	{
		GameObject tile = _tilePrefab;

		Vector3 spawnLoc = _gm.getSpawnLocation();


		if (_gm._tiles.ContainsKey(new Vector2Int((int)spawnLoc.x, (int)spawnLoc.y)))
		{
			GameObject bl = Instantiate(tile, spawnLoc, Quaternion.identity);

			bl.GetComponent<Tile>().setFood(true);

			bl.GetComponentInChildren<Food>().enabled = true;

			food.Add(bl);


		}

	}

	private void startSeeking()
	{
		foreach (GameObject s in skrs)
		{
			s.GetComponent<mySeeker>().hasStarted = true;
		}

	}

    private void stopNClear()
    {
		foreach(GameObject s in skrs)
		{
			Destroy(s);
		}
		skrs.RemoveRange(0, skrs.Count);

		foreach (GameObject tile in obst)
		{
			Destroy(tile);
		}
		obst.RemoveRange(0, obst.Count);

	}

}
