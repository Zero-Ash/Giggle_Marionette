using UnityEngine;
//
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

namespace Giggle_UI
{
    // MenuBar
    [Serializable]
    public class MenuBar : IDisposable
    {
        [SerializeField] protected RectTransform    Basic_parent;
        [SerializeField] protected List<Button>     Basic_list;
        [SerializeField] protected bool             Basic_isMenuVertical;

        ////////// Getter & Setter          //////////
        // Basic_list
        public Button   Basic_GetListBtn(int _count)    { return Basic_list[_count];        }

        public int      Basic_VarListCount              { get { return Basic_list.Count;    }   }

        ////////// Method                   //////////
        public void Basic_Init(string _name0, string _name1)
        {
            if(Basic_list == null)
            {
                Basic_list = new List<Button>();
            }

            float length = 0;
            if(Basic_isMenuVertical)    { length = Basic_parent.sizeDelta.y / Basic_parent.childCount;  }
            else                        { length = Basic_parent.sizeDelta.x / Basic_parent.childCount;  }

            for(int for0 = 0; for0 < Basic_parent.childCount; for0++)
            {
                RectTransform element = Basic_parent.Find(for0.ToString()).GetComponent<RectTransform>();
                if(Basic_isMenuVertical)
                {
                    element.anchoredPosition = new Vector2(0, -length * (for0 + 0.5f));
                    element.sizeDelta = new Vector2(element.sizeDelta.x, length);
                }
                else
                {
                    element.anchoredPosition = new Vector2(length * (for0 + 0.5f), 0);
                    element.sizeDelta = new Vector2(length, element.sizeDelta.y);
                }

                element = element.transform.Find("Button").GetComponent<RectTransform>();
                Basic_list.Add(element.GetComponent<Button>());
            }

            for(int for0 = 0; for0 < Basic_VarListCount; for0++)
            {
                Basic_GetListBtn(for0).name = "Button/" + _name0 + "/" + _name1 + "/" + for0.ToString();
            }
        }

        // Basic_SelectMenu
        public void Basic_SelectMenu(int _count)
        {
            for(int for0 = 0; for0 < Basic_list.Count; for0++)
            {
                bool isClick = for0.Equals(_count);

                Basic_list[for0].gameObject.SetActive(!isClick);
                Basic_SelectMenu__Setting(for0, _count);
            }
        }

        protected virtual void Basic_SelectMenu__Setting(int _for0, int _count)
        {
        }

        ////////// Constructor & Destroyer  //////////
        public void Dispose()
        {

        }
    }

    [Serializable]
    public class MenuBar_SelectScene : MenuBar
    {
        [SerializeField] protected Transform    Basic_menuParent;

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////

        protected override void Basic_SelectMenu__Setting(int _for0, int _count)
        {
            Basic_menuParent.Find(_for0.ToString()).gameObject.SetActive(_for0.Equals(_count));
        }

        ////////// Constructor & Destroyer  //////////
    }

    // ListArray
    [Serializable]
    public class ListArray : IDisposable
    {
        [SerializeField] GameObject Basic_parent;
        [SerializeField] Transform  Basic_btnOpen;

        [SerializeField] List<bool> Basic_isAscendings;
        [SerializeField] int        Basic_select;

        ////////// Getter & Setter          //////////

        // Basic_isAscendings
        public bool Basic_VarSelectAscending    { get{ return Basic_isAscendings[Basic_select]; }   }

        // Basic_select
        public int  Basic_VarSelect { get{ return Basic_select; }   }

        ////////// Method                   //////////
        
        //
        public void Basic_BtnOpen()
        {
            Basic_parent.SetActive(!Basic_parent.activeSelf);
        }

        //
        public void Basic_BtnClick(int _count)
        {
            if(Basic_select.Equals(_count))
            {
                Basic_isAscendings[_count] = !Basic_isAscendings[_count];
                Basic_SettingUI(_count);
            }
            else
            {
                Basic_select = _count;
            }

            Basic_btnOpen.Find("Name").GetComponent<TextMeshProUGUI>().text = Basic_parent.transform.Find(Basic_select.ToString()).Find("Name").GetComponent<TextMeshProUGUI>().text;
            Basic_btnOpen.Find("Image").localRotation = Basic_parent.transform.Find(Basic_select.ToString()).Find("Image").localRotation;
        }

