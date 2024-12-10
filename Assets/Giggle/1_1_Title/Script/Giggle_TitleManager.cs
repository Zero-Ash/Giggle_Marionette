using UnityEngine;

//
using System;
using System.Collections.Generic;
using System.Collections;
using TMPro;

public class Giggle_TitleManager : Giggle_SceneManager
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
    public partial class UI_MainBasicData : UI_BasicData
    {
        #region BASIC

        [SerializeField] Giggle_TitleManager Basic_manager;

        [SerializeField] RectTransform  Basic_safeArea;

        IEnumerator Basic_coroutine;

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////

        ////////// Constructor & Destroyer  //////////
        
        public override void Basic_Init()
        {
            base.Basic_Init();
            //
            LogIn_Init();

            //
            Basic_coroutine = Basic_Coroutine();
            Basic_manager.StartCoroutine(Basic_coroutine);
        }

        IEnumerator Basic_Coroutine()
        {
            int phase = 0;

            while(phase != -1)
            {
                if(
                    Giggle_ScriptBridge.Basic_VarInstance.Basic_GetIsInMethod(
                        Giggle_ScriptBridge.EVENT.MASTER__UI__SAFE_AREA_VAR_SIZE_DELTA))
                {
                    Basic_safeArea.sizeDelta        = (Vector2)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MASTER__UI__SAFE_AREA_VAR_SIZE_DELTA );
                    Basic_safeArea.localPosition    = (Vector2)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MASTER__UI__SAFE_AREA_VAR_POSITION   );

                    //
                    phase = -1;
                }

                yield return null;
            }
        }

        #endregion

        #region TITLE


        ////////// Getter & Setter          //////////

        ////////// Method                   //////////
        public void Title_BtnClick(string[] _names)
        {
            switch(_names[2])
            {
                case "LOG_IN":  { Title_BtnClick__LOG_IN(); }   break;
            }
        }

        void Title_BtnClick__LOG_IN()
        {
            LogIn_Active();
        }

        ////////// Constructor & Destroyer  //////////

        #endregion

        #region LOG_IN

        [Header("LOG_IN ==================================================")]
        [SerializeField] GameObject LogIn_parent;

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////

        // LogIn_BtnClick
        public void LogIn_BtnClick(string[] _names)
        {
            switch(_names[2])
            {
                //case "OK":      { Title_BtnClick__LOG_IN(); }   break;
                case "CANCEL":  { LogIn_BtnClick__CANCEL(); }   break;
            }
        }

        void LogIn_BtnClick__CANCEL()
        {
            LogIn_parent.SetActive(false);
        }

        //
        void LogIn_Active()
        {
            LogIn_parent.SetActive(true);
        }

        ////////// Constructor & Destroyer  //////////

        void LogIn_Init()
        {
            LogIn_parent.SetActive(false);
        }

        #endregion

        #region SIGN_IN


        ////////// Getter & Setter          //////////

        ////////// Method                   //////////
        public void SignIn_BtnClick(string[] _names)
        {
            switch(_names[2])
            {
                //case "LOG_IN":  { Title_BtnClick__LOG_IN(); }   break;
            }
        }

        ////////// Constructor & Destroyer  //////////

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
            case "TITLE":   { UI_basicData.Title_BtnClick(names);   }   break;

            case "LOG_IN":  { UI_basicData.LogIn_BtnClick(names);   }   break;
            case "SIGN_UP": { UI_basicData.SignIn_BtnClick(names);  }   break;
        }
    }

    ////////// Unity            //////////
    protected override void UI_Start()
    {
        UI_basicData.Basic_Init();
    }

    #endregion
}
