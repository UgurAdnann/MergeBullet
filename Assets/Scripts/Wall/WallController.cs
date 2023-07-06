using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WallController : MonoBehaviour
{

    #region Variables for General
    private PoolingManager poolingManager;
    #endregion

    #region Variables for Collision
    private BulletController currentBulletController;
    public int damage;
    #endregion

    private void Start()
    {
        poolingManager = ObjectManager.PoolingManager;
    }

    #region CollisionEvent
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            currentBulletController = other.GetComponent<BulletController>();
            BulletCollisionEvent();
        }
    }

    private void BulletCollisionEvent()
    {
        currentBulletController.hp -= damage;
        if (currentBulletController.hp <= 0)
            currentBulletController.DestroyEvent();

        poolingManager.useCubeDestroyFx().GetComponent<PoolingObjectController>().UseObject(transform.position-Vector3.forward*transform.localScale.z/2);
        transform.DOScale(transform.localScale * 1.3f, 0.015f).SetEase(Ease.Linear).OnStepComplete(() => transform.DOScale(Vector3.zero, 0.15f));
        GetComponent<Collider>().enabled = false;

    }
    #endregion
}
