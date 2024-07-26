using UnityEngine;
//
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;

[Serializable]
public class Giggle_Database : IDisposable
{
    #region BASIC
    ////////// Getter & Setter          //////////

    List<Dictionary<string, string>> Basic_CSVLoad(TextAsset _file)
    {
        //
        List<Dictionary<string, string>> res = new List<Dictionary<string, string>>();

        // 데이터 추가
        // 0번은 키값
        string[] strs = _file.text.Replace("\r", "").Split('\n');
        string[] strsKey = strs[2].Split(',');
        for (int i = 3; i < strs.Length; i++)
        {
            Dictionary<string, string> element = new Dictionary<string, string>();

            //
            string[] strsElement = strs[i].Split(',');
            for (int j = 0; j < strsKey.Length; j++)
            {
                element.Add(strsKey[j], strsElement[j]);
            }

            //
            res.Add(element);
        }

        return res;
    }

    ////////// Method                   //////////

    ////////// Constructor & Destroyer  //////////
    public Giggle_Database()
    {
        Character_Init();
        Item_Init();
    }

    public void Dispose()
    {

    }

    #endregion

    #region CHARACTER

    [Header("CHARACTER ==================================================")]
    [SerializeField] Giggle_Character.Database Character_empty;

    ////////// Getter & Setter          //////////
    
    ////////// Method                   //////////

