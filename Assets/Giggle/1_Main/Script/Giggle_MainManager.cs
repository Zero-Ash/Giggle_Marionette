using UnityEngine;

//
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using Unity.Mathematics;

public class Giggle_MainManager : Giggle_SceneManager
{

    #region BASIC

    ////////// Getter & Setter  //////////

    ////////// Method           //////////

    ////////// Unity            //////////

    protected override void Basic_Start()
    {
        UI_Start();
    }

    #endregion

    #region UI

    [Serializable]
    public class UI_MainBasicData : UI_BasicData
    {
        [SerializeField] Giggle_MainManager Basic_manager;

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////

        ////////// Constructor & Destroyer  //////////
        public void Basic_Init()
        {
            Pinocchio_data.Basic_Init();
            Marionette_Init();
        }
        
        #region AREA4

        [Header("AREA4 ==================================================")]
        [SerializeField] GameObject Area4_exMenu;

        ////////// Getter & Setter          //////////
        //bool Area4_VarExMenuActive   { set { Area4_exMenu.SetActive(value);  }   }

        ////////// Method                   //////////

        public void Area4_BtnClick(string[] _names)
        {
            switch(_names[2])
            {
                case "EXMENU_OPEN":     { Area4_exMenu.SetActive(true);     }   break;
                case "EXMENU_CLOSE":    { Area4_exMenu.SetActive(false);    }   break;
            }
        }

        ////////// Constructor & Destroyer  //////////

        #endregion
        
        #region AREA7

        [Header("AREA7 ==================================================")]
        [SerializeField] GameObject Area7_exMenu;

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////

        public void Area7_BtnClick(string[] _names)
        {
            switch(_names[2])
            {
                case "GACHA":       { Area7_Gacha();        }   break;
                case "PINOCCHIO":   { Area7_PinocchioOn();  }   break;
                case "MARIONETTE":  { Area7_MarionetteOn(); }   break;
            }
        }

        void Area7_Gacha()
        {
            Basic_manager.UI_PopUpActive("GACHA");
        }

        void Area7_PinocchioOn()
        {
            Pinocchio_data.Basic_Active();
        }

        void Area7_MarionetteOn()
        {
            Marionette_Active();
        }

        ////////// Constructor & Destroyer  //////////

        #endregion

        #region PINOCCHIO

        [Serializable]
        public class Pinocchio : IDisposable
        {
            [SerializeField] GameObject Basic_ui;

            [SerializeField] Giggle_UI.MenuBar BasicArea1_menuBar;

            ////////// Getter & Setter          //////////
            
            ////////// Method                   //////////
            
            public void Basic_BtnClick(string[] _names)
            {
                switch(_names[2])
                {
                    case "CLOSE":       { Basic_Close();                                }   break;
                    case "MENU_BAR":    { BasicArea1_SelectMenu(int.Parse(_names[3]));  }   break;
                    //
                    case "JOB__MENU_BAR":       { JobArea3_menuBar.Basic_SelectMenu(int.Parse(_names[3]));  }   break;
                    case "JOB__SCROLL_VIEW":    { JobArea3_scrollView.Basic_ClickBtn(int.Parse(_names[3])); }   break;
                    //
                    case "EQUIPMENT__MENU_BAR":     { EquipmentArea3_menuBar.Basic_SelectMenu(int.Parse(_names[3]));    }   break;
                    case "EQUIPMENT__EQUIPMENT":    {   }   break;
                }
            }

            void Basic_Close()
            {
                Basic_ui.SetActive(false);

                //
                Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MASTER__BATTLE__VAR_COROUTINE_PHASE, Giggle_Battle.Basic__COROUTINE_PHASE.PLAYER_SETTING_START);
            }

            void BasicArea1_SelectMenu(int _count)
            {
                BasicArea1_menuBar.Basic_SelectMenu(_count);
            }

            //
            public void Basic_Active()
            {
                Equipment_Active();

                BasicArea1_SelectMenu(0);
                Job_SelectMenu(0);
                Job_InfoSetting();

                Basic_ui.SetActive(true);
            }

            public void Basic_Init()
            {
                Basic_ui.SetActive(false);

                BasicArea1_menuBar.Basic_Init();
                for(int for0 = 0; for0 < BasicArea1_menuBar.Basic_VarListCount; for0++)
                {
                    BasicArea1_menuBar.Basic_GetListBtn(for0).name = "Button/PINOCCHIO/MENU_BAR/" + for0.ToString();
                }

                Job_Init();
                Equipment_Init();
            }

            ////////// Constructor & Destroyer  //////////
            
            public void Dispose()
            {

            }

            #region JOB

            [Serializable]
            public class MenuBar_Job : Giggle_UI.MenuBar
            {
                [SerializeField] Pinocchio Basic_parentClass;

                ////////// Getter & Setter          //////////

                ////////// Method                   //////////

