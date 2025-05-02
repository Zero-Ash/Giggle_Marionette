using UnityEngine;

//
using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class Giggle_GuildManager : Giggle_SceneManager
{

    #region BASIC

    ////////// Getter & Setter  //////////

    ////////// Method           //////////
    public override void Basic_Active(bool _isActive)
    {
        base.Basic_Active(_isActive);
        
        //
        
        //
        if(_isActive)
        {
            Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MAIN__POWER_SAVING__ON_OFF, true);
            //UI_Reset();
            UI_basicData.Basic_Active(_isActive);
        }
    }

    ////////// Unity            //////////

    protected override void Basic_Start()
    {
        UI_Start();
    }
    
    #endregion

    #region UI

    [Serializable]
    public partial class UI_MainBasicData : UI_BasicData
    {
        #region BASIC

        [SerializeField] Giggle_GuildManager Basic_manager;

        [SerializeField] Transform   Basic_parent;

        [SerializeField] List<RectTransform>    Basic_safeAreas;

        IEnumerator Basic_coroutine;

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////
        public void Basic_Active(bool _isActive)
        {
            if(_isActive)
            {
                Join_Reset();
            }
        }

        ////////// Constructor & Destroyer  //////////
        
        public override void Basic_Init()
        {
            base.Basic_Init();

            //
            Join_Init();
            Guild_Init();

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
                _phase = -1;
            }
        }
    
        #endregion
        
        #region JOIN

        [Serializable]
        public class MenuBar_Join : Giggle_UI.MenuBar_SelectScene
        {
            [SerializeField] UI_MainBasicData Basic_parentClass;

            ////////// Getter & Setter          //////////

            ////////// Method                   //////////

            public void Basic_Init(UI_MainBasicData _parentClass)
            {
                Basic_parentClass = _parentClass;
                Basic_Init("JOIN", "MENU");
            }

            //protected override void Basic_SelectMenu__Setting(int _for0, int _count)
            //{
            //    if(_for0.Equals(_count))
            //    {
            //        //Basic_parentClass.Job_VarScrollView.Basic_SelectMenuBar(_count);
            //    }
            //}

            ////////// Constructor & Destroyer  //////////
            
        }

        [Serializable]
        public class MenuBar_JoinCreateJoinType : Giggle_UI.MenuBar
        {
            ////////// Getter & Setter          //////////

            ////////// Method                   //////////
            
            protected override void Basic_SelectMenu__Setting(int _for0, int _count)
            {
                if(_for0.Equals(_count))
                {
                    //Basic_parentClass.Job_VarScrollView.Basic_SelectMenuBar(_count);
                }
            }

            ////////// Constructor & Destroyer  //////////
            
        }

        // Join
        [Serializable]
        public class ScrollView_JoinJoin : Giggle_UI.ScrollView
        {
            [SerializeField] UI_MainBasicData   Basic_parentClass;

            ////////// Getter & Setter          //////////

            ////////// Method                   //////////

            //
            public void Basic_Init(UI_MainBasicData _parentClass)
            {
                Basic_parentClass = _parentClass;

                //
                Basic_Init();
            }

            protected override void Basic_AddList__SetName(Transform _element, int _num)
            {
                _element.Find("Button").name = "Button/JOIN/JOIN__SCROLL_VIEW/" + _num;
            }

            // TODO: 길드 가입 실행
            public override void Basic_ClickBtn(int _count)
            {

            }

            // TODO: 길드 리스트 셋팅
            public void Basic_SettingList()
            {
                List<Giggle_Quest.Database> datas
                    = (List<Giggle_Quest.Database>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                        Giggle_ScriptBridge.EVENT.DATABASE__QUEST__GET_DATAS_FROM_TYPE,
                        //
                        0);

                int finalCount = 0;
                int whileCount = 0;
                while(whileCount < Basic_list.Count)
                {
                    if(whileCount < datas.Count)
                    {
                        Basic_list[whileCount].gameObject.SetActive(true);
                    
                        //
                        //Giggle_Quest.Text text
                        //    = (Giggle_Quest.Text)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                        //        Giggle_ScriptBridge.EVENT.DATABASE__QUEST__GET_TEXT,
                        //        //
                        //        datas[whileCount].Basic_VarCompleteConditionType);

                        //Basic_list[whileCount].Find("Rank"      ).GetChild(0).GetComponent<TextMeshProUGUI>().text = text.Basic_PrintTitle(datas[whileCount]);
                        //Basic_list[whileCount].Find("Emblem"    ).GetChild(0).GetComponent<TextMeshProUGUI>().text = text.Basic_PrintDescription(datas[whileCount]);
                        //Basic_list[whileCount].Find("Level"     ).GetChild(0).GetComponent<TextMeshProUGUI>().text = text.Basic_PrintTitle(datas[whileCount]);
                        //Basic_list[whileCount].Find("Name"      ).GetChild(0).GetComponent<TextMeshProUGUI>().text = text.Basic_PrintDescription(datas[whileCount]);
                        //Basic_list[whileCount].Find("Master"    ).GetChild(0).GetComponent<TextMeshProUGUI>().text = text.Basic_PrintTitle(datas[whileCount]);
                        //Basic_list[whileCount].Find("Poeple"    ).GetChild(0).GetComponent<TextMeshProUGUI>().text = text.Basic_PrintDescription(datas[whileCount]);
                        //Basic_list[whileCount].Find("Narrative" ).GetChild(0).GetComponent<TextMeshProUGUI>().text = text.Basic_PrintDescription(datas[whileCount]);
                        
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

            }

            ////////// Constructor & Destroyer  //////////
        }

        [Header("JOIN ==================================================")]
        [SerializeField] MenuBar_Join       JoinArea1_menuBar;

        [SerializeField] List<Image>                JoinCreateArea2_emblems;
        [SerializeField] TMPro.TMP_InputField       JoinCreateArea3_name;
        [SerializeField] MenuBar_JoinCreateJoinType JoinCreateArea3_joinTypeMenuBar;
        [SerializeField] int                        JoinCreateArea3_joinType;
        [SerializeField] TMPro.TMP_InputField       JoinCreateArea3_introduce;

        //
        [SerializeField] ScrollView_JoinJoin    JoinJoinArea2_list;

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////
        public void Join_BtnClick(string[] _names)
        {
            switch(_names[2])
            {
                case "MENU":    { JoinArea1_menuBar.Basic_SelectMenu(int.Parse(_names[3])); }   break;

                case "CREATE__EMBLEM":      { }   break;
                case "CREATE__JOIN_TYPE":   { Join_BtnClick__CreateJoinType(int.Parse(_names[3]));  }   break;
                case "CREATE__CREATE":      { Join_BtnClick__CreateCreate();                        }   break;
            }
        }

        void Join_BtnClick__CreateJoinType(int _count)
        {
            JoinCreateArea3_joinTypeMenuBar.Basic_SelectMenu(_count);
            JoinCreateArea3_joinType = _count;
        }

        void Join_BtnClick__CreateCreate()
        {
            Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.MASTER__NETWORK__GUILD_CREATE,
                //
                JoinCreateArea3_name.text, JoinCreateArea3_joinType, JoinCreateArea3_introduce.text );
        }

        //
        void Join_Reset()
        {
            JoinCreateArea3_name.text = "";
            Join_BtnClick__CreateJoinType(0);
            JoinCreateArea3_introduce.text = "";
        }

        ////////// Constructor & Destroyer  //////////
        void Join_Init()
        {
            JoinArea1_menuBar.Basic_Init(this);

            JoinCreateArea3_joinTypeMenuBar.Basic_Init("JOIN", "CREATE__JOIN_TYPE");
        }

        #endregion

        #region GUILD

        [Serializable]
        public class Guild_MenuBar : Giggle_UI.MenuBar
        {

            ////////// Getter & Setter          //////////

            ////////// Method                   //////////

            protected override void Basic_SelectMenu__Setting(int _for0, int _count)
            {
            }

            ////////// Constructor & Destroyer  //////////
        }

        [Header("GUILD ==================================================")]
        [SerializeField] Guild_MenuBar  Guild_menuBar;

        [SerializeField] Transform      Guild_partParent;

        [SerializeField] List<Image>            Guild_frifEmblems;
        [SerializeField] TMPro.TextMeshProUGUI  Guild_frifNarrative;

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////
        public void Guild_BtnClick(string[] _names)
        {
            switch(_names[2])
            {
                case "MENU_BAR":    { Guild_BtnClick__MENU_BAR(_names);     }   break;
                //case "SCROLL_VIEW": { Quest_BtnClick__SCROLL_VIEW(_names);  }   break;
                //case "CANCEL":      { Quest_BtnClick__CANCEL();             }   break;

                case "FRIF__OUT":       {   }   break;
                case "FRIF__DONATION":  {   }   break;
                case "FRIF__CHECK":     {   }   break;
            }
        }

        void Guild_BtnClick__MENU_BAR(string[] _names)
        {
            int select = int.Parse(_names[3]);
            Guild_menuBar.Basic_SelectMenu(select);

            for(int for0 = 0; for0 < Guild_partParent.childCount; for0++)
            {
                Guild_partParent.GetChild(for0).gameObject.SetActive(select.Equals(for0));
            }
        }

        ////////// Constructor & Destroyer  //////////
        void Guild_Init()
        {
            Guild_menuBar.Basic_Init("GUILD", "MENU_BAR");
        }

        #endregion
    }

    [Header("UI ==================================================")]
    [SerializeField] UI_MainBasicData   UI_basicData;

    ////////// Getter & Setter  //////////

    ////////// Method           //////////
    // UI_basicData
    public override void UI_BtnClick(GameObject _btn)
    {
        string[] names = _btn.name.Split('/');
        switch(names[1])
        {
            case "JOIN":    { UI_basicData.Join_BtnClick(names);    }   break;
            case "GUILD":   { UI_basicData.Guild_BtnClick(names);   }   break;
        }
    }

    ////////// Unity            //////////
    protected override void UI_Start()
    {
        UI_basicData.Basic_Init();
    }
    
    #endregion
}