using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingLayerInMesh : MonoBehaviour
{
    public string layerName;
    public int layerOrder;

    void Start()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.sortingLayerName = layerName;
        meshRenderer.sortingOrder = layerOrder;
    }

}
