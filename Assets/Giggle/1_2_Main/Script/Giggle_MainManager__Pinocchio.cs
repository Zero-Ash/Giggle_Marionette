using UnityEngine;

//
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class Giggle_MainManager__Pinocchio : MonoBehaviour
{
    [SerializeField] Canvas Basic_canvas;
    [SerializeField] GameObject Basic_back;
    [SerializeField] GameObject Basic_ui;
    [SerializeField] RectTransform Basic_safeArea;

    [SerializeField] Giggle_UI.MenuBar BasicArea1_menuBar;

    [Header("RUNNING")]
    IEnumerator Basic_coroutine;
    [SerializeField] int    Basic_coroutinePhase;

    ////////// Getter & Setter  //////////
    
    // Basic_back
    public Vector3 Basic_VarBackPosition { get { return Basic_back.transform.position;  }   }
    
    ////////// Method           //////////
            
    public void Basic_BtnClick(GameObject _obj)
    {
        string[] names = _obj.name.Split('/');
        switch(names[2])
        {
            case "CLOSE":       { Basic_Close();                                }   break;
            case "MENU_BAR":    { BasicArea1_SelectMenu(int.Parse(names[3]));   }   break;

            // JOB
            case "JOB__MENU_BAR":       { Job_BtnSelectMenu(int.Parse(names[3]));   }   break;
            case "JOB__SCROLL_VIEW":    { Job_BtnSscrollView(int.Parse(names[3]));  }   break;

            // EQUIPMENT
            case "EQUIPMENT__EQUIPMENT":    { Equipment_SelectEquipItem(names[3]);              }   break;
            case "EQUIPMENT__SAVE":         { Equipment_SelectEquipSave(int.Parse(names[3]));   }   break;
            case "EQUIPMENT__MENU_BAR":     { Equipment_BtnMenuBar(int.Parse(names[3]));        }   break;
            case "EQUIPMENT__ARRAY_OPEN":   { Equipment_BtnArrayOpen();                         }   break;
            case "EQUIPMENT__ARRAY_LIST":   { Equipment_BtnArrayList(int.Parse(names[3]));      }   break;
            case "EQUIPMENT__SCROLL_VIEW":  { Equipment_BtnScrollView(int.Parse(names[3]));     }   break;

            //// SKILL
            case "SKILL__SLOT":     { Skill_BtnSlot(int.Parse(names[3]));   }   break;
            case "SKILL__PRESET":   { Skill_BtnPreset(int.Parse(names[3])); }   break;

            case "SKILL__INFO__AWAKENING":  { Skill_InfoBtnAwakening();                                     }   break;
            case "SKILL__INFO__LEVEL_UP":   { Skill_InfoBtnLevelUp();                                       }   break;
            case "SKILL__INFO__EQUIP":      { Skill_InfoBtnEquip();                                         }   break;
            case "SKILL__INFO__UNEQUIP":    { Skill_InfoBtnUnEquip();                                       }   break;

            case "SKILL__SCROLL_VIEW":      { SkillArea3_scrollView.Basic_ClickBtn(int.Parse(names[3]));    }   break;

            // ATTRIBUTE
            case "ATTRIBUTE__CLOSE":    { Attribute_BtnClose(); }   break;

            case "ATTRIBUTE__ATTACK_SCROLL_VIEW":   { Attribute_BtnAttackElement(int.Parse(names[3]));  }   break;
            case "ATTRIBUTE__DEFENCE_SCROLL_VIEW":  { Attribute_BtnDefenceElement(int.Parse(names[3])); }   break;
            case "ATTRIBUTE__SUPPORT_SCROLL_VIEW":  { Attribute_BtnSupportElement(int.Parse(names[3])); }   break;

            case "ATTRIBUTE__INFO_AWAKENING":   { Attribute_BtnInfoAwakening(); }   break;
            case "ATTRIBUTE__INFO_LEVEL":       { Attribute_BtnLevelUp();       }   break;

            // ABILITY
            case "ABILITY_LOCK":    { Ability_BtnLock(int.Parse(names[3])); }   break;
            case "ABILITY_CHANGE":  { Ability_BtnChange();                  }   break;

            case "ABILITY__MARIONETTE_LIST_MENU_BAR":       { Ability_MarionetteListBtnMenuBar(int.Parse(names[3]));    }   break;
            case "ABILITY__MARIONETTE_LIST_SCROLL_VIEW":    { Ability_MarionetteListBtnScrollView(int.Parse(names[3])); }   break;

            case "ABILITY_WOOD_WORK":   { Ability_BtnWoodWork(int.Parse(names[3]));     }   break;
            case "ABILITY_WOOD_WORKER": { Ability_BtnWoodWorker(int.Parse(names[3]));   }   break;

            case "ABILITY__SELECT_WOOD__SELECT":    { Ability_PopUpSelectWoodBtnSelect(int.Parse(names[3]));    }   break;
            case "ABILITY__SELECT_WOOD__AD":        { Ability_PopUpSelectWoodBtnAd(int.Parse(names[3]));        }   break;
            case "ABILITY__SELECT_WOOD__OK":        { Ability_PopUpSelectWoodBtnOk();                           }   break;
            case "ABILITY__SELECT_WOOD__CANCEL":    { Ability_PopUpSelectWoodBtnCancel();                       }   break;

            // RELIC
            case "RELIC__SLOT":         { Relic_BtnSlot(int.Parse(names[3]));       }   break;
            case "RELIC__SCROLL_VIEW":  { Relic_BtnScrollView(int.Parse(names[3])); }   break;
        }
    }

    void Basic_Close()
    {
        Basic_back.SetActive(false);
        Basic_ui.SetActive(false);

        Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MASTER__BASIC__STOP_COROUTINE, Basic_coroutine);

        //
        Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MASTER__BATTLE__VAR_COROUTINE_PHASE, Giggle_Battle.Basic__COROUTINE_PHASE.RESET);
    }

    void BasicArea1_SelectMenu(int _count)
    {
        BasicArea1_menuBar.Basic_SelectMenu(_count);
        Basic_coroutinePhase = _count;
        switch(Basic_coroutinePhase)
        {
            case 4: { Ability_UISetting();  }   break;
        }
    }

    // Basic_Active
    public void Basic_Active()
    {
        Basic_back.SetActive(true);

        Basic_coroutine = Basic_Coroutine();
        Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MASTER__BASIC__START_COROUTINE, Basic_coroutine);
    }

    ////////// Unity            //////////
    void Start()
    {
        Basic_back.SetActive(false);
        Basic_ui.SetActive(false);
        
        BasicArea1_menuBar.Basic_Init();
        for(int for0 = 0; for0 < BasicArea1_menuBar.Basic_VarListCount; for0++)
        {
            BasicArea1_menuBar.Basic_GetListBtn(for0).name = "Button/PINOCCHIO/MENU_BAR/" + for0.ToString();
        }
        
        StartCoroutine(Start__Coroutine());
    }

    IEnumerator Start__Coroutine()
    {
        while(true)
        {
            if(Giggle_ScriptBridge.Basic_VarInstance.Basic_GetIsInMethod(Giggle_ScriptBridge.EVENT.MASTER__BASIC__START_COROUTINE))
            {
                break;
            }

            yield return null;
        }

        Basic_safeArea.sizeDelta        = (Vector2)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MASTER__UI__SAFE_AREA_VAR_SIZE_DELTA );
        Basic_safeArea.localPosition    = (Vector2)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MASTER__UI__SAFE_AREA_VAR_POSITION   );

        Job_Start();
        Equipment_Start();
        Skill_Start();
        Attribute_Start();
        Ability_Start();
        Relic_Start();
    }

    //
    IEnumerator Basic_Coroutine()
    {
        Basic_coroutinePhase = 0;

        while(Basic_coroutinePhase >= 0)
        {
            switch(Basic_coroutinePhase)
            {
                // 아이템
                case 0:
                    {
                        if((bool)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.DATABASE__ITEM__GET_IS_OPEN))
                        {
                            Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.DATABASE__ITEM__GET_DATA_FROM_ID, 701101001);

                            Basic_coroutinePhase = 1;
                        }
                    }
                    break;
                case 1:
                    {
                        if((bool)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.DATABASE__ITEM__GET_IS_OPEN))
                        {
                            Basic_coroutinePhase = 10;
                        }
                    }
                    break;

                case 10:
                    {
                        if((bool)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.DATABASE__ITEM__GET_IS_OPEN))
                        {
                            Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.DATABASE__ITEM__GET_DATA_FROM_ID, 72101001);

                            Basic_coroutinePhase = 11;
                        }
                    }
                    break;
                case 11:
                    {
                        if((bool)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.DATABASE__ITEM__GET_IS_OPEN))
                        {
                            Basic_coroutinePhase = 20;
                        }
                    }
                    break;

                case 20:
                    {
                        if((bool)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.DATABASE__ITEM__GET_IS_OPEN))
                        {
                            Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.DATABASE__ITEM__GET_DATA_FROM_ID, 74101001);

                            Basic_coroutinePhase = 21;
                        }
                    }
                    break;
                case 21:
                    {
                        if((bool)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.DATABASE__ITEM__GET_IS_OPEN))
                        {
                            Basic_coroutinePhase = 30;
                        }
                    }
                    break;

                case 30:
                    {
                        if((bool)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.DATABASE__ITEM__GET_IS_OPEN))
                        {
                            Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.DATABASE__ITEM__GET_DATA_FROM_ID, 76101001);

                            Basic_coroutinePhase = 31;
                        }
                    }
                    break;
                case 31:
                    {
                        if((bool)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.DATABASE__ITEM__GET_IS_OPEN))
                        {
                            Basic_coroutinePhase = 40;
                        }
                    }
                    break;
                    
                case 40:
                    {
                        if((bool)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.DATABASE__ITEM__GET_IS_OPEN))
                        {
                            Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.DATABASE__ITEM__GET_DATA_FROM_ID, 78101001);

                            Basic_coroutinePhase = 41;
                        }
                    }
                    break;
                case 41:
                    {
                        if((bool)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.DATABASE__ITEM__GET_IS_OPEN))
                        {
                            Basic_coroutinePhase = 10000;
                        }
                    }
                    break;
                // 캐릭터
                case 10000:
                    {
                        Basic_coroutinePhase = -1;
                    }
                    break;
            }
            yield return null;
        }

        //
        Equipment_Active();
        Skill_Active();
        Relic_Active();

        BasicArea1_SelectMenu(0);
        Job_BtnSelectMenu(0);
        Job_InfoSetting();

        Basic_ui.SetActive(true);

        //
        WaitForEndOfFrame wfeof = new WaitForEndOfFrame();
        while(true)
        {
            switch(Basic_coroutinePhase)
            {
                case 4: { Ability_Coroutine();  }   break;
            }
            yield return wfeof;
        }
    }

    #region JOB

    [Serializable]
    public class MenuBar_Job : Giggle_UI.MenuBar
    {
        [SerializeField] Giggle_MainManager__Pinocchio Basic_parentClass;

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////

        public void Basic_Init(Giggle_MainManager__Pinocchio _parentClass)
        {
            Basic_parentClass = _parentClass;
            Basic_Init();
        }

        protected override void Basic_SelectMenu__Setting(int _for0, int _count)
        {
            if(_for0.Equals(_count))
            {
                Basic_parentClass.Job_VarScrollView.Basic_SelectMenuBar(_count);
            }
        }

        ////////// Constructor & Destroyer  //////////
        
    }

    [Serializable]
    public class ScrollView_Job : Giggle_UI.ScrollView
    {
        [SerializeField] Giggle_MainManager__Pinocchio  Basic_parentClass;
        [SerializeField] Transform                      Basic_rawParent;

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////

        //
        public void Basic_Init(Giggle_MainManager__Pinocchio _parentClass)
        {
            Basic_parentClass = _parentClass;

            //
            Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MASTER__UI__RAW_IMAGE, Basic_rawParent);

            //
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
                pinocchioData.Basic_VarDataId = Basic_parentClass.Job_VarJobList[_count];
                Basic_parentClass.Job_InfoSetting();
                Basic_CheckCover();
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
    [SerializeField] Transform  JobArea2_rawParent;

    [SerializeField] MenuBar_Job    JobArea3_menuBar;
    [SerializeField] ScrollView_Job JobArea3_scrollView;

    [SerializeField] List<int>  Job_jobList;

    ////////// Getter & Setter  //////////

    //
    public ScrollView_Job   Job_VarScrollView   { get { return JobArea3_scrollView; }   }

    //
    public List<int>    Job_VarJobList  { get { return Job_jobList; }   }
    
    ////////// Method           //////////
    void Job_BtnSelectMenu(int _count)
    {
        Job_ListSetting(_count);
        JobArea3_menuBar.Basic_SelectMenu(_count);
    }

    //
    void Job_BtnSscrollView(int _count)
    {
        JobArea3_scrollView.Basic_ClickBtn(_count);
    }

    //
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

    ////////// Unity            //////////

    void Job_Start()
    {
        // Area2
        Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MASTER__UI__RAW_IMAGE, JobArea2_rawParent);

        // Area3
        JobArea3_menuBar.Basic_Init(this);
        for(int for0 = 0; for0 < JobArea3_menuBar.Basic_VarListCount; for0++)
        {
            JobArea3_menuBar.Basic_GetListBtn(for0).name = "Button/PINOCCHIO/JOB__MENU_BAR/" + for0.ToString();
        }
        JobArea3_scrollView.Basic_Init(this);
    }

    #endregion

    #region EQUIPMENT

    [Serializable]
    public class MenuBar_Equipment : Giggle_UI.MenuBar
    {
        [SerializeField] Giggle_MainManager__Pinocchio Basic_parentClass;

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////

        public void Basic_Init(Giggle_MainManager__Pinocchio _parentClass)
        {
            Basic_parentClass = _parentClass;
            Basic_Init();
        }

        protected override void Basic_SelectMenu__Setting(int _for0, int _count)
        {
            if(_for0.Equals(_count))
            {
                Basic_parentClass.Equipment_SelectMenu(_count);
            }
        }

        ////////// Constructor & Destroyer  //////////
    }

    [Serializable]
    public class ScrollView_Equipment : Giggle_UI.ScrollView
    {
        [SerializeField] Giggle_MainManager__Pinocchio  Basic_parentClass;

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////

        public void Basic_Init(Giggle_MainManager__Pinocchio _parentClass)
        {
            Basic_parentClass = _parentClass;
            Basic_Init();
        }

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
            Basic_parentClass.Equipment_SelectInventoryItem(_count);
        }

        // Basic_SelectMenuBar
        public void Basic_SelectMenuBar()
        {
            int finalCount = 0;
            int whileCount = 0;
            while(whileCount < Basic_list.Count)
            {
                if(whileCount < Basic_parentClass.Equipment_inventoryItems.Count)
                {
                    Basic_list[whileCount].gameObject.SetActive(true);

                    // 기존 데이터 날리기
                    Basic_list[whileCount].Find("Portrait").GetComponent<Image>().sprite = null;
                    Basic_list[whileCount].Find("Name").GetComponent<TextMeshProUGUI>().text = "";

                    //
                    Giggle_Item.Inventory item = (Giggle_Item.Inventory)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                        Giggle_ScriptBridge.EVENT.PLAYER__ITEM__GET_ITEM_FROM_INVENTORY_ID, Basic_parentClass.Equipment_VarInventoryItems[whileCount]);

                    Giggle_Item.List database = (Giggle_Item.List)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                        Giggle_ScriptBridge.EVENT.DATABASE__ITEM__GET_DATA_FROM_ID,
                        //
                        item.Basic_VarDataId);
                        
                    Sprite sprite = null;
                    switch(database.Basic_VarType)
                    {
                        case Giggle_Item.TYPE.ACCESSORY:
                            {
                                sprite
                                    = (Sprite)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                                        Giggle_ScriptBridge.EVENT.DATABASE__ITEM__GET_SPRITE_FROM_VALUE,
                                        //
                                        database.Basic_VarType, database.Basic_VarClass
                                    );
                            }
                            break;
                        default:
                            {
                                sprite
                                    = (Sprite)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                                        Giggle_ScriptBridge.EVENT.DATABASE__ITEM__GET_SPRITE_FROM_VALUE,
                                        //
                                        database.Basic_VarType
                                    );
                            }
                            break;
                    }
                    Basic_list[whileCount].transform.Find("Portrait").GetComponent<Image>().sprite = sprite;
                    Basic_list[whileCount].Find("Name").GetComponent<TextMeshProUGUI>().text = database.Basic_VarName;

                    //
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

            Basic_ClickBtn(-1);
        }

        //
        public void Basic_CheckCover()
        {
            string[] types = Basic_parentClass.Equipment_VarSelectEquipment.Split('_');
            Giggle_Item.TYPE itemType = (Giggle_Item.TYPE)Enum.Parse(typeof(Giggle_Item.TYPE), types[0]);

            Giggle_Character.Save pinocchioData
                = (Giggle_Character.Save)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__VAR_DATA);

            int whileCount = 0;
            while(whileCount < Basic_parentClass.Equipment_VarInventoryItems.Count)
            {
                Basic_list[whileCount].Find("Cover").gameObject.SetActive(false);

                //
                for(int for0 = 0; for0 < pinocchioData.Basic_VarEquipments.Count; for0++)
                {
                    if(pinocchioData.Basic_VarEquipments[for0].Equals(Basic_parentClass.Equipment_VarInventoryItems[whileCount]))
                    {
                        Basic_list[whileCount].Find("Cover").gameObject.SetActive(true);
                        break;
                    }
                }

                // 장비의 타입에 따라 커버를 씌운다.
                if(!Basic_list[whileCount].Find("Cover").gameObject.activeSelf)
                {
                    if(!Basic_parentClass.Equipment_VarSelectEquipment.Equals("NONE"))
                    {
                        if(whileCount < Basic_parentClass.Equipment_VarInventoryItems.Count)
                        {
                            Giggle_Item.Inventory item = (Giggle_Item.Inventory)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                                Giggle_ScriptBridge.EVENT.PLAYER__ITEM__GET_ITEM_FROM_INVENTORY_ID, Basic_parentClass.Equipment_VarInventoryItems[whileCount]);

                            Giggle_Item.List data = (Giggle_Item.List)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                                Giggle_ScriptBridge.EVENT.DATABASE__ITEM__GET_DATA_FROM_ID,
                                item.Basic_VarDataId);
                                
                            if(!itemType.Equals(data.Basic_VarType))
                            {
                                Basic_list[whileCount].Find("Cover").gameObject.SetActive(true);
                            }
                        }
                    }
                }

                //
                whileCount++;
            }
        }

        ////////// Constructor & Destroyer  //////////

    }

    [Header("EQUIPMENT ==========")]
    [SerializeField] Transform              EquipmentArea2_objParent;
    [SerializeField] Transform              EquipmentArea2_rawParent;
    [SerializeField] List<Button>           EquipmentArea2_equipmentBtns;

    [SerializeField] MenuBar_Equipment      EquipmentArea3_menuBar;
    [SerializeField] Giggle_UI.ListArray    EquipmentArea3_listArray;
    [SerializeField] ScrollView_Equipment   EquipmentArea3_scrollView;

    [Header("RUNNING")]
    [SerializeField] List<int>  Equipment_inventoryItems;
    
    [SerializeField] string Equipment_selectEquipment;
    [SerializeField] int    Equipment_selectItem;

    ////////// Getter & Setter  //////////
    public List<int>    Equipment_VarInventoryItems     { get { return Equipment_inventoryItems;    }   }

    public string       Equipment_VarSelectEquipment    { get { return Equipment_selectEquipment;   }   }
    
    ////////// Method           //////////

    void Equipment_BtnMenuBar(int _count)
    {
        EquipmentArea3_menuBar.Basic_SelectMenu(_count);
    }

    void Equipment_BtnArrayOpen()
    {
        EquipmentArea3_listArray.Basic_BtnOpen();
    }

    void Equipment_BtnArrayList(int _count)
    {
        EquipmentArea3_listArray.Basic_BtnClick(_count);
    }

    void Equipment_BtnScrollView(int _count)
    {
        EquipmentArea3_scrollView.Basic_ClickBtn(_count);
    }

    //
    void Equipment_Active()
    {

        Equipment_Reset();
        //
        EquipmentArea3_listArray.Basic_Reset();

        //
        EquipmentArea3_menuBar.Basic_SelectMenu(0);
    }

    //
    void Equipment_EquipItemUI()
    {
        while(EquipmentArea2_objParent.childCount > 0)
        {
            Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.MASTER__GARBAGE__REMOVE,
                //
                EquipmentArea2_objParent.GetChild(0));
        }

        Giggle_Character.Save pinocchioData
            = (Giggle_Character.Save)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__VAR_DATA);

        Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
            Giggle_ScriptBridge.EVENT.MASTER__UI__PINOCCHIO_INSTANTIATE,
            //
            pinocchioData.Basic_VarDataId,
            EquipmentArea2_objParent, -90.0f, 900.0f);

        for(int for0 = 0; for0 < EquipmentArea2_equipmentBtns.Count; for0++)
        {
            int id
                = (int)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__EUIPMENT_VAR_SELECT_FROM_COUNT,
                    //
                    for0);
            
            if(!id.Equals(-1))
            {
                //
                Giggle_Item.Inventory inventoryData
                    = (Giggle_Item.Inventory)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                        Giggle_ScriptBridge.EVENT.PLAYER__ITEM__GET_ITEM_FROM_INVENTORY_ID,
                        id);
                Giggle_Item.List database
                    = (Giggle_Item.List)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                        Giggle_ScriptBridge.EVENT.DATABASE__ITEM__GET_DATA_FROM_ID,
                        inventoryData.Basic_VarDataId);

                //
                Sprite sprite = null;
                switch(database.Basic_VarType)
                {
                    case Giggle_Item.TYPE.ACCESSORY:
                        {
                            sprite
                                = (Sprite)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                                    Giggle_ScriptBridge.EVENT.DATABASE__ITEM__GET_SPRITE_FROM_VALUE,
                                    //
                                    database.Basic_VarType, database.Basic_VarClass
                                );
                        }
                        break;
                    default:
                        {
                            sprite
                                = (Sprite)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                                    Giggle_ScriptBridge.EVENT.DATABASE__ITEM__GET_SPRITE_FROM_VALUE,
                                    //
                                    database.Basic_VarType
                                );
                        }
                        break;
                }
                EquipmentArea2_equipmentBtns[for0].transform.Find("Portrait").GetComponent<Image>().sprite = sprite;
                EquipmentArea2_equipmentBtns[for0].transform.Find("Name").GetComponent<TextMeshProUGUI>().text = database.Basic_VarName;
            }
            else
            {
                EquipmentArea2_equipmentBtns[for0].transform.Find("Portrait").GetComponent<Image>().sprite = null;
                EquipmentArea2_equipmentBtns[for0].transform.Find("Name").GetComponent<TextMeshProUGUI>().text = "00";
            }
        }
    }

    void Equipment_EquipItemCover(Giggle_Item.List _data)
    {
        for(int for0 = 0; for0 < EquipmentArea2_equipmentBtns.Count; for0++)
        {
            EquipmentArea2_equipmentBtns[for0].transform.Find("Cover").gameObject.SetActive(true);

            //
            string[] strs = EquipmentArea2_equipmentBtns[for0].name.Split('/')[3].Split('_');

            if(strs[0].Equals(_data.Basic_VarType.ToString()))
            {
                switch(_data.Basic_VarType)
                {
                    case Giggle_Item.TYPE.ACCESSORY:
                        {
                            if(strs[2].Equals(_data.Basic_VarClass.ToString()))
                            {
                                EquipmentArea2_equipmentBtns[for0].transform.Find("Cover").gameObject.SetActive(false);
                            }
                        }
                        break;
                    default:
                        {
                            EquipmentArea2_equipmentBtns[for0].transform.Find("Cover").gameObject.SetActive(false);
                        }
                        break;
                }
            }
        }
    }

    void Equipment_EquipItemCover__Open()
    {
        for(int for0 = 0; for0 < EquipmentArea2_equipmentBtns.Count; for0++)
        {
            EquipmentArea2_equipmentBtns[for0].transform.Find("Cover").gameObject.SetActive(false);
        }
    }

    //
    void Equipment_SelectEquipItem(string _itemType)
    {
        if(Equipment_selectItem.Equals(-1))
        {
            Equipment_selectEquipment = _itemType;
            EquipmentArea3_scrollView.Basic_CheckCover();
        }
        else
        {
            Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__EUIPMENT_ITEM,
                _itemType, Equipment_inventoryItems[Equipment_selectItem]);

            Equipment_Reset();
        }
    }

    //
    void Equipment_SelectEquipSave(int _count)
    {
        Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
            Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__EUIPMENT_SAVE_SELECT, _count);
        
        //
        Equipment_Reset();
    }

    //
    void Equipment_SelectInventoryItem(int _itemId)
    {
        if(_itemId != -1)
        {
            if(Equipment_selectEquipment.Equals("NONE"))
            {
                Equipment_selectItem = _itemId;

                Giggle_Item.Inventory item = (Giggle_Item.Inventory)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.PLAYER__ITEM__GET_ITEM_FROM_INVENTORY_ID, Equipment_inventoryItems[Equipment_selectItem]);

                Giggle_Item.List data = (Giggle_Item.List)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.DATABASE__ITEM__GET_DATA_FROM_ID,
                    item.Basic_VarDataId);

                Equipment_EquipItemCover(data);
            }
            else
            {
                Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__EUIPMENT_ITEM,
                    Equipment_selectEquipment, Equipment_inventoryItems[_itemId]);

                Equipment_Reset();
            }
        }
    }

    //
    void Equipment_SelectMenu(int _count)
    {
        switch(_count)
        {
            case 1:     {                               }   break;
            default:    { Equipment_SelectMenu__All();  }   break;
        }

        // 정렬
        EquipmentArea3_listArray.Basic_ArrayList_Item(ref Equipment_inventoryItems);

        //
        EquipmentArea3_scrollView.Basic_SelectMenuBar();
    }

    void Equipment_SelectMenu__All()
    {
        List<Giggle_Item.Inventory> inventoryItems
            = (List<Giggle_Item.Inventory>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.PLAYER__ITEM__GET_ITEM_LIST);
        Equipment_inventoryItems.Clear();
        for(int for0 = 0; for0 < inventoryItems.Count; for0++)
        {
            Equipment_inventoryItems.Add(inventoryItems[for0].Basic_VarInventoryId);
        }
    }

    //
    void EquipmentArrayOpen()
    {

    }

    //
    void Equipment_Reset()
    {
        // equip
        Equipment_EquipItemUI();
        Equipment_EquipItemCover__Open();

        // list
        Equipment_selectEquipment = "NONE";
        EquipmentArea3_scrollView.Basic_ClickBtn(-1);

        EquipmentArea3_scrollView.Basic_CheckCover();
    }

    ////////// Unity            //////////

    void Equipment_Start()
    {
        // Area2
        Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MASTER__UI__RAW_IMAGE, EquipmentArea2_rawParent);

        // Area3
        EquipmentArea3_menuBar.Basic_Init(this);
        for(int for0 = 0; for0 < EquipmentArea3_menuBar.Basic_VarListCount; for0++)
        {
            EquipmentArea3_menuBar.Basic_GetListBtn(for0).name = "Button/PINOCCHIO/EQUIPMENT__MENU_BAR/" + for0.ToString();
        }

        EquipmentArea3_listArray.Basic_Init();

        EquipmentArea3_scrollView.Basic_Init(this);

        //
        Equipment_inventoryItems = new List<int>();
    }

    #endregion

    #region SKILL

    public enum Skill_SELECT_TYPE
    {
        SLOT = 0,
        LIST
    }

    [Serializable]
    public class ScrollView_Skill : Giggle_UI.ScrollView
    {
        [SerializeField] Giggle_MainManager__Pinocchio  Basic_parentClass;

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////

        public void Basic_Init(Giggle_MainManager__Pinocchio _parentClass)
        {
            Basic_parentClass = _parentClass;
            Basic_Init();
        }

        protected override void Basic_Init__SetName()
        {
            Basic_list[0].Find("Button").name = "Button/PINOCCHIO/SKILL__SCROLL_VIEW/0";
            Basic_list[1].Find("Button").name = "Button/PINOCCHIO/SKILL__SCROLL_VIEW/1";
        }

        protected override void Basic_AddList__SetName(Transform _element)
        {
            _element.Find("Button").name = "Button/PINOCCHIO/SKILL__SCROLL_VIEW/" + Basic_list.Count;
        }

        public override void Basic_ClickBtn(int _count)
        {
            // ui 그래픽 갱신
            for(int for0 = 0; for0 < Basic_list.Count; for0++)
            {
                Basic_list[for0].Find("Select").gameObject.SetActive(for0.Equals(_count));
            }

            //
            Basic_parentClass.Skill_SelectSkill(_count);
        }

        // Basic_ListSetting
        public void Basic_ListSetting()
        {
            int finalCount = 0;
            int whileCount = 0;
            List<Giggle_Player.Pinocchio_Skill> list = (List<Giggle_Player.Pinocchio_Skill>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__VAR_SKILLS);
            while(whileCount < Basic_list.Count)
            {
                if(whileCount < list.Count)
                {
                    Debug.Log("Basic_ListSetting " + list[whileCount].Basic_VarId);
                    Basic_list[whileCount].gameObject.SetActive(true);

                    // 기존 데이터 날리기
                    Basic_list[whileCount].GetChild(1).GetComponent<Image>().sprite = null;
                    Basic_list[whileCount].GetChild(1).Find("Portrait").GetComponent<Image>().sprite = null;
                    Basic_list[whileCount].GetChild(1).Find("Name").GetComponent<TextMeshProUGUI>().text = "";

                    //TODO:아이템 포트레이트가 아직 안나왔어요!
                    Giggle_Character.Skill database = (Giggle_Character.Skill)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                        Giggle_ScriptBridge.EVENT.DATABASE__PINOCCHIO__GET_SKILL_FROM_ID,
                        //
                        list[whileCount].Basic_VarId);
                    Basic_list[whileCount].GetChild(1).GetComponent<Image>().sprite
                        = (Sprite)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                            Giggle_ScriptBridge.EVENT.DATABASE__CHARACTER__GET_SKILL_BACK_FROM_RANK,
                            //
                            database.Basic_VarRank);
                    Basic_list[whileCount].GetChild(1).Find("Name").GetComponent<TextMeshProUGUI>().text = database.Basic_VarName;

                    //
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

            Basic_ClickBtn(-1);
        }

        //
        public void Basic_CheckCover()
        {
            List<Giggle_Player.Pinocchio_Skill> skillList
                = (List<Giggle_Player.Pinocchio_Skill>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__VAR_SKILLS);
            List<int> skillSlots
                = (List<int>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__SKILL_VAR_SKILL_SLOTS);

            int whileCount = 0;
            while(whileCount < skillList.Count)
            {
                Basic_list[whileCount].Find("Cover").gameObject.SetActive(false);

                //
                for(int for0 = 0; for0 < skillSlots.Count; for0++)
                {
                    if(skillList[whileCount].Basic_VarId.Equals(skillSlots[for0]))
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

    [Header("SKILL ==========")]
    [SerializeField] List<Transform>    SkillArea2_slots;

    [SerializeField] GameObject             SkillArea2_info;
    [SerializeField] Image                  SkillArea2_infoInfoPortrait;
    [SerializeField] List<TextMeshProUGUI>  SkillArea2_infoInfoTexts;
    [SerializeField] List<Transform>        SkillArea2_infoBtns;

    [SerializeField] ScrollView_Skill   SkillArea3_scrollView;
    [SerializeField] Image              SkillArea3_cover;

    [Header("RUNNING")]
    [SerializeField] Skill_SELECT_TYPE  Skill_selectType;
    [SerializeField] int                Skill_selectNumber;

    ////////// Getter & Setter  //////////
    
    ////////// Method           //////////
            
    void Skill_Active()
    {
        Skill_Reset();
    }

    public void Skill_SelectSkill(int _count)
    {
        Skill_selectType = Skill_SELECT_TYPE.LIST;
        Skill_selectNumber = _count;

        Skill_InfoOn();
    }

    void Skill_BtnSlot(int _count)
    {
        if(Skill_selectNumber.Equals(-1))
        {
            Skill_selectType = Skill_SELECT_TYPE.SLOT;
            Skill_selectNumber = _count;
            
            Skill_InfoOn();
        }
        else
        {
            List<Giggle_Player.Pinocchio_Skill> skills
                = (List<Giggle_Player.Pinocchio_Skill>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__VAR_SKILLS);
            Giggle_Character.Skill data = (Giggle_Character.Skill)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.DATABASE__PINOCCHIO__GET_SKILL_FROM_ID,
                    skills[Skill_selectNumber].Basic_VarId);

            Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__SKILL_VAR_SKILL_SLOT_FROM_COUNT,
                //
                _count, skills[Skill_selectNumber].Basic_VarId);

            Skill_Reset();
        }
    }

    void Skill_BtnPreset(int _count)
    {
        Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__SKILL_VAR_SELECT_SKILL_SLOT, _count);
        
        //
        Skill_Reset();
    }

    // Info
    // 정보창이 뜰 때 기본 정보 갱신
    void Skill_InfoOn()
    {
        if(!Skill_selectNumber.Equals(-1))
        {
            bool isInfoOn = true;

            switch(Skill_selectType)
            {
                case Skill_SELECT_TYPE.SLOT:
                    {
                        List<int> slots = (List<int>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                            Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__SKILL_VAR_SKILL_SLOTS);

                        if(!slots[Skill_selectNumber].Equals(-1))
                        {
                            Giggle_Player.Pinocchio_Skill skill
                                = (Giggle_Player.Pinocchio_Skill)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                                    Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__VAR_SKILL_FROM_ID,
                                    //
                                    slots[Skill_selectNumber]);
                            
                            Giggle_Character.Skill data = (Giggle_Character.Skill)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                                Giggle_ScriptBridge.EVENT.DATABASE__PINOCCHIO__GET_SKILL_FROM_ID,
                                //
                                skill.Basic_VarId);

                            //
                            SkillArea2_infoInfoTexts[0].text = data.Basic_VarRank.ToString();
                            SkillArea2_infoInfoTexts[1].text = data.Basic_VarName;
                            SkillArea2_infoInfoTexts[2].text = "Lv " + skill.Basic_VarLv + "/" + data.Basic_VarLvCount;
                        }
                        else
                        {
                            Skill_selectNumber = -1;
                            isInfoOn = false;
                        }
                    }
                    break;
                case Skill_SELECT_TYPE.LIST:
                    {
                        List<Giggle_Player.Pinocchio_Skill> skills
                            = (List<Giggle_Player.Pinocchio_Skill>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                                Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__VAR_SKILLS);
                        Giggle_Character.Skill data = (Giggle_Character.Skill)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                                Giggle_ScriptBridge.EVENT.DATABASE__PINOCCHIO__GET_SKILL_FROM_ID,
                                skills[Skill_selectNumber].Basic_VarId);

                        SkillArea2_infoInfoTexts[0].text = data.Basic_VarRank.ToString();
                        SkillArea2_infoInfoTexts[1].text = data.Basic_VarName;
                        SkillArea2_infoInfoTexts[2].text = "Lv " + skills[Skill_selectNumber].Basic_VarLv + "/" + data.Basic_VarLvCount;
                    }
                    break;
            }

            if(isInfoOn)
            {
                for(int for0 = 0; for0 < SkillArea2_infoBtns[2].childCount; for0++)
                {
                    SkillArea2_infoBtns[2].GetChild(for0).gameObject.SetActive(for0.Equals((int)Skill_selectType));
                }

                //
                SkillArea2_info.SetActive(true);
            }
        }
    }

    void Skill_InfoBtnAwakening()
    {

    }

    void Skill_InfoBtnLevelUp()
    {

    }

    void Skill_InfoBtnEquip()
    {
        SkillArea2_info.SetActive(false);
        SkillArea3_cover.gameObject.SetActive(true);

        //
        List<Giggle_Player.Pinocchio_Skill> list = (List<Giggle_Player.Pinocchio_Skill>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
            Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__VAR_SKILLS);
        List<int> slots = (List<int>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
            Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__SKILL_VAR_SKILL_SLOTS);

        int for0Res = -1;
        for(int for0 = 0; for0 < slots.Count; for0++)
        {
            if(slots[for0].Equals(-1))
            {
                for0Res = for0;
                break;
            }
        }

        Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
            Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__SKILL_VAR_SKILL_SLOT_FROM_COUNT,
            //
            for0Res, list[Skill_selectNumber].Basic_VarId);

        Skill_Reset();
    }

    void Skill_InfoBtnUnEquip()
    {
        List<int> slots = (List<int>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
            Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__SKILL_VAR_SKILL_SLOTS);

        int for0Res = -1;
        for(int for0 = 0; for0 < slots.Count; for0++)
        {
            if(slots[for0].Equals(-1))
            {
                for0Res = for0;
                break;
            }
        }

        Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
            Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__SKILL_VAR_SKILL_SLOT_FROM_COUNT,
            //
            Skill_selectNumber, -1);

        Skill_Reset();
    }

    //
    void Skill_Reset()
    {
        Skill_selectNumber = -1;

        //
        List<int> slots = (List<int>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
            Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__SKILL_VAR_SKILL_SLOTS);
            
        for(int for0 = 0; for0 < SkillArea2_slots.Count; for0++)
        {
            if(slots[for0].Equals(-1))
            {
                SkillArea2_slots[for0].GetChild(1).GetComponent<Image>().sprite = null;
                SkillArea2_slots[for0].GetChild(1).Find("Name").GetComponent<TextMeshProUGUI>().text = "00";
            }
            else
            {
                Giggle_Player.Pinocchio_Skill skill
                    = (Giggle_Player.Pinocchio_Skill)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                        Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__VAR_SKILL_FROM_ID,
                        //
                        slots[for0]);
                
                Giggle_Character.Skill data = (Giggle_Character.Skill)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.DATABASE__PINOCCHIO__GET_SKILL_FROM_ID,
                    //
                    skill.Basic_VarId);

                SkillArea2_slots[for0].GetChild(1).GetComponent<Image>().sprite
                    = (Sprite)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                        Giggle_ScriptBridge.EVENT.DATABASE__CHARACTER__GET_SKILL_BACK_FROM_RANK,
                        //
                        data.Basic_VarRank);
                SkillArea2_slots[for0].GetChild(1).Find("Name").GetComponent<TextMeshProUGUI>().text = data.Basic_VarName;
            }
        }
        SkillArea2_info.SetActive(false);

        SkillArea3_scrollView.Basic_ListSetting();
        SkillArea3_scrollView.Basic_ClickBtn(-1);
        SkillArea3_cover.gameObject.SetActive(false);
    }

    ////////// Unity            //////////
            
    void Skill_Start()
    {
        // SkillArea2_info
        SkillArea2_info.SetActive(false);

        SkillArea2_infoInfoPortrait = SkillArea2_info.transform.Find("Info").Find("Portrait").GetComponent<Image>();
        if(SkillArea2_infoInfoTexts == null)
        {
            SkillArea2_infoInfoTexts = new List<TextMeshProUGUI>();
        }
        SkillArea2_infoInfoTexts.Add(SkillArea2_info.transform.Find("Info").Find("Type" ).GetComponent<TextMeshProUGUI>());
        SkillArea2_infoInfoTexts.Add(SkillArea2_info.transform.Find("Info").Find("Name" ).GetComponent<TextMeshProUGUI>());
        SkillArea2_infoInfoTexts.Add(SkillArea2_info.transform.Find("Info").Find("Lv"   ).GetComponent<TextMeshProUGUI>());

        if(SkillArea2_infoBtns == null)
        {
            SkillArea2_infoBtns = new List<Transform>();
        }
        SkillArea2_infoBtns.Add(SkillArea2_info.transform.Find("Btns").Find("Awakening"));
        SkillArea2_infoBtns.Add(SkillArea2_info.transform.Find("Btns").Find("Level"));
        SkillArea2_infoBtns.Add(SkillArea2_info.transform.Find("Btns").Find("Equip"));

        //
        SkillArea3_scrollView.Basic_Init(this);
    }

    #endregion

    #region ATTRIBUTE

    [Serializable]
    public class Attribute_AttackScrollView : Giggle_UI.ScrollView_Attribute
    {

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////

        protected override IEnumerator Basic_Init__Override(Vector2 _startPos, Vector2 _varPos)
        {
            Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.DATABASE__PINOCCHIO__GET_DATA_FROM_ID,
                1111);
            
            bool isWhile = true;
            while(isWhile)
            {
                if( (bool)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                        Giggle_ScriptBridge.EVENT.DATABASE__PINOCCHIO__GET_IS_OPEN))
                {
                    isWhile = false;
                }

                yield return null;
            }

            List<Giggle_Character.Attribute> list
                = (List<Giggle_Character.Attribute>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.DATABASE__PINOCCHIO__GET_ATTRIBUTE,
                    Giggle_Player.ATTRIBUTE_TYPE.ATTACK);
            
            //
            int x = 0;
            int y = 0;
            
            for(int for0 = 0; for0 < list.Count; for0++)
            {
                // x값
                if(list[for0].Basic_VarConditionId.Equals(-1))
                {
                    x++;
                }
                // y값
                else
                {
                    Giggle_Character.Attribute element = list[for0];
                    int tempY = 1;

                    while(!element.Basic_VarConditionId.Equals(-1))
                    {
                        tempY += element.Basic_VarConditionLv / 5;

                        for(int for1 = 0; for1 < list.Count; for1++)
                        {
                            if(list[for1].Basic_VarId.Equals(element.Basic_VarConditionId))
                            {
                                element = list[for1];
                                break;
                            }
                        }
                    }

                    if(y < tempY)
                    {
                        y = tempY;
                    }
                }
            }
            
            // ui생성
            int totalCount = x * y;
            while(Basic_list.Count < totalCount)
            {
                Transform element = GameObject.Instantiate(Basic_list[0], Basic_list[0].parent);
                Basic_list.Add(element);
            }

            for(int for0 = 0; for0 < Basic_list.Count; for0++)
            {
                Basic_list[for0].localPosition = new Vector3(_startPos.x + (_varPos.x * (for0 % x)), _startPos.y + (_varPos.y * (for0 / x)), 0);
                Basic_list[for0].Find("ButtonElement").gameObject.SetActive(false);
                Basic_list[for0].Find("Root").gameObject.SetActive(false);
            }

            // 특성들 배치
            int uiCount = 0;

            List<int> waitting = new List<int>();

            for(int for0 = 0; for0 < list.Count; for0++)
            {
                if(list[for0].Basic_VarConditionId.Equals(-1))
                {
                    Basic_list[for0].Find("ButtonElement").gameObject.SetActive(true);
                    Basic_list[uiCount].GetChild(1).GetChild(0).name = "Button/PINOCCHIO/ATTRIBUTE__ATTACK_SCROLL_VIEW/" + list[for0].Basic_VarId.ToString();
                    uiCount++;
                }
                else
                {
                    waitting.Add(for0);
                }
            }

            uiCount = 0;
            int whileCount = 0;
            bool isInsert = false;
            while(waitting.Count > 0)
            {
                isInsert = false;

                for(int for0 = 0; for0 < Basic_list.Count; for0++)
                {
                    int id = int.Parse(Basic_list[for0].GetChild(1).GetChild(0).name.Split('/')[3]);

                    if(list[waitting[whileCount]].Basic_VarConditionId.Equals(id))
                    {
                        isInsert = true;
                        uiCount = for0;
                        break;
                    }
                }

                //
                if(isInsert)
                {
                    int count = uiCount + x;

                    Basic_list[count].Find("ButtonElement").gameObject.SetActive(true);
                    Basic_list[count].GetChild(1).GetChild(0).name = "Button/PINOCCHIO/ATTRIBUTE__ATTACK_SCROLL_VIEW/" + list[waitting[whileCount]].Basic_VarId.ToString();
                    if(count >= x)
                    {
                        Basic_list[count - x].Find("Root").gameObject.SetActive(true);
                    }
                    waitting.RemoveAt(whileCount);
                }
                else
                {
                    whileCount++;
                    if(whileCount >= waitting.Count)
                    {
                        whileCount = 0;
                    }
                }
            }

            //
            Basic_UICheck();
        }

        // Basic_UICheck
        public void Basic_UICheck()
        {
            List<Giggle_Character.Attribute> list
                = (List<Giggle_Character.Attribute>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.DATABASE__PINOCCHIO__GET_ATTRIBUTE,
                    Giggle_Player.ATTRIBUTE_TYPE.ATTACK);

            for(int for0 = 0; for0 < list.Count; for0++)
            {
                Transform ui = Basic_GetListElementFromId(list[for0].Basic_VarId);

                Giggle_Player.Pinocchio_Skill element
                    = (Giggle_Player.Pinocchio_Skill)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                        Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__ATTRIBUTE_VAR_FROM_TYPE_AND_ID,
                        //
                        Giggle_Player.ATTRIBUTE_TYPE.ATTACK, list[for0].Basic_VarConditionId);

                if(list[for0].Basic_VarConditionId != -1)
                {
                    if(list[for0].Basic_VarConditionLv <= element.Basic_VarLv)
                    {
                        Basic_UICheck__SettingUI(list[for0].Basic_VarId);

                        ui.Find("ButtonElement").Find("Cover").gameObject.SetActive(false);
                    }
                    else
                    {
                        ui.Find("ButtonElement").Find("Cover").gameObject.SetActive(true);
                    }
                }
                else
                {
                    Basic_UICheck__SettingUI(list[for0].Basic_VarId);
                }
            }
        }

        void Basic_UICheck__SettingUI(int _id)
        {
            Transform ui = Basic_GetListElementFromId(_id);

            Giggle_Player.Pinocchio_Skill element
                = (Giggle_Player.Pinocchio_Skill)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__ATTRIBUTE_VAR_FROM_TYPE_AND_ID,
                    //
                    Giggle_Player.ATTRIBUTE_TYPE.ATTACK, _id);

            ui.Find("ButtonElement").Find("Level").GetComponent<TextMeshProUGUI>().text = element.Basic_VarLv.ToString();
        }

        ////////// Constructor & Destroyer  //////////
    }

    [Header("ATTRIBUTE ==========")]
    [SerializeField] Attribute_AttackScrollView Attribute_atttack;

    [SerializeField] GameObject         Attribute_info;
    [SerializeField] Image              Attribute_infoPortrait;
    [SerializeField] TextMeshProUGUI    Attribute_infoType;
    [SerializeField] TextMeshProUGUI    Attribute_infoName;
    [SerializeField] TextMeshProUGUI    Attribute_infoLevel;

    [Header("RUNNING")]
    [SerializeField] Giggle_Player.ATTRIBUTE_TYPE   Attribute_selectType;
    [SerializeField] int                            Attribute_selectId;

    ////////// Getter & Setter  //////////
    
    ////////// Method           //////////

    void Attribute_BtnClose()
    {
        Attribute_info.SetActive(false);
    }

    void Attribute_BtnAttackElement(int _id)
    {
        Attribute_selectType = Giggle_Player.ATTRIBUTE_TYPE.ATTACK;
        Attribute_selectId = _id;

        Attribute_InfoUI();

        Attribute_info.transform.position = Attribute_atttack.Basic_VarInfo.position;
        Attribute_info.SetActive(true);
    }

    void Attribute_BtnDefenceElement(int _id)
    {

    }

    void Attribute_BtnSupportElement(int _id)
    {

    }

    void Attribute_BtnInfoAwakening()
    {

    }

    void Attribute_BtnLevelUp()
    {
        Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
            Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__ATTRIBUTE_LEVEL_UP,
            //
            Attribute_selectType, Attribute_selectId);
        
        Attribute_InfoUI();
        switch(Attribute_selectType)
        {
            case Giggle_Player.ATTRIBUTE_TYPE.ATTACK:   { Attribute_atttack.Basic_UICheck();    }   break;
        }
    }

    //
    void Attribute_InfoUI()
    {
        Giggle_Player.Pinocchio_Skill element
            = (Giggle_Player.Pinocchio_Skill)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__ATTRIBUTE_VAR_FROM_TYPE_AND_ID,
                //
                Attribute_selectType, Attribute_selectId);
        
        Giggle_Character.Attribute data
            = (Giggle_Character.Attribute)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.DATABASE__PINOCCHIO__GET_ATTRIBUTE_FROM_ID,
                //
                Attribute_selectType, Attribute_selectId);

        Attribute_infoName.text = data.Basic_VarName;
        Attribute_infoLevel.text = "Lv. " + element.Basic_VarLv + "/" + data.Basic_VarLvListCount;
    }

    ////////// Unity            //////////
    void Attribute_Start()
    {
        Attribute_BtnClose();

        Attribute_atttack.Basic_Init();
    }

    #endregion

    #region ABILITY

    [Serializable]
    public class ScrollerView_MenuBar__AbilityMarionette : Giggle_UI.MenuBar
    {

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////

        protected override void Basic_SelectMenu__Setting(int _for0, int _count)
        {
        }

        ////////// Constructor & Destroyer  //////////
    }

    [Serializable]
    public class ScrollView__AbilityMarionette : Giggle_UI.ScrollView
    {
        [SerializeField] Giggle_MainManager__Pinocchio Basic_uiData;

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////

        protected override void Basic_Init__SetName()
        {
            Basic_list[0].Find("Button").name = "Button/PINOCCHIO/ABILITY__MARIONETTE_LIST_SCROLL_VIEW/0";
            Basic_list[1].Find("Button").name = "Button/PINOCCHIO/ABILITY__MARIONETTE_LIST_SCROLL_VIEW/1";
        }

        protected override void Basic_AddList__SetName(Transform _element)
        {
            _element.Find("Button").name = "Button/PINOCCHIO/ABILITY__MARIONETTE_LIST_SCROLL_VIEW/" + Basic_list.Count;
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
        public void Basic_SelectMenuBar()
        {
            Basic_SelectMenuBar__Check();

            Basic_ClickBtn(-1);
        }

        void Basic_SelectMenuBar__Check()
        {
            List<int> list = Basic_uiData.Ability_VarMarionetteListDatas;

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
            List<int>   characterList   = Basic_uiData.Ability_VarMarionetteListDatas;

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
        public void Basic_Init(Giggle_MainManager__Pinocchio _uiData)
        {
            Basic_Init();
            Basic_uiData = _uiData;
        }

        ////////// Constructor & Destroyer  //////////

    }

    [Header("ABILITY ==========")]
    // Area2
    [SerializeField] List<Transform>    Ability_uis;

    [SerializeField] GameObject                                 Ability_marionetteList;
    [SerializeField] ScrollerView_MenuBar__AbilityMarionette    Ability_marionetteListMenuBar;
    [SerializeField] ScrollView__AbilityMarionette              Ability_marionetteListScrollView;
    [SerializeField] Transform                                  Ability_marionetteListScrollViewRawParent;

    // Area3
    [SerializeField] List<Transform>    Ability_woodUis;
    [SerializeField] Transform          Ability_woodRawParent;

    // PopUp
    [SerializeField] Transform          Ability_popUpParent;
    [SerializeField] List<Transform>    Ability_popUpWoodUis;

    [Header("RUNNING")]
    [SerializeField] List<int>  Ability_marionetteListDatas;
    [SerializeField] int        Ability_select;
    [SerializeField] int        Ability_popUpWoodselect;

    ////////// Getter & Setter  //////////
    
    public List<int>    Ability_VarMarionetteListDatas  { get { return Ability_marionetteListDatas; }   }

    ////////// Method           //////////
    
    // Area2
    void Ability_BtnLock(int _count)
    {
        Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
            Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__ABILITY_LOCK,
            //
            _count);
    }

    void Ability_BtnChange()
    {
        Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__ABILITY_CHANGE);

        //
        Ability_UISetting();
    }

    // Ability_MarionetteListBtnMenuBar
    void Ability_MarionetteListBtnMenuBar(int _count)
    {
        Ability_marionetteListMenuBar.Basic_SelectMenu(_count);

        //
        List<Giggle_Character.Save> list = null;
        switch(_count)
        {
            case 0: { list = Ability_MarionetteListBtnMenuBar__All();   }   break;
            case 1: {   }   break;
            case 2: {   }   break;
            case 3: {   }   break;
        }

        //
        Ability_marionetteListDatas.Clear();
        for(int for0 = 0; for0 < list.Count; for0++)
        {
            Ability_marionetteListDatas.Add(list[for0].Basic_VarInventoryId);
        }

        //
        Ability_marionetteListScrollView.Basic_SelectMenuBar();
    }

    List<Giggle_Character.Save> Ability_MarionetteListBtnMenuBar__All()
    {
        List<Giggle_Character.Save> res
            = (List<Giggle_Character.Save>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.PLAYER__MARIONETTE__GET_LIST);

        //
        return res;
    }

    // Ability_MarionetteListBtnScrollView
    void Ability_MarionetteListBtnScrollView(int _count)
    {
        Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
            Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__ABILITY_WOOD_WORKER,
            //
            Ability_select, Ability_marionetteListDatas[_count]);

        Ability_marionetteList.SetActive(false);
        Ability_UISetting();
    }

    // Area3
    //
    void Ability_BtnWoodWork(int _count)
    {
        Giggle_Player.Pinocchio_AbilityWood data
            = (Giggle_Player.Pinocchio_AbilityWood)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__ABILITY_WOOD_VAR_DATA_FROM_COUNT,
                //
                _count);

        switch(data.Basic_VarSelect)
        {
            case -1:
                {
                    Ability_select = _count;
                    Ability_PopUpSelectWoodBtnSelect(-1);
                    
                    Ability_marionetteList.SetActive(false);
                    Ability_PopUpActive("SELECT_WOOD");
                }
                break;
            default:
                {
                    data.Basic_WorkEnd();

                    Ability_UISetting();
                }
                break;
        }
    }

    void Ability_BtnWoodWorker(int _count)
    {
        Ability_select = _count;

        Ability_MarionetteListBtnMenuBar(0);
        Ability_marionetteList.SetActive(true);
    }

    //
    void Ability_UISetting()
    {
        // Area2
        int count = (int)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__ABILITY_GET_ABILITYS_COUNT);

        for(int for0 = 0; for0 < count; for0++)
        {
            Giggle_Player.Pinocchio_Ability element
                = (Giggle_Player.Pinocchio_Ability)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__ABILITY_GET_ABILITY_FROM_COUNT,
                    //
                    for0);
            
            if(element.Basic_VarId != -1)
            {
                Giggle_Character.AbilityClass data
                    = (Giggle_Character.AbilityClass)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                        Giggle_ScriptBridge.EVENT.DATABASE__PINOCCHIO__ABILITY_GET_ABILITY_FROM_ELEMENT,
                        //
                        element.Basic_VarId, element.Basic_VarGrade);

                Ability_uis[for0].Find("Text").GetComponent<TextMeshProUGUI>().text = data.Basic_VarName + " " + element.Basic_VarValue;
            }
            else
            {
                Ability_uis[for0].Find("Text").GetComponent<TextMeshProUGUI>().text = "능력 없음";
            }
        }

        Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MASTER__UI__RAW_IMAGE, Ability_marionetteListScrollViewRawParent);
        Ability_marionetteList.SetActive(false);

        // Area3
        for(int for0 = 0; for0 < Ability_woodUis.Count; for0++)
        {
            Giggle_Player.Pinocchio_AbilityWood data
                = (Giggle_Player.Pinocchio_AbilityWood)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__ABILITY_WOOD_VAR_DATA_FROM_COUNT,
                    //
                    for0);

            //
            if(data.Basic_VarSelect == -1)
            {
                Ability_woodUis[for0].GetChild(1).GetChild(0).gameObject.SetActive(true);
                Ability_woodUis[for0].GetChild(1).gameObject.SetActive(true);

                Ability_woodUis[for0].Find("Timer").GetChild(0).GetComponent<TextMeshProUGUI>().text = "대기중";

                Ability_woodUis[for0].Find("Worker").gameObject.SetActive(false);
            }
            else
            {
                Ability_woodUis[for0].GetChild(1).gameObject.SetActive(false);
                
                // 나무 스프라이트 교채
                Ability_woodUis[for0].Find("Wood").GetComponent<Image>().sprite
                    = (Sprite)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                            Giggle_ScriptBridge.EVENT.DATABASE__PINOCCHIO__ABILITY_GET_WOOD_ICON_FROM_SELECT,
                            data.Basic_VarSelect);
                Ability_woodUis[for0].Find("Wood").gameObject.SetActive(true);

                // 작업자 배정
                if(data.Basic_VarMarionette == -1)
                {
                    Ability_woodUis[for0].Find("Worker").GetChild(0).gameObject.SetActive(false);
                    Ability_woodUis[for0].Find("Worker").GetChild(1).gameObject.SetActive(true);
                }
                else
                {
                    Ability_woodUis[for0].Find("Worker").GetChild(0).gameObject.SetActive(true);
                    Ability_woodUis[for0].Find("Worker").GetChild(1).gameObject.SetActive(false);

                    // 기존 데이터 날리기
                    while(Ability_woodUis[for0].Find("Worker").Find("Obj").childCount > 0)
                    {
                        Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                            Giggle_ScriptBridge.EVENT.MASTER__GARBAGE__REMOVE,
                            //
                            Ability_woodUis[for0].Find("Worker").Find("Obj").GetChild(0));
                    }

                    //
                    Giggle_Character.Save marionette
                        = (Giggle_Character.Save)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                            Giggle_ScriptBridge.EVENT.PLAYER__MARIONETTE__GET_DATA_FROM_INVENTORYID,
                            data.Basic_VarMarionette);

                    Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                        Giggle_ScriptBridge.EVENT.MASTER__UI__CHARACTER_INSTANTIATE,
                        //
                        marionette.Basic_VarDataId,
                        Ability_woodUis[for0].Find("Worker").Find("Obj"), -90.0f, 200.0f);
                }

                // 활성화
                Ability_woodUis[for0].Find("Worker").gameObject.SetActive(true);
            }
        }

        //
        Ability_popUpParent.gameObject.SetActive(false);
    }

    // PopUp ==========
    //
    void Ability_PopUpActive(string _name)
    {
        //
        switch(_name)
        {
            case "SELECT_WOOD": { Ability_PopUpSelectWoodActive();  }   break;
        }

        //
        for(int for0 = 0; for0 < Ability_popUpParent.childCount; for0++)
        {
            Ability_popUpParent.GetChild(for0).gameObject.SetActive(false);
        }
        Ability_popUpParent.Find(_name).gameObject.SetActive(true);

        Ability_popUpParent.gameObject.SetActive(true);
        
    }

    // SELECT_WOOD
    void Ability_PopUpSelectWoodBtnSelect(int _count)
    {
        Ability_popUpWoodselect = _count;

        for(int for0 = 0; for0 < Ability_popUpWoodUis.Count; for0++)
        {
            if(for0.Equals(Ability_popUpWoodselect))
            {
                Ability_popUpWoodUis[for0].Find("0").Find("Select").gameObject.SetActive(true);
            }
            else
            {
                Ability_popUpWoodUis[for0].Find("0").Find("Select").gameObject.SetActive(false);
            }
        }
    }

    void Ability_PopUpSelectWoodBtnAd(int _count)
    {
    }

    void Ability_PopUpSelectWoodBtnOk()
    {
        Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
            Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__ABILITY_WOOD_WORK,
            Ability_select, Ability_popUpWoodselect);

        //
        Ability_UISetting();
    }

    void Ability_PopUpSelectWoodBtnCancel()
    {
        Ability_popUpParent.gameObject.SetActive(false);
    }

    void Ability_PopUpSelectWoodActive()
    {
        Ability_popUpWoodselect = -1;

        for(int for0 = 0; for0 < Ability_popUpWoodUis.Count; for0++)
        {
            Ability_popUpWoodUis[for0].Find("0").gameObject.SetActive(true);
            Ability_popUpWoodUis[for0].Find("1").gameObject.SetActive(false);
        }
    }

    ////////// Unity            //////////

    void Ability_Start()
    {
        //
        Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MASTER__UI__RAW_IMAGE, Ability_woodRawParent);

        //
        if(Ability_marionetteListDatas == null)
        {
            Ability_marionetteListDatas = new List<int>();
        }

        Ability_marionetteList.SetActive(false);

        Ability_marionetteListMenuBar.Basic_Init();
        for(int for0 = 0; for0 < Ability_marionetteListMenuBar.Basic_VarListCount; for0++)
        {
            Ability_marionetteListMenuBar.Basic_GetListBtn(for0).name = "Button/PINOCCHIO/ABILITY__MARIONETTE_LIST_MENU_BAR/" + for0.ToString();
        }

        Ability_marionetteListScrollView.Basic_Init(this);

        //
        for(int for0 = 0; for0 < Ability_woodUis.Count; for0++)
        {
            Ability_woodUis[for0].Find("Timer").GetChild(0).GetComponent<TextMeshProUGUI>().text = "대기중";
        }
    }

    void Ability_Coroutine()
    {
        //
        for(int for0 = 0; for0 < Ability_woodUis.Count; for0++)
        {
            if(!Ability_woodUis[for0].GetChild(1).gameObject.activeSelf)
            {
                Giggle_Player.Pinocchio_AbilityWood data
                    = (Giggle_Player.Pinocchio_AbilityWood)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                        Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__ABILITY_WOOD_VAR_DATA_FROM_COUNT,
                        //
                        for0);

                //
                if(data.Basic_VarSelect == -1)
                {
                    Ability_woodUis[for0].Find("Timer").GetChild(0).GetComponent<TextMeshProUGUI>().text = "대기중";
                }
                else
                {
                    TimeSpan ts = data.Basic_VarEndTime - DateTime.Now;

                    if(ts.TotalSeconds > 0)
                    {
                        Ability_woodUis[for0].Find("Timer").GetChild(0).GetComponent<TextMeshProUGUI>().text = ts.Hours + ":" + ts.Minutes + ":" + ts.Seconds;
                    }
                    else
                    {
                        Ability_woodUis[for0].Find("Timer").GetChild(0).GetComponent<TextMeshProUGUI>().text = "작업완료";

                        Ability_woodUis[for0].GetChild(1).GetChild(0).gameObject.SetActive(false);
                        Ability_woodUis[for0].GetChild(1).gameObject.SetActive(true);
                    }
                }
            }
        }
    }

    #endregion

    #region RELIC

    [Serializable]
    public class ScrollView_Relic : Giggle_UI.ScrollView
    {

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////

        //public void Basic_Init(Pinocchio _parentClass)
        //{
        //    Basic_parentClass = _parentClass;
        //    Basic_Init();
        //}

        protected override void Basic_Init__SetName()
        {
            Basic_list[0].Find("Button").name = "Button/PINOCCHIO/RELIC__SCROLL_VIEW/0";
            Basic_list[1].Find("Button").name = "Button/PINOCCHIO/RELIC__SCROLL_VIEW/1";
        }

        protected override void Basic_AddList__SetName(Transform _element)
        {
            _element.Find("Button").name = "Button/PINOCCHIO/RELIC__SCROLL_VIEW/" + Basic_list.Count;
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
        public void Basic_SelectMenuBar()
        {
            List<Giggle_Player.Pinocchio_Relic> relics
                = (List<Giggle_Player.Pinocchio_Relic>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__RELIC_VAR_RELICS);

            int finalCount = 0;
            int whileCount = 0;
            while(whileCount < Basic_list.Count)
            {
                if(whileCount < relics.Count)
                {
                    Basic_list[whileCount].gameObject.SetActive(true);

                    // 기존 데이터 날리기
                    Basic_list[whileCount].Find("Portrait").GetComponent<Image>().sprite = null;
                    Basic_list[whileCount].Find("Name").GetComponent<TextMeshProUGUI>().text = "";

                    //TODO:유물 포트레이트가 아직 안나왔어요!
                    Giggle_Item.Relic database = (Giggle_Item.Relic)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                        Giggle_ScriptBridge.EVENT.DATABASE__PINOCCHIO__RELIC_VAR_DATA_FROM_ID,
                        //
                        relics[whileCount].Basic_VarDataId);
                    Basic_list[whileCount].Find("Name").GetComponent<TextMeshProUGUI>().text = database.Basic_VarName;

                    //
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

            Basic_ClickBtn(-1);
        }

        //
        public void Basic_CheckCover()
        {
            List<Giggle_Player.Pinocchio_Relic> relics
                = (List<Giggle_Player.Pinocchio_Relic>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__RELIC_VAR_RELICS);
            List<Giggle_Player.Pinocchio_RelicSlot> relicSlots
                = (List<Giggle_Player.Pinocchio_RelicSlot>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__RELIC_VAR_RELIC_SLOTS);

            int whileCount = 0;
            while(whileCount < relics.Count)
            {
                Basic_list[whileCount].Find("Cover").gameObject.SetActive(false);

                //
                for(int for0 = 0; for0 < relicSlots.Count; for0++)
                {
                    if(relicSlots[for0].Basic_VarInventoryId.Equals(relics[whileCount].Basic_VarInventoryId))
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

    [Header("RELIC ==========")]
    [SerializeField] Transform          Relic_lines;
    [SerializeField] Transform          Relic_btns;
    [SerializeField] ScrollView_Relic   Relic_scrollView;

    [Header("RUNNING")]
    [SerializeField] int    Relic_selectSlot;
    [SerializeField] int    Relic_selectList;

    ////////// Getter & Setter  //////////
    
    ////////// Method           //////////
    // Relic_BtnSlot
    public void Relic_BtnSlot(int _count)
    {
        Relic_selectSlot = _count;

        if(!Relic_selectSlot.Equals(-1))
        {
            if(Relic_selectList.Equals(-1))
            {
                Relic_BtnSlot__ui();
            }
            else
            {
                Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__RELIC_SLOT_CHANGE,
                    //
                    Relic_selectList, Relic_selectSlot);
                Relic_Reset();
            }
        }
        else
        {
            Relic_BtnSlot__ui();
        }
    }

    void Relic_BtnSlot__ui()
    {
        Transform forTrans = null;
        for(int for0 = 0; for0 < Relic_btns.childCount; for0++)
        {
            forTrans = Relic_btns.Find("Btn_" + for0);

            forTrans.Find("Select").gameObject.SetActive(for0.Equals(Relic_selectSlot));
        }
    }

    // Relic_BtnScrollView
    public void Relic_BtnScrollView(int _count)
    {
        Relic_selectList = _count;

        //
        if(!Relic_selectList.Equals(-1))
        {
            if(Relic_selectSlot.Equals(-1))
            {
                Relic_scrollView.Basic_ClickBtn(Relic_selectList);
            }
            else
            {
                Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__RELIC_SLOT_CHANGE,
                    //
                    Relic_selectList, Relic_selectSlot);
                Relic_Reset();
            }
        }
        else
        {
            Relic_scrollView.Basic_ClickBtn(Relic_selectList);
        }
    }

    void Relic_Active()
    {
        Relic_Reset();
    }

    void Relic_Reset()
    {
        // 슬롯 갱신
        List<Giggle_Player.Pinocchio_RelicSlot> relicSlots
            = (List<Giggle_Player.Pinocchio_RelicSlot>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__RELIC_VAR_RELIC_SLOTS);
        List<Giggle_Player.Pinocchio_Relic> relics
            = (List<Giggle_Player.Pinocchio_Relic>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__RELIC_VAR_RELICS);

        Transform forTrans = null;
        for(int for0 = 0; for0 < relicSlots.Count; for0++)
        {
            forTrans = Relic_btns.Find("Btn_" + for0).GetChild(1);
            switch(relicSlots[for0].Basic_VarInventoryId)
            {
                case -1:
                    {
                        forTrans.Find("Name").GetComponent<TextMeshProUGUI>().text = "";
                        forTrans.Find("Protrait").GetComponent<Image>().sprite = null;
                    }
                    break;
                default:
                    {
                        for(int for1 = 0; for1 < relics.Count; for1++)
                        {
                            if(relics[for1].Basic_VarInventoryId.Equals(relicSlots[for0].Basic_VarInventoryId))
                            {
                                Giggle_Item.Relic database = (Giggle_Item.Relic)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                                    Giggle_ScriptBridge.EVENT.DATABASE__PINOCCHIO__RELIC_VAR_DATA_FROM_ID,
                                    //
                                    relics[for1].Basic_VarDataId);
                                forTrans.Find("Name").GetComponent<TextMeshProUGUI>().text = database.Basic_VarName;
                                forTrans.Find("Protrait").GetComponent<Image>().sprite = null;
                            }
                        }
                    }
                    break;
            }
        }

        for(int for0 = 0; for0 < Relic_lines.childCount; for0++)
        {
            forTrans = Relic_lines.GetChild(for0);
            if(forTrans.gameObject.activeSelf)
            {
                bool isBuff = (bool)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__RELIC_VAR_RELIC_SLOT_BUFF_FROM_COUNT,
                    //
                    int.Parse(forTrans.name));
                
                if(isBuff)  { Relic_lines.GetChild(for0).GetComponent<Image>().color = Color.yellow;    }
                else        { Relic_lines.GetChild(for0).GetComponent<Image>().color = Color.white;     }
            }
        }

        Relic_scrollView.Basic_SelectMenuBar();
        
        //
        Relic_BtnSlot(-1);
        Relic_BtnScrollView(-1);
    }

    ////////// Unity            //////////
    void Relic_Start()
    {
        // Area2

        // Area3
        Relic_scrollView.Basic_Init();
    }

    #endregion

    #region AAAA

    ////////// Getter & Setter  //////////
    
    ////////// Method           //////////

    ////////// Unity            //////////

    #endregion
}