using UnityEngine;

//
using System;
using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;

public class Giggle_DungeonManager : Giggle_SceneManager
{

    #region BASIC

    //[Header("BASIC ==================================================")]

    ////////// Getter & Setter  //////////

    ////////// Method           //////////
    public override void Basic_Active(bool _isActive)
    {
        base.Basic_Active(_isActive);
        
        //
        if(_isActive)
        {
            Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MAIN__POWER_SAVING__ON_OFF, true);
            UI_Reset();
        }
    }

    ////////// Unity            //////////

    protected override void Basic_Start()
    {
        UI_Start();
        Battle_Start();
    }

    #endregion

    #region UI

    [Serializable]
    public partial class UI_MainBasicData : UI_BasicData
    {
        #region BASIC

        [SerializeField] Giggle_DungeonManager  Basic_manager;

        [SerializeField] List<RectTransform>    Basic_safeAreas;

        IEnumerator Basic_coroutine;

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////

        // Basic_BtnClick
        public void Basic_BtnClick(string[] _names)
        {
            switch(_names[2])
            {
                case "CLOSE":   { Basic_BtnClick__Close();  }   break;
            }
        }

        void Basic_BtnClick__Close()
        {
            Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.SCENE__LOAD_SCENE,
                //
                Giggle_Master.Scene_TYPE.MAIN);
        }

        //
        public void Basic_Reset()
        {
            Select_parent.SetActive(true);

            Waitting_parent.SetActive(false);
            Formation_parent.SetActive(false);
            Battle_parent.SetActive(false);
            Basic_manager.Battle_Active(false);
        }

        ////////// Constructor & Destroyer  //////////
        
        public override void Basic_Init()
        {
            base.Basic_Init();

            //
            Basic_coroutine = Basic_Coroutine();
            Basic_manager.StartCoroutine(Basic_coroutine);
        }

        // Basic_Coroutine
        protected override IEnumerator Basic_Coroutine()
        {
            int phase = 0;

            while(phase != -1)
            {
                switch(phase)
                {
                    //
                    case 0: { if(Basic_Coroutine__Canvas()) { phase = 1;    }   }   break;
                    case 1: { Basic_Coroutine__UI(  ref phase   );              }   break;
                    case 2: { Basic_Coroutine__ETC(  ref phase   );             }   break;
                }

                yield return null;
            }
        }

        void Basic_Coroutine__UI(ref int _phase)
        {
            if(
                Giggle_ScriptBridge.Basic_VarInstance.Basic_GetIsInMethod(
                    Giggle_ScriptBridge.EVENT.MASTER__UI__SAFE_AREA_VAR_SIZE_DELTA))
            {
                for(int for0 = 0; for0 < Basic_safeAreas.Count; for0++)
                {
                    Basic_safeAreas[for0].sizeDelta     = (Vector2)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MASTER__UI__SAFE_AREA_VAR_SIZE_DELTA );
                    Basic_safeAreas[for0].localPosition = (Vector2)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MASTER__UI__SAFE_AREA_VAR_POSITION   );
                }

                //
                _phase = 2;
            }
        }

        void Basic_Coroutine__ETC(ref int _phase)
        {
            //
            Formation_Init();

            //
            _phase = -1;
        }

        #endregion

        #region SELECT

        [Header("SELECT ==================================================")]
        [SerializeField] GameObject Select_parent;

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////

        public void Select_BtnClick(string[] _names)
        {
            switch(_names[2])
            {
                case "DUNGEON_GOLD":    { Select_BtnClick__DungeonGold();   }   break;
            }
        }

        void Select_BtnClick__DungeonGold()
        {
            Formation_formation.Basic_VarPlayerType = Giggle_UI.Formation.Basic__PLAY_TYPE.PVE;

            //
            Select_parent.SetActive(false);
            Formation_Active();
        }

        ////////// Constructor & Destroyer  //////////
        
        #endregion

        #region WAITTING

        [Header("WAITTING ==================================================")]
        [SerializeField] GameObject Waitting_parent;

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////

        public void Waitting_BtnClick(string[] _names)
        {
            switch(_names[2])
            {
                case "":    {   }   break;
            }
        }

        ////////// Constructor & Destroyer  //////////
        
        #endregion

        #region FORMATION

        [Serializable]
        public class Formation_Formation : Giggle_UI.Formation
        {


            [Header("==================================================")]
            [Header("BASIC ==================================================")]
            [SerializeField] UI_MainBasicData Basic_manager;

            ////////// Getter & Setter          //////////

            ////////// Method                   //////////

            //
            protected override void Basic_Reset__PVE()
            {
                List<int> formation = (List<int>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.PLAYER__DUNGEON__GET_FORMATION);
                for(int for0 = 0; for0 < formation.Count; for0++)
                {
                    Basic_datas[for0] = formation[for0];
                }
                Basic_manager.Formation_Reset();

            }

            //
            public void Basic_Init(UI_MainBasicData _manager)
            {
                Basic_manager = _manager;
                UI_rawImage.transform.position = Basic_manager.Basic_GetCanvas(0).transform.position;

                Basic_Init();
            }

            protected override void UI_Init__Btn(Transform _element, int _count)
            {
                _element.Find("Button").name = "Button/FORMATION/TILE/" + _count;
            }

            ////////// Constructor & Destroyer  //////////
            
            #region UI

            ////////// Getter & Setter          //////////

            ////////// Method                   //////////

            ////////// Constructor & Destroyer  //////////

            // UI_BtnClick__Tile__Tile
            protected override bool UI_BtnClick__Tile__Tile__Change(int _count)
            {
                bool isChange = false;

                //
                List<int> formation = (List<int>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.PLAYER__DUNGEON__GET_FORMATION);

                if(!formation[_count].Equals(-2))
                {
                    //
                    Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                        Giggle_ScriptBridge.EVENT.PLAYER__DUNGEON__FORMATION_SETTING,
                        formation[UI_selectFormation], _count);

                    // UI갱신
                    Basic_Reset();

                    isChange = true;
                }

                //
                return isChange;
            }

            // UI_BtnClick__Tile__Change
            protected override bool UI_BtnClick__Tile__Change(int _count, int _selectMarionette)
            {
                bool isChange = false;

                //
                List<int> formation = (List<int>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.PLAYER__DUNGEON__GET_FORMATION);

                if(!formation[_count].Equals(-2))
                {
                    Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                        Giggle_ScriptBridge.EVENT.PLAYER__DUNGEON__FORMATION_SETTING,
                        _selectMarionette, _count);

                    // UI갱신
                    Basic_Reset();

                    isChange = true;
                }

                //
                return isChange;
            }

            #endregion
        }

        [Serializable]
        public class ScrollerView_MenuBar__Formation : Giggle_UI.MenuBar
        {
            [SerializeField] UI_MainBasicData Basic_uiData;

            ////////// Getter & Setter          //////////

            ////////// Method                   //////////

            protected override void Basic_SelectMenu__Setting(int _for0, int _count)
            {
                if(_for0.Equals(_count))
                {
                    Basic_uiData.Formation_VarScrollView.Basic_SelectMenuBar(_count);
                }
            }

            ////////// Constructor & Destroyer  //////////
            public void Basic_Init(UI_MainBasicData _uiData)
            {
                Basic_uiData = _uiData;
                Basic_Init("FORMATION", "MENU_BAR");
            }
        }

        [Serializable]
        public class ScrollView_Formation : Giggle_UI.ScrollView
        {
            [SerializeField] UI_MainBasicData Basic_uiData;

            ////////// Getter & Setter          //////////

            ////////// Method                   //////////

            protected override void Basic_AddList__SetName(Transform _element, int _num)
            {
                _element.Find("Button").name = "Button/FORMATION/SCROLL_VIEW/" + _num;
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
                            Basic_list[whileCount].Find("Obj"), 300.0f);
                        
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
                List<int>   formation       = (List<int>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.PLAYER__DUNGEON__GET_FORMATION);
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
            public void Basic_Init(UI_MainBasicData _uiData)
            {
                Basic_Init();
                Basic_uiData = _uiData;
                Basic_rawImage.transform.position = Basic_uiData.Basic_GetCanvas(0).transform.position;
            }

            ////////// Constructor & Destroyer  //////////

        }

        [Header("FORMATION ==================================================")]
        [SerializeField] GameObject Formation_parent;

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

        // Formation_BtnClick   //////////
        public void Formation_BtnClick(string[] _names)
        {
            switch(_names[2])
            {
                case "TILE":        { Formation_BtnClick__Tile(         int.Parse(_names[3]));  }   break;
                case "FMENU_BAR":   { Formation_BtnClick__MenuBar(      int.Parse(_names[3]));  }   break;
                case "SCROLL_VIEW": { Formation_BtnClick__ScrollView(   int.Parse(_names[3]));  }   break;
                case "GAME_START":  { Formation_BtnClick__GameStart();                          }   break;
            }
        }

        // Formation_BtnClick__Tile
        void Formation_BtnClick__Tile(int _count)
        {
            int selectMarionette = -1;
            if(Formation_selectMarionette != -1)    { selectMarionette = Formation_marionetteList[Formation_selectMarionette];  }

            Formation_formation.UI_BtnClick__Tile(_count, selectMarionette);
        }

        // Formation_BtnClick__MenuBar
        void Formation_BtnClick__MenuBar(int _count)
        {
            Formation_scrollViewMenuBar.Basic_SelectMenu(_count);
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

        // Formation_BtnClick__GameStart
        void Formation_BtnClick__GameStart()
        {
            switch(Formation_formation.Basic_VarPlayerType)
            {
                case Giggle_UI.Formation.Basic__PLAY_TYPE.PVE:  { Basic_manager.Battle_FormationSettingPve();   }   break;
            }

            Formation_parent.SetActive(false);
            Battle_parent.SetActive(true);
            Basic_manager.Battle_Active(true);
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

        // Formation_Active //////////
        void Formation_Active()
        {
            //
            Formation_Reset();
            Formation_BtnClick__MenuBar(0);
            Formation_formation.Basic_Reset();

            //
            Formation_parent.SetActive(true);
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

        // Formation_Init   //////////
        void Formation_Init()
        {
            Formation_formation.Basic_Init(this);
            Formation_scrollView.Basic_Init(this);
            Formation_scrollViewMenuBar.Basic_Init(this);
        }

        ////////// Constructor & Destroyer  //////////
        
        #endregion

        #region BATTLE

        [Header("BATTLE ==================================================")]
        [SerializeField] GameObject Battle_parent;

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////
        
        ////////// Constructor & Destroyer  //////////
        
        #endregion
    }

    [Header("UI ==================================================")]
    [SerializeField] UI_MainBasicData   UI_basicData;

    ////////// Getter & Setter  //////////

    ////////// Method           //////////
    // UI_BtnClick
    public override void UI_BtnClick(GameObject _btn)
    {
        string[] names = _btn.name.Split('/');
        switch(names[1])
        {
            case "BASIC":       { UI_basicData.Basic_BtnClick(names);       }   break;
            case "SELECT":      { UI_basicData.Select_BtnClick(names);      }   break;
            case "WAITTING":    { UI_basicData.Waitting_BtnClick(names);    }   break;
            case "FORMATION":   { UI_basicData.Formation_BtnClick(names);   }   break;
        }
    }

    //
    void UI_Reset()
    {
        UI_basicData.Basic_Reset();
    }

    ////////// Unity            //////////
    protected override void UI_Start()
    {
        UI_basicData.Basic_Init();
    }

    #endregion

    #region BATTLE

    [Serializable]
    public class Battle_MainBasicData : Giggle_Battle
    {
        [Header("==================================================")]
        [Header("Battle_MainBasicData")]
        [Header("BASIC ==================================================")]
        [SerializeField] Giggle_DungeonManager  Basic_manager;

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////

        // Basic_Coroutine ====================

        // INIT ==========

        // RESET ==========
        protected override void Basic_Coroutine__RESET()
        {
            Basic_coroutinePhase = Basic__COROUTINE_PHASE.SETTING_STAGE_START;
        }

        // SETTING_STAGE ==========
        // SETTING_STAGE
        protected override void Basic_Coroutine__SETTING_STAGE()
        {
            GameObject obj = (GameObject)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.DATABASE__STAGE__GET_OBJ_FROM_ID,
                //
                01);

            Map_Load(obj);

            //
            Basic_coroutinePhase = Basic__COROUTINE_PHASE.SETTING_OBJECT;
        }

        // SETTING_PLAYER ==========
        // SETTING_PLAYER_START
        protected override void Basic_Coroutine__SETTING_PLAYER_START()
        {
            Basic_coroutinePhase = Basic__COROUTINE_PHASE.SETTING_PLAYER;
        }

        // PLAYER_SETTING
        protected override void Basic_Coroutine__SETTING_PLAYER()
        {
            Formation_Ally.Basic_Reset();

            // 캐릭터 생성
            Giggle_Character.Save save = null;
            Giggle_Character.Database data = null;
            Giggle_Unit unit = null;

            // player
            // 기존 데이터 날리기
            int whileCount = 0;
            while(whileCount < Basic_manager.Battle_VarFormationCount)
            {
                //
                int formationId = Basic_manager.Battle_GetFormation(whileCount).Basic_VarDataId;
                if(!formationId.Equals(-1))
                {
                    switch(formationId)
                    {
                        case -2:
                            {
                                save
                                    = (Giggle_Character.Save)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                                        Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__VAR_DATA);
                                data = (Giggle_Character.Database)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                                    Giggle_ScriptBridge.EVENT.DATABASE__PINOCCHIO__GET_DATA_FROM_ID,
                                    //
                                    save.Basic_VarDataId);
                            }
                            break;
                        default:
                            {
                                save = Basic_manager.Battle_GetFormation(whileCount);
                                data = (Giggle_Character.Database)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                                    Giggle_ScriptBridge.EVENT.DATABASE__MARIONETTE__GET_DATA_FROM_ID,
                                    //
                                    formationId);
                            }
                            break;
                    }
                    
                    unit = GameObject.Instantiate(data.Basic_VarUnit, Formation_Ally.Basic_GetTile(whileCount));
                    unit.Basic_Init(this, save);
                }

                whileCount++;
            }

            Basic_coroutinePhase = Basic__COROUTINE_PHASE.SETTING_ENEMY_START;
        }


        // ENEMY_SETTING ==========

        // STAGE_DATA_CHECK

        // SETTING_ENEMY

        // SETTING_POWER_SAVING ==========
        protected override void Basic_Coroutine__SETTING_POWER_SAVING()
        {

            // 유닛 활성화
            Formation_Ally.Basic_VarParent.gameObject.SetActive(true);
            Formation_Enemy.Basic_VarParent.gameObject.SetActive(true);

            //
            Basic_coroutinePhase = Basic__COROUTINE_PHASE.FADE_IN_START;
        }

        // ACTIVE_RESULT ==========

        // ACTIVE_RESULT_WIN
        protected override void Basic_Coroutine__ACTIVE_RESULT_WIN()
        {
            ////
            //Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
            //    Giggle_ScriptBridge.EVENT.MAIN__BATTLE__VAR_WIN_ACTIVE,
            //    //
            //    true);
//
            ////
            //bool isNext = (bool)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.PLAYER__STAGE__VAR_IS_NEXT);
            //
            //if(isNext)
            //{
            //    // 클리어한 스테이지의 정보 가져오기
            //    int stageId = (int)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.PLAYER__STAGE__VAR_SELECT);
//
            //    Giggle_Stage.Stage stage = (Giggle_Stage.Stage)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
            //        Giggle_ScriptBridge.EVENT.DATABASE__STAGE__GET_STAGE_FROM_ID,
            //        stageId);
//
            //    // 다음 스테이지로 넘기기
            //    Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.PLAYER__STAGE__NEXT);
//
            //    // 스테이지가 보스라면 배경까지 셋팅한다.
            //    if(stage.Basic_VarIsBoss)
            //    {
            //        Basic_coroutinePhase = Giggle_Battle.Basic__COROUTINE_PHASE.RESET;
            //    }
            //    // 유닛만 셋팅한다.
            //    else
            //    {
            //        Basic_coroutinePhase = Basic__COROUTINE_PHASE.SETTING_OBJECT;
            //    }
            //}
            //else
            //{
            //    Basic_coroutinePhase = Basic__COROUTINE_PHASE.SETTING_OBJECT;
            //}
        }

        protected override void Basic_Coroutine__ACTIVE_RESULT_WIN_DOING(float _timer)
        {

        }

        // ACTIVE_RESULT_LOSE
        protected override void Basic_Coroutine__ACTIVE_RESULT_LOSE()
        {

            Basic_coroutinePhase = Basic__COROUTINE_PHASE.ACTIVE_RESULT_LOSE_DOING;
        }

        protected override void Basic_Coroutine__ACTIVE_RESULT_DOING()
        {
            Basic_parent.SetActive(false);

            Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.SCENE__LOAD_SCENE,
                //
                Giggle_Master.Scene_TYPE.MAIN);
        }

        ////////// Constructor & Destroyer  //////////
    
    }

    [Header("BATTLE ==================================================")]
    [SerializeField] Battle_MainBasicData           Battle_basicData;

    [SerializeField] List<Giggle_Character.Save>    Battle_formations;
    [SerializeField] int                            Battle_dungeonId;

    ////////// Getter & Setter  //////////
    // Battle_formations
    public Giggle_Character.Save    Battle_GetFormation(int _count) { return Battle_formations[_count]; }
    public int                      Battle_VarFormationCount        { get { return Battle_formations.Count; }   }

    ////////// Method           //////////

    public void Battle_Active(bool _isActive)
    {
        Battle_basicData.Basic_VarParentActive = _isActive;
        Battle_basicData.Basic_VarCoroutinePhase = Giggle_Battle.Basic__COROUTINE_PHASE.RESET;
    }

    public void Battle_FormationSettingPve()
    {
        List<int> formation
            = (List<int>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.PLAYER__DUNGEON__GET_FORMATION);

        //
        List<Giggle_Character.Save> marionetteList
                = (List<Giggle_Character.Save>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.PLAYER__MARIONETTE__GET_LIST);

        //
        for(int for0 = 0; for0 < Battle_formations.Count; for0++)
        {
            if(formation[for0] != -1)
            {
                if(formation[for0] == -2)
                {
                    Battle_formations[for0].Basic_VarDataId = -2;
                }
                else
                {
                    Battle_formations[for0].Basic_VarDataId     = marionetteList[formation[for0]].Basic_VarDataId;
                    Battle_formations[for0].Basic_VarLevel      = marionetteList[formation[for0]].Basic_VarLevel;
                    Battle_formations[for0].Basic_VarSkillLv    = marionetteList[formation[for0]].Basic_VarSkillLv;
                }
            }
        }
    }

    ////////// Unity            //////////

    void Battle_Start()
    {
        if(Battle_formations == null)
        {
            Battle_formations = new List<Giggle_Character.Save>();
        }
        while(Battle_formations.Count < 9)
        {
            Battle_formations.Add(new Giggle_Character.Save(-1));
        }

        Battle_basicData.Basic_Init();
    }

    #endregion
}
