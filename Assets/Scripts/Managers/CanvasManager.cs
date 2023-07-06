using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    #region variables for General
    private GridCreator gridCreator;
    public LevelEditor levelEditor;
    private GameManager gameManager;
    public DataBase dataBase;
    #endregion

    #region Variables for Canvas
    public Text moneyText, bulletPriceText;
    private int money;
    public Color normalButtonColor, uselessButtonColor;
    public Button bulletButton;
    #endregion

    #region Variables for Bullet
    private GameObject tempBullet, tempgrid;
    private GridController gridController;
    private BulletController bulletController;
    #endregion

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
        SetMoneyText(0);
        SetBulletPrice();
    }

    #region Canvas Settings
    private void SetMoneyText(int addMoney)
    {
        money += addMoney;
        dataBase.money = money;

        moneyText.text = money.ToString();

        SetButtonColor();
    }

    private void SetBulletPrice()
    {
        bulletPriceText.text = levelEditor.bulletPrice.ToString();
    }

    private void SetButtonColor()
    {
        if (money >= levelEditor.bulletPrice)
            bulletButton.GetComponent<Image>().color = normalButtonColor;
        else
            bulletButton.GetComponent<Image>().color = uselessButtonColor;

    }
    #endregion

    #region BulletSettings
    public void AddBullet()
    {
        if (gridCreator.emptyGrids.Count > 0 && money >= levelEditor.bulletPrice)
        {
            int rndGrid = Random.Range(0, gridCreator.emptyGrids.Count);

            //Create Bullet
            tempBullet = Instantiate(levelEditor.bulletDatas[levelEditor.CreateingBulletType - 1].prefab);
            bulletController = tempBullet.GetComponent<BulletController>();
            tempgrid = gridCreator.emptyGrids[rndGrid];
            gridController = tempgrid.GetComponent<GridController>();

            //Set grid properties
            gridController.bulletType = levelEditor.bulletDatas[levelEditor.CreateingBulletType - 1].type;
            gridCreator.emptyGrids.Remove(tempgrid);
            gridController.gridSit = GridSit.Fill;

            //Set bullet properties
            tempBullet.transform.SetParent(tempgrid.transform);
            tempBullet.transform.localPosition = Vector3.zero;
            bulletController.gridNum = gridCreator.grids.IndexOf(tempgrid);
            bulletController.bulletType = levelEditor.CreateingBulletType;
            bulletController.hitValue = levelEditor.bulletDatas[levelEditor.CreateingBulletType - 1].hitValue;
            bulletController.hp = levelEditor.bulletDatas[levelEditor.CreateingBulletType - 1].hp;
            bulletController.pos = gridController.pos;
            bulletController.GetGridController();

            gameManager.bullets.Add(bulletController);

            SetMoneyText(-levelEditor.bulletPrice);
            gameManager.SaveSystem();
        }
    }
    #endregion
}
