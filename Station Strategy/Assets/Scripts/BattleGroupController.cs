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

    public List<GameObject> touchingBattleGroups = new List<GameObject>();

    void Awake()
    {
        Renderer = GetComponent<SpriteRenderer>();
        GroupName = transform.GetChild(0).GetComponent<TextMesh>();

        movePosition = transform.position;

    }

    private void Start()
    {  
        
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

        //This is here due to a weird issue where dividing max speed by
        //touchingBattleGroups.Count + 1 gives back infinity. This is due to a weird 
        //thing where touchingBattleGroups.Count + 1 is returning 01 insted of 1.
        //A work around is to store the calculation in an int first like below.
        int bgCount = touchingBattleGroups.Count + 1;
        float realSpeed = maxSpeed / bgCount;

        if (transform.position != movePosition)
        {
            transform.position = Vector2.MoveTowards(transform.position, movePosition, realSpeed * Time.deltaTime);
            
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("BattleGroup"))
        {

            touchingBattleGroups.Add(collision.gameObject);

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("BattleGroup"))
        {

            if (touchingBattleGroups.Contains(collision.gameObject))
            {

                touchingBattleGroups.Remove(collision.gameObject);

            }

        }

    }

    public Color GetColor() {
        return Renderer.color;
    }


}
