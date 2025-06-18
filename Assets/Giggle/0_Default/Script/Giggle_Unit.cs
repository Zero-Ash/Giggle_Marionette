using UnityEngine;

//
using System;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;

public class Giggle_Unit : MonoBehaviour
{
    #region BASIC
    
    [SerializeField] Giggle_Battle  Basic_Battle;

    [Header("RUNNING")]
    [SerializeField] Giggle_Character.Save Basic_Save;
    IEnumerator Basic_coroutine;

    ////////// Getter & Setter  //////////
    public Giggle_Character.Save    Basic_VarSave   { get { return Basic_Save;  }   }

    ////////// Method           //////////
    
    //
    public void Basic_Init(Giggle_Battle _battle, Giggle_Character.Save _save)
    {
        Basic_Battle = _battle;

        Basic_Save = _save;

        Giggle_Character.Database data = null;
        if (Basic_Save.Basic_VarSkillLv == -1)
        {
            data = (Giggle_Character.Database)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.DATABASE__PINOCCHIO__GET_DATA_FROM_ID,
                //
                Basic_Save.Basic_VarDataId);
        }
        else
        {
            data = (Giggle_Character.Database)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.DATABASE__MARIONETTE__GET_DATA_FROM_ID,
                //
                Basic_Save.Basic_VarDataId);
        }
        Status_Init(data);

        this.transform.localPosition = Vector3.zero;
        this.transform.localRotation = Quaternion.Euler(Vector3.zero);
        this.transform.localScale = Vector3.one;

        Model_AnimatorVarSpeed = (float)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.PLAYER__STAGE__VAR_SPEED);
    }

    public void Basic_Release()
    {
        StopCoroutine(Basic_coroutine);
    }

    // Basic_Coroutine
    IEnumerator Basic_Coroutine()
    {
        float time      = Time.time;
        float lastTime  = Time.time;

        bool isWhile = true;
        while(isWhile)
        {
            time = Time.time;

            switch(Basic_Battle.Basic_VarCoroutinePhase)
            {
                case Giggle_Battle.Basic__COROUTINE_PHASE.ACTIVE_BATTLE:    { Basic_Coroutine__ACTIVE_BATTLE((time - lastTime) * Model_animator.speed); }   break;
                case Giggle_Battle.Basic__COROUTINE_PHASE.ACTIVE_MOVE:      { Basic_Coroutine__ACTIVE_MOVE();                                           }   break;
                default:                                                    { Basic_Coroutine__default();                                               }   break;
            }

            lastTime = time;
            yield return null;
        }
    }

    void Basic_Coroutine__ACTIVE_BATTLE(float _time)
    {
        Status_Coroutine(_time);
        Active_Coroutine(_time);
    }

    void Basic_Coroutine__ACTIVE_MOVE()
    {
        switch(this.transform.parent.parent.name.Split('/')[1])
        {
            case "PLAYER":
                {
                    if(!Active_phase.Equals(Active_PHASE.WALK))
                    {
                        Active_phase = Active_PHASE.WALK;
                        Model_SetMotion();
                    }
                }
                break;
            case "ENEMY":
                {
                    if(!Active_phase.Equals(Active_PHASE.WAIT))
                    {
                        Active_phase = Active_PHASE.WAIT;
                        Model_SetMotion();
                    }
                }
                break;
        }
    }

    void Basic_Coroutine__default()
    {
        if(!Active_phase.Equals(Active_PHASE.WAIT))
        {
            Active_phase = Active_PHASE.WAIT;
            Model_SetMotion();
        }
    }

    ////////// Unity            //////////
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Active_Start();

        Basic_coroutine = Basic_Coroutine();
        StartCoroutine(Basic_coroutine);
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
    [SerializeField] int Status_mana;
    [SerializeField] int Status_shield;
    [SerializeField] List<Status_CoolTimer> Status_coolTimers;

    ////////// Getter & Setter  //////////
    //
    public Giggle_Character.Database    Status_VarDatabase  { get { return Status_database; }   }
    
    //
    public Giggle_Character.Status  Status_VarDatabaseStatus    { get { return Status_database.Basic_GetStatusList(Basic_Save.Basic_VarLevel);  }   }
    
    //
    public Giggle_Character.Status  Status_VarBounsStatus   { get { return Status_bonusStatus; } }

    //
    public Giggle_Character.Status  Status_VarTotalStatus   { get { return Status_totalStatus;  }   }
    
    //
    public int  Status_VarMana  { get { return Status_mana; }   set { Status_mana = value;  }   }

    ////////// Method           //////////
    //
    public void Status_Calculate()
    {
        Status_totalStatus.Basic_Calculate(Status_VarDatabaseStatus, Status_equipStatus, Status_bonusStatus);
        Status_hp = Status_totalStatus.Basic_VarHp;
    }

    //
    public void Status_AttackSuccess()
    {
        for (int for0 = 0; for0 < Basic_VarSave.Basic_VarPassiveCount; for0++)
        {
            Giggle_Character.Save.Passive passive = Basic_VarSave.Basic_GetPassive(for0);
            Giggle_Character.Passive data
                = (Giggle_Character.Passive)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.DATABASE__MARIONETTE__GET_PASSIVE_FROM_ID,
                    //
                    passive.Basic_VarId);
            data.Basic_OnAttackSuccess(this);
        }
    }
    
    //
    public void Status_Damage(int _damage)
    {
        Status_hp -= _damage;

        if (Status_hp <= 0)
        {
            if (Active_phase != Active_PHASE.DEFEAT)
            {
                Active_phase = Active_PHASE.DEFEAT;
                Model_SetMotionTime("Defeat");
                Model_SetMotion();

                Active_timer = 0.0f;
            }
            
            for (int for0 = 0; for0 < Basic_VarSave.Basic_VarPassiveCount; for0++)
            {
                Giggle_Character.Save.Passive passive = Basic_VarSave.Basic_GetPassive(for0);
                Giggle_Character.Passive data
                    = (Giggle_Character.Passive)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                        Giggle_ScriptBridge.EVENT.DATABASE__MARIONETTE__GET_PASSIVE_FROM_ID,
                        //
                        passive.Basic_VarId);
                data.Basic_OnHitTaken(this);
            }
        }
        else
        {
            //Active_phase = Active_PHASE.HIT;
        }
    }

    public void Status_ShieldCharge(int _value)
    {
        Status_shield += _value;
    }

    //
    void Status_Init(Giggle_Character.Database _database)
    {
        Status_database = _database;
        //Status_equipStatus;
        Status_bonusStatus.Basic_BounsSetting(Status_database.Basic_VarSkillId.Equals(-1));

        Status_shield = 0;

        if (Status_coolTimers == null)
        {
            Status_coolTimers = new List<Status_CoolTimer>();
        }

        Status_coolTimers.Add(new Status_CoolTimer());
        if(Status_database.Basic_VarSkillId.Equals(-1))
        {
            while(Status_coolTimers.Count < 6)
            {
                Status_coolTimers.Add(new Status_CoolTimer());
            }

            List<int> skills
                = (List<int>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__SKILL_VAR_SKILL_SLOTS);
            for(int for0 = 0; for0 < skills.Count; for0++)
            {
                Status_coolTimers[for0].Basic_VarIsOn = !skills[for0].Equals(-1);
            }
        }
    }

    // Status_Coroutine
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

    #region PASSIVE



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
    public Active_PHASE Active_VarPhase { get { return Active_phase;    }   }

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
            case Active_PHASE.HIT:  { Active_Coroutine__HIT(_deltaTime);    }   break;
            //
            case Active_PHASE.DEFEAT:   { Active_Coroutine__DEFEAT(_deltaTime); }   break;
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
            Active_time = 1.0f / Status_database.Basic_GetStatusList(Basic_Save.Basic_VarLevel).Basic_VarAttackSpeed;
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
                if(!_tile.GetChild(0).GetComponent<Giggle_Unit>().Active_VarPhase.Equals(Active_PHASE.DEFEAT))
                {
                    _unit = _tile.GetChild(0).GetComponent<Giggle_Unit>();
                    _distance = distance;
                }
            }
        }
    }

    // WALK

    // ATTACK
    void Active_Coroutine__ATTACK(float _deltaTime)
    {
        Active_timer += _deltaTime;

        // 모션 타임 세팅
        Model_SetMotionTime("attack");
        if(Model_motionTime == 0.0f)
        {
            Model_SetMotionTime("Attack_1");
        }
        
        // 모션 타임 설정되면 다음으로 넘기기
        if(Model_motionTime > 0.0f)
        {
            Active_phase = Active_PHASE.ATTACK_DOING;
        }
    }

    void Active_Coroutine__ATTACK_DOING(float _deltaTime)
    {
        Active_timer += _deltaTime;

        if(Model_VarMotionName.Equals("attack") || Model_VarMotionName.Equals("Attack_1"))
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

                            int damage = 0;
                            Giggle_Battle.Bullet.TYPE skillType = Giggle_Battle.Bullet.TYPE.NORMAL;
                            if(skillCount >= 0)
                            {
                                Giggle_Character.Skill skill = null;

                                // 피노키오일 때
                                if(Status_database.Basic_VarSkillId.Equals(-1))
                                {
                                    List<int> slots
                                        = (List<int>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                                            Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__SKILL_VAR_SKILL_SLOTS);
                                    Giggle_Player.Pinocchio_Skill playerSkill
                                        = (Giggle_Player.Pinocchio_Skill)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                                            Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__SKILL_VAR_SKILL_SLOTS,
                                            slots[skillCount]);

                                    skill
                                        = (Giggle_Character.Skill)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                                            Giggle_ScriptBridge.EVENT.DATABASE__PINOCCHIO__GET_SKILL_FROM_ID,
                                            playerSkill.Basic_VarId);

                                    Status_coolTimers[skillCount].Basic_VarTimer += skill.Basic_GetLvFromCount(playerSkill.Basic_VarLv - 1).Basic_VarCoolTime;
                                }
                                // 마리오네트일 때
                                else
                                {
                                    skill
                                        = (Giggle_Character.Skill)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                                            Giggle_ScriptBridge.EVENT.DATABASE__MARIONETTE__GET_SKILL_FROM_ID,
                                            Status_database.Basic_VarSkillId);

                                    Status_coolTimers[skillCount].Basic_VarTimer += skill.Basic_GetLvFromCount(0).Basic_VarCoolTime;
                                }

                                //
                                skillType = skill.Basic_GetLvFromCount(0).Basic_VarType;
                                switch(skillType)
                                {
                                    case Giggle_Battle.Bullet.TYPE.SPLIT:       { damage = (int)((float)Status_VarTotalStatus.Basic_VarAttack * (float)skill.Basic_GetLvFromCount(0).Basic_GetValueFromCount(0) / 100.0f);  }   break;
                                    case Giggle_Battle.Bullet.TYPE.FIRE_BALL:   { damage = (int)((float)Status_VarTotalStatus.Basic_VarAttack * (float)skill.Basic_GetLvFromCount(0).Basic_GetValueFromCount(0) / 100.0f);  }   break;
                                }
                            }
                            else
                            {
                                // 최종 데미지 연산
                                damage = 0;
                                if(Status_VarTotalStatus.Basic_VarAttack != 0)
                                {
                                    damage
                                        = (Status_VarTotalStatus.Basic_VarAttack * Status_VarTotalStatus.Basic_VarAttack)
                                        / (Status_VarTotalStatus.Basic_VarAttack + Active_target.Status_VarTotalStatus.Basic_VarDefence);
                                }
                            }

                            Basic_Battle.Bullet_Launch(
                                this, Active_target,
                                skillType, damage);

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
        else
        {
            Debug.Log(Model_VarMotionName);
        }

        Model_animator.SetFloat( "MotionTimer", Model_motionTime * Active_timer / Active_time );
        if(Model_animator.GetComponent<SkeletonMecanim>() != null)
        {
            Model_animator.Play("ATTACK", 0, Active_timer / Active_time);
        }

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
    void Active_Coroutine__HIT(float _deltaTime)
    {

    }

    // DEFEAT
    void Active_Coroutine__DEFEAT(float _deltaTime)
    {
        Active_timer += _deltaTime;
        if(Active_timer >= Active_time)
        {
            Active_timer = Active_time;

            //
            switch(this.transform.parent.parent.name.Split('/')[1])
            {
                case "PLAYER":  { Basic_Battle.Formation_VarAlly.Basic_RemoveUnit(this.transform);  }   break;
                case "ENEMY":   { Basic_Battle.Formation_VarEnemy.Basic_RemoveUnit(this.transform); }   break;
            }
        }

        Model_animator.SetFloat( "MotionTimer", Model_motionTime * Active_timer / Active_time );
    }

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
                    res = clipInfo[0].clip.name;
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
        Model_motionTime = 0.0f;

        if(Model_VarMotionName.Equals(_motionName))
        {
            Model_motionTime = Model_animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        }
    }

    public float    Model_AnimatorVarSpeed  { set { Model_animator.speed = value;   }   }

    ////////// Method           //////////

    //
    public void Model_View()
    {
        bool isSleep = (bool)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MAIN__POWER_SAVING__GET_IS_ON);

        for(int for0 = 0; for0 < this.transform.childCount; for0++)
        {
            Transform for0Trans = this.transform.GetChild(for0);
            for(int for1 = 0; for1 < for0Trans.childCount; for1++)
            {
                for0Trans.GetChild(for1).gameObject.SetActive(!isSleep);
            }
        }
    }

    ////////// Unity            //////////

    #endregion
}
