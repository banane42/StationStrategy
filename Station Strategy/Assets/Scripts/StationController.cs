using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationController : MonoBehaviour
{
    public GameObject[] stationConnections;
    public Vector3[] exits;
    public List<GameObject> battleGroups = new List<GameObject>();

    public GameObject battleGroupPrefab;
    public Team team;
    int battleGroupsSpawned = 0;

    SpriteRenderer Renderer;

    // Start is called before the first frame update
    void Awake()
    {

        Renderer = GetComponent<SpriteRenderer>();

        if (team == Team.Team1)
        {
            //Blue
            Renderer.color = new Color(0.36f, 0.47f, 0.92f, 1f);
        }
        else if (team == Team.Team2)
        {
            //Red
            Renderer.color = new Color(0.9f, 0.28f, 0.15f, 1f);
        }
        else if (team == Team.Team3)
        {
            //Green
            Renderer.color = new Color(0.36f, 1f, 0.28f, 1f);
        }
        else
        {
            Renderer.color = Color.white;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {

        Debug.Log("Spawning Station!");
        GameObject tempBG = Instantiate(battleGroupPrefab, transform.position, Quaternion.Euler(Vector3.zero));

        tempBG.GetComponent<BattleGroupController>().Initialize(gameObject, team, battleGroupsSpawned);

        battleGroupsSpawned++;

        battleGroups.Add(tempBG);

    }

}