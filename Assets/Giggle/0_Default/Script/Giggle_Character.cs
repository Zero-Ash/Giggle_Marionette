using UnityEngine;

//
using System;
using System.Collections.Generic;

namespace Giggle_Character
{
    public enum TYPE
    {
        SWORD = 1,
        THROWING_STAR,
        STAFF
    }

    public enum ATTRIBUTE
    {
        NONE = 0,

        //
        FIRE = 1,
        WATER,
        WIND,
        EARTH,
        DARK,
        LIGHT
    }
    
    public enum CATEGORY
    {
        ATTACK = 1,
        DEFENCE,
        SUPPORT
    }

    #region STATUS

    [Serializable]
    public class Status : IDisposable
    {
        [SerializeField] protected int  Basic_attack;   //  1. 공격력

        [SerializeField] protected int  Basic_defence;  //  2. 방어력

        [SerializeField] protected int  Basic_hp;   //  3. 체력

        [SerializeField] protected float    Basic_attackSpeed;  //  4. 공격 속도

        [SerializeField] protected int  Basic_criticalChance;           //  5. 치명타 확률 
        [SerializeField] protected int  Basic_criticalDamage;           //  6. 치명타 피해량 
        [SerializeField] protected int  Basic_criticalDamageReduction;  //  7. 치명 피해 감소

        [SerializeField] protected int  Basic_luckyChance;  //  8. 럭키샷 확률 
        [SerializeField] protected int  Basic_luckyDamage;  //  9. 럭키샷 피해량 

        [SerializeField] protected int  Basic_damageTakenReduction; // 10. 받는 피해 감소 
        [SerializeField] protected int  Basic_damageIncrease;       // 15. 가하는 피해량 증가

        [SerializeField] protected int  Basic_hpRegenPerSecond; // 11. 초당 체력 회복 
        [SerializeField] protected int  Basic_hpRegenAmount;    // 12. 회복량 
        [SerializeField] protected int  Basic_hpLifeSteal;      // 13. 생명력 흡수

        [SerializeField] protected int  Basic_goldGainIncrease; // 14. 골드 획득량 증가

        [SerializeField] protected int  Basic_allDamageIncrease;    // 16. 모든 속성 피해량 증가

        [SerializeField] protected int  Basic_normalMonsterDamageIncrease;  // 17. 일반 몬스터에게 가하는 피해량 증가 
        [SerializeField] protected int  Basic_bossDamageIncrease;           // 21. 보스에게 가하는 피해량 증가

        [SerializeField] protected int  Basic_skillCooldownReduction;   // 18. 재사용 대기시간 감소 
        [SerializeField] protected int  Basic_skillDamageIncrease;      // 19. 스킬 피해량 증가 
        [SerializeField] protected int  Basic_skillDamageReduction;     // 20. 스킬 피해량 감소 
        [SerializeField] protected int  Basic_skillCritical;            // 26. 스킬 치명 확률
        [SerializeField] protected int  Basic_skillCriticalDamage;      // 31. 스킬 치명 피해 

        [SerializeField] protected int  Basic_stun;             // 22. 스턴 
        [SerializeField] protected int  Basic_stunResistance;   // 23. 스턴 저항

        [SerializeField] protected int  Basic_multiHit;                 // 24. 다중 타격 
        [SerializeField] protected int  Basic_multiHitDamage;           // 27. 다중 타격 피해 
        [SerializeField] protected int  Basic_multiHitDamageReduction;  // 28. 다중 타격 피해 감소

        [SerializeField] protected int  Basic_counterAttack;                // 25. 반격 확률 
        [SerializeField] protected int  Basic_counterAttackDamage;          // 29. 반격 피해 
        [SerializeField] protected int  Basic_counterAttackDamageReduction; // 30. 반격 피해 감소

        [SerializeField] protected int  Basic_evasion;  // 32. 회피

        [SerializeField] protected int  Basic_accuracy; // 33. 명중 

        ////////// Getter & Setter          //////////
        // Basic_attack
        public int  Basic_VarAttack { get { return Basic_attack;    }   }

        // Basic_attack
        public int  Basic_VarDefence    { get { return Basic_defence;   }   }

        // Basic_hp
        public int  Basic_VarHp { get { return Basic_hp;    }   }

        //Basic_attackSpeed
        public float    Basic_VarAttackSpeed    { get { return Basic_attackSpeed;   }   }

        //
        //Basic_cirticalChance
        public int  Basic_VarCriticalChance             { get { return Basic_criticalChance;            }   }
        //Basic_cirticalDamage
        public int  Basic_VarCriticalDamage             { get { return Basic_criticalDamage;            }   }
        //Basic_criticalDamageReduction
        public int  Basic_VarCriticalDamageReduction    { get { return Basic_criticalDamageReduction;   }   }

        //
        //Basic_luckyChance
        public int  Basic_VarLuckyChance    { get { return Basic_luckyChance;   }   }
        //Basic_luckyDamage
        public int  Basic_VarLuckyDamage    { get { return Basic_luckyDamage;   }   }

        //
        //Basic_damageTakenReduction
        public int  Basic_VarDamageTakenReduction   { get { return Basic_damageTakenReduction;  }   }
        //Basic_damageIncrease
        public int  Basic_VarDamageIncrease         { get { return Basic_damageIncrease;        }   }

        //
        //Basic_hpRegenPerSecond
        public int  Basic_VarHpRegenPerSecond   { get { return Basic_hpRegenPerSecond;  }   }
        //Basic_hpRegenAmount
        public int  Basic_VarHpRegenAmount      { get { return Basic_hpRegenAmount;     }   }
        //Basic_hpLifeSteal
        public int  Basic_VarHpLifeSteal        { get { return Basic_hpLifeSteal;       }   }

        //
        //Basic_goldGainIncrease
        public int  Basic_VarGoldGainIncrease   { get { return Basic_goldGainIncrease;  }   }

        //
        //Basic_allDamageIncrease
        public int  Basic_VarAllDamageIncrease  { get { return Basic_allDamageIncrease; }   }

        //
        //Basic_normalMonsterDamageIncrease
        public int  Basic_VarNormalMonsterDamageIncrease    { get { return Basic_normalMonsterDamageIncrease;   }   }
        //Basic_bossDamageIncrease
        public int  Basic_VarBossDamageIncrease             { get { return Basic_bossDamageIncrease;            }   }

        //
        //Basic_skillCooldownReduction
        public int  Basic_VarSkillCooldownReduction { get { return Basic_skillCooldownReduction;    }   }
        //Basic_skillDamageIncrease
        public int  Basic_VarSkillDamageIncrease    { get { return Basic_skillDamageIncrease;       }   }
        //Basic_skillDamageReduction
        public int  Basic_VarSkillDamageReduction   { get { return Basic_skillDamageReduction;      }   }
        //Basic_skillCritical
        public int  Basic_VarSkillCritical          { get { return Basic_skillCritical;             }   }
        //Basic_skillCriticalDamage
        public int Basic_VarSkillCriticalDamage     { get { return Basic_skillCriticalDamage;       }   }

        //
        //Basic_stun
        public int  Basic_VarStun           { get { return Basic_stun;              }   }
        //Basic_stunResistance
        public int  Basic_VarStunResistance { get { return Basic_stunResistance;    }   }

