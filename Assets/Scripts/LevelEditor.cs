using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="LevelEditor")]
public class LevelEditor : ScriptableObject
{
    [System.Serializable]
    public struct BulletData
    {
        public string name;
        public int hitValue;
        public int type;
        public GameObject prefab;
    }

    public BulletData[] bulletDatas;

    public int gridRow,gridColumn;
    public Vector3 gridStartPoint;
    public int money;
    public int bulletPrice,CreateingBulletType;
}
