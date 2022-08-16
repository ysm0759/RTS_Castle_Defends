using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// TODO :: Tower 설치 UI

public class Tower : MonoBehaviour, IDamagable
{

    protected TowerInfo towerInfo; //소환할떄 그 데이터 세팅 해주기!
    [SerializeField] List<GameObject> towerModel;

    private AttackTower attackTower;
    private GameObject burning;
    private GameObject destroy;
    /// <summary>
    /// 타워 만들어야 할 메뉴얼 
    /// 상위에 Object에 Tower를 박는다
    /// 상위  object에 TowerInfo를 컴포넌트로
    /// 하위에 공격 콜라이더를 박는다
    /// 콜라이더에 Trigger를 체크
    /// </summary>


    /// 기본 제공 타워
    /// 캐술 , 게이트 등
    /// 스크립트를 붙인다 컴포넌트에
    [SerializeField] TowerScriptable dafaultTower;

    public bool isSetTower;
    bool isBurn;
    bool isDestroy;
    float burnHp;

    private void Awake()
    {
        attackTower = GetComponentInChildren<AttackTower>();
        isSetTower = false;

        // 기본 타워 생성 Component에 Tower Scriptable 컴포넌트를 붙여준다
        if (dafaultTower != null)
        {
            towerInfo = GetComponent<TowerInfo>();
            towerInfo.SetData(dafaultTower);
            InitTower();
        }
    }

    public void InitTower()
    {
        this.gameObject.SetActive(true);
        this.towerInfo.hp = towerInfo.maxHp;
        burnHp = towerInfo.maxHp * 0.5f;
        isBurn = false;
        isDestroy = false;
        isSetTower = true;

        if (attackTower != null)
        {
            attackTower.SetData();
        }

    }



    public void Hit(float damage) // 쳐 맞기 구현
    {
        towerInfo.hp -= damage;

        if (towerInfo.hp <= burnHp && isBurn == false)
        {
            isBurn = true;
            burning = ObjectPool.GetObject("burning");
            burning.transform.position = transform.position;
            burning.SetActive(true);
        }
        else if (towerInfo.hp <= 0 && isDestroy == false)
        {
            this.gameObject.SetActive(false);
            ObjectPool.ReturnObject("burning", burning);
            destroy = ObjectPool.GetObject("destroy");
            destroy.transform.position = transform.position;
            destroy.SetActive(true);
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
        SetModel(data);
        InitTower();
    }


    public void SetModel(TowerScriptable data)
    {
        for (int i = 0; i < towerModel.Count; i++)
        {
            if (data.modelIndex == i)
                this.towerModel[i].SetActive(true);
            else
                this.towerModel[i].SetActive(false);
        }
    }

    private void Update()
    {
        test();
    }

    public void test()
    {
        //Renderer ObstacleRenderer;
        //ObstacleRenderer = towerModel[0].gameObject.GetComponentInChildren<Renderer>();

        //Material Mat = ObstacleRenderer.material;

        //Color matColor = Mat.color;

        //matColor.a = 0.5f;

        //Mat.color = matColor;

    }
}