using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    #region Variables for General
    private GameManager gameManager;
    private GridCreator gridCreator;
    private LevelManager levelManager;
    #endregion

    #region Variables for Properties
    public int bulletType, hitValue, gridNum,hp;
    public Vector2 pos;
    public bool isUnbeatable;
    #endregion

    #region Variables for Movement
    Vector3 worldPosition;
    private bool isOnTouch;
    private BulletController targetBulletController;
    [HideInInspector] public GridController currentGridController, targetGridController;
    #endregion

    private void Start()
    {
        gridCreator = ObjectManager.GridCreator;
        gameManager = ObjectManager.GameManager;
    }

    private void OnEnable()
    {
        levelManager = ObjectManager.LevelManager;
        
    }


    public void GetGridController()
    {
        currentGridController = transform.parent.GetComponent<GridController>();
    }

    void Update()
    {
        MoveObject();
    }

    #region Movement
    private void OnMouseDown()
    {
        isOnTouch = true;
    }
    private void OnMouseUp()
    {
        isOnTouch = false;
        PutDown();
    }

    private void MoveObject() //Drag control system
    {
        if (isOnTouch)
        {
            Plane plane = new Plane(Vector3.forward, 0);//Forward Z de hareket etmemesi i�in
            float distance;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out distance))
            {
                worldPosition = ray.GetPoint(distance);
            }
            transform.position = Vector3.MoveTowards(transform.position, worldPosition, 30 * Time.deltaTime);
        }
    }

    private void PutDown()
    {
        currentGridController = transform.parent.GetComponent<GridController>();
        if (targetGridController.gridSit.Equals(GridSit.Empty)) //Empty Movement
        {
            //Grid Events
            ResetGrid();
            gridCreator.emptyGrids.Remove(targetGridController.gameObject);
            targetGridController.gridSit = GridSit.Fill;
            targetGridController.bulletType = bulletType;

            //Bullet Events
            transform.SetParent(targetGridController.transform);
            transform.localPosition = Vector3.zero;
        }
        else
        {
            if (bulletType.Equals(targetGridController.bulletType)) //Merge
            {
                targetBulletController = targetGridController.transform.GetChild(0).GetComponent<BulletController>();
                if (bulletType < gameManager.levelEditor.bulletDatas.Length)
                    gameManager.Merge(this, targetBulletController);
            }
            else //Move �nitial Place
                transform.localPosition = Vector3.zero;
        }

        gridNum = transform.parent.GetSiblingIndex();
        gameManager.SaveSystem();
    }

    public void ResetGrid()
    {
        currentGridController.gridSit = GridSit.Empty;
        gridCreator.emptyGrids.Add(currentGridController.gameObject);
        currentGridController.bulletType = 0;
    }
    #endregion

    #region Collision
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Grid"))
        {
            targetGridController = other.GetComponent<GridController>();
        }
        if (other.CompareTag("Character"))
        {
            levelManager.StartCharacterMovement();
            other.GetComponent<CharacterManager>().isPlay = true;
            Destroy(this.gameObject);
        }
    }
    #endregion

    #region Destroy Events
    public void DestroyEvent()
    {
        if (!isUnbeatable)
        {
            Destroy(this.gameObject);
        }
    }
    #endregion
}
