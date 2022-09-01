using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorSkill3_1 : MonoBehaviour, IWarriorSkill
{



    // 각자으 ㅣ쿨타임 이런거는 여기 에 있는게 맞는거같아 
    // 데미지 , 기타등등 

    Targeting targetingSkill;

    [SerializeField]
    float skillScope; // 스킬 범위
    [SerializeField]
    float skillRange; // 내 사거리

    [SerializeField]
    float cool;
    bool isCoolDown;
    //코루틴 돌려야함

    bool skillDone;
    float waitTime = 0;  // 시전시간
    float invokeTime = 4f; //공격생성시간
    [SerializeField]
    private SkillPoint skillPoint;



    Animator anim;
    private void Start()
    {
        targetingSkill = GetComponent<Targeting>();
        isCoolDown = false;
        anim = GetComponent<Animator>();

        skillPoint = FindObjectOfType<SkillPoint>();
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
        targetingSkill.ShowSkill(skillScope, skillRange);
    }

    public void UseSkill()
    {
        targetingSkill.UseSkill(skillRange, this); //넘겨줘서 );
    }

    public void CanselSkill()
    {
        targetingSkill.ShowCancel();
    }
    public void StartCoolDown(Collider hit)
    {
        if (isCoolDown == false)
        {
            StartCoroutine(CoolDown(hit));
        }

    }


    IEnumerator CoolDown(Collider hit)
    {
        // 쿨타임 관련 처리
        float tmpCool = 0f;
        waitTime = 0;
        anim.SetBool("SkillDone", false);
        skillDone = false;
        transform.LookAt(hit.transform.position);
        
        KeyManager.instance.skill = Skill.SKILL_USING_CANT_MOVE;

        StartCoroutine(ComboStart(hit));
        transform.LookAt(hit.transform.position);

        Vector3 disatnceTmp = hit.transform.position - transform.position;
        while (tmpCool < cool)
        {
            isCoolDown = true;
            InGameSkillUI.instance.skillUI[2].fillAmount = (float)(tmpCool / cool);
            tmpCool += Time.deltaTime;
            waitTime += Time.deltaTime;

            if(disatnceTmp.magnitude > skillRange)
            {
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, hit.transform.position, 10 * Time.deltaTime);
            }

            if (waitTime >= invokeTime && false == skillDone)
            {
                anim.SetBool("SkillDone", true);
                skillDone = true;
                KeyManager.instance.skill = Skill.SKILL_DONE;

                //데미지 주고 
            }


            yield return null;
        }

        isCoolDown = false;
        yield return null;
    }

    IEnumerator ComboStart(Collider hit)
    {
        anim.SetTrigger("SkillNum3");
        transform.LookAt(hit.transform.position);
        yield return new WaitForSeconds(2f);
        transform.LookAt(hit.transform.position);
        anim.SetTrigger("SkillNum3Combo");
        yield return new WaitForSeconds(2f);


    }

}