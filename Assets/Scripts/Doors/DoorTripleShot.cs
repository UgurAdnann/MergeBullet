using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTripleShot : DoorController
{
  private  PlayerManager playerManager;

    void Start()
    {
        playerManager = ObjectManager.PlayerManager;
        SetSpecialProperties();
        SetGeneralProperties();
    }

    private void SetSpecialProperties()
    {
        doorType = DoorType.TripleShot;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CloseCollider();
         
            playerManager.isTripleShot = true;
        }
    }
}
