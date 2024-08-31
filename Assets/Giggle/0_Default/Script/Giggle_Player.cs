using UnityEngine;

//
using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class Giggle_Player : IDisposable
{
    #region BASIC

    ////////// Getter & Setter          //////////

    ////////// Method                   //////////
    public void Basic_Init()
    {
        Stage_Contructor();
        Pinocchio_Contructor();
        Marionette_Contructor();
        Formation_Contructor();
        Item_Contructor();
        Power_Contructor();
    }

    ////////// Constructor & Destroyer  //////////
    public Giggle_Player()
    {
    }

    public void Dispose()
    {

    }

    #endregion

    #region STAGE

    [Header("STAGE ==================================================")]
    [SerializeField] int    Stage_max;
    [SerializeField] int    Stage_select;

    ////////// Getter & Setter          //////////
    // Stage_select
    object Stage_VarSelect(params object[] _args)
    {
        return Stage_select;
    }

    ////////// Method                   //////////
    
    ////////// Constructor & Destroyer  //////////
    void Stage_Contructor()
    {
        Stage_max = Stage_select = 0;

        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__STAGE__VAR_SELECT,  Stage_VarSelect );
    }
    
    #endregion

    #region PINOCCHIO

    public enum Pinocchio_GENDER
    {
        MALE = 1,
        FEMALE
    }

        #region PINOCCHIO_EQUIPMENT

    [Serializable]
    public class Pinocchio_Equipment : IDisposable
    {
        [SerializeField] List<int>  Basic_equips;

        ////////// Getter & Setter          //////////

        // Basic_equips
        public int  Basic_VarEquipsCount    { get { return Basic_equips.Count;  }   }

        public int  Basic_GetEquipmentId(int _count)                { return Basic_equips[_count];      }
        public void Basic_SetEquipmentId(int _count, int _value)    { Basic_equips[_count] = _value;    }

        ////////// Method                   //////////

        // Basic_equips
        public void Basic_EquipmentAdd()    { Basic_equips.Add(-1); }

        ////////// Constructor & Destroyer  //////////
        public Pinocchio_Equipment()
        {
            if(Basic_equips == null)
            {
                Basic_equips = new List<int>();
            }
        }

        //
        public void Dispose()
        {

        }
    }
        #endregion

        #region PINOCCHIO_SKILL
        
    [Serializable]
    public class Pinocchio_Skill : IDisposable
    {
        [SerializeField] int    Basic_id;   // 스킬의 id
        [SerializeField] int    Basic_lv;   // 스킬의 레벨

        ////////// Getter & Setter          //////////
        public int  Basic_VarId { get { return Basic_id;    }   }

        public int  Basic_VarLv { get { return Basic_lv;    } set { Basic_lv = value;   }   }

        ////////// Method                   //////////

        ////////// Constructor & Destroyer  //////////
        public Pinocchio_Skill(int _id)
        {
            Basic_id = _id;
            Basic_lv = 1;
        }
        
        //
        public void Dispose()
        {

        }
    }

    [Serializable]
    public class Pinocchio_SkillSlots : IDisposable
    {
        [SerializeField] List<int>    Basic_list;

        ////////// Getter & Setter          //////////
        public List<int>    Basic_VarList   { get { return Basic_list;  }   }

        ////////// Method                   //////////

        ////////// Constructor & Destroyer  //////////
        public Pinocchio_SkillSlots()
        {
            if(Basic_list == null)
            {
                Basic_list = new List<int>();
            }
            while(Basic_list.Count < 6)
            {
                Basic_list.Add(-1);
            }
        }
        
        //
        public void Dispose()
        {

        }
    }

        #endregion

    [Serializable]
    public class Pinocchio_Ability : IDisposable
    {
        [SerializeField] int    Basic_id;
        [SerializeField] int    Basic_grade;
        [SerializeField] float  Basic_value;
        [SerializeField] bool   Basic_isLock;

        ////////// Getter & Setter          //////////
        public int  Basic_VarId { get { return Basic_id;    }   }

        public int  Basic_VarGrade  { get { return Basic_grade; }   }

        public float    Basic_VarValue  { get { return Basic_value; }   }

        public bool Basic_VarIsLock { get { return Basic_isLock;    } set { Basic_isLock = value;   }   }

        ////////// Method                   //////////
        // 재능 변경
        public void Basic_Change(int _grade)
        {
            Basic_grade = _grade;

            //
            Giggle_Character.AbilityClass data
                = (Giggle_Character.AbilityClass)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.DATABASE__PINOCCHIO__GET_ABILITY_RANDOM_FROM_GRADE,
                    //
                    _grade);

            Basic_id = data.Basic_VarId;
            
            Basic_value
                = MathF.Round(
                    UnityEngine.Random.Range(data.Basic_VarMinValue, data.Basic_VarMaxValue),
                    2);
        }

        ////////// Constructor & Destroyer  //////////
        public Pinocchio_Ability()
        {
            Basic_id = -1;

            Basic_isLock = false;
        }

        //
        public void Dispose()
        {

        }
    }

    [Serializable]
    public class Pinocchio_Relic : IDisposable
    {
        [SerializeField] int    Basic_dataId;
        [SerializeField] int    Basic_inventoryId;

        [SerializeField] int    Basic_level;

        [SerializeField] List<int>  Basic_buffs;

        ////////// Getter & Setter          //////////
        
        // Basic_dataId
        public int  Basic_VarDataId { get { return Basic_dataId;        }   }

        // Basic_inventoryId
        public int  Basic_VarInventoryId    { get { return Basic_inventoryId;   }   }

        // Basic_level
        public int  Basic_VarLevel  { get { return Basic_level; }   }

        // Basic_buffs
        public int  Basic_GetBuffFromCount(int _count)              { return Basic_buffs[_count];   }

        public void Basic_SetBuffFromCount(int _count, int _value)  { Basic_buffs[_count] = _value; }

        public int  Basic_VarBuffCount                              { get { return Basic_buffs.Count;   }   }

        ////////// Method                   //////////
        //
        public void  Basic_BuffAddValueFromCount(int _count, int _value)    { Basic_buffs[_count] += _value;    }

        // Basic_buffs
        public void Basic_BuffReset()
        {
            for(int for0 = 0; for0 < Basic_buffs.Count; for0++)
            {
                Basic_buffs[for0] = 0;
            }
        }

        ////////// Constructor & Destroyer  //////////
        public Pinocchio_Relic(int _dataId, int _inventoryId)
        {
            Basic_dataId = _dataId;
            Basic_inventoryId = _inventoryId;

            Basic_level = 1;

            Basic_buffs = new List<int>();
            while(Basic_buffs.Count < (int)Giggle_Item.Relic_COLOR.TOTAL)
            {
                Basic_buffs.Add(0);
            }
        }

        //
        public void Dispose()
        {

        }
    }

    [Serializable]
    public class Pinocchio_RelicSlot : IDisposable
    {
        [SerializeField] int Basic_inventoryId;

        ////////// Getter & Setter          //////////
        public int  Basic_VarInventoryId    { get { return Basic_inventoryId;   } set { Basic_inventoryId = value;  }   }

        ////////// Method                   //////////

        ////////// Constructor & Destroyer  //////////
        public Pinocchio_RelicSlot()
        {
            Basic_inventoryId = -1;
        }

        //
        public void Dispose()
        {

        }
    }

    [Header("PINOCCHIO ==================================================")]
    [SerializeField] Giggle_Character.Save  Pinocchio_data;

    [SerializeField] Pinocchio_GENDER   Pinocchio_gender;

    ////////// Getter & Setter          //////////
    // Pinocchio_data
    object Pinocchio_VarData(params object[] _args)
    {
        return Pinocchio_data;
    }
    ////////// Method                   //////////

    ////////// Constructor & Destroyer  //////////
    void Pinocchio_Contructor()
    {
        Pinocchio_data = new Giggle_Character.Save(1111);

        //
        Pinocchio_gender = Pinocchio_GENDER.MALE;

        PinocchioJobs_Contructor();
        PinocchioEquips_Contructor();
        PinocchioSkills_Contructor();
        PinocchioAttributes_Contructor();
        PinocchioAbility_Contructor();
        PinocchioRelic_Contructor();

        // TODO:테스트용 임시데이터
        Pinocchio_jobs.Add(1111);
        Pinocchio_jobs.Add(1112);
        Pinocchio_jobs.Add(1113);

        Pinocchio_skills.Add(new Pinocchio_Skill(1310011));

        Pinocchio_RelicInsert(1611001);
        Pinocchio_RelicInsert(1611001);
        Pinocchio_RelicInsert(1611001);
        Pinocchio_RelicInsert(1612001);
        Pinocchio_RelicInsert(1612001);
        Pinocchio_RelicInsert(1612001);
    }

        #region PINOCCHIO_JOBS

    [SerializeField] List<int>  Pinocchio_jobs;

    ////////// Getter & Setter          //////////

    // Pinocchio_jobs
    object Pinocchio_VarJobs(params object[] _args)
    {
        List<int> res = new List<int>();

        for(int for0 = 0; for0 < Pinocchio_jobs.Count; for0++)
        {
            res.Add(Pinocchio_jobs[for0]);
        }

        return res;
    }
    
    ////////// Method                   //////////
    
    ////////// Constructor & Destroyer  //////////
    void PinocchioJobs_Contructor()
    {
        if(Pinocchio_jobs == null)  { Pinocchio_jobs = new List<int>(); }

        //
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__VAR_JOBS,    Pinocchio_VarJobs   );
    }
    
        #endregion

        #region PINOCCHIO_EQUIPS

    [SerializeField] List<Pinocchio_Equipment>  Pinocchio_equips;
    [SerializeField] int                        Pinocchio_equipSelect;

    ////////// Getter & Setter          //////////
    
    object Pinocchio_EquipmentVarSelectFromCount(params object[] _args)
    {
        int count = (int)_args[0];

        //
        int res = -1;
        if(Pinocchio_equips[Pinocchio_equipSelect].Basic_VarEquipsCount > count)
        {
            res = Pinocchio_equips[Pinocchio_equipSelect].Basic_GetEquipmentId(count);
        }

        //
        return res;
    }

    ////////// Method                   //////////

    object Pinocchio_EquipmentSaveSelect(params object[] _args)
    {
        int id = (int)_args[0];

        //
        Pinocchio_equipSelect = id;

        //
        Pinocchio_data.Basic_EquipmentReset();
        for(int for0 = 0; for0 < Pinocchio_equips[Pinocchio_equipSelect].Basic_VarEquipsCount; for0++)
        {
            Pinocchio_data.Basic_Equipment(for0, Pinocchio_equips[Pinocchio_equipSelect].Basic_GetEquipmentId(for0));
        }

        //
        return true;
    }

    object Pinocchio_EquipmentItem(params object[] _args)
    {
        string  socketName  = (string)_args[0];
        int     inventoryId = (int)_args[1];

        //
        string[] socketNames = socketName.Split('_');
        Giggle_Item.TYPE itemType = (Giggle_Item.TYPE)Enum.Parse(typeof(Giggle_Item.TYPE), socketNames[0]);

        int count = (int)itemType;
        if(itemType.Equals(Giggle_Item.TYPE.ACCESSORY))
        {
            count += int.Parse(socketNames[1]);
        }

        // Basic_equipments
        while(Pinocchio_equips[Pinocchio_equipSelect].Basic_VarEquipsCount <= count)
        {
            Pinocchio_equips[Pinocchio_equipSelect].Basic_EquipmentAdd();
        }

        Pinocchio_equips[Pinocchio_equipSelect].Basic_SetEquipmentId(count, inventoryId);

        //
        Pinocchio_data.Basic_Equipment(count, Pinocchio_equips[Pinocchio_equipSelect].Basic_GetEquipmentId(count));

        //
        return true;
    }
    
    ////////// Constructor & Destroyer  //////////
    void PinocchioEquips_Contructor()
    {
        if(Pinocchio_equips == null)    { Pinocchio_equips = new List<Pinocchio_Equipment>(); }
        while(Pinocchio_equips.Count < 3)
        {
            Pinocchio_equips.Add(new Pinocchio_Equipment());
        }
        //
        Pinocchio_equipSelect = 0;

        //
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__EUIPMENT_VAR_SELECT_FROM_COUNT,  Pinocchio_EquipmentVarSelectFromCount   );

        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__EUIPMENT_SAVE_SELECT,            Pinocchio_EquipmentSaveSelect           );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__EUIPMENT_ITEM,                   Pinocchio_EquipmentItem                 );
    }
    
        #endregion

        #region PINOCCHIO_SKILLS

    [SerializeField] List<Pinocchio_Skill>      Pinocchio_skills;
    [SerializeField] List<Pinocchio_SkillSlots> Pinocchio_skillSlots;
    [SerializeField] int                        Pinocchio_selectSkillSlot;

    ////////// Getter & Setter          //////////

    // Pinocchio_skills
    object Pinocchio_VarSkills(params object[] _args)
    {
        return Pinocchio_skills;
    }

    object Pinocchio_VarSkillFromId(params object[] _args)
    {
        int id = (int)_args[0];

        //
        Pinocchio_Skill res = null;

        // Get
        for(int for0 = 0; for0 < Pinocchio_skills.Count; for0++)
        {
            if(Pinocchio_skills[for0].Basic_VarId.Equals(id))
            {
                res = Pinocchio_skills[for0];
                break;
            }
        }

        // Set
        if(_args.Length >= 2)
        {
            int lv = (int)_args[1];

            if(res == null)
            {
                res = new Pinocchio_Skill(id);
                Pinocchio_skills.Add(res);
            }

            res.Basic_VarLv = lv;
        }

        if(res == null)
        {
            res = new Pinocchio_Skill(-1);
        }

        //
        return res;
    }

    // Pinocchio_skillSlots
    object Pinocchio_VarSkillSlots(params object[] _args)
    {
        return Pinocchio_skillSlots[Pinocchio_selectSkillSlot].Basic_VarList;
    }

    object Pinocchio_VarSkillSlotFromCount(params object[] _args)
    {
        int count = (int)_args[0];

        //
        if(_args.Length >= 2)
        {
            int id = (int)_args[1];

            Pinocchio_skillSlots[Pinocchio_selectSkillSlot].Basic_VarList[count] = id;
        }

        //
        return Pinocchio_skillSlots[Pinocchio_selectSkillSlot].Basic_VarList[count];
    }
    
    ////////// Method                   //////////
    
    ////////// Constructor & Destroyer  //////////

    void PinocchioSkills_Contructor()
    {
        //
        if(Pinocchio_skills == null)    { Pinocchio_skills = new List<Pinocchio_Skill>();   }
        //
        if(Pinocchio_skillSlots == null)    { Pinocchio_skillSlots = new List<Pinocchio_SkillSlots>();  }
        while(Pinocchio_skillSlots.Count < 3)
        {
            Pinocchio_skillSlots.Add(new Pinocchio_SkillSlots());
        }

        //
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__VAR_SKILLS,                  Pinocchio_VarSkills             );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__VAR_SKILL_FROM_ID,           Pinocchio_VarSkillFromId        );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__VAR_SKILL_SLOTS,             Pinocchio_VarSkillSlots         );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__VAR_SKILL_SLOT_FROM_COUNT,   Pinocchio_VarSkillSlotFromCount );
    }
    
        #endregion

        #region PINOCCHIO_ATTRIBUTES

    [SerializeField] List<Pinocchio_Skill>  Pinocchio_attributeAttacks;

    ////////// Getter & Setter          //////////

    // Pinocchio_attributeAttacks
    object Pinocchio_VarAttributeAttackFromId(params object[] _args)
    {
        int id = (int)_args[0];

        //
        Pinocchio_Skill res = null;

        for(int for0 = 0; for0 < Pinocchio_attributeAttacks.Count; for0++)
        {
            if(Pinocchio_attributeAttacks[for0].Basic_VarId.Equals(id))
            {
                res = Pinocchio_attributeAttacks[for0];
                break;
            }
        }

        if(res == null)
        {
            res = new Pinocchio_Skill(id);
            res.Basic_VarLv = 0;
            Pinocchio_attributeAttacks.Add(res);
        }

        //
        return res;
    }
    
    ////////// Method                   //////////
    
    // Pinocchio_attributeAttacks
    object Pinocchio_AttributeAttackLevelUp(params object[] _args)
    {
        int id = (int)_args[0];

        //
        Pinocchio_Skill element = null;

        for(int for0 = 0; for0 < Pinocchio_attributeAttacks.Count; for0++)
        {
            if(Pinocchio_attributeAttacks[for0].Basic_VarId.Equals(id))
            {
                element = Pinocchio_attributeAttacks[for0];
                break;
            }
        }

        element.Basic_VarLv += 1;

        //
        return true;
    }
    
    ////////// Constructor & Destroyer  //////////

    void PinocchioAttributes_Contructor()
    {
        //
        if(Pinocchio_attributeAttacks == null)  { Pinocchio_attributeAttacks = new List<Pinocchio_Skill>(); }

        //
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__VAR_ATTRIBUTE_ATTACK_FROM_ID,    Pinocchio_VarAttributeAttackFromId  );

        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__ATTRIBUTE_ATTACK_LEVEL_UP,       Pinocchio_AttributeAttackLevelUp    );
    }
    
        #endregion

        #region PINOCCHIO_ABILITY

    [SerializeField] List<Pinocchio_Ability>    Pinocchio_abilitys;
    [SerializeField] int                        Pinocchio_abilityLevel;
    [SerializeField] int                        Pinocchio_abilityExp;

    ////////// Getter & Setter          //////////

    // Pinocchio_abilitys
    object Pinocchio_AbilityGetAbilityFromCount(params object[] _args)
    {
        return Pinocchio_abilitys[(int)_args[0]];
    }

    object Pinocchio_AbilityGetAbilitysCount(params object[] _args)
    {
        return Pinocchio_abilitys.Count;
    }

    // 
    object Pinocchio_AbilityGetLevel(params object[] _args)
    {
        return Pinocchio_abilityLevel;
    }
    
    ////////// Method                   //////////
    
    //
    object Pinocchio_AbilityChange(params object[] _args)
    {
        Giggle_Character.Ability_Probability perData
            = (Giggle_Character.Ability_Probability)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.DATABASE__PINOCCHIO__ABILITY_GET_PROBABILITY_FROM_LEVEL,
                Pinocchio_abilityLevel);
        int perValue = UnityEngine.Random.Range(0, 100);
        int whileCount0 = 0;
        int whileCount1 = 0;
        while(whileCount0 < Pinocchio_abilitys.Count)
        {
            if(!Pinocchio_abilitys[whileCount0].Basic_VarIsLock)
            {
                whileCount1 = 0;
            
                while(whileCount1 <= (int)Giggle_Character.Ability_Probability_GRADE.SSS)
                {
                    if(perValue < perData.Basic_GetPercentageFromCount(whileCount1))
                    {
                        Pinocchio_abilitys[whileCount0].Basic_Change(whileCount1);
                        break;
                    }
                    else
                    {
                        perValue -= perData.Basic_GetPercentageFromCount(whileCount1);
                        whileCount1++;
                    }
                }
            }

            whileCount0++;
        }

        //
        return true;
    }
    
    ////////// Constructor & Destroyer  //////////

    void PinocchioAbility_Contructor()
    {
        //
        if(Pinocchio_abilitys == null)  { Pinocchio_abilitys = new List<Pinocchio_Ability>();   }
        while(Pinocchio_abilitys.Count < 6)
        {
            Pinocchio_abilitys.Add(new Pinocchio_Ability());
        }
        Pinocchio_abilityLevel = 1;

        //
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__ABILITY_GET_ABILITY_FROM_COUNT,  Pinocchio_AbilityGetAbilityFromCount    );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__ABILITY_GET_ABILITYS_COUNT,      Pinocchio_AbilityGetAbilitysCount       );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__ABILITY_GET_LEVEL,               Pinocchio_AbilityGetLevel               );

        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__ABILITY_CHANGE,                  Pinocchio_AbilityChange                 );
    }
    
        #endregion

        #region PINOCCHIO_RELIC

    [SerializeField] List<Pinocchio_Relic>      Pinocchio_relics;
    [SerializeField] List<Pinocchio_RelicSlot>  Pinocchio_relicSlots;

    ////////// Getter & Setter          //////////

    //
    object Pinocchio_RelicVarRelics__Bridge(params object[] _args)
    {
        return Pinocchio_relics;        
    }

    //
    object Pinocchio_RelicVarRelicSlots__Bridge(params object[] _args)
    {
        return Pinocchio_relicSlots;        
    }

    // RelicVarSlotsColorIsSame
    bool Pinocchio_RelicVarSlotsColorIsSame(int _slot0, int _slot1)
    {
        //
        bool res = false;

        if( Pinocchio_relicSlots[_slot0].Basic_VarInventoryId.Equals(-1) ||
            Pinocchio_relicSlots[_slot1].Basic_VarInventoryId.Equals(-1))
        {
            
        }
        else
        {
            Giggle_Item.Relic data0
                = (Giggle_Item.Relic)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.DATABASE__PINOCCHIO__RELIC_VAR_DATA_FROM_ID,
                    Pinocchio_GetRelicFromInventoryId(Pinocchio_relicSlots[_slot0].Basic_VarInventoryId).Basic_VarDataId);
            Giggle_Item.Relic data1
                = (Giggle_Item.Relic)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.DATABASE__PINOCCHIO__RELIC_VAR_DATA_FROM_ID,
                    Pinocchio_GetRelicFromInventoryId(Pinocchio_relicSlots[_slot1].Basic_VarInventoryId).Basic_VarDataId);

            res = data0.Basic_VarColor.Equals(data1.Basic_VarColor);
        }

        //
        return res;        
    }

    object Pinocchio_RelicVarSlotsColorIsSame__Bridge(params object[] _args)
    {
        int slot0 = (int)_args[0];
        int slot1 = (int)_args[1];

        //
        return Pinocchio_RelicVarSlotsColorIsSame(slot0, slot1);        
    }

    // Pinocchio_GetRelicFromInventoryId
    Pinocchio_Relic Pinocchio_GetRelicFromInventoryId(int _inventoryId)
    {
        Pinocchio_Relic res = null;

        //
        for(int for0 = 0; for0 < Pinocchio_relics.Count; for0++)
        {
            if(Pinocchio_relics[for0].Basic_VarInventoryId.Equals(_inventoryId))
            {
                res = Pinocchio_relics[for0];
                break;
            }
        }

        //
        return res;
    }
    
    ////////// Method                   //////////
    
    object Pinocchio_RelicSlotChange(params object[] _args)
    {
        int inventory   = (int)_args[0];
        int slot        = (int)_args[1];

        //
        if(Pinocchio_relicSlots[slot].Basic_VarInventoryId != -1)
        {
            Pinocchio_GetRelicFromInventoryId(Pinocchio_relicSlots[slot].Basic_VarInventoryId).Basic_BuffReset();
        }

        Pinocchio_relicSlots[slot].Basic_VarInventoryId = Pinocchio_relics[inventory].Basic_VarInventoryId;

        //
        return true;
    }

    // Pinocchio_RelicBuff
    static List<int> Pinocchio_RelicBuff__checkBuffs;

    void Pinocchio_RelicBuff()
    {
        Pinocchio_RelicBuff__Check(1, 1);
        Pinocchio_RelicBuff__Check(4, 1);
        Pinocchio_RelicBuff__Check(7, 1);

        Pinocchio_RelicBuff__Check(3, 3);
        Pinocchio_RelicBuff__Check(4, 3);
        Pinocchio_RelicBuff__Check(5, 3);
    }

    void Pinocchio_RelicBuff__Check(int _count, int _countRange)
    {
        //
        if(Pinocchio_RelicBuff__checkBuffs == null)
        {
            Pinocchio_RelicBuff__checkBuffs = new List<int>();
        }
        while(Pinocchio_RelicBuff__checkBuffs.Count < (int)Giggle_Item.Relic_COLOR.TOTAL)
        {
            Pinocchio_RelicBuff__checkBuffs.Add(0);
        }

        for(int for0 = 0; for0 < Pinocchio_RelicBuff__checkBuffs.Count; for0++)
        {
            Pinocchio_RelicBuff__checkBuffs[for0] = 0;
        }

        //
        bool isSame0 = Pinocchio_RelicVarSlotsColorIsSame(_count - _countRange, _count              );
        bool isSame1 = Pinocchio_RelicVarSlotsColorIsSame(_count,               _count + _countRange);

        if(isSame0 || isSame1)
        {
            Pinocchio_Relic relic = Pinocchio_GetRelicFromInventoryId(Pinocchio_relicSlots[_count].Basic_VarInventoryId);

            Giggle_Item.Relic data
                = (Giggle_Item.Relic)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.DATABASE__PINOCCHIO__RELIC_VAR_DATA_FROM_ID,
                    Pinocchio_GetRelicFromInventoryId(Pinocchio_relicSlots[_count].Basic_VarInventoryId).Basic_VarDataId);

            Pinocchio_RelicBuff__checkBuffs[(int)data.Basic_VarColor - 1] = 10;

            //
            data
                = (Giggle_Item.Relic)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.DATABASE__PINOCCHIO__RELIC_VAR_DATA_FROM_ID,
                    Pinocchio_GetRelicFromInventoryId(Pinocchio_relicSlots[_count - _countRange].Basic_VarInventoryId).Basic_VarDataId);
            Pinocchio_RelicBuff__checkBuffs[(int)data.Basic_VarColor - 1] += 10;

            //
            data
                = (Giggle_Item.Relic)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.DATABASE__PINOCCHIO__RELIC_VAR_DATA_FROM_ID,
                    Pinocchio_GetRelicFromInventoryId(Pinocchio_relicSlots[_count + _countRange].Basic_VarInventoryId).Basic_VarDataId);

            Pinocchio_RelicBuff__checkBuffs[(int)data.Basic_VarColor - 1] += 10;
        }

        for(int for0 = 0; for0 < (int)Giggle_Item.Relic_COLOR.TOTAL; for0++)
        {
            Pinocchio_GetRelicFromInventoryId(Pinocchio_relicSlots[_count - _countRange ].Basic_VarInventoryId).Basic_BuffAddValueFromCount(for0, Pinocchio_RelicBuff__checkBuffs[for0]);
            Pinocchio_GetRelicFromInventoryId(Pinocchio_relicSlots[_count               ].Basic_VarInventoryId).Basic_BuffAddValueFromCount(for0, Pinocchio_RelicBuff__checkBuffs[for0]);
            Pinocchio_GetRelicFromInventoryId(Pinocchio_relicSlots[_count + _countRange ].Basic_VarInventoryId).Basic_BuffAddValueFromCount(for0, Pinocchio_RelicBuff__checkBuffs[for0]);
        }
    }

    // Pinocchio_RelicInsert
    void Pinocchio_RelicInsert(int _dataId)
    {
        int inventoryId = -1;

        for(int for0 = 0; for0 < Pinocchio_relics.Count; for0++)
        {
            if(inventoryId < Pinocchio_relics[for0].Basic_VarInventoryId)
            {
                inventoryId = Pinocchio_relics[for0].Basic_VarInventoryId;
            }
        }

        inventoryId += 1;

        //
        Pinocchio_relics.Add(new Pinocchio_Relic(_dataId, inventoryId));
    }

    ////////// Constructor & Destroyer  //////////
    
    void PinocchioRelic_Contructor()
    {
        if(Pinocchio_relics == null)
        {
            Pinocchio_relics = new List<Pinocchio_Relic>();
        }
        //
        if(Pinocchio_relicSlots == null)
        {
            Pinocchio_relicSlots = new List<Pinocchio_RelicSlot>();
        }
        while(Pinocchio_relicSlots.Count < 9)
        {
            Pinocchio_relicSlots.Add(new Pinocchio_RelicSlot());
        }

        //
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__VAR_DATA,                        Pinocchio_VarData                           );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__RELIC_VAR_RELICS,                Pinocchio_RelicVarRelics__Bridge            );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__RELIC_VAR_RELIC_SLOTS,           Pinocchio_RelicVarRelicSlots__Bridge        );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__RELIC_VAR_SLOTS_COLOR_IS_SAME,   Pinocchio_RelicVarSlotsColorIsSame__Bridge  );

        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__RELIC_SLOT_CHANGE,               Pinocchio_RelicSlotChange                   );
    }

        #endregion

        #region PINOCCHIO_JOBS

    ////////// Getter & Setter          //////////
    
    ////////// Method                   //////////
    
    ////////// Constructor & Destroyer  //////////
    
        #endregion

    #endregion

    #region MARIONETTE
    
    [Header("MARIONETTE ==================================================")]
    [SerializeField] List<Giggle_Character.Save> Marionette_list;

    ////////// Getter & Setter          //////////
    object Marionette_GetList(params object[] _args)
    {
        return Marionette_list;
    }

    object Marionette_GetDataFromInventoryId(params object[] _args)
    {
        int id = (int)_args[0];

        //
        Giggle_Character.Save res = null;

        for(int for0 = 0; for0 < Marionette_list.Count; for0++)
        {
            if(Marionette_list[for0].Basic_VarInventoryId.Equals(id))
            {
                res = Marionette_list[for0];
                break;
            }
        }

        if(res == null)
        {
            res = new Giggle_Character.Save(-1);
        }

        //
        return res;
    }

    ////////// Method                   //////////
    
    // Marionette_Add
    object Marionette_Add__Script(params object[] _args)
    {
        int dataId = (int)_args[0];

        //
        Marionette_Add(dataId);

        //
        return true;
    }

    void Marionette_Add(int _dataId)
    {
        //
        int inventoryId = 0;
        for(int for0 = 0; for0 < Marionette_list.Count; for0++)
        {
            int forId = Marionette_list[for0].Basic_VarInventoryId;
            if(inventoryId <= forId)
            {
                inventoryId = forId + 1;
            }
        }
        Giggle_Character.Save element = new Giggle_Character.Save(inventoryId, _dataId);
        Marionette_list.Add(element);
    }
    
    // Marionette_CardEquip
    object Marionette_CardEquip__Script(params object[] _args)
    {
        int marionetteInventoryId = (int)_args[0];
        int cardSlot    = (int)_args[1];
        int cardId      = (int)_args[2];

        //
        Marionette_CardEquip(
            marionetteInventoryId,
            cardSlot, cardId);

        //
        return true;
    }

    void Marionette_CardEquip(
        int _marionetteInventoryId,
        int _cardSlot, int _cardId)
    {
        for(int for0 = 0; for0 < Marionette_list.Count; for0++)
        {
            if(Marionette_list[for0].Basic_VarInventoryId.Equals(_marionetteInventoryId))
            {
                Marionette_list[for0].Basic_SetCard(_cardSlot, _cardId);
                break;
            }
        }
    }

    ////////// Constructor & Destroyer  //////////
    void Marionette_Contructor()
    {
        if(Marionette_list == null)
        {
            Marionette_list = new List<Giggle_Character.Save>();
        }

        Marionette_Add(21001);

        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__MARIONETTE__GET_LIST,                   Marionette_GetList                  );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__MARIONETTE__GET_DATA_FROM_INVENTORYID,  Marionette_GetDataFromInventoryId   );

        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__MARIONETTE__ADD,        Marionette_Add__Script          );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__MARIONETTE__CARD_EQUIP, Marionette_CardEquip__Script    );
    }

    #endregion

    #region FORMATION

    [Serializable]
    public class Formation
    {
        // 인벤토리 id
        [SerializeField] List<int> Basic_formation;

        ////////// Getter & Setter          //////////
        public List<int>    Basic_VarFormation  { get{ return Basic_formation;  }   }

        ////////// Method                   //////////

        ////////// Constructor & Destroyer  //////////
        public Formation()
        {
            if(Basic_formation == null)
            {
                Basic_formation = new List<int>();
            }

            while(Basic_formation.Count < 9)
            {
                Basic_formation.Add(-1);
            }
            Basic_formation[4] = -2;
        }
    }
    
    [Header("FORMATION ==================================================")]
    [SerializeField] int                Formation_select;
    [SerializeField] List<Formation>    Formation_list;

    ////////// Getter & Setter          //////////
    // Formation_select
    object Formation_GetSelect(params object[] _args)   { return Formation_select;  }

    // Formation_list
    object Formation_GetFormationList(params object[] _args)    { return Formation_list;    }

    object Formation_GetSelectFormation(params object[] _args)
    {
        //
        return Formation_list[Formation_select].Basic_VarFormation;
    }

    ////////// Method                   //////////
    
    object Formation_FormationSetting(params object[] _args)
    {
        int id          = (int)_args[0];
        int formation   = (int)_args[1];

        //
        // 배치되어 있다면 교체
        for (int for0 = 0; for0 < Formation_list[Formation_select].Basic_VarFormation.Count; for0++)
        {
            if(Formation_list[Formation_select].Basic_VarFormation[for0].Equals(id))
            {
                Formation_list[Formation_select].Basic_VarFormation[for0] = Formation_list[Formation_select].Basic_VarFormation[formation];
                break;
            }
        }

        // 자리 갱신
        Formation_list[Formation_select].Basic_VarFormation[formation] = id;

        //
        return true;
    }

    ////////// Constructor & Destroyer  //////////
    void Formation_Contructor()
    {
        if(Formation_list == null)
        {
            Formation_list = new List<Formation>();
        }

        while(Formation_list.Count < 3)
        {
            Formation_list.Add(new Formation());
        }

        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__FORMATION__GET_SELECT,              Formation_GetSelect             );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__FORMATION__GET_FORMATION_LIST,      Formation_GetFormationList      );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__FORMATION__GET_SELECT_FORMATION,    Formation_GetSelectFormation    );

        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__FORMATION__FORMATION_SETTING,       Formation_FormationSetting      );
    }

    #endregion

    #region ITEM

    [SerializeField] List<Giggle_Item.Inventory>    Item_list;
    [SerializeField] List<Giggle_Item.Inventory>    Item_cardList;

    ////////// Getter & Setter          //////////

    // Item_list
    object Item_GetItemList(params object[] _args)
    {
        List<Giggle_Item.Inventory> res = new List<Giggle_Item.Inventory>();

        for(int for0 = 0; for0 < Item_list.Count; for0++)
        {
            res.Add(Item_list[for0]);
        }

        return res;
    }

    object Item_GetItemFromInventoryId(params object[] _args)
    {
        int id = (int)_args[0];

        //
        Giggle_Item.Inventory res = null;

        for(int for0 = 0; for0 < Item_list.Count; for0++)
        {
            if(Item_list[for0].Basic_VarInventoryId.Equals(id))
            {
                res = Item_list[for0];
                break;
            }
        }

        return res;
    }

    // Item_cards
    object Item_GetCardList(params object[] _args)
    {
        List<Giggle_Item.Inventory> res = new List<Giggle_Item.Inventory>();

        for(int for0 = 0; for0 < Item_cardList.Count; for0++)
        {
            res.Add(Item_cardList[for0]);
        }

        return res;
    }

    object Item_GetCardDataFromDataId(params object[] _args)
    {
        int id = (int)_args[0];

        //
        Giggle_Item.Inventory res = null;

        for(int for0 = 0; for0 < Item_cardList.Count; for0++)
        {
            if(Item_cardList[for0].Basic_VarDataId.Equals(id))
            {
                res = Item_cardList[for0];
                break;
            }
        }

        //
        return res;
    }

    ////////// Method                   //////////
    
    ////////// Constructor & Destroyer  //////////
    void Item_Contructor()
    {
        if(Item_list == null)
        {
            Item_list = new List<Giggle_Item.Inventory>();
        }
        if(Item_cardList == null)
        {
            Item_cardList = new List<Giggle_Item.Inventory>();
        }

        //
        Item_list.Add(new Giggle_Item.Inventory(0, 701101001));
        Item_list.Add(new Giggle_Item.Inventory(1, 72101001 ));
        Item_list.Add(new Giggle_Item.Inventory(2, 74101001 ));
        Item_list.Add(new Giggle_Item.Inventory(3, 76101001));
        Item_list.Add(new Giggle_Item.Inventory(4, 78101001));
        Item_list.Add(new Giggle_Item.Inventory(5, 78101001));

        Item_cardList.Add(new Giggle_Item.Inventory(0, 251001));

        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__ITEM__GET_ITEM_LIST,                Item_GetItemList            );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__ITEM__GET_ITEM_FROM_INVENTORY_ID,   Item_GetItemFromInventoryId );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__ITEM__GET_CARD_LIST,                Item_GetCardList            );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__CARD__GET_DATA_FROM_DATA_ID,        Item_GetCardDataFromDataId  );
    }

    #endregion

    #region POWER

    [Header("POWER ==================================================")]
    [SerializeField] List<int> Power_values;

    ////////// Getter & Setter          //////////

    ////////// Method                   //////////

    public void Power_Add(int _pow)
    {
        Power_values[0] += _pow;

        for(int for0 = 0; for0 < Power_values.Count - 1; for0++)
        {
            if (Power_values[for0] > 1000)
            {
                Power_values[for0 + 1] += Power_values[for0] / 1000;
                Power_values[for0] %= 1000;
            }
            else
            {
                break;
            }
        }
    }

    ////////// Constructor & Destroyer  //////////
    void Power_Contructor()
    {
        if(Power_values == null)
        {
            Power_values = new List<int>();
        }

        while(Power_values.Count < 5)
        {
            Power_values.Add(0);
        }
    }

    #endregion
}
