using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Pathfinding;

public class mySeeker : MonoBehaviour
{
    private IAstarAI[] _ais;

    [SerializeField]
    private GameObject _waypoint;

    private List<GameObject> wps = new List<GameObject>(10);

    

    int currPos = 0;

    public bool hasStarted = false;

    int layermask = 8;

    private bool isFleeing = false;

    private Transform fleewaypoint;


    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start");
        _ais = FindObjectsOfType<MonoBehaviour>().OfType<IAstarAI>().ToArray();

        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    void Update()
    {
        if (hasStarted)
        {

            if (!isFleeing)
            {
                this.gameObject.GetComponent<AIDestinationSetter>().target = player.transform;
                //_ais[100].SearchPath();
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.blue, Color.white, Mathf.PingPong(Time.time, 0.6f));
            }
            
            
        }
        
    }

    public Transform FleeWaypoint()
    {
        Vector3 waypoint;

        waypoint = new Vector3(Random.Range(-(GameData.Width / 2), (GameData.Width / 2)), Random.Range(-(GameData.Height / 2) + 2, (GameData.Height / 2) - 2));
        GameObject wp = Instantiate(_waypoint, waypoint, Quaternion.identity);
        wp.transform.position = waypoint;
        Debug.Log(wp.transform.position);
        wps.Add(wp);

        return wp.transform;
        
    }

    public IEnumerator Fleeing(Transform fwp)
    {
        Color currentColor = new Color(225,0,228);
        this.gameObject.GetComponent<AIDestinationSetter>().target = fwp;
        print("fleeing");
        yield return new WaitForSeconds(5f);
        print("not fleeing");
        gameObject.GetComponent<SpriteRenderer>().color = currentColor;
        isFleeing = false;
    }

    public void Flee()
    {
        fleewaypoint = FleeWaypoint();
        isFleeing = true;
        StartCoroutine(Fleeing(fleewaypoint));
    }

    
}