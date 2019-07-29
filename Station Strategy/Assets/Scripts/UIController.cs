using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text StationTitleTxt;
    public Button SpawnBattleGroupBtn;
    public GameObject MenuBackground;

    public GameObject BattleGroupBtnPrefab;

    public static UIController uic;

    StationController currentSelectedSc;

    void Awake()
    {

        if (uic != null) {
            Destroy(uic);
        }
        uic = this;

    }

    public void StationClicked(StationController sc) {

        StationTitleTxt.text = sc.name;
        currentSelectedSc = sc;

        if (sc.stationType == StationController.StationType.HomeBase) {
            SpawnBattleGroupBtn.gameObject.SetActive(true);
        }
        else {
            SpawnBattleGroupBtn.gameObject.SetActive(false);
        }

        RefreshBattleGroupButtons();

    }

    public void RefreshBattleGroupButtons() {

        foreach (Transform child in MenuBackground.transform) {

            if (child.CompareTag("BGBtn")) {
                Destroy(child.gameObject);
            }

        }

        int rowCount = 0;
        int colCount = 0;
        foreach (BattleGroupController bg in currentSelectedSc.battleGroups) {

            GameObject tempBtn = Instantiate(BattleGroupBtnPrefab , MenuBackground.transform);

            tempBtn.GetComponent<BGButtonController>().Initialize(bg);

            tempBtn.transform.position = new Vector3(tempBtn.transform.position.x + (50f * colCount) , tempBtn.transform.position.y - (50f * rowCount) , tempBtn.transform.position.z);

            colCount++;
            if (colCount > 4) {
                colCount = 0;
                rowCount++;
            }
        }

    }

    public void SpawnBattleGroupBtnPressed() {

        currentSelectedSc.SpawnBattleGroup();
        RefreshBattleGroupButtons();

    }

}
