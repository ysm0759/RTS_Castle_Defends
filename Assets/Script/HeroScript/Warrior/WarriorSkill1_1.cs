using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class WarriorSkill1_1 : MonoBehaviour, IWarriorSkill
{

    // 각자으 ㅣ쿨타임 이런거는 여기 에 있는게 맞는거같아 
    // 데미지 , 기타등등 

    Immediate immediateSkill;

    [SerializeField]
    float skillScope; // 스킬 범위
    [SerializeField] 
    float skillRange; // 내 사거리
    [SerializeField]
    float damage = 100;

    [SerializeField]
    float cool;
    bool isCoolDown;
    Animator anim;
    //코루틴 돌려야함

    float waitTime = 0;  // 시전시간
    float invokeTime = 4.0f; //끝나는시간

    float attackDelayTime = 0.3f;
    float attackDelay = 0;

    bool skillDone;

    private Collider[] hit;

    [SerializeField]
    private GameObject effect;


    private void Start()
    {
        effect.SetActive(false);
        immediateSkill = GetComponent<Immediate>();
        isCoolDown = false;
        anim = GetComponent<Animator>();
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
        immediateSkill.ImmediateSkill(this);
        //immediateSkill.ShowSkill(skillScope, skillRange);
    }

    public void UseSkill()
    {
        immediateSkill.UseSkill(skillRange, this); //넘겨줘서 );
    }

    public void CanselSkill()
    {
        immediateSkill.ShowCancel();
    }

    public void StartCoolDown(Collider hit = null)
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
        attackDelay = 0;
        waitTime = 0;

        anim.SetBool("SkillNum1", true);
        anim.SetBool("SkillDone", false);
        skillDone = false;
        effect.SetActive(true);


        KeyManager.instance.skill = Skill.SKILL_USING_CAN_MOVE;

        while (tmpCool < cool)
        {

            tmpCool += Time.deltaTime;
            isCoolDown = true;
            InGameSkillUI.instance.skillUI[0].fillAmount = (float)(tmpCool / cool);
            waitTime += Time.deltaTime;
            attackDelay += Time.deltaTime;
            if(attackDelay >= attackDelayTime && false == skillDone)
            {
                Attack();
                attackDelay = 0.0f;
            }

            if (waitTime >= invokeTime && false == skillDone)
            {
                anim.SetBool("SkillDone", true);
                anim.SetBool("SkillNum1", false);
                skillDone = true;
                KeyManager.instance.skill = Skill.SKILL_DONE;
                effect.SetActive(false);

            }


            yield return null;
        }
        isCoolDown = false;
        yield return null;
    }


    private void Attack()
    {
        int count = Physics.OverlapSphereNonAlloc(transform.position, skillRange, hit, LayerMask.GetMask("Enemy"));
       
        for(int i =0;i < count; i++)
        {
            hit[i].GetComponent<IDamagable>().Hit(damage);
        }

    }


    private void OnDisable()
    {
        isCoolDown = false;
        InGameSkillUI.instance.skillUI[0].fillAmount = 1f;
        KeyManager.instance.skill = Skill.SKILL_NONE;
    }
}


