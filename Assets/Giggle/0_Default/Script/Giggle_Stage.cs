using UnityEngine;

//
using System;
using System.Collections.Generic;

namespace Giggle_Stage
{
    [Serializable]
    public class Group : IDisposable
    {
        [SerializeField] int    Basic_id;

        [SerializeField] List<Stage> Basic_stages;

        ////////// Getter & Setter          //////////
        
        // Basic_id
        public int  Basic_VarId { get { return Basic_id;    }   }

        // Basic_stages
        public Stage    Basic_GetStage(int _id)
        {
            Stage res = null;

            //
            for(int for0 = 0; for0 < Basic_stages.Count; for0++)
            {
                if(Basic_stages[for0].Basic_VarId.Equals(_id))
                {
                    res = Basic_stages[for0];
                    break;
                }
            }

            //
            return res;
        }

        ////////// Method                   //////////

        // Basic_stages
        public void Basic_AddStage(Dictionary<string, string> _data)
        {
            Basic_stages.Add(new Stage(_data));
        }

        ////////// Constructor & Destroyer  //////////
        
        //
        public Group(int _id)
        {
            Basic_id = _id;

            if(Basic_stages == null)
            {
                Basic_stages = new List<Stage>();
            }
            Basic_stages.Clear();
        }
        
        //
        public void Dispose()
        {

        }
    }

    [Serializable]
    public class Stage : IDisposable
    {
        [Serializable]
        public class Monster : IDisposable
        {
            [SerializeField] int    Basic_id;
            [SerializeField] int    Basic_lv;
            [SerializeField] int    Basic_formation;

            ////////// Getter & Setter          //////////
            public int  Basic_VarId { get { return Basic_id;    }   }

            public int  Basic_VarLv { get { return Basic_lv;    }   }

            public int  Basic_VarFormation  { get { return Basic_formation; }   }

            ////////// Method                   //////////

            ////////// Constructor & Destroyer  //////////
            public Monster(Dictionary<string, string> _data, int _count)
            {
                Basic_id    = int.Parse(_data["monster_id" + _count]);
                Basic_lv    = int.Parse(_data["monster_lv" + _count]);
                Basic_formation = _count;
            }
        
            //
            public void Dispose()
            {

            }
        }
        [SerializeField] int    Basic_id;

        [SerializeField] int    Basic_groupId;

        [SerializeField] string Basic_name;

        [SerializeField] bool   Basic_isMulti;  // 멀티 플레이 여부

        [SerializeField] int    Basic_minLevel; // 최소 레벨
        [SerializeField] int    Basic_maxLevel; // 최대 레벨

        [SerializeField] int    Basic_maxPlayer;    // 최대 플레이어 수

        [SerializeField] int    Basic_limitPlayCount;   // 제한 횟수. 0이면 무제한.

        [SerializeField] int    Basic_needStage;    // 개방을 위한 선행 스테이지.

        [SerializeField] bool   Basic_isBoss;   // 보스 스테이지 여부

        [SerializeField] int    Basic_clearExp;
        [SerializeField] int    Basic_clearGold;

        //[SerializeField] int    Basic_clearGold;
        //[SerializeField] int    Basic_clearGold;

        [SerializeField] List<Monster>    Basic_formation;

        ////////// Getter & Setter          //////////
        
        // Basic_id
        public int  Basic_VarId { get { return Basic_id;    }   }

        // Basic_formation
        public Monster  Basic_FormationGetData(int _count)  { return Basic_formation[_count];           }

        public int      Basic_FormationVarCount             { get { return Basic_formation.Count;   }   }

        ////////// Method                   //////////
        public void Basic_SettingFormation(Dictionary<string, string> _data)
        {
            if(Basic_formation == null) { Basic_formation = new List<Monster>();    }
            Basic_formation.Clear();

            for(int for0 = 0; for0 < 9; for0++)
            {
                if(!int.Parse(_data["monster_id" + for0]).Equals(0))
                {
                    Basic_formation.Add(new Monster(_data, for0));
                }
            }
        }

        ////////// Constructor & Destroyer  //////////
        
        //
        public Stage(Dictionary<string, string> _data)
        {
            Basic_id = int.Parse(_data["stage_id"]);

            Basic_name = _data["stagename"];

            Basic_isMulti = _data["ismulti"].Equals("T");
            
            Basic_minLevel = int.Parse(_data["limit_min_lv"]);
            Basic_maxLevel = int.Parse(_data["limit_max_lv"]);

            Basic_maxPlayer = int.Parse(_data["base_player_count"]);

            Basic_limitPlayCount = int.Parse(_data["limitplaycount"]);

            Basic_needStage = int.Parse(_data["need_stage_did"]);

            Basic_isBoss = _data["isboss"].Equals("T");

            Basic_clearExp  = int.Parse(_data["clearexp"]);
            Basic_clearGold = int.Parse(_data["cleargold"]);
        }
        
        //
        public void Dispose()
        {

        }
    }
}