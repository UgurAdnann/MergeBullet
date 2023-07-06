using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingObjectController : MonoBehaviour
{
    public PoolingType poolingType;
    private PoolingManager poolingManager;

    void Start()
    {
        poolingManager = ObjectManager.PoolingManager;
    }


    public void UseObject(Vector3 pos)
    {
        transform.position = pos;
        gameObject.SetActive(true);
        if (poolingType != PoolingType.Bullet)
            StartCoroutine(WaitCloseObject());
    }

    private IEnumerator WaitCloseObject()
    {
        yield return new WaitForSeconds(1);
        if (poolingType.Equals(PoolingType.CubeDestroyFx))
            poolingManager.replacingCubeDestroyFx(transform.gameObject);
        else if (poolingType.Equals(PoolingType.BulletDestroyFx))
            poolingManager.replacingBulletDestroyFx(transform.gameObject);
        else if (poolingType.Equals(PoolingType.Bullet))
            poolingManager.replacingBullet(transform.gameObject);
    }
}
