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
        DATABASE__CHARACTER__GET_DATA

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
