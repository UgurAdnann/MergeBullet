using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class LevelManager : MonoBehaviour
{
    #region Variables for General
    public DataBase dataBase;
    private PlayerManager playerManager;
    private CameraManager cameraManager;
    private GameManager gameManager;
    #endregion

    #region Variables for CharacterMovement
    private bool isStartCharacterMovement;
    public List<CharacterManager> characterList = new List<CharacterManager>();
    public Transform camPos;
    #endregion

    #region Variables for Fire
    private StartBullets startBullets;
    private int GunNum;
    public float bulletSize, fireRate;
    #endregion

    #region Variables for Level Design
   [HideInInspector] public Vector3 playerStartPos;

    public int boxNum, boxDistance;

    public int doorNum, doorDistance;
    private int doorRndPos,rndDoor;
    public GameObject[] doors;
    private GameObject currentDoor;
    public GameObject finishLine;
    #endregion

    private void Awake()
    {
        ObjectManager.LevelManager = this;
    }

    private void Start()
    {
        playerManager = ObjectManager.PlayerManager;
        cameraManager = ObjectManager.CameraManager;
        gameManager = ObjectManager.GameManager;

    }

    public void StartGame()
    {
        DataManager.SaveData(dataBase);
        if (gameManager.bullets.Count > 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }


    #region StartCharacter Movement
    public void StartCharacterMovement(BulletController sc)
    {
        if (!isStartCharacterMovement)
        {
            isStartCharacterMovement = true;
            StartCoroutine(WaitCharacterMovement());
        }
    }

    private IEnumerator WaitCharacterMovement()
    {
        cameraManager.isFollow = false;
        yield return new WaitForSeconds(0.75f);
        //Set cam Settings
        cameraManager.transform.DOMove(camPos.position, 0.95f);
        cameraManager.transform.DORotate(camPos.transform.eulerAngles, 0.75f);

        //Set Bullet Settings
        startBullets = GameObject.FindGameObjectWithTag("StartBulletsParent").GetComponent<StartBullets>();
        startBullets.isMoveForward = false;
        startBullets.gameObject.SetActive(false);

        //Set Chaarcter Settings
        for (int i = characterList.Count - 1; i >= 0; i--)
        {
            if (characterList[i].isPlay)
            {
                characterList[i].transform.SetParent(playerManager.transform.GetChild(0));
                characterList[i].transform.DOMove(playerManager.transform.GetChild(1).GetChild(GunNum).position, 0.95f);
                GunNum++;
                characterList[i].GetComponent<Collider>().enabled = false;
            }
        }
        yield return new WaitForSeconds(1);
        cameraManager.SetTarget(playerManager.transform);
        playerManager.StartMove();
    }
    #endregion

    #region Level Design
    public void DesignLevel(Vector3 playerPos)
    {
        playerStartPos = playerPos;
        SetDoors();
    }

    private void SetDoors()
    {
        for (int i = 0; i < doorNum; i++)
        {
            rndDoor = Random.Range(0, doors.Length);
            currentDoor = Instantiate( doors[rndDoor]);

            doorRndPos = Random.Range(0, 2);

            if (doorRndPos == 0) //Put Left
                currentDoor.transform.position = new Vector3(-2.5f, -2.25f, playerStartPos.z+doorDistance + (i * doorDistance));
            if (doorRndPos == 1) //Put Right
                currentDoor.transform.position = new Vector3(2.5f, -2.25f, playerStartPos.z+doorDistance + (i * doorDistance));
        }

        finishLine.transform.position = new Vector3(0, 0.5f, currentDoor.transform.position.z + doorDistance); 
    }

    #endregion
}
