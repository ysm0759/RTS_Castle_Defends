﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSmash : MonoBehaviour, IWarriorSkill
{



    // 각자으 ㅣ쿨타임 이런거는 여기 에 있는게 맞는거같아 
    // 데미지 , 기타등등 

    Range rangeSkill;

    [SerializeField]
    float skillScope; // 스킬 범위
    [SerializeField]
    float skillRange; // 내 사거리
    [SerializeField]
    float damage = 10;


    [SerializeField]
    float cool;
    bool isCoolDown;
    //코루틴 돌려야함

    bool skillDone;
    float waitTime = 0;  // 시전시간
    float invokeTime = 2.0f; //공격생성시간
    float speed = 0;
    [SerializeField]
    private SkillPoint skillPoint;


    float moveDelay = 0;
    float moveDelayTime = 0.5f;


    Animator anim;
    Collider[] hit;


    [SerializeField] GameObject effects;
    
    private void Start()
    {
        rangeSkill = GetComponent<Range>();
        isCoolDown = false;
        anim = GetComponent<Animator>();

        skillPoint = FindObjectOfType < SkillPoint>();

        hit = new Collider[40];
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
    public void StartCoolDown(Collider hit = null)
    {
        if (isCoolDown == false)
        {
            StartCoroutine(CoolDown());
        }

    }


    IEnumerator CoolDown()
    {
        // 쿨타임 관련 처리
        float tmpCool = 0f;
        waitTime = 0;
        moveDelay = 0;
        anim.SetTrigger("SkillNum2");
        anim.SetBool("SkillDone", false);
        skillDone = false;

        speed = Vector3.Distance(skillPoint.transform.position, transform.position) / (invokeTime - moveDelayTime);
        transform.LookAt(skillPoint.transform.position);
        KeyManager.instance.skill = Skill.SKILL_USING_CANT_MOVE;

        while (tmpCool < cool)
        {

            tmpCool += Time.deltaTime;
            isCoolDown = true;
            InGameSkillUI.instance.skillUI[1].fillAmount = (float)(tmpCool / cool);
            waitTime += Time.deltaTime;
            moveDelay += Time.deltaTime;

            if(false == skillDone && moveDelay >= moveDelayTime)
            {
                transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.Self);
            }

            if(waitTime >= invokeTime && false == skillDone)
            {
                anim.SetBool("SkillDone", true);
                skillDone = true;
                KeyManager.instance.skill = Skill.SKILL_DONE;



                int count = Physics.OverlapSphereNonAlloc(transform.position, skillRange, hit, LayerMask.GetMask("Enemy"));
                GameObject effects = ObjectPool.GetObject("warriorRangeEffect");
                effects.SetActive(true);
                effects.transform.position = skillPoint.transform.position + (Vector3.up * 3);
                for (int i = 0; i < count; i++)
                {
                    hit[i].GetComponent<IDamagable>().Hit(damage);
                }

            }


            yield return null;
        }

        isCoolDown = false;
        yield return null;
    }


    private void OnDisable()
    {
        isCoolDown = false;
        InGameSkillUI.instance.skillUI[1].fillAmount = 1f;
        KeyManager.instance.skill = Skill.SKILL_NONE;

    }
}



