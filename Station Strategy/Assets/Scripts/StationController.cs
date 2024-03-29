﻿using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class StationController : MonoBehaviour
{
    public string name;

    public StationController[] stationConnections;
    public Vector3[] exits;
    public List<BattleGroupController> battleGroups = new List<BattleGroupController>();

    public GameObject battleGroupPrefab;
    public Team team;
    int battleGroupsSpawned = 0;

    SpriteRenderer Renderer;

    public enum StationType {
        HomeBase, Dome
    }
    public StationType stationType;

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
        if (GameController.gc.ctrlMode == GameController.ControlMode.Unselected) {
            UIController.uic.StationClicked(this);
        }
        else if (GameController.gc.ctrlMode == GameController.ControlMode.UnitSelected) {

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 finalMove = new Vector3(mousePos.x, mousePos.y, 0f);

            foreach (Tuple<Vector3, StationController> moves in GameController.gc.GetMoveOrders(GameController.gc.CurrentSelectedBattleGroup.parentStation, this, finalMove, new List<Tuple<Vector3, StationController>>()))
                //foreach (Tuple<Vector3, StationController> moves in GameController.gc.GetMoveOrders(GameController.gc.CurrentSelectedBattleGroup.parentStation, this, new List<Tuple<Vector3, StationController>>()))
            {
                GameController.gc.CurrentSelectedBattleGroup.moveOrders.Enqueue(moves);
            }
            GameController.gc.ctrlMode = GameController.ControlMode.Unselected;
            UIController.uic.StationClicked(this);

        }

        //Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //print("Station Click: " + mousePosWorld);

    }

    public void SpawnBattleGroup() {

        if (stationType == StationType.HomeBase) {

            GameObject tempBG = Instantiate(battleGroupPrefab , transform.position , Quaternion.Euler(Vector3.zero));
            BattleGroupController tempBGC = tempBG.GetComponent<BattleGroupController>();
            tempBGC.Initialize(this , team , battleGroupsSpawned);

            battleGroupsSpawned++;

            battleGroups.Add(tempBGC);

        }

    }

}