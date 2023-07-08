using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : MonoBehaviour
{
    public GameObject cubeDestroyFx,bulletDestroyFx,bullet,moneyText;
    private GameObject tempCubeDestroyFx, tempBulletDestroyFx,tempBullet,tempMoneyText;

    public Queue<GameObject> cubeDestroyFxQue = new Queue<GameObject>();
    public Queue<GameObject> bulletDestroyFxQue = new Queue<GameObject>();
    public Queue<GameObject> bulletQue = new Queue<GameObject>();
    public Queue<GameObject> moneyTextQue = new Queue<GameObject>();

    public int cubeDestroyFxCount, bulletDestroyFxCount,bulletCount,moneyTextCount;

    private void Awake()
    {
        ObjectManager.PoolingManager = this;
    }

    private void Start()
    {
        CubeFxPooling();
        BulletFxPooling();
        BulletPooling();
        MoneyTextPooling();
    }

    #region CubePoolingEvents

    private void CubeFxPooling()
    {
        for (int i = 0; i < cubeDestroyFxCount; i++)
        {
            tempCubeDestroyFx = Instantiate(cubeDestroyFx);
            tempCubeDestroyFx.transform.SetParent(transform.GetChild(0)); //CubeDestroyFx Parent
            tempCubeDestroyFx.transform.localPosition = Vector3.zero;
            tempCubeDestroyFx.GetComponent<PoolingObjectController>().poolingType = PoolingType.CubeDestroyFx;
            cubeDestroyFxQue.Enqueue(tempCubeDestroyFx);
        }
    }

    public GameObject useCubeDestroyFx()
    {
        if (cubeDestroyFxQue.Count <= 1)
            CubeFxPooling();
        return cubeDestroyFxQue.Dequeue();
    }

    public void replacingCubeDestroyFx(GameObject fx)
    {
        fx.SetActive(false);
        fx.transform.localPosition = Vector3.zero;
        cubeDestroyFxQue.Enqueue(fx);
    }
   
    #endregion

    #region Bullet Pooling Events
    private void BulletFxPooling()
    {
        for (int i = 0; i < bulletDestroyFxCount; i++)
        {
            tempBulletDestroyFx = Instantiate(bulletDestroyFx);
            tempBulletDestroyFx.transform.SetParent(transform.GetChild(1)); //BulletDestroyFx Parent
            tempBulletDestroyFx.transform.localPosition = Vector3.zero;
            tempBulletDestroyFx.GetComponent<PoolingObjectController>().poolingType = PoolingType.BulletDestroyFx;

            bulletDestroyFxQue.Enqueue(tempBulletDestroyFx);
        }
    }

    public GameObject UseBulletDestroyFx()
    {
        if (bulletDestroyFxQue.Count <= 1)
            BulletFxPooling();
        return bulletDestroyFxQue.Dequeue();
    }

    public void replacingBulletDestroyFx(GameObject fx)
    {
        fx.SetActive(false);
        fx.transform.localPosition = Vector3.zero;
        bulletDestroyFxQue.Enqueue(fx);
    }

     private void BulletPooling()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            tempBullet = Instantiate(bullet);
            tempBullet.transform.SetParent(transform.GetChild(2)); //Bullet Parent
            tempBullet.transform.localPosition = Vector3.zero;
            tempBullet.GetComponent<PoolingObjectController>().poolingType = PoolingType.Bullet;

            bulletQue.Enqueue(tempBullet);
        }
    }

    public GameObject UseBullet()
    {
        if (bulletQue.Count <= 1)
            BulletPooling();
        return bulletQue.Dequeue();
    }

    public void replacingBullet (GameObject go)
    {
        go.SetActive(false);
        go.transform.localPosition = Vector3.zero;
        bulletQue.Enqueue(go);
    }

    #endregion

    #region MoneyPoolingEvents
    private void MoneyTextPooling()
    {
        for (int i = 0; i < cubeDestroyFxCount; i++)
        {
            tempMoneyText = Instantiate(moneyText);
            tempMoneyText.transform.SetParent(transform.GetChild(3)); //CubeDestroyFx Parent
            tempMoneyText.transform.localPosition = Vector3.zero;
            tempMoneyText.GetComponent<PoolingObjectController>().poolingType = PoolingType.MoneyText;
            moneyTextQue.Enqueue(tempMoneyText);
        }
    }

    public GameObject UsemoneyText()
    {
        if (moneyTextQue.Count <= 1)
            MoneyTextPooling();
        return moneyTextQue.Dequeue();
    }

    public void replacingMoneyText(GameObject go)
    {
        go.SetActive(false);
        go.transform.localPosition = Vector3.zero;
        moneyTextQue.Enqueue(go);
    }
    #endregion
}
