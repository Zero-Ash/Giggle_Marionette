using UnityEngine;

//
using System;
using System.Collections.Generic;

namespace Giggle_Character
{
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
            res = (int)((float)res * (1.0f + ((float)_bouns * 0.01f)));

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

            Basic_attack    = int.Parse(_data["Cha_Attack"]);

            Basic_defence   = int.Parse(_data["Cha_Defense"]);

            Basic_hp    = int.Parse(_data["Cha_HP"]);

            Basic_attackSpeed   = float.Parse(_data["Cha_Attack_Speed"]);

            Basic_criticalChance            = int.Parse(_data["Cha_CriticalChance"]         );
            Basic_criticalDamage            = int.Parse(_data["Cha_CriticalDamage"]         );
            Basic_criticalDamageReduction   = int.Parse(_data["Cha_CriticalDamageReduction"]);

            Basic_luckyChance   = int.Parse(_data["Cha_LuckyChance"]);
            Basic_luckyDamage   = int.Parse(_data["Cha_LuckyDamage"]);

            Basic_damageTakenReduction  = int.Parse(_data["Cha_DamageTakenReduction"]   );
            Basic_damageIncrease        = int.Parse(_data["Cha_DamageIncrease"]         );

            Basic_hpRegenPerSecond  = int.Parse(_data["Cha_HPRegenPerSecond"]   );
            Basic_hpRegenAmount     = int.Parse(_data["Cha_HPRegenAmount"]      );
            Basic_hpLifeSteal       = int.Parse(_data["Cha_LifeSteal"]          );

            Basic_goldGainIncrease = int.Parse(_data["Cha_GoldGainIncrease"]);

            Basic_allDamageIncrease = int.Parse(_data["Cha_AllDamageIncrease"]);

            Basic_normalMonsterDamageIncrease   = int.Parse(_data["Cha_NormalMonsterDamageIncrease"]);
            Basic_bossDamageIncrease            = int.Parse(_data["Cha_BossDamageIncrease"]         );

            Basic_skillCooldownReduction    = int.Parse(_data["Cha_CooldownReduction"]      );
            Basic_skillDamageIncrease       = int.Parse(_data["Cha_SkillDamageIncrease"]    );
            Basic_skillDamageReduction      = int.Parse(_data["Cha_SkillDamageReduction"]   );
            Basic_skillCritical             = int.Parse(_data["Cha_SkillCritical"]          );
            Basic_skillCriticalDamage       = int.Parse(_data["Cha_SkillCriticalDamage"]    );

            Basic_stun              = int.Parse(_data["Cha_Stun"]           );
            Basic_stunResistance    = int.Parse(_data["Cha_StunResistance"] );

            Basic_multiHit                  = int.Parse(_data["Cha_MultiHit"]               );
            Basic_multiHitDamage            = int.Parse(_data["Cha_MultiHitDamage"]         );
            Basic_multiHitDamageReduction   = int.Parse(_data["Cha_MultiHitDamageReduction"]);

            Basic_counterAttack                 = int.Parse(_data["Cha_Counterattack"]                  );
            Basic_counterAttackDamage           = int.Parse(_data["Cha_CounterattackDamage"]            );
            Basic_counterAttackDamageReduction  = int.Parse(_data["Cha_CounterattackDamageReduction"]   );

            Basic_evasion = int.Parse(_data["Cha_Evasion"]);

            Basic_accuracy = int.Parse(_data["Cha_Accuracy"]);
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

        [SerializeField] List<Skill>    Basic_skillList;

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
            return Basic_statusList[_level];
        }

        public void Basic_SetStatusList(Dictionary<string, string> _data)
        {
            if(Basic_statusList == null)
            {
                Basic_statusList = new List<Status>();
            }

            //
            int level = int.Parse(_data["Cha_Level"]);
            while(Basic_statusList.Count < level)
            {
                Basic_statusList.Add(null);
            }

            Basic_statusList[level - 1] = new Status(_data);
        }

        // Basic_skillList
        public Skill Basic_GetSkillListFromId(int _id)
        {
            Skill res = null;

            //
            for(int for0 = 0; for0 < Basic_skillList.Count; for0++)
            {
                if(Basic_skillList[for0].Basic_VarId.Equals(_id))
                {
                    res = Basic_skillList[for0];
                }
            }

            //
            return res;
        }

        public Skill Basic_GetSkillListFromCount(int _count)    { return Basic_skillList[_count];   }

        public void Basic_SetSkillList(Dictionary<string, string> _data)
        {
            if(Basic_skillList == null)
            {
                Basic_skillList = new List<Skill>();
            }

            //
            Basic_skillList.Add(new Skill(_data));
        }

        // Marionette
        //
        public virtual int  Marionette_VarAttribute { get { return -1;  }   }

        //
        public virtual int  Marionette_VarRole      { get { return -1;  }   }


        ////////// Method                   //////////

        ////////// Constructor & Destroyer  //////////
        public Database()
        {
            Basic_id = -1;
        }

        public Database(Dictionary<string, string> _data)
        {
            Basic_id        = int.Parse(_data["cha_id"]);
            Basic_name      = _data["Cha_Name"];
        }

        //
        public void Dispose()
        {

        }
    }

    [Serializable]
    public class Database_Marionette : Database
    {
        [SerializeField] protected int  Basic_grade;
        [SerializeField] protected int  Basic_attribute;
        [SerializeField] protected int  Basic_equipment;
        [SerializeField] protected int  Basic_role;

        ////////// Getter & Setter          //////////

        // Basic_attribute
        public override int Marionette_VarAttribute { get { return Basic_attribute; }   }

        // Basic_role
        public override int Marionette_VarRole      { get { return Basic_role;      }   }

        ////////// Method                   //////////

        ////////// Constructor & Destroyer  //////////
        public Database_Marionette(Dictionary<string, string> _data) : base(_data)
        {
            Basic_grade     = int.Parse(_data["Cha_Grade"]);
            Basic_attribute = int.Parse(_data["Cha_Attribute"]);
            Basic_equipment = int.Parse(_data["Cha_Equipment"]);
            Basic_role      = int.Parse(_data["Cha_Role"]);
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

        // Basic_lvList
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
            Basic_id = int.Parse(_data["skill_id"]);

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
            Basic_id = int.Parse(_data["skill_id"]);
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

    #region SAVE
    
    [Serializable]
    public class Save : IDisposable
    {
        [SerializeField] int    Basic_inventoryId;
        [SerializeField] int    Basic_dataId;

        ////////// Getter & Setter          //////////
        public int  Basic_VarInventoryId    { get { return Basic_inventoryId;   }   }

        public int  Basic_VarDataId { get { return Basic_dataId;    } set { Basic_dataId = value;   }   }

        ////////// Method                   //////////

        ////////// Constructor & Destroyer  //////////
        public Save(int _inventoryId, int _dataId)
        {
            Basic_inventoryId = _inventoryId;
            Basic_dataId = _dataId;
        }

        public void Dispose()
        {
            
        }
    }

    #endregion
}