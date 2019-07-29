using System.Collections;
using System.Collections.Generic;
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

}
