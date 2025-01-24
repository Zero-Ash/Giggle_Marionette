using UnityEngine;

//
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class Giggle_MainManager__Marionette : MonoBehaviour
{
    #region BASIC

    [SerializeField] GameObject Basic_back;
    [SerializeField] GameObject Basic_ui;
    [SerializeField] RectTransform  Basic_safeArea;

    [SerializeField] Giggle_UI.MenuBar BasicArea1_menuBar;

    [Header("RUNNING")]
    [SerializeField] IEnumerator    Basic_coroutine;

    ////////// Getter & Setter  //////////
    
    ////////// Method           //////////
    public void Basic_BtnClick(GameObject _obj)
    {
        string[] names = _obj.name.Split('/');
        switch(names[2].Split('_')[0])
        {
            case "CLOSE":   { Basic_BtnClose();                                 }   break;
            case "MENU":    { BasicArea1_BtnSelectMenu(int.Parse(names[3]));    }   break;
            default:        { Basic_BtnClick__Default(names);                   }   break;
        }
    }

    void Basic_BtnClose()
    {
        Basic_back.SetActive(false);
        Basic_ui.SetActive(false);

        //
        Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MASTER__BATTLE__VAR_COROUTINE_PHASE, Giggle_Battle.Basic__COROUTINE_PHASE.RESET);
    }

    void BasicArea1_BtnSelectMenu(int _count)
    {
        BasicArea1_menuBar.Basic_SelectMenu(_count);
    }

    void Basic_BtnClick__Default(string[] _names)
    {
        switch(_names[2].Split('_')[0])
        {
            case "FORMATION":       { Formation_BtnClick(_names);       }   break;
            case "MARIONETTE":      { Marionette_BtnClick(_names);      }   break;
            case "CONSTELLATION":   { Constellation_BtnClick(_names);   }   break;
            case "CARD":            { Card_BtnClick(_names);            }   break;
            case "ITEM":            { Item_BtnClick(_names);            }   break;
        }
    }

    // Basic_Active
    public void Basic_Active()
    {
        Basic_back.SetActive(true);

        Basic_coroutine = Basic_Active__Coroutine();
        Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MASTER__BASIC__START_COROUTINE, Basic_coroutine);
    }

    IEnumerator Basic_Active__Coroutine()
    {
        int phase = 0;

        //while(phase >= 0)
        {
            switch(phase)
            {
                case 0:
                    {

                    }
                    break;
            }

            yield return null;
        }

        Formation_Active();
        Marionette_Active();
        Constellation_Active();
        Card_Active();

        BasicArea1_BtnSelectMenu(0);

        Basic_ui.SetActive(true);

        Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MASTER__BASIC__STOP_COROUTINE, Basic_coroutine);
    }

    ////////// Unity            //////////

    // Start
    public void Start()
    {
        Basic_back.SetActive(false);
        Basic_ui.SetActive(false);

        Basic_safeArea.sizeDelta        = (Vector2)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MASTER__UI__SAFE_AREA_VAR_SIZE_DELTA );
        Basic_safeArea.localPosition    = (Vector2)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MASTER__UI__SAFE_AREA_VAR_POSITION   );
        
        BasicArea1_menuBar.Basic_Init();
        
        Formation_Start();
        Marionette_Start();
        Constellation_Start();
        Card_Start();
        Item_Start();
    }

    #endregion

    #region FORMATION

    [Serializable]
    public class ScrollerView_MenuBar__Formation : Giggle_UI.MenuBar
    {
        [SerializeField] Giggle_MainManager__Marionette Basic_uiData;

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////

        protected override void Basic_SelectMenu__Setting(int _for0, int _count)
        {
        }

        ////////// Constructor & Destroyer  //////////
        public void Basic_Init(Giggle_MainManager__Marionette _uiData)
        {
            Basic_uiData = _uiData;
            Basic_Init();
            for(int for0 = 0; for0 < Basic_list.Count; for0++)
            {
                Basic_GetListBtn(for0).name = "Button/MARIONETTE/FORMATION__MENU_BAR/" + for0.ToString();
            }
        }
    }

    [Serializable]
    public class ScrollView_Formation : Giggle_UI.ScrollView
    {
        [SerializeField] Giggle_MainManager__Marionette Basic_uiData;

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////

        protected override void Basic_Init__SetName()
        {
            Basic_list[0].Find("Button").name = "Button/MARIONETTE/FORMATION__SCROLL_VIEW/0";
            Basic_list[1].Find("Button").name = "Button/MARIONETTE/FORMATION__SCROLL_VIEW/1";
        }

        protected override void Basic_AddList__SetName(Transform _element)
        {
            _element.Find("Button").name = "Button/MARIONETTE/FORMATION__SCROLL_VIEW/" + Basic_list.Count;
        }

        public override void Basic_ClickBtn(int _count)
        {
            // ui 그래픽 갱신
            for(int for0 = 0; for0 < Basic_list.Count; for0++)
            {
                Basic_list[for0].Find("Select").gameObject.SetActive(for0.Equals(_count));
            }
        }

        // Basic_SelectMenuBar
        public void Basic_SelectMenuBar(int _count)
        {
            Basic_SelectMenuBar__Check();

            Basic_ClickBtn(-1);
        }

        void Basic_SelectMenuBar__Check()
        {
            List<int> list = Basic_uiData.Formation_VarMarionetteList;

            //
            int finalCount = 0;
            int whileCount = 0;
            while(whileCount < Basic_list.Count)
            {
                if(whileCount < list.Count)
                {
                    Basic_list[whileCount].gameObject.SetActive(true);

                    // 기존 데이터 날리기
                    while(Basic_list[whileCount].Find("Obj").childCount > 0)
                    {
                        Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                            Giggle_ScriptBridge.EVENT.MASTER__GARBAGE__REMOVE,
                            //
                            Basic_list[whileCount].Find("Obj").GetChild(0));
                    }

                    //
                    Giggle_Character.Save data
                        = (Giggle_Character.Save)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                            Giggle_ScriptBridge.EVENT.PLAYER__MARIONETTE__GET_DATA_FROM_INVENTORYID,
                            list[whileCount]);

                    Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                        Giggle_ScriptBridge.EVENT.MASTER__UI__CHARACTER_INSTANTIATE,
                        //
                        data.Basic_VarDataId,
                        Basic_list[whileCount].Find("Obj"), -90.0f, 300.0f);
                    
                    finalCount = whileCount;
                }
                else
                {
                    Basic_list[whileCount].gameObject.SetActive(false);
                }

                //
                whileCount++;
            }

            Basic_content.sizeDelta
                = new Vector2(
                    0,
                    (Basic_list[finalCount].GetComponent<RectTransform>().sizeDelta.y * 0.5f) + Basic_list[finalCount].localPosition.y);

            Basic_CheckCover();
        }

        //
        public void Basic_CheckCover()
        {
            List<int>   formation       = (List<int>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.PLAYER__FORMATION__GET_SELECT_FORMATION);
            List<int>   characterList   = Basic_uiData.Formation_VarMarionetteList;

            int whileCount = 0;
            while(whileCount < characterList.Count)
            {
                //
                Basic_list[whileCount].Find("Cover").gameObject.SetActive(false);
                for(int for0 = 0; for0 < formation.Count; for0++)
                {
                    if( characterList[whileCount].Equals(formation[for0]))
                    {
                        Basic_list[whileCount].Find("Cover").gameObject.SetActive(true);
                        break;
                    }
                }

                //
                whileCount++;
            }
        }

        //
        public void Basic_Init(Giggle_MainManager__Marionette _uiData)
        {
            Basic_Init();
            Basic_uiData = _uiData;
        }

        ////////// Constructor & Destroyer  //////////

    }

    [Serializable]
    public class Formation_Formation : Giggle_UI.Formation
    {
        [Header("==================================================")]
        [Header("BASIC ==================================================")]
        [SerializeField] Giggle_MainManager__Marionette Basic_manager;

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////

        protected override void Basic_Reset__PVE()
        {
            base.Basic_Reset__PVE();
            Basic_manager.Formation_Reset();
        }


        ////////// Constructor & Destroyer  //////////
        

    }

    [Header("FORMATION ==========")]
    // Area2
    [SerializeField] Formation_Formation    Formation_formation;
    
    // Area3
    [SerializeField] ScrollerView_MenuBar__Formation    Formation_scrollViewMenuBar;
    [SerializeField] ScrollView_Formation               Formation_scrollView;

    [Header("RUNNING")]
    [SerializeField] List<int>  Formation_marionetteList;   // inventoryId

    [SerializeField] int    Formation_selectFormation;
    [SerializeField] int    Formation_selectMarionette;

    ////////// Getter & Setter          //////////
    public List<int> Formation_VarMarionetteList    { get { return Formation_marionetteList;    }   }

    public ScrollView_Formation Formation_VarScrollView { get { return Formation_scrollView;    }   }

    ////////// Method                   //////////
    void Formation_BtnClick(string[] _names)
    {
        switch(_names[2])
        {
            case "FORMATION__PRESET":       { Formation_BtnClick__Preset(       int.Parse(_names[3]));  }   break;
            case "FORMATION__TILE":         { Formation_BtnClick__Tile(         int.Parse(_names[3]));  }   break;
            case "FORMATION__MENU_BAR":     { Formation_BtnClick__MenuBar(      int.Parse(_names[3]));  }   break;
            case "FORMATION__SCROLL_VIEW":  { Formation_BtnClick__ScrollView(   int.Parse(_names[3]));  }   break;
        }
    }

    void Formation_BtnClick__Preset(int _count)
    {
        //Marionette_formationSelect = _count;

        Formation_Reset();
    }

    // Formation_BtnClick__Tile
    void Formation_BtnClick__Tile(int _count)
    {
        int selectMarionette = -1;
        if(Formation_selectMarionette != -1)    { selectMarionette = Formation_marionetteList[Formation_selectMarionette];  }

        Formation_formation.UI_BtnClick__Tile(_count, selectMarionette);
    }

    // 
    void Formation_BtnClick__MenuBar(int _count)
    {
        Formation_SelectTypeFormList(_count);

        Formation_scrollViewMenuBar.Basic_SelectMenu(_count);
        Formation_scrollView.Basic_SelectMenuBar(_count);
    }

    // Formation_BtnClick__ScrollView
    void Formation_BtnClick__ScrollView(int _count)
    {
        if(_count == -1)
        {
            Formation_BtnClick__ScrollView__Select(-1);
        }
        else
        {
            //
            switch(Formation_formation.UI_VarSelectFormation)
            {
                case -1:    { Formation_BtnClick__ScrollView__Select(_count);   }   break;
                default:    { Formation_BtnClick__ScrollView__Change(_count);   }   break;
            }
        }
    }

    void Formation_BtnClick__ScrollView__Select(int _count)
    {
        Formation_scrollView.Basic_ClickBtn(_count);

        Formation_selectMarionette = _count;
    }

    void Formation_BtnClick__ScrollView__Change(int _count)
    {
        Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
            Giggle_ScriptBridge.EVENT.PLAYER__FORMATION__FORMATION_SETTING,
            Formation_marionetteList[_count], Formation_selectFormation);

        // UI갱신
        Formation_Reset();
    }

    //
    public void Formation_SelectTypeFormList(int _count)
    {
        //
        List<Giggle_Character.Save> list = null;
        switch(_count)
        {
            case 0: { list = Formation_SelectTypeFromList__All();   }   break;
            case 1: {   }   break;
            case 2: {   }   break;
            case 3: {   }   break;
        }

        //
        Formation_marionetteList.Clear();
        for(int for0 = 0; for0 < list.Count; for0++)
        {
            Formation_marionetteList.Add(list[for0].Basic_VarInventoryId);
        }
    }

    List<Giggle_Character.Save> Formation_SelectTypeFromList__All()
    {
        List<Giggle_Character.Save> res
            = (List<Giggle_Character.Save>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.PLAYER__MARIONETTE__GET_LIST);

        //
        return res;
    }

    //
    void Formation_Active()
    {
        //
        Formation_Reset();
        Formation_formation.Basic_Reset();

        Formation_BtnClick__MenuBar(0);
    }

    //
    public void Formation_Reset()
    {
        Formation_marionetteList.Clear();

        List<Giggle_Character.Save> list
            = (List<Giggle_Character.Save>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.PLAYER__MARIONETTE__GET_LIST);

        for(int for0 = 0; for0 < list.Count; for0++)
        {
            Formation_marionetteList.Add(list[for0].Basic_VarInventoryId);
        }

        //
        Formation_BtnClick__ScrollView(-1);
        Formation_scrollView.Basic_CheckCover();
        //Formation_BtnClick__Tile(-1);

        //Formation_formation.Basic_Reset();
    }

    ////////// Constructor & Destroyer  //////////

    void Formation_Start()
    {
        // Area2
        Formation_formation.Basic_Init();

        // Area3
        Formation_scrollViewMenuBar.Basic_Init(this);

        Formation_scrollView.Basic_Init(this);

        // Running
        if(Formation_marionetteList == null)
        {
            Formation_marionetteList = new List<int>();
        }
    }

    #endregion

    #region MARIONETTE

    [Serializable]
    public class ScrollerView_MenuBar__Marionette : Giggle_UI.MenuBar
    {
        [SerializeField] Giggle_MainManager__Marionette Basic_uiData;

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////

        protected override void Basic_SelectMenu__Setting(int _for0, int _count)
        {
        }

        ////////// Constructor & Destroyer  //////////
        public void Basic_Init(Giggle_MainManager__Marionette _uiData)
        {
            Basic_uiData = _uiData;
            Basic_Init();
        }
    }

    [Serializable]
    public class ScrollView_Marionette : Giggle_UI.ScrollView
    {
        [SerializeField] Giggle_MainManager__Marionette Basic_uiData;

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////

        protected override void Basic_Init__SetName()
        {
            Basic_list[0].Find("Button").name = "Button/MARIONETTE/MARIONETTE__SCROLL_VIEW/0";
            Basic_list[1].Find("Button").name = "Button/MARIONETTE/MARIONETTE__SCROLL_VIEW/1";
        }

        protected override void Basic_AddList__SetName(Transform _element)
        {
            _element.Find("Button").name = "Button/MARIONETTE/MARIONETTE__SCROLL_VIEW/" + Basic_list.Count;
        }

        public override void Basic_ClickBtn(int _count)
        {
            // ui 그래픽 갱신
            for(int for0 = 0; for0 < Basic_list.Count; for0++)
            {
                Basic_list[for0].Find("Select").gameObject.SetActive(for0.Equals(_count));
            }
        }

        // Basic_SelectMenuBar
        public void Basic_SelectMenuBar(int _count)
        {
            Basic_SelectMenuBar__Check();

            Basic_ClickBtn(-1);
        }

        void Basic_SelectMenuBar__Check()
        {
            List<int> list = Basic_uiData.Formation_VarMarionetteList;

            //
            int finalCount = 0;
            int whileCount = 0;
            while(whileCount < Basic_list.Count)
            {
                if(whileCount < list.Count)
                {
                    Basic_list[whileCount].gameObject.SetActive(true);

                    // 기존 데이터 날리기
                    while(Basic_list[whileCount].Find("Obj").childCount > 0)
                    {
                        Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                            Giggle_ScriptBridge.EVENT.MASTER__GARBAGE__REMOVE,
                            //
                            Basic_list[whileCount].Find("Obj").GetChild(0));
                    }

                    //
                    Giggle_Character.Save data
                        = (Giggle_Character.Save)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                            Giggle_ScriptBridge.EVENT.PLAYER__MARIONETTE__GET_DATA_FROM_INVENTORYID,
                            list[whileCount]);

                    Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                        Giggle_ScriptBridge.EVENT.MASTER__UI__CHARACTER_INSTANTIATE,
                        //
                        data.Basic_VarDataId,
                        Basic_list[whileCount].Find("Obj"), -90.0f, 300.0f);
                    
                    finalCount = whileCount;
                }
                else
                {
                    Basic_list[whileCount].gameObject.SetActive(false);
                }

                //
                whileCount++;
            }

            Basic_content.sizeDelta
                = new Vector2(
                    0,
                    (Basic_list[finalCount].GetComponent<RectTransform>().sizeDelta.y * 0.5f) + Basic_list[finalCount].localPosition.y);

            Basic_CheckCover();
        }

        //
        public void Basic_CheckCover()
        {
            List<int>   formation       = (List<int>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.PLAYER__FORMATION__GET_SELECT_FORMATION);
            List<int>   characterList   = Basic_uiData.Formation_VarMarionetteList;

            int whileCount = 0;
            while(whileCount < characterList.Count)
            {
                Giggle_Character.Save data
                    = (Giggle_Character.Save)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                        Giggle_ScriptBridge.EVENT.PLAYER__MARIONETTE__GET_DATA_FROM_INVENTORYID,
                        characterList[whileCount]);
                
                //
                Basic_list[whileCount].Find("Cover").gameObject.SetActive(false);
                for(int for0 = 0; for0 < formation.Count; for0++)
                {
                    if( data.Equals(formation[for0]))
                    {
                        Basic_list[whileCount].Find("Cover").gameObject.SetActive(true);
                        break;
                    }
                }

                //
                whileCount++;
            }
        }

        //
        public void Basic_Init(Giggle_MainManager__Marionette _uiData)
        {
            Basic_Init();
            Basic_uiData = _uiData;
        }

        ////////// Constructor & Destroyer  //////////

    }

    [Header("MARIONETTE ==========")]
    [SerializeField] ScrollerView_MenuBar__Marionette   Marionette_scrollViewMenuBar;
    [SerializeField] ScrollView_Marionette              Marionette_scrollView;

    [SerializeField] GameObject Marionette_infoParent;
    [SerializeField] Transform  Mationette_infoBasic;
    [SerializeField] Transform  Mationette_infoSkill;

    [SerializeField] Transform  Mationette_infoStatusNow;
    [SerializeField] Transform  Mationette_infoStatusNext;

    [Header("RUNNING")]
    [SerializeField] List<int>  Marionette_marionetteList;   // inventoryId
    [SerializeField] int        Marionette_selectMarionette;

    ////////// Getter & Setter          //////////
    public ScrollView_Marionette    Marionette_VarScrollView    { get { return Marionette_scrollView;   }   }

    ////////// Method                   //////////
    void Marionette_BtnClick(string[] _names)
    {
        switch(_names[2])
        {
            case "MARIONETTE__MENU_BAR":        { Marionette_BtnClick__MenuBar(int.Parse(_names[3]));       }   break;
            case "MARIONETTE__SCROLL_VIEW":     { Marionette_BtnClick__ScrollView(int.Parse(_names[3]));    }   break;
            case "MARIONETTE__INFO_BACK":       { Marionette_BtnClick__InfoBack();                          }   break;
            case "MARIONETTE__INFO_LEVEL_UP":   { Marionette_BtnClick__InfoLevelUp();                       }   break;
        }
    }

    //
    void Marionette_BtnClick__MenuBar(int _count)
    {
        Marionette_SelectTypeFormList(_count);
        
        Marionette_scrollViewMenuBar.Basic_SelectMenu(_count);
        Marionette_VarScrollView.Basic_SelectMenuBar(_count);
    }

    //
    void Marionette_BtnClick__ScrollView(int _count)
    {
        Marionette_scrollView.Basic_ClickBtn(_count);

        //
        Marionette_selectMarionette = _count;

        //
        if(!Marionette_selectMarionette.Equals(-1))
        {
            Giggle_Character.Save data
                = (Giggle_Character.Save)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.PLAYER__MARIONETTE__GET_DATA_FROM_INVENTORYID,
                    Marionette_marionetteList[Marionette_selectMarionette]);
            Giggle_Character.Database database
                = (Giggle_Character.Database)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.DATABASE__MARIONETTE__GET_DATA_FROM_ID,
                    data.Basic_VarDataId);
            
            // Info Area2
            Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.MASTER__UI__CHARACTER_INSTANTIATE,
                //
                data.Basic_VarDataId,
                Mationette_infoBasic.Find("Portrait"), -90.0f, 300.0f);
            Mationette_infoBasic.Find("Name").GetComponent<TextMeshProUGUI>().text = database.Basic_VarName;
            //Mationette_infoBasic.Find("Type").GetComponent<TextMeshProUGUI>().text = database.ba;
            Mationette_infoBasic.Find("Attribute").GetComponent<TextMeshProUGUI>().text = database.Basic_VarAttribute.ToString();

            // Info Area3
            Marionette_BtnClick__ScrollView__Status(Mationette_infoStatusNow, database.Basic_GetStatusList(data.Basic_VarLevel));
            Marionette_BtnClick__ScrollView__Status(Mationette_infoStatusNext, database.Basic_GetStatusList(data.Basic_VarLevel + 1));

            //
            Marionette_scrollView.Basic_VarContent.gameObject.SetActive(false);
            Marionette_infoParent.SetActive(true);
        }
    }

    void Marionette_BtnClick__ScrollView__Status(Transform _trans, Giggle_Character.Status _status)
    {
        _trans.localPosition = Vector3.zero;

        _trans.Find("Attack"                        ).Find("Value").GetComponent<TextMeshProUGUI>().text = _status.Basic_VarAttack.ToString();
        _trans.Find("Defence"                       ).Find("Value").GetComponent<TextMeshProUGUI>().text = _status.Basic_VarDefence.ToString();
        _trans.Find("Hp"                            ).Find("Value").GetComponent<TextMeshProUGUI>().text = _status.Basic_VarHp.ToString();
        _trans.Find("AttackSpeed"                   ).Find("Value").GetComponent<TextMeshProUGUI>().text = _status.Basic_VarAttackSpeed.ToString();

        _trans.Find("CriticalChance"                ).Find("Value").GetComponent<TextMeshProUGUI>().text = _status.Basic_VarCriticalChance.ToString();
        _trans.Find("CriticalDamage"                ).Find("Value").GetComponent<TextMeshProUGUI>().text = _status.Basic_VarCriticalDamage.ToString();
        _trans.Find("CriticalDamageReduction"       ).Find("Value").GetComponent<TextMeshProUGUI>().text = _status.Basic_VarCriticalDamageReduction.ToString();

        _trans.Find("LuckyChance"                   ).Find("Value").GetComponent<TextMeshProUGUI>().text = _status.Basic_VarLuckyChance.ToString();
        _trans.Find("LuckyDamage"                   ).Find("Value").GetComponent<TextMeshProUGUI>().text = _status.Basic_VarLuckyDamage.ToString();
        _trans.Find("DamageTakenReduction"          ).Find("Value").GetComponent<TextMeshProUGUI>().text = _status.Basic_VarDamageTakenReduction.ToString();

        _trans.Find("HpRegenPerSecond"              ).Find("Value").GetComponent<TextMeshProUGUI>().text = _status.Basic_VarHpRegenPerSecond.ToString();
        _trans.Find("HpRegenAmount"                 ).Find("Value").GetComponent<TextMeshProUGUI>().text = _status.Basic_VarHpRegenAmount.ToString();
        _trans.Find("LifeSteal"                     ).Find("Value").GetComponent<TextMeshProUGUI>().text = _status.Basic_VarHpLifeSteal.ToString();

        _trans.Find("GoldGainIncrease"              ).Find("Value").GetComponent<TextMeshProUGUI>().text = _status.Basic_VarGoldGainIncrease.ToString();

        _trans.Find("DamageIncrease"                ).Find("Value").GetComponent<TextMeshProUGUI>().text = _status.Basic_VarDamageIncrease.ToString();
        _trans.Find("AllDamageIncrease"             ).Find("Value").GetComponent<TextMeshProUGUI>().text = _status.Basic_VarAllDamageIncrease.ToString();
        _trans.Find("NormalMonsterDamageIncrease"   ).Find("Value").GetComponent<TextMeshProUGUI>().text = _status.Basic_VarNormalMonsterDamageIncrease.ToString();
        _trans.Find("BossDamageIncrease"            ).Find("Value").GetComponent<TextMeshProUGUI>().text = _status.Basic_VarBossDamageIncrease.ToString();

        _trans.Find("CooldownReduction"             ).Find("Value").GetComponent<TextMeshProUGUI>().text = _status.Basic_VarSkillCooldownReduction.ToString();
        _trans.Find("SkillDamageIncrease"           ).Find("Value").GetComponent<TextMeshProUGUI>().text = _status.Basic_VarSkillDamageIncrease.ToString();
        _trans.Find("SkillDamageReduction"          ).Find("Value").GetComponent<TextMeshProUGUI>().text = _status.Basic_VarSkillDamageReduction.ToString();

        _trans.Find("Stun"                          ).Find("Value").GetComponent<TextMeshProUGUI>().text = _status.Basic_VarStun.ToString();
        _trans.Find("StunResistance"                ).Find("Value").GetComponent<TextMeshProUGUI>().text = _status.Basic_VarStunResistance.ToString();

        _trans.Find("MultiHit"                      ).Find("Value").GetComponent<TextMeshProUGUI>().text = _status.Basic_VarMultiHit.ToString();
        _trans.Find("MultiHitDamage"                ).Find("Value").GetComponent<TextMeshProUGUI>().text = _status.Basic_VarMultiHitDamage.ToString();
        _trans.Find("MultiHitDamageReduction"       ).Find("Value").GetComponent<TextMeshProUGUI>().text = _status.Basic_VarMultiHitDamageReduction.ToString();

        _trans.Find("CounterAttack"                 ).Find("Value").GetComponent<TextMeshProUGUI>().text = _status.Basic_VarCounterAttack.ToString();
        _trans.Find("CounterAttackDamage"           ).Find("Value").GetComponent<TextMeshProUGUI>().text = _status.Basic_VarCounterAttackDamage.ToString();
        _trans.Find("CounterAttackDamageReduction"  ).Find("Value").GetComponent<TextMeshProUGUI>().text = _status.Basic_VarCounterAttackDamageReduction.ToString();

        _trans.Find("SkillCritical"                 ).Find("Value").GetComponent<TextMeshProUGUI>().text = _status.Basic_VarSkillCritical.ToString();
        _trans.Find("SkillCriticalDamage"           ).Find("Value").GetComponent<TextMeshProUGUI>().text = _status.Basic_VarSkillCriticalDamage.ToString();

        _trans.Find("Evasion"                       ).Find("Value").GetComponent<TextMeshProUGUI>().text = _status.Basic_VarEvasion.ToString();
        _trans.Find("Accuracy"                      ).Find("Value").GetComponent<TextMeshProUGUI>().text = _status.Basic_VarAccuracy.ToString();
    }

    //
    void Marionette_BtnClick__InfoBack()
    {
        Marionette_BtnClick__ScrollView(-1);

        Marionette_scrollView.Basic_VarContent.gameObject.SetActive(true);
        Marionette_infoParent.SetActive(false);
    }

    //
    void Marionette_BtnClick__InfoLevelUp()
    {
        // TODO: 재화는 나중에 지불하기로
        Giggle_Character.Save data
            = (Giggle_Character.Save)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.PLAYER__MARIONETTE__GET_DATA_FROM_INVENTORYID,
                Marionette_marionetteList[Marionette_selectMarionette]);
        
        data.Basic_VarLevel += 1;
        
        // UI 갱신
        Marionette_BtnClick__ScrollView(Marionette_selectMarionette);
    }

    //
    void Marionette_Active()
    {
        //
        //Formation_Reset();

        Marionette_BtnClick__MenuBar(0);
    }

    //
    public void Marionette_SelectTypeFormList(int _count)
    {
        //
        List<Giggle_Character.Save> list = null;
        switch(_count)
        {
            case 0: { list = Marionette_SelectTypeFromList__All();   }   break;
            case 1: {   }   break;
            case 2: {   }   break;
            case 3: {   }   break;
        }

        //
        Marionette_marionetteList.Clear();
        for(int for0 = 0; for0 < list.Count; for0++)
        {
            Marionette_marionetteList.Add(list[for0].Basic_VarInventoryId);
        }
    }

    List<Giggle_Character.Save> Marionette_SelectTypeFromList__All()
    {
        List<Giggle_Character.Save> res
            = (List<Giggle_Character.Save>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.PLAYER__MARIONETTE__GET_LIST);

        //
        return res;
    }
    
    //
    void Marionette_Start()
    {
        Marionette_scrollViewMenuBar.Basic_Init(this);
        for(int for0 = 0; for0 < Marionette_scrollViewMenuBar.Basic_VarListCount; for0++)
        {
            Marionette_scrollViewMenuBar.Basic_GetListBtn(for0).name = "Button/MARIONETTE/MARIONETTE__MENU_BAR/" + for0.ToString();
        }

        Marionette_scrollView.Basic_Init(this);
        Marionette_scrollView.Basic_VarContent.gameObject.SetActive(true);

        Marionette_infoParent.SetActive(false);

        // Running
        if(Marionette_marionetteList == null)
        {
            Marionette_marionetteList = new List<int>();
        }
    }

    ////////// Constructor & Destroyer  //////////

    #endregion

    #region CONSTELLATION

    [Serializable]
    public class ScrollerView_MenuBar__Constellation : Giggle_UI.MenuBar
    {
        [SerializeField] Giggle_MainManager__Marionette Basic_uiData;

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////

        protected override void Basic_SelectMenu__Setting(int _for0, int _count)
        {
            if(_for0.Equals(_count))
            {
                Basic_uiData.Constellation_PopUpList_VarScrollView.Basic_SelectMenuBar(_count);
            }
        }

        ////////// Constructor & Destroyer  //////////
        public void Basic_Init(Giggle_MainManager__Marionette _uiData)
        {
            Basic_uiData = _uiData;
            Basic_Init();
        }
    }

    [Serializable]
    public class ScrollView_Constellation : Giggle_UI.ScrollView
    {
        [SerializeField] Giggle_MainManager__Marionette Basic_uiData;

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////

        protected override void Basic_Init__SetName()
        {
            Basic_list[0].Find("Button").name = "Button/MARIONETTE/CONSTELLATION__POP_UP_LIST__SCROLL_VIEW/0";
            Basic_list[1].Find("Button").name = "Button/MARIONETTE/CONSTELLATION__POP_UP_LIST__SCROLL_VIEW/1";
        }

        protected override void Basic_AddList__SetName(Transform _element)
        {
            _element.Find("Button").name = "Button/MARIONETTE/CONSTELLATION__POP_UP_LIST__SCROLL_VIEW/" + Basic_list.Count;
        }

        public override void Basic_ClickBtn(int _count)
        {
            // ui 그래픽 갱신
            for(int for0 = 0; for0 < Basic_list.Count; for0++)
            {
                Basic_list[for0].Find("Select").gameObject.SetActive(for0.Equals(_count));
            }
        }

        // Basic_SelectMenuBar
        public void Basic_SelectMenuBar(int _count)
        {
            Basic_uiData.Constellation_SelectTypeFormList(_count);

            Basic_SelectMenuBar__Check();

            Basic_ClickBtn(-1);
        }

        void Basic_SelectMenuBar__Check()
        {
            List<int> list = Basic_uiData.Formation_VarMarionetteList;

            //
            int finalCount = 0;
            int whileCount = 0;
            while(whileCount < Basic_list.Count)
            {
                if(whileCount < list.Count)
                {
                    Basic_list[whileCount].gameObject.SetActive(true);

                    // 기존 데이터 날리기
                    while(Basic_list[whileCount].Find("Obj").childCount > 0)
                    {
                        Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                            Giggle_ScriptBridge.EVENT.MASTER__GARBAGE__REMOVE,
                            //
                            Basic_list[whileCount].Find("Obj").GetChild(0));
                    }

                    //
                    Giggle_Character.Save data
                        = (Giggle_Character.Save)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                            Giggle_ScriptBridge.EVENT.PLAYER__MARIONETTE__GET_DATA_FROM_INVENTORYID,
                            list[whileCount]);

                    Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                        Giggle_ScriptBridge.EVENT.MASTER__UI__CHARACTER_INSTANTIATE,
                        //
                        data.Basic_VarDataId,
                        Basic_list[whileCount].Find("Obj"), -90.0f, 300.0f);
                    
                    finalCount = whileCount;
                }
                else
                {
                    Basic_list[whileCount].gameObject.SetActive(false);
                }

                //
                whileCount++;
            }

            Basic_content.sizeDelta
                = new Vector2(
                    0,
                    (Basic_list[finalCount].GetComponent<RectTransform>().sizeDelta.y * 0.5f) + Basic_list[finalCount].localPosition.y);

            Basic_CheckCover();
        }

        //
        public void Basic_CheckCover()
        {
            List<int>   formation       = (List<int>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.PLAYER__FORMATION__GET_SELECT_FORMATION);
            List<int>   characterList   = Basic_uiData.Formation_VarMarionetteList;

            int whileCount = 0;
            while(whileCount < characterList.Count)
            {
                Giggle_Character.Save data
                    = (Giggle_Character.Save)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                        Giggle_ScriptBridge.EVENT.PLAYER__MARIONETTE__GET_DATA_FROM_INVENTORYID,
                        characterList[whileCount]);
                
                //
                Basic_list[whileCount].Find("Cover").gameObject.SetActive(false);
                for(int for0 = 0; for0 < formation.Count; for0++)
                {
                    if( data.Equals(formation[for0]))
                    {
                        Basic_list[whileCount].Find("Cover").gameObject.SetActive(true);
                        break;
                    }
                }

                //
                whileCount++;
            }
        }

        //
        public void Basic_Init(Giggle_MainManager__Marionette _uiData)
        {
            Basic_Init();
            Basic_uiData = _uiData;
        }

        ////////// Constructor & Destroyer  //////////

    }

    [Header("CONSTELLATION ==========")]

    // marionette
    [SerializeField] TextMeshProUGUI    Constellation_marionetteName;

    // constellation
    [SerializeField] Transform  Constellation_constellationBackParent;
    [SerializeField] List<Transform>  Constellation_constellationStars;

    // popUp
    [SerializeField] GameObject Contellation_popUp;

    [SerializeField] ScrollerView_MenuBar__Constellation    Contellation__popUpList_scrollViewMenuBar;
    [SerializeField] ScrollView_Constellation               Contellation__popUpList_scrollView;

    [SerializeField] TextMeshProUGUI    Contellation__popUpStar_infoName;
    [SerializeField] TextMeshProUGUI    Contellation__popUpStar_infoLevel;

    [Header("RUNNING")]
    [SerializeField] List<int>  Contellation_marionetteList;    // inventoryId
    [SerializeField] int        Contellation_selectMarionette;
    [SerializeField] int        Contellation_selectStar;

    ////////// Getter & Setter          //////////
    
    public ScrollView_Constellation Constellation_PopUpList_VarScrollView   { get { return Contellation__popUpList_scrollView;  }   }
    
    ////////// Method                   //////////
    void Constellation_BtnClick(string[] _names)
    {
        switch(_names[2])
        {
            case "CONSTELLATION__LIST":                     { Constellation_BtnClick__PopUpOn(_names[2]);                           }   break;
            case "CONSTELLATION__STAR":                     { Constellation_BtnClick__Star(int.Parse(_names[3]));                   }   break;
            case "CONSTELLATION__POP_UP_LIST__MENU_BAR":    { Constellation_BtnClick__PopUpList_MenuBar(int.Parse(_names[3]));      }   break;
            case "CONSTELLATION__POP_UP_LIST__SCROLL_VIEW": { Constellation_BtnClick__PopUpList_ScrollView(int.Parse(_names[3]));   }   break;
            case "CONSTELLATION__POP_UP_STAR__CLOSE":       { Constellation_BtnClick__PopUp_Close();                                }   break;
        }
    }

    //
    void Constellation_BtnClick__Star(int _count)
    {
        Contellation_selectStar = _count;

        Giggle_Character.Save data
            = (Giggle_Character.Save)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.PLAYER__MARIONETTE__GET_DATA_FROM_INVENTORYID,
                Contellation_marionetteList[Contellation_selectMarionette]);
        Giggle_Character.Database database
            = (Giggle_Character.Database)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.DATABASE__MARIONETTE__GET_DATA_FROM_ID,
                data.Basic_VarDataId);
        Giggle_Item.Constellation constellationData
            = (Giggle_Item.Constellation)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.DATABASE__MARIONETTE__GET_CONSTELLATION_FROM_ID,
                database.Basic_VarConstellation);

        Contellation__popUpStar_infoName.text = constellationData.Basic_GetValueData(Contellation_selectStar).Basic_VarName;
        Contellation__popUpStar_infoLevel.text = "Lv. " + data.Basic_GetConstellationLv(Contellation_selectStar);

        Constellation_BtnClick__PopUpOn("CONSTELLATION__STAR");
    }

    //
    void Constellation_BtnClick__PopUpOn(string _name)
    {
        Contellation_popUp.SetActive(true);
        //
        for(int for0 = 0; for0 < Contellation_popUp.transform.childCount; for0++)
        {
            Contellation_popUp.transform.GetChild(for0).gameObject.SetActive(false);
        }
        Contellation_popUp.transform.Find(_name).gameObject.SetActive(true);
    }

    // Constellation_BtnClick__PopUpList
    void Constellation_BtnClick__PopUpList_MenuBar(int _count)
    {
        Contellation__popUpList_scrollViewMenuBar.Basic_SelectMenu(_count);
        Constellation_SelectTypeFormList(0);
    }

    //
    void Constellation_BtnClick__PopUpList_ScrollView(int _count)
    {
        //
        Contellation_selectMarionette = _count;

        // 초기화 ==========
        // 별자리 배경
        while(Constellation_constellationBackParent.childCount > 0)
        {
            Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.MASTER__GARBAGE__REMOVE,
                Constellation_constellationBackParent.GetChild(0));
        }

        //
        for(int for0 = 0; for0 < Constellation_constellationStars.Count; for0++)
        {
            Constellation_constellationStars[for0].gameObject.SetActive(false);
        }

        // 셋팅 시작 ==========
        if(!Contellation_selectMarionette.Equals(-1))
        {
            Giggle_Character.Save data
                = (Giggle_Character.Save)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.PLAYER__MARIONETTE__GET_DATA_FROM_INVENTORYID,
                    Contellation_marionetteList[Contellation_selectMarionette]);

            //
            Giggle_Character.Database database
                = (Giggle_Character.Database)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.DATABASE__MARIONETTE__GET_DATA_FROM_ID,
                    data.Basic_VarDataId);

            Constellation_marionetteName.text = database.Basic_VarName;

            // 별자리 셋팅 ========== ==========
            Giggle_Item.Constellation constellationData
                = (Giggle_Item.Constellation)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.DATABASE__MARIONETTE__GET_CONSTELLATION_FROM_ID,
                    database.Basic_VarConstellation);
            
            // 별자리 배경 불러오기
            Transform background = Instantiate(constellationData.Basic_VarBackground, Constellation_constellationBackParent);
            background.localPosition = Vector3.zero;
            background.localScale = Vector3.one;
            background.localRotation = Quaternion.Euler(Vector3.zero);

            for(int for0 = 0; for0 < constellationData.Basic_VarValueCount; for0++)
            {
                // TODO:차후에 별크기는 수정해주세요.
                Constellation_constellationStars[for0].GetComponent<Image>().sprite
                    = (Sprite)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                        Giggle_ScriptBridge.EVENT.DATABASE__MARIONETTE__GET_CONSTELLATION_STAR_FROM_TYPE,
                        Giggle_Database.Marionette_Constellation_STARS.SMALL);
                Constellation_constellationStars[for0].Find("Name").GetComponent<TextMeshProUGUI>().text = constellationData.Basic_GetValueData(for0).Basic_VarName;
                Constellation_constellationStars[for0].position = background.GetChild(for0).position;
                Constellation_constellationStars[for0].gameObject.SetActive(true);
            }

            //
            Contellation_popUp.SetActive(false);
        }
    }

    //
    void Constellation_BtnClick__PopUp_Close()
    {
        Contellation_popUp.SetActive(false);
    }

    //
    void Constellation_Active()
    {
        Constellation_BtnClick__PopUpList_MenuBar(0);
    }

    //
    public void Constellation_SelectTypeFormList(int _count)
    {
        //
        List<Giggle_Character.Save> list = null;
        switch(_count)
        {
            case 0: { list = Constellation_SelectTypeFromList__All();   }   break;
            case 1: {   }   break;
            case 2: {   }   break;
            case 3: {   }   break;
        }

        //
        Contellation_marionetteList.Clear();
        for(int for0 = 0; for0 < list.Count; for0++)
        {
            Contellation_marionetteList.Add(list[for0].Basic_VarInventoryId);
        }
    }

    List<Giggle_Character.Save> Constellation_SelectTypeFromList__All()
    {
        List<Giggle_Character.Save> res
            = (List<Giggle_Character.Save>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.PLAYER__MARIONETTE__GET_LIST);

        //
        return res;
    }

    ////////// Constructor & Destroyer  //////////
    
    //
    void Constellation_Start()
    {
        // constellation
        Transform startsParent = Constellation_constellationStars[0].parent;
        for(int for0 = 1; for0 < startsParent.childCount; for0++)
        {
            Constellation_constellationStars.Add(startsParent.GetChild(for0));
        }

        // popUp
        Contellation__popUpList_scrollViewMenuBar.Basic_Init(this);
        for(int for0 = 0; for0 < Contellation__popUpList_scrollViewMenuBar.Basic_VarListCount; for0++)
        {
            Contellation__popUpList_scrollViewMenuBar.Basic_GetListBtn(for0).name = "Button/MARIONETTE/CONSTELLATION__POP_UP_LIST__MENU_BAR/" + for0.ToString();
        }

        Contellation__popUpList_scrollView.Basic_Init(this);
        Contellation__popUpList_scrollView.Basic_VarContent.gameObject.SetActive(true);

        Contellation_popUp.SetActive(false);
    }

    #endregion

    #region CARD

    [Serializable]
    public class ScrollView__Card_Pop_Up_Card_List : Giggle_UI.ScrollView
    {
        [SerializeField] Giggle_MainManager__Marionette Basic_uiData;

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////

        protected override void Basic_Init__SetName()
        {
            Basic_list[0].Find("Button").name = "Button/MARIONETTE/CARD__POP_UP_CARD_LIST__SCROLL_VIEW/0";
            Basic_list[1].Find("Button").name = "Button/MARIONETTE/CARD__POP_UP_CARD_LIST__SCROLL_VIEW/1";
        }

        protected override void Basic_AddList__SetName(Transform _element)
        {
            _element.Find("Button").name = "Button/MARIONETTE/CARD__POP_UP_CARD_LIST__SCROLL_VIEW/" + Basic_list.Count;
        }

        public override void Basic_ClickBtn(int _count)
        {
            // ui 그래픽 갱신
            for(int for0 = 0; for0 < Basic_list.Count; for0++)
            {
                Basic_list[for0].Find("Select").gameObject.SetActive(for0.Equals(_count));
            }
        }

        // Basic_SelectMenuBar
        public void Basic_SelectMenuBar(int _count)
        {
            Basic_uiData.Card_SettingCardList();

            Basic_SelectMenuBar__Check();

            Basic_ClickBtn(-1);
        }

        void Basic_SelectMenuBar__Check()
        {
            List<int> list = Basic_uiData.Card_VarCardList;

            //
            int finalCount = 0;
            int whileCount = 0;
            while(whileCount < Basic_list.Count)
            {
                if(whileCount < list.Count)
                {
                    Basic_list[whileCount].gameObject.SetActive(true);

                    //
                    Giggle_Item.Inventory data
                        = (Giggle_Item.Inventory)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                            Giggle_ScriptBridge.EVENT.PLAYER__CARD__GET_DATA_FROM_DATA_ID,
                            list[whileCount]);
                    Giggle_Item.Card database
                        = (Giggle_Item.Card)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                            Giggle_ScriptBridge.EVENT.DATABASE__MARIONETTE__GET_CARD_FROM_ID,
                            list[whileCount]);
                    //Basic_list[whileCount].Find("Portrait").GetComponent<Image>().sprite = database.;
                    Basic_list[whileCount].Find("Name").GetComponent<TextMeshProUGUI>().text = database.Basic_VarName;
                    
                    finalCount = whileCount;
                }
                else
                {
                    Basic_list[whileCount].gameObject.SetActive(false);
                }

                //
                whileCount++;
            }

            Basic_content.sizeDelta
                = new Vector2(
                    0,
                    (Basic_list[finalCount].GetComponent<RectTransform>().sizeDelta.y * 0.5f) + Basic_list[finalCount].localPosition.y);

            Basic_CheckCover();
        }

        //
        public void Basic_CheckCover()
        {
            List<int>   formation       = (List<int>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.PLAYER__FORMATION__GET_SELECT_FORMATION);
            List<int>   characterList   = Basic_uiData.Formation_VarMarionetteList;

            int whileCount = 0;
            while(whileCount < characterList.Count)
            {
                Giggle_Character.Save data
                    = (Giggle_Character.Save)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                        Giggle_ScriptBridge.EVENT.PLAYER__MARIONETTE__GET_DATA_FROM_INVENTORYID,
                        characterList[whileCount]);
                
                //
                Basic_list[whileCount].Find("Cover").gameObject.SetActive(false);
                for(int for0 = 0; for0 < formation.Count; for0++)
                {
                    if( data.Equals(formation[for0]))
                    {
                        Basic_list[whileCount].Find("Cover").gameObject.SetActive(true);
                        break;
                    }
                }

                //
                whileCount++;
            }
        }

        //
        public void Basic_Init(Giggle_MainManager__Marionette _uiData)
        {
            Basic_Init();
            Basic_uiData = _uiData;
        }

        ////////// Constructor & Destroyer  //////////

    }

    [Serializable]
    public class ScrollView_Card_Card_List : Giggle_UI.ScrollView
    {
        [SerializeField] Giggle_MainManager__Marionette Basic_uiData;

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////

        protected override void Basic_Init__SetName()
        {
            Basic_list[0].Find("Button").name = "Button/MARIONETTE/CARD__CARD_LIST__SCROLL_VIEW/0";
            Basic_list[1].Find("Button").name = "Button/MARIONETTE/CARD__CARD_LIST__SCROLL_VIEW/1";
        }

        protected override void Basic_AddList__SetName(Transform _element)
        {
            _element.Find("Button").name = "Button/MARIONETTE/CARD__CARD_LIST__SCROLL_VIEW/" + Basic_list.Count;
        }

        public override void Basic_ClickBtn(int _count)
        {
            // ui 그래픽 갱신
            for(int for0 = 0; for0 < Basic_list.Count; for0++)
            {
                Basic_list[for0].Find("Select").gameObject.SetActive(for0.Equals(_count));
            }
        }

        // Basic_SelectMenuBar
        public void Basic_SelectMenuBar(int _count)
        {
            //Basic_uiData.Card_SelectTypeFormList(_count);

            Basic_SelectMenuBar__Check();

            Basic_ClickBtn(-1);
        }

        void Basic_SelectMenuBar__Check()
        {
            List<int> list = Basic_uiData.Formation_VarMarionetteList;

            //
            int finalCount = 0;
            int whileCount = 0;
            while(whileCount < Basic_list.Count)
            {
                if(whileCount < list.Count)
                {
                    Basic_list[whileCount].gameObject.SetActive(true);

                    // 기존 데이터 날리기
                    while(Basic_list[whileCount].Find("Obj").childCount > 0)
                    {
                        Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                            Giggle_ScriptBridge.EVENT.MASTER__GARBAGE__REMOVE,
                            //
                            Basic_list[whileCount].Find("Obj").GetChild(0));
                    }

                    //
                    Giggle_Character.Save data
                        = (Giggle_Character.Save)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                            Giggle_ScriptBridge.EVENT.PLAYER__MARIONETTE__GET_DATA_FROM_INVENTORYID,
                            list[whileCount]);

                    Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                        Giggle_ScriptBridge.EVENT.MASTER__UI__CHARACTER_INSTANTIATE,
                        //
                        data.Basic_VarDataId,
                        Basic_list[whileCount].Find("Obj"), -90.0f, 300.0f);
                    
                    finalCount = whileCount;
                }
                else
                {
                    Basic_list[whileCount].gameObject.SetActive(false);
                }

                //
                whileCount++;
            }

            Basic_content.sizeDelta
                = new Vector2(
                    0,
                    (Basic_list[finalCount].GetComponent<RectTransform>().sizeDelta.y * 0.5f) + Basic_list[finalCount].localPosition.y);

            Basic_CheckCover();
        }

        //
        public void Basic_CheckCover()
        {
            List<int>   formation       = (List<int>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.PLAYER__FORMATION__GET_SELECT_FORMATION);
            List<int>   characterList   = Basic_uiData.Formation_VarMarionetteList;

            int whileCount = 0;
            while(whileCount < characterList.Count)
            {
                Giggle_Character.Save data
                    = (Giggle_Character.Save)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                        Giggle_ScriptBridge.EVENT.PLAYER__MARIONETTE__GET_DATA_FROM_INVENTORYID,
                        characterList[whileCount]);
                
                //
                Basic_list[whileCount].Find("Cover").gameObject.SetActive(false);
                for(int for0 = 0; for0 < formation.Count; for0++)
                {
                    if( data.Equals(formation[for0]))
                    {
                        Basic_list[whileCount].Find("Cover").gameObject.SetActive(true);
                        break;
                    }
                }

                //
                whileCount++;
            }
        }

        //
        public void Basic_Init(Giggle_MainManager__Marionette _uiData)
        {
            Basic_Init();
            Basic_uiData = _uiData;
        }

        ////////// Constructor & Destroyer  //////////

    }

    [Serializable]
    public class ScrollerView_MenuBar__Card_Pop_Up_Marionette_List : Giggle_UI.MenuBar
    {
        [SerializeField] Giggle_MainManager__Marionette Basic_uiData;

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////

        protected override void Basic_SelectMenu__Setting(int _for0, int _count)
        {
            if(_for0.Equals(_count))
            {
                Basic_uiData.Card_PopUpMarionetteList_VarScrollView.Basic_SelectMenuBar(_count);
            }
        }

        ////////// Constructor & Destroyer  //////////
        public void Basic_Init(Giggle_MainManager__Marionette _uiData)
        {
            Basic_uiData = _uiData;
            Basic_Init();
        }
    }

    [Serializable]
    public class ScrollView__Card_Pop_Up_Marionette_List : Giggle_UI.ScrollView
    {
        [SerializeField] Giggle_MainManager__Marionette Basic_uiData;

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////

        protected override void Basic_Init__SetName()
        {
            Basic_list[0].Find("Button").name = "Button/MARIONETTE/CARD__POP_UP_MARIONETTE_LIST__SCROLL_VIEW/0";
            Basic_list[1].Find("Button").name = "Button/MARIONETTE/CARD__POP_UP_MARIONETTE_LIST__SCROLL_VIEW/1";
        }

        protected override void Basic_AddList__SetName(Transform _element)
        {
            _element.Find("Button").name = "Button/MARIONETTE/CARD__POP_UP_MARIONETTE_LIST__SCROLL_VIEW/" + Basic_list.Count;
        }

        public override void Basic_ClickBtn(int _count)
        {
            // ui 그래픽 갱신
            for(int for0 = 0; for0 < Basic_list.Count; for0++)
            {
                Basic_list[for0].Find("Select").gameObject.SetActive(for0.Equals(_count));
            }
        }

        // Basic_SelectMenuBar
        public void Basic_SelectMenuBar(int _count)
        {
            Basic_uiData.Card_SelectTypeFormList(_count);

            Basic_SelectMenuBar__Check();

            Basic_ClickBtn(-1);
        }

        void Basic_SelectMenuBar__Check()
        {
            List<int> list = Basic_uiData.Card_VarMarionetteList;

            //
            int finalCount = 0;
            int whileCount = 0;
            while(whileCount < Basic_list.Count)
            {
                if(whileCount < list.Count)
                {
                    Basic_list[whileCount].gameObject.SetActive(true);

                    // 기존 데이터 날리기
                    while(Basic_list[whileCount].Find("Obj").childCount > 0)
                    {
                        Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                            Giggle_ScriptBridge.EVENT.MASTER__GARBAGE__REMOVE,
                            //
                            Basic_list[whileCount].Find("Obj").GetChild(0));
                    }

                    //
                    Giggle_Character.Save data
                        = (Giggle_Character.Save)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                            Giggle_ScriptBridge.EVENT.PLAYER__MARIONETTE__GET_DATA_FROM_INVENTORYID,
                            list[whileCount]);

                    Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                        Giggle_ScriptBridge.EVENT.MASTER__UI__CHARACTER_INSTANTIATE,
                        //
                        data.Basic_VarDataId,
                        Basic_list[whileCount].Find("Obj"), -90.0f, 300.0f);
                    
                    finalCount = whileCount;
                }
                else
                {
                    Basic_list[whileCount].gameObject.SetActive(false);
                }

                //
                whileCount++;
            }

            Basic_content.sizeDelta
                = new Vector2(
                    0,
                    (Basic_list[finalCount].GetComponent<RectTransform>().sizeDelta.y * 0.5f) + Basic_list[finalCount].localPosition.y);

            Basic_CheckCover();
        }

        //
        public void Basic_CheckCover()
        {
            List<int>   formation       = (List<int>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.PLAYER__FORMATION__GET_SELECT_FORMATION);
            List<int>   characterList   = Basic_uiData.Formation_VarMarionetteList;

            int whileCount = 0;
            while(whileCount < characterList.Count)
            {
                Giggle_Character.Save data
                    = (Giggle_Character.Save)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                        Giggle_ScriptBridge.EVENT.PLAYER__MARIONETTE__GET_DATA_FROM_INVENTORYID,
                        characterList[whileCount]);
                
                //
                Basic_list[whileCount].Find("Cover").gameObject.SetActive(false);
                for(int for0 = 0; for0 < formation.Count; for0++)
                {
                    if( data.Equals(formation[for0]))
                    {
                        Basic_list[whileCount].Find("Cover").gameObject.SetActive(true);
                        break;
                    }
                }

                //
                whileCount++;
            }
        }

        //
        public void Basic_Init(Giggle_MainManager__Marionette _uiData)
        {
            Basic_Init();
            Basic_uiData = _uiData;
        }

        ////////// Constructor & Destroyer  //////////

    }

    [Header("CARD ==========")]

    // marionette
    [SerializeField] TextMeshProUGUI    Card_marionetteName;

    // popUp
    [SerializeField] GameObject Card_popUp;

    [SerializeField] ScrollView__Card_Pop_Up_Card_List  Card__popUpCardList_scrollView;

    [SerializeField] ScrollerView_MenuBar__Card_Pop_Up_Marionette_List  Card__popUpMarionetteList_scrollViewMenuBar;
    [SerializeField] ScrollView__Card_Pop_Up_Marionette_List            Card__popUpMarionetteList_scrollView;


    [Header("RUNNING")]
    [SerializeField] List<int>  Card_marionetteList;    // inventoryId
    [SerializeField] int        Card_selectMarionette;

    [SerializeField] List<int>  Card_cardList;      // inventoryId
    [SerializeField] int        Card_selectCardSlot;

    ////////// Getter & Setter          //////////
    public ScrollView__Card_Pop_Up_Marionette_List  Card_PopUpMarionetteList_VarScrollView  { get { return Card__popUpMarionetteList_scrollView;    }   }
    
    public List<int>    Card_VarMarionetteList  { get { return Card_marionetteList; }   }

    public List<int>    Card_VarCardList    { get { return Card_cardList;   }   }

    ////////// Method                   //////////
    void Card_BtnClick(string[] _names)
    {
        switch(_names[2])
        {
            case "CARD__MARIONETTE_LIST":                       { Card_BtnClick__PopUpOn(_names[2]);                                    }   break;
            case "CARD__CARD_LIST":                             { Card_BtnClick__PopUpOnCardList(int.Parse(_names[3]));                 }   break;
            case "CARD__POP_UP_CARD_LIST__SCROLL_VIEW":         { Card_BtnClick__PopUpCardList_ScrollView(int.Parse(_names[3]));        }   break;
            case "CARD__POP_UP_MARIONETTE_LIST__MENU_BAR":      { Card_BtnClick__PopUpMarionetteList_MenuBar(int.Parse(_names[3]));     }   break;
            case "CARD__POP_UP_MARIONETTE_LIST__SCROLL_VIEW":   { Card_BtnClick__PopUpMarionetteList_ScrollView(int.Parse(_names[3]));  }   break;
        }
    }

    //
    void Card_BtnClick__PopUpOn(string _name)
    {
        Card_popUp.SetActive(true);
        //
        for(int for0 = 0; for0 < Card_popUp.transform.childCount; for0++)
        {
            Card_popUp.transform.GetChild(for0).gameObject.SetActive(false);
        }
        Card_popUp.transform.Find(_name).gameObject.SetActive(true);
    }

    void Card_BtnClick__PopUpOnCardList(int _slot)
    {
        Card_selectCardSlot = _slot;

        Card__popUpCardList_scrollView.Basic_SelectMenuBar(0);
        Card_BtnClick__PopUpOn("CARD__CARD_LIST");
    }

    //
    void Card_BtnClick__PopUpCardList_ScrollView(int _count)
    {
        Giggle_Item.Inventory data
            = (Giggle_Item.Inventory)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.PLAYER__CARD__GET_DATA_FROM_DATA_ID,
                Card_cardList[_count]);
        Giggle_Item.Card database
            = (Giggle_Item.Card)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.DATABASE__MARIONETTE__GET_CARD_FROM_ID,
                Card_cardList[_count]);
        
        Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
            Giggle_ScriptBridge.EVENT.PLAYER__MARIONETTE__CARD_EQUIP,
            //
            Card_marionetteList[Card_selectMarionette],
            Card_selectCardSlot, Card_cardList[_count]);

        Card_popUp.SetActive(false);
    }

    //
    void Card_BtnClick__PopUpMarionetteList_MenuBar(int _count)
    {
        Card__popUpMarionetteList_scrollViewMenuBar.Basic_SelectMenu(_count);
        Card_SelectTypeFormList(0);
    }

    //
    void Card_BtnClick__PopUpMarionetteList_ScrollView(int _count)
    {
        //
        Card_selectMarionette = _count;

        //
        if(!Card_selectMarionette.Equals(-1))
        {
            Giggle_Character.Save data
                = (Giggle_Character.Save)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.PLAYER__MARIONETTE__GET_DATA_FROM_INVENTORYID,
                    Card_marionetteList[Card_selectMarionette]);
            Giggle_Character.Database database
                = (Giggle_Character.Database)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.DATABASE__MARIONETTE__GET_DATA_FROM_ID,
                    data.Basic_VarDataId);

            Card_marionetteName.text = database.Basic_VarName;

            Card_popUp.SetActive(false);
        }
    }

    //
    void Card_Active()
    {
        Card_BtnClick__PopUpMarionetteList_MenuBar(0);
    }

    //
    public void Card_SettingCardList()
    {
        List<Giggle_Item.Inventory> res
            = (List<Giggle_Item.Inventory>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.PLAYER__ITEM__GET_CARD_LIST);

        //
        Card_cardList.Clear();
        for(int for0 = 0; for0 < res.Count; for0++)
        {
            Card_cardList.Add(res[for0].Basic_VarDataId);
        }
    }

    //
    public void Card_SelectTypeFormList(int _count)
    {
        //
        List<Giggle_Character.Save> list = null;
        switch(_count)
        {
            case 0: { list = Card_SelectTypeFromList__All();   }   break;
            case 1: {   }   break;
            case 2: {   }   break;
            case 3: {   }   break;
        }

        //
        Card_marionetteList.Clear();
        for(int for0 = 0; for0 < list.Count; for0++)
        {
            Card_marionetteList.Add(list[for0].Basic_VarInventoryId);
        }
    }

    List<Giggle_Character.Save> Card_SelectTypeFromList__All()
    {
        List<Giggle_Character.Save> res
            = (List<Giggle_Character.Save>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.PLAYER__MARIONETTE__GET_LIST);

        //
        return res;
    }

    ////////// Constructor & Destroyer  //////////
    
    //
    void Card_Start()
    {
        Card__popUpMarionetteList_scrollViewMenuBar.Basic_Init(this);
        for(int for0 = 0; for0 < Card__popUpMarionetteList_scrollViewMenuBar.Basic_VarListCount; for0++)
        {
            Card__popUpMarionetteList_scrollViewMenuBar.Basic_GetListBtn(for0).name = "Button/MARIONETTE/CARD__POP_UP_MARIONETTE_LIST__MENU_BAR/" + for0.ToString();
        }

        Card__popUpMarionetteList_scrollView.Basic_Init(this);
        Card__popUpMarionetteList_scrollView.Basic_VarContent.gameObject.SetActive(true);

        Card_popUp.SetActive(false);

        //
        if(Card_marionetteList == null)
        {
            Card_marionetteList = new List<int>();
        }
    }

    #endregion

    #region ITEM

    [Serializable]
    public class ScrollerView_MenuBar__Item : Giggle_UI.MenuBar
    {
        [SerializeField] Giggle_MainManager__Marionette Basic_uiData;

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////

        protected override void Basic_SelectMenu__Setting(int _for0, int _count)
        {
            if(_for0.Equals(_count))
            {
                Basic_uiData.Item_VarScrollView.Basic_SelectMenuBar(_count);
            }
        }

        ////////// Constructor & Destroyer  //////////
        public void Basic_Init(Giggle_MainManager__Marionette _uiData)
        {
            Basic_uiData = _uiData;
            Basic_Init();
        }
    }

    [Serializable]
    public class ScrollView_Item : Giggle_UI.ScrollView
    {
        [SerializeField] Giggle_MainManager__Marionette Basic_uiData;

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////

        protected override void Basic_Init__SetName()
        {
            Basic_list[0].Find("Button").name = "Button/MARIONETTE/ITEM__SCROLL_VIEW/0";
            Basic_list[1].Find("Button").name = "Button/MARIONETTE/ITEM__SCROLL_VIEW/1";
        }

        protected override void Basic_AddList__SetName(Transform _element)
        {
            _element.Find("Button").name = "Button/MARIONETTE/ITEM__SCROLL_VIEW/" + Basic_list.Count;
        }

        public override void Basic_ClickBtn(int _count)
        {
            // ui 그래픽 갱신
            for(int for0 = 0; for0 < Basic_list.Count; for0++)
            {
                Basic_list[for0].Find("Select").gameObject.SetActive(for0.Equals(_count));
            }
        }

        // Basic_SelectMenuBar
        public void Basic_SelectMenuBar(int _count)
        {
            Basic_uiData.Item_SelectTypeFormList(_count);

            Basic_SelectMenuBar__Check();

            Basic_ClickBtn(-1);
        }

        void Basic_SelectMenuBar__Check()
        {
            List<int> list = Basic_uiData.Formation_VarMarionetteList;

            //
            int finalCount = 0;
            int whileCount = 0;
            while(whileCount < Basic_list.Count)
            {
                if(whileCount < list.Count)
                {
                    Basic_list[whileCount].gameObject.SetActive(true);

                    // 기존 데이터 날리기
                    while(Basic_list[whileCount].Find("Obj").childCount > 0)
                    {
                        Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                            Giggle_ScriptBridge.EVENT.MASTER__GARBAGE__REMOVE,
                            //
                            Basic_list[whileCount].Find("Obj").GetChild(0));
                    }

                    //
                    Giggle_Character.Save data
                        = (Giggle_Character.Save)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                            Giggle_ScriptBridge.EVENT.PLAYER__MARIONETTE__GET_DATA_FROM_INVENTORYID,
                            list[whileCount]);

                    Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                        Giggle_ScriptBridge.EVENT.MASTER__UI__CHARACTER_INSTANTIATE,
                        //
                        data.Basic_VarDataId,
                        Basic_list[whileCount].Find("Obj"), -90.0f, 300.0f);
                    
                    finalCount = whileCount;
                }
                else
                {
                    Basic_list[whileCount].gameObject.SetActive(false);
                }

                //
                whileCount++;
            }

            Basic_content.sizeDelta
                = new Vector2(
                    0,
                    (Basic_list[finalCount].GetComponent<RectTransform>().sizeDelta.y * 0.5f) + Basic_list[finalCount].localPosition.y);

            Basic_CheckCover();
        }

        //
        public void Basic_CheckCover()
        {
            List<int>   formation       = (List<int>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.PLAYER__FORMATION__GET_SELECT_FORMATION);
            List<int>   characterList   = Basic_uiData.Formation_VarMarionetteList;

            int whileCount = 0;
            while(whileCount < characterList.Count)
            {
                Giggle_Character.Save data
                    = (Giggle_Character.Save)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                        Giggle_ScriptBridge.EVENT.PLAYER__MARIONETTE__GET_DATA_FROM_INVENTORYID,
                        characterList[whileCount]);
                
                //
                Basic_list[whileCount].Find("Cover").gameObject.SetActive(false);
                for(int for0 = 0; for0 < formation.Count; for0++)
                {
                    if( data.Equals(formation[for0]))
                    {
                        Basic_list[whileCount].Find("Cover").gameObject.SetActive(true);
                        break;
                    }
                }

                //
                whileCount++;
            }
        }

        //
        public void Basic_Init(Giggle_MainManager__Marionette _uiData)
        {
            Basic_Init();
            Basic_uiData = _uiData;
        }

        ////////// Constructor & Destroyer  //////////

    }

    [Header("ITEM ==========")]
    [SerializeField] List<Transform> Item_list;

    //
    [SerializeField] ScrollerView_MenuBar__Item Item_scrollViewMenuBar;
    [SerializeField] ScrollView_Item            Item_scrollView;

    [SerializeField] GameObject Item_infoParent;
    [SerializeField] Transform  Item_infoBasic;
    [SerializeField] Transform  Item_infoSkill;
    [SerializeField] Transform  Item_infoStatus;

    [Header("RUNNING")]
    [SerializeField] List<int>  Item_marionetteList;    // inventoryId
    [SerializeField] int        Item_selectMarionette;

    ////////// Getter & Setter          //////////
    public ScrollView_Item  Item_VarScrollView  { get { return Item_scrollView; }   }

    ////////// Method                   //////////
    void Item_BtnClick(string[] _names)
    {
        switch(_names[2])
        {
            case "ITEM__MENU_BAR":      { Item_BtnMenuBar(int.Parse(_names[3]));    }   break;
            case "ITEM__SCROLL_VIEW":   { Item_BtnScrollView(int.Parse(_names[3])); }   break;
            case "ITEM__INFO_BACK":     { Item_BtnInfoBack();                       }   break;
            //case "ITEM__INFO_LEVEL_UP": { Marionette_BtnInfoLevelUp();              }   break;
        }
    }
    //
    void Item_BtnMenuBar(int _count)
    {
        Item_scrollViewMenuBar.Basic_SelectMenu(_count);
    }

    //
    void Item_BtnScrollView(int _count)
    {
        Item_scrollView.Basic_ClickBtn(_count);

        //
        Item_selectMarionette = _count;

        //
        if(!Item_selectMarionette.Equals(-1))
        {
            Giggle_Character.Save data
                = (Giggle_Character.Save)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.PLAYER__MARIONETTE__GET_DATA_FROM_INVENTORYID,
                    Item_marionetteList[Item_selectMarionette]);
            Giggle_Character.Database database
                = (Giggle_Character.Database)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.DATABASE__MARIONETTE__GET_DATA_FROM_ID,
                    data.Basic_VarDataId);
            
            Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.MASTER__UI__CHARACTER_INSTANTIATE,
                //
                data.Basic_VarDataId,
                Item_infoBasic.Find("Portrait"), -90.0f, 300.0f);
            Item_infoBasic.Find("Name").GetComponent<TextMeshProUGUI>().text = database.Basic_VarName;
            //Mationette_infoBasic.Find("Type").GetComponent<TextMeshProUGUI>().text = database.ba;
            //Mationette_infoBasic.Find("Attribute").GetComponent<TextMeshProUGUI>().text = database.ba;

            Item_scrollView.Basic_VarContent.gameObject.SetActive(false);
            Item_infoParent.SetActive(true);
        }
    }

    //
    void Item_BtnInfoBack()
    {
        Item_BtnScrollView(-1);

        Item_scrollView.Basic_VarContent.gameObject.SetActive(true);
        Item_infoParent.SetActive(false);
    }

    //
    public void Item_SelectTypeFormList(int _count)
    {
        //
        List<Giggle_Character.Save> list = null;
        switch(_count)
        {
            case 0: { list = Item_SelectTypeFromList__All();   }   break;
            case 1: {   }   break;
            case 2: {   }   break;
            case 3: {   }   break;
        }

        //
        Item_marionetteList.Clear();
        for(int for0 = 0; for0 < list.Count; for0++)
        {
            Item_marionetteList.Add(list[for0].Basic_VarInventoryId);
        }
    }

    List<Giggle_Character.Save> Item_SelectTypeFromList__All()
    {
        List<Giggle_Character.Save> res
            = (List<Giggle_Character.Save>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.PLAYER__MARIONETTE__GET_LIST);

        //
        return res;
    }
    
    //
    void Item_Start()
    {
        Item_scrollViewMenuBar.Basic_Init(this);
        for(int for0 = 0; for0 < Item_scrollViewMenuBar.Basic_VarListCount; for0++)
        {
            Item_scrollViewMenuBar.Basic_GetListBtn(for0).name = "Button/MARIONETTE/ITEM__MENU_BAR/" + for0.ToString();
        }

        Item_scrollView.Basic_Init(this);
        Item_scrollView.Basic_VarContent.gameObject.SetActive(true);

        Item_infoParent.SetActive(false);

        // Running
        if(Item_marionetteList == null)
        {
            Item_marionetteList = new List<int>();
        }
    }

    ////////// Constructor & Destroyer  //////////

    #endregion

    #region COSTUMN

    ////////// Getter & Setter          //////////
    
    ////////// Method                   //////////

    ////////// Constructor & Destroyer  //////////

    #endregion
}