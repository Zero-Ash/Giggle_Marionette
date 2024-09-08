using UnityEngine;
//
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

namespace Giggle_UI
{
    [Serializable]
    public class MenuBar : IDisposable
    {
        [SerializeField] protected RectTransform    Basic_parent;
        [SerializeField] protected List<Button>     Basic_list;
        [SerializeField] protected Transform        Basic_menuParent;

        ////////// Getter & Setter          //////////
        // Basic_list
        public Button   Basic_GetListBtn(int _count)    { return Basic_list[_count];        }

        public int      Basic_VarListCount              { get { return Basic_list.Count;    }   }

        ////////// Method                   //////////
        public void Basic_Init()
        {
            if(Basic_list == null)
            {
                Basic_list = new List<Button>();
            }

            float width = Basic_parent.sizeDelta.x / Basic_parent.childCount;
            for(int for0 = 0; for0 < Basic_parent.childCount; for0++)
            {
                RectTransform element = Basic_parent.Find(for0.ToString()).GetComponent<RectTransform>();
                element.anchoredPosition = new Vector2(width * (for0 + 0.5f), 0);
                element.sizeDelta = new Vector2(width, element.sizeDelta.y);

                element = element.transform.Find("Button").GetComponent<RectTransform>();
                Basic_list.Add(element.GetComponent<Button>());
            }
        }

        public virtual void Basic_SelectMenu(int _count)
        {
            for(int for0 = 0; for0 < Basic_list.Count; for0++)
            {
                bool isClick = for0.Equals(_count);

                Basic_list[for0].gameObject.SetActive(!isClick);
                Basic_menuParent.Find(for0.ToString()).gameObject.SetActive(for0.Equals(_count));
            }
        }

        ////////// Constructor & Destroyer  //////////
        public void Dispose()
        {

        }
    }

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

        [SerializeField] protected Vector2          Basic_startPos;
        [SerializeField] protected Vector2          Basic_varPos;

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

            Basic_Init__SetName();
            Basic_SetListPosition(0);
            Basic_SetListPosition(1);
        }

        protected virtual void Basic_Init__SetName()
        {

        }

        // Basic_AddList
        protected void Basic_AddList()
        {
            int whileCount = 0;
            while(whileCount < 100)
            {
                Transform element = GameObject.Instantiate(Basic_list[0], Basic_content);
                Basic_AddList__SetName(element.transform);
                Basic_list.Add(element);
                Basic_SetListPosition(Basic_list.Count - 1);

                whileCount++;
            }

            Transform target = Basic_list[Basic_list.Count - 1].transform;
            Basic_content.sizeDelta = new Vector2(0, (target.GetComponent<RectTransform>().sizeDelta.y * 0.5f) + 10.0f - target.localPosition.y);
        }

        protected virtual void Basic_AddList__SetName(Transform _element)
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
}
