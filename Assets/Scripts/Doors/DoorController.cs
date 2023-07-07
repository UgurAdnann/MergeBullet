using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorController : MonoBehaviour
{
    #region Variables for Door Properties
    public DoorType doorType;
    public string doorName;
    public bool isPositive;
    private int rnd;
    private bool isFirstColor;
    #endregion

    #region Variables for  door Object
    public TMPro.TextMeshPro valueText;
    public TMPro.TextMeshPro nameText;
    public GameObject textBG, doorBG;
    public BulletController tempBulletController;
    #endregion

    public void SetGeneralProperties()
    {
        nameText.text = doorName;
    }

    public void SetPositiveNegativeDoors(Material negativeTextBGMat,Material negativeDoorBGMat,Material positiveTextBGMat,Material positiveDoorBGMat)
    {
        rnd = Random.Range(0, 2);
        if (rnd == 0) //Negative
        {
            isPositive = false;
            textBG.GetComponent<Renderer>().sharedMaterial = negativeTextBGMat;
            doorBG.GetComponent<Renderer>().sharedMaterial = negativeDoorBGMat;
        }
        else //Positive
        {
            SetPositiveColor(positiveTextBGMat, positiveDoorBGMat);
        }
    }

    public void SetPositiveColor (Material positiveTextBGMat, Material positiveDoorBGMat)
    {
        isPositive = true;
        textBG.GetComponent<Renderer>().sharedMaterial = positiveTextBGMat;
        doorBG.GetComponent<Renderer>().sharedMaterial = positiveDoorBGMat;
    }
   
    public int SetInitialValues(int min,int max,int  value)
    {
        value = Random.Range(min, max);
        if (!isPositive)
            value = 0 - value;
        valueText.text = value.ToString();
        return value;
    }

    public void ValueTextAnim()
    {
        DOTween.Complete(this);
        valueText.transform.DOPunchScale(valueText.transform.localScale * 1.1f,0.1f);
    }
}
