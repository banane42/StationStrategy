using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleGroupController : MonoBehaviour
{
    public GameObject parentStation;
    public Vector3 movePosition;
    public float maxSpeed = 1f;
    public int tagNumber;

    public Team team;

    TextMesh GroupName;
    SpriteRenderer Renderer;
    bool isEngaged = false;

    public List<BattleGroupController> touchingBattleGroups = new List<BattleGroupController>();

    void Awake()
    {
        Renderer = GetComponent<SpriteRenderer>();
        GroupName = transform.GetChild(0).GetComponent<TextMesh>();

        movePosition = transform.position;

    }

    //Call this every time a battle group is Instantiated
    public void Initialize(GameObject ps, Team team, int tagNum)
    {

        parentStation = ps;
        this.team = team;
        this.tagNumber = tagNum;

        if (team == Team.Team1)
        {
            //Blue
            Renderer.color = new Color(0f,0f,1f,0.5f);
        }
        else if (team == Team.Team2)
        {
            //Red
            Renderer.color = new Color(1f, 0f, 0f, 0.5f);
        }
        else if (team == Team.Team3)
        {
            //Green
            Renderer.color = new Color(0f, 1f, 0f, 0.5f);
        }
        else
        {
            //Grey
            Renderer.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        }

        GroupName.text = "BG\n" + tagNum;

    }

    void Update()
    {
        IsBattleGroupEngaged();

        //This is here due to a weird issue where dividing max speed by
        //touchingBattleGroups.Count + 1 gives back infinity. This is due to a weird 
        //thing where touchingBattleGroups.Count + 1 is returning 01 insted of 1.
        //A work around is to store the calculation in an int first like below.
        int bgCount = touchingBattleGroups.Count + 1;
        float realSpeed = maxSpeed / bgCount;

        if (transform.position != movePosition) {
            transform.position = Vector2.MoveTowards(transform.position , movePosition , realSpeed * Time.deltaTime);

        }
        else if (!isEngaged) {

            if (touchingBattleGroups.Count > 0) {

                foreach (BattleGroupController bg in touchingBattleGroups) {

                    Vector3 origin = (bg.transform.position + this.transform.position) / 2;

                    //Prevents a bug where is the two battle groups are in the same exact position, nothing will happen.
                    //So just move them slightly in a random direction
                    if (origin == transform.position) {
                        this.movePosition += new Vector3(0.01f * ((Random.Range(0,2) * 2) - 1), 0.01f * ((Random.Range(0 , 2) * 2) - 1));
                    }

                    Vector3 bgDir = (bg.transform.position - origin);
                    bg.movePosition += bgDir * 0.1f;                  

                    Vector3 thisDir = (this.transform.position - origin);
                    this.movePosition += thisDir * 0.1f;

                }

            }

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("BattleGroup"))
        {

            touchingBattleGroups.Add(collision.gameObject.GetComponent<BattleGroupController>());

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("BattleGroup"))
        {
            BattleGroupController tempBg = collision.gameObject.GetComponent<BattleGroupController>();
            if (touchingBattleGroups.Contains(tempBg))
            {

                touchingBattleGroups.Remove(tempBg);

            }

        }

    }

    public Color GetColor() {
        return Renderer.color;
    }

    void IsBattleGroupEngaged() {

        foreach (BattleGroupController bg in touchingBattleGroups) {

            if (bg.team != this.team) {
                isEngaged = true;
                return;
            }

        }

        isEngaged = false;
    }
}
