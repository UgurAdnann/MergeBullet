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

        StartCoroutine(WaitFire());
    }

    void Update()
    {
        CheckAnim();
    }

    private void CheckAnim()
    {
        if (playerManager.isCanMove && isPlay)
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
            CreateBullet();
            tempbulletController.ForwardMovement(hitValue);

            if (playerManager.isTripleShot)
            {
                //Right Bullet
                CreateBullet();
                tempbulletController.TripleRightMovement(hitValue);

                //LeftBullet Bullet
                CreateBullet();
                tempbulletController.TripleLeftMovement(hitValue);

            }


            yield return new WaitForSeconds(levelManager.fireRate);
        }
        yield return new WaitForSeconds(0.05f);
        StartCoroutine(WaitFire());
    }

    private void CreateBullet()
    {
        tempbulletController = poolingManager.UseBullet().GetComponent<BulletController>();
        tempbulletController.GetComponent<PoolingObjectController>().UseObject(firePoint.position);
        tempbulletController.transform.GetChild(0).gameObject.SetActive(false);
        tempbulletController.transform.GetChild(1).gameObject.SetActive(false);
    }
}
