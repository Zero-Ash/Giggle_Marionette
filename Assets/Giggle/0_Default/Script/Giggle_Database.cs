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
            Giggle_Character.Database element = new Giggle_Character.Database(_data);
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
    [SerializeField] List<Character_Data> Character_datas;
    [SerializeField] bool Character_isOpen;
    [SerializeField] Giggle_Character.Database Character_empty;

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
            Giggle_Character.ATTRIBUTE attribute = (Giggle_Character.ATTRIBUTE)Enum.Parse(typeof(Giggle_Character.ATTRIBUTE), attributes[for0]);
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

                        Giggle_Character.ATTRIBUTE attribute = (Giggle_Character.ATTRIBUTE)_attribute;
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

                        Giggle_Character.ATTRIBUTE attribute = (Giggle_Character.ATTRIBUTE)_attribute;
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

                        Giggle_Character.ATTRIBUTE attribute = (Giggle_Character.ATTRIBUTE)_attribute;
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

                        Giggle_Character.ATTRIBUTE attribute = (Giggle_Character.ATTRIBUTE)_attribute;
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

                        while(Character_datas.Count <= _attribute)
                        {
                            Character_datas.Add(new Character_Data());
                        }

                        Giggle_Character.ATTRIBUTE attribute = (Giggle_Character.ATTRIBUTE)_attribute;
                        Addressables.LoadAssetAsync<GameObject>("CHARACTER/" + attribute.ToString() + "_MODEL").Completed
                        += handle =>
                        {
                            GameObject res = handle.Result;
                            for(int for0 = 0; for0 < res.transform.childCount; for0++)
                            {
                                Transform child = res.transform.GetChild(for0);
                                int id = int.Parse(child.name);
                                Character_datas[_attribute].Basic_GetDataFromId(id).Basic_VarUnit = child.GetComponent<Giggle_Unit>();
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
    void Character_Init()
    {
        Character_isOpen = true;
        if(Character_empty == null)
        {
            Character_empty = new Giggle_Character.Database();
        }

        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.DATABASE__CHARACTER__GET_IS_OPEN,       Character_GetIsOpen     );

        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.DATABASE__CHARACTER__GET_DATA_FROM_ID,          Character_GetDataFromId         );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.DATABASE__CHARACTER__GET_DATAS_FROM_ATTRIBUTE,  Character_GetDatasFromAttribute );
    }

    #endregion
}
