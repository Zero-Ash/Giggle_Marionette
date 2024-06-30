using UnityEngine;

//
using System.Collections.Generic;

public class Giggle_ScriptBridge
{
    public enum EVENT
    {
        #region Giggle_Master

        /// <summary>
        /// 코루틴 시작<br/>
        /// 매개변수 : IEnumerator - 시작하고자 하는 코루틴<br/>
        /// return type : 
        /// </summary>
        MASTER__BASIC__START_COROUTINE,

        /// <summary>
        /// 코루틴 중지<br/>
        /// 매개변수 : IEnumerator - 시작하고자 하는 코루틴<br/>
        /// return type : 
        /// </summary>
        MASTER__BASIC__STOP_COROUTINE,

        /// <summary>
        /// 코루틴 시작<br/>
        /// 매개변수 : Transform - 버리고자 하는 오브젝트<br/>
        /// return type : 
        /// </summary>
        MASTER__GARBAGE__REMOVE,

        /// <summary>
        /// UI용 캐릭터 오브젝트 생성<br/>
        /// 매개변수 : int - 캐릭터 데이터 id, Transform - 오브젝트의 부모, float - 각도, float - 크기<br/>
        /// return type : Giggle_Unit
        /// </summary>
        MASTER__UI__CHARACTER_INSTANTIATE,

        /// <summary>
        /// UI용 캐릭터 오브젝트 생성<br/>
        /// 매개변수 : Giggle_Battle.Basic__COROUTINE_PHASE - 진행시키고자 하는 단계<br/>
        /// return type : Giggle_Battle.Basic__COROUTINE_PHASE
        /// </summary>
        MASTER__BATTLE__VAR_COROUTINE_PHASE,

        #endregion

        #region Giggle_Database

        /// <summary>
        /// 캐릭터 데이터의 개방 여부<br/>
        /// 매개변수<br/>
        /// return type : bool
        /// </summary>
        DATABASE__CHARACTER__GET_IS_OPEN,
        /// <summary>
        /// 캐릭터 데이터<br/>
        /// 매개변수 : int - 캐릭터id<br/>
        /// return type : Giggle_Character.Database
        /// </summary>
        DATABASE__CHARACTER__GET_DATA_FROM_ID,
        /// <summary>
        /// 캐릭터 데이터<br/>
        /// 매개변수 : Giggle_Character.ATTRIBUTE - 캐릭터 속성(복수 기입 가능)<br/>
        /// return type : List(Giggle_Character.Database)
        /// </summary>
        DATABASE__CHARACTER__GET_DATAS_FROM_ATTRIBUTE,

        #endregion

        #region Giggle_Player

        /// <summary>
        /// 마리오네트 추가<br/>
        /// 매개변수 : int - 캐릭터 id<br/>
        /// return type : 
        /// </summary>
        PLAYER__MARIONETTE__ADD,

        /// <summary>
        /// 마리오네트 리스트 가져오기<br/>
        /// 매개변수 : <br/>
        /// return type : List(Giggle_Character.Save)
        /// </summary>
        PLAYER__MARIONETTE__GET_LIST,

        /// <summary>
        /// 마리오네트 리스트 가져오기<br/>
        /// 매개변수 : <br/>
        /// return type : int
        /// </summary>
        PLAYER__FORMATION__GET_SELECT,

        /// <summary>
        /// 마리오네트 리스트 가져오기<br/>
        /// 매개변수 : <br/>
        /// return type : List(Giggle_Player.Formation)
        /// </summary>
        PLAYER__FORMATION__GET_FORMATION_LIST,

        /// <summary>
        /// 마리오네트 리스트 가져오기<br/>
        /// 매개변수 : int - 진영 번호<br/>
        /// return type : List(int)
        /// </summary>
        PLAYER__FORMATION__GET_FORMATION_FROM_COUNT,

        #endregion
    }

    #region BASIC
    public delegate object Basic_Method(params object[] _args);

    static Giggle_ScriptBridge Basic_instance;

    Dictionary<EVENT, Basic_Method> Basic_methods;

    ////////// Getter & Setter          //////////
    public static Giggle_ScriptBridge Basic_VarInstance
    {
        get
        {
            if (Basic_instance == null)
            {
                Basic_instance = new Giggle_ScriptBridge();
            }

            return Basic_instance;
        }
    }

    ////////// Method                   //////////
    public object   Basic_GetMethod(EVENT _event, params object[] _args)
    {
        return Basic_methods[_event](_args);
    }

    public void     Basic_SetMethod(EVENT _event, Basic_Method _method)
    {
        if(Basic_methods.ContainsKey(_event))
        {
            Basic_methods[_event] = _method;
        }
        else
        {
            Basic_methods.Add(_event, _method);
        }
    }

    ////////// Constructor & Destroyer  //////////
    public Giggle_ScriptBridge()
    {
        if(Basic_methods == null)
        {
            Basic_methods = new Dictionary<EVENT, Basic_Method>();
        }
    }

    #endregion
}