    ////////// Constructor & Destroyer  //////////
    void Character_Init()
    {
        Character_isOpen = true;
        if(Character_empty == null)
        {
            Character_empty = new Giggle_Character.Database();
        }

        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.DATABASE__CHARACTER__GET_IS_OPEN,   Character_GetIsOpen );

        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.DATABASE__CHARACTER__GET_DATA_FROM_ID,          Character_GetDataFromId         );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.DATABASE__CHARACTER__GET_DATAS_FROM_ATTRIBUTE,  Character_GetDatasFromAttribute );

        Pinocchio_Init();
    }

    #region PINOCCHIO

    [Header("PINOCCHIO ==========")]
    [SerializeField] Character_Data Pinocchio_data;

    [SerializeField] List<Giggle_Character.Skill> Pinocchio_skills;

    [SerializeField] List<Giggle_Character.Attribute> Pinocchio_attributeAttacks;
    [SerializeField] List<Giggle_Character.Attribute> Pinocchio_attributeDefences;
    [SerializeField] List<Giggle_Character.Attribute> Pinocchio_attributeSupports;

    [SerializeField] List<Giggle_Character.Ability>             Pinocchio_abilitys;
    [SerializeField] List<Giggle_Character.Ability_Probability> Pinocchio_abilityProbabilitys;

    [SerializeField] bool Pinocchio_isOpen;

    ////////// Getter & Setter          //////////
    object Pinocchio_GetIsOpen(params object[] _args)
    {
        return Pinocchio_isOpen;
    }

    // Pinocchio_data
    object Pinocchio_GetDataFromId(params object[] _args)
    {
        Giggle_Character.Database res = Character_empty;

        //
        int id = (int)_args[0];

        res = Pinocchio_data.Basic_GetDataFromId(id);
        if(res == null)
        {
            res = Character_empty;
        }

        // 찾고자 하는 캐릭터가 존재하는가?
        if(res.Equals(Character_empty))
        {
            if(Pinocchio_isOpen)
            {
                IEnumerator coroutine = Pinocchio_LoadData__Coroutine();
                Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MASTER__BASIC__START_COROUTINE, coroutine);
            }
        }

        //
        return res;
    }

    // Pinocchio_skills
    object Pinocchio_GetSkillFromId(params object[] _args)
    {
        Giggle_Character.Skill res = null;

        //
        int id = (int)_args[0];

        for(int for0 = 0; for0 < Pinocchio_skills.Count; for0++)
        {
            if(Pinocchio_skills[for0].Basic_VarId.Equals(id))
            {
                res = Pinocchio_skills[for0];
                break;
            }
        }

        //
        return res;
    }

    // Pinocchio_attributeAttacks
    object Pinocchio_GetAttributeAttack(params object[] _args)
    {
        return Pinocchio_attributeAttacks;
    }

    object Pinocchio_GetAttributeAttackFromId(params object[] _args)
    {
        Giggle_Character.Attribute res = null;

        //
        int id = (int)_args[0];

        for(int for0 = 0; for0 < Pinocchio_attributeAttacks.Count; for0++)
        {
            if(Pinocchio_attributeAttacks[for0].Basic_VarId.Equals(id))
            {
                res = Pinocchio_attributeAttacks[for0];
                break;
            }
        }

        //
        return res;
    }

    // Pinocchio_attributeDefences
    // Pinocchio_attributeSupports

    // Pinocchio_abilitys
    object Pinocchio_GetAbilitysFromGrade(params object[] _args)
    {
        Giggle_Character.Ability res = null;

        //
        int grade = (int)_args[0];

        res = Pinocchio_abilitys[grade - 1];

        //
        return res;
    }

    object Pinocchio_GetAbilityRandomFromGrade(params object[] _args)
    {
        Giggle_Character.AbilityClass res = null;

        //
        int grade = (int)_args[0];

        res = Pinocchio_abilitys[grade].Basic_GetRandomData;

        //
        return res;
    }

    object Pinocchio_GetAbilityFromElement(params object[] _args)
    {
        Giggle_Character.AbilityClass res = null;

        //
        int id      = (int)_args[0];
        int grade   = (int)_args[1];

        for(int for0 = 0; for0 < Pinocchio_abilitys[grade].Basic_VarListCount; for0++)
        {
            if(Pinocchio_abilitys[grade].Basic_GetListFromCount(for0).Basic_VarId.Equals(id))
            {
                res = Pinocchio_abilitys[grade].Basic_GetListFromCount(for0);
                break;
            }
        }

        //
        return res;
    }

    object Pinocchio_AbilityGetProbabilityFromLevel(params object[] _args)
    {
        //
        int level = (int)_args[0];

        //
        return Pinocchio_abilityProbabilitys[level - 1];
    }
    
    ////////// Method                   //////////

    // Pinocchio_LoadData__Coroutine
    IEnumerator Pinocchio_LoadData__Coroutine()
    {
        Pinocchio_isOpen = false;

        //
        int phase = 0;

        while (phase != -1)
        {
            switch(phase)
            {
                // 캐릭터 데이터
                // 리스트
                case 0:
                    {
                        phase = 1;
                        Addressables.LoadAssetAsync<TextAsset>("PINOCCHIO/CSV_LIST").Completed
                        += handle =>
                        {
                            List<Dictionary<string, string>> data = Basic_CSVLoad(handle.Result);
                            for(int for0 = 0; for0 < data.Count; for0++)
                            {
                                Pinocchio_data.Basic_SetData(data[for0]);
                            }

                            phase = 2;
                        };
                    }
                    break;
                // 캐릭터 레벨
                case 2:
                    {
                        phase = 3;
                        Addressables.LoadAssetAsync<TextAsset>("PINOCCHIO/CSV_LV").Completed
                        += handle =>
                        {
                            List<Dictionary<string, string>> data = Basic_CSVLoad(handle.Result);
                            for(int for0 = 0; for0 < data.Count; for0++)
                            {
                                int id = int.Parse(data[for0]["cha_id"]);
                                Pinocchio_data.Basic_GetDataFromId(id).Basic_SetStatusList(data[for0]);
                            }

                            phase = 100;
                        };
                    }
                    break;
                // 캐릭터 모델링
                case 100:
                    {
                        phase = 101;

                        Addressables.LoadAssetAsync<GameObject>("PINOCCHIO/MODEL").Completed
                        += handle =>
                        {
                            GameObject res = handle.Result;
                            for(int for0 = 0; for0 < res.transform.childCount; for0++)
                            {
                                Transform child = res.transform.GetChild(for0);
                                if(child.gameObject.activeSelf)
                                {
                                    int id = int.Parse(child.name);
                                    Pinocchio_data.Basic_GetDataFromId(id).Basic_VarUnit = child.GetComponent<Giggle_Unit>();
                                }
                            }

                            phase = 200;
                        };
                    }
                    break;
                // 스킬
                // 리스트
                case 200:
                    {
                        phase = 201;

                        if(Pinocchio_skills == null)
                        {
                            Pinocchio_skills = new List<Giggle_Character.Skill>();
                        }

                        Addressables.LoadAssetAsync<TextAsset>("PINOCCHIO/CSV_SKILL_LIST").Completed
                        += handle =>
                        {
                            List<Dictionary<string, string>> data = Basic_CSVLoad(handle.Result);
                            for(int for0 = 0; for0 < data.Count; for0++)
                            {
                                Pinocchio_skills.Add(new Giggle_Character.Skill(data[for0]));
                            }

                            phase = 202;
                        };
                    }
                    break;
                // 스킬 레벨
                case 202:
                    {
                        phase = 203;

                        Addressables.LoadAssetAsync<TextAsset>("PINOCCHIO/CSV_SKILL_LV").Completed
                        += handle =>
                        {
                            List<Dictionary<string, string>> data = Basic_CSVLoad(handle.Result);
                            for(int for0 = 0; for0 < data.Count; for0++)
                            {
                                int id = int.Parse(data[for0]["cha_skill_id"]);

                                for(int for1 = 0; for1 < Pinocchio_skills.Count; for1++)
                                {
                                    if(Pinocchio_skills[for1].Basic_VarId.Equals(id))
                                    {
                                        Pinocchio_skills[for1].Basic_SetLvList(data[for0]);
                                        break;
                                    }
                                }
                            }

                            phase = 300;
                        };
                    }
                    break;
                // 특성
                case 300:
                    {
                        phase = 301;
                        
                        if(Pinocchio_attributeAttacks == null)
                        {
                            Pinocchio_attributeAttacks = new List<Giggle_Character.Attribute>();
                        }

                        if(Pinocchio_attributeDefences == null)
                        {
                            Pinocchio_attributeDefences = new List<Giggle_Character.Attribute>();
                        }

                        if(Pinocchio_attributeSupports == null)
                        {
                            Pinocchio_attributeSupports = new List<Giggle_Character.Attribute>();
                        }

                        Addressables.LoadAssetAsync<TextAsset>("PINOCCHIO/CSV_ATTRIBUTE_LIST").Completed
                        += handle =>
                        {
                            List<Dictionary<string, string>> data = Basic_CSVLoad(handle.Result);
                            for(int for0 = 0; for0 < data.Count; for0++)
                            {
                                int id = int.Parse(data[for0]["attribute_id"]);

                                int attributeClass = id / 1000;
                                attributeClass %= 10;

                                switch(attributeClass)
                                {
                                    case 1: { Pinocchio_attributeAttacks.Add(   new Giggle_Character.Attribute(data[for0]));    }   break;
                                    case 2: { Pinocchio_attributeDefences.Add(  new Giggle_Character.Attribute(data[for0]));    }   break;
                                    case 3: { Pinocchio_attributeSupports.Add(  new Giggle_Character.Attribute(data[for0]));    }   break;
                                }
                            }

                            phase = 302;
                        };
                    }
                    break;
                // 스킬 레벨
                case 302:
                    {
                        phase = 303;

                        Addressables.LoadAssetAsync<TextAsset>("PINOCCHIO/CSV_ATTRIBUTE_LV").Completed
                        += handle =>
                        {
                            List<Dictionary<string, string>> data = Basic_CSVLoad(handle.Result);
                            for(int for0 = 0; for0 < data.Count; for0++)
                            {
                                int id = int.Parse(data[for0]["cha_attribute_id"]);

                                int attributeClass = id / 1000;
                                attributeClass %= 10;

                                List<Giggle_Character.Attribute> list = Pinocchio_attributeAttacks;
                                switch(attributeClass)
                                {
                                    case 2: { list = Pinocchio_attributeDefences;   }   break;
                                    case 3: { list = Pinocchio_attributeSupports;   }   break;
                                }

                                for(int for1 = 0; for1 < list.Count; for1++)
                                {
                                    if(list[for1].Basic_VarId.Equals(id))
                                    {
                                        list[for1].Basic_AddLv(data[for0]);
                                        break;
                                    }
                                }
                            }

                            phase = 400;
                        };
                    }
                    break;
                // 재능
                case 400:
                    {
                        phase = 401;

                        if(Pinocchio_abilitys == null)
                        {
                            Pinocchio_abilitys = new List<Giggle_Character.Ability>();
                        }

                        Addressables.LoadAssetAsync<TextAsset>("PINOCCHIO/CSV_ABILITY_LIST").Completed
                        += handle =>
                        {
                            List<Dictionary<string, string>> data = Basic_CSVLoad(handle.Result);
                            for(int for0 = 0; for0 < data.Count; for0++)
                            {
                                int aClass = int.Parse(data[for0]["cha_ability_class"]) -1;

                                while(Pinocchio_abilitys.Count <= aClass)
                                {
                                    Pinocchio_abilitys.Add(new Giggle_Character.Ability());
                                }

                                Pinocchio_abilitys[aClass].Basic_Add(data[for0]);
                            }

                            phase = 402;
                        };
                    }
                    break;
                case 402:
                    {
                        phase = 403;

                        if(Pinocchio_abilityProbabilitys == null)
                        {
                            Pinocchio_abilityProbabilitys = new List<Giggle_Character.Ability_Probability>();
                        }

                        Addressables.LoadAssetAsync<TextAsset>("PINOCCHIO/CSV_ABILITY_PROBABILITY").Completed
                        += handle =>
                        {
                            List<Dictionary<string, string>> data = Basic_CSVLoad(handle.Result);
                            for(int for0 = 0; for0 < data.Count; for0++)
                            {
                                Giggle_Character.Ability_Probability element = new Giggle_Character.Ability_Probability(data[for0]);
                                while(Pinocchio_abilityProbabilitys.Count < element.Basic_VarLevel)
                                {
                                    Pinocchio_abilityProbabilitys.Add(null);
                                }
                                Pinocchio_abilityProbabilitys[element.Basic_VarLevel - 1] = element;
                            }

                            phase = 404;
                        };
                    }
                    break;
                case 404:
                    {
                        Pinocchio_isOpen = true;
                        phase = -1;
                    }
                    break;
            }

            yield return null;
        }
    }

    ////////// Constructor & Destroyer  //////////
    void Pinocchio_Init()
    {
        if(Pinocchio_data == null)
        {
            Pinocchio_data = new Character_Data();
        }
        Pinocchio_isOpen = true;

        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.DATABASE__PINOCCHIO__GET_IS_OPEN,   Pinocchio_GetIsOpen );

        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.DATABASE__PINOCCHIO__GET_DATA_FROM_ID,                      Pinocchio_GetDataFromId                     );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.DATABASE__PINOCCHIO__GET_SKILL_FROM_ID,                     Pinocchio_GetSkillFromId                    );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.DATABASE__PINOCCHIO__GET_ATTRIBUTE_ATTACK,                  Pinocchio_GetAttributeAttack                );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.DATABASE__PINOCCHIO__GET_ATTRIBUTE_ATTACK_FROM_ID,          Pinocchio_GetAttributeAttackFromId          );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.DATABASE__PINOCCHIO__GET_ABILITYS_FROM_GRADE,               Pinocchio_GetAbilitysFromGrade              );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.DATABASE__PINOCCHIO__GET_ABILITY_RANDOM_FROM_GRADE,         Pinocchio_GetAbilityRandomFromGrade         );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.DATABASE__PINOCCHIO__ABILITY_GET_ABILITY_FROM_ELEMENT,      Pinocchio_GetAbilityFromElement             );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.DATABASE__PINOCCHIO__ABILITY_GET_PROBABILITY_FROM_LEVEL,    Pinocchio_AbilityGetProbabilityFromLevel    );
    }
    
    #endregion

    //[Header("CHARACTER ==================================================")]

    [Serializable]
    public class Character_Data : IDisposable
    {
        [SerializeField] List<Giggle_Character.Database> Basic_characters;

        ////////// Getter & Setter          //////////
        public Giggle_Character.Database Basic_GetDataFromId(int _id)
        {
            Giggle_Character.Database res = null;

            //
            for(int for0 = 0; for0 < Basic_characters.Count; for0++)
            {
                if(Basic_characters[for0].Basic_GetIdIsSame(_id))
                {
                    res = Basic_characters[for0];
                    break;
                }
            }

            //
            return res;
        }

        public Giggle_Character.Database    Basic_GetDataFromCount(int _count)  { return Basic_characters[_count];  }

        public int                          Basic_VarCount                      { get{ return Basic_characters.Count;   }   }

        public void Basic_SetData(Dictionary<string, string> _data)
        {
            Giggle_Character.Database element = null;

            if(_data.ContainsKey("cha_gender"))
            {
                element = new Giggle_Character.Database_Pinocchio(_data);
            }
            else
            {
                element = new Giggle_Character.Database_Marionette(_data);
            }

            Basic_characters.Add(element);
        }

        ////////// Method                   //////////

        ////////// Constructor & Destroyer  //////////
        public Character_Data()
        {
            if(Basic_characters == null)
            {
                Basic_characters = new List<Giggle_Character.Database>();
            }
        }

        public void Dispose()
        {

        }
    }

    [SerializeField] List<Character_Data> Character_datas;
    [SerializeField] bool Character_isOpen;

    ////////// Getter & Setter          //////////
    object Character_GetIsOpen(params object[] _args)
    {
        return Character_isOpen;
    }

    object Character_GetDataFromId(params object[] _args)
    {
        Giggle_Character.Database res = Character_empty;

        //
        int id = (int)_args[0];

        // 찾고자 하는 캐릭터가 존재하는가?
        int attribute = id % 10;
        if (attribute < Character_datas.Count)
        {
            res = Character_datas[attribute].Basic_GetDataFromId(id);
            if(res == null)
            {
                res = Character_empty;
            }
        }

        if(res.Equals(Character_empty))
        {
            if(Character_isOpen)
            {
                IEnumerator coroutine = Character_LoadData__Coroutine(attribute);
                Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MASTER__BASIC__START_COROUTINE, coroutine);
            }
        }

        //
        return res;
    }

    object Character_GetDatasFromAttribute(params object[] _args)
    {
        List<Giggle_Character.Database> res = new List<Giggle_Character.Database>();

        //
        string[] attributes = ((string)_args[0]).Split('|');
        for(int for0 = 0; for0 < attributes.Length; for0++)
        {
            Giggle_Master.ATTRIBUTE attribute = (Giggle_Master.ATTRIBUTE)Enum.Parse(typeof(Giggle_Master.ATTRIBUTE), attributes[for0]);
            Character_Data data = Character_datas[(int)attribute];
            for(int for1 = 0; for1 < data.Basic_VarCount; for1++)
            {
                res.Add(data.Basic_GetDataFromCount(for1));
            }
        }

        //
        return res;
    }

    ////////// Method                   //////////

    IEnumerator Character_LoadData__Coroutine(int _attribute)
    {
        Character_isOpen = false;

        //
        int phase = 0;

        while (phase != -1)
        {
            switch(phase)
            {
                // 캐릭터 데이터
                // 리스트
                case 0:
                    {
                        phase = 1;

                        while(Character_datas.Count <= _attribute)
                        {
                            Character_datas.Add(new Character_Data());
                        }

                        Giggle_Master.ATTRIBUTE attribute = (Giggle_Master.ATTRIBUTE)_attribute;
                        Addressables.LoadAssetAsync<TextAsset>("CHARACTER/" + attribute.ToString() + "_CSV_LIST").Completed
                        += handle =>
                        {
                            List<Dictionary<string, string>> data = Basic_CSVLoad(handle.Result);
                            for(int for0 = 0; for0 < data.Count; for0++)
                            {
                                Character_datas[_attribute].Basic_SetData(data[for0]);
                            }

                            phase = 2;
                        };
                    }
                    break;
                // 캐릭터 레벨
                case 2:
                    {
                        phase = 3;

                        Giggle_Master.ATTRIBUTE attribute = (Giggle_Master.ATTRIBUTE)_attribute;
                        Addressables.LoadAssetAsync<TextAsset>("CHARACTER/" + attribute.ToString() + "_CSV_LV").Completed
                        += handle =>
                        {
                            List<Dictionary<string, string>> data = Basic_CSVLoad(handle.Result);
                            for(int for0 = 0; for0 < data.Count; for0++)
                            {
                                int id = int.Parse(data[for0]["cha_id"]);
                                Character_datas[_attribute].Basic_GetDataFromId(id).Basic_SetStatusList(data[for0]);
                            }

                            phase = 100;
                        };
                    }
                    break;
                // 스킬
                // 리스트
                case 100:
                    {
                        phase = 101;

                        Giggle_Master.ATTRIBUTE attribute = (Giggle_Master.ATTRIBUTE)_attribute;
                        Addressables.LoadAssetAsync<TextAsset>("CHARACTER/" + attribute.ToString() + "_CSV_SKILL_LIST").Completed
                        += handle =>
                        {
                            List<Dictionary<string, string>> data = Basic_CSVLoad(handle.Result);
                            for(int for0 = 0; for0 < data.Count; for0++)
                            {
                                int id = int.Parse(data[for0]["cha_id"]);
                                Character_datas[_attribute].Basic_GetDataFromId(id).Basic_SetSkillList(data[for0]);
                            }

                            phase = 102;
                        };
                    }
                    break;
                // 스킬 레벨
                case 102:
                    {
                        phase = 103;

                        Giggle_Master.ATTRIBUTE attribute = (Giggle_Master.ATTRIBUTE)_attribute;
                        Addressables.LoadAssetAsync<TextAsset>("CHARACTER/" + attribute.ToString() + "_CSV_SKILL_LV").Completed
                        += handle =>
                        {
                            List<Giggle_Character.Skill> skills = new List<Giggle_Character.Skill>();

                            List<Dictionary<string, string>> data = Basic_CSVLoad(handle.Result);
                            for(int for0 = 0; for0 < data.Count; for0++)
                            {
                                int id = int.Parse(data[for0]["cha_skill_id"]);

                                Giggle_Character.Skill whileSkill = null;
                                while(whileSkill == null)
                                {
                                    for(int for1 = 0; for1 < skills.Count; for1++)
                                    {
                                        if(skills[for1].Basic_VarId.Equals(id))
                                        {
                                            whileSkill = skills[for1];
                                            break;
                                        }
                                    }

                                    if(whileSkill == null)
                                    {
                                        for(int for2 = 0; for2 < Character_datas[_attribute].Basic_VarCount; for2++)
                                        {
                                            Giggle_Character.Skill element = Character_datas[_attribute].Basic_GetDataFromCount(for2).Basic_GetSkillListFromId(id);
                                            if(element != null)
                                            {
                                                skills.Add(Character_datas[_attribute].Basic_GetDataFromCount(for2).Basic_GetSkillListFromId(id));
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        whileSkill.Basic_SetLvList(data[for0]);
                                    }
                                }
                            }

                            phase = 200;
                        };
                    }
                    break;
                // 캐릭터 모델링
                case 200:
                    {
                        phase = 201;

                        Giggle_Master.ATTRIBUTE attribute = (Giggle_Master.ATTRIBUTE)_attribute;
                        Addressables.LoadAssetAsync<GameObject>("CHARACTER/" + attribute.ToString() + "_MODEL").Completed
                        += handle =>
                        {
                            GameObject res = handle.Result;
                            for(int for0 = 0; for0 < res.transform.childCount; for0++)
                            {
                                Transform child = res.transform.GetChild(for0);
                                if(child.gameObject.activeSelf)
                                {
                                    int id = int.Parse(child.name);
                                    Character_datas[_attribute].Basic_GetDataFromId(id).Basic_VarUnit = child.GetComponent<Giggle_Unit>();
                                }
                            }

                            Character_isOpen = true;
                            phase = -1;
                        };
                    }
                    break;
            }

            yield return null;
        }
    }

    ////////// Constructor & Destroyer  //////////

    #endregion

    #region ITEM

    [Serializable]
    public class Item_Data : IDisposable
    {
        [SerializeField] List<Giggle_Item.List> Basic_items;

        ////////// Getter & Setter          //////////
        public Giggle_Item.List Basic_GetDataFromId(int _id)
        {
            Giggle_Item.List res = null;

            //
            for(int for0 = 0; for0 < Basic_items.Count; for0++)
            {
                if(Basic_items[for0].Basic_GetIdIsSame(_id))
                {
                    res = Basic_items[for0];
                    break;
                }
            }

            //
            return res;
        }

        public Giggle_Item.List Basic_GetDataFromCount(int _count)  { return Basic_items[_count];   }

        public int              Basic_VarCount                      { get{ return Basic_items.Count;    }   }

        public void Basic_SetData(Dictionary<string, string> _data)
        {
            Giggle_Item.List element = new Giggle_Item.List(_data);
            Basic_items.Add(element);
        }

        ////////// Method                   //////////

        ////////// Constructor & Destroyer  //////////
        public Item_Data()
        {
            if(Basic_items == null)
            {
                Basic_items = new List<Giggle_Item.List>();
            }
        }

        public void Dispose()
        {

        }

    }

    [Header("ITEM ==================================================")]
    [SerializeField] List<Item_Data>    Item_datas;
    [SerializeField] bool   Item_isOpen;
    [SerializeField] Giggle_Item.List   Item_empty;

    ////////// Getter & Setter          //////////
    object Item_GetIsOpen(params object[] _args)
    {
        return Item_isOpen;
    }

    object Item_GetDataFromId(params object[] _args)
    {
        Giggle_Item.List res = Item_empty;

        //
        int id = (int)_args[0];

        // 아이디의 자릿수 구하기
        double idLength = Math.Floor(Math.Log10(id));
        // 찾고자 하는 캐릭터가 존재하는가?
        int itemType = (id / (int)Math.Pow(10, idLength - 1)) % 10;
        if(itemType > 0)
        {
            itemType /= 2;
        }

        if (itemType < Item_datas.Count)
        {
            res = Item_datas[itemType].Basic_GetDataFromId(id);
            if(res == null)
            {
                res = Item_empty;
            }
        }

        if(res.Equals(Item_empty))
        {
            if(Item_isOpen)
            {
                IEnumerator coroutine = Item_LoadData__Coroutine(itemType);
                Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MASTER__BASIC__START_COROUTINE, coroutine);
            }
        }

        //
        return res;
    }

    ////////// Method                   //////////

    IEnumerator Item_LoadData__Coroutine(int _itemType)
    {
        Item_isOpen = false;

        //
        int phase = 0;

        while (phase != -1)
        {
            switch(phase)
            {
                // 아이템 데이터
                // 리스트
                case 0:
                    {
                        phase = 1;

                        while(Item_datas.Count <= _itemType)
                        {
                            Item_datas.Add(new Item_Data());
                        }

                        Giggle_Item.TYPE itemType = (Giggle_Item.TYPE)_itemType;
                        Addressables.LoadAssetAsync<TextAsset>("ITEM/" + itemType.ToString() + "_CSV_LIST").Completed
                        += handle =>
                        {
                            List<Dictionary<string, string>> data = Basic_CSVLoad(handle.Result);
                            for(int for0 = 0; for0 < data.Count; for0++)
                            {
                                Item_datas[_itemType].Basic_SetData(data[for0]);
                            }

                            phase = -1;
                            Item_isOpen = true;
                        };
                    }
                    break;
            }
            
            yield return null;
        }
    }

    ////////// Constructor & Destroyer  //////////
    void Item_Init()
    {
        Item_isOpen = true;
        if(Item_empty == null)
        {
            Item_empty = new Giggle_Item.List();
        }

        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.DATABASE__ITEM__GET_IS_OPEN,    Item_GetIsOpen  );

        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.DATABASE__ITEM__GET_DATA_FROM_ID,   Item_GetDataFromId  );
    }

    #endregion
}
