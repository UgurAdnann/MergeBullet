using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Variables for General
    private LevelManager levelManager;
    public DataBase dataBase;
    private PoolingManager poolingManager;
    #endregion
    #region Input
    public bool isTouch, isCanMove, isFirstTouch;
    private float xDifference;
    [HideInInspector] public float forwardSpeed, sideSpeed;
    [HideInInspector] public bool isStartPanelClose;
    public float initialSideSpeed, initialForwardSpeed;
    private Vector3 currentTouch, firstTouch, mousePos;
    #endregion

    #region Variables for Fire
    public bool isTripleShot;
    public int collectedMoney;
    #endregion


    private void Awake()
    {
        ObjectManager.PlayerManager = this;
    }

    void Start()
    {
        levelManager = ObjectManager.LevelManager;
        poolingManager = ObjectManager.PoolingManager;

        forwardSpeed = initialForwardSpeed;
        sideSpeed = initialSideSpeed;
    }

    void Update()
    {

        if (isCanMove)
        {
            if (Input.GetMouseButtonDown(0))
                isFirstTouch = true;

            if (isFirstTouch)
                InputListener();
        }
    }
    #region Input
    private void InputListener()
    {
        //Forward Movement
        if (isTouch)
            transform.Translate(transform.forward * forwardSpeed * Time.deltaTime);  //15

        //Mouse ile sað-sol yapma (Runner Oyunlar)
        if (isTouch)
        {
            currentTouch = Input.mousePosition;
            xDifference = (currentTouch.x - firstTouch.x) * 100f / Screen.width;
            xDifference = Mathf.Clamp(xDifference, -1, 1); //Clamp Side acceleration
            Vector3 newPos = transform.position + new Vector3(xDifference, 0, 0);
            transform.position = Vector3.Lerp(transform.position, new Vector3(newPos.x, transform.position.y, transform.position.z), sideSpeed * Time.deltaTime);// MoveSpeed = 11
            //Border Control
            if (transform.position.x <= -4)
                transform.position = new Vector3(-4, transform.position.y, transform.position.z);
            if (transform.position.x > 4)
                transform.position = new Vector3(4, transform.position.y, transform.position.z);
        }
        if (Input.GetMouseButton(0))
        {
            isTouch = true;
            firstTouch = Input.mousePosition;
        }


        if (Input.GetMouseButtonUp(0))
        {
            isTouch = false;
        }
    }
    #endregion

    public void StartMove()
    {
        isCanMove = true;
    }

    private void EndGameEvents()
    {
        isCanMove = false;
        isTouch = false;

        //Set Characters Anim
        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            transform.GetChild(0).GetChild(i).GetComponent<Animator>().SetBool("Run", false);
        }
        if (transform.position.z > dataBase.highScore)
            dataBase.highScore = transform.position.z;
        levelManager.OpenWinPanel(collectedMoney);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Golds"))
        {
            poolingManager.UsemoneyText().GetComponent<PoolingObjectController>().UseObject(transform.position + new Vector3(0, 2, 2));
            other.gameObject.SetActive(false);
            collectedMoney += levelManager.goldValue;
        }

        if (other.CompareTag("Box"))
        {
            EndGameEvents();
        }

        if (other.CompareTag("LevelEnd"))
        {
            EndGameEvents();
        }
    }
}
