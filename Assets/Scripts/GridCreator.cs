using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCreator : MonoBehaviour
{
    public LevelEditor levelEditor;
    public GameObject gridPrefab;
    private GameObject tempGrid;
    private Transform gridsparent;
    public List<GameObject> grids = new List<GameObject>();
    public List<GameObject> emptyGrids = new List<GameObject>();


    private void Awake()
    {
        ObjectManager.GridCreator = this;
    }
    void Start()
    {
        gridsparent = GameObject.FindGameObjectWithTag("GridsParent").transform;
        CreateGrids();
    }

    private void CreateGrids()
    {
        for (int i = 0; i < levelEditor.gridRow; i++)
        {
            for (int j = 0; j < levelEditor.gridColumn; j++)
            {
                tempGrid = Instantiate(gridPrefab);
                tempGrid.transform.SetParent(gridsparent);
                tempGrid.transform.position = Vector3.zero;
                tempGrid.transform.position = levelEditor.gridStartPoint + new Vector3(j, -i, 0)*tempGrid.transform.localScale.x*2;
                tempGrid.GetComponent<GridController>().pos = new Vector2(j, -i);
                grids.Add(tempGrid);
                emptyGrids.Add(tempGrid);
            }
        }
    }

}