        //
        void Basic_SettingUI(int _count)
        {
            if(Basic_isAscendings[_count])
            {
                Basic_parent.transform.Find(_count.ToString()).Find("Image").localRotation = Quaternion.Euler(0, 0, 180);
            }
            else
            {
                Basic_parent.transform.Find(_count.ToString()).Find("Image").localRotation = Quaternion.Euler(Vector3.zero);
            }
        }

        //
        public void Basic_Reset()
        {
            Basic_parent.SetActive(false);

            for(int for0 = 0; for0 < Basic_isAscendings.Count; for0++)
            {
                Basic_isAscendings[for0] = false;
                Basic_SettingUI(for0);
            }
        }

        // 아이템 리스트 정리
        public void Basic_ArrayList_Item(ref List<int> _list)
        {
            switch(Basic_select)
            {
                case 0: { Basic_ArrayList_Item__SkillClass(ref _list);  }   break;  // 등급
                case 1: break;  // 획득
                case 2: break;  // 착용레벨
                case 3: break;  // 강화
            }
        }
        
        void Basic_ArrayList_Item__SkillClass(ref List<int> _list)
        {
            int whileCount = 0;
            bool isNext = false;
            while(whileCount < _list.Count)
            {
                isNext = true;

                Giggle_Item.Inventory whileItem = (Giggle_Item.Inventory)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.PLAYER__ITEM__GET_ITEM_FROM_INVENTORY_ID, _list[whileCount]);

                Giggle_Item.List whileData = (Giggle_Item.List)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.DATABASE__ITEM__GET_DATA_FROM_ID,
                    whileItem.Basic_VarDataId);

                //
                for(int for0 = 0; for0 < whileCount; for0++)
                {
                    Giggle_Item.Inventory for0Item = (Giggle_Item.Inventory)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                        Giggle_ScriptBridge.EVENT.PLAYER__ITEM__GET_ITEM_FROM_INVENTORY_ID, _list[for0]);

                    Giggle_Item.List for0Data = (Giggle_Item.List)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                        Giggle_ScriptBridge.EVENT.DATABASE__ITEM__GET_DATA_FROM_ID,
                        for0Item.Basic_VarDataId);
                    
