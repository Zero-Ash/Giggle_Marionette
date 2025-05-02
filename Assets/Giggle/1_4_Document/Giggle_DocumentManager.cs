using UnityEngine;

//
using System;
using System.Collections.Generic;
using System.Collections;
using TMPro;

public class Giggle_DocumentManager : Giggle_SceneManager
{

    #region BASIC

    ////////// Getter & Setter  //////////

    ////////// Method           //////////
    // Basic_Active
    public override void Basic_Active(bool _isActive)
    {
        base.Basic_Active(_isActive);
        
        //
        if(_isActive)
        {
            Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MAIN__POWER_SAVING__ON_OFF, true);
            StartCoroutine(Basic_Active__Coroutine());
        }
    }

    IEnumerator Basic_Active__Coroutine()
    {
        while(!UI_basicData.Basic_VarIsInit)
        {
            yield return null;
        }

        UI_Reset();
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

        [SerializeField] Giggle_DocumentManager Basic_manager;

        [SerializeField] List<RectTransform>    Basic_safeAreas;

        [Header("RUNNING")]
        [SerializeField] bool   Basic_isInit;

        IEnumerator Basic_coroutine;

        ////////// Getter & Setter          //////////
        public bool Basic_VarIsInit { get { return Basic_isInit;    }   }

        ////////// Method                   //////////

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
                }

                yield return null;
            }

            Basic_isInit = true;
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
                List_Start();

                //
                _phase = -1;
            }
        }

        #endregion

        #region BASIC

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////
        public void Basic_BtnClick(string[] _names)
        {
            switch(_names[2])
            {
                case "BACK":    { Basic_BtnClick__BACK();   }   break;
                case "CLOSE":   { Basic_BtnClick__CLOSE();  }   break;
            }
        }

        void Basic_BtnClick__BACK()
        {
        }

        void Basic_BtnClick__CLOSE()
        {
            if(Info_parent.activeSelf)
            {
                Info_parent.SetActive(false);
            }
            else
            {
                Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.SCENE__LOAD_SCENE,  Giggle_Master.Scene_TYPE.MAIN   );
            }
        }

        //
        public void Basic_Reset()
        {
            Info_parent.SetActive(false);

            //
            List_BtnClick__MENU_BAR(0);
        }

        ////////// Constructor & Destroyer  //////////

        #endregion

        #region LIST

        [Serializable]
        public class ScrollerView_MenuBar__List : Giggle_UI.MenuBar
        {
            [SerializeField] UI_MainBasicData Basic_uiData;

            ////////// Getter & Setter          //////////

            ////////// Method                   //////////

            //protected override void Basic_SelectMenu__Setting(int _for0, int _count)
            //{
            //    if(_for0.Equals(_count))
            //    {
            //        Basic_uiData.List_VarScrollView.Basic_SelectMenuBar(_count);
            //    }
            //}

            ////////// Constructor & Destroyer  //////////
            public void Basic_Init(UI_MainBasicData _uiData)
            {
                Basic_uiData = _uiData;
                Basic_Init("FORMATION", "MENU_BAR");
            }
        }

        [Serializable]
        public class ScrollView_List : Giggle_UI.ScrollView
        {
            [SerializeField] UI_MainBasicData Basic_uiData;

            ////////// Getter & Setter          //////////

            ////////// Method                   //////////
            protected override void Basic_AddList__SetName(Transform _element, int _num)
            {
                _element.Find("Button").name = "Button/LIST/SCROLL_VIEW/" + _num;
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
                //Basic_uiData.List_SelectTypeFormList(_count);
            
                Basic_SelectMenuBar__Check();
            
                Basic_ClickBtn(-1);
            }

            void Basic_SelectMenuBar__Check()
            {
                List<int> list = Basic_uiData.List_VarMarionetteList;
                if(Basic_list.Count < list.Count)
                {
                    Basic_AddList();
                }

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
                        Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                            Giggle_ScriptBridge.EVENT.MASTER__UI__CHARACTER_INSTANTIATE,
                            //
                            list[whileCount],
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
                List<int>   document        = (List<int>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.PLAYER__MARIONETTE__GET_DOCUMENT);
                List<int>   characterList   = Basic_uiData.List_VarMarionetteList;

                int whileCount = 0;
                while(whileCount < characterList.Count)
                {
                    //
                    Basic_list[whileCount].Find("Cover").gameObject.SetActive(true);
                    for(int for0 = 0; for0 < document.Count; for0++)
                    {
                        if( characterList[whileCount].Equals(document[for0]))
                        {
                            Basic_list[whileCount].Find("Cover").gameObject.SetActive(false);
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

        [Header("LIST ==================================================")]
        [SerializeField] ScrollerView_MenuBar__List List_scrollViewMenuBar;
        [SerializeField] ScrollView_List            List_scrollView;

        [Header("RUNNING")]
        [SerializeField] List<int>  List_marionetteList;    // dataId

        ////////// Getter & Setter          //////////
        public List<int>    List_VarMarionetteList  { get { return List_marionetteList; }   }

        public ScrollView_List  List_VarScrollView  { get { return List_scrollView; }   }

        ////////// Method                   //////////
        public void List_BtnClick(string[] _names)
        {
            switch(_names[2])
            {
                case "MENU_BAR":    { List_BtnClick__MENU_BAR(int.Parse(_names[3]));    }   break;
                case "SCROLL_VIEW": { List_BtnClick__SCROLL_VIEW(int.Parse(_names[3])); }   break;
                //case "COSTUME":     { List_BtnClick__COSTUME();     }   break;
            }
        }

        void List_BtnClick__MENU_BAR(int _count)
        {
            List_SelectTypeFormList(_count);

            List_scrollView.Basic_SelectMenuBar(_count);
            List_scrollViewMenuBar.Basic_SelectMenu(-1);
        }

        void List_BtnClick__SCROLL_VIEW(int _count)
        {
            //List_scrollView.Basic_ClickBtn(_count);

            //
            Info_Active(_count);
        }

        //
        public void List_SelectTypeFormList(int _count)
        {
            //
            List<Giggle_Character.Database> list = null;
            switch(_count)
            {
                case 0: { list = List_SelectTypeFromList__All();   }   break;
                case 1: {   }   break;
                case 2: {   }   break;
                case 3: {   }   break;
            }

            //
            List_marionetteList.Clear();
            for(int for0 = 0; for0 < list.Count; for0++)
            {
                List_marionetteList.Add(list[for0].Basic_VarId);
            }
        }

        List<Giggle_Character.Database> List_SelectTypeFromList__All()
        {
            List<Giggle_Character.Database> res
                = (List<Giggle_Character.Database>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.DATABASE__MARIONETTE__GET_DATAS);

            //
            return res;
        }

        //
        void List_Start()
        {
            List_scrollViewMenuBar.Basic_Init(this);
            for(int for0 = 0; for0 < List_scrollViewMenuBar.Basic_VarListCount; for0++)
            {
                List_scrollViewMenuBar.Basic_GetListBtn(for0).name = "Button/LIST/MENU_BAR/" + for0.ToString();
            }

            List_scrollView.Basic_Init(this);
            List_scrollView.Basic_VarContent.gameObject.SetActive(true);

            // Running
            if(List_marionetteList == null)
            {
                List_marionetteList = new List<int>();
            }
        }

        ////////// Constructor & Destroyer  //////////

        #endregion

        #region INFO

        [Header("INFO ==================================================")]
        [SerializeField] GameObject Info_parent;

        [SerializeField] TextMeshProUGUI    Info_name;
        [SerializeField] TextMeshProUGUI    Info_story;

        [SerializeField] Transform  Info_model;
        [SerializeField] Transform  Info_ld;

        [SerializeField] List<Transform>    Info_Costumes;

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////
        public void Info_BtnClick(string[] _names)
        {
            switch(_names[2])
            {
                case "COSTUME":     { Info_BtnClick__COSTUME();     }   break;
                case "COSTUME__OK": { Info_BtnClick__COSTUME__OK(); }   break;
            }
        }

        void Info_BtnClick__COSTUME()
        {
        }

        void Info_BtnClick__COSTUME__OK()
        {
        }

        //
        void Info_Active(int _count)
        {
            Giggle_Character.Database database
                = (Giggle_Character.Database)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.DATABASE__MARIONETTE__GET_DATA_FROM_ID,
                    List_marionetteList[_count]);

            Info_name.text = database.Basic_VarName;
            //
            while(Info_model.childCount > 0)
            {
                Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.MASTER__GARBAGE__REMOVE,
                    Info_model.GetChild(0));
            }
            if(database.Basic_VarSd != null)
            {
                Transform element = Transform.Instantiate(database.Basic_VarSd, Info_model);
                element.localPosition   = new Vector3(0,    0,      0   );
                element.localScale      = new Vector3(200,  200,    200 );
            }
            //
            //
            while(Info_ld.childCount > 0)
            {
                Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.MASTER__GARBAGE__REMOVE,
                    Info_ld.GetChild(0));
            }
            if(database.Basic_VarLd != null)
            {
                Transform element = Transform.Instantiate(database.Basic_VarLd, Info_ld);
                element.localPosition   = new Vector3(0,    0,  0   );
                element.localScale      = new Vector3(1,    1,  1   );
            }

            //
            Info_parent.SetActive(true);
        }

        ////////// Constructor & Destroyer  //////////

        #endregion
    }

    [Header("UI ==================================================")]
    [SerializeField] UI_MainBasicData   UI_basicData;

    ////////// Getter & Setter  //////////

    ////////// Method           //////////
    public override void UI_BtnClick(GameObject _btn)
    {
        string[] names = _btn.name.Split('/');
        switch(names[1])
        {
            case "BASIC":   { UI_basicData.Basic_BtnClick(names);   }   break;
            //
            case "LIST":    { UI_basicData.List_BtnClick(names);    }   break;
            case "INFO":    { UI_basicData.Info_BtnClick(names);    }   break;
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
}
