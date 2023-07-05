using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataBase")]
public class DataBase : ScriptableObject
{
    [System.Serializable]
    public struct BulletSave
    {
        public int type;
        public int GridNum;
    }

    public List<BulletSave>  bulletSaves=new List<BulletSave>();
    [Header( "Click R to Reset Data")]
    public int money;
}