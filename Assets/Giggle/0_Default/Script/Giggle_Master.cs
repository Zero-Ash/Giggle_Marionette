using UnityEngine;

//
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using BackEnd;
using UnityEngine.Networking;

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

    [Serializable]
    public class Network_Values
    {
        public int Network_initState;
    }

    [Header("NETWORK ==================================================")]
    [SerializeField] int   Network_initState;
    [SerializeField] TMPro.TextMeshProUGUI Network_tempText;

    ////////// Getter & Setter  //////////
    object Network_VarInitState(params object[] _args)  { return Network_initState; }

    object Network_VarGoogleHash(params object[] _args) { return Backend.Utils.GetGoogleHash(); }

    ////////// Method           //////////

    // Network_GuildCreate
    object Network_GuildCreate(params object[] _args)
    {
        string guildName        = (string)_args[0];
        string guildIntroduce   = (string)_args[2];

        //
        Param param = new Param();
        param.Add("Introduce",  guildIntroduce  );
        Backend.Guild.CreateGuildV3(
            guildName, 10, param,
            //
            callback =>
            {
                switch(callback.StatusCode)
                {
                    case 204:   { Network_GuildCreate__CreateGuildV3Success(_args);  }   break;
                }
            });

        //
        return true;
    }

    void Network_GuildCreate__CreateGuildV3Success(params object[] _args)
    {
        int guildJoinType   = (int)_args[1];

        bool isAuto = guildJoinType.Equals(0);

        // 길드 가입방식 설정
        Backend.Guild.SetRegistrationValueV3(isAuto,
            //
            callback =>
            {
                switch(callback.StatusCode)
                {
                    case 204:   {   }   break;
                }
            });
    }

    ////////// Unity            //////////
    void Network_Start()
    {
        Network_tempText.text = "Network_Start";
        Network_initState = -1;

        //
        Backend.InitializeAsync(
            callback =>
            {
                if(callback.IsSuccess())
                {
        Network_tempText.text = "Network_Start 1";
                    Network_initState = 1;
                }
                else
                {
        Network_tempText.text = "Network_Start 0";
                    Network_initState = 0;
                }
            });
        
        Network_Account_Start();
        Network_DataLoad_Start();
        Network_Gacha_Start();
        Network_Formation_Start();
        Network_Stage_Start();

        //
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.MASTER__NETWORK__VAR_INIT_STATE,    Network_VarInitState    );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.MASTER__NETWORK__VAR_GOOGLE_HASH,   Network_VarGoogleHash   );

        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.MASTER__NETWORK__GUILD_CREATE,  Network_GuildCreate );
    }

        #region ACCOUNT

    public class Network_Account_LogIn
    {
        public string   ID;
        public int      Market;

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////
    
        ////////// Constructor & Destroyer  //////////
    }

    [SerializeField] Network_Account_LogIn  Network_Account_logIn;

    ////////// Getter & Setter  //////////

    ////////// Method           //////////
    
    object Network_Account_GuestLogIn(params object[] _args)
    {
        TMPro.TextMeshProUGUI text = (TMPro.TextMeshProUGUI)_args[0];
        Giggle_TitleManager.UI_MainBasicData.LogIn_Values values = (Giggle_TitleManager.UI_MainBasicData.LogIn_Values)_args[1];

        //
        //StartCoroutine(Network_Account_GuestLogIn__PostRequest(_args));
        
        Backend.BMember.GuestLogin(
            callback =>
            {
                if(callback.IsSuccess())
                {
                    text.text = "LogIn_BtnClick__GUEST_SIGN_IN__Coroutine0 " + callback.StatusCode;
                    switch(callback.StatusCode)
                    {
                        // 최초 접속
                        case 201:   { values.Basic_SignInCoroutinePhase = 10;   }   break;
                        // 로그인
                        default:    { values.Basic_SignInCoroutinePhase = 100;  }   break;
                    }
                }
                else
                {
                    text.text = "LogIn_BtnClick__GUEST_SIGN_IN__Coroutine0 " + callback.StatusCode;
                }
            });

        //
        return true;
    }

    IEnumerator Network_Account_GuestLogIn__PostRequest(params object[] _args)
    {
        Giggle_TitleManager.UI_MainBasicData.LogIn_Values values = (Giggle_TitleManager.UI_MainBasicData.LogIn_Values)_args[1];

        //
        Network_Account_logIn.ID = "dfd3gfjhr4";
        Network_Account_logIn.Market = 0;
        string dataStr = JsonUtility.ToJson(Network_Account_logIn);

        using (UnityWebRequest webRequest = UnityWebRequest.Post("http://49.168.197.235:65200/User/Login", dataStr, "application/json; charset=utf-8"))
        {
            // 요청 보내기
            yield return webRequest.SendWebRequest();

            switch(webRequest.result)
            {
                case UnityWebRequest.Result.Success:
                {
                    Debug.Log("응답 데이터: " + webRequest.downloadHandler.text);

                    LitJson.JsonData data = LitJson.JsonMapper.ToObject(webRequest.downloadHandler.text);
                    Debug.Log(data["Result"]);

                    //
                    values.Basic_SignInCoroutinePhase = 10;
                }
                break;
            }
        }
    }
    
    //
    object Network_Account_GuestDelete(params object[] _args)
    {
        //
        Backend.BMember.DeleteGuestInfo();

        //
        return true;
    }

    object Network_Account_UserSignUp(params object[] _args)
    {
        TMPro.TextMeshProUGUI text = (TMPro.TextMeshProUGUI)_args[0];
        Giggle_TitleManager.UI_MainBasicData.LogIn_Values values = (Giggle_TitleManager.UI_MainBasicData.LogIn_Values)_args[1];
        
        //
        Backend.Chart.GetChartContents("172424",
            (callback) =>
            {
                text.text = "LogIn_BtnClick__GUEST_SIGN_IN__Coroutine10 " + callback.StatusCode;

                //
                LitJson.JsonData datas = callback.FlattenRows();
                Param param = new Param();
                param.Add("RESOURCE_GOLD",  int.Parse(datas[0]["RESOURCE_GOLD"].ToString())     );

                param.Add("STAGE_MAX",      int.Parse(datas[0]["STAGE"].ToString()) );
                param.Add("STAGE_SELECT",   int.Parse(datas[0]["STAGE"].ToString()) );

                Backend.GameData.Insert("PLAYER_DATA", param);

                //
                param.Clear();
                param.Add("GACHA_COUNT",    int.Parse(datas[0]["RESOURCE_GACHA"].ToString())    );
                param.Add("GACHA_LIST",     "empty");
                param.Add("GACHA_CHANGE",   0);

                Backend.GameData.Insert("PLAYER_GACHA", param);

                //
                param.Clear();
                string paramName = "FORMATION_MAIN";
                string strs = "-1|-1|-1|-1|-2|-1|-1|-1|-1";
                for(int for0 = 0; for0 < 3; for0++)
                {
                    param.Add(paramName + for0, strs);
                }

                Backend.GameData.Insert("PLAYER_FORMATION", param);
                
                ////
                //param.Clear();
                //string[] strs0 = datas[0]["PINOCCHIO__EQUIPS"].ToString().Split('|');
                //for(int for0 = 0; for0 < strs0.Length; for0++)
                //{
                //    param.Add("EQUIPS_" + for0,    strs0[for0]);
                //}
                //param.Add("EQUIPS_SELECT",   0);
                //
                //strs0 = datas[0]["PINOCCHIO__SKILLS"].ToString().Split('|');
                //for(int for0 = 0; for0 < strs0.Length; for0++)
                //{
                //    string[] skills1 = strs0[for0].Split('/');
                //    param.Add("SKILL__" + skills1[0],    int.Parse(skills1[1]));
                //}
                //
                //strs0 = datas[0]["PINOCCHIO__SKILL_SLOTS"].ToString().Split('|');
                //for(int for0 = 0; for0 < strs0.Length; for0++)
                //{
                //    param.Add("SKILL__SLOT" + for0, strs0[for0]);
                //}
                //param.Add("SKILL__SLOT_SELECT", 0);
                //
                //param.Add("ABILITYS",                   datas[0]["PINOCCHIO__ABILITYS"].ToString()      );
                //param.Add("ABILITY_LEVEL",  int.Parse(  datas[0]["PINOCCHIO__ABILITY_LEVEL"].ToString()));
                //param.Add("ABILITY_EXP",    int.Parse(  datas[0]["PINOCCHIO__ABILITY_EXP"].ToString())  );
                //param.Add("ABILITY_POINT",  int.Parse(  datas[0]["PINOCCHIO__ABILITY_POINT"].ToString()));
                //
                //Backend.GameData.Insert("PINOCCHIO", param);
                //
                ////
                //strs0 = datas[0]["MARIONETTE"].ToString().Split('/');
                //for(int for0 = 0; for0 < strs0.Length; for0++)
                //{
                //    param.Clear();
                //    param.Add("DATA_ID",        int.Parse(strs0[for0])          );
                //    param.Add("INVENTORY_ID",   Backend.UserInDate + "|" + for0 );
                //    param.Add("LEVEL",          1                               );
                //    param.Add("SKILL_LV",       1                               );
                //    Backend.GameData.Insert("MARIONETTE", param);
                //}

                //
                values.Basic_SignInCoroutinePhase = 100;
            });

        //
        return true;
    }

    ////////// Unity            //////////
    void Network_Account_Start()
    {
        Network_Account_logIn = new Network_Account_LogIn();

        //
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.MASTER__NETWORK__GUEST_LOG_IN,  Network_Account_GuestLogIn  );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.MASTER__NETWORK__GUEST_DELETE,  Network_Account_GuestDelete );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.MASTER__NETWORK__USER_SIGN_UP,  Network_Account_UserSignUp  );
    }

        #endregion

        #region DATA_LOAD

    ////////// Getter & Setter  //////////

    ////////// Method           //////////
    //
    object Network_DataLoadPlayer(params object[] _args)
    {
        Giggle_Player.Data_Values values = (Giggle_Player.Data_Values)_args[0];

        Backend.GameData.GetMyData(
            "PLAYER_DATA", new Where(),
            callback =>
            {
                if(!callback.IsSuccess())
                {
                    return;
                }

                //
                values.Basic_coroutineDatas = callback.FlattenRows();

                values.Basic_coroutinePhase = Giggle_Player.Basic__DATA_COROUTINE_PHASE.RESOURCE;
            });

        //
        return true;
    }

    //
    object Network_DataLoadPinocchio(params object[] _args)
    {
        Giggle_Player.Data_Values values = (Giggle_Player.Data_Values)_args[0];

        Backend.GameData.GetMyData(
            "PINOCCHIO", new Where(),
            callback =>
            {
                if(!callback.IsSuccess())
                {
                    values.Basic_coroutinePhase = Giggle_Player.Basic__DATA_COROUTINE_PHASE.MARIONETTE;
                    return;
                }

                //
                values.Basic_coroutineDatas = callback.FlattenRows();
                if(values.Basic_coroutineDatas.Count > 0)
                {
                    values.Basic_coroutinePhase = Giggle_Player.Basic__DATA_COROUTINE_PHASE.PINOCCHIO_DATA;
                }
                else
                {
                    values.Basic_coroutinePhase = Giggle_Player.Basic__DATA_COROUTINE_PHASE.MARIONETTE;
                }
            });

        //
        return true;
    }

    //
    object Network_DataLoadMarionette(params object[] _args)
    {
        Giggle_Player.Data_Values values = (Giggle_Player.Data_Values)_args[0];

        Backend.GameData.GetMyData(
            "MARIONETTE", new Where(),
            callback =>
            {
                if(!callback.IsSuccess())
                {
                    values.Basic_coroutinePhase = Giggle_Player.Basic__DATA_COROUTINE_PHASE.FORMATION;
                    return;
                }

                //
                values.Basic_coroutineDatas = callback.FlattenRows();

                values.Basic_coroutinePhase = Giggle_Player.Basic__DATA_COROUTINE_PHASE.MARIONETTE_DATA;
            });

        //
        return true;
    }

    //
    object Network_DataLoadFormation(params object[] _args)
    {
        Giggle_Player.Data_Values values = (Giggle_Player.Data_Values)_args[0];

        Backend.GameData.GetMyData(
            "PLAYER_FORMATION", new Where(),
            callback =>
            {
                if(!callback.IsSuccess())
                {
                    values.Basic_coroutinePhase = Giggle_Player.Basic__DATA_COROUTINE_PHASE.ITEM;
                    return;
                }

                //
                values.Basic_coroutineDatas = callback.FlattenRows();
                Formation_inDate = values.Basic_coroutineDatas[0]["inDate"].ToString();

                values.Basic_coroutinePhase = Giggle_Player.Basic__DATA_COROUTINE_PHASE.FORMATION_DATA;
            });

        //
        return true;
    }

    ////////// Unity            //////////
    void Network_DataLoad_Start()
    {
        //
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.MASTER__NETWORK__DATA_LOAD_PLAYER,      Network_DataLoadPlayer      );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.MASTER__NETWORK__DATA_LOAD_PINOCCHIO,   Network_DataLoadPinocchio   );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.MASTER__NETWORK__DATA_LOAD_MARIONETTE,  Network_DataLoadMarionette  );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.MASTER__NETWORK__DATA_LOAD_FORMATION,   Network_DataLoadFormation   );
    }

        #endregion

        #region GACHA

    ////////// Getter & Setter  //////////

    ////////// Method           //////////
    
    //
    object Network_Gacha_Check(params object[] _args)
    {
        Giggle_MainManager.UI_MainPopUpData.Gacha_Values values = (Giggle_MainManager.UI_MainPopUpData.Gacha_Values)_args[0];

        //
        Backend.GameData.GetMyData("PLAYER_GACHA", new Where(), 1,
            (callback)=>
            {
                if(!callback.IsSuccess())
                {
                    return;
                }

                Network_Gacha_Check0(values, callback.FlattenRows());
            });

        //
        return true;
    }

    void Network_Gacha_Check0(Giggle_MainManager.UI_MainPopUpData.Gacha_Values _values, LitJson.JsonData _rows)
    {
        string inDate = _rows[0]["inDate"].ToString();

        //
        _values.Gacha_list = _rows[0]["GACHA_LIST"].ToString();
        _values.Gacha_changeCount = int.Parse(_rows[0]["GACHA_CHANGE"].ToString());

        _values.Basic_coroutinePhase = 20;
    }
    
    //
    object Network_Gacha_Select(params object[] _args)
    {
        Giggle_MainManager.UI_MainPopUpData.Gacha_Values values = (Giggle_MainManager.UI_MainPopUpData.Gacha_Values)_args[0];

        //
        Backend.GameData.GetMyData("PLAYER_GACHA", new Where(), 1,
            (callback)=>
            {
                if(!callback.IsSuccess())
                {
                    return;
                }

                Network_Gacha_Select0(values, callback.FlattenRows());
            });

        //
        return true;
    }

    void Network_Gacha_Select0(Giggle_MainManager.UI_MainPopUpData.Gacha_Values _values, LitJson.JsonData _rowsGacha)
    {
        //
        Backend.GameData.GetMyData("MARIONETTE", new Where(),
            (callback)=>
            {
                if(!callback.IsSuccess())
                {
                    return;
                }

                Network_Gacha_Select1(_values, _rowsGacha, callback.FlattenRows());
            });
    }

    void Network_Gacha_Select1(Giggle_MainManager.UI_MainPopUpData.Gacha_Values _values, LitJson.JsonData _rowsGacha, LitJson.JsonData _rowsMarionette)
    {
        string[] list = _rowsGacha[0]["GACHA_LIST"].ToString().Split("|");

        Param param = new Param();
        _values.Gacha_list = "empty";
        param.Add("GACHA_LIST", _values.Gacha_list);

        Backend.GameData.UpdateV2("PLAYER_GACHA", _rowsGacha[0]["inDate"].ToString(), Backend.UserInDate, param);

        //
        int inventoryId = -1;
        for(int for0 = 0; for0 < _rowsMarionette.Count; for0++)
        {
            int num = int.Parse(_rowsMarionette[for0]["INVENTORY_ID"].ToString());
            if(inventoryId < num)
            {
                inventoryId = num;
            }
        }
        inventoryId++;

        //
        param.Clear();
        param.Add("DATA_ID",            int.Parse(list[_values.Gacha_select])   );
        param.Add("INVENTORY_ID",       inventoryId                             );

        param.Add("LEVEL",              1                                       );
        param.Add("SKILL_LV",           1                                       );

        param.Add("EQUIPMENT_WEAPON",   -1                                      );

        Backend.GameData.Insert("MARIONETTE", param);
    }
    
    //
    object Network_Gacha_Gacha(params object[] _args)
    {
        Giggle_MainManager.UI_MainPopUpData.Gacha_Values values = (Giggle_MainManager.UI_MainPopUpData.Gacha_Values)_args[0];

        //
        Backend.GameData.GetMyData("PLAYER_GACHA", new Where(), 1,
            (callback)=>
            {
                if(!callback.IsSuccess())
                {
                    return;
                }

                Network_Gacha_Gacha0(values, callback.FlattenRows());
            });

        //
        return true;
    }

    void Network_Gacha_Gacha0(Giggle_MainManager.UI_MainPopUpData.Gacha_Values _values, LitJson.JsonData _rows)
    {
        string inDate = _rows[0]["inDate"].ToString();
        int gachaCount = int.Parse(_rows[0]["GACHA_COUNT"].ToString());
        
        // 가챠횟수가 0이라면 여기서 끝내기
        if(gachaCount == 0)
        {
            _values.Basic_coroutinePhase = -1;

            return;
        }

        Param param = new Param();
        param.Add("GACHA_COUNT", gachaCount - 1);
        
        //
        _values.Gacha_list = Network_Gacha_ListSetting();
        param.Add("GACHA_LIST", _values.Gacha_list);

        _values.Gacha_changeCount = 3;
        param.Add("GACHA_CHANGE", _values.Gacha_changeCount);

        Backend.GameData.UpdateV2("PLAYER_GACHA", inDate, Backend.UserInDate, param,
            (callback) =>
            {
                if(callback.IsSuccess())
                {
                    _values.Basic_coroutinePhase = 10;
                }
                else
                {
                    _values.Basic_coroutinePhase = -1;
                }
            });
    }
    
    //
    object Network_Gacha_Change(params object[] _args)
    {
        Giggle_MainManager.UI_MainPopUpData.Gacha_Values values = (Giggle_MainManager.UI_MainPopUpData.Gacha_Values)_args[0];

        //

        //
        Backend.GameData.GetMyData("PLAYER_GACHA", new Where(), 1,
            (callback)=>
            {
                if(!callback.IsSuccess())
                {
                    return;
                }

                Network_Gacha_Change0(values, callback.FlattenRows());
            });

        //
        return true;
    }

    void Network_Gacha_Change0(Giggle_MainManager.UI_MainPopUpData.Gacha_Values _values, LitJson.JsonData _rows)
    {
        string inDate = _rows[0]["inDate"].ToString();
        _values.Gacha_changeCount = int.Parse(_rows[0]["GACHA_CHANGE"].ToString());
        
        // 가챠횟수가 0이라면 여기서 끝내기
        if(_values.Gacha_changeCount == 0)
        {
            return;
        }

        //
        Param param = new Param();

        _values.Gacha_list = Network_Gacha_ListSetting();
        param.Add("GACHA_LIST", _values.Gacha_list);

        _values.Gacha_changeCount--;
        param.Add("GACHA_CHANGE", _values.Gacha_changeCount);

        Backend.GameData.UpdateV2("PLAYER_GACHA", inDate, Backend.UserInDate, param,
            (callback) =>
            {
                if(callback.IsSuccess())
                {
                    _values.Basic_coroutinePhase = 10;
                }
                else
                {
                    _values.Basic_coroutinePhase = -1;
                }
            });
    }

    //
    string Network_Gacha_ListSetting()
    {
        string res = "";

        // 가챠 리스트 불러오기
        List<Giggle_Character.Database> datas
            = (List<Giggle_Character.Database>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.DATABASE__MARIONETTE__GET_DATAS_FROM_ATTRIBUTE,
                Giggle_Master.ATTRIBUTE.FIRE.ToString());

        for(int for0 = 0; for0 < 3; for0++)
        {
            Giggle_Character.Database data = datas[UnityEngine.Random.Range(0, datas.Count)];
            res += data.Basic_VarId + "|";
        }

        return res;
    }

    ////////// Unity            //////////
    void Network_Gacha_Start()
    {
        //
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.MASTER__NETWORK__GACHA__CHECK,  Network_Gacha_Check     );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.MASTER__NETWORK__GACHA__SELECT, Network_Gacha_Select    );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.MASTER__NETWORK__GACHA__GACHA,  Network_Gacha_Gacha     );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.MASTER__NETWORK__GACHA__CHANGE, Network_Gacha_Change    );
    }

        #endregion


        #region FORMATION
    [Header("FORMATION ==========")]
    [SerializeField] string Formation_inDate;

    ////////// Getter & Setter  //////////

    ////////// Method           //////////
    
    //
    object Network_Formation_Save(params object[] _args)
    {
        Giggle_Player.Data_Values values = (Giggle_Player.Data_Values)_args[0];

        //
        string[] datas = values.Basic_coroutineString.Split('/');

        Param param = new Param();
        param.Add("FORMATION_MAIN" + datas[1], datas[0]);

        Backend.GameData.UpdateV2(
            "PLAYER_FORMATION", Formation_inDate, Backend.UserInDate, param,
            (callback) =>
            {
                if(callback.IsSuccess())
                {
                    values.Basic_coroutinePhase = Giggle_Player.Basic__DATA_COROUTINE_PHASE.FORMATION;
                }
                else
                {
                    values.Basic_coroutinePhase = Giggle_Player.Basic__DATA_COROUTINE_PHASE.END;
                }
            });

        //
        return true;
    }

    ////////// Unity            //////////
    void Network_Formation_Start()
    {
        //
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.MASTER__NETWORK__FOAMRTION__SAVE,  Network_Formation_Save   );
    }

        #endregion

        #region STAGE

    public class Network_Stage_StartStage
    {
        public int  SK;
        public int  SelectStage;
        public int  Loop;

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////
    
        ////////// Constructor & Destroyer  //////////
    }

    public class Network_Stage_EndStage
    {
        public int  SK;
        public int  Clear;

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////
    
        ////////// Constructor & Destroyer  //////////
    }

    [SerializeField] Network_Stage_StartStage   Network_Stage_startStage;
    [SerializeField] Network_Stage_EndStage     Network_Stage_endStage;

    ////////// Getter & Setter  //////////

    ////////// Method           //////////

    // Stage
    IEnumerator Network_Stage_StartStage__PostRequest()
    {
        Network_Stage_startStage.SK = 293458878;
        Network_Stage_startStage.SelectStage = 0;
        Network_Stage_startStage.Loop = 1;
        string dataStr = JsonUtility.ToJson(Network_Stage_startStage);
        Debug.Log("응답 데이터: " + dataStr);

        using (UnityWebRequest webRequest = UnityWebRequest.Post("http://49.168.197.235:65200/stage/StartStage", dataStr, "application/json; charset=utf-8"))
        {
            // 요청 보내기
            yield return webRequest.SendWebRequest();

            switch(webRequest.result)
            {
                case UnityWebRequest.Result.Success:
                {
                    string jsonResponse = webRequest.downloadHandler.text;
                    Debug.Log("응답 데이터: " + jsonResponse);

                    LitJson.JsonData data = LitJson.JsonMapper.ToObject(webRequest.downloadHandler.text);
                    Debug.Log(data["Result"]);
                }
                break;
            }
        }
    }

    IEnumerator Network_Stage_EndStage__PostRequest()
    {
        Network_Stage_endStage.SK = 293458878;
        Network_Stage_endStage.Clear = 1;
        string dataStr = JsonUtility.ToJson(Network_Stage_endStage);
        Debug.Log("응답 데이터: " + dataStr);

        using (UnityWebRequest webRequest = UnityWebRequest.Post("http://49.168.197.235:65200/stage/EndStage", dataStr, "application/json; charset=utf-8"))
        {
            // 요청 보내기
            yield return webRequest.SendWebRequest();

            switch(webRequest.result)
            {
                case UnityWebRequest.Result.Success:
                {
                    string jsonResponse = webRequest.downloadHandler.text;
                    Debug.Log("응답 데이터: " + jsonResponse);

                    LitJson.JsonData data = LitJson.JsonMapper.ToObject(webRequest.downloadHandler.text);
                    Debug.Log(data["Result"]);
                }
                break;
            }
        }
    }

    ////////// Unity            //////////
    void Network_Stage_Start()
    {
        Network_Stage_startStage    = new Network_Stage_StartStage();
        Network_Stage_endStage      = new Network_Stage_EndStage();
    }

        #endregion

    #endregion

    #region SCENE

    public enum Scene_TYPE
    {
        TITLE   = 0,
        MAIN,
        DUNGEON,
        DOCUMENT,
        GUILD
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
        float       scale   = (float    )_args[2];

        //
        Giggle_Character.Database data
            = (Giggle_Character.Database)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.DATABASE__PINOCCHIO__GET_DATA_FROM_ID,
                //
                id);

        Transform res = Transform.Instantiate(data.Basic_VarSd, parent);
        UI_CharacterInstantiate__ChangeModelLayer(res);
        res.localPosition = Vector3.zero;
        res.localRotation = Quaternion.Euler(0,0,0);
        res.localScale = new Vector3(scale, scale, scale);

        //
        return res;
    }
    

    // UI_CharacterInstantiate
    object UI_CharacterInstantiate(params object[] _args)
    {
        int         id      = (int      )_args[0];
        Transform   parent  = (Transform)_args[1];
        float       scale   = (float    )_args[2];

        //
        Giggle_Character.Database data
            = (Giggle_Character.Database)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.DATABASE__MARIONETTE__GET_DATA_FROM_ID,
                //
                id);

        Transform res = Transform.Instantiate(data.Basic_VarSd, parent);
        UI_CharacterInstantiate__ChangeModelLayer(res);
        res.localPosition = Vector3.zero;
        res.localRotation = Quaternion.Euler(0,0,0);
        res.localScale = new Vector3(scale, scale, scale);

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
