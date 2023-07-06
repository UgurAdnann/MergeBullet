using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Input
    [HideInInspector] public bool isTouch, isCanMove;
    private float xDifference;
    [HideInInspector] public float forwardSpeed, sideSpeed;
    [HideInInspector] public bool isStartPanelClose;
    public float initialSideSpeed, initialForwardSpeed;
    private Vector3 currentTouch, firstTouch, mousePos;
    #endregion

    #region Variables for Fire
    public int distance;
    public bool isTriple;
    public float fireRate;
    #endregion


    private void Awake()
    {
        ObjectManager.PlayerManager = this;
    }

    void Start()
    {
        forwardSpeed = initialForwardSpeed;
        sideSpeed = initialSideSpeed;
    }

    void Update()
    {
        if (isCanMove)
            InputListener();
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
}
