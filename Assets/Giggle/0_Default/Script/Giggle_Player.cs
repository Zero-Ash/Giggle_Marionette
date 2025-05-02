using UnityEngine;

//
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.TextCore.Text;
using Unity.VisualScripting;
using BackEnd;

[Serializable]
public class Giggle_Player : IDisposable
{
    #region BASIC
    public enum Basic__DATA_COROUTINE_PHASE
    {
        LOAD        = 0,
        RESOURCE    = 10,
        STAGE       = 20,
        PINOCCHIO   = 30,
        MARIONETTE  = 40,
        FORMATION   = 50,
        ITEM        = 60,
        DUNGEON     = 70,

        END         = 10000
    }

    [SerializeField]    IEnumerator                 Basic_dataCoroutine;
    [SerializeField]    Basic__DATA_COROUTINE_PHASE Basic_dataCoroutinePhase;
                        LitJson.JsonData            Basic_dataCoroutineDatas;

    ////////// Getter & Setter          //////////
    object  Basic_IsNetworkDataLoadDoing__Bridge(params object[] _args) { return Basic_dataCoroutine != null;   }

    ////////// Method                   //////////
    object  Basic_NetworkDataLoad__Bridge(params object[] _args)
    {
        Basic_dataCoroutine = Basic_NetworkDataLoad__Coroutine();
        Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
            Giggle_ScriptBridge.EVENT.MASTER__BASIC__START_COROUTINE,
            //
            Basic_dataCoroutine);

