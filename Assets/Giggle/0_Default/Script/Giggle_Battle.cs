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
        STAGE_SETTING,

        ACTIVE
    }
    [SerializeField] Basic__COROUTINE_PHASE Basic_coroutinePhase;

    IEnumerator Basic_coroutine;

    ////////// Getter & Setter          //////////
    public Basic__COROUTINE_PHASE   Basic_VarCoroutinePhase { get { return Basic_coroutinePhase;    } set{ Basic_coroutinePhase = value;    }   }

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
                case Basic__COROUTINE_PHASE.STAGE_SETTING:      { Basic_Coroutine__STAGE_SETTING();     }   break;
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

    // STAGE_DATA_CHECK
    void Basic_Coroutine__STAGE_DATA_CHECK()
    {
        object isOnObj
            = Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.DATABASE__CHARACTER__GET_IS_OPEN);
        
        if(isOnObj != null)
        {
            if((bool)isOnObj)
            {
                if(Basic_Coroutine__STAGE_DATA_CHECK__Player())
                {
                    //
                    object dataObj1
                        = Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                            Giggle_ScriptBridge.EVENT.DATABASE__CHARACTER__GET_DATA_FROM_ID,
                            11002011);
                    Giggle_Character.Database data1 = (Giggle_Character.Database)dataObj1;

                    if((bool)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.DATABASE__CHARACTER__GET_IS_OPEN))
                    {
                        //
                        Basic_coroutinePhase = Basic__COROUTINE_PHASE.STAGE_SETTING;
                    }
                }
            }
        }
    }

    bool Basic_Coroutine__STAGE_DATA_CHECK__Player()
    {
        bool res = true;

        //
        int formationSelect
            = (int)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.PLAYER__FORMATION__GET_SELECT);

        List<Giggle_Player.Formation> formationList
            = (List<Giggle_Player.Formation>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.PLAYER__FORMATION__GET_FORMATION_LIST);

        List<Giggle_Character.Save> marionetteList
                = (List<Giggle_Character.Save>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.PLAYER__MARIONETTE__GET_LIST);

        //
        int whileCount = 0;
        while(whileCount < formationList[formationSelect].Basic_VarFormation.Count)
        {
            if(!formationList[formationSelect].Basic_VarFormation[whileCount].Equals(-1))
            {
                Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.DATABASE__CHARACTER__GET_DATA_FROM_ID,
                    marionetteList[formationList[formationSelect].Basic_VarFormation[whileCount]].Basic_VarDataId);
            }

            whileCount++;
        }

        res = (bool)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.DATABASE__CHARACTER__GET_IS_OPEN);

        //
        return res;
    }

    //
    void Basic_Coroutine__STAGE_SETTING()
    {
        //
        int formationSelect
            = (int)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.PLAYER__FORMATION__GET_SELECT);

        List<Giggle_Player.Formation> formationList
            = (List<Giggle_Player.Formation>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.PLAYER__FORMATION__GET_FORMATION_LIST);

        List<Giggle_Character.Save> marionetteList
                = (List<Giggle_Character.Save>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.PLAYER__MARIONETTE__GET_LIST);

        
        // 캐릭터 생성
        Giggle_Character.Database data = null;
        Giggle_Unit unit = null;

        // player
        int whileCount = 0;
        while(whileCount < formationList[formationSelect].Basic_VarFormation.Count)
        {
            // 기존 데이터 날리기
            while(Formation_Ally.Basic_GetTile(whileCount).childCount > 0)
            {
                Formation_Ally.Basic_GetTile(whileCount).GetChild(0).GetComponent<Giggle_Unit>().Basic_Release();

                Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.MASTER__GARBAGE__REMOVE,
                    //
                    Formation_Ally.Basic_GetTile(whileCount).GetChild(0));
            }

            //
            if(!formationList[formationSelect].Basic_VarFormation[whileCount].Equals(-1))
            {
                data = (Giggle_Character.Database)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.DATABASE__CHARACTER__GET_DATA_FROM_ID,
                    marionetteList[formationList[formationSelect].Basic_VarFormation[whileCount]].Basic_VarDataId);

                unit = GameObject.Instantiate(data.Basic_VarUnit, Formation_Ally.Basic_GetTile(whileCount));
                unit.Basic_Init(this, data);
            }

            whileCount++;
        }

        // enemy
        // 기존 데이터 날리기
        while(Formation_Enemy.Basic_GetTile(0).childCount > 0)
        {
            Formation_Enemy.Basic_GetTile(0).GetChild(0).GetComponent<Giggle_Unit>().Basic_Release();
            
            Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.MASTER__GARBAGE__REMOVE,
                //
                Formation_Enemy.Basic_GetTile(0).GetChild(0));
        }

        data = (Giggle_Character.Database)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
            Giggle_ScriptBridge.EVENT.DATABASE__CHARACTER__GET_DATA_FROM_ID,
            11002011);

        unit = GameObject.Instantiate(data.Basic_VarUnit, Formation_Enemy.Basic_GetTile(0));
        unit.Basic_Init(this, data);

        // 총알 리셋
        Bullet_Reset();

        //
        Basic_coroutinePhase = Basic__COROUTINE_PHASE.ACTIVE;
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
        [SerializeField] int         Basic_damage;

        [SerializeField] Vector3 Basic_startPos;
        [SerializeField] Vector3 Basic_destinationPos;
        [SerializeField] float Basic_timer;
        [SerializeField] float Basic_time;

        ////////// Getter & Setter          //////////
        public GameObject Basic_VarObj  { get{ return Basic_obj;    }   }

        public bool Basic_IsReady   { get{ return !Basic_obj.activeSelf;    }   }

        ////////// Method                   //////////
        public void Basic_Launch(
            Giggle_Unit _owner, Giggle_Unit _target,
            int _damage)
        {
            Basic_owner  = _owner;
            Basic_target = _target;
            Basic_damage = _damage;

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
                    if(Basic_target.gameObject.activeInHierarchy)
                    {
                        //Debug.Log(Basic_owner.transform.parent.parent.name + " 공격 성공 " + Basic_damage);
                    }

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

    public void Bullet_Launch(
        Giggle_Unit _owner, Giggle_Unit _target,
        int _damage)
    {
        bool isLaunch = false;

        int whileCount = 0;
        while(whileCount < Bullet_bullets.Count)
        {
            if(Bullet_bullets[whileCount].Basic_IsReady)
            {
                Bullet_bullets[whileCount].Basic_Launch(
                    _owner, _target,
                    _damage);

                isLaunch = true;
                break;
            }

            whileCount++;
        }

        // 총알이 부족해서 발사를 못했다면 생성하자.
        if(!isLaunch)
        {
            whileCount = 0;
            while(whileCount < 100)
            {
                Bullet_bullets.Add(new Bullet(Bullet_bullets[0].Basic_VarObj));
                whileCount++;
            }

            Bullet_Launch(
                _owner, _target,
                _damage);
        }
    }

    void Bullet_Reset()
    {
        int whileCount = 0;
        while(whileCount < Bullet_bullets.Count)
        {
            Bullet_bullets[whileCount].Basic_VarObj.SetActive(false);

            whileCount++;
        }
    }

    ////////// Constructor & Destroyer  //////////

    #endregion
}
