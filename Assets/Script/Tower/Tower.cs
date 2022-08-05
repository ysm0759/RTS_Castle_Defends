using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour ,IDamagable
{

    protected TowerInfo towerInfo; //소환할떄 그 데이터 세팅 해주기!

    /// <summary>
    /// 타워 만들어야 할 메뉴얼 
    /// 상위에 Object에 Tower를 박는다
    /// 상위  object에 TowerInfo를 컴포넌트로
    /// 하위에 공격 콜라이더를 박는다
    /// </summary>


    public void InitTower()
    {
        this.gameObject.SetActive(true);
        this.towerInfo.hp = towerInfo.maxHp;
    }



    public void Hit(float damage) // 쳐 맞기 구현
    {
 
    }


    public TowerInfo GetTowerInfo()
    {
        return towerInfo;
    }


    public void SetTowerInfo(TowerScriptable data)
    {
        towerInfo = GetComponent<TowerInfo>();
        towerInfo.SetData(data);

    }


}
