using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public enum Team {
    Neutral, Team1, Team2, Team3
}

public class GameController : MonoBehaviour
{
    public enum ControlMode {
        Unselected, UnitSelected
    }
    public ControlMode ctrlMode = ControlMode.Unselected;

    public BattleGroupController CurrentSelectedBattleGroup;

    //Singleton instance
    public static GameController gc; 

    private void Awake()
    {

        if (gc != null)
        {
            Destroy(gc);
        }
        gc = this;

    }

    private void FixedUpdate() {

        if (Input.GetKeyDown(KeyCode.Escape)) {

            ctrlMode = ControlMode.Unselected;

        }

    }

    public List<Tuple<Vector3, StationController>> GetMoveOrders(StationController present, StationController end, Vector3 finalMove, List<Tuple<Vector3, StationController>> moves)
    {
        //public List<Tuple<Vector3, StationController>> GetMoveOrders(StationController present, StationController end, List<Tuple<Vector3, StationController>> moves){

        if (present != end)
        {

            StationController nextTarget = present.stationConnections[0];
            
            foreach (StationController connect in present.stationConnections)
            {

                if (Vector2.Distance(connect.transform.position, end.transform.position) < Vector2.Distance(nextTarget.transform.position, end.transform.position))
                {

                    nextTarget = connect;

                }

            }

            Vector3 closestExit = present.exits[0];
            foreach (Vector3 exit in present.exits)
            {

                if (Vector3.Distance(exit, nextTarget.transform.position) < Vector3.Distance(closestExit, nextTarget.transform.position))
                {
                    closestExit = exit;
                }

            }

            Vector3 closestEntrance = nextTarget.exits[0];
            foreach (Vector3 entrance in nextTarget.exits)
            {

                if (Vector3.Distance(entrance, present.transform.position) < Vector3.Distance(closestEntrance, present.transform.position))
                {
                    closestEntrance = entrance;
                }

            }
            Tuple<Vector3, StationController> exitMove = Tuple.Create<Vector3, StationController>(closestExit, null);
            Tuple<Vector3, StationController> entranceMove = Tuple.Create<Vector3, StationController>(closestEntrance, nextTarget);

            moves.Add(exitMove);
            moves.Add(entranceMove);

            return GetMoveOrders(nextTarget, end, finalMove, moves);
            //return GetMoveOrders(nextTarget, end, moves);

        }
        else
        {

            moves.Add(Tuple.Create<Vector3, StationController>(finalMove, end));
            //moves.Add(Tuple.Create<Vector3, StationController>(present.transform.position, null));
            return moves;

        }

    }

}
