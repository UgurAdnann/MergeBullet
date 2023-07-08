using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BoxController : MonoBehaviour
{
    #region Variables for General
    private PoolingManager poolingManager;
    #endregion
    #region Variables for Properties
    public int hp;
    public TMPro.TextMeshPro hpText;
    #endregion

    #region Variables for Hit
    private BulletController currentBulletController;
    private GameObject boxFx;
    private Vector3 startScale;
    public Rigidbody golds;
    #endregion

    void Start()
    {
        poolingManager = ObjectManager.PoolingManager;

        startScale = transform.localScale;
        SetHp(0);
    }

    private void SetHp(int hitValue)
    {
        hp -= hitValue;
        hpText.text = hp.ToString();
        hitAnim();
    }

    private void hitAnim()
    {
        DOTween.Complete(this);
        transform.DOScale(startScale * 1.2f, 0.05f).OnStepComplete(() => transform.DOScale(startScale, 0.05f));
    }

    private void CheckDeath()
    {
        if (hp <= 0)
        {
            transform.GetComponent<Collider>().enabled = false;
            transform.GetComponent<MeshRenderer>().enabled = false;
            golds.isKinematic = false;
            boxFx = poolingManager.useCubeDestroyFx();
            boxFx.transform.localScale *= 3;
            boxFx.GetComponent<PoolingObjectController>().UseObject(transform.position);
            hpText.gameObject.SetActive(false);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            currentBulletController = other.GetComponent<BulletController>();
            poolingManager.UseBulletDestroyFx().GetComponent<PoolingObjectController>().UseObject(currentBulletController.transform.position);
            currentBulletController.ReplaceQue();
            SetHp(currentBulletController.hitValue);
            hitAnim();
            CheckDeath();
        }
    }
}