        //
        return true;
    }

    IEnumerator Basic_NetworkDataLoad__Coroutine()
    {
        Basic_dataCoroutinePhase = Basic__DATA_COROUTINE_PHASE.LOAD;

        while(Basic_dataCoroutinePhase != Basic__DATA_COROUTINE_PHASE.END)
        {
            switch(Basic_dataCoroutinePhase)
            {
                case Basic__DATA_COROUTINE_PHASE.LOAD:          { Basic_NetworkDataLoad();      }   break;
                case Basic__DATA_COROUTINE_PHASE.RESOURCE:      { Resource_NetworkDataLoad();   }   break;
                case Basic__DATA_COROUTINE_PHASE.STAGE:         { Stage_NetworkDataLoad();      }   break;
                case Basic__DATA_COROUTINE_PHASE.PINOCCHIO:     { Pinocchio_NetworkDataLoad();  }   break;
                case Basic__DATA_COROUTINE_PHASE.MARIONETTE:    { Marionette_NetworkDataLoad(); }   break;
                case Basic__DATA_COROUTINE_PHASE.FORMATION:     { Basic_dataCoroutinePhase = Basic__DATA_COROUTINE_PHASE.END;   }   break;
                case Basic__DATA_COROUTINE_PHASE.ITEM:          {                                   }   break;
                case Basic__DATA_COROUTINE_PHASE.DUNGEON:       {                                   }   break;
            }

            //
            yield return null;
        }

        //
        Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
            Giggle_ScriptBridge.EVENT.MASTER__BASIC__STOP_COROUTINE,
            //
            Basic_dataCoroutine);
        Basic_dataCoroutine = null;
    }
    
    //
    void Basic_NetworkDataLoad()
    {
        Basic_dataCoroutinePhase = Basic__DATA_COROUTINE_PHASE.LOAD + 1;

        //
        Backend.GameData.GetMyData(
            "PLAYER", new Where(),
            callback =>
            {
                if(!callback.IsSuccess())
                {
                    return;
                }

                //
                Basic_dataCoroutineDatas = callback.FlattenRows();

                Basic_dataCoroutinePhase = Basic__DATA_COROUTINE_PHASE.RESOURCE;
            });
    }

    //
    public void Basic_Init()
    {
        Stage_Contructor();
        Pinocchio_Contructor();
        //Marionette_Contructor();
        Formation_Contructor();
        Item_Contructor();
        Power_Contructor();
        Dungeon_Contructor();
    }

    ////////// Constructor & Destroyer  //////////
    public Giggle_Player()
    {
        Basic_dataCoroutine = null;

        //Stage_Contructor();
        //Pinocchio_Contructor();
        Marionette_Contructor();
        //Formation_Contructor();
        //Item_Contructor();
        //Power_Contructor();
        //Dungeon_Contructor();

        //
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(  Giggle_ScriptBridge.EVENT.PLAYER__BASIC__IS_NETWORK_DATA_LOAD_DOING,    Basic_IsNetworkDataLoadDoing__Bridge    );

        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(  Giggle_ScriptBridge.EVENT.PLAYER__BASIC__NETWORK_DATA_LOAD,             Basic_NetworkDataLoad__Bridge           );
    }

    public void Dispose()
    {

    }

    #endregion

    #region RESOURCE

    [Header("RESOURCE ==================================================")]
    [SerializeField] int    Resource_gold;
    [SerializeField] int    Resource_gacha;

    ////////// Getter & Setter          //////////
    

    ////////// Method                   //////////
    
    //
    void Resource_NetworkDataLoad()
    {
        //
        Resource_gold   = int.Parse(Basic_dataCoroutineDatas[0]["RESOURCE_GOLD" ].ToString());
        Resource_gacha  = int.Parse(Basic_dataCoroutineDatas[0]["RESOURCE_GACHA"].ToString());

        //
        Basic_dataCoroutinePhase = Basic__DATA_COROUTINE_PHASE.STAGE;
    }

    
    ////////// Constructor & Destroyer  //////////

    
    #endregion

    #region STAGE

    [Header("STAGE ==================================================")]
    [SerializeField] int    Stage_max;
    [SerializeField] int    Stage_select;

    [SerializeField] bool   Stage_isNext;

    [SerializeField] float  Stage_speedTimer;
    [SerializeField] float  Stage_speed;

    IEnumerator Stage_coroutine;

    ////////// Getter & Setter          //////////
    // Stage_select
    object Stage_VarSelect(params object[] _args)
    {
        return Stage_select;
    }

    // Stage_isNext
    object Stage_VarIsNext(params object[] _args)
    {
        return Stage_isNext;
    }

    // Stage_isNext
    object Stage_VarSpeedTimer(params object[] _args)
    {
        return Stage_speedTimer;
    }

    // Stage_isNext
    object Stage_VarSpeed(params object[] _args)
    {
        return Stage_speed;
    }

    ////////// Method                   //////////
    // Stage_select
    object Stage_Next(params object[] _args)
    {
        int stage0 = Stage_select / 100;
        int stage1 = Stage_select % 100;

        // 보스 스테이지 여부에 따라 다음 스테이지 id 갱신
        Giggle_Stage.Stage stage = (Giggle_Stage.Stage)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
            Giggle_ScriptBridge.EVENT.DATABASE__STAGE__GET_STAGE_FROM_ID,
            Stage_select);

        if(stage.Basic_VarIsBoss)
        {
            stage0 += 1;
            stage1 = 1;
        }
        else
        {
            stage1 += 1;
        }

        // 스테이지id 업데이트
        Stage_select = (stage0 * 100) + stage1;

        if(Stage_select > Stage_max)
        {
            Stage_max = Stage_select;
        }

        return true;
    }

    //
    object Stage_Accel(params object[] _args)
    {
        Stage_speedTimer = 60.0f * 30.0f;

        Stage_speed = 2.0f;

        //
        Stage_coroutine = Stage_Coroutine();
        Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MASTER__BASIC__START_COROUTINE, Stage_coroutine);

        return true;
    }
    
    //
    void Stage_NetworkDataLoad()
    {
        //
        Stage_max       = int.Parse(Basic_dataCoroutineDatas[0]["STAGE_MAX"     ].ToString());
        Stage_select    = int.Parse(Basic_dataCoroutineDatas[0]["STAGE_SELECT"  ].ToString());

        //
        Basic_dataCoroutinePhase = Basic__DATA_COROUTINE_PHASE.PINOCCHIO;
    }
    
    ////////// Constructor & Destroyer  //////////
    void Stage_Contructor()
    {
        Stage_max = Stage_select = 610101;

        Stage_isNext = true;

        Stage_speedTimer    = 0.0f;
        Stage_speed         = 1.0f;

        //
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__STAGE__VAR_SELECT,      Stage_VarSelect     );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__STAGE__VAR_IS_NEXT,     Stage_VarIsNext     );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__STAGE__VAR_SPEED_TIMER, Stage_VarSpeedTimer );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__STAGE__VAR_SPEED,       Stage_VarSpeed      );

        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__STAGE__NEXT,    Stage_Next  );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__STAGE__ACCEL,   Stage_Accel );
    }

    IEnumerator Stage_Coroutine()
    {
        float time = Time.time;
        float lastTime = Time.time;

        while(Stage_speedTimer >= 0.0f)
        {
            time = Time.time;

            //
            Stage_speedTimer -= time - lastTime;

            if(Stage_speedTimer <= 0.0f)
            {
                Stage_speedTimer = 0.0f;
                Stage_speed = 1.0f;

                Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MASTER__BASIC__STOP_COROUTINE, Stage_coroutine);
            }

            lastTime = time;

            yield return null;
        }
    }
    
    #endregion

    #region PINOCCHIO

    public enum Pinocchio_GENDER
    {
        MALE = 1,
        FEMALE
    }

    [Header("PINOCCHIO ==================================================")]
    [SerializeField] Giggle_Character.Save  Pinocchio_data;

    [SerializeField] Pinocchio_GENDER   Pinocchio_gender;

    ////////// Getter & Setter          //////////
    // Pinocchio_data
    object Pinocchio_VarData__Script(params object[] _args)
    {
        return Pinocchio_VarData;
    }

    public Giggle_Character.Save    Pinocchio_VarData   { get { return Pinocchio_data;  }   }

    ////////// Method                   //////////
    
    //
    void Pinocchio_NetworkDataLoad()
    {
        Basic_dataCoroutinePhase = Basic__DATA_COROUTINE_PHASE.PINOCCHIO + 1;

        //
        Marionette_list.Clear();

        Backend.GameData.GetMyData(
            "PINOCCHIO", new Where(),
            callback =>
            {
                if(!callback.IsSuccess())
                {
                    return;
                }

                //
                LitJson.JsonData datas = callback.FlattenRows();
                if(datas.Count > 0)
                {
                    PinocchioEquips_NetworkDataLoad(datas);
                    PinocchioSkills_NetworkDataLoad(datas);
                    PinocchioAttribute_NetworkDataLoad(datas);
                    PinocchioAbility_NetworkDataLoad(datas);
                    PinocchioRelic_NetworkDataLoad(datas);
                }

                Basic_dataCoroutinePhase = Basic__DATA_COROUTINE_PHASE.MARIONETTE;
            });
    }

    ////////// Constructor & Destroyer  //////////
    void Pinocchio_Contructor()
    {
        Pinocchio_data = new Giggle_Character.Save(1121);

        //
        Pinocchio_gender = Pinocchio_GENDER.FEMALE;

        PinocchioJobs_Contructor();
        PinocchioEquips_Contructor();
        PinocchioSkills_Contructor();
        PinocchioAttributes_Contructor();
        PinocchioAbility_Contructor();
        PinocchioRelic_Contructor();

        Pinocchio_RelicInsert(1611001);
        Pinocchio_RelicInsert(1611001);
        Pinocchio_RelicInsert(1611001);
        Pinocchio_RelicInsert(1612001);
        Pinocchio_RelicInsert(1612001);
        Pinocchio_RelicInsert(1612001);

        //
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__VAR_DATA,    Pinocchio_VarData__Script   );
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

        // TODO:테스트용 임시데이터
        Pinocchio_jobs.Add(1121);
        Pinocchio_jobs.Add(1112);
        Pinocchio_jobs.Add(1113);

        //
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__VAR_JOBS,    Pinocchio_VarJobs   );
    }
    
        #endregion

        #region PINOCCHIO_EQUIPS

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
            if(socketNames.Length > 2)
            {
                count += int.Parse(socketNames[1]);
            }
            else
            {
                // TODO:장신구 자동장착에 대해 추가 필요
                //Giggle_Item.Inventory data = Item_GetItemFromInventoryId(inventoryId);
                //data.
            }
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

    void PinocchioEquips_NetworkDataLoad(LitJson.JsonData _datas)
    {
        // Pinocchio_equips
        int whileCount = 0;
        while(true)
        {
            if(!_datas[0].ContainsKey("EQUIPS_" + whileCount))
            {
                break;
            }

            //
            string[] equips0 = _datas[0]["EQUIPS_" + whileCount].ToString().Split('/');

            while(Pinocchio_equips[whileCount].Basic_VarEquipsCount <= equips0.Length)
            {
                Pinocchio_equips[whileCount].Basic_EquipmentAdd();
            }

            for(int for0 = 0; for0 < equips0.Length; for0++)
            {
                Pinocchio_equips[whileCount].Basic_SetEquipmentId(for0, int.Parse(equips0[for0]));
            }

            //
            whileCount++;
        }
        
        // Pinocchio_equipSelect
        Pinocchio_equipSelect   = int.Parse(_datas[0]["EQUIP_SELECT"].ToString());
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

        public Pinocchio_Skill(int _id, int _lv)
        {
            Basic_id = _id;
            Basic_lv = _lv;
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
        public void Basic_DataLoad(string _data)
        {
            string[] datas = _data.Split('/');
            for(int for0 = 0; for0 < datas.Length; for0++)
            {
                Basic_list[for0] = int.Parse(datas[for0]);
            }
        }

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

    // Pinocchio_selectSkillSlot
    object Pinocchio_VarSelectSkillSlot(params object[] _args)
    {
        if(_args.Length > 0)
        {
            int select = (int)_args[0];

            //
            Pinocchio_selectSkillSlot = select;
        }

        //
        return Pinocchio_selectSkillSlot;
    }
    
    ////////// Method                   //////////

    void PinocchioSkills_NetworkDataLoad(LitJson.JsonData _datas)
    {
        //
        List<Giggle_Character.Skill> skillDatas
            = (List<Giggle_Character.Skill>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.DATABASE__PINOCCHIO__GET_SKILLS);
        
        for(int for0 = 0; for0 < skillDatas.Count; for0++)
        {
            if(_datas[0].ContainsKey("SKILL__" + skillDatas[for0].Basic_VarId))
            {
                Pinocchio_skills.Add(new Pinocchio_Skill(skillDatas[for0].Basic_VarId, int.Parse(_datas[0]["SKILL_" + skillDatas[for0].Basic_VarId].ToString())));
            }
        }

        //
        for(int for0 = 0; for0 < Pinocchio_skillSlots.Count; for0++)
        {
            Pinocchio_skillSlots[for0].Basic_DataLoad(_datas[0]["SKILL__SLOT" + for0].ToString());
        }

        //
        Pinocchio_selectSkillSlot = int.Parse(_datas[0]["SKILL__SLOT_SELECT"].ToString());
    }
    
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
        Pinocchio_selectSkillSlot = 0;

        //
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__VAR_SKILLS,                      Pinocchio_VarSkills             );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__VAR_SKILL_FROM_ID,               Pinocchio_VarSkillFromId        );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__SKILL_VAR_SKILL_SLOTS,           Pinocchio_VarSkillSlots         );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__SKILL_VAR_SKILL_SLOT_FROM_COUNT, Pinocchio_VarSkillSlotFromCount );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__SKILL_VAR_SELECT_SKILL_SLOT,     Pinocchio_VarSelectSkillSlot    );
    }
    
        #endregion

        #region PINOCCHIO_ATTRIBUTES

    public enum ATTRIBUTE_TYPE
    {
        ATTACK = 0,
        DEFENCE,
        SUPPORT,

        TOTAL
    }

    [Serializable]
    public class Attribute_Data : IDisposable
    {
        [SerializeField] List<Pinocchio_Skill>  Basic_list;

        ////////// Getter & Setter          //////////
        
        // Basic_list
        public int              Basic_VarListCount                  { get { return Basic_list.Count;    }   }

        public Pinocchio_Skill  Basic_GetListFromCount(int _count)  { return Basic_list[_count];            }

        ////////// Method                   //////////

        //
        public Pinocchio_Skill Basic_Add(int _id)
        {
            Pinocchio_Skill res = new Pinocchio_Skill(_id);
            res.Basic_VarLv = 0;
            Basic_list.Add(res);

            return res;
        }

        //
        public void Basic_Load(int _id, int _lv)
        {
            Pinocchio_Skill res = new Pinocchio_Skill(_id);
            res.Basic_VarLv = _lv;
            Basic_list.Add(res);
        }

        ////////// Constructor & Destroyer  //////////

        public Attribute_Data()
        {
            Basic_list = new List<Pinocchio_Skill>();
        }

        //
        public void Dispose()
        {

        }
    }

    [Header("PINOCCHIO_ATTRIBUTE ==========")]
    [SerializeField] List<Attribute_Data>   Pinocchio_attributes;

    ////////// Getter & Setter          //////////

    // Pinocchio_attributes
    object Pinocchio_VarAttributeFromType(params object[] _args)
    {
        ATTRIBUTE_TYPE type0 = (ATTRIBUTE_TYPE)_args[0];

        //
        return Pinocchio_attributes[(int)type0];
    }

    object Pinocchio_VarAttributeFromId(params object[] _args)
    {
        ATTRIBUTE_TYPE type0 = (ATTRIBUTE_TYPE)_args[0];
        int id = (int)_args[1];

        //
        Pinocchio_Skill res = null;

        if(id != -1)
        {
            for(int for0 = 0; for0 < Pinocchio_attributes[(int)type0].Basic_VarListCount; for0++)
            {
                if(Pinocchio_attributes[(int)type0].Basic_GetListFromCount(for0).Basic_VarId.Equals(id))
                {
                    res = Pinocchio_attributes[(int)type0].Basic_GetListFromCount(for0);
                    break;
                }
            }

            if(res == null)
            {
                res = Pinocchio_attributes[(int)type0].Basic_Add(id);
            }
        }
        else
        {
            res = new Pinocchio_Skill(-1);
        }

        //
        return res;
    }
    
    ////////// Method                   //////////
    
    // Pinocchio_attributes
    object Pinocchio_AttributeLevelUp(params object[] _args)
    {
        ATTRIBUTE_TYPE type0 = (ATTRIBUTE_TYPE)_args[0];
        int id = (int)_args[1];

        //
        Pinocchio_Skill element = null;

        for(int for0 = 0; for0 < Pinocchio_attributes[(int)type0].Basic_VarListCount; for0++)
        {
            if(Pinocchio_attributes[(int)type0].Basic_GetListFromCount(for0).Basic_VarId.Equals(id))
            {
                element = Pinocchio_attributes[(int)type0].Basic_GetListFromCount(for0);
                break;
            }
        }

        element.Basic_VarLv += 1;

        //
        return true;
    }

    // PinocchioAttribute_NetworkDataLoad
    void PinocchioAttribute_NetworkDataLoad(LitJson.JsonData _datas)
    {
        PinocchioAttribute_NetworkDataLoad__0(_datas, Giggle_Player.ATTRIBUTE_TYPE.ATTACK);
        PinocchioAttribute_NetworkDataLoad__0(_datas, Giggle_Player.ATTRIBUTE_TYPE.DEFENCE);
        PinocchioAttribute_NetworkDataLoad__0(_datas, Giggle_Player.ATTRIBUTE_TYPE.SUPPORT);
    }

    void PinocchioAttribute_NetworkDataLoad__0(LitJson.JsonData _datas, Giggle_Player.ATTRIBUTE_TYPE _type)
    {
        List<Giggle_Character.Attribute> list
            = (List<Giggle_Character.Attribute>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.DATABASE__PINOCCHIO__GET_ATTRIBUTE,
                _type);
        for(int for0 = 0; for0 < list.Count; for0++)
        {
            string dataKey = "ATTRIBUTE__" + _type.ToString() + "_" + list[for0].Basic_VarId;
            if(!_datas[0].ContainsKey(dataKey))
            {
                continue;
            }

            Pinocchio_attributes[(int)_type].Basic_Load(list[for0].Basic_VarId, int.Parse(_datas[0]["ATTRIBUTE__" + _type.ToString() + "_" + list[for0].Basic_VarId].ToString()));
        }
    }
    
    ////////// Constructor & Destroyer  //////////

    void PinocchioAttributes_Contructor()
    {
        //
        if(Pinocchio_attributes == null)    { Pinocchio_attributes = new List<Attribute_Data>();    }
        for(int for0 = 0; for0 < (int)ATTRIBUTE_TYPE.TOTAL; for0++)
        {
            Pinocchio_attributes.Add(new Attribute_Data());
        }

        //
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__ATTRIBUTE_VAR_FROM_TYPE,         Pinocchio_VarAttributeFromType  );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__ATTRIBUTE_VAR_FROM_TYPE_AND_ID,  Pinocchio_VarAttributeFromId    );

        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__ATTRIBUTE_LEVEL_UP,      Pinocchio_AttributeLevelUp      );
    }
    
        #endregion

        #region PINOCCHIO_ABILITY

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

        public bool Basic_VarIsLock { get { return Basic_isLock;    }   }

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

        //
        public void Basic_Lock()
        {
            Basic_isLock = !Basic_isLock;
        }

        public void Basic_DataLoad(string _data)
        {
            string[] datas = _data.Split('/');

            Basic_id        = int.Parse(datas[0]);
            Basic_grade     = int.Parse(datas[1]);
            Basic_value     = float.Parse(datas[2]);
            Basic_isLock    = bool.Parse(datas[3]);
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

    [Header("PINOCCHIO_ABILITY ==========")]
    [SerializeField] List<Pinocchio_Ability>    Pinocchio_abilitys;
    [SerializeField] int                        Pinocchio_abilityLevel;
    [SerializeField] int                        Pinocchio_abilityExp;
    [SerializeField] int                        Pinocchio_abilityPoint; // 목공소 재화

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

    // Pinocchio_abilityLevel
    object Pinocchio_AbilityGetLevel(params object[] _args)
    {
        return Pinocchio_abilityLevel;
    }

    // Pinocchio_abilityExp

    // Pinocchio_abilityPoint
    
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
    
    //
    object Pinocchio_AbilityLock(params object[] _args)
    {
        int count = (int)_args[0];

        //
        Pinocchio_abilitys[count].Basic_Lock();

        //
        return true;
    }

    //
    public void  Pinocchio_AbilityAddPoint(int _point)
    {
        Pinocchio_abilityPoint += _point;
    }

    void PinocchioAbility_NetworkDataLoad(LitJson.JsonData _datas)
    {
        string[] strs = _datas[0]["ABILITYS"].ToString().Split('|');
        for(int for0 = 0; for0 < strs.Length; for0++)
        {
            Pinocchio_abilitys[for0].Basic_DataLoad(strs[for0]);
        }

        Pinocchio_abilityLevel  = int.Parse(_datas[0]["ABILITY_LEVEL"].ToString()   );

        Pinocchio_abilityExp    = int.Parse(_datas[0]["ABILITY_EXP"].ToString()     );

        Pinocchio_abilityPoint  = int.Parse(_datas[0]["ABILITY_POINT"].ToString()   );
    }

    // PopUp ==========
    // Wood
    
    ////////// Constructor & Destroyer  //////////

    void PinocchioAbility_Contructor()
    {
        PinocchioAbility_Contructor__Wood();

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
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__ABILITY_LOCK,                    Pinocchio_AbilityLock                   );
    }

            #region PINOCCHIO_ABILITY__WOOD

    [Serializable]
    public class Pinocchio_AbilityWood : IDisposable
    {
        [SerializeField] Giggle_Player  Basic_Player;

        [SerializeField] DateTime   Basic_endTime;
        [SerializeField] int        Basic_marionette;   // 투입된 마리오네트의 inventoryId
        [SerializeField] int        Basic_select;        // 획득하게 될 재화(0이면 작업 대기상태)

        ////////// Getter & Setter          //////////

        public DateTime Basic_VarEndTime    { get { return Basic_endTime;  }   }

        public int Basic_VarMarionette  { get { return Basic_marionette;    }   }

        public int Basic_VarSelect  { get { return Basic_select;    }   }

        ////////// Method                   //////////

        public void Basic_Work(int _select)
        {
            Basic_select = _select;
            
            switch(_select)
            {
                case 0: { Basic_endTime = DateTime.Now.AddHours(2);     }   break;
                case 1: { Basic_endTime = DateTime.Now.AddHours(6);     }   break;
                case 2: { Basic_endTime = DateTime.Now.AddHours(12);    }   break;
            }
        }

        public void Basic_MarionetteJoin(int _id)
        {
            Basic_marionette = _id;

            // 시간 재설정
            DateTime nowTime = DateTime.Now;
            TimeSpan ts = Basic_endTime - nowTime;
            ts = ts.Multiply(0.9);
            Debug.Log(ts.Hours + " " + ts.Minutes + " " + ts.Seconds);
            Basic_endTime = nowTime.Add(ts);
        }

        public void Basic_WorkEnd()
        {
            // 재화 지급
            int value = 0;

            switch(Basic_select)
            {
                case 0: { value = 100;  }   break;
                case 1: { value = 500;  }   break;
                case 2: { value = 1500; }   break;
            }

            Basic_Player.Pinocchio_AbilityAddPoint(value);

            // 초기화
            Basic_select = -1;
        }

        ////////// Constructor & Destroyer  //////////

        public Pinocchio_AbilityWood(Giggle_Player _Player)
        {
            Basic_Player = _Player;

            Basic_marionette = -1;
            Basic_select = -1;
        }

        //
        public void Dispose()
        {

        }
    }
    
    [SerializeField] List<Pinocchio_AbilityWood>    Pinocchio_abilityWoodDatas;

    ////////// Getter & Setter          //////////
    
    //
    object Pinocchio_AbilityWoodVarDataFromCount(params object[] _args)
    {
        int count = (int)_args[0];

        //
        return Pinocchio_abilityWoodDatas[count];
    }
    
    ////////// Method                   //////////
    
    //
    object Pinocchio_AbilityWoodWork(params object[] _args)
    {
        int count   = (int)_args[0];
        int select  = (int)_args[1];

        //
        Pinocchio_abilityWoodDatas[count].Basic_Work(select);

        //
        return true;
    }
    
    //
    object Pinocchio_AbilityWoodWorker(params object[] _args)
    {
        int count   = (int)_args[0];
        int id      = (int)_args[1];

        //
        Pinocchio_abilityWoodDatas[count].Basic_MarionetteJoin(id);

        //
        return true;
    }
    
    //
    object Pinocchio_AbilityWoodWorkEnd(params object[] _args)
    {
        int count   = (int)_args[0];

        //
        Pinocchio_abilityWoodDatas[count].Basic_WorkEnd();

        //
        return true;
    }
    
    ////////// Constructor & Destroyer  //////////

    void PinocchioAbility_Contructor__Wood()
    {
        if(Pinocchio_abilityWoodDatas == null)
        {
            Pinocchio_abilityWoodDatas = new List<Pinocchio_AbilityWood>();
        }
        while(Pinocchio_abilityWoodDatas.Count < 6)
        {
            Pinocchio_abilityWoodDatas.Add(new Pinocchio_AbilityWood(this));
        }

        //
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__ABILITY_WOOD_VAR_DATA_FROM_COUNT,    Pinocchio_AbilityWoodVarDataFromCount   );

        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__ABILITY_WOOD_WORK,                   Pinocchio_AbilityWoodWork               );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__ABILITY_WOOD_WORKER,                 Pinocchio_AbilityWoodWorker             );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__ABILITY_WOOD_WORKER_END,             Pinocchio_AbilityWoodWorkEnd            );
    }
            #endregion
    
        #endregion

        #region PINOCCHIO_RELIC

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

        public void Basic_DataLoad(string _data)
        {
            string[] datas = _data.Split('/');

            //
            Basic_dataId        = int.Parse(datas[0]);
            Basic_inventoryId   = int.Parse(datas[1]);

            Basic_level         = int.Parse(datas[2]);
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

    [Header("RELIC ==========")]
    [SerializeField] List<Pinocchio_Relic>      Pinocchio_relics;
    [SerializeField] List<Pinocchio_RelicSlot>  Pinocchio_relicSlots;
    [SerializeField] List<bool>                 Pinocchio_relicSlotBuffs;

    ////////// Getter & Setter          //////////

    // Pinocchio_relics
    object Pinocchio_RelicVarRelics__Bridge(params object[] _args)
    {
        return Pinocchio_relics;        
    }

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

    object Pinocchio_GetRelicFromInventoryId__Bridge(params object[] _args)
    {
        //
        int inventoryId = (int)_args[0];

        //
        return Pinocchio_GetRelicFromInventoryId(inventoryId);        
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

    //
    object Pinocchio_RelicVarRelicSlotBuffFromCount__Bridge(params object[] _args)
    {
        int count = (int)_args[0];

        //
        return Pinocchio_relicSlotBuffs[count];
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

        Pinocchio_RelicBuff();

        //
        return true;
    }

    // Pinocchio_RelicBuff
    static List<int> Pinocchio_RelicBuff__checkBuffs;

    void Pinocchio_RelicBuff()
    {
        for(int for0 = 0; for0 < Pinocchio_relicSlotBuffs.Count; for0++)
        {
            Pinocchio_relicSlotBuffs[for0] = false;
        }

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
        if( (Pinocchio_GetRelicFromInventoryId(Pinocchio_relicSlots[_count - _countRange].Basic_VarInventoryId) != null) &&
            (Pinocchio_GetRelicFromInventoryId(Pinocchio_relicSlots[_count              ].Basic_VarInventoryId) != null) &&
            (Pinocchio_GetRelicFromInventoryId(Pinocchio_relicSlots[_count + _countRange].Basic_VarInventoryId) != null))
        {
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

                //
                int buffCount = _count;
                switch(_countRange)
                {
                    case 1: { buffCount = (_count - 1) / 3; }   break;
                }
                Pinocchio_relicSlotBuffs[buffCount] = true;
            }

            //
            for(int for0 = 0; for0 < (int)Giggle_Item.Relic_COLOR.TOTAL; for0++)
            {
                Pinocchio_Relic data = Pinocchio_GetRelicFromInventoryId(Pinocchio_relicSlots[_count - _countRange].Basic_VarInventoryId);
                if(data != null) { data.Basic_BuffAddValueFromCount(for0, Pinocchio_RelicBuff__checkBuffs[for0]);   }

                data = Pinocchio_GetRelicFromInventoryId(Pinocchio_relicSlots[_count].Basic_VarInventoryId);
                if(data != null) { data.Basic_BuffAddValueFromCount(for0, Pinocchio_RelicBuff__checkBuffs[for0]);   }

                data = Pinocchio_GetRelicFromInventoryId(Pinocchio_relicSlots[_count + _countRange].Basic_VarInventoryId);
                if(data != null) { data.Basic_BuffAddValueFromCount(for0, Pinocchio_RelicBuff__checkBuffs[for0]);   }
            }
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

    void PinocchioRelic_NetworkDataLoad(LitJson.JsonData _datas)
    {
        string[] strs = _datas[0]["RELIC"].ToString().Split('|');
        while(Pinocchio_relics.Count < strs.Length)
        {
            Pinocchio_relics.Add(new Pinocchio_Relic(-1,-1));
        }

        for(int for0 = 0; for0 < strs.Length; for0++)
        {
            Pinocchio_relics[for0].Basic_DataLoad(strs[for0]);
        }

        //
        
        strs = _datas[0]["RELIC_SLOT"].ToString().Split('/');
        for(int for0 = 0; for0 < strs.Length; for0++)
        {
            Pinocchio_relicSlots[for0].Basic_VarInventoryId = int.Parse(strs[0]);
        }
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
        if(Pinocchio_relicSlotBuffs == null)
        {
            Pinocchio_relicSlotBuffs = new List<bool>();
        }
        while(Pinocchio_relicSlotBuffs.Count < 6)
        {
            Pinocchio_relicSlotBuffs.Add(false);
        }

        //
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__RELIC_VAR_RELICS,                        Pinocchio_RelicVarRelics__Bridge                    );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__RELIC_GET_RELIC_FROM_INVENTORY_ID,       Pinocchio_GetRelicFromInventoryId__Bridge           );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__RELIC_VAR_RELIC_SLOTS,                   Pinocchio_RelicVarRelicSlots__Bridge                );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__RELIC_VAR_SLOTS_COLOR_IS_SAME,           Pinocchio_RelicVarSlotsColorIsSame__Bridge          );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__RELIC_VAR_RELIC_SLOT_BUFF_FROM_COUNT,    Pinocchio_RelicVarRelicSlotBuffFromCount__Bridge    );

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
    [SerializeField] List<Giggle_Character.Save>    Marionette_list;
    [SerializeField] List<int>                      Marionette_document;    // 획득했던 마리오네트 데이터 아이디

    ////////// Getter & Setter          //////////
    // Marionette_list
    public List<Giggle_Character.Save>  Marionette_VarList      { get { return Marionette_list; }   }

    object Marionette_GetList__Bridge(params object[] _args)    { return Marionette_VarList;        }

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

    // Marionette_document
    object Marionette_GetDocument(params object[] _args)    { return Marionette_document;   }

    ////////// Method                   //////////
    
    void Marionette_NetworkDataLoad()
    {
        Basic_dataCoroutinePhase = Basic__DATA_COROUTINE_PHASE.MARIONETTE + 1;

        //
        Marionette_list.Clear();

        Backend.GameData.GetMyData(
            "MARIONETTE", new Where(),
            callback =>
            {
                if(!callback.IsSuccess())
                {
                    return;
                }

                //
                LitJson.JsonData datas = callback.FlattenRows();
                for(int for0 = 0; for0 < datas.Count; for0++)
                {
                    Giggle_Character.Save element = new Giggle_Character.Save(datas[for0]);
                    Marionette_list.Add(element);
                }

                Basic_dataCoroutinePhase = Basic__DATA_COROUTINE_PHASE.FORMATION;
            });
    }

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

        // 도감에 갱신
        bool isNew = true;
        for(int for0 = 0; for0 < Marionette_document.Count; for0++)
        {
            if(Marionette_document[for0].Equals(_dataId))
            {
                isNew = false;
                break;
            }
        }

        if(isNew)
        {
            int whileNum = 0;
            while(whileNum < Marionette_document.Count)
            {
                if(_dataId < Marionette_document[whileNum])
                {
                    break;
                }
                whileNum++;
            }

            Marionette_document.Insert(whileNum, _dataId);
        }
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
        if(Marionette_document == null)
        {
            Marionette_document = new List<int>();
        }

        //
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__MARIONETTE__GET_LIST,                   Marionette_GetList__Bridge          );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__MARIONETTE__GET_DATA_FROM_INVENTORYID,  Marionette_GetDataFromInventoryId   );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__MARIONETTE__GET_DOCUMENT,               Marionette_GetDocument              );

        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__MARIONETTE__ADD,        Marionette_Add__Script          );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__MARIONETTE__CARD_EQUIP, Marionette_CardEquip__Script    );
    }

    #endregion

    #region FORMATION

    [Serializable]
    public class Formation : IDisposable
    {
        // 인벤토리 id
        [SerializeField] List<int> Basic_formation;

        ////////// Getter & Setter          //////////
        public List<int>    Basic_VarFormation  { get{ return Basic_formation;  }   }

        ////////// Method                   //////////
        public void Basic_Setting(int _id, int _formation)
        {
            // 배치되어 있다면 교체
            for (int for0 = 0; for0 < Basic_formation.Count; for0++)
            {
                if(Basic_formation[for0].Equals(_id))
                {
                    Basic_formation[for0] = Basic_formation[_formation];
                    
                    break;
                }
            }

            // 자리 갱신
            Basic_formation[_formation] = _id;
        }

        ////////// Constructor & Destroyer  //////////
        public Formation()
        {
            //
            if(Basic_formation == null)
            {
                Basic_formation = new List<int>();
            }

            while(Basic_formation.Count < 9)
            {
                Basic_formation.Add(-1);
            }
            Basic_formation[4] = -2;    // -2는 피노키오
        }

        public void Dispose()
        {
            
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
        Formation_list[Formation_select].Basic_Setting(id, formation);

        //
        return true;
    }

    ////////// Constructor & Destroyer  //////////
    void Formation_Contructor()
    {
        // Formation_list
        if(Formation_list == null)
        {
            Formation_list = new List<Formation>();
        }

        while(Formation_list.Count < 3)
        {
            Formation_list.Add(new Formation());
        }

        //
        //
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
    //
    object Item_GetItemList(params object[] _args)
    {
        List<Giggle_Item.Inventory> res = new List<Giggle_Item.Inventory>();

        for(int for0 = 0; for0 < Item_list.Count; for0++)
        {
            res.Add(Item_list[for0]);
        }

        return res;
    }

    //
    Giggle_Item.Inventory Item_GetItemFromInventoryId(int _id)
    {
        Giggle_Item.Inventory res = null;

        for(int for0 = 0; for0 < Item_list.Count; for0++)
        {
            if(Item_list[for0].Basic_VarInventoryId.Equals(_id))
            {
                res = Item_list[for0];
                break;
            }
        }

        return res;
    }

    object Item_GetItemFromInventoryId__ScriptBridge(params object[] _args)
    {
        int id = (int)_args[0];
        
        //
        return Item_GetItemFromInventoryId(id);
    }

    // Item_cards
    //
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

    //
    object Item_Crafting(params object[] _args)
    {
        int id = (int)_args[0];
        int count = (int)_args[1];

        //
        Giggle_Item.List data
            = (Giggle_Item.List)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.DATABASE__ITEM__GET_DATA_FROM_ID,
                id);
        
        Item_Looting(id, count);
        for(int for0 = 0; for0 < data.Basic_VarMattersCount; for0++)
        {
            Giggle_Item.List.Matter for0Element = data.Basic_GetMatterFromCount(for0);
            Item_DisCount(for0Element.Basic_VarId, for0Element.Basic_VarCount * count);
        }

        //
        return true;
    }

    //
    object Item_Looting__Bridge(params object[] _args)
    {
        int id = (int)_args[0];
        int count = (int)_args[1];

        //
        Giggle_Item.Inventory res = Item_Looting(id, count);

        //
        return res != null;
    }

    Giggle_Item.Inventory Item_Looting(int _id, int _count)
    {
        Giggle_Item.Inventory res = null;

        //
        Giggle_Item.List data
            = (Giggle_Item.List)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.DATABASE__ITEM__GET_DATA_FROM_ID,
                _id);
        
        // 장비일 때
        if(data.Basic_VarIsEquipment)
        {
            int inventoryId = 0;
            for(int for0 = 0; for0 < Item_list.Count; for0++)
            {
                if(inventoryId <= Item_list[for0].Basic_VarInventoryId)
                {
                    inventoryId = Item_list[for0].Basic_VarInventoryId + 1;
                }
            }

            for(int for0 = 0; for0 < _count; for0++)
            {
                res = new Giggle_Item.Inventory(inventoryId + for0, _id);
            }
        }
        // 기타
        else
        {
            for(int for0 = 0; for0 < Item_cardList.Count; for0++)
            {
                if(Item_cardList[for0].Basic_VarDataId.Equals(_id))
                {
                    res = Item_list[for0];
                    break;
                }
            }

            if(res != null)
            {
                res.Basic_AddCount(_count);
                if(res.Basic_VarCount <= 0)
                {
                    Item_list.Remove(res);
                }
            }
            else
            {
                int inventoryId = 0;
                for(int for0 = 0; for0 < Item_list.Count; for0++)
                {
                    if(inventoryId <= Item_list[for0].Basic_VarInventoryId)
                    {
                        inventoryId = Item_list[for0].Basic_VarInventoryId + 1;
                    }
                }

                res = new Giggle_Item.Inventory(inventoryId, _id);
                res.Basic_AddCount(_count);
            }
        }

        //
        return res;
    }

    //
    object Item_DisCount__Bridge(params object[] _args)
    {
        int id = (int)_args[0];
        int count = (int)_args[1];

        //
        Giggle_Item.Inventory element = null;

        for(int for0 = 0; for0 < Item_list.Count; for0++)
        {
            if(Item_list[for0].Basic_VarDataId.Equals(id))
            {
                element = Item_list[for0];
                break;
            }
        }

        if(element != null)
        {
            element.Basic_DisCount(count);
            if(element.Basic_VarCount <= 0)
            {
                Item_list.Remove(element);
            }
        }

        //
        return element != null;
    }

    Giggle_Item.Inventory Item_DisCount(int _id, int _count)
    {
        Giggle_Item.Inventory res = null;

        //

        for(int for0 = 0; for0 < Item_list.Count; for0++)
        {
            if(Item_list[for0].Basic_VarDataId.Equals(_id))
            {
                res = Item_list[for0];
                break;
            }
        }

        if(res != null)
        {
            res.Basic_DisCount(_count);
            if(res.Basic_VarCount <= 0)
            {
                Item_list.Remove(res);
            }
        }

        //
        return res;
    }

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
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__ITEM__GET_ITEM_FROM_INVENTORY_ID,   Item_GetItemFromInventoryId__ScriptBridge   );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__ITEM__GET_CARD_LIST,                Item_GetCardList            );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__CARD__GET_DATA_FROM_DATA_ID,        Item_GetCardDataFromDataId  );

        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__ITEM__CRAFTING,     Item_Crafting           );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__ITEM__LOOTING,      Item_Looting__Bridge    );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__ITEM__DIS_COUNT,    Item_DisCount__Bridge   );
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

    #region DUNGEON

    [Header("DUNGEON ==================================================")]
    [SerializeField] Formation  Dungeon_formation;

    ////////// Getter & Setter          //////////

    object Dungeon_GetSelectFormation(params object[] _args)
    {
        //
        return Dungeon_formation.Basic_VarFormation;
    }

    ////////// Method                   //////////
    
    object Dungeon_FormationSetting(params object[] _args)
    {
        int id          = (int)_args[0];
        int formation   = (int)_args[1];

        //
        Dungeon_formation.Basic_Setting(id, formation);

        //
        return true;
    }

    ////////// Constructor & Destroyer  //////////
    void Dungeon_Contructor()
    {
        // Dungeon_formation
        Dungeon_formation = new Formation();

        //
        //
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__DUNGEON__GET_FORMATION,     Dungeon_GetSelectFormation  );

        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__DUNGEON__FORMATION_SETTING, Dungeon_FormationSetting    );
    }

    #endregion
}
