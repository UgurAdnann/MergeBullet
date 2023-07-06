using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Vector3 targetDistance;
    public float followSpeed;
    public Transform target;
    private Transform firstTarget;
    public bool isFollow;

    private void Awake()
    {
        ObjectManager.CameraManager = this;
    }

    void Start()
    {
        firstTarget = GameObject.FindGameObjectWithTag("StartBulletsParent").transform;
        SetTarget(firstTarget);
    }

    public void SetTarget(Transform tr)
    {
        target = tr;

        targetDistance = transform.position - target.transform.position;
        isFollow = true;
    }
    void LateUpdate()
    {
        if (isFollow)
            transform.position = Vector3.Lerp(transform.position, targetDistance + target.transform.position, followSpeed * Time.deltaTime); //CamFollowDelta = 5
    }

}
