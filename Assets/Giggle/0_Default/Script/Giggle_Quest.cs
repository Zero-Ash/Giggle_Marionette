using UnityEngine;

//
using System;
using System.Collections.Generic;

namespace Giggle_Quest
{
    public enum TYPE
    {
        DAILY = 1,
        WEEKLY,
        FRIENDSHIP
    }

    public enum completeCondition_TYPE
    {
        EPISODE_ALL_CLEAR,
        EPISODE_ALL_3_STAR,
        STAGE_CLEAR_COUNT,
        MONSTER_SLAIN,
        BOSS_SLAIN,
        WEEKLY_BOSS_SLAIN,
        GUILD_CHECK,
        FORMATION_SETTING
    }

    [Serializable]
    public class Database : IDisposable
    {
        [SerializeField] int m_id;

        // openCondition
        public enum openCondition_TYPE
        {
            NONE,

            PRE_QUEST
        }

        [SerializeField] openCondition_TYPE m_openConditionType;
        [SerializeField] string             m_openConditionValue;

        // completeCondition
        [SerializeField] completeCondition_TYPE m_completeConditionType;
        [SerializeField] List<int>              m_completeConditionValues;

        // reward
        [SerializeField] List<Database_Reward>  m_rewards;

        ////////// Getter & Setter          //////////
        
        // m_id
        public int  Basic_VarId                 { get { return m_id;    }   }

        public bool Basic_GetIdIsSame(int _id)  { return m_id.Equals(_id);  }

        // completeCondition
        public completeCondition_TYPE   Basic_VarCompleteConditionType              { get { return m_completeConditionType; }   }
        public int                      Basic_GetCompleteConditionValue(int _count) { return m_completeConditionValues[_count]; }

        // reward
        public Database_Reward  Basic_GetReward(int _count) { return m_rewards[_count]; }

        ////////// Method                   //////////

        ////////// Constructor & Destroyer  //////////

        public Database()
        {
            Database_Prepare();

            m_id = -1;
        }

        public Database(Dictionary<string, string> _data)
        {
            Database_Prepare();

            m_id = int.Parse(_data["quest_id"]);

            // openCondition
            m_openConditionType     = (openCondition_TYPE)Enum.Parse(typeof(openCondition_TYPE), _data["questOpenCondition"]);
            m_openConditionValue    = _data["questOpenConditionValue"];

            // completeCondition
            m_completeConditionType = (completeCondition_TYPE)Enum.Parse(typeof(completeCondition_TYPE), _data["questCompletionCondition"]);
            for(int for0 = 1; for0 <= 3; for0++)
            {
                int element = int.Parse(_data["questCompletionConditionValue" + for0]);
                if(element != 0)
                {
                    m_completeConditionValues.Add(0);
                    m_completeConditionValues[for0 - 1] = element;
                }
            }
        }

        void Database_Prepare()
        {
            // openCondition

            // completeCondition
            m_completeConditionValues = new List<int>();
        }

        //
        public void Dispose()
        {
            
        }
    }

    [Serializable]
    public class Database_Reward : IDisposable
    {
        public enum TYPE
        {
            DIA,
            ITEM,
            FRIENDSHIP
        }

        [SerializeField] TYPE   m_rewardType;
        [SerializeField] int    m_rewardId;
        [SerializeField] int    m_rewardValue;

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////

        ////////// Constructor & Destroyer  //////////

        public Database_Reward()
        {

        }

        public void Dispose()
        {
            
        }
    }

    [Serializable]
    public class Text : IDisposable
    {
        [SerializeField] completeCondition_TYPE m_type;

        [SerializeField] string m_title;
        [SerializeField] string m_description;

        ////////// Getter & Setter          //////////
        public completeCondition_TYPE   Basic_VarType   { get { return m_type;  }   }

        ////////// Method                   //////////
        public string   Basic_PrintTitle(Database _data)
        {
            string res = m_title.Replace("{0}", _data.Basic_GetCompleteConditionValue(0).ToString());

            int whileCount = 1;
            while(true)
            {
                if(!res.Contains("{" + whileCount + "}"))
                {
                    break;
                }

                res = res.Replace("{" + whileCount + "}", _data.Basic_GetCompleteConditionValue(whileCount).ToString());
            }

            //
            return res;
        }

        public string   Basic_PrintDescription(Database _data)
        {
            string res = m_description.Replace("{0}", _data.Basic_GetCompleteConditionValue(0).ToString());

            int whileCount = 1;
            while(true)
            {
                if(!res.Contains("{" + whileCount + "}"))
                {
                    break;
                }

                res = res.Replace("{" + whileCount + "}", _data.Basic_GetCompleteConditionValue(whileCount).ToString());
            }

            //
            return res;
        }

        ////////// Constructor & Destroyer  //////////

        public Text(Dictionary<string, string> _data)
        {
            m_type  = (completeCondition_TYPE)Enum.Parse(typeof(completeCondition_TYPE), _data["questCompletionCondition"]);

            m_title         = _data["questTitle"];
            m_description   = _data["questDescription"];
        }

        public void Dispose()
        {
            
        }
    }

    // 플레이어 진행저장
    [Serializable]
    public class Save : IDisposable
    {
        [SerializeField] int m_id;  // 퀘스트의 id

        [SerializeField] int m_count;   // -1이면 클리어

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////

        ////////// Constructor & Destroyer  //////////

        public Save()
        {

        }

        public void Dispose()
        {
            
        }
    }
}
