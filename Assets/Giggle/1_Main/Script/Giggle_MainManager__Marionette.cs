using UnityEngine;

//
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class Giggle_MainManager__Marionette : MonoBehaviour
{
    [SerializeField] GameObject Basic_back;
    [SerializeField] GameObject Basic_ui;

    [SerializeField] Giggle_UI.MenuBar BasicArea1_menuBar;

    [Header("RUNNING")]
    [SerializeField] IEnumerator    Basic_coroutine;

    ////////// Getter & Setter  //////////
    
    ////////// Method           //////////
    public void Basic_BtnClick(GameObject _obj)
    {
        string[] names = _obj.name.Split('/');
        switch(names[2])
        {
            case "CLOSE":       { Basic_BtnClose();                                 }   break;
            case "MENU_BAR":    { BasicArea1_BtnSelectMenu(int.Parse(names[3]));    }   break;
            //
            case "FORMATION__PRESET":       { Formation_BtnPreset(      int.Parse(names[3]));   }   break;
            case "FORMATION__TILE":         { Formation_BtnTile(        int.Parse(names[3]));   }   break;
            case "FORMATION__MENU_BAR":     { Formation_BtnMenuBar(     int.Parse(names[3]));   }   break;
            case "FORMATION__SCROLL_VIEW":  { Formation_BtnScrollView(  int.Parse(names[3]));   }   break;
            //
            case "MARIONETTE__MENU_BAR":        { Marionette_BtnMenuBar(int.Parse(names[3]));       }   break;
            case "MARIONETTE__SCROLL_VIEW":     { Marionette_BtnScrollView(int.Parse(names[3]));    }   break;
            case "MARIONETTE__INFO_BACK":       { Marionette_BtnInfoBack();                         }   break;
            case "MARIONETTE__INFO_LEVEL_UP":   { Marionette_BtnInfoLevelUp();                      }   break;
            //
            case "CONSTELLATION__LIST":                     { Constellation_PopUpOn(names[2]);                              }   break;
            case "CONSTELLATION__STAR":                     { Constellation_BtnStar(int.Parse(names[3]));                   }   break;
            case "CONSTELLATION__POP_UP_LIST__MENU_BAR":    { Constellation_PopUpList_BtnMenuBar(int.Parse(names[3]));      }   break;
            case "CONSTELLATION__POP_UP_LIST__SCROLL_VIEW": { Constellation_PopUpList_BtnScrollView(int.Parse(names[3]));   }   break;
            case "CONSTELLATION__POP_UP_STAR__CLOSE":       { Constellation_PopUp_BtnClose();                               }   break;
            //
            case "CARD__MARIONETTE_LIST":                       { Card_PopUpOn(names[2]);                                       }   break;
            case "CARD__CARD_LIST":                             { Card_PopUpOnCardList(int.Parse(names[3]));                    }   break;
            case "CARD__POP_UP_CARD_LIST__SCROLL_VIEW":         { Card_PopUpCardList_BtnScrollView(int.Parse(names[3]));        }   break;
            case "CARD__POP_UP_MARIONETTE_LIST__MENU_BAR":      { Card_PopUpMarionetteList_BtnMenuBar(int.Parse(names[3]));     }   break;
            case "CARD__POP_UP_MARIONETTE_LIST__SCROLL_VIEW":   { Card_PopUpMarionetteList_BtnScrollView(int.Parse(names[3]));  }   break;
            //
            case "ITEM__MENU_BAR":      { Item_BtnMenuBar(int.Parse(names[3]));     }   break;
            case "ITEM__SCROLL_VIEW":   { Item_BtnScrollView(int.Parse(names[3]));  }   break;
            case "ITEM__INFO_BACK":     { Item_BtnInfoBack();                       }   break;
            //case "ITEM__INFO_LEVEL_UP": { Marionette_BtnInfoLevelUp();              }   break;
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
        
        BasicArea1_menuBar.Basic_Init();
        for(int for0 = 0; for0 < BasicArea1_menuBar.Basic_VarListCount; for0++)
        {
            BasicArea1_menuBar.Basic_GetListBtn(for0).name = "Button/MARIONETTE/MENU_BAR/" + for0.ToString();
        }
        
        Formation_Start();
        Marionette_Start();
        Constellation_Start();
        Card_Start();
        Item_Start();
    }

    #region FORMATION

    [Serializable]
    public class ScrollerView_MenuBar__Formation : Giggle_UI.MenuBar
    {
        [SerializeField] Giggle_MainManager__Marionette Basic_uiData;

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////

        public override void Basic_SelectMenu(int _count)
        {
            for(int for0 = 0; for0 < Basic_list.Count; for0++)
            {
                bool isClick = for0.Equals(_count);

                Basic_list[for0].gameObject.SetActive(!isClick);
            }

            //
            Basic_uiData.Formation_VarScrollView.Basic_SelectMenuBar(_count);
        }

        ////////// Constructor & Destroyer  //////////
        public void Basic_Init(Giggle_MainManager__Marionette _uiData)
        {
            Basic_uiData = _uiData;
            Basic_Init();
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
            Basic_uiData.Formation_SelectTypeFormList(_count);

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

    [Header("FORMATION ==========")]
    // Area2
    [SerializeField] Transform       Formation_parent;
    [SerializeField] List<Transform> Formation_list;
    
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
    void Formation_BtnPreset(int _count)
    {
        //Marionette_formationSelect = _count;

        Formation_Reset();
    }

    // Formation_BtnTile
    void Formation_BtnTile(int _count)
    {
        Formation_selectFormation = _count;

        switch(Formation_selectMarionette)
        {
            case -1:    { Formation_BtnTile__Select(_count);    }   break;
            default:    { Formation_BtnTile__Change(_count);    }   break;
        }
    }

    void Formation_BtnTile__Select(int _count)
    {
        for (int for0 = 0; for0 < Formation_list.Count; for0++)
        {
            Formation_list[for0].Find("Select").gameObject.SetActive(for0.Equals(_count));
        }

        //
        Formation_selectFormation = _count;
        if(!_count.Equals(-1))
        {
            //List<int> formation = (List<int>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.PLAYER__FORMATION__GET_SELECT_FORMATION);
            Formation_selectMarionette = _count;
        }

        // UI갱신
        //MarionetteFormation_Reset();
    }

    void Formation_BtnTile__Change(int _count)
    {
        Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
            Giggle_ScriptBridge.EVENT.PLAYER__FORMATION__FORMATION_SETTING,
            Formation_marionetteList[Formation_selectMarionette], _count);

        // UI갱신
        Formation_Reset();
    }

    // 
    void Formation_BtnMenuBar(int _count)
    {
        Formation_scrollViewMenuBar.Basic_SelectMenu(_count);
    }


    //
    void Formation_BtnScrollView(int _count)
    {
        Formation_scrollView.Basic_ClickBtn(_count);

        //
        Formation_selectMarionette = _count;
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

        Formation_BtnMenuBar(0);
    }

    //
    void Formation_Reset()
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
        Formation_BtnScrollView(-1);
        Formation_scrollView.Basic_CheckCover();
        Formation_BtnTile(-1);

        List<int> formation = (List<int>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.PLAYER__FORMATION__GET_SELECT_FORMATION);
        // 타일 처리
        for(int for0 = 0; for0 < Formation_list.Count; for0++)
        {
            // 기존 마리오네트 모델링 날리기
            while(Formation_list[for0].Find("Obj").childCount > 0)
            {
                Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.MASTER__GARBAGE__REMOVE,
                    Formation_list[for0].Find("Obj").GetChild(0));
            }

            // 배치된 모델링이 있다면 모델링 배치
            int tileId = formation[for0];
            if(!tileId.Equals(-1))
            {
                // 주인공
                if(tileId.Equals(-2))
                {
                    Giggle_Character.Save pinocchioData
                        = (Giggle_Character.Save)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                            Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__VAR_DATA);
                    Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                        Giggle_ScriptBridge.EVENT.MASTER__UI__PINOCCHIO_INSTANTIATE,
                        //
                        pinocchioData.Basic_VarDataId,
                        Formation_list[for0].Find("Obj"), -90.0f, 300.0f);
                }
                else
                {
                    Giggle_Character.Save data
                        = (Giggle_Character.Save)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                            Giggle_ScriptBridge.EVENT.PLAYER__MARIONETTE__GET_DATA_FROM_INVENTORYID,
                            tileId);
                    Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                        Giggle_ScriptBridge.EVENT.MASTER__UI__CHARACTER_INSTANTIATE,
                        //
                        data.Basic_VarDataId,
                        Formation_list[for0].Find("Obj"), -90.0f, 300.0f);
                }
            }
        }
    }

    void Formation_Start()
    {
        // Area2
        if(Formation_list == null)
        {
            Formation_list = new List<Transform>();
        }

        int whileCount = 0;
        while(whileCount < Formation_parent.childCount)
        {
            Transform for0Child = Formation_parent.GetChild(whileCount);
            for(int for0 = 0; for0 < for0Child.childCount; for0++)
            {
                Formation_list.Add(for0Child.GetChild(for0));
            }

            whileCount++;
        }

        whileCount = 0;
        while(whileCount < Formation_list.Count)
        {
            for(int for0 = whileCount; for0 < Formation_list.Count; for0++)
            {
                Transform element = Formation_list[for0];
                if(element.name.Equals(whileCount.ToString()))
                {
                    element.Find("Button").name = "Button/MARIONETTE/FORMATION__TILE/" + whileCount;
                    Formation_list.RemoveAt(for0);
                    Formation_list.Insert(whileCount, element);
                }
            }

            whileCount++;
        }

        // Area3
        Formation_scrollViewMenuBar.Basic_Init(this);
        for(int for0 = 0; for0 < Formation_scrollViewMenuBar.Basic_VarListCount; for0++)
        {
            Formation_scrollViewMenuBar.Basic_GetListBtn(for0).name = "Button/MARIONETTE/FORMATION__MENU_BAR/" + for0.ToString();
        }

        Formation_scrollView.Basic_Init(this);

        // Running
        if(Formation_marionetteList == null)
        {
            Formation_marionetteList = new List<int>();
        }
    }

    ////////// Constructor & Destroyer  //////////

    #endregion

    #region MARIONETTE

    [Serializable]
    public class ScrollerView_MenuBar__Marionette : Giggle_UI.MenuBar
    {
        [SerializeField] Giggle_MainManager__Marionette Basic_uiData;

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////

        public override void Basic_SelectMenu(int _count)
        {
            for(int for0 = 0; for0 < Basic_list.Count; for0++)
            {
                bool isClick = for0.Equals(_count);

                Basic_list[for0].gameObject.SetActive(!isClick);
            }

            //
            Basic_uiData.Formation_VarScrollView.Basic_SelectMenuBar(_count);
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
            Basic_uiData.Marionette_SelectTypeFormList(_count);

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

    [Header("RUNNING")]
    [SerializeField] List<int>  Marionette_marionetteList;   // inventoryId
    [SerializeField] int        Marionette_selectMarionette;

    ////////// Getter & Setter          //////////
    public ScrollView_Marionette    Marionette_VarScrollView    { get { return Marionette_scrollView;   }   }

    ////////// Method                   //////////
    //
    void Marionette_BtnMenuBar(int _count)
    {
        Marionette_scrollViewMenuBar.Basic_SelectMenu(_count);
    }

    //
    void Marionette_BtnScrollView(int _count)
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
            
            Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.MASTER__UI__CHARACTER_INSTANTIATE,
                //
                data.Basic_VarDataId,
                Mationette_infoBasic.Find("Portrait"), -90.0f, 300.0f);
            Mationette_infoBasic.Find("Name").GetComponent<TextMeshProUGUI>().text = database.Basic_VarName;
            //Mationette_infoBasic.Find("Type").GetComponent<TextMeshProUGUI>().text = database.ba;
            //Mationette_infoBasic.Find("Attribute").GetComponent<TextMeshProUGUI>().text = database.ba;

            Marionette_scrollView.Basic_VarContent.gameObject.SetActive(false);
            Marionette_infoParent.SetActive(true);
        }
    }

    //
    void Marionette_BtnInfoBack()
    {
        Marionette_BtnScrollView(-1);

        Marionette_scrollView.Basic_VarContent.gameObject.SetActive(true);
        Marionette_infoParent.SetActive(false);
    }

    //
    void Marionette_BtnInfoLevelUp()
    {

    }

    //
    void Marionette_Active()
    {
        //
        //Formation_Reset();

        Marionette_BtnMenuBar(0);
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

        public override void Basic_SelectMenu(int _count)
        {
            for(int for0 = 0; for0 < Basic_list.Count; for0++)
            {
                bool isClick = for0.Equals(_count);

                Basic_list[for0].gameObject.SetActive(!isClick);
            }

            //
            Basic_uiData.Constellation_PopUpList_VarScrollView.Basic_SelectMenuBar(_count);
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
    void Constellation_BtnStar(int _count)
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

        Constellation_PopUpOn("CONSTELLATION__STAR");
    }

    //
    void Constellation_PopUpOn(string _name)
    {
        Contellation_popUp.SetActive(true);
        //
        for(int for0 = 0; for0 < Contellation_popUp.transform.childCount; for0++)
        {
            Contellation_popUp.transform.GetChild(for0).gameObject.SetActive(false);
        }
        Contellation_popUp.transform.Find(_name).gameObject.SetActive(true);
    }

    //
    void Constellation_PopUp_BtnClose()
    {
        Contellation_popUp.SetActive(false);
    }

    //
    void Constellation_PopUpList_BtnMenuBar(int _count)
    {
        Contellation__popUpList_scrollViewMenuBar.Basic_SelectMenu(_count);
        Constellation_SelectTypeFormList(0);
    }

    //
    void Constellation_PopUpList_BtnScrollView(int _count)
    {
        //
        Contellation_selectMarionette = _count;

        //
        if(!Contellation_selectMarionette.Equals(-1))
        {
            Giggle_Character.Save data
                = (Giggle_Character.Save)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.PLAYER__MARIONETTE__GET_DATA_FROM_INVENTORYID,
                    Contellation_marionetteList[Contellation_selectMarionette]);
            Giggle_Character.Database database
                = (Giggle_Character.Database)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.DATABASE__MARIONETTE__GET_DATA_FROM_ID,
                    data.Basic_VarDataId);

            Constellation_marionetteName.text = database.Basic_VarName;

            Contellation_popUp.SetActive(false);
        }
    }

    //
    void Constellation_Active()
    {
        Constellation_PopUpList_BtnMenuBar(0);
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

        public override void Basic_SelectMenu(int _count)
        {
            for(int for0 = 0; for0 < Basic_list.Count; for0++)
            {
                bool isClick = for0.Equals(_count);

                Basic_list[for0].gameObject.SetActive(!isClick);
            }

            //
            Basic_uiData.Card_PopUpMarionetteList_VarScrollView.Basic_SelectMenuBar(_count);
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

    //
    void Card_PopUpOn(string _name)
    {
        Card_popUp.SetActive(true);
        //
        for(int for0 = 0; for0 < Card_popUp.transform.childCount; for0++)
        {
            Card_popUp.transform.GetChild(for0).gameObject.SetActive(false);
        }
        Card_popUp.transform.Find(_name).gameObject.SetActive(true);
    }

    void Card_PopUpOnCardList(int _slot)
    {
        Card_selectCardSlot = _slot;

        Card__popUpCardList_scrollView.Basic_SelectMenuBar(0);
        Card_PopUpOn("CARD__CARD_LIST");
    }

    //
    void Card_PopUpCardList_BtnScrollView(int _count)
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
    void Card_PopUpMarionetteList_BtnMenuBar(int _count)
    {
        Card__popUpMarionetteList_scrollViewMenuBar.Basic_SelectMenu(_count);
        Card_SelectTypeFormList(0);
    }

    //
    void Card_PopUpMarionetteList_BtnScrollView(int _count)
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
        Card_PopUpMarionetteList_BtnMenuBar(0);
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

        public override void Basic_SelectMenu(int _count)
        {
            for(int for0 = 0; for0 < Basic_list.Count; for0++)
            {
                bool isClick = for0.Equals(_count);

                Basic_list[for0].gameObject.SetActive(!isClick);
            }

            //
            Basic_uiData.Item_VarScrollView.Basic_SelectMenuBar(_count);
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