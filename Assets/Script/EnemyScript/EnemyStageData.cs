using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;








[CreateAssetMenu(fileName = "EnumyStage.asset", menuName = "Stage/StageData")]
public class EnemyStageData : ScriptableObject
{


    [Serializable]
    public struct SerializeFieldDictionary
    {
        public GameObject PrefabObject;
        public int EA;
        public UnitDataScriptableObject unitDataScriptableObject;
    }
    public SerializeFieldDictionary[] EnemySpawnData;

    public int rewordMoney;
    public string stageLevel;
    public EnemyStageData NextStage;


}