        //
        //Basic_multiHit
        public int  Basic_VarMultiHit                   { get { return Basic_multiHit;                  }   }
        //Basic_multiHitDamage
        public int  Basic_VarMultiHitDamage             { get { return Basic_multiHitDamage;            }   }
        //Basic_multiHitDamageReduction
        public int  Basic_VarMultiHitDamageReduction    { get { return Basic_multiHitDamageReduction;   }   }

        //
        //Basic_counterAttack
        public int Basic_VarCounterAttack                   { get { return Basic_counterAttack;                 }   }
        //Basic_counterAttackDamage
        public int Basic_VarCounterAttackDamage             { get { return Basic_counterAttackDamage;           }   }
        //Basic_counterAttackDamageReduction
        public int Basic_VarCounterAttackDamageReduction    { get { return Basic_counterAttackDamageReduction;  }   }

        //
        //Basic_evasion
        public int Basic_VarEvasion { get { return Basic_evasion;   }   }

        //
        //Basic_accuracy
        public int Basic_VarAccuracy    { get { return Basic_accuracy;  }   }

        ////////// Method                   //////////
        public void Basic_Reset()
        {
            Basic_attack    = 0;

            Basic_defence   = 0;

            Basic_hp    = 0;

            Basic_attackSpeed   = 0.0f;

            Basic_criticalChance            = 0;
            Basic_criticalDamage            = 0;
            Basic_criticalDamageReduction   = 0;

            Basic_luckyChance   = 0;
            Basic_luckyDamage   = 0;

            Basic_damageTakenReduction  = 0;
            Basic_damageIncrease        = 0;

            Basic_hpRegenPerSecond  = 0;
            Basic_hpRegenAmount     = 0;
            Basic_hpLifeSteal       = 0;

            Basic_goldGainIncrease  = 0;

            Basic_allDamageIncrease = 0;

            Basic_normalMonsterDamageIncrease   = 0;
            Basic_bossDamageIncrease            = 0;

            Basic_skillCooldownReduction    = 0;
            Basic_skillDamageIncrease       = 0;
            Basic_skillDamageReduction      = 0;
            Basic_skillCritical             = 0;
            Basic_skillCriticalDamage       = 0;

            Basic_stun              = 0;
            Basic_stunResistance    = 0;

            Basic_multiHit                  = 0;
            Basic_multiHitDamage            = 0;
            Basic_multiHitDamageReduction   = 0;

            Basic_counterAttack                 = 0;
            Basic_counterAttackDamage           = 0;
            Basic_counterAttackDamageReduction  = 0;

            Basic_evasion   = 0;

            Basic_accuracy  = 0;
        }

        //
        public void Basic_BounsSetting(bool _isPinocchio)
        {
            Basic_Reset();

            if(_isPinocchio)
            {
                Basic_BounsSetting__PinocchioAttribute(Giggle_Player.ATTRIBUTE_TYPE.ATTACK);
            }
        }

        void Basic_BounsSetting__PinocchioAttribute(Giggle_Player.ATTRIBUTE_TYPE _type)
        {
            // attribute
            Giggle_Player.Attribute_Data attribute
                = (Giggle_Player.Attribute_Data)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__ATTRIBUTE_VAR_FROM_TYPE,
                    _type);
            
            for(int for0 = 0; for0 < attribute.Basic_VarListCount; for0++)
            {
                int level = attribute.Basic_GetListFromCount(for0).Basic_VarLv - 1;

                if(level >= 0)
                {
                    Giggle_Character.Attribute element0
                        = (Giggle_Character.Attribute)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                            Giggle_ScriptBridge.EVENT.DATABASE__PINOCCHIO__GET_ATTRIBUTE_FROM_ID,
                            _type, attribute.Basic_GetListFromCount(for0).Basic_VarId);

                    Giggle_Character.Attribute_Lv element1 = element0.Basic_VarLvFromCount(level);

                    //
                    switch(element1.Basic_VarAbility)
                    {
                        case Attribute_Lv.ABILITY.ATTACK_PER:   { Basic_attack  += element1.Basic_VarAbilityValue * 100;  }   break;
                        case Attribute_Lv.ABILITY.DEFENCE_PER:  { Basic_defence += element1.Basic_VarAbilityValue * 100;  }   break;
                        case Attribute_Lv.ABILITY.HP_PER:       { Basic_hp      += element1.Basic_VarAbilityValue * 100;  }   break;
                    }
                }
            }

