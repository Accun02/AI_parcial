using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PFNodeGrid : MonoBehaviour
{
    [SerializeField] LayerMask nodes;
    [SerializeField] LayerMask obs;
    public PFNodes prefab;
    public PFNodes[] nodeGrid;
    public int width;
    public int height;
    public float distance;

    [ContextMenu("Crear Nodos")]
    public void SetNodeGrid()
    {
        nodeGrid = new PFNodes[width * height];
        for (int h = 0; h < height; h++)
        {
            for (int w = 0; w < width; w++)
            {
                PFNodes newNode = Instantiate(prefab, transform.position + 
                    new Vector3(w * distance, 0, h * distance), Quaternion.identity, transform);
                newNode.SetIndexes(w, h);
                nodeGrid[w + h * width] = newNode;
            }
        }
        for (int h = 0; h < height; h++)
        {

            for (int w = 0; w < width; w++)
            {
       

                nodeGrid[w + h * width].SetNeighbors(nodes,obs);
            }
        }
    }
  
}
