using UnityEngine;
//
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class Giggle_Battle : IDisposable
{
    #region BASIC
    public enum Basic__COROUTINE_PHASE
    {
        INIT,
        STAGE_DATA_CHECK,

        ACTIVE
    }
    [SerializeField] Basic__COROUTINE_PHASE Basic_coroutinePhase;

    IEnumerator Basic_coroutine;

    ////////// Getter & Setter          //////////
    public Basic__COROUTINE_PHASE   Basic_VarCoroutinePhase { get{ return Basic_coroutinePhase; }   }

    ////////// Method                   //////////
    public void Basic_Init()
    {
        //
        Basic_coroutinePhase = Basic__COROUTINE_PHASE.INIT;
        Basic_coroutine = Basic_Coroutine();

        Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
            Giggle_ScriptBridge.EVENT.MASTER__BASIC__START_COROUTINE, Basic_coroutine);
    }

    // Basic_Coroutine
    IEnumerator Basic_Coroutine()
    {
        int phase = 0;

        float time = Time.time;
        float lastTime = Time.time;

        while(phase != -1)
        {
            time = Time.time;

            switch(Basic_coroutinePhase)
            {
                case Basic__COROUTINE_PHASE.INIT:               { Basic_Coroutine__INIT(ref phase);     }   break;
                case Basic__COROUTINE_PHASE.STAGE_DATA_CHECK:   { Basic_Coroutine__STAGE_DATA_CHECK();  }   break;
                case Basic__COROUTINE_PHASE.ACTIVE:
                    {
                        Bullet_Coroutine(time - lastTime);
                    }
                    break;
            }

            lastTime = time;

            yield return null;
        }
    }

    void Basic_Coroutine__INIT(ref int _phase)
    {
        switch(_phase)
        {
            case 0:
                {
                    if(Formation_CotoutineInit0())
                    {
                        _phase = 1;
                    }
                }
                break;
            case 1:
                {
                    if(Formation_CotoutineInit1())
                    {
                        _phase = 0;
                        Basic_coroutinePhase = Basic__COROUTINE_PHASE.STAGE_DATA_CHECK;
                    }
                }
                break;
        }
    }

    void Basic_Coroutine__STAGE_DATA_CHECK()
    {
        object isOnObj
            = Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.DATABASE__CHARACTER__GET_IS_OPEN);
        
        if(isOnObj != null)
        {
            if((bool)isOnObj)
            {
                object dataObj
                    = Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                        Giggle_ScriptBridge.EVENT.DATABASE__CHARACTER__GET_DATA,
                        11001011);
                Giggle_Character.Database data = (Giggle_Character.Database)dataObj;

                if(!data.Basic_VarId.Equals(-1))
                {
                    Giggle_Unit unit = GameObject.Instantiate(data.Basic_VarUnit, Formation_Ally.Basic_GetTile(0));
                    unit.Basic_Init(this, data);

                    unit = GameObject.Instantiate(data.Basic_VarUnit, Formation_Enemy.Basic_GetTile(0));
                    unit.Basic_Init(this, data);

                    //
                    Basic_coroutinePhase = Basic__COROUTINE_PHASE.ACTIVE;
                }
            }
        }
    }

    ////////// Constructor & Destroyer  //////////
    public Giggle_Battle()
    {
    }

    public void Dispose()
    {
        
    }

    #endregion

    #region FORMATION

    [Serializable]
    public class Formation : IDisposable
    {
        [SerializeField] Transform          Basic_parent;
        [SerializeField] List<Transform>    Basic_tiles;

        [Header("RUNNING")]
        [SerializeField] List<Giggle_Unit> Basic_units;

        ////////// Getter & Setter          //////////
        // Basic_tiles
        public Transform    Basic_GetTile(int _count)   { return Basic_tiles[_count];   }

        ////////// Method                   //////////
        public void Basic_Init()
        {
            for (int for0 = 0; for0 < Basic_parent.childCount; for0++)
            {
                Basic_tiles.Add(Basic_parent.Find(for0.ToString()));
            }
        }

        ////////// Constructor & Destroyer  //////////
        public void Dispose()
        {

        }
    }

    [Header("FORMATION ==================================================")]
    [SerializeField] Formation  Formation_Ally;
    [SerializeField] Formation  Formation_Enemy;

    ////////// Getter & Setter          //////////
    public Formation Formation_VarAlly  { get{ return Formation_Ally;   }   }

    public Formation Formation_VarEnemy { get{ return Formation_Enemy;  }   }

    ////////// Method                   //////////
    bool Formation_CotoutineInit0()
    {
        bool res = false;

        if(Formation_Ally != null)
        {
            Formation_Ally.Basic_Init();

            res = true;
        }

        return res;
    }

    bool Formation_CotoutineInit1()
    {
        bool res = false;

        if(Formation_Enemy != null)
        {
            Formation_Enemy.Basic_Init();

            res = true;
        }

        return res;
    }

    ////////// Constructor & Destroyer  //////////

    #endregion

    #region BULLET

    [Serializable]
    public class Bullet : IDisposable
    {
        [SerializeField] GameObject Basic_obj;

        [SerializeField] Giggle_Unit Basic_owner;
        [SerializeField] Giggle_Unit Basic_target;

        [SerializeField] Vector3 Basic_startPos;
        [SerializeField] Vector3 Basic_destinationPos;
        [SerializeField] float Basic_timer;
        [SerializeField] float Basic_time;

        ////////// Getter & Setter          //////////
        public GameObject Basic_VarObj  { get{ return Basic_obj;    }   }

        public bool Basic_IsReady   { get{ return !Basic_obj.activeSelf;    }   }

        ////////// Method                   //////////
        public void Basic_Launch(Giggle_Unit _owner, Giggle_Unit _target)
        {
            Basic_owner  = _owner;
            Basic_target = _target;

            Basic_startPos       = Basic_owner.transform.position;
            Basic_destinationPos = Basic_target.transform.position;

            Basic_timer = 0.0f;
            float distance = Vector3.Distance(Basic_startPos, Basic_destinationPos);
            Basic_time = distance / 10.0f;

            Basic_obj.transform.position = Basic_startPos;
            Basic_obj.SetActive(true);
        }

        public void Basic_Coroutine(float _deltaTime)
        {
            if(Basic_obj.activeSelf)
            {
                // 시간 진행
                Basic_timer += _deltaTime;
                if(Basic_timer >= Basic_time)
                {
                    Basic_timer = Basic_time;
                    
                    // TODO: 투사체들의 공격을 편집할 때는 이곳을 편집하세요.
                    Debug.Log(Basic_owner.transform.parent.parent.name + " 공격 성공");

                    // 비활성화
                    Basic_obj.SetActive(false);
                }

                Basic_obj.transform.position = Vector3.Lerp(Basic_startPos, Basic_destinationPos, Basic_timer / Basic_time);
            }
        }

        ////////// Constructor & Destroyer  //////////
        public Bullet()
        {

        }

        public Bullet(GameObject _obj)
        {
            Basic_obj = GameObject.Instantiate<GameObject>(_obj, _obj.transform.parent);
            Basic_obj.SetActive(false);
        }

        //
        public void Dispose()
        {

        }
    }

    [Header("BULLET ==================================================")]
    [SerializeField] Transform Bullet_parent;
    [SerializeField] List<Bullet> Bullet_bullets;

    ////////// Getter & Setter          //////////

    ////////// Method                   //////////
    void Bullet_Coroutine(float _deltaTime)
    {
        for(int for0 = 0; for0 < Bullet_bullets.Count; for0++)
        {
            Bullet_bullets[for0].Basic_Coroutine(_deltaTime);
        }
    }

    public void Bullet_Launch(Giggle_Unit _owner, Giggle_Unit _target)
    {
        bool isLaunch = false;

        int whileCount = 0;
        while(whileCount < Bullet_bullets.Count)
        {
            if(Bullet_bullets[whileCount].Basic_IsReady)
            {
                Bullet_bullets[whileCount].Basic_Launch(_owner, _target);

                isLaunch = true;
                break;
            }

            whileCount++;
        }

        if(!isLaunch)
        {
            whileCount = 0;
            while(whileCount < 100)
            {
                Bullet_bullets.Add(new Bullet(Bullet_bullets[0].Basic_VarObj));
                whileCount++;
            }

            Bullet_Launch(_owner, _target);
        }
    }

    ////////// Constructor & Destroyer  //////////

    #endregion
}
