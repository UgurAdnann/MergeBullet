using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    #region Variables for General
    private PlayerManager playerManager;
    private PoolingManager poolingManager;
    private Animator animator;
    #endregion

    #region Variables for Shooting
    public bool isPlay;
    private BulletController tempbulletController;
    public Transform firePoint;
    public int hitValue;
    #endregion

    void Start()
    {
        playerManager = ObjectManager.PlayerManager;
        poolingManager = ObjectManager.PoolingManager;

        animator = GetComponent<Animator>();

        StartCoroutine(WaitFire());
    }

    void Update()
    {
        CheckAnim();
    }

    private void CheckAnim()
    {
        if (isPlay)
        {
            if (Input.GetMouseButtonDown(0))
                animator.SetTrigger("Run");
            if (Input.GetMouseButtonUp(0))
                animator.SetTrigger("Idle");
        }
    }

    private IEnumerator WaitFire()
    {
        if (playerManager.isTouch && isPlay)
        {
            tempbulletController = poolingManager.UseBullet().GetComponent<BulletController>();
            tempbulletController.GetComponent<PoolingObjectController>().UseObject(firePoint.position);
            tempbulletController.ForwardMovement(hitValue);

            yield return new WaitForSeconds(1);
        }
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(WaitFire());
    }
}
