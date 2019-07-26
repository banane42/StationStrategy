using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleGroupController : MonoBehaviour
{

    TextMesh GroupName;
    SpriteRenderer Renderer;

    // Start is called before the first frame update
    void Awake()
    {
        Renderer = GetComponent<SpriteRenderer>();
        GroupName = transform.GetChild(0).GetComponent<TextMesh>();

        GroupName.text = "BG\n1";
        Renderer.color = Color.blue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
