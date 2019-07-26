using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    //Singleton instance
    public static GameController gc; 

    private void Awake()
    {

        if (gc != null)
        {
            Destroy(gc);
            gc = this;
        }

    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

public enum Team { Neutral, Team1, Team2, Team3 }