                    //
                    if(Basic_VarSelectAscending)
                    {
                        if(for0Data.Basic_VarSkillClass > whileData.Basic_VarSkillClass)
                        {
                            int id = _list[whileCount];
                            _list.RemoveAt(whileCount);
                            _list.Insert(for0, id);

                            isNext = false;
                            break;
                        }
                    }
                    else
                    {
                        if(for0Data.Basic_VarSkillClass < whileData.Basic_VarSkillClass)
                        {
                            int id = _list[whileCount];
                            _list.RemoveAt(whileCount);
                            _list.Insert(for0, id);

                            isNext = false;
                            break;
                        }
                    }
                }

                //
                if(isNext)
                {
                    whileCount++;
                }
            }
        }

        //
        public void Basic_Init()
        {
            Basic_parent.SetActive(false);

            //
            if(Basic_isAscendings == null)
            {
                Basic_isAscendings = new List<bool>();
            }
            while(Basic_isAscendings.Count < Basic_parent.transform.childCount)
            {
                Basic_isAscendings.Add(false);
            }

            //
            Basic_select = 0;
        }
        
        ////////// Constructor & Destroyer  //////////
        public void Dispose()
        {

        }
    }

    #region ScrollView

    [Serializable]
    public class ScrollView : IDisposable
    {
        [SerializeField] protected RectTransform    Basic_content;
        [SerializeField] protected List<Transform>  Basic_list;
        [SerializeField] protected int              Basic_x;

        [SerializeField] protected Vector2  Basic_startPos;
        [SerializeField] protected Vector2  Basic_varPos;

        [SerializeField] protected RawImage Basic_rawImage;

        ////////// Getter & Setter          //////////
        //
        public RectTransform    Basic_VarContent    { get { return Basic_content;   }   }

        // Basic_list
        //public Button   Basic_GetListBtn(int _count)    { return Basic_list[_count];        }

        public int      Basic_VarListCount              { get { return Basic_list.Count;    }   }

        protected void Basic_SetListPosition(int _count)
        {
            Basic_list[_count].localPosition
            = new Vector3(
                Basic_startPos.x + (Basic_varPos.x * (float)(_count % Basic_x)),
                Basic_startPos.y + (Basic_varPos.y * (float)(_count / Basic_x)),
                0);
        }

        ////////// Method                   //////////

        // Basic_Init
        public void Basic_Init()
        {
            Basic_startPos = Basic_list[0].localPosition;
            Basic_varPos.x = Basic_list[1].localPosition.x - Basic_list[0].localPosition.x;
            Basic_varPos.y = Basic_list[1].localPosition.y - Basic_list[0].localPosition.y;

            Basic_AddList();

            Basic_AddList__SetName(Basic_list[0], 0);
            Basic_AddList__SetName(Basic_list[1], 1);
            Basic_SetListPosition(0);
            Basic_SetListPosition(1);
        }

        // Basic_AddList
        protected void Basic_AddList()
        {
            int whileCount = 0;
            while(whileCount < 100)
            {
                Transform element = GameObject.Instantiate(Basic_list[0], Basic_content);
                Basic_AddList__SetName(element.transform, Basic_list.Count - 1);
                Basic_list.Add(element);
                Basic_SetListPosition(Basic_list.Count - 1);

                whileCount++;
            }

            Transform target = Basic_list[Basic_list.Count - 1].transform;
            Basic_content.sizeDelta = new Vector2(0, (target.GetComponent<RectTransform>().sizeDelta.y * 0.5f) + 10.0f - target.localPosition.y);
        }

        protected virtual void Basic_AddList__SetName(Transform _element, int _num)
        {

        }

        // Basic_ClickBtn
        public virtual void Basic_ClickBtn(int _count)
        {
        }

        ////////// Constructor & Destroyer  //////////
        public void Dispose()
        {
            
        }
    }

    [Serializable]
    public class ScrollView_Attribute : IDisposable
    {
        [SerializeField] protected RectTransform    Basic_content;
        [SerializeField] protected List<Transform>  Basic_list;

        [SerializeField] protected Transform Basic_info;

        ////////// Getter & Setter          //////////
        public Transform    Basic_VarInfo   { get { return Basic_info;  }   }

        public Transform    Basic_GetListElementFromId(int _id)
        {
            Transform res = null;

            //
            for(int for0 = 0; for0 < Basic_list.Count; for0++)
            {
                if(int.Parse(Basic_list[for0].Find("ButtonElement").GetChild(0).name.Split('/')[3]).Equals(_id))
                {
                    res = Basic_list[for0];
                    break;
                }
            }

            //
            return res;
        }

        ////////// Method                   //////////
        public void Basic_Init()
        {
            Vector2 startPos;
            Vector2 varPos;
            startPos    = Basic_list[0].localPosition;
            varPos.x    = Basic_list[1].localPosition.x - Basic_list[0].localPosition.x;
            varPos.y    = Basic_list[1].localPosition.y - Basic_list[0].localPosition.y;

            IEnumerator coroutine = Basic_Init__Override(startPos, varPos);
            Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MASTER__BASIC__START_COROUTINE, coroutine);
        }

        protected virtual IEnumerator Basic_Init__Override(Vector2 _startPos, Vector2 _varPos)
        {
            yield return null;
        }

        ////////// Constructor & Destroyer  //////////
        public void Dispose()
        {
            
        }
    }

    #endregion

    #region FORMATION

    [Serializable]
    public class Formation : IDisposable
    {
        #region BASIC

        public enum Basic__PLAY_TYPE
        {
            PVP,
            PVE
        }

        [Header("BASIC ==================================================")]
        [SerializeField]            Basic__PLAY_TYPE    Basic_playType;
        [SerializeField] protected  List<int>    Basic_datas;

        ////////// Getter & Setter          //////////
        public Basic__PLAY_TYPE Basic_VarPlayerType { get { return Basic_playType;  } set { Basic_playType = value; }   }

        //
        public List<int>    Basic_VarDatas  { get { return Basic_datas; }   }

        ////////// Method                   //////////

        // Basic_Reset
        public void Basic_Reset()
        {
            for(int for0 = 0; for0 < Basic_datas.Count; for0++)
            {
                Basic_datas[for0] = -1;
            }

            switch(Basic_playType)
            {
                case Basic__PLAY_TYPE.PVE:  { Basic_Reset__PVE();   }   break;
            }

            UI_Reset();
        }

        protected virtual void Basic_Reset__PVE()
        {
            List<int> formation = (List<int>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.PLAYER__FORMATION__GET_SELECT_FORMATION);
            for(int for0 = 0; for0 < formation.Count; for0++)
            {
                Basic_datas[for0] = formation[for0];
            }
        }

        //
        public void Basic_Change(int _count, int _id)
        {
            Basic_datas[_count] = _id;

            UI_Reset();
        }

        //
        public void Basic_Init()
        {
            Basic_playType = Basic__PLAY_TYPE.PVE;

            if(Basic_datas == null)
            {
                Basic_datas = new List<int>();
            }
            while(Basic_datas.Count < 9)
            {
                Basic_datas.Add(-1);
            }

            //
            UI_Init();
        }

        ////////// Constructor & Destroyer  //////////

        //
        public void Dispose()
        {
            
        }

        #endregion

        #region UI

        [Header("UI ==================================================")]
        [SerializeField] Transform          UI_parent;
        [SerializeField] List<Transform>    UI_list;

        [SerializeField] protected RawImage UI_rawImage;

        [SerializeField] protected int      UI_selectFormation;

        ////////// Getter & Setter          //////////

        public int  UI_VarSelectFormation   { get { return UI_selectFormation;  }   }

        ////////// Method                   //////////
        
        //////////
        // UI_BtnClick__Tile
        public bool UI_BtnClick__Tile(int _count, int _selectMarionette)
        {
            bool isChange = false;

            //
            if(_count == -1)
            {
                UI_BtnClick__Tile__Tile__Select(-1);
            }
            else
            {
                switch(_selectMarionette)
                {
                    case -1:    { isChange = UI_BtnClick__Tile__Tile(_count);                       }   break;
                    default:    { isChange = UI_BtnClick__Tile__Change(_count, _selectMarionette);  }   break;
                }
            }

            //
            return isChange;
        }

        // UI_BtnClick__Tile__Tile
        bool UI_BtnClick__Tile__Tile(int _count)
        {
            bool isChange = false;

            //
            switch(UI_selectFormation)
            {
                case -1:    { UI_BtnClick__Tile__Tile__Select(_count);              }   break;
                default:    { isChange = UI_BtnClick__Tile__Tile__Change(_count);   }   break;
            }

            //
            return isChange;
        }

        void UI_BtnClick__Tile__Tile__Select(int _count)
        {
            for (int for0 = 0; for0 < UI_list.Count; for0++)
            {
                UI_list[for0].Find("Select").gameObject.SetActive(for0.Equals(_count));
            }

            //
            UI_selectFormation = _count;
        }

        protected virtual bool UI_BtnClick__Tile__Tile__Change(int _count)
        {
            bool isChange = false;

            //
            if(!Basic_datas[_count].Equals(-2))
            {
                //
                int id = Basic_datas[UI_selectFormation];
                Basic_datas[UI_selectFormation] = Basic_datas[_count];
                Basic_Change(_count, id);

                isChange = true;
            }

            //
            return isChange;
        }

        // UI_BtnClick__Tile__Change
        protected virtual bool UI_BtnClick__Tile__Change(int _count, int _selectMarionette)
        {
            bool isChange = false;

            //
            if (!Basic_datas[_count].Equals(-2))
            {
                //
                Basic_Change(_count, _selectMarionette);
                //Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                //    Giggle_ScriptBridge.EVENT.PLAYER__FORMATION__FORMATION_SETTING,
                //    _selectMarionette, _count);

                isChange = true;
            }

            //
            return isChange;
        }
        
        //
        void UI_Reset()
        {
            UI_BtnClick__Tile__Tile__Select(-1);

            // 타일 처리
            for(int for0 = 0; for0 < UI_list.Count; for0++)
            {
                // 기존 마리오네트 모델링 날리기
                while(UI_list[for0].Find("Obj").childCount > 0)
                {
                    Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                        Giggle_ScriptBridge.EVENT.MASTER__GARBAGE__REMOVE,
                        UI_list[for0].Find("Obj").GetChild(0));
                }

                // 배치된 모델링이 있다면 모델링 배치
                switch(Basic_playType)
                {
                    case Basic__PLAY_TYPE.PVE:  { UI_Reset__PVE(for0);  }   break;
                    case Basic__PLAY_TYPE.PVP:  { UI_Reset__PVP(for0);  }   break;
                }
            }
        }

        void UI_Reset__PVE(int _for0)
        {
            int tileId = Basic_datas[_for0];
            if(!tileId.Equals(-1))
            {
                // 주인공
                if(tileId.Equals(-2))
                {
                    Giggle_Character.Save pinocchioData
                        = (Giggle_Character.Save)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                            Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__VAR_DATA);
                    Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                        Giggle_ScriptBridge.EVENT.MASTER__UI__PINOCCHIO_INSTANTIATE,
                        //
                        pinocchioData.Basic_VarDataId,
                        UI_list[_for0].Find("Obj"), 300.0f);
                }
                else
                {
                    Giggle_Character.Save data
                        = (Giggle_Character.Save)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                            Giggle_ScriptBridge.EVENT.PLAYER__MARIONETTE__GET_DATA_FROM_INVENTORYID,
                            tileId);
                    Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                        Giggle_ScriptBridge.EVENT.MASTER__UI__CHARACTER_INSTANTIATE,
                        //
                        data.Basic_VarDataId,
                        UI_list[_for0].Find("Obj"), 300.0f);
                }
            }
        }

        void UI_Reset__PVP(int _for0)
        {
            Giggle_Unit unit = (Giggle_Unit)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.MASTER__UI__PINOCCHIO_INSTANTIATE,
                //
                Basic_datas[_for0],
                UI_list[_for0].Find("Obj"), 300.0f);

                //unit
        }

        //
        void UI_Init()
        {
            if(UI_list == null)
            {
                UI_list = new List<Transform>();
            }

            int whileCount = 0;
            while(whileCount < UI_parent.childCount)
            {
                Transform for0Child = UI_parent.GetChild(whileCount);
                for(int for0 = 0; for0 < for0Child.childCount; for0++)
                {
                    UI_list.Add(for0Child.GetChild(for0));
                }

                whileCount++;
            }

            whileCount = 0;
            while(whileCount < UI_list.Count)
            {
                for(int for0 = whileCount; for0 < UI_list.Count; for0++)
                {
                    Transform element = UI_list[for0];
                    if(element.name.Equals(whileCount.ToString()))
                    {
                        UI_Init__Btn(element, whileCount);
                        //element.Find("Button").name = "Button/MARIONETTE/FORMATION__TILE/" + whileCount;
                        UI_list.RemoveAt(for0);
                        UI_list.Insert(whileCount, element);
                    }
                }

                whileCount++;
            }
        }

        protected virtual void UI_Init__Btn(Transform _element, int _count)
        {
            _element.Find("Button").name = "Button/MARIONETTE/FORMATION__TILE/" + _count;
        }

        ////////// Constructor & Destroyer  //////////


        #endregion
    }

    #endregion
}
