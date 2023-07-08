using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoneyTextFxController : MonoBehaviour
{
    private LevelManager levelManager;



    public void OpenEvent()
    {
        levelManager = ObjectManager.LevelManager;
        GetComponent<TMPro.TextMeshPro>().text = "+" + levelManager.goldValue.ToString();
        transform.DOMoveY(transform.position.y + 5, 0.75f).OnStepComplete(() => gameObject.SetActive(false));
    }



}
