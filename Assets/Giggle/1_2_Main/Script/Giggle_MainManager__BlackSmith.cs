using UnityEngine;

//
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class Giggle_MainManager__BlackSmith : MonoBehaviour
{

    #region BASIC

    [SerializeField] GameObject Basic_back;
    [SerializeField] GameObject Basic_ui;
    [SerializeField] RectTransform  Basic_safeArea;

    [SerializeField] Giggle_UI.MenuBar BasicArea1_menuBar;

    [Header("RUNNING")]
    [SerializeField] IEnumerator    Basic_coroutine;

    ////////// Getter & Setter  //////////
    
    ////////// Method           //////////
    public void Basic_BtnClick(GameObject _obj)
    {
        string[] names = _obj.name.Split('/');
        switch(names[2])
        {
            case "CLOSE":       { Basic_BtnClick__Close();                          }   break;
            case "MENU_BAR":    { BasicArea1_BtnSelectMenu(int.Parse(names[3]));    }   break;
            //
            case "WEAPON__SCROLL_VIEW":     { Weapon_BtnScrollView(int.Parse(names[3]));    }   break;
            case "WEAPON__CRAFTING__BACK":  { Weapon_BtnCraftingBack();                     }   break;
            case "WEAPON__CRAFTING__DOING": { Weapon_BtnCraftingDoing();                    }   break;
        }
    }

    void Basic_BtnClick__Close()
    {
        Basic_back.SetActive(false);
        Basic_ui.SetActive(false);
    }

    void BasicArea1_BtnSelectMenu(int _count)
    {
        BasicArea1_menuBar.Basic_SelectMenu(_count);
    }

    // Basic_Active
    public void Basic_Active()
    {
        Basic_back.SetActive(true);

        Basic_coroutine = Basic_Active__Coroutine();
        Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MASTER__BASIC__START_COROUTINE, Basic_coroutine);
    }

    IEnumerator Basic_Active__Coroutine()
    {
        int phase = 0;

        while(phase >= 0)
        {
            switch(phase)
            {
                // 아이템
                case 0:
                    {
                        if((bool)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.DATABASE__ITEM__GET_IS_OPEN))
                        {
                            Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.DATABASE__ITEM__GET_DATA_FROM_ID, 701101001);

                            phase = 1;
                        }
                    }
                    break;
                case 1:
                    {
                        if((bool)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.DATABASE__ITEM__GET_IS_OPEN))
                        {
                            phase = 10;
                        }
                    }
                    break;

                case 10:
                    {
                        if((bool)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.DATABASE__ITEM__GET_IS_OPEN))
                        {
                            Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.DATABASE__ITEM__GET_DATA_FROM_ID, 72101001);

                            phase = 11;
                        }
                    }
                    break;
                case 11:
                    {
                        if((bool)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.DATABASE__ITEM__GET_IS_OPEN))
                        {
                            phase = 20;
                        }
                    }
                    break;

                case 20:
                    {
                        if((bool)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.DATABASE__ITEM__GET_IS_OPEN))
                        {
                            Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.DATABASE__ITEM__GET_DATA_FROM_ID, 74101001);

                            phase = 21;
                        }
                    }
                    break;
                case 21:
                    {
                        if((bool)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.DATABASE__ITEM__GET_IS_OPEN))
                        {
                            phase = 30;
                        }
                    }
                    break;

                case 30:
                    {
                        if((bool)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.DATABASE__ITEM__GET_IS_OPEN))
                        {
                            Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.DATABASE__ITEM__GET_DATA_FROM_ID, 76101001);

                            phase = 31;
                        }
                    }
                    break;
                case 31:
                    {
                        if((bool)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.DATABASE__ITEM__GET_IS_OPEN))
                        {
                            phase = 40;
                        }
                    }
                    break;
                    
                case 40:
                    {
                        if((bool)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.DATABASE__ITEM__GET_IS_OPEN))
                        {
                            Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.DATABASE__ITEM__GET_DATA_FROM_ID, 78101001);

                            phase = 41;
                        }
                    }
                    break;
                case 41:
                    {
                        if((bool)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.DATABASE__ITEM__GET_IS_OPEN))
                        {
                            phase = 10000;
                        }
                    }
                    break;
                // 캐릭터
                case 10000:
                    {
                        phase = -1;
                    }
                    break;
            }

            yield return null;
        }

        Weapon_Active();

        BasicArea1_BtnSelectMenu(0);

        Basic_ui.SetActive(true);

        Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MASTER__BASIC__STOP_COROUTINE, Basic_coroutine);
    }

    ////////// Unity            //////////

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Basic_back.SetActive(false);
        Basic_ui.SetActive(false);
        
        BasicArea1_menuBar.Basic_Init();
        for(int for0 = 0; for0 < BasicArea1_menuBar.Basic_VarListCount; for0++)
        {
            BasicArea1_menuBar.Basic_GetListBtn(for0).name = "Button/MARIONETTE/MENU_BAR/" + for0.ToString();
        }

        Weapon_Start();

        StartCoroutine(Start__Coroutine());
    }

    IEnumerator Start__Coroutine()
    {
        int phase = 0;
        while(phase >= 0)
        {
            switch(phase)
            {
                case 0:
                    {
                        if(Giggle_ScriptBridge.Basic_VarInstance.Basic_GetIsInMethod(Giggle_ScriptBridge.EVENT.MASTER__UI__SAFE_AREA_VAR_SIZE_DELTA ))
                        {
                            Basic_safeArea.sizeDelta        = (Vector2)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MASTER__UI__SAFE_AREA_VAR_SIZE_DELTA );
                            Basic_safeArea.localPosition    = (Vector2)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MASTER__UI__SAFE_AREA_VAR_POSITION   );

                            phase = -1;
                        }
                    }
                    break;
            }

            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion

    #region CLASS
    #endregion

    #region WEAPON

    //[Serializable]
    //public class ScrollerView_MenuBar__Weapon : Giggle_UI.MenuBar
    //{
    //    [SerializeField] Giggle_MainManager__BlackSmith Basic_uiData;
    //
    //    ////////// Getter & Setter          //////////
    //
    //    ////////// Method                   //////////
    //
    //    protected override void Basic_SelectMenu__Setting(int _for0, int _count)
    //    {
    //        if(_for0.Equals(_count))
    //        {
    //            Basic_uiData.Formation_VarScrollView.Basic_SelectMenuBar(_count);
    //        }
    //    }
    //
    //    ////////// Constructor & Destroyer  //////////
    //    public void Basic_Init(Giggle_MainManager__BlackSmith _uiData)
    //    {
    //        Basic_uiData = _uiData;
    //        Basic_Init();
    //    }
    //}

    [Serializable]
    public class ScrollView_Weapon : Giggle_UI.ScrollView
    {
        [SerializeField] Giggle_MainManager__BlackSmith Basic_uiData;

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////

        protected override void Basic_Init__SetName()
        {
            Basic_list[0].Find("Button").name = "Button/BLACK_SMITH/WEAPON__SCROLL_VIEW/0";
            Basic_list[1].Find("Button").name = "Button/BLACK_SMITH/WEAPON__SCROLL_VIEW/1";
        }

        protected override void Basic_AddList__SetName(Transform _element)
        {
            _element.Find("Button").name = "Button/BLACK_SMITH/WEAPON__SCROLL_VIEW/" + Basic_list.Count;
        }

        public override void Basic_ClickBtn(int _count)
        {
            // ui 그래픽 갱신
            for(int for0 = 0; for0 < Basic_list.Count; for0++)
            {
                Basic_list[for0].Find("Select").gameObject.SetActive(for0.Equals(_count));
            }
        }

        // Basic_SelectMenuBar
        public void Basic_SelectMenuBar(int _count)
        {
            //Basic_uiData.Formation_SelectTypeFormList(_count);

            Basic_SelectMenuBar__Check();

            //Basic_ClickBtn(-1);
        }

        void Basic_SelectMenuBar__Check()
        {
            List<int> list = Basic_uiData.Weapon_VarList;

            //
            int finalCount = 0;
            int whileCount = 0;
            while(whileCount < Basic_list.Count)
            {
                if(whileCount < list.Count)
                {
                    Basic_list[whileCount].gameObject.SetActive(true);

                    // 데이터 셋팅
                    Giggle_Item.List data
                        = (Giggle_Item.List)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                            Giggle_ScriptBridge.EVENT.DATABASE__ITEM__GET_DATA_FROM_ID,
                            list[whileCount]);


                    //
                    Sprite sprite = null;
                    switch(data.Basic_VarType)
                    {
                        case Giggle_Item.TYPE.ACCESSORY:
                            {
                                sprite
                                    = (Sprite)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                                        Giggle_ScriptBridge.EVENT.DATABASE__ITEM__GET_SPRITE_FROM_VALUE,
                                        //
                                        data.Basic_VarType, data.Basic_VarClass
                                    );
                            }
                            break;
                        default:
                            {
                                sprite
                                    = (Sprite)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                                        Giggle_ScriptBridge.EVENT.DATABASE__ITEM__GET_SPRITE_FROM_VALUE,
                                        //
                                        data.Basic_VarType
                                    );
                            }
                            break;
                    }
                    Basic_list[whileCount].transform.Find("Portrait").GetComponent<Image>().sprite = sprite;
                    Basic_list[whileCount].transform.Find("Name").GetComponent<TextMeshProUGUI>().text = data.Basic_VarName;

                    // 커버 여부
                    Giggle_Item.Inventory inventoryData
                        = (Giggle_Item.Inventory)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                            Giggle_ScriptBridge.EVENT.PLAYER__ITEM__GET_ITEM_FROM_INVENTORY_ID,
                            data.Basic_VarId);

                    Basic_list[whileCount].transform.Find("Cover").gameObject.SetActive(inventoryData == null);
                    
                    finalCount = whileCount;
                }
                else
                {
                    Basic_list[whileCount].gameObject.SetActive(false);
                }

                //
                whileCount++;
            }

            Basic_content.sizeDelta
                = new Vector2(
                    0,
                    (Basic_list[finalCount].GetComponent<RectTransform>().sizeDelta.y * 0.5f) - Basic_list[finalCount].localPosition.y);

            //Basic_CheckCover();
        }

        //
        //public void Basic_CheckCover()
        //{
        //    List<int>   formation       = (List<int>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.PLAYER__FORMATION__GET_SELECT_FORMATION);
        //    List<int>   characterList   = Basic_uiData.Formation_VarMarionetteList;
        //
        //    int whileCount = 0;
        //    while(whileCount < characterList.Count)
        //    {
        //        //
        //        Basic_list[whileCount].Find("Cover").gameObject.SetActive(false);
        //        for(int for0 = 0; for0 < formation.Count; for0++)
        //        {
        //            if( characterList[whileCount].Equals(formation[for0]))
        //            {
        //                Basic_list[whileCount].Find("Cover").gameObject.SetActive(true);
        //                break;
        //            }
        //        }
        //
        //        //
        //        whileCount++;
        //    }
        //}

        //
        public void Basic_Init(Giggle_MainManager__BlackSmith _uiData)
        {
            Basic_Init();
            Basic_uiData = _uiData;
        }

        ////////// Constructor & Destroyer  //////////

    }

    [Header("WEAPON ==========")]
    
    // Area3
    //[SerializeField] ScrollerView_MenuBar__Weapon   Weapon_scrollViewMenuBar;
    [SerializeField] ScrollView_Weapon              Weapon_scrollView;

    [SerializeField] Transform  Weapon_crafting;

    [Header("RUNNING")]
    [SerializeField] List<int>  Weapon_list;    // dataId
    [SerializeField] int        Weapon_select;

    ////////// Getter & Setter  //////////
    
    public List<int>    Weapon_VarList  { get { return Weapon_list; }   }

    ////////// Method           //////////

    //
    void Weapon_BtnScrollView(int _count)
    {
        Weapon_select = _count;

        //
        Giggle_Item.List data
            = (Giggle_Item.List)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.DATABASE__ITEM__GET_DATA_FROM_ID,
                Weapon_list[Weapon_select]);

        Sprite sprite = null;
        switch(data.Basic_VarType)
        {
            case Giggle_Item.TYPE.ACCESSORY:
                {
                    sprite
                        = (Sprite)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                            Giggle_ScriptBridge.EVENT.DATABASE__ITEM__GET_SPRITE_FROM_VALUE,
                            //
                            data.Basic_VarType, data.Basic_VarClass
                        );
                }
                break;
            default:
                {
                    sprite
                        = (Sprite)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                            Giggle_ScriptBridge.EVENT.DATABASE__ITEM__GET_SPRITE_FROM_VALUE,
                            //
                            data.Basic_VarType
                        );
                }
                break;
        }

        // Area2
        Transform element0 = Weapon_crafting.Find("Area2");
        Transform element1 = element0.Find("Info");

        element1.Find("Portrait"        ).GetComponent<Image>().sprite = sprite;
        element1.Find("Type_Text (TMP)" ).GetComponent<TextMeshProUGUI>().text = data.Basic_VarType.ToString();
        element1.Find("Name_Text (TMP)" ).GetComponent<TextMeshProUGUI>().text = data.Basic_VarName;
        element1.Find("Lv_Text (TMP)"   ).GetComponent<TextMeshProUGUI>().text = "1";

        // Area3
        element0 = Weapon_crafting.Find("Area3");
        element1 = element0.Find("Recipe");

        string text
            = data.Basic_VarRecipe + "\n"
            + "0" + "/" + "1";

        element1.GetChild(0).GetComponent<TextMeshProUGUI>().text = text;

        for(int for0 = 0; for0 < data.Basic_VarMattersCount; for0++)
        {
            Giggle_Item.List.Matter matter = data.Basic_GetMatterFromCount(for0);

            text
                = matter.Basic_VarId + "\n"
                + "0" + "/" + matter.Basic_VarCount;

            element1 = element0.Find("Mat" + (for0 / 5));
            element1.GetChild(for0 % 5).GetChild(0).GetComponent<TextMeshProUGUI>().text = text;
        }

        //
        Weapon_crafting.gameObject.SetActive(true);
    }

    //
    void Weapon_BtnCraftingBack()
    {
        Weapon_crafting.gameObject.SetActive(false);
    }

    //
    void Weapon_BtnCraftingDoing()
    {
        Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
            Giggle_ScriptBridge.EVENT.PLAYER__ITEM__LOOTING,
            Weapon_list[Weapon_select], 1);
    }

    //
    void Weapon_Active()
    {
        //
        Weapon_Reset();

        Weapon_scrollView.Basic_SelectMenuBar(-1);
    }

    //
    void Weapon_Reset()
    {
        //
        Weapon_list.Clear();

        Giggle_Database.Item_Data datas
            = (Giggle_Database.Item_Data)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.DATABASE__ITEM__GET_DATAS_FROM_TYPE,
                //
                (int)Giggle_Item.TYPE.WEAPON);

        for(int for0 = 0; for0 < datas.Basic_VarCount; for0++)
        {
            Weapon_list.Add(datas.Basic_GetDataFromCount(for0).Basic_VarId);
        }

        //
    }

    ////////// Unity            //////////

    void Weapon_Start()
    {
        Weapon_BtnCraftingBack();

        Weapon_scrollView.Basic_Init(this);
    }

    #endregion
}
