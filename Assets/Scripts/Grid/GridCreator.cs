using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCreator : MonoBehaviour
{
    #region Variables for General
    public LevelEditor levelEditor;
    #endregion

    #region Variables for Grids
    public GameObject gridPrefab;
    private GameObject tempGrid;
    private Transform gridsparent;
    public List<GameObject> grids = new List<GameObject>();
    public List<GameObject> emptyGrids = new List<GameObject>();
    #endregion


    private void Awake()
    {
        ObjectManager.GridCreator = this;
        gridsparent = GameObject.FindGameObjectWithTag("GridsParent").transform;

        CreateGrids();
    }

    private void CreateGrids()
    {
        for (int i = 0; i < levelEditor.gridRow; i++)
        {
            for (int j = 0; j < levelEditor.gridColumn; j++)
            {
                //Create Grids
                tempGrid = Instantiate(gridPrefab);
                tempGrid.transform.SetParent(gridsparent);
                tempGrid.transform.position = Vector3.zero;
                tempGrid.transform.position = levelEditor.gridStartPoint + new Vector3(j, -i, 0)*tempGrid.transform.localScale.x*2;

                //Set grid properties
                grids.Add(tempGrid);
                tempGrid.GetComponent<GridController>().gridNum = grids.IndexOf(tempGrid);
                tempGrid.GetComponent<GridController>().pos = new Vector2(j, -i);
                emptyGrids.Add(tempGrid);
            }
        }
    }
}
