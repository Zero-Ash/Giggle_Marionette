using UnityEngine;

//
using System;
using System.Collections.Generic;

namespace Giggle_Item
{

    #region EQUIPMENT

    public enum TYPE
    {
        WEAPON = 0,
        HELMET,
        ARMOR,
        BRACE,
        ACCESSORY,

        NONE
    }

    [Serializable]
    public class List : IDisposable
    {
        public class Matter : IDisposable
        {
            [SerializeField] int    Basic_id;
            [SerializeField] int    Basic_count;

            ////////// Getter & Setter          //////////
            
            ////////// Method                   //////////

            ////////// Constructor & Destroyer  //////////
            
            public Matter(Dictionary<string, string> _data, int _count)
            {
                Basic_id    = int.Parse(   _data["matter" + _count]         );
                Basic_count = int.Parse(   _data["matter_value" + _count]   );
            }

            public void Dispose()
            {

            }
        }

        [SerializeField] int    Basic_id;

        [SerializeField] string Basic_name;

        [SerializeField] Giggle_Master.ATTRIBUTE    Basic_attribute;
        [SerializeField] int                        Basic_class;
        [SerializeField] int                        Basic_skillClass;

        [SerializeField] int            Basic_recipe;
        [SerializeField] List<Matter>   Basic_matters;

        ////////// Getter & Setter          //////////

        // Basic_id
        public int  Basic_VarId                 { get { return Basic_id;    }   }

        public bool Basic_GetIdIsSame(int _id)  { return Basic_id.Equals(_id);  }

        public TYPE Basic_VarType
        {
            get
            {
                TYPE res = TYPE.NONE;

                //
                double idLength = Math.Floor(Math.Log10(Basic_id));
                int itemType = (Basic_id / (int)Math.Pow(10, idLength - 1)) % 10;

                res = (TYPE)(itemType / 2);
                
                //
                return res;
            }
        }

        //
        public string   Basic_VarName   { get { return Basic_name;  }   }

        //
        public Giggle_Master.ATTRIBUTE  Basic_VarAttribute  { get { return Basic_attribute;     }   }
        //
        public int                      Basic_VarClass      { get { return Basic_class;         }   }
        //
        public int                      Basic_VarSkillClass { get { return Basic_skillClass;    }   }

        //
        public int  Basic_VarRecipe { get { return Basic_recipe;    }   }
        //
        
        ////////// Method                   //////////

        ////////// Constructor & Destroyer  //////////
        public List()
        {
            Basic_id = -1;
        }
        
        public List(Dictionary<string, string> _data)
        {
            Basic_id    = int.Parse(    _data["id"]    );

            Basic_name  =               _data["name"];

            Basic_attribute = Giggle_Master.ATTRIBUTE.NONE;
            if(_data.ContainsKey("attribute"))
            {
                Basic_attribute = (Giggle_Master.ATTRIBUTE)int.Parse(_data["attribute"]);
                //Basic_attribute     = (Giggle_Master.ATTRIBUTE)Enum.Parse(typeof(Giggle_Master.ATTRIBUTE), _data["attribute"]);
            }

            Basic_class = -1;
            if(_data.ContainsKey("class"))
            {
                Basic_class = int.Parse(    _data["class"]  );
            }

            Basic_skillClass    = int.Parse(    _data["skill_class"]    );

            //
            Basic_recipe    = int.Parse(    _data["recipe"] );

            if(Basic_matters == null)
            {
                Basic_matters = new List<Matter>();
            }
            for(int for0 = 1; for0 <= ((_data.Count - 6) / 2); for0++)
            {
                Basic_matters.Add(new Matter(_data, for0));
            }
        }
        
        public void Dispose()
        {

        }
    }

    [Serializable]
    public class Inventory : IDisposable
    {
        [SerializeField] int    Basic_inventoryId;
        [SerializeField] int    Basic_dataId;

        [SerializeField] int    Basic_level;
        [SerializeField] int    Basic_count;

        ////////// Getter & Setter          //////////
        public int  Basic_VarInventoryId    { get { return Basic_inventoryId;   }   }
        public int  Basic_VarDataId         { get { return Basic_dataId;        }   }

        public int  Basic_VarCount  { get { return Basic_count; }   }
        
