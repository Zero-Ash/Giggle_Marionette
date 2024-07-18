using UnityEngine;

//
using System;
using System.Collections;
using System.Collections.Generic;

public class Giggle_Unit : MonoBehaviour
{
    #region BASIC
    
    [SerializeField] Giggle_Battle  Basic_Battle;
    IEnumerator Basic_coroutine;

    [Header("RUNNING")]
    [SerializeField] Giggle_Character.Save Basic_Save;

    ////////// Getter & Setter  //////////

    ////////// Method           //////////
    public void Basic_Init(Giggle_Battle _battle, Giggle_Character.Database _database)
    {
        Basic_Battle = _battle;

        Status_Init(_database);

        this.transform.localPosition = Vector3.zero;
        this.transform.localRotation = Quaternion.Euler(Vector3.zero);
        this.transform.localScale    = Vector3.one;

        Basic_coroutine = Basic_Coroutine();
        StartCoroutine(Basic_coroutine);
    }

    public void Basic_Release()
    {
        StopCoroutine(Basic_coroutine);
    }

    IEnumerator Basic_Coroutine()
    {
        float time      = Time.time;
        float lastTime  = Time.time;

        bool isWhile = true;
        while(isWhile)
        {
            time = Time.time;

            if(Basic_Battle.Basic_VarCoroutinePhase.Equals(Giggle_Battle.Basic__COROUTINE_PHASE.ACTIVE))
            {
                Status_Coroutine(time - lastTime);
                Active_Coroutine(time - lastTime);
            }
            else
            {
                if(!Active_phase.Equals(Active_PHASE.WAIT))
                {
                    Active_phase = Active_PHASE.WAIT;
                    Model_SetMotion();
                }
            }

            lastTime = time;
            yield return null;
        }
    }

