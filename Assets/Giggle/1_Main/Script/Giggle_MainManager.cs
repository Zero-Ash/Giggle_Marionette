using UnityEngine;

//
using System;

public class Giggle_MainManager : Giggle_SceneManager
{

    #region BASIC

    ////////// Getter & Setter  //////////

    ////////// Method           //////////

    ////////// Unity            //////////

    #endregion

    #region UI
    [Serializable]
    public class UI_MainBasicData : UI_BasicData
    {

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////

        ////////// Constructor & Destroyer  //////////
        
        #region AREA4

        [Header("AREA4 ==================================================")]
        [SerializeField] GameObject Area4_exMenu;

        ////////// Getter & Setter          //////////
        public bool Area4_VarExMenuActive   { set { Area4_exMenu.SetActive(value);  }   }

        ////////// Method                   //////////

        ////////// Constructor & Destroyer  //////////

        #endregion

    }

    [Serializable]
    public class UI_MainPopUpData : UI_PopUpData
    {
        ////////// Getter & Setter          //////////

        ////////// Method                   //////////

        ////////// Constructor & Destroyer  //////////
    }

    [Header("UI ==================================================")]
    [SerializeField] UI_MainBasicData   UI_basicData;
    [SerializeField] UI_MainPopUpData   UI_popUpData;

    ////////// Getter & Setter  //////////

    ////////// Method           //////////
    public override void UI_BtnClick(GameObject _btn)
    {
        string[] names = _btn.name.Split('/');
        switch(names[1])
        {
            case "AREA4":   { UI_BtnClick__AREA4(names);    }   break;
        }
    }

    void UI_BtnClick__AREA4(string[] _names)
    {
        switch(_names[2])
        {
            case "EXMENU_OPEN":     { UI_basicData.Area4_VarExMenuActive = true;    }   break;
            case "EXMENU_CLOSE":    { UI_basicData.Area4_VarExMenuActive = false;   }   break;
        }
    }

    ////////// Unity            //////////

    #endregion

    #region 

    ////////// Getter & Setter  //////////

    ////////// Method           //////////

    ////////// Unity            //////////

    #endregion
}