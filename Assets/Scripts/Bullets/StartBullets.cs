using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBullets : MonoBehaviour
{
    #region Variables for Movement
    public bool isMoveForward;
    public float forwardSpeed;
    #endregion

    void Start()
    {
        
    }

    void Update()
    {
        MoveForward();
    }

    private void MoveForward()
    {
        if(isMoveForward)
        transform.Translate(transform.forward * forwardSpeed * Time.deltaTime);
    }
}
