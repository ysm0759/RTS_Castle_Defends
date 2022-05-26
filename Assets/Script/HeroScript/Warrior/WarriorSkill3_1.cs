using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorSkill3_1 : MonoBehaviour, IWarriorSkill
{


    // 각자으 ㅣ쿨타임 이런거는 여기 에 있는게 맞는거같아 
    // 데미지 , 기타등등 

    Targeting targetingSkill;

    [SerializeField]
    float skillScope;
    [SerializeField]
    float skillRange;
    [SerializeField]
    float cool;
    bool isCoolDown;

    private void Start()
    {
        targetingSkill = GetComponent<Targeting>();
        isCoolDown = false;
    }


    public bool IsCoolDown()
    {


        return isCoolDown;
    }

    public void ShowRange()
    {
        targetingSkill.ShowSkill(skillScope, skillRange);
    }

    public void UseSkill()
    {
        targetingSkill.UseSkill(skillRange,this);
    }

    public void CanselSkill()
    {
        targetingSkill.ShowCancel();
    }


    public void StartCoolDown()
    {
        StartCoroutine(CoolDown());
    }



    IEnumerator CoolDown()
    {
        // 쿨타임 관련 처리
        float tmpCool = 0f;
        while (tmpCool < cool)
        {
            tmpCool += Time.deltaTime;
            isCoolDown = true;
            InGameSkillUI.instance.skillUI[2].fillAmount = (float)(tmpCool / cool);
            yield return null;
        }

        isCoolDown = false;
        yield return null;
    }

}