                public void Basic_Init(Pinocchio _parentClass)
                {
                    Basic_parentClass = _parentClass;
                    Basic_Init();
                }

                public override void Basic_SelectMenu(int _count)
                {
                    for(int for0 = 0; for0 < Basic_list.Count; for0++)
                    {
                        bool isClick = for0.Equals(_count);

                        Basic_list[for0].gameObject.SetActive(!isClick);
                    }

                    //
                    Basic_parentClass.Job_VarScrollView.Basic_SelectMenuBar(_count);
                }

                ////////// Constructor & Destroyer  //////////
                
            }

            [Serializable]
            public class ScrollView_Job : Giggle_UI.ScrollView
            {
                [SerializeField] Pinocchio  Basic_parentClass;

                ////////// Getter & Setter          //////////

                ////////// Method                   //////////

                public void Basic_Init(Pinocchio _parentClass)
                {
                    Basic_parentClass = _parentClass;
                    Basic_Init();
                }

                protected override void Basic_Init__SetName()
                {
                    Basic_list[0].Find("Button").name = "Button/PINOCCHIO/JOB__SCROLL_VIEW/0";
                    Basic_list[1].Find("Button").name = "Button/PINOCCHIO/JOB__SCROLL_VIEW/1";
                }

                protected override void Basic_AddList__SetName(Transform _element)
                {
                    _element.Find("Button").name = "Button/PINOCCHIO/JOB__SCROLL_VIEW/" + Basic_list.Count;
                }

                public override void Basic_ClickBtn(int _count)
                {
                    //
                    if(!_count.Equals(-1))
                    {
                        Giggle_Character.Save pinocchioData
                            = (Giggle_Character.Save)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                                Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__VAR_DATA);

                        List<int> jobList = (List<int>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__VAR_JOBS);

                        //
                        pinocchioData.Basic_VarDataId = jobList[_count];
                        Basic_parentClass.Job_InfoSetting();
                        Basic_CheckCover();
                    }
                }

                // Basic_SelectMenuBar
                public void Basic_SelectMenuBar(int _count)
                {
                    Basic_parentClass.Job_ListSetting(0);
                    Basic_SelectMenuBar__Check();

                    Basic_ClickBtn(-1);
                }

                void Basic_SelectMenuBar__Check()
                {
                    int finalCount = 0;
                    int whileCount = 0;
                    while(whileCount < Basic_list.Count)
                    {
                        if(whileCount < Basic_parentClass.Job_VarJobList.Count)
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
                            Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                                Giggle_ScriptBridge.EVENT.MASTER__UI__PINOCCHIO_INSTANTIATE,
                                //
                                Basic_parentClass.Job_VarJobList[whileCount],
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
                void Basic_CheckCover()
                {
                    Giggle_Character.Save pinocchioData
                        = (Giggle_Character.Save)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                            Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__VAR_DATA);

                    List<int> jobList = (List<int>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__VAR_JOBS);

                    int whileCount = 0;
                    while(whileCount < jobList.Count)
                    {
                        Basic_list[whileCount].Find("Cover").gameObject.SetActive(false);
                        if(jobList[whileCount].Equals(pinocchioData.Basic_VarDataId))
                        {
                            Basic_list[whileCount].Find("Cover").gameObject.SetActive(true);
                        }

                        //
                        whileCount++;
                    }
                }

                ////////// Constructor & Destroyer  //////////
            }

            [Header("JOB ==========")]
            [SerializeField] Transform  JobArea2_objParent;

            [SerializeField] MenuBar_Job    JobArea3_menuBar;
            [SerializeField] ScrollView_Job JobArea3_scrollView;

            [SerializeField] List<int>  Job_jobList;

            ////////// Getter & Setter          //////////

            //
            public ScrollView_Job   Job_VarScrollView   { get { return JobArea3_scrollView; }   }

            //
            public List<int>    Job_VarJobList  { get { return Job_jobList; }   }

            ////////// Method                   //////////
            void Job_SelectMenu(int _count)
            {
                JobArea3_menuBar.Basic_SelectMenu(_count);
            }

            void Job_Active()
            {
                //
                //Equipment_inventoryItems
                //    = (List<Giggle_Item.Inventory>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                //        Giggle_ScriptBridge.EVENT.PLAYER__ITEM__GET_ITEM_LIST);

                //Equipment_characterList
                //    = (List<Giggle_Character.Save>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                //        Giggle_ScriptBridge.EVENT.PLAYER__MARIONETTE__GET_LIST);
            }

            public void Job_ListSetting(int _selectType)
            {
                Job_jobList = (List<int>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__VAR_JOBS);
                switch(_selectType)
                {
                    case 0: {   }   break;
                }
            }

            public void Job_InfoSetting()
            {
                while(JobArea2_objParent.childCount > 0)
                {
                    Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                        Giggle_ScriptBridge.EVENT.MASTER__GARBAGE__REMOVE,
                        //
                        JobArea2_objParent.GetChild(0));
                }

