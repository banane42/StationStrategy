using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGButtonController : MonoBehaviour
{

    Image img;
    Text nameTxt;
    BattleGroupController BGController;

    void Awake()
    {
        img = GetComponent<Image>();
        nameTxt = transform.GetChild(0).GetComponent<Text>();
    }

    public void Initialize(BattleGroupController bg) {

        BGController = bg;
        img.color = bg.GetColor();
        nameTxt.text = "BG\n" + bg.tagNumber;

    }

    public void Pressed() {

        print("I was pressed! " + BGController.tagNumber);
        GameController.gc.ctrlMode = GameController.ControlMode.UnitSelected;
        GameController.gc.CurrentSelectedBattleGroup = BGController;

    }

}