        ////////// Method                   //////////
        public void Basic_AddCount(int _count)
        {
            Basic_count += _count;
        }

        ////////// Constructor & Destroyer  //////////
        public Inventory(int _inventoryId, int _dataId)
        {
            Basic_inventoryId   = _inventoryId;
            Basic_dataId        = _dataId;

            Basic_level = 1;
            Basic_count = 0;
        }

        public void Dispose()
        {

        }
    }

    #endregion

    #region RELIC

    public enum Relic_COLOR
    {
        BLACK = 1,
        WHITE,

        TOTAL
    }

    public enum Relic_CLASS
    {
        NORMAL = 1,
        RARE
    }

    [Serializable]
    public class Relic : IDisposable
    {
        [SerializeField] int    Basic_id;

        [SerializeField] string Basic_name;

        [SerializeField] Relic_COLOR    Basic_color;
        [SerializeField] Relic_CLASS    Basic_class;
        [SerializeField] int            Basic_index;

        [SerializeField] List<RelicLv>  Basic_lvs;

        ////////// Getter & Setter          //////////
        //
        public int  Basic_VarId { get { return Basic_id;    }   }

        public string   Basic_VarName   { get { return Basic_name;  }   }

        public Relic_COLOR  Basic_VarColor  { get { return Basic_color; }   }

        public RelicLv  Basic_GetLvDataFromLv(int _lv) { return Basic_lvs[_lv - 1]; }

        ////////// Method                   //////////
        public void Basic_SetLvList(Dictionary<string, string> _data)
        {
            if(Basic_lvs == null)
            {
                Basic_lvs = new List<RelicLv>();
            }

            Basic_lvs.Add(new RelicLv(_data));
        }

        ////////// Constructor & Destroyer  //////////
        public Relic()
        {
            Basic_id = -1;
        }

        public Relic(Dictionary<string, string> _data)
        {
            Basic_id = int.Parse(_data["relic_id"]);

            Basic_name = _data["cha_relic_name"];

            Basic_color = (Relic_COLOR) int.Parse(_data["cha_relic_color"]);
            Basic_class = (Relic_CLASS) int.Parse(_data["cha_relic_class"]);
            Basic_index =               int.Parse(_data["cha_relic_index"]);
        }
        
        //
        public void Dispose()
        {

        }
    
    }

    [Serializable]
    public class RelicLv : IDisposable
    {
        public enum ABILITY
        {
            ATTACK_PER,
            DEFENCE_PER
        }

        public class Material : IDisposable
        {
            [SerializeField] int    Basic_id;
            [SerializeField] int    Basic_value;

            ////////// Getter & Setter          //////////

            ////////// Method                   //////////

            ////////// Constructor & Destroyer  //////////
            
            public Material(Dictionary<string, string> _data, int _count)
            {
                string value = (_count / 10).ToString() + (_count % 10).ToString();
                Basic_id    = int.Parse(_data["relic_LvMaterials_" + value]);
                Basic_value = int.Parse(_data["Materials_Value_"   + value]);
            }

            public void Dispose()
            {

            }
        }

        [SerializeField] int    Basic_id;

        [SerializeField] List<Material> Basic_materials;

        [SerializeField] ABILITY    Basic_ability;
        [SerializeField] int        Basic_abilityValue;

        ////////// Getter & Setter          //////////
        //
        public int  Basic_VarId { get { return Basic_id;    }   }

        //
        public ABILITY  Basic_VarAbility        { get { return Basic_ability;       }   }

        public int      Basic_VarAbilityValue   { get { return Basic_abilityValue;  }   }

        ////////// Method                   //////////

        ////////// Constructor & Destroyer  //////////
        public RelicLv(Dictionary<string, string> _data)
        {
            Basic_id = int.Parse(_data["relicLv_id"]);

            if(Basic_materials == null)
            {
                Basic_materials = new List<Material>();
            }
            Basic_materials.Add(new Material(_data, 1));
            Basic_materials.Add(new Material(_data, 2));

            Basic_ability       = (ABILITY)Enum.Parse(typeof(ABILITY), _data["ability"]);
            Basic_abilityValue  = int.Parse(_data["value_01"]);
        }
        
        public void Dispose()
        {

        }
    
    }

