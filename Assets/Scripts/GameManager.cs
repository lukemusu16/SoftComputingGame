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

		_gm.CreateMaze();

		/*
		for (int i = 0; i < 2; i++)
		{
			addSeeker();
		}

		for (int i = 0; i < Random.Range(0, 10); i++)
		{
			addFood();
		}

		for (int i = 0; i < Random.Range(20, 80); i++)
		{
			addObstacle();
		}

		addPlayer();

		startSeeking();
		*/
	}

	private void Update()
	{
		AstarPath.active.Scan();
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

	public void addObstacle()
    {
		GameObject tile = _tilePrefab;

		Vector3 spawnLoc = _gm.getSpawnLocation();


		if (_gm._tiles.ContainsKey(new Vector2Int((int)spawnLoc.x, (int)spawnLoc.y)))
        {
            GameObject bl = Instantiate(tile, spawnLoc, Quaternion.identity);

            bl.GetComponent<Tile>().setObstacle(true);

			obst.Add(bl);	
		}

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
