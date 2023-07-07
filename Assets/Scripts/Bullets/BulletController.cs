using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    #region Variables for General
    private GameManager gameManager;
    private GridCreator gridCreator;
    private LevelManager levelManager;
    private PoolingManager poolingManager;
    #endregion

    #region Variables for Properties
    public int bulletType, hitValue, gridNum, hp;
    public Vector2 pos;
    public bool isUnbeatable, isGameBullet, isGunBullet;
    #endregion

    #region Variables for Movement
    Vector3 worldPosition;
    private bool isOnTouch, isForwardFire, isRightTripleFire, isLeftTripleFire;
    private BulletController targetBulletController;
    [HideInInspector] public GridController currentGridController, targetGridController;
    public float bulletSpeed;
    Coroutine StopBullet = null;
    #endregion

    private void Start()
    {
        gridCreator = ObjectManager.GridCreator;
        gameManager = ObjectManager.GameManager;
        poolingManager = ObjectManager.PoolingManager;
    }

    private void OnEnable()
    {
        levelManager = ObjectManager.LevelManager;
        if (isGunBullet)
        {
            transform.localScale = Vector3.one * levelManager.bulletSize;
            StopBullet = StartCoroutine(WaitStopBullet());
        }
    }


    public void GetGridController()
    {
        currentGridController = transform.parent.GetComponent<GridController>();
    }

    void Update()
    {
        MoveObject();
        Fire();
    }

    #region Movement
    private void OnMouseDown()
    {
        if (!isGameBullet)
            isOnTouch = true;
    }
    private void OnMouseUp()
    {
        if (!isGameBullet)
        {
            isOnTouch = false;
            PutDown();
        }
    }

    private void MoveObject() //Drag control system
    {
        if (isOnTouch)
        {
            Plane plane = new Plane(Vector3.forward, 0);//Forward Z de hareket etmemesi için
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
            pos = targetGridController.pos;
        }
        else
        {
            if (bulletType.Equals(targetGridController.bulletType)) //Merge
            {
                targetBulletController = targetGridController.transform.GetChild(0).GetComponent<BulletController>();
                if (bulletType < gameManager.levelEditor.bulletDatas.Length)
                    gameManager.Merge(this, targetBulletController);
            }
            else //Move Ýnitial Place
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

    public void ForwardMovement(int hit)
    {
        hitValue = hit;
        isForwardFire = true;
        StartCoroutine(WaitStopBullet());
    }

    public void TripleRightMovement(int hit)
    {
        hitValue = hit;
        isRightTripleFire = true;
        StartCoroutine(WaitStopBullet());
    }

    public void TripleLeftMovement(int hit)
    {
        hitValue = hit;
        isLeftTripleFire = true;
        StartCoroutine(WaitStopBullet());
    }

    private void Fire()
    {
        if (isForwardFire)
            transform.Translate(transform.forward * -bulletSpeed * Time.deltaTime);

        else if (isRightTripleFire)
            transform.Translate(Vector3.Normalize(transform.forward + transform.right * 0.2f) * -bulletSpeed * Time.deltaTime);
        else if (isLeftTripleFire)
            transform.Translate(Vector3.Normalize(transform.forward - transform.right * 0.2f) * -bulletSpeed * Time.deltaTime);

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
            other.GetComponent<CharacterManager>().hitValue += hitValue;
            levelManager.StartCharacterMovement(this);
            other.GetComponent<CharacterManager>().isPlay = true;
            Destroy(this.gameObject);
        }
        if (other.CompareTag("BulletRange"))
        {
            ReplaceQue();
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

    public void ReplaceQue()
    {
        isForwardFire = false;
        isRightTripleFire = false;
        isLeftTripleFire = false;
        StopCoroutine(StopBullet);

        poolingManager.replacingBullet(transform.gameObject);
    }
    IEnumerator WaitStopBullet()
    {
        yield return new WaitForSeconds(3);
        ReplaceQue();
    }
    #endregion
}
