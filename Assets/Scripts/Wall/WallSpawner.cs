using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawner : MonoBehaviour
{
    #region Variables for General
    private LevelManager levelManager;
    private PlayerManager playerManager;
    #endregion
    #region Variables for Character
    public GameObject character;
    public Transform charactersparent;
    #endregion
    #region Variables for Walls
    public GameObject[] walls;
    public int column;
    [HideInInspector] public int row;
    private GameObject tempWall;
    private int rndWallValue;
    public Vector3 startPos;
    public Transform wallsParent;
    #endregion

    private void Awake()
    {
        ObjectManager.wallSpawner = this;
        wallsParent = GameObject.FindGameObjectWithTag("WallsParent").transform;
    }


    #region WallSpawn
    //triggerred from GameBulletSpawnerSc
    public void SpawnWall()
    {
        levelManager = ObjectManager.LevelManager;
        playerManager = ObjectManager.PlayerManager;

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                //Create Wall
                rndWallValue = Random.Range(0, 100);
                if (rndWallValue < 40)
                    tempWall = Instantiate(walls[0]);
                else if (rndWallValue < 70)
                    tempWall = Instantiate(walls[1]);
                else
                    tempWall = Instantiate(walls[2]);

                //Set wall Pos
                tempWall.transform.SetParent(wallsParent);
                tempWall.transform.position = startPos + new Vector3(j * 2, 0, i * 1.5f);

                //SetCharacters
                if (i == row - 1)
                {
                    GameObject tempChar = Instantiate(character);
                    tempChar.transform.SetParent(charactersparent);
                    tempChar.transform.position = tempWall.transform.position + Vector3.forward * 10;
                    levelManager.characterList.Add(tempChar.GetComponent<CharacterManager>());
                    playerManager.transform.position = new Vector3(0, 0, tempWall.transform.position.z + 20);
                }

            }
        }
    }
    #endregion
}
