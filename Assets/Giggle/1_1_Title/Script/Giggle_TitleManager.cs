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
        [SerializeField] TMPro.TextMeshProUGUI Temp_text;

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
                _phase = -1;
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
                case "HASH":    { Title_BtnClick__HASH();   }   break;
            }
        }

        void Title_BtnClick__LOG_IN()
        {
            if((int)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MASTER__NETWORK__VAR_INIT_STATE ) == 1)
            {
                LogIn_Active();
            }
        }

        void Title_BtnClick__HASH()
        {
            Temp_text.text = "LogIn_BtnClick__TEMP\n";

            string googleHash = (string)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MASTER__NETWORK__VAR_GOOGLE_HASH    );
            if(string.IsNullOrEmpty(googleHash))
            {
                Temp_text.text += "empty";
            }
            else
            {
                Temp_text.text += googleHash;
            }
        }

        ////////// Constructor & Destroyer  //////////

        #endregion

        #region LOG_IN

        [Serializable]
        public class LogIn_Values
        {
            public int Basic_SignInCoroutinePhase;

            ////////// Getter & Setter          //////////

            ////////// Method                   //////////

            ////////// Constructor & Destroyer  //////////
            public LogIn_Values()
            {
                Basic_SignInCoroutinePhase = 0;
            }
        }

        [Header("LOG_IN ==================================================")]
        [SerializeField] GameObject     LogIn_parent;
        [SerializeField] LogIn_Values   LogIn_values;

        [Header("RUNNING")]
        [SerializeField] IEnumerator    LogIn_SignInCoroutine;

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
                case "GUEST_SIGN_IN":   { LogIn_BtnClick__GUEST_SIGN_IN();  }   break;
                case "GUEST_SIGN_OUT":  { LogIn_BtnClick__GUEST_SIGN_OUT(); }   break;
                //
                case "TEMP":    { LogIn_BtnClick__TEMP();   }   break;
            }
        }

        void LogIn_BtnClick__CANCEL()
        {
            LogIn_parent.SetActive(false);
        }

        ////////// LogIn_BtnClick__GUEST_SIGN_IN
        //
        void LogIn_BtnClick__GUEST_SIGN_IN()
        {
            if(LogIn_SignInCoroutine == null)
            {
                LogIn_SignInCoroutine = LogIn_BtnClick__GUEST_SIGN_IN__Coroutine();
                Basic_manager.StartCoroutine(LogIn_SignInCoroutine);
            }
        }

        IEnumerator LogIn_BtnClick__GUEST_SIGN_IN__Coroutine()
        {
            // 코루틴 초기화
            LogIn_values.Basic_SignInCoroutinePhase = 0;

            // 코루틴 실행
            while(LogIn_values.Basic_SignInCoroutinePhase != -1)
            {
                switch(LogIn_values.Basic_SignInCoroutinePhase)
                {
                    case 0:     { LogIn_BtnClick__GUEST_SIGN_IN__Coroutine0();      }   break;
                    // 최초 접속
                    case 10:    { LogIn_BtnClick__GUEST_SIGN_IN__Coroutine10();     }   break;
                    // 코루틴 종료
                    case 100:   { LogIn_BtnClick__GUEST_SIGN_IN__Coroutine100();    }   break;
                }
                yield return null;
            }

            // 코루틴 종료
            Basic_manager.StopCoroutine(LogIn_SignInCoroutine);
            LogIn_SignInCoroutine = null;
        }

        void LogIn_BtnClick__GUEST_SIGN_IN__Coroutine0()
        {
            LogIn_values.Basic_SignInCoroutinePhase = 1;
            
            //
            Temp_text.text = "LogIn_BtnClick__GUEST_SIGN_IN__Coroutine0";
            Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.MASTER__NETWORK__GUEST_LOG_IN,
                //
                Temp_text, LogIn_values);
        }

        // 최초 접속
        void LogIn_BtnClick__GUEST_SIGN_IN__Coroutine10()
        {
            LogIn_values.Basic_SignInCoroutinePhase = 11;

            //
            Temp_text.text = "LogIn_BtnClick__GUEST_SIGN_IN__Coroutine10 0";
            Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.MASTER__NETWORK__USER_SIGN_UP,
                //
                Temp_text, LogIn_values);
        }

        void LogIn_BtnClick__GUEST_SIGN_IN__Coroutine100()
        {
            LogIn_values.Basic_SignInCoroutinePhase = -1;
            
            //
            Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.PLAYER__BASIC__NETWORK_DATA_LOAD    );

            //
            LogIn_parent.SetActive(false);
            
            Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.SCENE__LOAD_SCENE,  Giggle_Master.Scene_TYPE.MAIN   );
        }

        // LogIn_BtnClick__GUEST_SIGN_OUT
        void LogIn_BtnClick__GUEST_SIGN_OUT()
        {
            Temp_text.text = "LogIn_BtnClick__GUEST_SIGN_OUT";
            Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MASTER__NETWORK__GUEST_DELETE   );
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
            LogIn_values = new LogIn_Values();

            LogIn_SignInCoroutine = null;
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
