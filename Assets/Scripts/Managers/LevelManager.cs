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
    #endregion

    #region Variables for CharacterMovement
    private bool isStartCharacterMovement;
    private StartBullets startBullets;
    public List<CharacterManager> characterList = new List<CharacterManager>();
    private int GunNum;
    #endregion

    private void Awake()
    {
        ObjectManager.LevelManager = this;
    }

    private void Start()
    {
        playerManager = ObjectManager.PlayerManager;
        cameraManager = ObjectManager.CameraManager;

    }

    public void StartGame()
    {
        DataManager.SaveData(dataBase);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }


    #region StartCharacter Movement
    public void StartCharacterMovement()
    {
        if (!isStartCharacterMovement)
        {
            isStartCharacterMovement = true;
            StartCoroutine(WaitCharacterMovement());
        }
    }

    private IEnumerator WaitCharacterMovement()
    {
        yield return new WaitForSeconds(0.2f);
            startBullets = GameObject.FindGameObjectWithTag("StartBulletsParent").GetComponent<StartBullets>();
        startBullets.isMoveForward = false;
        startBullets.gameObject.SetActive(false);
        cameraManager.isFollow = false;
        for (int i = characterList.Count - 1; i >= 0; i--)
        {
            if (characterList[i].isPlay)
            {
                characterList[i].transform.SetParent(playerManager.transform.GetChild(0));
                characterList[i].transform.DOMove(playerManager.transform.GetChild(1).GetChild(GunNum).position, 0.95f);
                GunNum++;
            }
        }
    }
    #endregion
}
