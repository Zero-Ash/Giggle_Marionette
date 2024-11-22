using UnityEngine;
//
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using Unity.VisualScripting;

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
    public Giggle_Database(GameObject _obj)
    {
        Character_Init();
        Stage_Init(_obj);
        Item_Init();
        Quest_Init();
    }

    public void Dispose()
    {

    }

    #endregion

    #region CHARACTER

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

    [Header("CHARACTER ==================================================")]
    [SerializeField] Giggle_Character.Database  Character_empty;

    [SerializeField] List<Sprite>   Character_skillBacks;

    ////////// Getter & Setter          //////////
    object Character_GetSkillBackFromRank(params object[] _args)
    {
        //
        int rank = (int)_args[0];

        //
        return Character_skillBacks[rank - 1];
    }
    
    ////////// Method                   //////////

    ////////// Constructor & Destroyer  //////////
    void Character_Init()
    {
        if(Character_empty == null)
        {
            Character_empty = new Giggle_Character.Database();
        }
        
        if(Character_skillBacks == null)
        {
            Character_skillBacks = new List<Sprite>();
        }

        Addressables.LoadAssetAsync<GameObject>("CHARACTER/SKILL_BACKGROUND").Completed
        += handle =>
        {
            Transform trans = handle.Result.transform;
            for(int for0 = 0; for0 < trans.childCount; for0++)
            {
                Sprite sprite = trans.GetChild(for0).GetComponent<Image>().sprite;
                int num = int.Parse(sprite.name.Split('_')[0]);

                while(Character_skillBacks.Count < num)
                {
                    Character_skillBacks.Add(null);
                }

                Character_skillBacks[num - 1] = sprite;
            }
        };
        
        Pinocchio_Init();
        Marionette_Init();

        //
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.DATABASE__CHARACTER__GET_SKILL_BACK_FROM_RANK,  Character_GetSkillBackFromRank  );
    }

    #region PINOCCHIO

    [Serializable]
    public class Pinocchio_Attribute : IDisposable
    {
        [SerializeField] List<Giggle_Character.Attribute>   Basic_list;

        ////////// Getter & Setter          //////////
        
        // Basic_list
        public List<Giggle_Character.Attribute> Basic_VarList                       { get { return Basic_list;          }   }

        public int                              Basic_VarListCount                  { get { return Basic_list.Count;    }   }

        public Giggle_Character.Attribute       Basic_GetDataFromCount(int _count)  { return Basic_list[_count];            }
        
        ////////// Method                   //////////

        public void Basic_Add(Dictionary<string, string> _data)
        {
            Basic_list.Add(new Giggle_Character.Attribute(_data));
        }

        ////////// Constructor & Destroyer  //////////
        
        public Pinocchio_Attribute()
        {
            if(Basic_list == null)  { Basic_list = new List<Giggle_Character.Attribute>();  }
        }

        //
        public void Dispose()
        {

        }
    }

    [Header("PINOCCHIO ==========")]
    [SerializeField] Character_Data Pinocchio_data;

    [SerializeField] List<Giggle_Character.Skill> Pinocchio_skills;

    [SerializeField] List<Pinocchio_Attribute>  Pinocchio_attributes;

    [SerializeField] List<Giggle_Character.Ability>             Pinocchio_abilitys;
    [SerializeField] List<Giggle_Character.Ability_Probability> Pinocchio_abilityProbabilitys;
    [SerializeField] List<Sprite>                               Pinocchio_abilityWoodIcons;

    [SerializeField] List<Giggle_Item.Relic>   Pinocchio_relics;

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

    // Pinocchio_attributes
    object Pinocchio_GetAttribute(params object[] _args)
    {
        Giggle_Player.ATTRIBUTE_TYPE type0  = (Giggle_Player.ATTRIBUTE_TYPE)_args[0];

        //
        return Pinocchio_attributes[(int)type0].Basic_VarList;
    }

    object Pinocchio_GetAttributeFromId(params object[] _args)
    {
        Giggle_Character.Attribute res = null;

        //
        Giggle_Player.ATTRIBUTE_TYPE type0  = (Giggle_Player.ATTRIBUTE_TYPE)_args[0];
        int id = (int)_args[1];

        for(int for0 = 0; for0 < Pinocchio_attributes[(int)type0].Basic_VarListCount; for0++)
        {
            if(Pinocchio_attributes[(int)type0].Basic_GetDataFromCount(for0).Basic_VarId.Equals(id))
            {
                res = Pinocchio_attributes[(int)type0].Basic_GetDataFromCount(for0);
                break;
            }
        }

        //
        return res;
    }

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

    object Pinocchio_AbilityGetWoodIconFromSelect(params object[] _args)
    {
        //
        int select = (int)_args[0];

        //
        return Pinocchio_abilityWoodIcons[select];
    }

    //
    object Pinocchio_RelicGetDataFromId(params object[] _args)
    {
        int id = (int)_args[0];

        //
        Giggle_Item.Relic res = null;

        for(int for0 = 0; for0 < Pinocchio_relics.Count; for0++)
        {
            if(Pinocchio_relics[for0].Basic_VarId.Equals(id))
            {
                res = Pinocchio_relics[for0];
                break;
            }
        }

        if(res == null)
        {
            res = new Giggle_Item.Relic();
        }

        //
        return res;
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
                #region DATA
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
                #endregion
                // 스킬
                #region SKILL

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
                // 레벨
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
                
                #endregion
                // 특성
                case 300:
                    {
                        phase = 301;
                        
                        if(Pinocchio_attributes == null)
                        {
                            Pinocchio_attributes = new List<Pinocchio_Attribute>();
                        }
                        for(int for0 = 0; for0 < (int)Giggle_Player.ATTRIBUTE_TYPE.TOTAL; for0++)
                        {
                            Pinocchio_attributes.Add(new Pinocchio_Attribute());
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

                                Pinocchio_attributes[attributeClass - 1].Basic_Add(data[for0]);
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

                                List<Giggle_Character.Attribute> list = Pinocchio_attributes[attributeClass - 1].Basic_VarList;

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
                #region ABILITY

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
                        phase = 405;

                        if(Pinocchio_abilityWoodIcons == null)
                        {
                            Pinocchio_abilityWoodIcons = new List<Sprite>();
                        }

                        Addressables.LoadAssetAsync<GameObject>("PINOCCHIO/ABILITY_WOOD_ICON").Completed
                        += handle =>
                        {
                            GameObject data = handle.Result;

                            while(Pinocchio_abilityWoodIcons.Count < data.transform.childCount)
                            {
                                Pinocchio_abilityWoodIcons.Add(null);
                            }

                            for(int for0 = 0; for0 < data.transform.childCount; for0++)
                            {
                                Pinocchio_abilityWoodIcons[for0] = data.transform.Find(for0.ToString()).GetComponent<Image>().sprite;
                            }

                            phase = 500;
                        };
                    }
                    break;

                #endregion
                // 유물
                // 목록
                case 500:
                    {
                        phase = 501;

                        if(Pinocchio_relics == null)
                        {
                            Pinocchio_relics = new List<Giggle_Item.Relic>();
                        }

                        Addressables.LoadAssetAsync<TextAsset>("PINOCCHIO/CSV_RELIC_LIST").Completed
                        += handle =>
                        {
                            List<Dictionary<string, string>> data = Basic_CSVLoad(handle.Result);
                            for(int for0 = 0; for0 < data.Count; for0++)
                            {
                                Pinocchio_relics.Add(new Giggle_Item.Relic(data[for0]));
                            }

                            phase = 502;
                        };
                    }
                    break;
                case 502:
                    {
                        phase = 503;

                        Addressables.LoadAssetAsync<TextAsset>("PINOCCHIO/CSV_RELIC_LV").Completed
                        += handle =>
                        {
                            List<Dictionary<string, string>> data = Basic_CSVLoad(handle.Result);
                            for(int for0 = 0; for0 < data.Count; for0++)
                            {
                                int id = int.Parse(data[for0]["cha_relic_id"]);

                                for(int for1 = 0; for1 < Pinocchio_relics.Count; for1++)
                                {
                                    if(Pinocchio_relics[for1].Basic_VarId.Equals(id))
                                    {
                                        Pinocchio_relics[for1].Basic_SetLvList(data[for0]);
                                        break;
                                    }
                                }
                            }

                            phase = 504;
                        };
                    }
                    break;
                case 504:
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
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.DATABASE__PINOCCHIO__GET_ATTRIBUTE,                         Pinocchio_GetAttribute                      );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.DATABASE__PINOCCHIO__GET_ATTRIBUTE_FROM_ID,                 Pinocchio_GetAttributeFromId                );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.DATABASE__PINOCCHIO__GET_ABILITYS_FROM_GRADE,               Pinocchio_GetAbilitysFromGrade              );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.DATABASE__PINOCCHIO__GET_ABILITY_RANDOM_FROM_GRADE,         Pinocchio_GetAbilityRandomFromGrade         );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.DATABASE__PINOCCHIO__ABILITY_GET_ABILITY_FROM_ELEMENT,      Pinocchio_GetAbilityFromElement             );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.DATABASE__PINOCCHIO__ABILITY_GET_PROBABILITY_FROM_LEVEL,    Pinocchio_AbilityGetProbabilityFromLevel    );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.DATABASE__PINOCCHIO__ABILITY_GET_WOOD_ICON_FROM_SELECT,     Pinocchio_AbilityGetWoodIconFromSelect      );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.DATABASE__PINOCCHIO__RELIC_VAR_DATA_FROM_ID,                Pinocchio_RelicGetDataFromId                );
    }
    
    #endregion

    #region MARIONETTE

    public enum Marionette_Constellation_STARS
    {
        SMALL,
        BIG,

        TOTAL
    }

    [Header("MARIONETTE ==================================================")]

    [SerializeField] Character_Data                     Marionette_data;

    [SerializeField] List<Giggle_Character.Skill>       Marionette_skills;

    [SerializeField] List<Giggle_Item.Constellation>    Marionette_constellations;
    [SerializeField] List<Sprite>                       Marionette_constellationStars;

    [SerializeField] List<Giggle_Item.Card>             Marionette_cards;
    [SerializeField] List<Giggle_Item.CardSet>          Marionette_cardSets;

    [SerializeField] bool Marionette_isOpen;

    ////////// Getter & Setter          //////////
    object Marionette_GetIsOpen(params object[] _args)
    {
        return Marionette_isOpen;
    }

    object Marionette_GetDataFromId(params object[] _args)
    {
        Giggle_Character.Database res = Character_empty;

        //
        int id = (int)_args[0];

        // 찾고자 하는 캐릭터가 존재하는가?
        res = Marionette_data.Basic_GetDataFromId(id);
        if(res == null)
        {
            res = Character_empty;
        }

        if(res.Equals(Character_empty))
        {
            if(Marionette_isOpen)
            {
                IEnumerator coroutine = Marionette_LoadData__Coroutine();
                Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MASTER__BASIC__START_COROUTINE, coroutine);
            }
        }

        //
        return res;
    }

    object Marionette_GetDatasFromAttribute(params object[] _args)
    {
        List<Giggle_Character.Database> res = new List<Giggle_Character.Database>();

        //
        string[] attributes = ((string)_args[0]).Split('|');
        for(int for0 = 0; for0 < attributes.Length; for0++)
        {
            Giggle_Master.ATTRIBUTE attribute = (Giggle_Master.ATTRIBUTE)Enum.Parse(typeof(Giggle_Master.ATTRIBUTE), attributes[for0]);
            for(int for1 = 0; for1 < Marionette_data.Basic_VarCount; for1++)
            {
                Giggle_Character.Database data = Marionette_data.Basic_GetDataFromCount(for1);
                if(data.Basic_VarAttribute.Equals(attribute))
                {
                    res.Add(data);
                }
            }
        }

        //
        return res;
    }

    object Marionette_GetSkillFromId(params object[] _args)
    {
        Giggle_Character.Skill res = null;

        //
        int id = (int)_args[0];
        for(int for0 = 0; for0 < Marionette_skills.Count; for0++)
        {
            if(Marionette_skills[for0].Basic_VarId.Equals(id))
            {
                res = Marionette_skills[for0];
                break;
            }
        }

        //
        return res;
    }

    //
    object Marionette_GetConstellationFromId(params object[] _args)
    {
        Giggle_Item.Constellation res = null;

        //
        int id = (int)_args[0];
        for(int for0 = 0; for0 < Marionette_constellations.Count; for0++)
        {
            if(Marionette_constellations[for0].Basic_VarId.Equals(id))
            {
                res = Marionette_constellations[for0];
                break;
            }
        }

        //
        return res;
    }

    object Marionette_GetConstellationStarFromType(params object[] _args)
    {
        Sprite res = null;

        //
        Marionette_Constellation_STARS starType = (Marionette_Constellation_STARS)_args[0];
        res = Marionette_constellationStars[(int)starType];

        //
        return res;
    }

    //
    object Marionette_GetCardFromId(params object[] _args)
    {
        Giggle_Item.Card res = null;

        //
        int id = (int)_args[0];
        for(int for0 = 0; for0 < Marionette_cards.Count; for0++)
        {
            if(Marionette_cards[for0].Basic_VarId.Equals(id))
            {
                res = Marionette_cards[for0];
                break;
            }
        }

        //
        return res;
    }

    ////////// Method                   //////////

    IEnumerator Marionette_LoadData__Coroutine()
    {
        Marionette_isOpen = false;

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

                        Addressables.LoadAssetAsync<TextAsset>("MARIONETTE/CSV_LIST").Completed
                        += handle =>
                        {
                            List<Dictionary<string, string>> data = Basic_CSVLoad(handle.Result);
                            for(int for0 = 0; for0 < data.Count; for0++)
                            {
                                Marionette_data.Basic_SetData(data[for0]);
                            }

                            phase = 2;
                        };
                    }
                    break;
                // 캐릭터 레벨
                case 2:
                    {
                        phase = 3;

                        Addressables.LoadAssetAsync<TextAsset>("MARIONETTE/CSV_LV").Completed
                        += handle =>
                        {
                            List<Dictionary<string, string>> data = Basic_CSVLoad(handle.Result);
                            for(int for0 = 0; for0 < data.Count; for0++)
                            {
                                int id = int.Parse(data[for0]["cha_id"]);
                                Marionette_data.Basic_GetDataFromId(id).Basic_SetStatusList(data[for0]);
                            }

                            phase = 100;
                        };
                    }
                    break;
                // 캐릭터 모델링
                case 100:
                    {
                        phase = 101;

                        Addressables.LoadAssetAsync<GameObject>("MARIONETTE/MODEL").Completed
                        += handle =>
                        {
                            GameObject res = handle.Result;
                            for(int for0 = 0; for0 < res.transform.childCount; for0++)
                            {
                                Transform child = res.transform.GetChild(for0);
                                if(child.gameObject.activeSelf)
                                {
                                    int id = int.Parse(child.name);
                                    Marionette_data.Basic_GetDataFromId(id).Basic_VarUnit = child.GetComponent<Giggle_Unit>();
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

                        Addressables.LoadAssetAsync<TextAsset>("MARIONETTE/CSV_SKILL_LIST").Completed
                        += handle =>
                        {
                            List<Dictionary<string, string>> data = Basic_CSVLoad(handle.Result);
                            for(int for0 = 0; for0 < data.Count; for0++)
                            {
                                Marionette_skills.Add(new Giggle_Character.Skill(data[for0]));
                            }

                            phase = 202;
                        };
                    }
                    break;
                // 스킬 레벨
                case 202:
                    {
                        phase = 203;

                        Addressables.LoadAssetAsync<TextAsset>("MARIONETTE/CSV_SKILL_LV").Completed
                        += handle =>
                        {
                            List<Dictionary<string, string>> data = Basic_CSVLoad(handle.Result);
                            for(int for0 = 0; for0 < data.Count; for0++)
                            {
                                for(int for1 = 0; for1 < Marionette_skills.Count; for1++)
                                {
                                    if(Marionette_skills[for1].Basic_VarId.Equals(int.Parse(data[for0]["cha_skill_id"])))
                                    {
                                        Marionette_skills[for1].Basic_SetLvList(data[for0]);
                                    }
                                }
                            }

                            phase = 300;
                        };
                    }
                    break;
                // 별자리
                case 300:
                    {
                        phase = 301;

                        Addressables.LoadAssetAsync<TextAsset>("MARIONETTE/CONSTELLATION__CSV_LIST").Completed
                        += handle =>
                        {
                            List<Dictionary<string, string>> data = Basic_CSVLoad(handle.Result);
                            for(int for0 = 0; for0 < data.Count; for0++)
                            {
                                Marionette_constellations.Add(new Giggle_Item.Constellation(data[for0]));
                            }

                            phase = 302;
                        };
                    }
                    break;
                case 302:
                    {
                        phase = 303;

                        Addressables.LoadAssetAsync<TextAsset>("MARIONETTE/CONSTELLATION__CSV_VALUE").Completed
                        += handle =>
                        {
                            List<Dictionary<string, string>> data = Basic_CSVLoad(handle.Result);
                            for(int for0 = 0; for0 < data.Count; for0++)
                            {
                                for(int for1 = 0; for1 < Marionette_constellations.Count; for1++)
                                {
                                    if(Marionette_constellations[for1].Basic_SettingValue(data[for0]))
                                    {
                                        break;
                                    }
                                }
                            }

                            phase = 304;
                        };
                    }
                    break;
                case 304:
                    {
                        phase = 305;

                        Addressables.LoadAssetAsync<TextAsset>("MARIONETTE/CONSTELLATION__CSV_VALUE_LV").Completed
                        += handle =>
                        {
                            List<Dictionary<string, string>> data = Basic_CSVLoad(handle.Result);
                            for(int for0 = 0; for0 < data.Count; for0++)
                            {
                                for(int for1 = 0; for1 < Marionette_constellations.Count; for1++)
                                {
                                    if(Marionette_constellations[for1].Basic_SettingLv(data[for0]))
                                    {
                                        break;
                                    }
                                }
                            }

                            phase = 306;
                        };
                    }
                    break;
                case 306:
                    {
                        phase = 307;

                        Addressables.LoadAssetAsync<GameObject>("MARIONETTE/CONSTELLATION__BACKGROUND").Completed
                        += handle =>
                        {
                            if(Marionette_constellationStars == null)
                            {
                                Marionette_constellationStars = new List<Sprite>();
                            }
                            Marionette_constellationStars.Clear();
                            while(Marionette_constellationStars.Count < (int)Marionette_Constellation_STARS.TOTAL)
                            {
                                Marionette_constellationStars.Add(null);
                            }

                            //
                            Transform trans = handle.Result.transform;
                            for(int for0 = 0; for0 < trans.childCount; for0++)
                            {
                                Transform element = trans.GetChild(for0);
                                switch(element.name)
                                {
                                    case "Stars":
                                    {
                                        for(int for1 = 0; for1 < element.childCount; for1++)
                                        {
                                            Sprite starSprite = element.GetChild(for1).GetComponent<Image>().sprite;
                                            Marionette_Constellation_STARS starType = (Marionette_Constellation_STARS)Enum.Parse(typeof(Marionette_Constellation_STARS), starSprite.name.Split('_')[2]);

                                            Marionette_constellationStars[(int)starType] = starSprite;
                                        }
                                    }
                                    break;
                                    default:    { Marionette_constellations[int.Parse(element.name.Split('_')[0]) - 1].Basic_SettingBackground(element);    }   break;
                                }
                            }

                            phase = 400;
                        };
                    }
                    break;
                // 카드
                case 400:
                    {
                        phase = 401;

                        Addressables.LoadAssetAsync<TextAsset>("MARIONETTE/CARD__CSV_LIST").Completed
                        += handle =>
                        {
                            List<Dictionary<string, string>> data = Basic_CSVLoad(handle.Result);
                            for(int for0 = 0; for0 < data.Count; for0++)
                            {
                                Marionette_cards.Add(new Giggle_Item.Card(data[for0]));
                            }

                            phase = 402;
                        };
                    }
                    break;
                case 402:
                    {
                        phase = 403;

                        Addressables.LoadAssetAsync<TextAsset>("MARIONETTE/CARD__CSV_LV").Completed
                        += handle =>
                        {
                            List<Dictionary<string, string>> data = Basic_CSVLoad(handle.Result);
                            for(int for0 = 0; for0 < data.Count; for0++)
                            {
                                for(int for1 = 0; for1 < Marionette_cards.Count; for1++)
                                {
                                    if(Marionette_cards[for1].Basic_VarId.Equals(int.Parse(data[for0]["card_id"])))
                                    {
                                        Marionette_cards[for1].Basic_SettingLv(data[for0]);
                                        break;
                                    }
                                }
                            }

                            phase = 404;
                        };
                    }
                    break;
                case 404:
                    {
                        phase = 405;

                        Addressables.LoadAssetAsync<TextAsset>("MARIONETTE/CARD__CSV_SET").Completed
                        += handle =>
                        {
                            List<Dictionary<string, string>> data = Basic_CSVLoad(handle.Result);
                            for(int for0 = 0; for0 < data.Count; for0++)
                            {
                                Marionette_cardSets.Add(new Giggle_Item.CardSet(data[for0]));
                            }

                            phase = 402;
                        };
                        Marionette_isOpen = true;
                        phase = -1;
                    }
                    break;
            }

            yield return null;
        }
    }

    ////////// Constructor & Destroyer  //////////
    void Marionette_Init()
    {
        Marionette_isOpen = true;

        Marionette_data             = new Character_Data();
        Marionette_skills           = new List<Giggle_Character.Skill>();
        Marionette_constellations   = new List<Giggle_Item.Constellation>();
        Marionette_cards            = new List<Giggle_Item.Card>();
        Marionette_cardSets         = new List<Giggle_Item.CardSet>();

        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.DATABASE__MARIONETTE__GET_IS_OPEN,  Marionette_GetIsOpen );

        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.DATABASE__MARIONETTE__GET_DATA_FROM_ID,                 Marionette_GetDataFromId                );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.DATABASE__MARIONETTE__GET_DATAS_FROM_ATTRIBUTE,         Marionette_GetDatasFromAttribute        );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.DATABASE__MARIONETTE__GET_SKILL_FROM_ID,                Marionette_GetSkillFromId               );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.DATABASE__MARIONETTE__GET_CONSTELLATION_FROM_ID,        Marionette_GetConstellationFromId       );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.DATABASE__MARIONETTE__GET_CONSTELLATION_STAR_FROM_TYPE, Marionette_GetConstellationStarFromType );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.DATABASE__MARIONETTE__GET_CARD_FROM_ID,                 Marionette_GetCardFromId                );
    }

    #endregion

    #endregion

    #region STAGE

    [Header("STAGE ==================================================")]
    [SerializeField] List<Giggle_Stage.Group> Stage_datas;
    [SerializeField] List<GameObject> Stage_objs;
    [SerializeField] GameObject Stage_empty;

    [SerializeField] bool Stage_isOpen;

    ////////// Getter & Setter          //////////
    object Stage_GetIsOpen(params object[] _args)
    {
        return Stage_isOpen;
    }

    object Stage_GetStageFromId(params object[] _args)
    {
        int stage = (int)_args[0];

        //
        Giggle_Stage.Stage res = null;

        // 찾고자 하는 스테이지가 존재하는가?
        for(int for0 = 0; for0 < Stage_datas.Count; for0++)
        {
            if(Stage_datas[for0].Basic_VarId.Equals(stage / 100))
            {
                res = Stage_datas[for0].Basic_GetStage(stage);
                break;
            }
        }

        //
        return res;
    }

    object Stage_GetObjFromSave(params object[] _args)
    {
        GameObject res = Stage_empty;

        //
        int stage = (int)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.PLAYER__STAGE__VAR_SELECT);
        stage = (stage / 100) % 100;

        // 찾고자 하는 모델링이 존재하는가?
        for(int for0 = 0; for0 < Stage_objs.Count; for0++)
        {
            if(Stage_objs[for0].name.Equals("Stage_" + (stage / 10).ToString() + (stage % 10).ToString()))
            {
                res = Stage_objs[for0];
                break;
            }
        }

        if(res.Equals(Stage_empty))
        {
            if(Stage_isOpen)
            {
                IEnumerator coroutine = Stage_LoadData__Coroutine();
                Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MASTER__BASIC__START_COROUTINE, coroutine);
            }
        }

        //
        return res;
    }
    
    ////////// Method                   //////////

    IEnumerator Stage_LoadData__Coroutine()
    {
        Stage_isOpen = false;

        //
        int phase = 0;

        while (phase != -1)
        {
            switch(phase)
            {
                case 0:
                    {
                        phase = 1;

                        Addressables.LoadAssetAsync<TextAsset>("STAGE/CSV").Completed
                        += handle =>
                        {
                            List<Dictionary<string, string>> data = Basic_CSVLoad(handle.Result);
                            for(int for0 = 0; for0 < data.Count; for0++)
                            {
                                int groupId = int.Parse(data[for0]["actgroupid"]);

                                // 그룹 찾기
                                Giggle_Stage.Group group = null;
                                for(int for1 = 0; for1 < Stage_datas.Count; for1++)
                                {
                                    if(Stage_datas[for1].Basic_VarId.Equals(groupId))
                                    {
                                        group = Stage_datas[for1];
                                        break;
                                    }
                                }
                                if(group == null)
                                {
                                    group = new Giggle_Stage.Group(groupId);
                                    Stage_datas.Add(group);
                                }

                                // 스테이지 추가
                                group.Basic_AddStage(data[for0]);
                            }

                            phase = 100;
                        };
                    }
                    break;
                case 100:
                    {
                        phase = 101;

                        Addressables.LoadAssetAsync<TextAsset>("STAGE/CSV_MONSTER").Completed
                        += handle =>
                        {
                            List<Dictionary<string, string>> data = Basic_CSVLoad(handle.Result);
                            for(int for0 = 0; for0 < data.Count; for0++)
                            {
                                int id = int.Parse(data[for0]["stage_id"]);

                                for(int for1 = 0; for1 < Stage_datas.Count; for1++)
                                {
                                    if(Stage_datas[for1].Basic_VarId.Equals(id / 100))
                                    {
                                        Stage_datas[for1].Basic_GetStage(id).Basic_SettingFormation(data[for0]);
                                        break;
                                    }
                                }
                            }

                            phase = 200;
                        };
                    }
                    break;
                
                case 200:
                    {
                        phase = 201;

                        Addressables.LoadAssetAsync<GameObject>("STAGE/MODEL").Completed
                        += handle =>
                        {
                            GameObject res = handle.Result;
                            for(int for0 = 0; for0 < res.transform.childCount; for0++)
                            {
                                Transform child = res.transform.GetChild(for0);
                                if(child.gameObject.activeSelf)
                                {
                                    Stage_objs.Add(child.gameObject);
                                }
                            }

                            Stage_isOpen = true;
                            phase = -1;
                        };
                    }
                    break;
            }

            yield return null;
        }
    }

    ////////// Constructor & Destroyer  //////////
    void Stage_Init(GameObject _obj)
    {
        if(Stage_datas == null) { Stage_datas = new List<Giggle_Stage.Group>(); }
        if(Stage_objs == null)  { Stage_objs = new List<GameObject>();          }
        Stage_empty = _obj;

        Stage_isOpen = true;
        
        //
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.DATABASE__STAGE__GET_IS_OPEN,   Stage_GetIsOpen );

        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.DATABASE__STAGE__GET_STAGE_FROM_ID, Stage_GetStageFromId    );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.DATABASE__STAGE__GET_OBJ_FROM_SAVE, Stage_GetObjFromSave    );
    }

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
    [SerializeField] List<Sprite>       Item_sprites;
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

    //
    object Item_GetSpriteFromValue(params object[] _args)
    {
        Sprite res = null;

        //
        Giggle_Item.TYPE type = (Giggle_Item.TYPE)_args[0];
        int classValue = -1;
        if(_args.Length >= 2)
        {
            classValue = (int)_args[1];
        }

        //
        string str = type.ToString();
        if(classValue != -1)
        {
            str += "_" + classValue;
        }

        for(int for0 = 0; for0 < Item_sprites.Count; for0++)
        {
            if(Item_sprites[for0].name.Equals(str))
            {
                res = Item_sprites[for0];
                break;
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

                            phase = 100;
                        };
                    }
                    break;
                case 100:
                    {
                        phase = 101;

                        if(Item_sprites == null)
                        {
                            Item_sprites = new List<Sprite>();
                        }

                        if(Item_sprites.Count == 0)
                        {
                            Addressables.LoadAssetAsync<GameObject>("ITEM/ICON_LIST").Completed
                            += handle =>
                            {
                                Transform trans = handle.Result.transform;
                                for(int for0 = 0; for0 < trans.childCount; for0++)
                                {
                                    Item_sprites.Add(trans.GetChild(for0).GetComponent<Image>().sprite);
                                }

                                phase = -1;
                                Item_isOpen = true;
                            };
                        }
                        else
                        {
                            Item_isOpen = true;
                        }
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
        if(Item_datas == null)
        {
            Item_datas = new List<Item_Data>();
        }

        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.DATABASE__ITEM__GET_IS_OPEN,    Item_GetIsOpen  );

        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.DATABASE__ITEM__GET_DATA_FROM_ID,       Item_GetDataFromId      );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.DATABASE__ITEM__GET_SPRITE_FROM_VALUE,  Item_GetSpriteFromValue );
    }

    #endregion

    #region QUEST

    [Serializable]
    public class Quest_Data : IDisposable
    {
        [SerializeField] List<Giggle_Quest.Database> Basic_quests;

        ////////// Getter & Setter          //////////
        public List<Giggle_Quest.Database>  Basic_VarQuests { get { return Basic_quests;    }   }

        public Giggle_Quest.Database Basic_GetDataFromId(int _id)
        {
            Giggle_Quest.Database res = null;

            //
            for(int for0 = 0; for0 < Basic_quests.Count; for0++)
            {
                if(Basic_quests[for0].Basic_GetIdIsSame(_id))
                {
                    res = Basic_quests[for0];
                    break;
                }
            }

            //
            return res;
        }

        public Giggle_Quest.Database    Basic_GetDataFromCount(int _count)  { return Basic_quests[_count];  }

        public int                      Basic_VarCount                      { get{ return Basic_quests.Count;   }   }

        public void Basic_SetData(Dictionary<string, string> _data)
        {
            Giggle_Quest.Database element = new Giggle_Quest.Database(_data);
            Basic_quests.Add(element);
        }

        ////////// Method                   //////////

        ////////// Constructor & Destroyer  //////////
        public Quest_Data()
        {
            if(Basic_quests == null)
            {
                Basic_quests = new List<Giggle_Quest.Database>();
            }
        }

        public void Dispose()
        {

        }

    }

    [Header("QUEST ==================================================")]
    [SerializeField] List<Quest_Data>       Quest_datas;
    [SerializeField] bool                   Quest_isOpen;
    [SerializeField] Giggle_Quest.Database  Quest_empty;

    [SerializeField] List<Giggle_Quest.Text>    Quest_texts;

    ////////// Getter & Setter          //////////
    
    // Quest_datas
    object Quest_GetDatasFromType(params object[] _args)
    {
        List<Giggle_Quest.Database> res = null;

        //
        Giggle_Quest.TYPE questType = (Giggle_Quest.TYPE)_args[0];

        res = Quest_datas[(int)questType - 1].Basic_VarQuests;

        //
        return res;
    }

    object Quest_GetDataFromId(params object[] _args)
    {
        Giggle_Quest.Database res = Quest_empty;

        if(Quest_isOpen)
        {
            //
            int id = (int)_args[0];

            int category    = id / 10000 % 10;
            int count       = id % 10000;
            
            if(Quest_datas.Count > category)
            {
                if(Quest_datas[category - 1].Basic_GetDataFromCount(count).Basic_VarId.Equals(id))
                {
                    res = Quest_datas[category - 1].Basic_GetDataFromCount(count);
                }
                
                if(res.Equals(Quest_empty))
                {
                    for(int for0 = 0; for0 < Quest_datas[category - 1].Basic_VarCount; for0++)
                    {
                        if(Quest_datas[category - 1].Basic_GetDataFromCount(for0).Basic_VarId.Equals(id))
                        {
                            res = Quest_datas[category - 1].Basic_GetDataFromCount(for0);
                            break;
                        }
                    }
                }
            }

            //
            if(res.Equals(Quest_empty))
            {
                IEnumerator coroutine = Quest_LoadData__Coroutine();
                Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MASTER__BASIC__START_COROUTINE, coroutine);
            }
        }

        //
        return res;
    }

    // Quest_isOpen
    object Quest_GetIsOpen(params object[] _args)
    {
        return Quest_isOpen;
    }

    // Quest_texts
    object Quest_GetText(params object[] _args)
    {
        Giggle_Quest.Text res = null;

        //
        Giggle_Quest.completeCondition_TYPE questType = (Giggle_Quest.completeCondition_TYPE)_args[0];

        res = Quest_texts[(int)questType];

        //
        return res;
    }

    ////////// Method                   //////////

    IEnumerator Quest_LoadData__Coroutine()
    {
        Quest_isOpen = false;

        //
        int phase = 0;

        while (phase != -1)
        {
            switch(phase)
            {
                // 리스트
                case 0:
                    {
                        phase = 1;
            
                        Addressables.LoadAssetAsync<TextAsset>("QUEST/CSV_LIST").Completed
                        += handle =>
                        {
                            List<Dictionary<string, string>> data = Basic_CSVLoad(handle.Result);
                            for(int for0 = 0; for0 < data.Count; for0++)
                            {
                                int category = int.Parse(data[for0]["questCategory"]);
                                if(Quest_datas.Count < category)
                                {
                                    Quest_datas.Add(new Quest_Data());
                                }
                                Quest_datas[category - 1].Basic_SetData(data[for0]);
                            }
            
                            phase = 100;
                        };
                    }
                    break;
                case 100:
                    {
                        phase = 101;
            
                        Addressables.LoadAssetAsync<TextAsset>("QUEST/CSV_TEXT").Completed
                        += handle =>
                        {
                            List<Dictionary<string, string>> data = Basic_CSVLoad(handle.Result);
                            for(int for0 = 0; for0 < data.Count; for0++)
                            {
                                Giggle_Quest.completeCondition_TYPE questType = (Giggle_Quest.completeCondition_TYPE)Enum.Parse(typeof(Giggle_Quest.completeCondition_TYPE), data[for0]["questCompletionCondition"]);
                                
                                while(Quest_texts.Count <= (int)questType)
                                {
                                    Quest_texts.Add(null);
                                }

                                Giggle_Quest.Text element = new Giggle_Quest.Text(data[for0]);
                                Quest_texts[(int)questType] = element;
                            }
            
                            phase = -1;
                            Quest_isOpen = true;
                        };
                    }
                    break;
            }
            
            yield return null;
        }
    }

    ////////// Constructor & Destroyer  //////////
    void Quest_Init()
    {
        if(Quest_datas == null)
        {
            Quest_datas = new List<Quest_Data>();
        }

        Quest_isOpen = true;

        if(Quest_empty == null)
        {
            Quest_empty = new Giggle_Quest.Database();
        }

        if(Quest_texts == null)
        {
            Quest_texts = new List<Giggle_Quest.Text>();
        }

        //
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.DATABASE__QUEST__GET_DATAS_FROM_TYPE,   Quest_GetDatasFromType  );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.DATABASE__QUEST__GET_DATA_FROM_ID,      Quest_GetDataFromId     );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.DATABASE__QUEST__GET_IS_OPEN,           Quest_GetIsOpen         );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.DATABASE__QUEST__GET_TEXT,              Quest_GetText           );
    }

    #endregion
}
