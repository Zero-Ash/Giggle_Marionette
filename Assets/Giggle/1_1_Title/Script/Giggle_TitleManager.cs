using UnityEngine;

//
using System;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using BackEnd;

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

        [SerializeField] List<RectTransform>    Basic_safeAreas;

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
                _phase = 100;
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
            if((int)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MASTER__NETWORK__VAR_INIT_STATE ) == 1)
            {
                LogIn_Active();
            }
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
                case "CANCEL":  { LogIn_BtnClick__CANCEL();     }   break;
                //
                case "SIGN_IN":     { LogIn_BtnClick__SIGN_IN();    }   break;
                case "SIGN_OUT":    { LogIn_BtnClick__SIGN_OUT();   }   break;
                //
                case "TEMP":    { LogIn_BtnClick__TEMP();   }   break;
            }
        }

        void LogIn_BtnClick__CANCEL()
        {
            LogIn_parent.SetActive(false);
        }

        void LogIn_BtnClick__SIGN_IN()
        {
            Backend.BMember.GuestLogin(
                callback =>
                {
                    if(callback.IsSuccess())
                    {
                        Debug.Log("LogIn_BtnClick__SIGN_IN : " + callback.StatusCode);

                        LogIn_parent.SetActive(false);
                        
                        Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.SCENE__LOAD_SCENE,  Giggle_Master.Scene_TYPE.MAIN   );
                    }
                });
        }

        void LogIn_BtnClick__SIGN_OUT()
        {
            Backend.BMember.DeleteGuestInfo();
        }

        void LogIn_BtnClick__TEMP()
        {
            LogIn_parent.SetActive(false);
            
            Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.SCENE__LOAD_SCENE,  Giggle_Master.Scene_TYPE.MAIN   );
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

        [Header("SIGN_IN ==================================================")]
        [SerializeField] GameObject SignIn_parent;


        ////////// Getter & Setter          //////////

        ////////// Method                   //////////
        public void SignIn_BtnClick(string[] _names)
        {
            switch(_names[2])
            {
                //case "LOG_IN":  { Title_BtnClick__LOG_IN(); }   break;
                case "CANCEL":  { SignIn_BtnClick__CANCEL();    }   break;
            }
        }

        void SignIn_BtnClick__CANCEL()
        {
            SignIn_parent.SetActive(false);
        }

        //
        void SignIn_Active()
        {
            SignIn_parent.SetActive(true);
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
