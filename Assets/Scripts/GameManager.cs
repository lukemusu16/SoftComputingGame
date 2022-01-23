using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public enum GameDiff
{ 
	Easy,
	Medium,
	Hard,
}


public class GameManager : MonoBehaviour
{
    private Camera _cam;
    
	[SerializeField] 
	private GameObject _tilePrefab;

	[SerializeField]
	public GameObject _seekerPrefab;

	[SerializeField]
	private GameObject _playerPrefab;

	[SerializeField]
	private GameObject _foodPrefab;

	public GridManager _gm;

	List<GameObject> skrs = new List<GameObject>();

	List<GameObject> obst = new List<GameObject>();

	List<GameObject> foods = new List<GameObject>();

	public int checkEaten = 0;



	public void Start()
	{
		SaveHighscores data = SaveSystem.LoadData();

		Debug.Log(data.hs1);
		Debug.Log(data.hs2);
		Debug.Log(data.hs3);
		Debug.Log(data.hs4);
		Debug.Log(data.hs5);

		GameData.HS1 = data.hs1;
		GameData.HS2 = data.hs2;
		GameData.HS3 = data.hs3;
		GameData.HS4 = data.hs4;
		GameData.HS5 = data.hs5;

		print(GameData.HS1);

		//Setting the _cam to main camera
		_cam = Camera.main;
		//Loading the path finding
		
		//Creating a new grid
		_gm = new GridManager(30	, 30, _tilePrefab);


		Text health = GameObject.Find("Canvas").transform.GetChild(1).GetComponentInChildren<Text>();
		Text score = GameObject.Find("Canvas").transform.GetChild(2).GetComponentInChildren<Text>();

		health.text = GameData.Health.ToString();
		score.text = GameData.Score.ToString();

		CreateMaze();

		
		for (int i = 0; i < 2; i++)
		{
			addSeeker();
		}

		for (int i = 0; i < Random.Range(10, 20); i++)
		{
			addFood();
		}

		addPlayer();

		startSeeking();
		
	}



	private void Update()
	{
		AstarPath.active.Scan();

		foreach (GameObject food in foods)
		{
			if (checkEaten == foods.Count)
			{
				SceneManager.LoadScene("Main");
			}
		}

		print(checkEaten + "/" + foods.Count);
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
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		Vector3 spawnLoc = (_gm.getSpawnLocation(enemies[0]) + _gm.getSpawnLocation(enemies[1]))/2;

		GameObject seek = Instantiate(_playerPrefab, spawnLoc, Quaternion.identity);
	}

	public void addObstacle(Vector2 spawnLoc)
    {
		GameObject tile = _tilePrefab;

        GameObject bl = Instantiate(tile, spawnLoc, Quaternion.identity);

        bl.GetComponent<Tile>().setObstacle(true);

		obst.Add(bl);

    }

	private void addFood()
	{
		GameObject food = _foodPrefab;

		Vector3 spawnLoc = _gm.getSpawnLocation();


		if (_gm._tiles.ContainsKey(new Vector2Int((int)spawnLoc.x, (int)spawnLoc.y)))
		{
			GameObject bl = Instantiate(food, spawnLoc, Quaternion.identity);

			foods.Add(bl);


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