    #endregion

    #region CONSTELLATION

    [Serializable]
    public class Constellation : IDisposable
    {
        [SerializeField] int    Basic_id;

        [SerializeField] Transform  Basic_background;

        [SerializeField] string                     Basic_name;
        [SerializeField] List<Constellation_Value>  Basic_values;

        ////////// Getter & Setter          //////////
        public int  Basic_VarId { get { return Basic_id;    }   }

        public Transform    Basic_VarBackground { get { return Basic_background;    }   }

        public string   Basic_VarName   { get { return Basic_name;  }   }

        public Constellation_Value  Basic_GetValueData(int _count)  { return Basic_values[_count];  }
        public int                  Basic_VarValueCount             { get { return Basic_values.Count;  }   }

        ////////// Method                   //////////
        
        // Basic_background
        public void Basic_SettingBackground(Transform  _background)
        {
            Basic_background = _background;
        }

        // Constellation_Value
        public bool Basic_SettingValue(Dictionary<string, string> _data)
        {
            bool res = false;

            //
            int id = int.Parse(_data["constellation_id"]);
            for(int for0 = 0; for0 < Basic_values.Count; for0++)
            {
                if(Basic_values[for0].Basic_VarId.Equals(id))
                {
                    Basic_values[for0].Basic_SettingName(_data);
                    res = true;
                    break;
                }
            }

            //
            return res;
        }

        public bool Basic_SettingLv(Dictionary<string, string> _data)
        {
            bool res = false;

            //
            int id = int.Parse(_data["constellation_id"]);
            for(int for0 = 0; for0 < Basic_values.Count; for0++)
            {
                if(Basic_values[for0].Basic_VarId.Equals(id))
                {
                    Basic_values[for0].Basic_SettingLv(_data);
                    res = true;
                    break;
                }
            }

            //
            return res;
        }

        ////////// Constructor & Destroyer  //////////
        public Constellation(Dictionary<string, string> _data)
        {
            Basic_id = int.Parse(_data["constellation_b_id"]);
            
            //
            Basic_name = _data["constellation_Bname"];
            //
            if(Basic_values == null)
            {
                Basic_values = new List<Constellation_Value>();
            }
            for(int for0 = 1; for0 <= 12; for0++)
            {
                Basic_values.Add(
                    new Constellation_Value(
                        int.Parse(
                            _data[
                                "constellation_value_" +
                                (for0 / 10).ToString() +
                                (for0 % 10).ToString()])));
            }
        }

        //
        public void Dispose()
        {

        }
    }

    [Serializable]
    public class Constellation_Value : IDisposable
    {
        [SerializeField] int    Basic_id;

        [SerializeField] string Basic_name;

        [SerializeField] List<Constellation_ValueLv> Basic_lvs;


        ////////// Getter & Setter          //////////
        public int  Basic_VarId { get { return Basic_id;    }   }

        public string   Basic_VarName   { get { return Basic_name;  }   }

        ////////// Method                   //////////
        public void Basic_SettingName(Dictionary<string, string> _data)
        {
            Basic_name = _data["constellation_name"];
        }

        public void Basic_SettingLv(Dictionary<string, string> _data)
        {
            int level = int.Parse(_data["constellation_lv"]);
            while(Basic_lvs.Count < level)
            {
                Basic_lvs.Add(null);
            }
            Basic_lvs[level - 1] = new Constellation_ValueLv(_data);
        }

        ////////// Constructor & Destroyer  //////////
        public Constellation_Value(int _id)
        {
            Basic_id = _id;

            if(Basic_lvs == null)
            {
                Basic_lvs = new List<Constellation_ValueLv>();
            }
        }

        //
        public void Dispose()
        {

        }
    }

    [Serializable]
    public class Constellation_ValueLv : IDisposable
    {
        [Serializable]
        public class Material
        {
            [SerializeField] int    Basic_id;
            [SerializeField] int    Basic_value;
            
            ////////// Getter & Setter          //////////

            ////////// Method                   //////////

            ////////// Constructor & Destroyer  //////////
            public Material(int _id, int _value)
            {
                Basic_id    = _id;
                Basic_value = _value;
            }
        }

        [SerializeField] int    Basic_id;

