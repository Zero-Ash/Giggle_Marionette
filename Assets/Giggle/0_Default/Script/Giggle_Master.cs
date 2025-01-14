using UnityEngine;

//
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using BackEnd;
using UnityEngine.AddressableAssets;

public class Giggle_Master : MonoBehaviour
{
    public enum ATTRIBUTE
    {

        NONE,

        FIRE = 1,
        WATER,
        WIND,
        EARTH,

        DARK,
        LIGHT,

        TOTAL
    }

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

        Application.targetFrameRate = 120;

        Basic_Database = new Giggle_Database(this.gameObject);
        
        if(Basic_player == null)
        {
            Basic_player = new Giggle_Player();
        }
        Basic_player.Basic_Init();

        Network_Start();
        Scene_Start();
        Garbage_Start();
        UI_Start();
        Battle_Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion

    #region NETWORK

    [Header("NETWORK ==================================================")]
    [SerializeField] int   Network_initState;

    ////////// Getter & Setter  //////////
    object Network_VarInitState(params object[] _args)
    {
        //
        return Network_initState;
    }

    ////////// Method           //////////

    ////////// Unity            //////////
    void Network_Start()
    {
        Network_initState = -1;

        //
        Backend.InitializeAsync(
            callback =>
            {
                if(callback.IsSuccess())
                {
                    Network_initState = 1;
                }
                else
                {
                    Network_initState = 0;
                }
            });

        //
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.MASTER__NETWORK__VAR_INIT_STATE,    Network_VarInitState    );
    }

    #endregion

    #region SCENE

    public enum Scene_TYPE
    {
        TITLE   = 0,
        MAIN,
        DUNGEON,
        DOCUMENT
    }

    [Header("SCENE ==================================================")]
    [SerializeField] GameObject         Scene_parent;

    [SerializeField] List<GameObject>   Scene_scenes;

    [SerializeField] GameObject Scene_loading;

    [SerializeField] Transform  Scene_cameraParent;

    ////////// Getter & Setter  //////////

    ////////// Method           //////////
    object Scene_LoadScene__Script(params object[] _args)
    {
        Scene_TYPE sceneType = (Scene_TYPE)_args[0];

        // 전체 비활성화
        Scene_loading.SetActive(true);
        for(int for0 = 0; for0 < Scene_scenes.Count; for0++)
        {
            if(Scene_scenes[for0] != null)
            {
                Scene_scenes[for0].GetComponent<Giggle_SceneManager>().Basic_Active(false);
            }
        }

        // 초기화
        while(Scene_scenes.Count <= (int)sceneType)
        {
            Scene_scenes.Add(null);
        }

        if(Scene_scenes[(int)sceneType] == null)
        {
            Addressables.LoadAssetAsync<GameObject>("SCENE/" + sceneType.ToString()).Completed
            += handle =>
            {
                Scene_scenes[(int)sceneType] = Instantiate(handle.Result, Scene_parent.transform);
                Scene_scenes[(int)sceneType].GetComponent<Giggle_SceneManager>().Basic_Active(true);

                Scene_loading.SetActive(false);
            };
        }
        else
        {
            Scene_scenes[(int)sceneType].GetComponent<Giggle_SceneManager>().Basic_Active(true);
            Scene_loading.SetActive(false);
        }

        //
        return true;
    }

    ////////// Unity            //////////
    void Scene_Start()
    {
        float ratio = (float)Screen.height / (float)Screen.width;
        for(int for0 = 0; for0 < Scene_cameraParent.childCount; for0++)
        {
            Scene_cameraParent.GetChild(for0).GetComponent<Camera>().orthographicSize = ratio * 4.7f;
        }
        //Scene_cameraParent.localPosition = new Vector3(0, 0, -8.0f * ratio);

        //
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.SCENE__LOAD_SCENE,  Scene_LoadScene__Script );
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
            Destroy(Garbage_parent.gameObject);
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
    [SerializeField] CanvasScaler   UI_canvasScaler;
    [SerializeField] Camera         UI_camera;

    [SerializeField] Vector2    UI_safeAreaSizeDelta;
    [SerializeField] Vector2    UI_safeAreaPos;

    [SerializeField] Transform UI_rawImages;

    ////////// Getter & Setter  //////////
    
    // UI_SafeArea
    object UI_SafeAreaVarSizeDelta(params object[] _args)
    {
        //
        return UI_safeAreaSizeDelta;
    }

    object UI_SafeAreaVarPosition(params object[] _args)
    {
        //
        return UI_safeAreaPos;
    }

    ////////// Method           //////////
    // UI_PinocchioInstantiate
    object UI_PinocchioInstantiate(params object[] _args)
    {
        int         id      = (int      )_args[0];
        Transform   parent  = (Transform)_args[1];
        float       rot_x   = (float    )_args[2];
        float       scale   = (float    )_args[3];

        //
        Giggle_Character.Database data
            = (Giggle_Character.Database)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.DATABASE__PINOCCHIO__GET_DATA_FROM_ID,
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
                Giggle_ScriptBridge.EVENT.DATABASE__MARIONETTE__GET_DATA_FROM_ID,
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
        _parent.gameObject.layer = 6;

        for(int for0 = 0; for0 < _parent.childCount; for0++)
        {
            UI_CharacterInstantiate__ChangeModelLayer(_parent.GetChild(for0));
        }
    }

    // UI_canvas
    object UI_CanvasSetting(params object[] _args)
    {
        //
        Canvas canvas = (Canvas)_args[0];

        //
        canvas.worldCamera = UI_camera;
        canvas.planeDistance = 2;
        CanvasScaler cs = canvas.GetComponent<CanvasScaler>();
        cs.matchWidthOrHeight   = UI_canvasScaler.matchWidthOrHeight;
        cs.referenceResolution  = UI_canvasScaler.referenceResolution;

        //
        return true;
    }
    
    // UI_ValueText
    object UI_ValueText(params object[] _args)
    {
        string res = "0";

        //
        List<int> values = (List<int>)_args[0];
        for(int for0 = values.Count - 1; for0 <= 0; for0--)
        {
            if(values[for0] > 0)
            {
                res = values[for0].ToString();
                switch(for0)
                {
                    case 1: { res += "K";   }   break;
                    case 2: { res += "M";   }   break;
                    case 3: { res += "G";   }   break;
                    case 4: { res += "T";   }   break;
                }
                break;
            }
        }

        //
        return res;
    }

    // UI_rawImages
    object UI_RawImage(params object[] _args)
    {
        Transform parentTrans = (Transform)_args[0];

        //
        if(UI_rawImages.childCount <= 1)
        {
            while(UI_rawImages.childCount < 10)
            {
                Instantiate(UI_rawImages.GetChild(0), UI_rawImages);
            }
        }

        Transform element = UI_rawImages.GetChild(0);
        element.parent = parentTrans;
        element.localScale = Vector3.one;
        element.GetComponent<RectTransform>().sizeDelta = new Vector2(3840.0f / 2160.0f * UI_canvasScaler.referenceResolution.y, UI_canvasScaler.referenceResolution.y);

        //
        return true;
    }

    ////////// Unity            //////////
    void UI_Start()
    {
        //
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

        //
        float width     = Screen.safeArea.width     / (float)Screen.width;
        float height    = Screen.safeArea.height    / (float)Screen.height;
        UI_safeAreaSizeDelta = new Vector2(rs.x * width, rs.y * height);
        float posX  = Screen.safeArea.position.x - (((float)Screen.width - Screen.safeArea.width) * 0.5f);
        float posY  = Screen.safeArea.position.y - (((float)Screen.height - Screen.safeArea.height) * 0.5f);
        float scale = UI_canvasScaler.referenceResolution.x / (float)Screen.width;
        UI_safeAreaPos = new Vector2(posX * scale, posY * scale);

        //
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.MASTER__UI__SAFE_AREA_VAR_SIZE_DELTA,   UI_SafeAreaVarSizeDelta );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.MASTER__UI__SAFE_AREA_VAR_POSITION,     UI_SafeAreaVarPosition  );

        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.MASTER__UI__PINOCCHIO_INSTANTIATE,  UI_PinocchioInstantiate );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.MASTER__UI__CHARACTER_INSTANTIATE,  UI_CharacterInstantiate );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.MASTER__UI__CANVAS_SETTING,         UI_CanvasSetting        );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.MASTER__UI__VALUE_TEXT,             UI_ValueText            );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.MASTER__UI__RAW_IMAGE,              UI_RawImage             );
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
    ///
    object Battle_Accel(params object[] _args)
    {
        Battle_data.Basic_Accel();

        //
        return true;
    }

    //
    object Battle_SleepOn(params object[] _args)
    {
        Battle_data.Basic_PowerSaving();

        //
        return true;
    }

    object Battle_SleepOff(params object[] _args)
    {
        Battle_data.Basic_PowerSaving();

        //
        return true;
    }

    //

    object Battle_Init__Bridge(params object[] _args)
    {
        Battle_data.Basic_Init();

        //
        return true;
    }

    ////////// Unity            //////////
    void Battle_Start()
    {
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.MASTER__BATTLE__VAR_COROUTINE_PHASE,    Battle_VarCoroutinePhase    );

        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.MASTER__BATTLE__ACCEL,      Battle_Accel        );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.MASTER__BATTLE__SLEEP_ON,   Battle_SleepOn      );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.MASTER__BATTLE__SLEEP_OFF,  Battle_SleepOff     );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.MASTER__BATTLE__INIT,       Battle_Init__Bridge );
    }

    // Update
    void Battle_Update()
    {

    }

    #endregion
}
