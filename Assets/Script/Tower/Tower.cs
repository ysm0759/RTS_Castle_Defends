using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour ,IDamagable
{

    protected TowerInfo towerInfo; //소환할떄 그 데이터 세팅 해주기!
    [SerializeField] TowerScriptable tmp;

    private GameObject burning;
    private GameObject destroy;
    /// <summary>
    /// 타워 만들어야 할 메뉴얼 
    /// 상위에 Object에 Tower를 박는다
    /// 상위  object에 TowerInfo를 컴포넌트로
    /// 하위에 공격 콜라이더를 박는다
    /// </summary>
    bool isBurn;
    bool isDestroy;

    float burnHp;

    private void Awake()
    {
        if (tmp != null)
        {
            SetTowerInfo(tmp);
            InitTower();
        }
    }

    public void InitTower()
    {
        this.gameObject.SetActive(true);
        this.towerInfo.hp = towerInfo.maxHp;
        burnHp = towerInfo.maxHp * 0.5f;
        Debug.Log(burnHp);
        isBurn = false;
        isDestroy = false;
    }



    public void Hit(float damage) // 쳐 맞기 구현
    {
        towerInfo.hp -= damage;

        if (towerInfo.hp <= burnHp && isBurn == false)
        {
            isBurn = true;
            burning = ObjectPool.GetObject("burning");
            burning.transform.position = transform.position;
        }
        else if (towerInfo.hp <= 0 && isDestroy == false)
        {

            this.gameObject.SetActive(false);
            ObjectPool.ReturnObject("burning", burning);
            destroy = ObjectPool.GetObject("destroy");
            destroy.transform.position = transform.position;
        }

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
