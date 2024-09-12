using UnityEngine;

//
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class Giggle_SceneManager : MonoBehaviour
{

    ////////// Getter & Setter  //////////

    ////////// Method           //////////

    ////////// Unity            //////////

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Basic_Start();
    }

    // Update is called once per frame
    void Update()
    {
        Basic_Update();
    }

    #region BASIC

    ////////// Getter & Setter  //////////

    ////////// Method           //////////

    ////////// Unity            //////////

    protected virtual void Basic_Start()
    {
    }

    protected virtual void Basic_Update()
    {

    }

    #endregion

    #region UI

    [Serializable]
    public class UI_BasicData : IDisposable
    {
        [SerializeField] List<Canvas> Basic_canvases;

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////

        ////////// Constructor & Destroyer  //////////
        public virtual void Basic_Init()
        {
            for(int for0 = 0; for0 < Basic_canvases.Count; for0++)
            {
                Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.MASTER__UI__CANVAS_SCALER_SETTING,
                    Basic_canvases[for0].GetComponent<CanvasScaler>());
            }
        }

        public void Dispose()
        {

        }
    }

    [Serializable]
    public class UI_PopUpData : IDisposable
    {
        [SerializeField] protected Transform Basic_parent;

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////

        public virtual void Basic_Active(string _name)
        {
            Basic_parent.gameObject.SetActive(true);

            //
            for(int for0 = 0; for0 < Basic_parent.childCount; for0++)
            {
                Transform element = Basic_parent.GetChild(for0);
                if (element.name.Equals(_name))
                {
                    element.gameObject.SetActive(true);
                }
                else
                {
                    element.gameObject.SetActive(false);
                }
            }
        }

        public void Basic_DeActive()
        {
            Basic_parent.gameObject.SetActive(false);
        }

        ////////// Constructor & Destroyer  //////////
        public virtual void Basic_Init()
        {
            Basic_DeActive();
        }
        
        public void Dispose()
        {

        }
    }

    ////////// Getter & Setter  //////////

    ////////// Method           //////////
    public virtual void UI_BtnClick(GameObject _btn)
    {

    }
    
    public virtual void UI_PopUpBtnClick(GameObject _btn)
    {

    }

    ////////// Unity            //////////

    protected virtual void UI_Start()
    {

    }

    #endregion
}
