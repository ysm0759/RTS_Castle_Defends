using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class WarriorSkill1_1 : MonoBehaviour, IWarriorSkill
{

    // 각자으 ㅣ쿨타임 이런거는 여기 에 있는게 맞는거같아 
    // 데미지 , 기타등등 

    Range rangeSkill;

    [SerializeField]
    float skillScope; // 스킬 범위
    [SerializeField] 
    float skillRange; // 내 사거리

    [SerializeField]
    float cool;
    bool isCoolDown;
    //코루틴 돌려야함 
    private void Start()
    {
        rangeSkill = GetComponent<Range>();
        isCoolDown = false;
    }
    
    // 이벤트를 만들고
    //
    public bool IsCoolDown()
    {


        //여기서 관리 해야함
        //return isCoolDown;
        return isCoolDown;
    }

    public void ShowRange()
    {
        rangeSkill.ShowSkill(skillScope, skillRange);
    }

    public void UseSkill()
    {
        rangeSkill.UseSkill(skillRange, this); //넘겨줘서 );
    }

    public void CanselSkill()
    {
        rangeSkill.ShowCancel();
    }

    public void StartCoolDown()
    {
        if(isCoolDown == false)  
        {
            StartCoroutine(CoolDown());
        }

    }


    IEnumerator CoolDown()
    {
        // 쿨타임 관련 처리
        float tmpCool = 0f;
        while (tmpCool < cool)
        {
            tmpCool += Time.deltaTime;
            isCoolDown = true;
            InGameSkillUI.instance.skillUI[0].fillAmount = (float)(tmpCool / cool);
            yield return null;
        }
  
        isCoolDown = false;
        yield return null;
    }

}


