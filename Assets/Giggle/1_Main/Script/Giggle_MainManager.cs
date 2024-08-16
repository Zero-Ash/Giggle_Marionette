using UnityEngine;

//
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using Unity.Mathematics;
using System.Collections;
using TMPro;

public partial class Giggle_MainManager : Giggle_SceneManager
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
        [SerializeField] Giggle_MainManager Basic_manager;
        [SerializeField] Giggle_MainManager_Marionette Basic_marionetteManager;

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////

        ////////// Constructor & Destroyer  //////////
        public void Basic_Init()
        {
            Pinocchio_data.Basic_Init();
        }
        
        #region AREA4

        [Header("AREA4 ==================================================")]
        [SerializeField] GameObject Area4_exMenu;

        ////////// Getter & Setter          //////////
        //bool Area4_VarExMenuActive   { set { Area4_exMenu.SetActive(value);  }   }

        ////////// Method                   //////////

        public void Area4_BtnClick(string[] _names)
        {
            switch(_names[2])
            {
                case "EXMENU_OPEN":     { Area4_exMenu.SetActive(true);     }   break;
                case "EXMENU_CLOSE":    { Area4_exMenu.SetActive(false);    }   break;
            }
        }

        ////////// Constructor & Destroyer  //////////

        #endregion
        
        #region AREA7

        [Header("AREA7 ==================================================")]
        [SerializeField] GameObject Area7_exMenu;

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////

        public void Area7_BtnClick(string[] _names)
        {
            switch(_names[2])
            {
                case "GACHA":       { Area7_Gacha();        }   break;
                case "PINOCCHIO":   { Area7_PinocchioOn();  }   break;
                case "MARIONETTE":  { Area7_MarionetteOn(); }   break;
            }
        }

        void Area7_Gacha()
        {
            Basic_manager.UI_PopUpActive("GACHA");
        }

        void Area7_PinocchioOn()
        {
            Pinocchio_data.Basic_Active();
        }

        void Area7_MarionetteOn()
        {
            Basic_marionetteManager.Basic_Active();
        }

        ////////// Constructor & Destroyer  //////////

        #endregion

    }

    [Serializable]
    public class UI_MainPopUpData : UI_PopUpData
    {
        public enum LIST
        {
            GACHA
        }

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////

        public override void Basic_Active(string _name)
        {
            base.Basic_Active(_name);

            switch(_name)
            {
                case "GACHA":   { Gacha_Reset();    }   break;
            }
        }

        ////////// Constructor & Destroyer  //////////
        public override void Basic_Init()
        {
            base.Basic_Init();

            Gacha_Init();
        }

        #region GACHA

        [Serializable]
        public class Gacha_List : IDisposable
        {
            [SerializeField] Giggle_Character.Database Basic_character;

            // Background
            [SerializeField] GameObject Basic_background;

            // Info
            [SerializeField] Transform              Basic_infoModel;
            [SerializeField] TMPro.TextMeshProUGUI  Basic_infoName;
            [SerializeField] TMPro.TextMeshProUGUI  Basic_infoType;
            [SerializeField] TMPro.TextMeshProUGUI  Basic_infoAttribute;

            // Status
            [SerializeField] List<TMPro.TextMeshProUGUI>    Basic_statusList;

            ////////// Getter & Setter          //////////
            public int  Basic_VarCharacterId    { get { return Basic_character.Basic_VarId; }   }

            public bool Basic_VarBackgroundActive   { set {Basic_background.SetActive(value);   }   }

            ////////// Method                   //////////
            public void Basic_Gacha(List<Giggle_Character.Database> _list)
            {
                Basic_background.SetActive(false);

                // TODO:나중에 가챠 조건이 생기면 여기에 기입합시다.
                Basic_character = _list[UnityEngine.Random.Range(0, _list.Count)];

                // UI갱신
                while(Basic_infoModel.childCount > 0)
                {
                    Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                        Giggle_ScriptBridge.EVENT.MASTER__GARBAGE__REMOVE,
                        //
                        Basic_infoModel.GetChild(0));
                }
                Giggle_Unit model = Instantiate(Basic_character.Basic_VarUnit, Basic_infoModel);
                Basic_Gacha__ChangeModelLayer(model.transform);
                model.transform.localPosition = Vector3.zero;
                model.transform.localRotation = quaternion.Euler(-90,0,0);
                model.transform.localScale = new Vector3(600, 600, 600);

                Basic_infoName.text      = Basic_character.Basic_VarName;
                Basic_infoType.text      = Basic_character.Basic_VarCategory.ToString();
                Basic_infoAttribute.text = Basic_character.Basic_VarAttribute.ToString();

                Basic_statusList[0].text = Basic_character.Basic_GetStatusList(0).Basic_VarAttack.ToString();
                Basic_statusList[1].text = Basic_character.Basic_GetStatusList(0).Basic_VarDefence.ToString();
                Basic_statusList[2].text = Basic_character.Basic_GetStatusList(0).Basic_VarHp.ToString();
                Basic_statusList[3].text = Basic_character.Basic_GetStatusList(0).Basic_VarAttackSpeed.ToString();
            }

            void Basic_Gacha__ChangeModelLayer(Transform _parent)
            {
                _parent.gameObject.layer = 5;

                for(int for0 = 0; for0 < _parent.childCount; for0++)
                {
                    Basic_Gacha__ChangeModelLayer(_parent.GetChild(for0));
                }
            }

            ////////// Constructor & Destroyer  //////////
            public Gacha_List(Transform _trans)
            {
                Basic_background = _trans.Find("Back").gameObject;

                // Info
                Transform element = _trans.Find("Data").Find("Info");
                Basic_infoModel     = element.Find("Model");
                Basic_infoName      = element.Find("Text (TMP)_Name").GetComponent<TMPro.TextMeshProUGUI>();
                Basic_infoType      = element.Find("Text (TMP)_Type").GetComponent<TMPro.TextMeshProUGUI>();
                Basic_infoAttribute = element.Find("Text (TMP)_Attribute").GetComponent<TMPro.TextMeshProUGUI>();

                // Status
                if(Basic_statusList == null)
                {
                    Basic_statusList = new List<TMPro.TextMeshProUGUI>();
                }

                int whileCount = 0;
                while(whileCount < 4)
                {
                    Basic_statusList.Add(null);
                    whileCount++;
                }

                element = _trans.Find("Data").Find("Status");
                whileCount = 0;
                while(whileCount < 4)
                {
                    Basic_statusList[whileCount] = element.Find(whileCount.ToString()).Find("Text (TMP) (1)").GetComponent<TMPro.TextMeshProUGUI>();
                    whileCount++;
                }
            }

            public void Dispose()
            {

            }
        }

        [Header("GACHA ==================================================")]
        [SerializeField] List<Gacha_List>   Gacha_list;
        //
        [SerializeField] GameObject             Gacha_changeBtn;
        [SerializeField] TMPro.TextMeshProUGUI  Gacha_changeText;
        //
        [SerializeField] GameObject Gacha_selectBtn;

        [Header("RUNNING")]
        [SerializeField] int Gacha_gachaCount;
        [SerializeField] int Gacha_select;

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////
        public void Gacha_BtnClick(string[] _names)
        {
            switch(_names[2])
            {
                case "CHARACTER":   { Gacha_BtnClick__CHARACTER(_names[3]); }   break;
                case "CHANGE":      { Gacha_BtnClick__CHANGE();             }   break;
                case "SELECT":      { Gacha_BtnClick__SELECT();             }   break;
            }
        }

        void Gacha_BtnClick__CHARACTER(string _num)
        {
            Gacha_select = int.Parse(_num);
            Gacha_selectBtn.SetActive(true);

            //
            for(int for0 = 0; for0 < Gacha_list.Count; for0++)
            {
                Gacha_list[for0].Basic_VarBackgroundActive = Gacha_select.Equals(for0);
            }
        }

        void Gacha_BtnClick__CHANGE()
        {
            Gacha_Gacha();
        }

        void Gacha_BtnClick__SELECT()
        {
            Basic_parent.gameObject.SetActive(false);

            // TODO:스킬도 넣어줘야 합니다!
            Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.PLAYER__MARIONETTE__ADD,
                //
                Gacha_list[Gacha_select].Basic_VarCharacterId);
        }
        
        //
        void Gacha_Reset()
        {
            //
            Gacha_changeBtn.SetActive(true);

            //
            Gacha_gachaCount = 4;
            Gacha_Gacha();
        }

        void Gacha_Gacha()
        {
            Gacha_select = -1;
            Gacha_selectBtn.SetActive(false);

            // 가챠 리스트 불러오기
            List<Giggle_Character.Database> list
                = (List<Giggle_Character.Database>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.DATABASE__MARIONETTE__GET_DATAS_FROM_ATTRIBUTE,
                    Giggle_Master.ATTRIBUTE.FIRE.ToString());
            
            for(int for0 = 0; for0 < Gacha_list.Count; for0++)
            {
                Gacha_list[for0].Basic_Gacha(list);
            }

            // 가챠 교환횟수 차감 및 텍스트 갱신
            Gacha_gachaCount--;

            Gacha_changeText.text = "Change (" + Gacha_gachaCount + "/3)";

            // 가챠 교환횟수가 0이라면 버튼 비활성화
            if(Gacha_gachaCount <= 0)
            {
                Gacha_changeBtn.SetActive(false);
            }
        }

        ////////// Constructor & Destroyer  //////////
        void Gacha_Init()
        {
            Transform main = Basic_parent.Find("GACHA");

            // List
            if(Gacha_list == null)
            {
                Gacha_list = new List<Gacha_List>();
            }

            Transform list = main.Find("List");
            int whileCount = 0;
            while(whileCount < list.childCount)
            {
                Gacha_list.Add(null);
                whileCount++;
            }

            for(int for0 = 0; for0 < list.childCount; for0++)
            {
                Transform forTrans = list.GetChild(for0);

                int count = int.Parse(forTrans.name);

                Gacha_List element = new Gacha_List(forTrans);
                Gacha_list[count] = element;
            }
        }

        #endregion
    }

    [Header("UI ==================================================")]
    [SerializeField] UI_MainBasicData   UI_basicData;
    [SerializeField] UI_MainPopUpData   UI_popUpData;

    ////////// Getter & Setter  //////////
    public UI_MainBasicData UI_VarBasicData { get { return UI_basicData;    }   }

    ////////// Method           //////////
    // UI_basicData
    public override void UI_BtnClick(GameObject _btn)
    {
        string[] names = _btn.name.Split('/');
        switch(names[1])
        {
            case "AREA4":   { UI_basicData.Area4_BtnClick(names);   }   break;
            case "AREA7":   { UI_basicData.Area7_BtnClick(names);   }   break;
            //
            case "PINOCCHIO":   { UI_basicData.Pinocchio_VarData.Basic_BtnClick(names);     }   break;
            //case "MARIONETTE":  { UI_basicData.Marionette_VarData.Basic_BtnClick(names);    }   break;
        }
    }
    
    // UI_popUpData
    public override void UI_PopUpBtnClick(GameObject _btn)
    {
        string[] names = _btn.name.Split('/');
        switch(names[1])
        {
            case "GACHA":   { UI_popUpData.Gacha_BtnClick(names);   }   break;
        }
    }

    public void UI_PopUpActive(string _name)
    {
        UI_popUpData.Basic_Active(_name);
    }

    ////////// Unity            //////////
    protected override void UI_Start()
    {
        UI_basicData.Basic_Init();
        UI_popUpData.Basic_Init();
    }

    #endregion

    #region 

    ////////// Getter & Setter  //////////

    ////////// Method           //////////

    ////////// Unity            //////////

    #endregion
}