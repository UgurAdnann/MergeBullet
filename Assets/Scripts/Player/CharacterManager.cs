using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    #region Variables for General
    private PlayerManager playerManager;
    private LevelManager levelManager;
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
        levelManager = ObjectManager.LevelManager;

        animator = GetComponent<Animator>();
        animator.SetBool("Idle", false);

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
                animator.SetBool("Run", true);
            if (Input.GetMouseButtonUp(0))
                animator.SetBool("Run", false);
        }
    }

    private IEnumerator WaitFire()
    {
        if (playerManager.isTouch && isPlay)
        {
            tempbulletController = poolingManager.UseBullet().GetComponent<BulletController>();
            tempbulletController.GetComponent<PoolingObjectController>().UseObject(firePoint.position);
            tempbulletController.ForwardMovement(hitValue);

            if (playerManager.isTripleShot)
            {
                //Right Bullet
                tempbulletController = poolingManager.UseBullet().GetComponent<BulletController>();
                tempbulletController.GetComponent<PoolingObjectController>().UseObject(firePoint.position);
                tempbulletController.TripleRightMovement(hitValue);

                //LeftBullet Bullet
                tempbulletController = poolingManager.UseBullet().GetComponent<BulletController>();
                tempbulletController.GetComponent<PoolingObjectController>().UseObject(firePoint.position);
                tempbulletController.TripleLeftMovement(hitValue);

            }


            yield return new WaitForSeconds(levelManager.fireRate);
        }
        yield return new WaitForSeconds(0.05f);
        StartCoroutine(WaitFire());
    }
}
