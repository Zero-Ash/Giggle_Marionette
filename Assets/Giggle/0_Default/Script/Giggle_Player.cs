using UnityEngine;

//
using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class Giggle_Player : IDisposable
{
    #region BASIC

    ////////// Getter & Setter          //////////

    ////////// Method                   //////////
    public void Basic_Init()
    {
        Pinocchio_Contructor();
        Marionette_Contructor();
        Formation_Contructor();
        Item_Contructor();
        Power_Contructor();
    }

    ////////// Constructor & Destroyer  //////////
    public Giggle_Player()
    {
    }

    public void Dispose()
    {

    }

    #endregion

    #region PINOCCHIO

    public enum Pinocchio_GENDER
    {
        MALE = 1,
        FEMALE
    }

    [Header("PINOCCHIO ==================================================")]
    [SerializeField] Giggle_Character.Save  Pinocchio_data;
    [SerializeField] Pinocchio_GENDER       Pinocchio_gender;
    [SerializeField] List<int>              Pinocchio_jobs;

    ////////// Getter & Setter          //////////
    object Pinocchio_VarData(params object[] _args)
    {
        return Pinocchio_data;
    }

    object Pinocchio_VarJobs(params object[] _args)
    {
        List<int> res = new List<int>();

        for(int for0 = 0; for0 < Pinocchio_jobs.Count; for0++)
        {
            res.Add(Pinocchio_jobs[for0]);
        }

        return res;
    }

    ////////// Method                   //////////

    ////////// Constructor & Destroyer  //////////
    void Pinocchio_Contructor()
    {
        Pinocchio_data = new Giggle_Character.Save(0,1111);
        Pinocchio_gender = Pinocchio_GENDER.MALE;
        if(Pinocchio_jobs == null)
        {
            Pinocchio_jobs = new List<int>();
        }
        Pinocchio_jobs.Add(1111);
        Pinocchio_jobs.Add(1121);
        Pinocchio_jobs.Add(1131);

        //
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__VAR_DATA,    Pinocchio_VarData   );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__VAR_JOBS,    Pinocchio_VarJobs   );
    }

    #endregion

    #region MARIONETTE
    
    [Header("MARIONETTE ==================================================")]
    [SerializeField] List<Giggle_Character.Save> Marionette_list;

    ////////// Getter & Setter          //////////
    object Marionette_GetList(params object[] _args)
    {
        return Marionette_list;
    }

    ////////// Method                   //////////
    
    // Marionette_Add
    object Marionette_Add__Script(params object[] _args)
    {
        int dataId = (int)_args[0];

        //
        Marionette_Add(dataId);

        //
        return true;
    }

    void Marionette_Add(int _dataId)
    {
        //
        int inventoryId = 0;
        for(int for0 = 0; for0 < Marionette_list.Count; for0++)
        {
            int forId = Marionette_list[for0].Basic_VarInventoryId;
            if(inventoryId <= forId)
            {
                inventoryId = forId + 1;
            }
        }
        Giggle_Character.Save element = new Giggle_Character.Save(inventoryId, _dataId);
        Marionette_list.Add(element);
    }

    ////////// Constructor & Destroyer  //////////
    void Marionette_Contructor()
    {
        if(Marionette_list == null)
        {
            Marionette_list = new List<Giggle_Character.Save>();
        }

        Marionette_Add(11001011);

        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__MARIONETTE__GET_LIST,   Marionette_GetList  );

        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__MARIONETTE__ADD,    Marionette_Add__Script  );
    }

    #endregion

    #region FORMATION

    [Serializable]
    public class Formation
    {
        // 인벤토리 id
        [SerializeField] List<int> Basic_formation;

        ////////// Getter & Setter          //////////
        public List<int>    Basic_VarFormation  { get{ return Basic_formation;  }   }

        ////////// Method                   //////////

        ////////// Constructor & Destroyer  //////////
        public Formation()
        {
            if(Basic_formation == null)
            {
                Basic_formation = new List<int>();
            }

            while(Basic_formation.Count < 9)
            {
                Basic_formation.Add(-1);
            }
            Basic_formation[4] = 0;
        }
    }
    
    [Header("FORMATION ==================================================")]
    [SerializeField] int                Formation_select;
    [SerializeField] List<Formation>    Formation_list;

    ////////// Getter & Setter          //////////
    // Formation_select
    object Formation_GetSelect(params object[] _args)   { return Formation_select;  }

    // Formation_list
    object Formation_GetFormationList(params object[] _args)    { return Formation_list;    }

    object Formation_GetFormationFromCount(params object[] _args)
    {
        int count = (int)_args[0];

        //
        return Formation_list[count].Basic_VarFormation;
    }

    ////////// Method                   //////////
    
    ////////// Constructor & Destroyer  //////////
    void Formation_Contructor()
    {
        if(Formation_list == null)
        {
            Formation_list = new List<Formation>();
        }

        while(Formation_list.Count < 3)
        {
            Formation_list.Add(new Formation());
        }

        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__FORMATION__GET_SELECT,                  Formation_GetSelect             );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__FORMATION__GET_FORMATION_LIST,          Formation_GetFormationList      );
        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__FORMATION__GET_FORMATION_FROM_COUNT,    Formation_GetFormationFromCount );
    }

    #endregion

    #region ITEM
    [SerializeField] List<Giggle_Item.Inventory>    Item_list;

    ////////// Getter & Setter          //////////

    // Item_list
    object Item_GetItemList(params object[] _args)  { return Item_list; }

    ////////// Method                   //////////
    
    ////////// Constructor & Destroyer  //////////
    void Item_Contructor()
    {
        if(Item_list == null)
        {
            Item_list = new List<Giggle_Item.Inventory>();
        }

        Giggle_ScriptBridge.Basic_VarInstance.Basic_SetMethod(Giggle_ScriptBridge.EVENT.PLAYER__ITEM__GET_ITEM_LIST,    Item_GetItemList    );
    }

    #endregion

    #region POWER

    [Header("POWER ==================================================")]
    [SerializeField] List<int> Power_values;

    ////////// Getter & Setter          //////////

    ////////// Method                   //////////

    public void Power_Add(int _pow)
    {
        Power_values[0] += _pow;

        for(int for0 = 0; for0 < Power_values.Count - 1; for0++)
        {
            if (Power_values[for0] > 1000)
            {
                Power_values[for0 + 1] += Power_values[for0] / 1000;
                Power_values[for0] %= 1000;
            }
            else
            {
                break;
            }
        }
    }

    ////////// Constructor & Destroyer  //////////
    void Power_Contructor()
    {
        if(Power_values == null)
        {
            Power_values = new List<int>();
        }

        while(Power_values.Count < 5)
        {
            Power_values.Add(0);
        }
    }

    #endregion
}