                Giggle_Character.Save pinocchioData
                    = (Giggle_Character.Save)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                        Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__VAR_DATA);

                Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.MASTER__UI__PINOCCHIO_INSTANTIATE,
                    //
                    pinocchioData.Basic_VarDataId,
                    JobArea2_objParent, -90.0f, 900.0f);
            }

            void Job_Init()
            {
                // Area2
                // Area3
                JobArea3_menuBar.Basic_Init(this);
                for(int for0 = 0; for0 < JobArea3_menuBar.Basic_VarListCount; for0++)
                {
                    JobArea3_menuBar.Basic_GetListBtn(for0).name = "Button/PINOCCHIO/JOB__MENU_BAR/" + for0.ToString();
                }
                JobArea3_scrollView.Basic_Init(this);
            }

            ////////// Constructor & Destroyer  //////////

            #endregion

            #region EQUIPMENT

            [Serializable]
            public class MenuBar_Equipment : Giggle_UI.MenuBar
            {
                [SerializeField] Pinocchio Basic_parentClass;

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
                    Basic_parentClass.Equipment_VarScrollView.Basic_SelectMenuBar(_count);
                }

                public void Basic_Init(Pinocchio _parentClass)
                {
                    Basic_parentClass = _parentClass;
                }

                ////////// Constructor & Destroyer  //////////
            }

            [Serializable]
            public class ScrollView_Equipment : Giggle_UI.ScrollView
            {
                [SerializeField] Pinocchio  Basic_parentClass;

                ////////// Getter & Setter          //////////

                ////////// Method                   //////////

                protected override void Basic_Init__SetName()
                {
                    Basic_list[0].Find("Button").name = "Button/PINOCCHIO/EQUIPMENT__SCROLL_VIEW/0";
                    Basic_list[1].Find("Button").name = "Button/PINOCCHIO/EQUIPMENT__SCROLL_VIEW/1";
                }

                protected override void Basic_AddList__SetName(Transform _element)
                {
                    _element.Find("Button").name = "Button/PINOCCHIO/EQUIPMENT__SCROLL_VIEW/" + Basic_list.Count;
                }

                public override void Basic_ClickBtn(int _count)
                {
                    // ui 그래픽 갱신
                    for(int for0 = 0; for0 < Basic_list.Count; for0++)
                    {
                        Basic_list[for0].Find("Select").gameObject.SetActive(for0.Equals(_count));
                    }

                    //
                    //Basic_manager.UI_VarBasicData.MarionetteFormation_VarSelectMarionette = _count;
                }

                // Basic_SelectMenuBar
                public void Basic_SelectMenuBar(int _count)
                {
                    switch(_count)
                    {
                        case 0: { Basic_SelectMenuBar__All();   }   break;
                        case 1: {   }   break;
                        case 2: {   }   break;
                        case 3: {   }   break;
                    }

                    Basic_ClickBtn(-1);
                }

                void Basic_SelectMenuBar__All()
                {
                    List<Giggle_Character.Save> characterList   = Basic_parentClass.Equipment_characterList;
                    
                    Basic_SelectMenuBar__Check(characterList);
                }

                void Basic_SelectMenuBar__Check(List<Giggle_Character.Save> _characterList)
                {
                    int finalCount = 0;
                    int whileCount = 0;
                    while(whileCount < Basic_list.Count)
                    {
                        if(whileCount < _characterList.Count)
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
                            Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                                Giggle_ScriptBridge.EVENT.MASTER__UI__CHARACTER_INSTANTIATE,
                                //
                                _characterList[whileCount].Basic_VarDataId,
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
                    List<Giggle_Item.Inventory> itemList    = Basic_parentClass.Equipment_VarInventoryItems;

                    int whileCount = 0;
                    while(whileCount < itemList.Count)
                    {
                        Basic_list[whileCount].Find("Cover").gameObject.SetActive(false);
                        //for(int for0 = 0; for0 < formation.Basic_VarFormation.Count; for0++)
                        //{
                        //    if( characterList[whileCount].Basic_VarInventoryId.Equals(
                        //            formation.Basic_VarFormation[for0]))
                        //    {
                        //        Basic_list[whileCount].Find("Cover").gameObject.SetActive(true);
                        //        break;
                        //    }
                        //}

                        //
                        whileCount++;
                    }
                }

                ////////// Constructor & Destroyer  //////////

            }

            [Header("EQUIPMENT ==========")]
            [SerializeField] MenuBar_Equipment      EquipmentArea3_menuBar;
            [SerializeField] ScrollView_Equipment   EquipmentArea3_scrollView;

            [SerializeField] List<Giggle_Item.Inventory>    Equipment_inventoryItems;
            [SerializeField] List<Giggle_Character.Save>    Equipment_characterList;

            ////////// Getter & Setter          //////////
            public ScrollView_Equipment Equipment_VarScrollView { get { return EquipmentArea3_scrollView;   }   }

            public List<Giggle_Item.Inventory>  Equipment_VarInventoryItems { get { return Equipment_inventoryItems;    }   }
            
            ////////// Method                   //////////
            void Equipment_SelectMenu(int _count)
            {
                EquipmentArea3_menuBar.Basic_SelectMenu(_count);
            }

            void Equipment_Active()
            {
                //
                Equipment_inventoryItems
                    = (List<Giggle_Item.Inventory>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                        Giggle_ScriptBridge.EVENT.PLAYER__ITEM__GET_ITEM_LIST);

                Equipment_characterList
                    = (List<Giggle_Character.Save>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                        Giggle_ScriptBridge.EVENT.PLAYER__MARIONETTE__GET_LIST);
            }

            void Equipment_Init()
            {
                // Area2
                // Area3
                EquipmentArea3_menuBar.Basic_Init(this);
                for(int for0 = 0; for0 < EquipmentArea3_menuBar.Basic_VarListCount; for0++)
                {
                    EquipmentArea3_menuBar.Basic_GetListBtn(for0).name = "Button/PINOCCHIO/EQUIPMENT__MENU_BAR/" + for0.ToString();
                }
                EquipmentArea3_scrollView.Basic_Init();
            }

            ////////// Constructor & Destroyer  //////////

            #endregion
        }

        [Header("PINOCCHIO ==================================================")]
        [SerializeField] Pinocchio Pinocchio_data;

        ////////// Getter & Setter          //////////
        public Pinocchio Pinocchio_VarData   { get { return Pinocchio_data; }   }

        ////////// Method                   //////////

        ////////// Constructor & Destroyer  //////////

        #endregion

        #region MARIONETTE

        [Header("MARIONETTE ==================================================")]
        [SerializeField] GameObject Marionette_ui;

        // Area1
        [SerializeField] Giggle_UI.MenuBar  Marionette_menuBar;

        // Marionette
        [SerializeField] List<Giggle_Character.Save>    Marionette_marionetteList;
        [SerializeField] int                            Marionette_formationSelect;
        [SerializeField] List<Giggle_Player.Formation>  Marionette_formationList;

        ////////// Getter & Setter          //////////
        public List<Giggle_Character.Save> Marionette_VarMarionetteList { get { return Marionette_marionetteList;   }   }

        ////////// Method                   //////////

        public void Marionette_BtnClick(string[] _names)
        {
            switch(_names[2])
            {
                case "CLOSE":       { Marionette_Close();                           }   break;
                case "MENU_BAR":    { Marionette_SelectMenu(int.Parse(_names[3]));  }   break;
                //
                case "FORMATION__PRESET":       { MarionetteFormation_Preset(int.Parse(_names[3]));     }   break;
                case "FORMATION__TILE":         { MarionetteFormation_Tile(int.Parse(_names[3]));       }   break;
                case "FORMATION__MENU_BAR":     { MarionetteFormation_SelectMenu(int.Parse(_names[3])); }   break;
                case "FORMATION__SCROLL_VIEW":  { MarionetteFormation_ClickBtn(int.Parse(_names[3]));   }   break;
                //
                //case "SKIN_MENU_BAR":   { Marionette_SkinSelectMenu(int.Parse(_names[3]));  }   break;
            }
        }

        void Marionette_Close()
        {
            Marionette_ui.SetActive(false);

            //
            Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MASTER__BATTLE__VAR_COROUTINE_PHASE, Giggle_Battle.Basic__COROUTINE_PHASE.PLAYER_SETTING_START);
        }

        void Marionette_SelectMenu(int _count)
        {
            Marionette_menuBar.Basic_SelectMenu(_count);

            switch(_count)
            {
                case 0: { MarionetteFormation_Select(); }   break;
            }
        }

        //
        void Marionette_Active()
        {
            //
            Marionette_marionetteList
                = (List<Giggle_Character.Save>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.PLAYER__MARIONETTE__GET_LIST);

            Marionette_formationSelect
                = (int)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.PLAYER__FORMATION__GET_SELECT);

            Marionette_formationList
                = (List<Giggle_Player.Formation>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.PLAYER__FORMATION__GET_FORMATION_LIST);

            Marionette_SelectMenu(0);
            MarionetteFormation_SelectMenu(0);

            Marionette_ui.SetActive(true);
        }

        ////////// Constructor & Destroyer  //////////
        void Marionette_Init()
        {
            Marionette_ui.SetActive(false);
            Marionette_menuBar.Basic_Init();
            for(int for0 = 0; for0 < Marionette_menuBar.Basic_VarListCount; for0++)
            {
                Marionette_menuBar.Basic_GetListBtn(for0).name = "Button/MARIONETTE/MENU_BAR/" + for0.ToString();
            }

            MarionetteFormation_Init();
        }

            #region MARIONETTE_FORMATION

            [Serializable]
            public class MenuBar_MarionetteFormation : Giggle_UI.MenuBar
            {
                [SerializeField] Giggle_MainManager Basic_manager;

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
                    Basic_manager.UI_VarBasicData.MarionetteFormation_VarScrollView.Basic_SelectMenuBar(_count);
                }

                ////////// Constructor & Destroyer  //////////

            }

            [Serializable]
            public class ScrollView_MarionetteFormation : Giggle_UI.ScrollView
            {
                [SerializeField] Giggle_MainManager Basic_manager;

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

                    //
                    Basic_manager.UI_VarBasicData.MarionetteFormation_VarSelectMarionette = _count + 1;
                }

                // Basic_SelectMenuBar
                public void Basic_SelectMenuBar(int _count)
                {
                    switch(_count)
                    {
                        case 0: { Basic_SelectMenuBar__All();   }   break;
                        case 1: {   }   break;
                        case 2: {   }   break;
                        case 3: {   }   break;
                    }

                    Basic_ClickBtn(-1);
                }

                void Basic_SelectMenuBar__All()
                {
                    List<Giggle_Character.Save> characterList   = Basic_manager.UI_VarBasicData.Marionette_VarMarionetteList;
                    
                    Basic_SelectMenuBar__Check(characterList);
                }

                void Basic_SelectMenuBar__Check(List<Giggle_Character.Save> _characterList)
                {
                    int finalCount = 0;
                    int whileCount = 0;
                    while(whileCount < Basic_list.Count)
                    {
                        if(whileCount < _characterList.Count)
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
                            Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                                Giggle_ScriptBridge.EVENT.MASTER__UI__CHARACTER_INSTANTIATE,
                                //
                                _characterList[whileCount].Basic_VarDataId,
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
                    Giggle_Player.Formation     formation       = Basic_manager.UI_VarBasicData.Marionette_formationList[Basic_manager.UI_VarBasicData.Marionette_formationSelect];
                    List<Giggle_Character.Save> characterList   = Basic_manager.UI_VarBasicData.Marionette_VarMarionetteList;

                    int whileCount = 0;
                    while(whileCount < characterList.Count)
                    {
                        Basic_list[whileCount].Find("Cover").gameObject.SetActive(false);
                        for(int for0 = 0; for0 < formation.Basic_VarFormation.Count; for0++)
                        {
                            if( characterList[whileCount].Basic_VarInventoryId.Equals(
                                    formation.Basic_VarFormation[for0]))
                            {
                                Basic_list[whileCount].Find("Cover").gameObject.SetActive(true);
                                break;
                            }
                        }

                        //
                        whileCount++;
                    }
                }

                ////////// Constructor & Destroyer  //////////

            }
            
            [Header("FORMATION ==========")]
            // Area2
            [SerializeField] Transform       MarionetteFormation_formationParent;
            [SerializeField] List<Transform> MarionetteFormation_formationList;
            
            // Area3
            [SerializeField] MenuBar_MarionetteFormation    MarionetteFormation_menuBar;
            [SerializeField] ScrollView_MarionetteFormation MarionetteFormation_scrollView;

            [Header("RUNNING")]
            [SerializeField] int MarionetteFormation_selectFormation;
            [SerializeField] int MarionetteFormation_selectMarionette;

            ////////// Getter & Setter          //////////
            
            //
            public ScrollView_MarionetteFormation   MarionetteFormation_VarScrollView   { get { return MarionetteFormation_scrollView;  }   }
            
            //
            public int  Marionette_VarFormationSelect   { get { return Marionette_formationSelect; }   }

            //
            public List<Giggle_Player.Formation>    Marionette_VarFormationList { get { return Marionette_formationList;    }   }

            //
            public int  MarionetteFormation_VarSelectMarionette { set { MarionetteFormation_selectMarionette = value;   }   }

            ////////// Method                   //////////

            void MarionetteFormation_Select()
            {
                MarionetteFormation_Reset();
            }

            // MarionetteFormation_Preset
            void MarionetteFormation_Preset(int _count)
            {
                Marionette_formationSelect = _count;

                MarionetteFormation_Reset();
            }

            // MarionetteFormation_Tile
            void MarionetteFormation_Tile(int _count)
            {
                MarionetteFormation_selectFormation = _count;

                switch(MarionetteFormation_selectMarionette)
                {
                    case -1: { MarionetteFormation_Tile__Select(_count);    }   break;
                    default: { MarionetteFormation_Tile__Change(_count);    }   break;
                }
            }

            void MarionetteFormation_Tile__Select(int _count)
            {
                for (int for0 = 0; for0 < MarionetteFormation_formationList.Count; for0++)
                {
                    MarionetteFormation_formationList[for0].Find("Select").gameObject.SetActive(for0.Equals(_count));
                }

                //
                MarionetteFormation_selectFormation = _count;
                if(!_count.Equals(-1))
                {
                    MarionetteFormation_selectMarionette = Marionette_formationList[Marionette_formationSelect].Basic_VarFormation[_count];
                }

                // UI갱신
                //MarionetteFormation_Reset();
            }

            void MarionetteFormation_Tile__Change(int _count)
            {
                // 배치되어 있다면 비활성화
                for (int for0 = 0; for0 < MarionetteFormation_formationList.Count; for0++)
                {
                    if(Marionette_formationList[Marionette_formationSelect].Basic_VarFormation[for0].Equals(MarionetteFormation_selectMarionette))
                    {
                        Marionette_formationList[Marionette_formationSelect].Basic_VarFormation[for0] = -1;
                        break;
                    }
                }

                // 자리 갱신
                Marionette_formationList[Marionette_formationSelect].Basic_VarFormation[_count] = MarionetteFormation_selectMarionette;

                // UI갱신
                MarionetteFormation_Reset();
            }

            // MarionetteFormation_SelectMenu
            void MarionetteFormation_SelectMenu(int _count)
            {
                MarionetteFormation_menuBar.Basic_SelectMenu(_count);
            }

            void MarionetteFormation_ClickBtn(int _count)
            {
                MarionetteFormation_scrollView.Basic_ClickBtn(_count);
            }

            void MarionetteFormation_Reset()
            {
                MarionetteFormation_scrollView.Basic_ClickBtn(-1);
                MarionetteFormation_scrollView.Basic_CheckCover();
                MarionetteFormation_Tile(-1);

                // 타일 처리
                for(int for0 = 0; for0 < MarionetteFormation_formationList.Count; for0++)
                {
                    // 기존 마리오네트 모델링 날리기
                    while(MarionetteFormation_formationList[for0].Find("Obj").childCount > 0)
                    {
                        Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                            Giggle_ScriptBridge.EVENT.MASTER__GARBAGE__REMOVE,
                            MarionetteFormation_formationList[for0].Find("Obj").GetChild(0));
                    }

                    // 배치된 모델링이 있다면 모델링 배치
                    int tileId = Marionette_formationList[Marionette_formationSelect].Basic_VarFormation[for0];
                    if(!tileId.Equals(-1))
                    {
                        // 주인공
                        if(tileId.Equals(0))
                        {

                        }
                        else
                        {
                            tileId -= 1;
                            for(int for1 = 0; for1 < Marionette_marionetteList.Count; for1++)
                            {
                                if(Marionette_marionetteList[for1].Basic_VarInventoryId.Equals(tileId))
                                {
                                    Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                                        Giggle_ScriptBridge.EVENT.MASTER__UI__CHARACTER_INSTANTIATE,
                                        //
                                        Marionette_marionetteList[for1].Basic_VarDataId,
                                        MarionetteFormation_formationList[for0].Find("Obj"), -90.0f, 300.0f);
                                }
                            }
                        }
                    }
                }
            }
            
            ////////// Constructor & Destroyer  //////////
            void MarionetteFormation_Init()
            {
                // Area2
                if(MarionetteFormation_formationList == null)
                {
                    MarionetteFormation_formationList = new List<Transform>();
                }

                int whileCount = 0;
                while(whileCount < MarionetteFormation_formationParent.childCount)
                {
                    Transform for0Child = MarionetteFormation_formationParent.GetChild(whileCount);
                    for(int for0 = 0; for0 < for0Child.childCount; for0++)
                    {
                        MarionetteFormation_formationList.Add(for0Child.GetChild(for0));
                    }

                    whileCount++;
                }

                whileCount = 0;
                while(whileCount < MarionetteFormation_formationList.Count)
                {
                    for(int for0 = whileCount; for0 < MarionetteFormation_formationList.Count; for0++)
                    {
                        Transform element = MarionetteFormation_formationList[for0];
                        if(element.name.Equals(whileCount.ToString()))
                        {
                            element.Find("Button").name = "Button/MARIONETTE/FORMATION__TILE/" + whileCount;
                            MarionetteFormation_formationList.RemoveAt(for0);
                            MarionetteFormation_formationList.Insert(whileCount, element);
                        }
                    }

                    whileCount++;
                }

                // Area3
                MarionetteFormation_menuBar.Basic_Init();
                for(int for0 = 0; for0 < MarionetteFormation_menuBar.Basic_VarListCount; for0++)
                {
                    MarionetteFormation_menuBar.Basic_GetListBtn(for0).name = "Button/MARIONETTE/FORMATION__MENU_BAR/" + for0.ToString();
                }

                MarionetteFormation_scrollView.Basic_Init();
            }

            #endregion


            #region MARIONETTE_INFO
            
            [Header("INFO ==========")]
            [SerializeField] int MarionetteInfo_0;
            
            [Header("RUNNING")]
            [SerializeField] int MarionetteInfo_1;

            ////////// Getter & Setter          //////////

            ////////// Method                   //////////
            
            ////////// Constructor & Destroyer  //////////
            
            #endregion

        #endregion
    }

    [Serializable]
    public class UI_MainPopUpData : UI_PopUpData
    {
        public enum LIST
        {
            GACHA
        }

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////

        public override void Basic_Active(string _name)
        {
            base.Basic_Active(_name);

            switch(_name)
            {
                case "GACHA":   { Gacha_Reset();    }   break;
            }
        }

        ////////// Constructor & Destroyer  //////////
        public override void Basic_Init()
        {
            base.Basic_Init();

            Gacha_Init();
        }

        #region GACHA

        [Serializable]
        public class Gacha_List : IDisposable
        {
            [SerializeField] Giggle_Character.Database Basic_character;

            // Background
            [SerializeField] GameObject Basic_background;

            // Info
            [SerializeField] Transform              Basic_infoModel;
            [SerializeField] TMPro.TextMeshProUGUI  Basic_infoName;
            [SerializeField] TMPro.TextMeshProUGUI  Basic_infoType;
            [SerializeField] TMPro.TextMeshProUGUI  Basic_infoAttribute;

            // Status
            [SerializeField] List<TMPro.TextMeshProUGUI>    Basic_statusList;

            ////////// Getter & Setter          //////////
            public int  Basic_VarCharacterId    { get { return Basic_character.Basic_VarId; }   }

            public bool Basic_VarBackgroundActive   { set {Basic_background.SetActive(value);   }   }

            ////////// Method                   //////////
            public void Basic_Gacha(List<Giggle_Character.Database> _list)
            {
                Basic_background.SetActive(false);

                // TODO:나중에 가챠 조건이 생기면 여기에 기입합시다.
                Basic_character = _list[UnityEngine.Random.Range(0, _list.Count)];

                // UI갱신
                while(Basic_infoModel.childCount > 0)
                {
                    Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                        Giggle_ScriptBridge.EVENT.MASTER__GARBAGE__REMOVE,
                        //
                        Basic_infoModel.GetChild(0));
                }
                Giggle_Unit model = Instantiate(Basic_character.Basic_VarUnit, Basic_infoModel);
                Basic_Gacha__ChangeModelLayer(model.transform);
                model.transform.localPosition = Vector3.zero;
                model.transform.localRotation = quaternion.Euler(-90,0,0);
                model.transform.localScale = new Vector3(600, 600, 600);

                Basic_infoName.text      = Basic_character.Basic_VarName;
                Basic_infoType.text      = Basic_character.Marionette_VarRole.ToString();
                Basic_infoAttribute.text = Basic_character.Marionette_VarAttribute.ToString();

                Basic_statusList[0].text = Basic_character.Basic_GetStatusList(0).Basic_VarAttack.ToString();
                Basic_statusList[1].text = Basic_character.Basic_GetStatusList(0).Basic_VarDefence.ToString();
                Basic_statusList[2].text = Basic_character.Basic_GetStatusList(0).Basic_VarHp.ToString();
                Basic_statusList[3].text = Basic_character.Basic_GetStatusList(0).Basic_VarAttackSpeed.ToString();
            }

            void Basic_Gacha__ChangeModelLayer(Transform _parent)
            {
                _parent.gameObject.layer = 5;

                for(int for0 = 0; for0 < _parent.childCount; for0++)
                {
                    Basic_Gacha__ChangeModelLayer(_parent.GetChild(for0));
                }
            }

            ////////// Constructor & Destroyer  //////////
            public Gacha_List(Transform _trans)
            {
                Basic_background = _trans.Find("Back").gameObject;

                // Info
                Transform element = _trans.Find("Data").Find("Info");
                Basic_infoModel     = element.Find("Model");
                Basic_infoName      = element.Find("Text (TMP)_Name").GetComponent<TMPro.TextMeshProUGUI>();
                Basic_infoType      = element.Find("Text (TMP)_Type").GetComponent<TMPro.TextMeshProUGUI>();
                Basic_infoAttribute = element.Find("Text (TMP)_Attribute").GetComponent<TMPro.TextMeshProUGUI>();

                // Status
                if(Basic_statusList == null)
                {
                    Basic_statusList = new List<TMPro.TextMeshProUGUI>();
                }

                int whileCount = 0;
                while(whileCount < 4)
                {
                    Basic_statusList.Add(null);
                    whileCount++;
                }

                element = _trans.Find("Data").Find("Status");
                whileCount = 0;
                while(whileCount < 4)
                {
                    Basic_statusList[whileCount] = element.Find(whileCount.ToString()).Find("Text (TMP) (1)").GetComponent<TMPro.TextMeshProUGUI>();
                    whileCount++;
                }
            }

            public void Dispose()
            {

            }
        }

        [Header("GACHA ==================================================")]
        [SerializeField] List<Gacha_List>   Gacha_list;
        //
        [SerializeField] GameObject             Gacha_changeBtn;
        [SerializeField] TMPro.TextMeshProUGUI  Gacha_changeText;
        //
        [SerializeField] GameObject Gacha_selectBtn;

        [Header("RUNNING")]
        [SerializeField] int Gacha_gachaCount;
        [SerializeField] int Gacha_select;

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////
        public void Gacha_BtnClick(string[] _names)
        {
            switch(_names[2])
            {
                case "CHARACTER":   { Gacha_BtnClick__CHARACTER(_names[3]); }   break;
                case "CHANGE":      { Gacha_BtnClick__CHANGE();             }   break;
                case "SELECT":      { Gacha_BtnClick__SELECT();             }   break;
            }
        }

        void Gacha_BtnClick__CHARACTER(string _num)
        {
            Gacha_select = int.Parse(_num);
            Gacha_selectBtn.SetActive(true);

            //
            for(int for0 = 0; for0 < Gacha_list.Count; for0++)
            {
                Gacha_list[for0].Basic_VarBackgroundActive = Gacha_select.Equals(for0);
            }
        }

        void Gacha_BtnClick__CHANGE()
        {
            Gacha_Gacha();
        }

        void Gacha_BtnClick__SELECT()
        {
            Basic_parent.gameObject.SetActive(false);

            // TODO:스킬도 넣어줘야 합니다!
            Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.PLAYER__MARIONETTE__ADD,
                //
                Gacha_list[Gacha_select].Basic_VarCharacterId);
        }
        
        //
        void Gacha_Reset()
        {
            //
            Gacha_changeBtn.SetActive(true);

            //
            Gacha_gachaCount = 4;
            Gacha_Gacha();
        }

        void Gacha_Gacha()
        {
            Gacha_select = -1;
            Gacha_selectBtn.SetActive(false);

            // 가챠 리스트 불러오기
            List<Giggle_Character.Database> list
                = (List<Giggle_Character.Database>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.DATABASE__CHARACTER__GET_DATAS_FROM_ATTRIBUTE,
                    Giggle_Master.ATTRIBUTE.FIRE.ToString());
            
            for(int for0 = 0; for0 < Gacha_list.Count; for0++)
            {
                Gacha_list[for0].Basic_Gacha(list);
            }

            // 가챠 교환횟수 차감 및 텍스트 갱신
            Gacha_gachaCount--;

            Gacha_changeText.text = "Change (" + Gacha_gachaCount + "/3)";

            // 가챠 교환횟수가 0이라면 버튼 비활성화
            if(Gacha_gachaCount <= 0)
            {
                Gacha_changeBtn.SetActive(false);
            }
        }

        ////////// Constructor & Destroyer  //////////
        void Gacha_Init()
        {
            Transform main = Basic_parent.Find("GACHA");

            // List
            if(Gacha_list == null)
            {
                Gacha_list = new List<Gacha_List>();
            }

            Transform list = main.Find("List");
            int whileCount = 0;
            while(whileCount < list.childCount)
            {
                Gacha_list.Add(null);
                whileCount++;
            }

            for(int for0 = 0; for0 < list.childCount; for0++)
            {
                Transform forTrans = list.GetChild(for0);

                int count = int.Parse(forTrans.name);

                Gacha_List element = new Gacha_List(forTrans);
                Gacha_list[count] = element;
            }
        }

        #endregion
    }

    [Header("UI ==================================================")]
    [SerializeField] UI_MainBasicData   UI_basicData;
    [SerializeField] UI_MainPopUpData   UI_popUpData;

    ////////// Getter & Setter  //////////
    public UI_MainBasicData UI_VarBasicData { get { return UI_basicData;    }   }

    ////////// Method           //////////
    // UI_basicData
    public override void UI_BtnClick(GameObject _btn)
    {
        string[] names = _btn.name.Split('/');
        switch(names[1])
        {
            case "AREA4":   { UI_basicData.Area4_BtnClick(names);   }   break;
            case "AREA7":   { UI_basicData.Area7_BtnClick(names);   }   break;
            //
            case "PINOCCHIO":   { UI_basicData.Pinocchio_VarData.Basic_BtnClick(names); }   break;
            case "MARIONETTE":  { UI_basicData.Marionette_BtnClick(names);  }   break;
        }
    }
    
    // UI_popUpData
    public override void UI_PopUpBtnClick(GameObject _btn)
    {
        string[] names = _btn.name.Split('/');
        switch(names[1])
        {
            case "GACHA":   { UI_popUpData.Gacha_BtnClick(names);   }   break;
        }
    }

    public void UI_PopUpActive(string _name)
    {
        UI_popUpData.Basic_Active(_name);
    }

    ////////// Unity            //////////
    protected override void UI_Start()
    {
        UI_basicData.Basic_Init();
        UI_popUpData.Basic_Init();
    }

    #endregion

    #region 

    ////////// Getter & Setter  //////////

    ////////// Method           //////////

    ////////// Unity            //////////

    #endregion
}