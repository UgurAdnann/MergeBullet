using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    private GridCreator gridCreator;

    public LevelEditor levelEditor;
    private GameManager gameManager;
    public DataBase dataBase;
    public Text moneyText, bulletPriceText;
    private int money;

    private GameObject tempBullet, tempgrid;
    private GridController gridController;
    private BulletController bulletController;

    private void OnEnable()
    {
        EventManager.SetMoneyText += SetMoneyText;
    }

    private void OnDisable()
    {
        EventManager.SetMoneyText -= SetMoneyText;
    }


    private void Start()
    {
        gridCreator = ObjectManager.GridCreator;
        gameManager = ObjectManager.GameManager;

        money = dataBase.money;
        SetMoneyText();
        SetBulletPrice();
    }

    private void SetMoneyText()
    {
        moneyText.text = money.ToString();
    }

    private void SetBulletPrice()
    {
        bulletPriceText.text = levelEditor.bulletPrice.ToString();
    }

    public void AddBullet()
    {
        if (gridCreator.emptyGrids.Count > 0 && money >= levelEditor.bulletPrice)
        {
            int rndGrid = Random.Range(0, gridCreator.emptyGrids.Count);

            tempBullet = Instantiate(levelEditor.bulletDatas[levelEditor.CreateingBulletType - 1].prefab);
            bulletController = tempBullet.GetComponent<BulletController>();
            tempgrid = gridCreator.emptyGrids[rndGrid];
            gridController = tempgrid.GetComponent<GridController>();

            gridController.bulletType = levelEditor.bulletDatas[levelEditor.CreateingBulletType - 1].type;
            gridCreator.emptyGrids.Remove(tempgrid);
            gridController.gridSit = GridSit.Fill;

            tempBullet.transform.SetParent(tempgrid.transform);
            tempBullet.transform.localPosition = Vector3.zero;
            bulletController.gridNum = gridCreator.grids.IndexOf(tempgrid);
            bulletController.bulletType = levelEditor.CreateingBulletType;
            bulletController.hitValue = levelEditor.bulletDatas[levelEditor.CreateingBulletType - 1].hitValue;

            gameManager.bullets.Add(bulletController);


            money -= levelEditor.bulletPrice;
            dataBase.money = money;
            SetMoneyText();
            gameManager.SaveSystem();
        }
    }
}
