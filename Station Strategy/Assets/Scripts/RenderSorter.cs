using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderSorter : MonoBehaviour
{

    public int order;
    public string layerName;

    // Start is called before the first frame update
    void Start()
    {

        this.gameObject.GetComponent<MeshRenderer>().sortingOrder = order;
        this.gameObject.GetComponent<MeshRenderer>().sortingLayerName = layerName;

    }
}