    ////////// Unity            //////////
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Active_Start();
    }

    // Update is called once per frame
    void Update()
    {

    }

    #endregion

    #region STATUS

    [Serializable]
    public class Status_CoolTimer : IDisposable
    {
        [SerializeField] bool   Basic_isOn;
        [SerializeField] float  Basic_timer;

        ////////// Getter & Setter          //////////
        
        public bool Basic_VarIsOn   { get { return Basic_isOn;  }   set { Basic_isOn = value;   }   }

        public float    Basic_VarTimer  { get { return Basic_timer; }   set { Basic_timer = value;  }   }

        ////////// Method                   //////////
        public void Status_CoolTime(float _deltaTime)
        {
            Basic_timer -= _deltaTime;
        }
        
        ////////// Constructor & Destroyer  //////////
        public Status_CoolTimer()
        {
            Basic_isOn = true;
            Basic_timer = 0.0f;
        }
        
        public void Dispose()
        {

        }
    }

    [Header("STATUS ==================================================")]
    // DB에 저장된 능력치
    [SerializeField] Giggle_Character.Database  Status_database;
    [Header("RUNNING")]
    // 장비 능력치
    [SerializeField] Giggle_Character.Status            Status_equipStatus;
    // 보너스 능력치 ( %로 저장 )
    [SerializeField] Giggle_Character.Status            Status_bonusStatus;
    // 능력치들을 종합한 최종 능력치 ( 게임에서 사용되는 능력치 )
    [SerializeField] Giggle_Character.Status            Status_totalStatus;

    [SerializeField] int Status_hp;
    [SerializeField] List<Status_CoolTimer> Status_coolTimers;

    ////////// Getter & Setter  //////////
    public Giggle_Character.Status  Status_VarTotalStatus   { get { return Status_totalStatus;  }   }

    ////////// Method           //////////
    void Status_Init(Giggle_Character.Database _database)
    {
        Status_database = _database;
        Status_totalStatus.Basic_Calculate(Status_database.Basic_GetStatusList(0), Status_equipStatus, Status_bonusStatus);
        Status_hp = Status_totalStatus.Basic_VarHp;

        if(Status_coolTimers == null)
        {
            Status_coolTimers = new List<Status_CoolTimer>();
        }

        Status_coolTimers.Add(new Status_CoolTimer());
        if(Basic_Save.Basic_VarSkillLv.Equals(-1))
        {
            while(Status_coolTimers.Count < 6)
            {
                Status_coolTimers.Add(new Status_CoolTimer());
            }

            List<int> skills
                = (List<int>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__VAR_SKILL_SLOTS);
            for(int for0 = 0; for0 < skills.Count; for0++)
            {
                Status_coolTimers[for0].Basic_VarIsOn = !skills[for0].Equals(-1);
            }
        }
    }

    void Status_Coroutine(float _deltaTime)
    {
        for(int for0 = 0; for0 < Status_coolTimers.Count; for0++)
        {
            if(Status_coolTimers[for0].Basic_VarIsOn)
            {
                Status_coolTimers[for0].Status_CoolTime(_deltaTime);

                if(Status_coolTimers[for0].Basic_VarTimer < 0.0f)
                {
                    Status_coolTimers[for0].Basic_VarTimer = 0.0f;
                }
            }
        }
    }

    ////////// Unity            //////////

    #endregion

    #region ACTIVE
    public enum Active_PHASE
    {
        WAIT = 0,
        WALK,
        ATTACK,
        HIT,
        DEFEAT,

        ATTACK_DOING
    }
    [Header("ACTIVE ==================================================")]
    [SerializeField] Active_PHASE Active_phase;

    [Header("RUNNING")]
    [SerializeField] Giggle_Unit Active_target;

    [SerializeField] float Active_timer;
    [SerializeField] float Active_time;

    ////////// Getter & Setter  //////////

    ////////// Method           //////////

    void Active_Coroutine(float _deltaTime)
    {
        switch(Active_phase)
        {
            //
            case Active_PHASE.WAIT: { Active_Coroutine__WAIT(); }   break;
            //
            case Active_PHASE.WALK: {                                       }   break;
            //
            case Active_PHASE.ATTACK:       { Active_Coroutine__ATTACK(_deltaTime);         }   break;
            case Active_PHASE.ATTACK_DOING: { Active_Coroutine__ATTACK_DOING(_deltaTime);   }   break;
            //
            case Active_PHASE.HIT:  {                                       }   break;
            //
            case Active_PHASE.DEFEAT:   {                                       }   break;
        }
    }

    // WAIT
    void Active_Coroutine__WAIT()
    {
        // 피아구분
        Giggle_Battle.Formation targetFormation = Basic_Battle.Formation_VarEnemy;
        switch(this.transform.parent.parent.name.Split('/')[1])
        {
            case "PLAYER":  { targetFormation = Basic_Battle.Formation_VarEnemy; }   break;
            case "ENEMY":   { targetFormation = Basic_Battle.Formation_VarAlly;  }   break;
        }

        // 타겟 찾기
        // 앞줄부터 타겟 찾기
        Active_target = Active_Coroutine__WAIT_FindTarget(targetFormation, 2);
        if(Active_target == null)
        {
            Active_target = Active_Coroutine__WAIT_FindTarget(targetFormation, 1);
            if(Active_target == null)
            {
                Active_target = Active_Coroutine__WAIT_FindTarget(targetFormation, 0);
            }
        }

        if(Active_target != null)
        {
            Active_phase = Active_PHASE.ATTACK;

            Model_SetMotion();

            Active_timer = 0.0f;
            Active_time = 1.0f / Status_database.Basic_GetStatusList(0).Basic_VarAttackSpeed;
        }
    }

    // FindTarget
    Giggle_Unit Active_Coroutine__WAIT_FindTarget(Giggle_Battle.Formation _targetFormation, int _num)
    {
        Giggle_Unit res = null;

        //
        float distance = -1.0f;

        Active_Coroutine__FindTarget0(
            _targetFormation.Basic_GetTile(6 + _num),
            ref res, ref distance);
        
        Active_Coroutine__FindTarget0(
            _targetFormation.Basic_GetTile(3 + _num),
            ref res, ref distance);

        Active_Coroutine__FindTarget0(
            _targetFormation.Basic_GetTile(0 + _num),
            ref res, ref distance);

        //
        return res;
    }

    void Active_Coroutine__FindTarget0(
        Transform _tile,
        //
        ref Giggle_Unit _unit, ref float _distance)
    {
        if(_tile.childCount > 0)
        {
            float distance = Vector3.Distance(this.transform.position, _tile.position);
            if((_distance < 0.0f) || (distance < _distance))
            {
                _unit = _tile.GetChild(0).GetComponent<Giggle_Unit>();
                _distance = distance;
            }
        }
    }

    // WALK

    // ATTACK
    void Active_Coroutine__ATTACK(float _deltaTime)
    {
        Active_timer += _deltaTime;

        Model_SetMotionTime("attack");
        
        if(Model_motionTime > 0.0f)
        {
            Active_phase = Active_PHASE.ATTACK_DOING;
        }
    }

    void Active_Coroutine__ATTACK_DOING(float _deltaTime)
    {
        Active_timer += _deltaTime;

        if(Model_VarMotionName.Equals("attack"))
        {
            switch(Model_motionPhase)
            {
                case 0:
                    {
                        if(Active_timer >= Active_time * 0.7f)
                        {

                            // Skill
                            int skillCount = -1;
                            for(int for0 = 0; for0 < Status_coolTimers.Count; for0++)
                            {
                                if(Status_coolTimers[for0].Basic_VarIsOn)
                                {
                                    if(Status_coolTimers[for0].Basic_VarTimer <= 0.0f)
                                    {
                                        skillCount = for0;
                                        break;
                                    }
                                }
                            }

                            if(skillCount >= 0)
                            {
                                // 피노키오일 때
                                if(Status_database.Basic_GetSkillListCount() <= 0)
                                {
                                    Debug.Log(this.name + "스킬");
                                    Status_coolTimers[skillCount].Basic_VarTimer += 5.0f;
                                }
                                // 마리오네트일 때
                                else
                                {
                                    Debug.Log(this.name + "스킬 " + Status_database.Basic_GetSkillListFromCount(0).Basic_VarId);
                                    Status_coolTimers[skillCount].Basic_VarTimer += Status_database.Basic_GetSkillListFromCount(0).Basic_GetLvFromCount(0).Basic_VarCoolTime;
                                }
                            }
                            else
                            {
                                // 최종 데미지 연산
                                int damage = 0;
                                if(Status_VarTotalStatus.Basic_VarAttack != 0)
                                {
                                    damage
                                        = (Status_VarTotalStatus.Basic_VarAttack * Status_VarTotalStatus.Basic_VarAttack)
                                        / (Status_VarTotalStatus.Basic_VarAttack + Active_target.Status_VarTotalStatus.Basic_VarDefence);
                                }

                                Basic_Battle.Bullet_Launch(
                                    this, Active_target,
                                    damage);
                            }

                            Model_motionPhase = 1;
                        }
                    }
                    break;
                case 1:
                    {
                        if(Active_timer >= Active_time)
                        {
                            Active_timer = Active_time;

                            Active_phase = Active_PHASE.WAIT;
                            Model_SetMotion();
                        }
                    }
                    break;
            }
        }

        Model_animator.SetFloat( "MotionTimer", Model_motionTime * Active_timer / Active_time );

        #region 1안
        //Active_timer += _deltaTime;
        //if(Active_timer >= Active_time)
        //{
        //    Active_timer = Active_time;
//
        //    Basic_Battle.Bullet_Launch(this, Active_target);
//
        //    Active_phase = Active_PHASE.WAIT;
        //    Model_SetMotion();
        //}
//
        //Model_animator.SetFloat( "MotionTimer", Model_motionTime * Active_timer / Active_time );
        #endregion
    }

    // HIT

    // DEFEAT

    ////////// Unity            //////////
    void Active_Start()
    {
        Active_phase = Active_PHASE.WAIT;
    }

    #endregion

    #region MODEL
    [Header("MODEL ==================================================")]
    [SerializeField] Animator   Model_animator;
    [SerializeField] float  Model_motionTime;
    [SerializeField] int    Model_motionPhase;

    ////////// Getter & Setter  //////////
    string Model_VarMotionName
    {
        get
        {
            string res = "NONE";

            AnimatorClipInfo[] clipInfo = Model_animator.GetCurrentAnimatorClipInfo(0);
            if(clipInfo != null)
            {
                if(clipInfo.Length > 0)
                {
                    string[] names = clipInfo[0].clip.name.Split('_');
                    res = names[names.Length - 1];
                }
            }

            return res;
        }
    }

    void Model_SetMotion()
    {
        Model_animator.SetInteger(  "MotionNumber", (int)Active_phase   );
        Model_animator.SetTrigger(  "Change"                            );
        Model_animator.SetFloat(    "MotionTimer",  0.0f                );
        Model_motionPhase = 0;
    }

    void Model_SetMotionTime(string _motionName)
    {
        Model_motionTime = 1.0f;

        if(Model_VarMotionName.Equals(_motionName))
        {
            //Model_motionTime = Model_animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        }
    }

    ////////// Method           //////////

    ////////// Unity            //////////

    #endregion
}
