using UnityEngine;

//
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class Giggle_Master : MonoBehaviour
{
    #region BASIC
    [SerializeField] Giggle_Database Basic_Database;

    ////////// Getter & Setter  //////////

    ////////// Method           //////////
    object Basic_StartCoroutine(params object[] _args)
    {
        IEnumerator coroutine = (IEnumerator)_args[0];

        //
        StartCoroutine(coroutine);

        //
        return true;
    }

    object Basic_StopCoroutine(params object[] _args)
    {
        IEnumerator coroutine = (IEnumerator)_args[0];

        //
        StopCoroutine(coroutine);

        //
        return true;
    }

    ////////// Unity            //////////
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.MASTER__BASIC__START_COROUTINE, Basic_StartCoroutine);
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.MASTER__BASIC__STOP_COROUTINE,  Basic_StopCoroutine );

        if(Basic_Database == null)
        {
            Basic_Database = new Giggle_Database();
        }
        UI_Start();
        Battle_Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion

    #region UI

    [Header("UI ==================================================")]
    [SerializeField] CanvasScaler UI_canvasScaler;

    ////////// Getter & Setter  //////////

    ////////// Method           //////////
    public string UI_ValueText(List<int> _values)
    {
        string res = "";

        //
        int for0 = 0;
        for (for0 = _values.Count - 1; for0 >= 0; for0--)
        {
            if (_values[for0] > 0)
            {
                break;
            }
        }

        //
        res = _values[for0].ToString();
        // 단위 삽입
        switch (for0)
        {
            case 0: { } break;
            case 1: { res += "K"; } break;
            case 2: { res += "M"; } break;
            case 3: { res += "G"; } break;
            case 4: { res += "T"; } break;
        }

        //
        return res;
    }

    ////////// Unity            //////////
    void UI_Start()
    {
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float canvasRatio = UI_canvasScaler.referenceResolution.x / UI_canvasScaler.referenceResolution.y;

        Vector2 rs = UI_canvasScaler.referenceResolution;

        // 화면비가 가로로 더 클 때
        if (screenRatio > canvasRatio)
        {
            UI_canvasScaler.matchWidthOrHeight = 1.0f;

            rs.x = UI_canvasScaler.referenceResolution.y * screenRatio;
            UI_canvasScaler.referenceResolution = rs;
        }
        // 화면비가 세로로 더 클 때
        else
        {
            UI_canvasScaler.matchWidthOrHeight = 0.0f;

            rs.y = UI_canvasScaler.referenceResolution.x / screenRatio;
            UI_canvasScaler.referenceResolution = rs;
        }
    }

    #endregion

    #region BATTLE

    [Header("BATTLE ==================================================")]
    [SerializeField] Giggle_Battle Battle_data;

    ////////// Getter & Setter  //////////

    ////////// Method           //////////

    ////////// Unity            //////////
    void Battle_Start()
    {
        Battle_data.Basic_Init();
    }

    // Update
    void Battle_Update()
    {

    }

    #endregion
}