            // ability
            int abilityCount = (int)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__ABILITY_GET_ABILITYS_COUNT);
            for(int for0 = 0; for0 < abilityCount; for0++)
            {
                Giggle_Player.Pinocchio_Ability element0
                    = (Giggle_Player.Pinocchio_Ability)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                        Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__ABILITY_GET_ABILITY_FROM_COUNT,
                        for0);
                
                if(!element0.Basic_VarId.Equals(-1))
                {
                    Giggle_Character.AbilityClass element1
                        = (Giggle_Character.AbilityClass)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                            Giggle_ScriptBridge.EVENT.DATABASE__PINOCCHIO__ABILITY_GET_ABILITY_FROM_ELEMENT,
                            element0.Basic_VarId, element0.Basic_VarGrade);
                    
                    switch(element1.Basic_VarType)
                    {
                        case AbilityClass.TYPE.ATTACK_PER:                      { Basic_attack                      += (int)(element0.Basic_VarValue * 100.0f);   }   break;
                        case AbilityClass.TYPE.DEFENCE_PER:                     { Basic_defence                     += (int)(element0.Basic_VarValue * 100.0f);   }   break;
                        case AbilityClass.TYPE.HP_PER:                          { Basic_hp                          += (int)(element0.Basic_VarValue * 100.0f);   }   break;
                        case AbilityClass.TYPE.SKILL_DAMAGE_PER:                { Basic_skillDamageIncrease         += (int)(element0.Basic_VarValue * 100.0f);   }   break;
                        case AbilityClass.TYPE.MULTI_DAMAGE_PER:                { Basic_multiHitDamage              += (int)(element0.Basic_VarValue * 100.0f);   }   break;
                        case AbilityClass.TYPE.COUNTER_ATTACK_PER:              { Basic_counterAttackDamage         += (int)(element0.Basic_VarValue * 100.0f);   }   break;
                        case AbilityClass.TYPE.CRITICAL_DAMAGE_PER:             { Basic_criticalDamage              += (int)(element0.Basic_VarValue * 100.0f);   }   break;
                        case AbilityClass.TYPE.CRITICAL_DAMAGE_REDUCTION_PER:   { Basic_criticalDamageReduction     += (int)(element0.Basic_VarValue * 100.0f);   }   break;
                        case AbilityClass.TYPE.BOSS_DAMAGE_PER:                 { Basic_bossDamageIncrease          += (int)(element0.Basic_VarValue * 100.0f);   }   break;
                        case AbilityClass.TYPE.NORMAL_DAMAGE_PER:               { Basic_normalMonsterDamageIncrease += (int)(element0.Basic_VarValue * 100.0f);   }   break;
                        case AbilityClass.TYPE.ALL_DAMAGE_PER:                  { Basic_allDamageIncrease           += (int)(element0.Basic_VarValue * 100.0f);   }   break;
                        case AbilityClass.TYPE.LUCKY_DAMAGE_PER:                { Basic_luckyDamage                 += (int)(element0.Basic_VarValue * 100.0f);   }   break;
                    }
                }
            }

            // relic
            List<Giggle_Player.Pinocchio_RelicSlot> relicSlot = (List<Giggle_Player.Pinocchio_RelicSlot>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__RELIC_VAR_RELIC_SLOTS);
            for(int for0 = 0; for0 < relicSlot.Count; for0++)
            {
                if(!relicSlot[for0].Basic_VarInventoryId.Equals(-1))
                {
                    Giggle_Player.Pinocchio_Relic element0
                        = (Giggle_Player.Pinocchio_Relic)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                            Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__RELIC_GET_RELIC_FROM_INVENTORY_ID,
                            relicSlot[for0].Basic_VarInventoryId);
                    
                    Giggle_Item.Relic element1
                        = (Giggle_Item.Relic)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                            Giggle_ScriptBridge.EVENT.DATABASE__PINOCCHIO__RELIC_VAR_DATA_FROM_ID,
                            element0.Basic_VarDataId);
                    
                    switch(element1.Basic_GetLvDataFromLv(element0.Basic_VarLevel).Basic_VarAbility)
                    {
                        case Giggle_Item.RelicLv.ABILITY.ATTACK_PER:    { Basic_attack  += element1.Basic_GetLvDataFromLv(element0.Basic_VarLevel).Basic_VarAbilityValue;   }   break;
                        case Giggle_Item.RelicLv.ABILITY.DEFENCE_PER:   { Basic_defence += element1.Basic_GetLvDataFromLv(element0.Basic_VarLevel).Basic_VarAbilityValue;   }   break;
                    }
                    
                    // 배치 버프
                    Basic_attack    += element0.Basic_GetBuffFromCount((int)Giggle_Item.Relic_COLOR.WHITE) * 100;
                    Basic_defence   += element0.Basic_GetBuffFromCount((int)Giggle_Item.Relic_COLOR.BLACK) * 100;
                }
            }
        }

        #region Basic_Calculate

        public void Basic_Calculate(Status _database, Status _equipStatus, Status _bonusStatus)
        {
            //
            Basic_Calculate_Attack( _database,  _equipStatus,   _bonusStatus    );
            
            //
            Basic_Calculate_Defence(    _database,  _equipStatus,   _bonusStatus    );
            
            //
            Basic_Calculate_Hp( _database,  _equipStatus,   _bonusStatus    );
            
            //
            Basic_Calculate_AttackSpeed(
                _database, _equipStatus, _bonusStatus);
            
            //
            Basic_Calculate_CriticalChance(         _database,  _equipStatus,   _bonusStatus    );
            Basic_Calculate_CriticalDamage(         _database,  _equipStatus,   _bonusStatus    );
            Basic_Calculate_CriticalDamageReduction(_database,  _equipStatus,   _bonusStatus    );
            
            //
            Basic_Calculate_LuckyChance(_database,  _equipStatus,   _bonusStatus    );
            Basic_Calculate_LuckyDamage(_database,  _equipStatus,   _bonusStatus    );
            
            //
            Basic_Calculate_DamageTakenReduction(   _database,  _equipStatus,   _bonusStatus    );
            Basic_Calculate_DamageIncrease(         _database,  _equipStatus,   _bonusStatus    );
            
            //
            Basic_Calculate_HpRegenPerSecond(   _database,  _equipStatus,   _bonusStatus    );
            Basic_Calculate_HpRegenAmount(      _database,  _equipStatus,   _bonusStatus    );
            Basic_Calculate_HpLifeSteal(        _database,  _equipStatus,   _bonusStatus    );
            
            //
            Basic_Calculate_GoldGainIncrease(_database,  _equipStatus,   _bonusStatus);

            //
            Basic_Calculate_AllDamageIncrease(_database,  _equipStatus,   _bonusStatus);

            //
            Basic_Calculate_NormalMonsterDamageIncrease(_database,  _equipStatus,   _bonusStatus);
            Basic_Calculate_BossDamageIncrease(         _database,  _equipStatus,   _bonusStatus);

            //
            Basic_Calculate_SkillCooldownReduction( _database,  _equipStatus,   _bonusStatus);
            Basic_Calculate_SkillDamageIncrease(    _database,  _equipStatus,   _bonusStatus);
            Basic_Calculate_SkillDamageReduction(   _database,  _equipStatus,   _bonusStatus);
            Basic_Calculate_SkillCritical(          _database,  _equipStatus,   _bonusStatus);
            Basic_Calculate_SkillCriticalDamage(    _database,  _equipStatus,   _bonusStatus);

            //
            Basic_Calculate_Stun(           _database,  _equipStatus,   _bonusStatus);
            Basic_Calculate_StunResistance( _database,  _equipStatus,   _bonusStatus);

            //
            Basic_Calculate_MultiHit(               _database,  _equipStatus,   _bonusStatus);
            Basic_Calculate_MultiHitDamage(         _database,  _equipStatus,   _bonusStatus);
            Basic_Calculate_MultiHitDamageReduction(_database,  _equipStatus,   _bonusStatus);

            //
            Basic_Calculate_CounterAttack(                  _database,  _equipStatus,   _bonusStatus);
            Basic_Calculate_CounterAttackDamage(            _database,  _equipStatus,   _bonusStatus);
            Basic_Calculate_CounterAttackDamageReduction(   _database,  _equipStatus,   _bonusStatus);
            
            //
            Basic_Calculate_Evasion(_database,  _equipStatus,   _bonusStatus);
            
            //
            Basic_Calculate_Accuracy(_database,  _equipStatus,   _bonusStatus);
        }

        //
        //TODO:전투 미적용 
        public void Basic_Calculate_Attack(Status _database, Status _equipStatus, Status _bonusStatus)
        {
            Basic_attack = Basic_Calculate_Type0(_database.Basic_VarAttack, _equipStatus.Basic_VarAttack,   _bonusStatus.Basic_VarAttack    );
        }

        //
        //TODO:전투 미적용 
        public void Basic_Calculate_Defence(Status _database, Status _equipStatus, Status _bonusStatus)
        {
            Basic_defence = Basic_Calculate_Type0(_database.Basic_VarDefence, _equipStatus.Basic_VarDefence, _bonusStatus.Basic_VarDefence);
        }

        //
        //TODO:전투 미적용 
        public void Basic_Calculate_Hp(Status _database, Status _equipStatus, Status _bonusStatus)
        {
            Basic_hp = Basic_Calculate_Type0(_database.Basic_VarHp, _equipStatus.Basic_VarHp, _bonusStatus.Basic_VarHp);
        }

        //
        //TODO:전투 미적용 
        public void Basic_Calculate_AttackSpeed(
            Status _database, Status _equipStatus, Status _bonusStatus)
        {
            Basic_attackSpeed = _database.Basic_VarAttackSpeed + _equipStatus.Basic_VarAttackSpeed;
            Basic_attackSpeed *= (100.0f + _bonusStatus.Basic_VarAttackSpeed) * 0.01f;

            //TODO:아이템 타입에 따른 공격속도 조절. 아이템의 정보가 업데이트 되는 날 작업 
        }

        //
        //TODO:전투 미적용 
        public void Basic_Calculate_CriticalChance(Status _database, Status _equipStatus, Status _bonusStatus)
        {
            Basic_criticalChance = _database.Basic_VarCriticalChance + _equipStatus.Basic_VarCriticalChance + _bonusStatus.Basic_VarCriticalChance;
        }

        //TODO:전투 미적용 
        public void Basic_Calculate_CriticalDamage(Status _database, Status _equipStatus, Status _bonusStatus)
        {
            Basic_criticalDamage = 100 + _database.Basic_VarCriticalDamage + _equipStatus.Basic_VarCriticalDamage + _bonusStatus.Basic_VarCriticalDamage;
        }

        //TODO:전투 미적용 
        public void Basic_Calculate_CriticalDamageReduction(Status _database, Status _equipStatus, Status _bonusStatus)
        {
            Basic_criticalDamageReduction = 100 + _database.Basic_VarCriticalDamageReduction + _equipStatus.Basic_VarCriticalDamageReduction + _bonusStatus.Basic_VarCriticalDamageReduction;
        }

        //
        //TODO:전투 미적용 
        public void Basic_Calculate_LuckyChance(Status _database, Status _equipStatus, Status _bonusStatus)
        {
            Basic_luckyChance =_database.Basic_VarLuckyChance + _equipStatus.Basic_VarLuckyChance + _bonusStatus.Basic_VarLuckyChance;
        }

        //TODO:전투 미적용 
        public void Basic_Calculate_LuckyDamage(Status _database, Status _equipStatus, Status _bonusStatus)
        {
            Basic_luckyDamage = 150 + _database.Basic_VarLuckyDamage + _equipStatus.Basic_VarLuckyDamage + _bonusStatus.Basic_VarLuckyDamage;
        }

        //
        //TODO:전투 미적용 
        public void Basic_Calculate_DamageTakenReduction(Status _database, Status _equipStatus, Status _bonusStatus)
        {
            Basic_damageTakenReduction = _database.Basic_VarDamageTakenReduction + _equipStatus.Basic_VarDamageTakenReduction + _bonusStatus.Basic_VarDamageTakenReduction;
        }

        //TODO:전투 미적용 
        public void Basic_Calculate_DamageIncrease(Status _database, Status _equipStatus, Status _bonusStatus)
        {
            Basic_damageIncrease = _database.Basic_VarDamageIncrease + _equipStatus.Basic_VarDamageIncrease + _bonusStatus.Basic_VarDamageIncrease;
        }

        //
        //TODO:전투 미적용 
        public void Basic_Calculate_HpRegenPerSecond(Status _database, Status _equipStatus, Status _bonusStatus)
        {
            Basic_hpRegenPerSecond = _database.Basic_VarHpRegenPerSecond + _equipStatus.Basic_VarHpRegenPerSecond + _bonusStatus.Basic_VarHpRegenPerSecond;
        }

        //TODO:전투 미적용 
        public void Basic_Calculate_HpRegenAmount(Status _database, Status _equipStatus, Status _bonusStatus)
        {
            Basic_hpRegenAmount = _database.Basic_VarHpRegenAmount + _equipStatus.Basic_VarHpRegenAmount + _bonusStatus.Basic_VarHpRegenAmount;
        }

        //TODO:전투 미적용 
        public void Basic_Calculate_HpLifeSteal(Status _database, Status _equipStatus, Status _bonusStatus)
        {
            Basic_hpLifeSteal = _database.Basic_VarHpLifeSteal + _equipStatus.Basic_VarHpLifeSteal + _bonusStatus.Basic_VarHpLifeSteal;
        }

        //
        //TODO:전투 미적용 
        public void Basic_Calculate_GoldGainIncrease(Status _database, Status _equipStatus, Status _bonusStatus)
        {
            Basic_goldGainIncrease = _database.Basic_VarGoldGainIncrease + _equipStatus.Basic_VarGoldGainIncrease + _bonusStatus.Basic_VarGoldGainIncrease;
        }

        //
        //TODO:전투 미적용 
        public void Basic_Calculate_AllDamageIncrease(Status _database, Status _equipStatus, Status _bonusStatus)
        {
            Basic_allDamageIncrease = _database.Basic_VarAllDamageIncrease + _equipStatus.Basic_VarAllDamageIncrease + _bonusStatus.Basic_VarAllDamageIncrease;
        }

        //
        //TODO:전투 미적용 
        public void Basic_Calculate_NormalMonsterDamageIncrease(Status _database, Status _equipStatus, Status _bonusStatus)
        {
            Basic_normalMonsterDamageIncrease = _database.Basic_VarNormalMonsterDamageIncrease + _equipStatus.Basic_VarNormalMonsterDamageIncrease + _bonusStatus.Basic_VarNormalMonsterDamageIncrease;
        }

        //TODO:전투 미적용 
        public void Basic_Calculate_BossDamageIncrease(Status _database, Status _equipStatus, Status _bonusStatus)
        {
            Basic_bossDamageIncrease = _database.Basic_VarBossDamageIncrease + _equipStatus.Basic_VarBossDamageIncrease + _bonusStatus.Basic_VarBossDamageIncrease;
        }

        //
        //TODO:전투 미적용 
        public void Basic_Calculate_SkillCooldownReduction(Status _database, Status _equipStatus, Status _bonusStatus)
        {
            Basic_skillCooldownReduction = _database.Basic_VarSkillCooldownReduction + _equipStatus.Basic_VarSkillCooldownReduction + _bonusStatus.Basic_VarSkillCooldownReduction;
        }

        //TODO:전투 미적용 
        public void Basic_Calculate_SkillDamageIncrease(Status _database, Status _equipStatus, Status _bonusStatus)
        {
            Basic_skillDamageIncrease = _database.Basic_VarSkillDamageIncrease + _equipStatus.Basic_VarSkillDamageIncrease + _bonusStatus.Basic_VarSkillDamageIncrease;
        }

        //TODO:전투 미적용 
        public void Basic_Calculate_SkillDamageReduction(Status _database, Status _equipStatus, Status _bonusStatus)
        {
            Basic_skillDamageReduction = _database.Basic_VarSkillDamageReduction + _equipStatus.Basic_VarSkillDamageReduction + _bonusStatus.Basic_VarSkillDamageReduction;
        }

        //TODO:전투 미적용 
        public void Basic_Calculate_SkillCritical(Status _database, Status _equipStatus, Status _bonusStatus)
        {
            Basic_skillCritical = _database.Basic_VarSkillCritical + _equipStatus.Basic_VarSkillCritical + _bonusStatus.Basic_VarSkillCritical;
        }

        //TODO:전투 미적용 
        public void Basic_Calculate_SkillCriticalDamage(Status _database, Status _equipStatus, Status _bonusStatus)
        {
            Basic_skillCriticalDamage = 100 + _database.Basic_VarSkillCriticalDamage + _equipStatus.Basic_VarSkillCriticalDamage + _bonusStatus.Basic_VarSkillCriticalDamage;
        }

        //
        //TODO:전투 미적용 
        public void Basic_Calculate_Stun(Status _database, Status _equipStatus, Status _bonusStatus)
        {
            Basic_stun = _database.Basic_VarStun + _equipStatus.Basic_VarStun + _bonusStatus.Basic_VarStun;
        }

        //TODO:전투 미적용 
        public void Basic_Calculate_StunResistance(Status _database, Status _equipStatus, Status _bonusStatus)
        {
            Basic_stunResistance = _database.Basic_VarStunResistance + _equipStatus.Basic_VarStunResistance + _bonusStatus.Basic_VarStunResistance;
        }

        //
        //TODO:전투 미적용 
        public void Basic_Calculate_MultiHit(Status _database, Status _equipStatus, Status _bonusStatus)
        {
            Basic_multiHit = _database.Basic_VarMultiHit + _equipStatus.Basic_VarMultiHit + _bonusStatus.Basic_VarMultiHit;
        }

        //TODO:전투 미적용 
        public void Basic_Calculate_MultiHitDamage(Status _database, Status _equipStatus, Status _bonusStatus)
        {
            Basic_multiHitDamage = 100 + _database.Basic_VarMultiHitDamage + _equipStatus.Basic_VarMultiHitDamage + _bonusStatus.Basic_VarMultiHitDamage;
        }

        //TODO:전투 미적용 
        public void Basic_Calculate_MultiHitDamageReduction(Status _database, Status _equipStatus, Status _bonusStatus)
        {
            Basic_multiHitDamageReduction = _database.Basic_VarMultiHitDamageReduction + _equipStatus.Basic_VarMultiHitDamageReduction + _bonusStatus.Basic_VarMultiHitDamageReduction;
        }

        //
        //TODO:전투 미적용 
        public void Basic_Calculate_CounterAttack(Status _database, Status _equipStatus, Status _bonusStatus)
        {
            Basic_counterAttack = _database.Basic_VarCounterAttack + _equipStatus.Basic_VarCounterAttack + _bonusStatus.Basic_VarCounterAttack;
        }

        //TODO:전투 미적용 
        public void Basic_Calculate_CounterAttackDamage(Status _database, Status _equipStatus, Status _bonusStatus)
        {
            Basic_counterAttackDamage = 100 + _database.Basic_VarCounterAttackDamage + _equipStatus.Basic_VarCounterAttackDamage + _bonusStatus.Basic_VarCounterAttackDamage;
        }

        //TODO:전투 미적용 
        public void Basic_Calculate_CounterAttackDamageReduction(Status _database, Status _equipStatus, Status _bonusStatus)
        {
            Basic_counterAttackDamageReduction = _database.Basic_VarCounterAttackDamageReduction + _equipStatus.Basic_VarCounterAttackDamageReduction + _bonusStatus.Basic_VarCounterAttackDamageReduction;
        }

        //
        //TODO:전투 미적용 
        public void Basic_Calculate_Evasion(Status _database, Status _equipStatus, Status _bonusStatus)
        {
            Basic_evasion = _database.Basic_VarEvasion + _equipStatus.Basic_VarEvasion + _bonusStatus.Basic_VarEvasion;
        }

        //
        //TODO:전투 미적용 
        public void Basic_Calculate_Accuracy(Status _database, Status _equipStatus, Status _bonusStatus)
        {
            Basic_accuracy = _database.Basic_VarAccuracy + _equipStatus.Basic_VarAccuracy + _bonusStatus.Basic_VarAccuracy;
        }

        // 계산식 
        // (캐릭터 능력치 + 장비 종합 능력치) * ( 100% + 보너스 능력치 )
        int Basic_Calculate_Type0(int _database, int _equip, int _bouns)
        {
            int res = _database + _equip;
            res = (int)((float)res * (1.0f + ((float)_bouns * 0.0001f)));

            //
            return res;
        }

        #endregion

        ////////// Constructor & Destroyer  //////////
        //
        public Status()
        {
            Basic_Reset();
        }

        public Status(Dictionary<string, string> _data)
        {
            Basic_Reset();

            Basic_attack    = int.Parse(_data["cha_attack"]);

            Basic_defence   = int.Parse(_data["cha_defense"]);

            Basic_hp    = int.Parse(_data["cha_hp"]);

            Basic_attackSpeed   = float.Parse(_data["cha_attack_speed"]);

            Basic_criticalChance            = int.Parse(_data["cha_criticalchance"]         );
            Basic_criticalDamage            = int.Parse(_data["cha_criticaldamage"]         );
            Basic_criticalDamageReduction   = int.Parse(_data["cha_criticaldamagereduction"]);

            Basic_luckyChance   = int.Parse(_data["cha_luckychance"]);
            Basic_luckyDamage   = int.Parse(_data["cha_luckydamage"]);

            Basic_damageTakenReduction  = int.Parse(_data["cha_damagetakenreduction"]   );
            Basic_damageIncrease        = int.Parse(_data["cha_damageincrease"]         );

            Basic_hpRegenPerSecond  = int.Parse(_data["cha_hpregenpersecond"]   );
            Basic_hpRegenAmount     = int.Parse(_data["cha_hpregenamount"]      );
            Basic_hpLifeSteal       = int.Parse(_data["cha_lifesteal"]          );

            Basic_goldGainIncrease = int.Parse(_data["cha_goldgainincrease"]);

            Basic_allDamageIncrease = int.Parse(_data["cha_alldamageincrease"]);

            Basic_normalMonsterDamageIncrease   = int.Parse(_data["cha_normalmonsterdamageincrease"]);
            Basic_bossDamageIncrease            = int.Parse(_data["cha_bossdamageincrease"]         );

            Basic_skillCooldownReduction    = int.Parse(_data["cha_cooldownreduction"]      );
            Basic_skillDamageIncrease       = int.Parse(_data["cha_skilldamageincrease"]    );
            Basic_skillDamageReduction      = int.Parse(_data["cha_skilldamagereduction"]   );
            Basic_skillCritical             = int.Parse(_data["cha_skillcritical"]          );
            Basic_skillCriticalDamage       = int.Parse(_data["cha_skillcriticaldamage"]    );

            Basic_stun              = int.Parse(_data["cha_stun"]           );
            Basic_stunResistance    = int.Parse(_data["cha_stunresistance"] );

            Basic_multiHit                  = int.Parse(_data["cha_multihit"]               );
            Basic_multiHitDamage            = int.Parse(_data["cha_multihitdamage"]         );
            Basic_multiHitDamageReduction   = int.Parse(_data["cha_multihitdamagereduction"]);

            Basic_counterAttack                 = int.Parse(_data["cha_counterattack"]                  );
            Basic_counterAttackDamage           = int.Parse(_data["cha_counterattackdamage"]            );
            Basic_counterAttackDamageReduction  = int.Parse(_data["cha_counterattackdamagereduction"]   );

            Basic_evasion = int.Parse(_data["cha_evasion"]);

            Basic_accuracy = int.Parse(_data["cha_accuracy"]);
        }

        //
        public void Dispose()
        {

        }
    }

    #endregion

    #region Database

    [Serializable]
    public class Database : IDisposable
    {
        [SerializeField] protected int      Basic_id;
        [SerializeField] protected string   Basic_name;

        [SerializeField] Giggle_Unit    Basic_unit;
        [SerializeField] List<Status>   Basic_statusList;

        [SerializeField] protected ATTRIBUTE    Basic_attribute;
        [SerializeField] protected TYPE         Basic_type;
        [SerializeField] protected CATEGORY     Basic_category;

        [SerializeField] int    Basic_mana;
        [SerializeField] int    Basic_manaAttackRecovery;
        [SerializeField] int    Basic_manaHitRecovery;

        ////////// Getter & Setter          //////////
        // Basic_id
        public int  Basic_VarId                 { get { return Basic_id;    }   }

        public bool Basic_GetIdIsSame(int _id)  { return Basic_id.Equals(_id);  }

        // Basic_name
        public string   Basic_VarName   { get { return Basic_name;  }   }

        // Basic_unit
        public Giggle_Unit  Basic_VarUnit   { get { return Basic_unit;  } set { Basic_unit = value; }   }

        // Basic_statusList
        public Status Basic_GetStatusList(int _level)
        {
            return Basic_statusList[_level - 1];
        }

        public void Basic_SetStatusList(Dictionary<string, string> _data)
        {
            if(Basic_statusList == null)
            {
                Basic_statusList = new List<Status>();
            }

            //
            int level = int.Parse(_data["cha_level"]);
            while(Basic_statusList.Count < level)
            {
                Basic_statusList.Add(null);
            }

            Basic_statusList[level - 1] = new Status(_data);
        }

        //
        public ATTRIBUTE    Basic_VarAttribute  { get { return Basic_attribute; }   }

        //
        public CATEGORY     Basic_VarCategory   { get { return Basic_category;  }   }

        public virtual int  Basic_VarSkillId { get { return -1; }   }

        public virtual int  Basic_VarConstellation  { get { return -1;  }   }


        ////////// Method                   //////////

        ////////// Constructor & Destroyer  //////////
        public Database()
        {
            Basic_id = -1;

            Basic_Constructor();
        }

        public Database(Dictionary<string, string> _data)
        {
            Basic_id        = int.Parse(_data["cha_id"]);
            Basic_name      = _data["cha_name"];

            Basic_Constructor();
            
            Basic_attribute = ATTRIBUTE.NONE;
            Basic_type      = TYPE.SWORD;
            Basic_category  = CATEGORY.ATTACK;

            Basic_mana               = int.Parse(_data["cha_mana"]);
            Basic_manaAttackRecovery = int.Parse(_data["cha_attack_recovery"]);
            Basic_manaHitRecovery    = int.Parse(_data["cha_hit_recovery"]);
        }

        void Basic_Constructor()
        {
        }

        //
        public void Dispose()
        {

        }
    }

    [Serializable]
    public class Database_Marionette : Database
    {
        [SerializeField] protected int  Basic_skillId;

        [SerializeField] protected int  Basic_constellation;

        ////////// Getter & Setter          //////////
        public override int Basic_VarSkillId    { get { return Basic_skillId;   }   }

        public override int Basic_VarConstellation  { get { return Basic_constellation; }   }

        ////////// Method                   //////////

        ////////// Constructor & Destroyer  //////////
        public Database_Marionette(Dictionary<string, string> _data) : base(_data)
        {
            Basic_attribute = (ATTRIBUTE)int.Parse(_data["cha_attribute"]);
            Basic_type      = (TYPE)int.Parse(_data["cha_type"]);
            Basic_category  = (CATEGORY)int.Parse(_data["cha_category"]);

            Basic_skillId   = int.Parse(_data["cha_skill_id"]);

            Basic_constellation = int.Parse(_data["constellation_b_id"]);
        }
    }

    [Serializable]
    public class Database_Pinocchio : Database
    {
        [SerializeField] protected Giggle_Player.Pinocchio_GENDER   Basic_gender;
        [SerializeField] protected int                              Basic_attack;
        [SerializeField] protected int                              Basic_defence;
        [SerializeField] protected int                              Basic_hp;
        [SerializeField] protected float                            Basic_attackSpeed;
        [SerializeField] protected float                            Basic_doubleAttackChance;
        [SerializeField] protected int                              Basic_counterAttackChance;
        [SerializeField] protected int                              Basic_criticalAttackchance;

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////

        ////////// Constructor & Destroyer  //////////
        public Database_Pinocchio(Dictionary<string, string> _data) : base(_data)
        {
            Basic_gender                = (Giggle_Player.Pinocchio_GENDER)int.Parse(_data["cha_gender"]);
            Basic_attack                = int.Parse(_data["cha_basic_attack"]);
            Basic_defence               = int.Parse(_data["cha_basic_defense"]);
            Basic_hp                    = int.Parse(_data["cha_basic_hp"]);
            Basic_attackSpeed           = float.Parse(_data["cha_basic_attack_speed"]);
            Basic_doubleAttackChance    = float.Parse(_data["cha_basic_double_attack_chance"]);
            Basic_counterAttackChance   = int.Parse(_data["cha_basic_counter_attack_chance"]);
            Basic_criticalAttackchance  = int.Parse(_data["cha_basic_critical_attack_chance"]);
        }
    }

    #endregion

    #region SKILL

    [Serializable]
    public class Skill : IDisposable
    {
        [SerializeField] int    Basic_id;

        [SerializeField] string Basic_name;

        [SerializeField] int    Basic_class;
        [SerializeField] int    Basic_rank;

        [SerializeField] List<Skill_Lv>  Basic_lvList;

        ////////// Getter & Setter          //////////
        // Basic_id
        public int  Basic_VarId { get { return Basic_id;    }   }

        //
        // Basic_name
        public string   Basic_VarName   { get { return Basic_name;  }   }

        //
        // Basic_rank
        public int  Basic_VarRank   { get { return Basic_rank;  }   }

        //
        // Basic_lvList
        public int      Basic_VarLvCount                    { get { return Basic_lvList.Count;  }   }

        public Skill_Lv Basic_GetLvFromCount(int _count)    { return Basic_lvList[_count]; }

        ////////// Method                   //////////

        public void Basic_SetLvList(Dictionary<string, string> _data)
        {
            if(Basic_lvList == null)
            {
                Basic_lvList = new List<Skill_Lv>();
            }

            //
            int level = int.Parse(_data["cha_skill_lv"]);
            while(Basic_lvList.Count < level)
            {
                Basic_lvList.Add(null);
            }

            Basic_lvList[level - 1] = new Skill_Lv(_data);
        }

        ////////// Constructor & Destroyer  //////////
        public Skill(Dictionary<string, string> _data)
        {
            // cha_id
            Basic_id = int.Parse(_data["cha_skill_id"]);

            Basic_name = _data["cha_skill_name"];

            Basic_class = int.Parse(_data["cha_skill_class"]);
            Basic_rank  = int.Parse(_data["cha_skill_rank"]);
        }

        public void Dispose()
        {

        }
    }

    [Serializable]
    public class Skill_Lv : IDisposable
    {
        [SerializeField] int    Basic_id;

        [SerializeField] List<int>  Basic_values;

        [SerializeField] float  Basic_coolTime;

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////
        public float    Basic_VarCoolTime   { get { return Basic_coolTime;  }   }

        ////////// Constructor & Destroyer  //////////
        public Skill_Lv(Dictionary<string, string> _data)
        {
            Basic_id = int.Parse(_data["cha_skill_lv_id"]);
            //
            if(Basic_values == null)
            {
                Basic_values = new List<int>();
            }
            Skill_Lv__AddValue(_data, 1);
            Skill_Lv__AddValue(_data, 2);
            Skill_Lv__AddValue(_data, 3);

            // TODO: 쿨타임
            Basic_coolTime = 5.0f;
            //Basic_coolTime = float.Parse(_data["cha_skill_cool"]);
        }

        void Skill_Lv__AddValue(
            Dictionary<string, string> _data,
            int _count)
        {
            while(_count > Basic_values.Count)
            {
                Basic_values.Add(0);
            }

            Basic_values[_count - 1] = int.Parse(_data["value_0" + _count]);
        }

        public void Dispose()
        {
            
        }
    }

    #endregion

    #region ATTRIBUTE

    [Serializable]
    public class Attribute : IDisposable
    {
        [SerializeField] int    Basic_id;

        [SerializeField] string Basic_name;

        [SerializeField] int    Basic_class;

        [SerializeField] int    Basic_conditionId;  // 개방을 위한 선행 특성
        [SerializeField] int    Basic_conditionLv;  // 개방을 위한 선행 특성의 레벨

        [SerializeField] List<Attribute_Lv>  Basic_lvList;

        ////////// Getter & Setter          //////////
        // Basic_id
        public int  Basic_VarId { get { return Basic_id;    }   }

        // Basic_name
        public string   Basic_VarName   { get { return Basic_name;  }   }

        // Basic_class
        public int  Basic_VarClass  { get { return Basic_class; }   }

        // Basic_conditionId
        public int  Basic_VarConditionId    { get { return Basic_conditionId;   }   }

        // Basic_conditionLv
        public int  Basic_VarConditionLv    { get { return Basic_conditionLv;   }   }

        // Basic_lvList
        public Attribute_Lv Basic_VarLvFromCount(int _count)    { return Basic_lvList[_count];          }

        public int          Basic_VarLvListCount                { get { return Basic_lvList.Count;  }   }

        ////////// Method                   //////////
        public void Basic_AddLv(Dictionary<string, string> _data)
        {
            Attribute_Lv element = new Attribute_Lv(_data);

            int level = int.Parse(_data["cha_attribute_lv"]);
            while(Basic_lvList.Count < level)
            {
                Basic_lvList.Add(null);
            }
            Basic_lvList[level - 1] = element;
        }

        ////////// Constructor & Destroyer  //////////
        public Attribute(Dictionary<string, string> _data)
        {
            // cha_id
            Basic_id = int.Parse(_data["attribute_id"]);

            Basic_name = _data["cha_attribute_name"];

            Basic_class = int.Parse(_data["cha_attribute_class"]);
            
            Basic_conditionId = int.Parse(_data["attribute_condition_01"]);
            Basic_conditionLv = int.Parse(_data["condition_value_01"]);

            if(Basic_lvList == null)
            {
                Basic_lvList = new List<Attribute_Lv>();
            }
        }

        public void Dispose()
        {

        }
    }

    [Serializable]
    public class Attribute_Lv : IDisposable
    {
        public enum ABILITY
        {
            ATTACK_PER,
            DEFENCE_PER,
            HP_PER
        }

        public class Mateiral : IDisposable
        {
            [SerializeField] int    Basic_materialId;

            [SerializeField] int    Basic_count;

            ////////// Getter & Setter          //////////
            public int  Basic_VarMaterialId { get { return Basic_materialId;    }   }

            public int  Basic_VarCount      { get { return Basic_count;         }   }

            ////////// Method                   //////////

            ////////// Constructor & Destroyer  //////////

            public Mateiral(
                Dictionary<string, string> _data,
                int _count)
            {
                string countStr = "";
                int count = _count;
                int value = 10;
                while(value > 0)
                {
                    countStr += (count / value).ToString();
                    value /= 10;
                }
                
                Basic_materialId = int.Parse(_data["attribute_LvMaterials_" + countStr]);
                Basic_count      = int.Parse(_data["Materials_Value_" + countStr]);
            }

            public void Dispose()
            {

            }
        }
        
        [SerializeField] int    Basic_id;

        [SerializeField] int    Basic_class;

        [SerializeField] List<Mateiral> Basic_mateirals;

        [SerializeField] ABILITY    Basic_ability;
        [SerializeField] int        Basic_abilityValue;

        ////////// Getter & Setter          //////////
        
        // ABILITY
        public ABILITY  Basic_VarAbility        { get { return Basic_ability;       }   }

        public int      Basic_VarAbilityValue   { get { return Basic_abilityValue;  }   }

        ////////// Method                   //////////

        ////////// Constructor & Destroyer  //////////

        public Attribute_Lv(Dictionary<string, string> _data)
        {
            Basic_id = int.Parse(_data["attributeLv_id"]);

            Basic_class = int.Parse(_data["cha_attribute_class"]);

            if(Basic_mateirals == null)
            {
                Basic_mateirals = new List<Mateiral>();
            }
            Basic_mateirals.Add(new Mateiral(_data, 1));

            Basic_ability       = (ABILITY)Enum.Parse(typeof(ABILITY), _data["ability"]);
            Basic_abilityValue  = int.Parse(_data["value_01"]);
        }

        public void Dispose()
        {

        }
    }
    
    #endregion

    #region ABILITY

    [Serializable]
    public class Ability : IDisposable
    {
        [SerializeField] List<AbilityClass> Basic_classList;

        ////////// Getter & Setter          //////////
        //
        public AbilityClass Basic_GetListFromCount(int _count)  { return Basic_classList[_count];   }
        public AbilityClass Basic_GetRandomData
        {
            get
            {
                return Basic_classList[UnityEngine.Random.Range(0, Basic_classList.Count)];
            }
        }

        public int          Basic_VarListCount                  { get { return Basic_classList.Count;   }   }

        ////////// Method                   //////////
        public void Basic_Add(Dictionary<string, string> _data)
        {
            Basic_classList.Add(new AbilityClass(_data));
        }

        ////////// Constructor & Destroyer  //////////
        public Ability()
        {
            if(Basic_classList == null)
            {
                Basic_classList = new List<AbilityClass>();
            }
        }

        public void Dispose()
        {
            
        }
    }

    [Serializable]
    public class AbilityClass : IDisposable
    {
        public enum TYPE
        {
            ATTACK_PER,
            DEFENCE_PER,
            HP_PER,
            SKILL_DAMAGE_PER,
            MULTI_DAMAGE_PER,
            COUNTER_ATTACK_PER,
            CRITICAL_DAMAGE_PER,
            CRITICAL_DAMAGE_REDUCTION_PER,
            BOSS_DAMAGE_PER,
            NORMAL_DAMAGE_PER,
            ALL_DAMAGE_PER,
            LUCKY_DAMAGE_PER
        }

        [SerializeField] int    Basic_id;

        [SerializeField] string Basic_name;

        [SerializeField] TYPE   Basic_type;

        [SerializeField] float  Basic_minValue;
        [SerializeField] float  Basic_maxValue;

        ////////// Getter & Setter          //////////
        public int  Basic_VarId { get { return Basic_id;    }   }

        public string   Basic_VarName   { get { return Basic_name;  }   }

        public TYPE Basic_VarType   { get { return Basic_type;  }   }

        public float    Basic_VarMinValue   { get { return Basic_minValue;  }   }
        public float    Basic_VarMaxValue   { get { return Basic_maxValue;  }   }

        ////////// Method                   //////////

        ////////// Constructor & Destroyer  //////////
        public AbilityClass(Dictionary<string, string> _data)
        {
            Basic_id = int.Parse(_data["ability_id"]);

            Basic_name = _data["cha_ability_name"];

            Basic_type = (TYPE)Enum.Parse(typeof(TYPE), _data["cha_ability_type"]);

            Basic_minValue = int.Parse(_data["cha_ability_increase_min"]);
            Basic_maxValue = int.Parse(_data["cha_ability_increase_max"]);
        }

        public void Dispose()
        {
            
        }
    }

    public enum Ability_Probability_GRADE
    {
        E   = 0,
        D,
        C,
        B,
        A,
        S,
        SS,
        SSS
    }

    [Serializable]
    public class Ability_Probability : IDisposable
    {
        [SerializeField] int    Basic_id;
        
        [SerializeField] List<int>  Basic_percentage;

        ////////// Getter & Setter          //////////
        //
        public int Basic_VarLevel
        {
            get
            {
                return Basic_id % 100;
            }
        }

        public int  Basic_GetPercentageFromCount(int _count)    { return Basic_percentage[_count];  }

        ////////// Method                   //////////

        ////////// Constructor & Destroyer  //////////
        
        //
        public Ability_Probability(Dictionary<string, string> _data)
        {
            // Basic_id
            Basic_id = int.Parse(_data["abilityLv"]);

            // Basic_percentage
            if(Basic_percentage == null)        { Basic_percentage = new List<int>();   }
            while(Basic_percentage.Count < 8)   { Basic_percentage.Add(0);              }

            Ability_Probability__Grade(_data,   Ability_Probability_GRADE.E     );
            Ability_Probability__Grade(_data,   Ability_Probability_GRADE.D     );
            Ability_Probability__Grade(_data,   Ability_Probability_GRADE.C     );
            Ability_Probability__Grade(_data,   Ability_Probability_GRADE.B     );
            Ability_Probability__Grade(_data,   Ability_Probability_GRADE.A     );
            Ability_Probability__Grade(_data,   Ability_Probability_GRADE.S     );
            Ability_Probability__Grade(_data,   Ability_Probability_GRADE.SS    );
            Ability_Probability__Grade(_data,   Ability_Probability_GRADE.SSS   );
        }

        void Ability_Probability__Grade(
            Dictionary<string, string> _data,
            //
            Ability_Probability_GRADE _grade)
        {
            Basic_percentage[(int)_grade] = int.Parse(_data["cha_ability_" + _grade.ToString()]);
        }

        //
        public void Dispose()
        {
            
        }
    }

    #endregion

    #region SAVE
    
    [Serializable]
    public class Save : IDisposable
    {
        public class Skill : IDisposable
        {
            [SerializeField] int    Basic_id;
            [SerializeField] int    Basic_level;

            ////////// Getter & Setter          //////////
            public int  Basic_VarId     { get { return Basic_id;    } set { Basic_id = value;   }  }

            public int  Basic_VarLevel  { get { return Basic_level; }   }
        
            ////////// Method                   //////////
        
            ////////// Constructor & Destroyer  //////////
            public Skill()
            {
                Basic_id = -1;
            }

            public void Dispose()
            {

            }
        }

        [SerializeField] int    Basic_inventoryId;
        [SerializeField] int    Basic_dataId;

        [SerializeField] int    Basic_level;

        [SerializeField] List<int>  Basic_equipments;
        
        [SerializeField] int    Basic_skillLv;

        [SerializeField] List<int>  Basic_contellationLvs;

        [SerializeField] protected List<int>    Basic_cards;

        ////////// Getter & Setter          //////////
        public int  Basic_VarInventoryId    { get { return Basic_inventoryId;   }   }

        public int  Basic_VarDataId { get { return Basic_dataId;    } set { Basic_dataId = value;   }   }

        //
        public int  Basic_VarLevel  { get { return Basic_level; } set { Basic_level = value;    }   }

        //
        public List<int>    Basic_VarEquipments { get { return Basic_equipments;    }   }

        //
        public int  Basic_VarSkillLv    { get { return Basic_skillLv;   }   }

        //
        public int  Basic_GetConstellationLv(int _count)
        {
            int res = 0;

            //
            while(Basic_contellationLvs.Count <= _count)
            {
                Basic_contellationLvs.Add(1);
            }
            res = Basic_contellationLvs[_count];

            //
            return res;
        }

        //
        public void Basic_SetCard(int _count, int _id)  { Basic_cards[_count] = _id;    }

        ////////// Method                   //////////
        public void Basic_EquipmentReset()
        {
            for(int for0 = 0; for0 < Basic_equipments.Count; for0++)
            {
                Basic_equipments[for0] = -1;
            }
        }

        public void Basic_Equipment(string _socketName, int _inventoryId)
        {
            string[] socketNames = _socketName.Split('_');
            Giggle_Item.TYPE itemType = (Giggle_Item.TYPE)Enum.Parse(typeof(Giggle_Item.TYPE), socketNames[0]);

            int count = (int)itemType;
            if(itemType.Equals(Giggle_Item.TYPE.ACCESSORY))
            {
                count += int.Parse(socketNames[1]);
            }

            //
            Basic_Equipment(count, _inventoryId);
        }

        public void Basic_Equipment(int _count, int _inventoryId)
        {
            // Basic_equipments
            while(Basic_equipments.Count <= _count)
            {
                Basic_equipments.Add(-1);
            }

            Basic_equipments[_count] = _inventoryId;
        }

        ////////// Constructor & Destroyer  //////////
        
        ////////// Constructor
        //
        public Save(int _dataId)
        {
            Basic_Constructor();

            Basic_dataId = _dataId;

            Basic_level = 1;
            
            Basic_skillLv = -1;
        }

        public Save(int _inventoryId, int _dataId)
        {
            Basic_Constructor();

            Basic_inventoryId = _inventoryId;
            Basic_dataId = _dataId;

            Basic_level = 1;
            
            Basic_skillLv = 1;
        }

        void Basic_Constructor()
        {
            if(Basic_equipments == null)
            {
                Basic_equipments = new List<int>();
            }

            if(Basic_contellationLvs == null)
            {
                Basic_contellationLvs = new List<int>();
            }

            if(Basic_cards == null)
            {
                Basic_cards = new List<int>();
            }
            while(Basic_cards.Count < 3)
            {
                Basic_cards.Add(-1);
            }
        }

        //////////
        //
        public void Dispose()
        {
            
        }
    }

    #endregion
}