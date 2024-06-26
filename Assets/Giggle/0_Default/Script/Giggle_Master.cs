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
    [SerializeField] Giggle_Player  Basic_player;

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
        
        if(Basic_player == null)
        {
            Basic_player = new Giggle_Player();
        }
        Basic_player.Basic_Init();

        Garbage_Start();
        UI_Start();
        Battle_Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion

    #region GARBAGE

    [Header("GARBAGE ==================================================")]
    [SerializeField] Transform Garbage_parent;

    ////////// Getter & Setter  //////////

    ////////// Method           //////////

    object Garbage_Remove(params object[] _args)
    {
        Transform garbage = (Transform)_args[0];

        //
        if(Garbage_parent == null)
        {
            GameObject obj = new GameObject();
            obj.name = "garbage";
            obj.SetActive(false);
            Garbage_parent = obj.transform;
        }

        garbage.parent = Garbage_parent;

        if(Garbage_parent.childCount > 100)
        {
            Destroy(Garbage_parent);
            Garbage_parent = null;
        }

        //
        return true;
    }

    ////////// Unity            //////////
    void Garbage_Start()
    {
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.MASTER__GARBAGE__REMOVE, Garbage_Remove);
    }

    #endregion

    #region UI

    [Header("UI ==================================================")]
    [SerializeField] CanvasScaler UI_canvasScaler;

    ////////// Getter & Setter  //////////

    ////////// Method           //////////

    // UI_CharacterInstantiate
    object UI_CharacterInstantiate(params object[] _args)
    {
        int         id      = (int      )_args[0];
        Transform   parent  = (Transform)_args[1];
        float       rot_x   = (float    )_args[2];
        float       scale   = (float    )_args[3];

        //
        Giggle_Character.Database data
            = (Giggle_Character.Database)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.DATABASE__CHARACTER__GET_DATA_FROM_ID,
                //
                id);

        Giggle_Unit res = GameObject.Instantiate(data.Basic_VarUnit, parent);
        UI_CharacterInstantiate__ChangeModelLayer(res.transform);
        res.transform.localPosition = Vector3.zero;
        res.transform.localRotation = Quaternion.Euler(rot_x,0,0);
        res.transform.localScale = new Vector3(scale, scale, scale);

        //
        return res;
    }

    void UI_CharacterInstantiate__ChangeModelLayer(Transform _parent)
    {
        _parent.gameObject.layer = 5;

        for(int for0 = 0; for0 < _parent.childCount; for0++)
        {
            UI_CharacterInstantiate__ChangeModelLayer(_parent.GetChild(for0));
        }
    }
    
    // UI_ValueText
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

        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.MASTER__UI__CHARACTER_INSTANTIATE,  UI_CharacterInstantiate );
    }

    #endregion

    #region BATTLE

    [Header("BATTLE ==================================================")]
    [SerializeField] Giggle_Battle Battle_data;

    ////////// Getter & Setter  //////////
    object Battle_VarCoroutinePhase(params object[] _args)
    {
        if(_args.Length > 0)
        {
            Giggle_Battle.Basic__COROUTINE_PHASE phase = (Giggle_Battle.Basic__COROUTINE_PHASE)_args[0];
            Battle_data.Basic_VarCoroutinePhase = phase;
        }

        //
        return Battle_data.Basic_VarCoroutinePhase;
    }

    ////////// Method           //////////

    ////////// Unity            //////////
    void Battle_Start()
    {
        Battle_data.Basic_Init();

        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.MASTER__BATTLE__VAR_COROUTINE_PHASE,    Battle_VarCoroutinePhase    );
    }

    // Update
    void Battle_Update()
    {

    }

    #endregion
}
