using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text StationTitleTxt;

    public static UIController uic;

    void Awake()
    {

        if (uic != null) {
            Destroy(uic);
        }
        uic = this;

    }

    void FixedUpdate()
    {
        
    }

    public void SetStationTitle(string txt) {

        StationTitleTxt.text = txt;

    }

}
