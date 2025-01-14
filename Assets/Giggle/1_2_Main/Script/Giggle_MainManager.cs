using UnityEngine;

//
using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using Unity.Mathematics;
using TMPro;

public partial class Giggle_MainManager : Giggle_SceneManager
{

    #region BASIC

    ////////// Getter & Setter  //////////

    ////////// Method           //////////
    public override void Basic_Active(bool _isActive)
    {
        base.Basic_Active(_isActive);
        
        //
        UI_basicData.PowerSaving_OnOff(!_isActive);
    }


    ////////// Unity            //////////

    protected override void Basic_Start()
    {
        Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MASTER__BATTLE__INIT);

        //
        UI_Start();
    }

    #endregion

    #region UI

    [Serializable]
    public partial class UI_MainBasicData : UI_BasicData
    {
        [Header("BASIC ==================================================")]
        [SerializeField] Giggle_MainManager Basic_manager;
        [SerializeField] Giggle_MainManager__Pinocchio  Basic_pinocchioManager;
        [SerializeField] Giggle_MainManager__Marionette Basic_marionetteManager;
        [SerializeField] Giggle_MainManager__BlackSmith Basic_blackSmithManager;

        [SerializeField] RectTransform  Basic_safeArea;

        [Header("RUNNING")]
        IEnumerator             Basic_coroutine;
        [SerializeField] int    Basic_coroutinePhase;

        ////////// Getter & Setter          //////////

        object Basic_VarCoroutinePhase(params object[] _args)
        {
            //
            return Basic_coroutinePhase;
        }

        ////////// Method                   //////////

        ////////// Constructor & Destroyer  //////////
        public override void Basic_Init()
        {
            base.Basic_Init();

            Basic_safeArea.sizeDelta        = (Vector2)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MASTER__UI__SAFE_AREA_VAR_SIZE_DELTA );
            Basic_safeArea.localPosition    = (Vector2)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MASTER__UI__SAFE_AREA_VAR_POSITION   );

            Area3_Init();
            Area4_Init();
            Area5_Init();
            PowerSaving_Init();
            Battle_Init();

            //
            Basic_coroutine = Basic_Coroutine();
            Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MASTER__BASIC__START_COROUTINE, Basic_coroutine );

            //
            Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.MAIN__UI__VAR_COROUTINE_PHASE,  Basic_VarCoroutinePhase );
        }

        // Basic_Coroutine
        protected override IEnumerator Basic_Coroutine()
        {
            Basic_coroutinePhase = 0;

            float time = Time.time;
            float lastTime = Time.time;

            float stageSpeed = (float)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.PLAYER__STAGE__VAR_SPEED);

            while(Basic_coroutinePhase != -1)
            {
                time = Time.time;

                stageSpeed = (float)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.PLAYER__STAGE__VAR_SPEED);

                switch(Basic_coroutinePhase / 10000)
                {
                    case 0: { Basic_Coroutine__Basic();                         }   break;
                    case 1: { Battle_Coroutine((time - lastTime) * stageSpeed); }   break;
                }

                lastTime = time;

                yield return null;
            }
        }

        void Basic_Coroutine__Basic()
        {
            if(Basic_Coroutine__Canvas())
            {
                Area3_Coroutine();
                Basic_coroutinePhase = 1;
            }
        }

        #region AREA1

        [Header("AREA1 ==================================================")]
        [SerializeField] Image  Area1_portrait;
        [SerializeField] TextMeshProUGUI    Area1_level;
        [SerializeField] Slider             Area1_levelSlider;
        [SerializeField] TextMeshProUGUI    Area1_name;
        [SerializeField] TextMeshProUGUI    Area1_power;

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////

        ////////// Constructor & Destroyer  //////////

        #endregion

        #region AREA2

        [Header("AREA2 ==================================================")]
        [SerializeField] TextMeshProUGUI    Area2_gold;
        [SerializeField] TextMeshProUGUI    Area2_dia;

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////

        ////////// Constructor & Destroyer  //////////

        #endregion

        #region AREA3

        [Header("AREA3 ==================================================")]
        [SerializeField] TextMeshProUGUI    Area3_stage;
        [SerializeField] Slider             Area3_stageSlider;

        [SerializeField] List<Transform>    Area3_accelUis;
        [SerializeField] TextMeshProUGUI    Area3_accelText;

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////

        public void Area3_BtnClick(string[] _names)
        {
            switch(_names[2])
            {
                case "ACCEL":   { Area3_Accel();    }   break;
            }
        }

        void Area3_Accel()
        {
            Basic_manager.UI_PopUpActive("ACCEL");
        }

        // Area3_AccelDoing
        public void Area3_AccelDoing(bool _isActive)
        {
            Area3_accelUis[0].gameObject.SetActive(!_isActive);
            Area3_accelUis[1].gameObject.SetActive(_isActive);

            if(_isActive)
            {
                Area3_Coroutine();
            }
        }

        ////////// Constructor & Destroyer  //////////

        void Area3_Init()
        {
            //
            Transform parent = Area3_accelUis[0];

            while(Area3_accelUis.Count < parent.childCount)
            {
                Area3_accelUis.Add(null);
            }

            for(int for0 = 0; for0 < parent.childCount; for0++)
            {
                Area3_accelUis[for0] = parent.GetChild(for0);
            }
        }

        //
        void Area3_Coroutine()
        {
            if(Area3_accelUis[1].gameObject.activeSelf)
            {
                int timer = (int)((float)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.PLAYER__STAGE__VAR_SPEED_TIMER));

                if(timer <= 0)
                {
                    Area3_AccelDoing(false);
                }
                else
                {
                    int timer0 = timer / 60;
                    Debug.Log(timer0);
                    int timer1 = timer % 60;
                    Area3_accelText.text = (timer0 / 10).ToString() + (timer0 % 10).ToString() + " : " + (timer1 / 10).ToString() + (timer1 % 10).ToString();
                }
            }
        }

        #endregion
        
        #region AREA4

        [Serializable]
        public class Area4_Inventory__ScrollView : Giggle_UI.ScrollView
        {
            [SerializeField] UI_MainBasicData   Basic_parentClass;
            [SerializeField] Transform          Basic_rawParent;

            ////////// Getter & Setter          //////////

            ////////// Method                   //////////

            //
            public void Basic_Init(UI_MainBasicData _parentClass)
            {
                Basic_parentClass = _parentClass;

                //
                Basic_Init();
            }

            protected override void Basic_Init__SetName()
            {
                Basic_list[0].Find("Button").name = "Button/AREA4/SCROLL_VIEW/0";
                Basic_list[1].Find("Button").name = "Button/AREA4/SCROLL_VIEW/1";
            }

            protected override void Basic_AddList__SetName(Transform _element)
            {
                _element.Find("Button").name = "Button/AREA4/SCROLL_VIEW/" + Basic_list.Count;
            }

            //
            public override void Basic_ClickBtn(int _count)
            {
                //
                if(!_count.Equals(-1))
                {
                    Giggle_Character.Save pinocchioData
                        = (Giggle_Character.Save)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                            Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__VAR_DATA);

                    //
                    //pinocchioData.Basic_VarDataId = Basic_parentClass.Job_VarJobList[_count];
                    //Basic_parentClass.Job_InfoSetting();
                    Basic_CheckCover();
                }
            }

            // Basic_SelectMenuBar
            public void Basic_SelectMenuBar(int _count)
            {
                //Basic_parentClass.Job_ListSetting(_count);
                Basic_SelectMenuBar__Check();

                Basic_ClickBtn(-1);
            }

            void Basic_SelectMenuBar__Check()
            {
                int finalCount = 0;
                int whileCount = 0;
                while(whileCount < Basic_list.Count)
                {
                    if(whileCount < Basic_parentClass.Area4_VarInventoryDatas.Count)
                    {
                        Basic_list[whileCount].gameObject.SetActive(true);
                        
                        //
                        Giggle_Item.Inventory item
                            = (Giggle_Item.Inventory)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                                Giggle_ScriptBridge.EVENT.PLAYER__ITEM__GET_ITEM_FROM_INVENTORY_ID,
                                //
                                Basic_parentClass.Area4_VarInventoryDatas[whileCount]);

                        Giggle_Item.List data
                            = (Giggle_Item.List)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                                Giggle_ScriptBridge.EVENT.DATABASE__ITEM__GET_DATA_FROM_ID,
                                //
                                item.Basic_VarDataId);
                        
                        //Basic_list[whileCount].Find("Portrait").GetComponent<Image>().sprite = data.
                        Basic_list[whileCount].Find("Name").GetComponent<TextMeshProUGUI>().text = data.Basic_VarName;
                        Basic_list[whileCount].Find("Value").GetComponent<TextMeshProUGUI>().text = item.Basic_VarCount + " / ";
                        
                        //
                        finalCount = whileCount;
                    }
                    else
                    {
                        Basic_list[whileCount].gameObject.SetActive(false);
                    }

                    
                    whileCount++;
                }

                Basic_content.sizeDelta
                    = new Vector2(
                        0,
                        (Basic_list[finalCount].GetComponent<RectTransform>().sizeDelta.y * 0.5f) - Basic_list[finalCount].localPosition.y);

                Basic_CheckCover();
            }

            //
            void Basic_CheckCover()
            {
                //Giggle_Character.Save pinocchioData
                //    = (Giggle_Character.Save)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                //        Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__VAR_DATA);
                //
                //List<int> jobList = (List<int>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__VAR_JOBS);
                //
                //int whileCount = 0;
                //while(whileCount < jobList.Count)
                //{
                //    Basic_list[whileCount].Find("Cover").gameObject.SetActive(false);
                //    if(jobList[whileCount].Equals(pinocchioData.Basic_VarDataId))
                //    {
                //        Basic_list[whileCount].Find("Cover").gameObject.SetActive(true);
                //    }
                //
                //    //
                //    whileCount++;
                //}
            }

            ////////// Constructor & Destroyer  //////////
        }

        [Header("AREA4 ==================================================")]
        [SerializeField] GameObject Area4_exMenu;

        [SerializeField] GameObject                     Area4_inventory;
        [SerializeField] Area4_Inventory__ScrollView    Area4_inventory__scrollView;
        [SerializeField] List<int>                      Area4_inventoryDatas;
        IEnumerator Area4_inventoryCoroutine;

        ////////// Getter & Setter          //////////
        List<int> Area4_VarInventoryDatas   { get { return Area4_inventoryDatas;    }   }

        ////////// Method                   //////////

        // Area4_BtnClick
        public void Area4_BtnClick(string[] _names)
        {
            switch(_names[2])
            {
                case "EXMENU__OPEN":            { Area4_exMenu.SetActive(true);     }   break;
                case "EXMENU__CLOSE":           { Area4_exMenu.SetActive(false);    }   break;
                case "EXMENU__BLACK_SMITH":     { Area4_BtnClick__BlackSmith();     }   break;
                case "EXMENU__DUNGEON":         { Area4_BtnClick__Dungeon();        }   break;
                case "EXMENU__DOCUMENT":        { Area4_BtnClick__Document();       }   break;
                case "EXMENU__POWER_SAVING":    { Area4_BtnClick__PowerSaving();    }   break;
                
                case "INVENTORY_OPEN":  { Area4_BtnClick__InventoryOpen();  }   break;
                case "INVENTORY_CLOSE": { Area4_inventory.SetActive(false); }   break;
            }
        }

        // Area4_BtnClick__BlackSmith
        void Area4_BtnClick__BlackSmith()
        {
            Basic_blackSmithManager.Basic_Active();
            
            Area4_exMenu.SetActive(false);
        }

        // Area4_BtnClick__Dungeon
        void Area4_BtnClick__Dungeon()
        {
            Area4_exMenu.SetActive(false);

            Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.SCENE__LOAD_SCENE,  Giggle_Master.Scene_TYPE.DUNGEON    );
        }

        // Area4_BtnClick__BlackSmith
        void Area4_BtnClick__Document()
        {
            Area4_exMenu.SetActive(false);

            Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.SCENE__LOAD_SCENE,  Giggle_Master.Scene_TYPE.DOCUMENT   );
        }

        // Area4_BtnClick__PowerSaving
        void Area4_BtnClick__PowerSaving()
        {
            PowerSaving_OnOff(true);

            Area4_exMenu.SetActive(false);
        }

        // Area4_BtnClick__InventoryOpen
        void Area4_BtnClick__InventoryOpen()
        {
            if(Area4_inventoryCoroutine == null)
            {
                Area4_inventoryCoroutine = Area4_BtnClick__InventoryOpen__Coroutine();
                Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MASTER__BASIC__START_COROUTINE, Area4_inventoryCoroutine    );
            }
        }

        IEnumerator Area4_BtnClick__InventoryOpen__Coroutine()
        {
            List<Giggle_Item.Inventory> inventory
                = (List<Giggle_Item.Inventory>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.PLAYER__ITEM__GET_ITEM_LIST);
            
            //
            int inventoryCount = 0;

            while(inventoryCount < inventory.Count)
            {
                if((bool)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.DATABASE__ITEM__GET_IS_OPEN))
                {
                    Giggle_Item.List data
                        = (Giggle_Item.List)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                            Giggle_ScriptBridge.EVENT.DATABASE__ITEM__GET_DATA_FROM_ID,
                            //
                            inventory[inventoryCount].Basic_VarDataId);

                    if((bool)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.DATABASE__ITEM__GET_IS_OPEN))
                    {
                        inventoryCount++;
                    }
                }
                
                yield return null;
            }

            Area4_inventoryDatas.Clear();
            for(int for0 = 0; for0 < inventory.Count; for0++)
            {
                Area4_inventoryDatas.Add(inventory[for0].Basic_VarInventoryId);
            }

            Area4_inventory__scrollView.Basic_SelectMenuBar(0);

            Area4_inventory.SetActive(true);

            //
            IEnumerator inventoryCoroutine = Area4_inventoryCoroutine;
            Area4_inventoryCoroutine = null;
            Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MASTER__BASIC__STOP_COROUTINE,  inventoryCoroutine  );
        }

        ////////// Constructor & Destroyer  //////////

        void Area4_Init()
        {
            Area4_inventory__scrollView.Basic_Init(this);
            if(Area4_inventoryDatas == null)
            {
                Area4_inventoryDatas = new List<int>();
            }

            Area4_inventoryCoroutine = null;
        }

        #endregion

        #region AREA5

        [Header("AREA5 ==================================================")]
        [SerializeField] TextMeshProUGUI    Area5_questText;

        IEnumerator Area5_coroutine;

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////

        public void Area5_BtnClick(string[] _names)
        {
            switch(_names[2])
            {
                case "POP_UP":       { Area5_Quest();        }   break;
            }
        }

        // Area5_Quest
        void Area5_Quest()
        {
            if(Area5_coroutine == null)
            {
                Area5_coroutine = Area5_Quest__Coroutine();
                Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MASTER__BASIC__START_COROUTINE, Area5_coroutine );
            }
        }

        IEnumerator Area5_Quest__Coroutine()
        {
            bool isWhile = true;
            while(isWhile)
            {
                if((bool)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.DATABASE__QUEST__GET_IS_OPEN))
                {
                    Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.DATABASE__QUEST__GET_DATA_FROM_ID,  510001 );

                    if((bool)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.DATABASE__QUEST__GET_IS_OPEN))
                    {
                        isWhile = false;
                    }
                }

                yield return null;
            }

            //
            Basic_manager.UI_PopUpActive("QUEST");

            IEnumerator element = Area5_coroutine;
            Area5_coroutine = null;

            Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MASTER__BASIC__STOP_COROUTINE,  element );
        }

        ////////// Constructor & Destroyer  //////////

        void Area5_Init()
        {
            Area5_coroutine = null;
        }

        #endregion

        #region AREA6

        [Header("AREA6 ==================================================")]
        [SerializeField] TextMeshProUGUI    Area6_aa;

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////

        ////////// Constructor & Destroyer  //////////

        #endregion
        
        #region AREA7

        [Header("AREA7 ==================================================")]
        [SerializeField] TextMeshProUGUI    Area7_gachaText;

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
            Basic_pinocchioManager.Basic_Active();
        }

        void Area7_MarionetteOn()
        {
            Basic_marionetteManager.Basic_Active();
        }

        ////////// Constructor & Destroyer  //////////

        #endregion

        #region AREA8

        [Header("AREA8 ==================================================")]
        [SerializeField] TextMeshProUGUI    Area8_aa;

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////

        ////////// Constructor & Destroyer  //////////

        #endregion

        #region BATTLE

        //[Header("BATTLE ==================================================")]

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////

        public void Battle_BtnClick(string[] _names)
        {
            switch(_names[2])
            {
                //case "RESET":   { Battle_Reset();   }   break;
            }
        }

        ////////// Constructor & Destroyer  //////////

        #endregion

        #region POWER_SAVING

        [Header("POWER_SAVING ==================================================")]
        [SerializeField] GameObject PowerSaving_parent;

        ////////// Getter & Setter          //////////
        object PowerSaving_GetIsOn(params object[] _args)   { return PowerSaving_parent.activeSelf;   }

        ////////// Method                   //////////

        public void PowerSaving_BtnClick(string[] _names)
        {
            switch(_names[2])
            {
                case "OFF":   { PowerSaving_OnOff(false);   }   break;
            }
        }

        //
        public void PowerSaving_OnOff(bool _isActive)
        {
            PowerSaving_parent.SetActive(_isActive);

            Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MASTER__BATTLE__SLEEP_ON);
        }

        object PowerSaving_OnOff__Bridge(params object[] _args)
        {
            bool isActive = (bool)_args[0];
            PowerSaving_OnOff(isActive);

            return true;
        }

        ////////// Constructor & Destroyer  //////////

        void PowerSaving_Init()
        {
            Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.MAIN__POWER_SAVING__GET_IS_ON,  PowerSaving_GetIsOn         );
            Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.MAIN__POWER_SAVING__ON_OFF,     PowerSaving_OnOff__Bridge   );
        }

        #endregion

        #region BATTLE

        [Header("BATTLE ==================================================")]
        [SerializeField] Image Battle_fade;

        [SerializeField] Transform  Battle_win;

        [SerializeField] Transform          Battle_lose;
        [SerializeField] TextMeshProUGUI    Battle_loseTimer;

        [Header("RUNNING")]
        [SerializeField] List<float>    Battle_timers;

        ////////// Getter & Setter          //////////
        object Battle_VarFade(params object[] _args)
        {
            //
            return Battle_fade;
        }

        // Battle_win
        object Battle_VarWinActive(params object[] _args)
        {
            bool isActive = (bool)_args[0];

            //
            Battle_win.gameObject.SetActive(isActive);

            //
            return Battle_win.gameObject.activeSelf;
        }

        // Battle_win
        object Battle_VarLoseActive(params object[] _args)
        {
            bool isActive = (bool)_args[0];

            //
            Battle_lose.gameObject.SetActive(isActive);

            //
            return Battle_lose.gameObject.activeSelf;
        }

        ////////// Method                   //////////
        
        object Battle_FadeOut(params object[] _args)
        {
            Basic_coroutinePhase = 10000;

            //
            return true;
        }

        object Battle_FadeIn(params object[] _args)
        {
            Basic_coroutinePhase = 10100;

            //
            return true;
        }

        object Battle_GetLose(params object[] _args)
        {
            Basic_coroutinePhase = 10200;

            //
            return true;
        }

        ////////// Constructor & Destroyer  //////////

        void Battle_Init()
        {
            if(Battle_timers == null)
            {
                Battle_timers = new List<float>();
            }

            while(Battle_timers.Count < 1)
            {
                Battle_timers.Add(0.0f);
            }

            //
            Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.MAIN__BATTLE__VAR_FADE,         Battle_VarFade          );
            Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.MAIN__BATTLE__VAR_WIN_ACTIVE,   Battle_VarWinActive     );
            Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.MAIN__BATTLE__VAR_LOSE_ACTIVE,  Battle_VarLoseActive    );

            Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.MAIN__BATTLE__FADE_OUT, Battle_FadeOut  );
            Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.MAIN__BATTLE__FADE_IN,  Battle_FadeIn   );
            Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.MAIN__BATTLE__LOSE,     Battle_GetLose  );
        }

        // Battle_Coroutine //////////
        void Battle_Coroutine(float _timer)
        {
            switch(Basic_coroutinePhase)
            {
                // FadeOut
                case 10000: { Battle_Coroutine__FadeOutStart();     }   break;
                case 10001: { Battle_Coroutine__FadeOut(_timer  );  }   break;
                case 10002: {                                       }   break;
                // FadeIn
                case 10100: { Battle_Coroutine__FadeInStart();      }   break;
                case 10101: { Battle_Coroutine__FadeIn(_timer   );  }   break;
                case 10102: {                                       }   break;
                // Lose
                case 10200: { Battle_Coroutine__LoseStart();            }   break;
                case 10201: { Battle_Coroutine__LoseDoing(_timer  );    }   break;
                case 10202: {                                           }   break;
            }
        }

        // Battle_Coroutine__FadeOut
        void Battle_Coroutine__FadeOutStart()
        {
            Battle_timers[0] = 0.0f;

            Color c = Battle_fade.color;
            c.a = 0.0f;
            Battle_fade.color = c;
            Battle_fade.gameObject.SetActive(true);

            //
            Basic_coroutinePhase = 10001;
        }

        void Battle_Coroutine__FadeOut(float _timer)
        {
            Battle_timers[0] += _timer;

            if(Battle_timers[0] >= 1.0f)
            {
                Battle_timers[0] = 1.0f;

                //
                Basic_coroutinePhase = 10002;
            }

            Color c = Battle_fade.color;
            c.a = Battle_timers[0];
            Battle_fade.color = c;
        }

        // Battle_Coroutine__FadeIn
        void Battle_Coroutine__FadeInStart()
        {
            Battle_timers[0] = 1.0f;

            Color c = Battle_fade.color;
            c.a = 1.0f;
            Battle_fade.color = c;

            //
            Basic_coroutinePhase = 10101;
        }

        void Battle_Coroutine__FadeIn(float _timer)
        {
            Battle_timers[0] -= _timer;

            if(Battle_timers[0] <= 0.0f)
            {
                Battle_timers[0] = 0.0f;
                Battle_fade.gameObject.SetActive(false);

                //
                Basic_coroutinePhase = 10102;
            }

            Color c = Battle_fade.color;
            c.a = Battle_timers[0];
            Battle_fade.color = c;
        }

        // Battle_Coroutine__Lose
        void Battle_Coroutine__LoseStart()
        {
            Battle_lose.gameObject.SetActive(true);

            Battle_timers[0] = 2.0f;
            Battle_loseTimer.text = (1 + (int)Battle_timers[0]).ToString();

            Basic_coroutinePhase = 10201;
        }

        void Battle_Coroutine__LoseDoing(float _timer)
        {
            //
            Battle_timers[0] -= _timer;
            if(Battle_timers[0] <= 0.0f)
            {
                Battle_timers[0] = 0.0f;

                Basic_coroutinePhase = 10202;
            }

            Battle_loseTimer.text = (1 + (int)Battle_timers[0]).ToString();
        }

        #endregion

    }

    [Serializable]
    public class UI_MainPopUpData : UI_PopUpData
    {
        public enum LIST
        {
            GACHA
        }

        [SerializeField] Giggle_MainManager Basic_manager;

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////

        public override void Basic_Active(string _name)
        {
            base.Basic_Active(_name);

            // 팝업 열기 외에 기능이 필요한 경우
            switch(_name)
            {
                case "ACCEL":   {                   }   break;
                case "GACHA":   { Gacha_Reset();    }   break;
                case "QUEST":   { Quest_Reset();    }   break;
            }
        }

        ////////// Constructor & Destroyer  //////////

        public void Basic_Init(Giggle_MainManager _manager)
        {
            base.Basic_Init();

            Basic_manager = _manager;
            Gacha_Init();
            Quest_Init();
        }

        #region ACCEL

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////
        public void Accel_BtnClick(string[] _names)
        {
            switch(_names[2])
            {
                case "AD":      { Accel_BtnClick__AD();     }   break;
                case "CANCEL":  { Accel_BtnClick__CANCEL(); }   break;
            }
        }

        void Accel_BtnClick__AD()
        {
            // TODO: 차후 편집해주세요.
            Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MASTER__BATTLE__ACCEL);
            Basic_manager.UI_VarBasicData.Area3_AccelDoing(true);
            
            Basic_parent.gameObject.SetActive(false);
        }

        void Accel_BtnClick__CANCEL()
        {
            Basic_parent.gameObject.SetActive(false);
        }

        ////////// Constructor & Destroyer  //////////
        
        
        #endregion

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

                Debug.Log(_list.Count);
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
                Basic_infoType.text      = Basic_character.Basic_VarCategory.ToString();
                Basic_infoAttribute.text = Basic_character.Basic_VarAttribute.ToString();

                Basic_statusList[0].text = Basic_character.Basic_GetStatusList(1).Basic_VarAttack.ToString();
                Basic_statusList[1].text = Basic_character.Basic_GetStatusList(1).Basic_VarDefence.ToString();
                Basic_statusList[2].text = Basic_character.Basic_GetStatusList(1).Basic_VarHp.ToString();
                Basic_statusList[3].text = Basic_character.Basic_GetStatusList(1).Basic_VarAttackSpeed.ToString();
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
            Gacha_selectBtn.SetActive(!Gacha_select.Equals(-1));

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
            Gacha_BtnClick__CHARACTER("-1");
            
            // 가챠 리스트 불러오기
            List<Giggle_Character.Database> list
                = (List<Giggle_Character.Database>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.DATABASE__MARIONETTE__GET_DATAS_FROM_ATTRIBUTE,
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

        #region QUEST

        [Serializable]
        public class Quest_MenuBar : Giggle_UI.MenuBar
        {

            ////////// Getter & Setter          //////////

            ////////// Method                   //////////

            protected override void Basic_SelectMenu__Setting(int _for0, int _count)
            {
            }

            ////////// Constructor & Destroyer  //////////
        }

        [Serializable]
        public class Quest_ScrollView : Giggle_UI.ScrollView
        {
            [SerializeField] UI_MainPopUpData   Basic_parentClass;
            [SerializeField] Transform          Basic_rawParent;

            ////////// Getter & Setter          //////////

            ////////// Method                   //////////

            //
            public void Basic_Init(UI_MainPopUpData _parentClass)
            {
                Basic_parentClass = _parentClass;

                //
                Basic_Init();
            }

            protected override void Basic_Init__SetName()
            {
                Basic_list[0].Find("Button").name = "Button/QUEST/SCROLL_VIEW/0";
                Basic_list[1].Find("Button").name = "Button/QUEST/SCROLL_VIEW/1";
            }

            protected override void Basic_AddList__SetName(Transform _element)
            {
                _element.Find("Button").name = "Button/QUEST/SCROLL_VIEW/" + Basic_list.Count;
            }

            //
            public override void Basic_ClickBtn(int _count)
            {
                //
                if(!_count.Equals(-1))
                {
                    Giggle_Character.Save pinocchioData
                        = (Giggle_Character.Save)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                            Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__VAR_DATA);

                    //
                    //pinocchioData.Basic_VarDataId = Basic_parentClass.Job_VarJobList[_count];
                    //Basic_parentClass.Job_InfoSetting();
                    Basic_CheckCover();
                }
            }

            // Basic_SelectMenuBar
            public void Basic_SelectMenuBar(int _count)
            {
                //Basic_parentClass.Job_ListSetting(_count);
                Basic_SelectMenuBar__Check();

                Basic_ClickBtn(-1);
            }

            void Basic_SelectMenuBar__Check()
            {
                List<Giggle_Quest.Database> datas
                    = (List<Giggle_Quest.Database>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                        Giggle_ScriptBridge.EVENT.DATABASE__QUEST__GET_DATAS_FROM_TYPE,
                        //
                        Basic_parentClass.Quest_VarSelect);
                
                int finalCount = 0;
                int whileCount = 0;
                while(whileCount < Basic_list.Count)
                {
                    if(whileCount < datas.Count)
                    {
                        Basic_list[whileCount].gameObject.SetActive(true);
                    
                        //
                        Giggle_Quest.Text text
                            = (Giggle_Quest.Text)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                                Giggle_ScriptBridge.EVENT.DATABASE__QUEST__GET_TEXT,
                                //
                                datas[whileCount].Basic_VarCompleteConditionType);

                        Basic_list[whileCount].Find("Title"         ).GetComponent<TextMeshProUGUI>().text = text.Basic_PrintTitle(datas[whileCount]);
                        Basic_list[whileCount].Find("Description"   ).GetComponent<TextMeshProUGUI>().text = text.Basic_PrintDescription(datas[whileCount]);
                        
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
                        (Basic_list[finalCount].GetComponent<RectTransform>().sizeDelta.y * 0.5f) + 10.0f - Basic_list[finalCount].localPosition.y);

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

        [Header("QUEST ==================================================")]
        [SerializeField] Quest_MenuBar      Quest_menuBar;
        [SerializeField] Quest_ScrollView   Quest_scrollView;

        [Header("RUNNING")]
        [SerializeField] Giggle_Quest.TYPE  Quest_select;

        ////////// Getter & Setter          //////////
        public Giggle_Quest.TYPE    Quest_VarSelect { get { return Quest_select;    }   }

        ////////// Method                   //////////
        public void Quest_BtnClick(string[] _names)
        {
            switch(_names[2])
            {
                case "MENU_BAR":    { Quest_BtnClick__MENU_BAR(_names);     }   break;
                case "SCROLL_VIEW": { Quest_BtnClick__SCROLL_VIEW(_names);  }   break;
                case "CANCEL":      { Quest_BtnClick__CANCEL();             }   break;
            }
        }

        void Quest_BtnClick__MENU_BAR(string[] _names)
        {
            int select = int.Parse(_names[3]);
            Quest_menuBar.Basic_SelectMenu(select);
        }

        void Quest_BtnClick__SCROLL_VIEW(string[] _names)
        {
            int select = int.Parse(_names[3]);
            Debug.Log("Quest_BtnClick__SCROLL_VIEW " + select);
        }

        void Quest_BtnClick__CANCEL()
        {
            Basic_parent.gameObject.SetActive(false);
        }

        //
        void Quest_Reset()
        {
            Quest_select = (Giggle_Quest.TYPE)1;
            Quest_menuBar.Basic_SelectMenu(0);
            Quest_scrollView.Basic_SelectMenuBar(0);
        }

        ////////// Constructor & Destroyer  //////////
        void Quest_Init()
        {
            Quest_menuBar.Basic_Init();
            for(int for0 = 0; for0 < Quest_menuBar.Basic_VarListCount; for0++)
            {
                Quest_menuBar.Basic_GetListBtn(for0).name = "Button/QUEST/MENU_BAR/" + for0.ToString();
            }

            Quest_scrollView.Basic_Init(this);
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
            case "AREA3":           { UI_basicData.Area3_BtnClick(names);       }   break;
            case "AREA4":           { UI_basicData.Area4_BtnClick(names);       }   break;
            case "AREA5":           { UI_basicData.Area5_BtnClick(names);       }   break;
            case "AREA7":           { UI_basicData.Area7_BtnClick(names);       }   break;
            case "BATTLE":          { UI_basicData.Battle_BtnClick(names);      }   break;
            case "POWER_SAVING":    { UI_basicData.PowerSaving_BtnClick(names); }   break;
            //
            //case "PINOCCHIO":   { UI_basicData.Pinocchio_VarData.Basic_BtnClick(names);     }   break;
            //case "MARIONETTE":  { UI_basicData.Marionette_VarData.Basic_BtnClick(names);    }   break;
        }
    }
    
    // UI_popUpData
    public override void UI_PopUpBtnClick(GameObject _btn)
    {
        string[] names = _btn.name.Split('/');
        switch(names[1])
        {
            case "ACCEL":   { UI_popUpData.Accel_BtnClick(names);   }   break;
            case "GACHA":   { UI_popUpData.Gacha_BtnClick(names);   }   break;
            case "QUEST":   { UI_popUpData.Quest_BtnClick(names);   }   break;
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
        UI_popUpData.Basic_Init(this);
    }

    #endregion

    #region 

    ////////// Getter & Setter  //////////

    ////////// Method           //////////

    ////////// Unity            //////////

    #endregion
}