        [SerializeField] List<Material> Basic_LvMaterials;

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////

        ////////// Constructor & Destroyer  //////////
        public Constellation_ValueLv(Dictionary<string, string> _data)
        {
            //
            Basic_id = int.Parse(_data["constellationLv_id"]);
            //
            if(Basic_LvMaterials == null)
            {
                Basic_LvMaterials = new List<Material>();
            }
            for(int for0 = 1; for0 <= 2; for0++)
            {
                Basic_LvMaterials.Add(
                    new Material(
                        int.Parse(_data["constellation_LvMaterials_" + (for0 / 10).ToString() + (for0 % 10).ToString()]),
                        int.Parse(_data["Materials_Value_"           + (for0 / 10).ToString() + (for0 % 10).ToString()])));
            }

        }

        //
        public void Dispose()
        {

        }
    }

    #endregion

    #region CARD

    [Serializable]
    public class Card : IDisposable
    {
        [SerializeField] int    Basic_id;

        [SerializeField] string Basic_name;

        [SerializeField] int    Basic_class;
        [SerializeField] int    Basic_index;

        [SerializeField] List<CardLv>   Basic_lvs;

        ////////// Getter & Setter          //////////
        public int  Basic_VarId { get { return Basic_id;    }   }

        public string   Basic_VarName   { get { return Basic_name;  }   }

        ////////// Method                   //////////
        public void Basic_SettingLv(Dictionary<string, string> _data)
        {
            int level = int.Parse(_data["card_lv"]);

            while(Basic_lvs.Count < level)
            {
                Basic_lvs.Add(null);
            }
            Basic_lvs[level - 1] = new CardLv(_data);
        }

        ////////// Constructor & Destroyer  //////////
        public Card(Dictionary<string, string> _data)
        {
            Basic_id    = int.Parse(_data["card_id"]);

            Basic_name  = _data["cha_card_name"];

            Basic_class = int.Parse(_data["cha_card_class"]);
            Basic_index = int.Parse(_data["cha_card_index"]);

            if(Basic_lvs == null)
            {
                Basic_lvs = new List<CardLv>();
            }
        }

        //
        public void Dispose()
        {

        }
    }

    [Serializable]
    public class CardLv : IDisposable
    {
        [Serializable]
        public class Material
        {
            [SerializeField] int    Basic_id;
            [SerializeField] int    Basic_value;
            
            ////////// Getter & Setter          //////////

            ////////// Method                   //////////

            ////////// Constructor & Destroyer  //////////
            public Material(int _id, int _value)
            {
                Basic_id    = _id;
                Basic_value = _value;
            }
        }

        [SerializeField] int    Basic_id;

        [SerializeField] List<Material> Basic_mats;

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////

        ////////// Constructor & Destroyer  //////////
        public CardLv(Dictionary<string, string> _data)
        {
            Basic_id    = int.Parse(_data["cardLv_id"]);

            if(Basic_mats == null)
            {
                Basic_mats = new List<Material>();
            }
            for(int for0 = 1; for0 <= 2; for0++)
            {
                Basic_mats.Add(
                    new Material(
                        int.Parse(_data["card_LvMaterials_" + (for0 / 10).ToString() + (for0 % 10).ToString()]),
                        int.Parse(_data["Materials_Value_"  + (for0 / 10).ToString() + (for0 % 10).ToString()])));
            }
        }

        //
        public void Dispose()
        {

        }
    }

    [Serializable]
    public class CardSet : IDisposable
    {
        [SerializeField] int    Basic_id;

        [SerializeField] string Basic_name;

        [SerializeField] List<int>    Basic_cards;

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////

        ////////// Constructor & Destroyer  //////////
        public CardSet(Dictionary<string, string> _data)
        {
            Basic_id    = int.Parse(_data["card_set_id"]);

            Basic_name  = _data["card_set_name"];

            if(Basic_cards == null)
            {
                Basic_cards = new List<int>();
            }
            for(int for0 = 0; for0 <= 7; for0++)
            {
                Basic_cards.Add(
                    int.Parse(
                        _data["card_id_" + (for0 / 10).ToString() + (for0 % 10).ToString()]));
            }
        }

        //
        public void Dispose()
        {

        }
    }

    #endregion

}
