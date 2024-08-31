using UnityEngine;
//
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[Serializable]
public class Giggle_Battle : IDisposable
{
    #region BASIC
    public enum Basic__COROUTINE_PHASE
    {
        INIT,

        //
        RESET,

        //
        SETTING_FADE_OUT_START,
        SETTING_FADE_OUT,

        //
        SETTING_STAGE_START,
        SETTING_STAGE_DATA_CHECK,
        SETTING_STAGE,

        //
        SETTING_PLAYER_START,
        SETTING_PLAYER_DATA_CHECK,
        SETTING_PLAYER,

        //
        SETTING_ENEMY_START,
        SETTING_ENEMY_DATA_CHECK,
        SETTING_ENEMY,

        //
        FADE_IN_START,
        FADE_IN,

        //
        SETTING_END,

        //
        ACTIVE_BATTLE,
        ACTIVE_MOVE_READY,
        ACTIVE_MOVE,
        ACTIVE_MOVE_END
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

                // SETTING
                case Basic__COROUTINE_PHASE.RESET:  { Basic_Coroutine__RESET(); }   break;

                case Basic__COROUTINE_PHASE.SETTING_FADE_OUT_START: { Basic_Coroutine__SETTING_FADE_OUT_START();            }   break;
                case Basic__COROUTINE_PHASE.SETTING_FADE_OUT:       { Basic_Coroutine__SETTING_FADE_OUT(time - lastTime);   }   break;

                case Basic__COROUTINE_PHASE.SETTING_STAGE_START:        { Basic_Coroutine__SETTING_STAGE_START();       }   break;
                case Basic__COROUTINE_PHASE.SETTING_STAGE_DATA_CHECK:   { Basic_Coroutine__SETTING_STAGE_DATA_CHECK();  }   break;
                case Basic__COROUTINE_PHASE.SETTING_STAGE:              { Basic_Coroutine__SETTING_STAGE();             }   break;

                case Basic__COROUTINE_PHASE.SETTING_PLAYER_START:       { Basic_Coroutine__SETTING_PLAYER_START();      }   break;
                case Basic__COROUTINE_PHASE.SETTING_PLAYER_DATA_CHECK:  { Basic_Coroutine__SETTING_PLAYER_DATA_CHECK(); }   break;
                case Basic__COROUTINE_PHASE.SETTING_PLAYER:             { Basic_Coroutine__SETTING_PLAYER();            }   break;

                case Basic__COROUTINE_PHASE.SETTING_ENEMY_START:        { Basic_Coroutine__SETTING_ENEMY_START();       }   break;
                case Basic__COROUTINE_PHASE.SETTING_ENEMY_DATA_CHECK:   { Basic_Coroutine__SETTING_ENEMY_DATA_CHECK();  }   break;
                case Basic__COROUTINE_PHASE.SETTING_ENEMY:              { Basic_Coroutine__SETTING_ENEMY();             }   break;

                case Basic__COROUTINE_PHASE.FADE_IN_START:  { Basic_Coroutine__FADE_IN_START();             }   break;
                case Basic__COROUTINE_PHASE.FADE_IN:        { Basic_Coroutine__FADE_IN(time - lastTime);    }   break;

                case Basic__COROUTINE_PHASE.SETTING_END:    { Basic_Coroutine__SETTING_END();   }   break;

                // ACTIVE
                case Basic__COROUTINE_PHASE.ACTIVE_BATTLE:  { Basic_Coroutine__ACTIVE_BATTLE(time - lastTime);  }   break;

                case Basic__COROUTINE_PHASE.ACTIVE_MOVE_READY:  { Basic_Coroutine__ACTIVE_MOVE_READY();             }   break;
                case Basic__COROUTINE_PHASE.ACTIVE_MOVE:        { Basic_Coroutine__ACTIVE_MOVE(time - lastTime);    }   break;
                case Basic__COROUTINE_PHASE.ACTIVE_MOVE_END:    { Basic_Coroutine__ACTIVE_MOVE_END();               }   break;
            }

            lastTime = time;

            yield return null;
        }
    }

    // INIT ==========
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
                        Basic_coroutinePhase = Basic__COROUTINE_PHASE.SETTING_STAGE_START;
                    }
                }
                break;
        }
    }

    // RESET ==========
    void Basic_Coroutine__RESET()
    {
        Basic_coroutinePhase = Basic__COROUTINE_PHASE.SETTING_FADE_OUT_START;
    }

    // FADE_OUT ==========
    void Basic_Coroutine__SETTING_FADE_OUT_START()
    {
        UI_FadeOutStart();
        Basic_coroutinePhase = Basic__COROUTINE_PHASE.SETTING_FADE_OUT;
    }

    void Basic_Coroutine__SETTING_FADE_OUT(float _timer)
    {
        UI_FadeOut(_timer);

        if(UI_fadeTime >= 1.0f)
        {
            Basic_coroutinePhase = Basic__COROUTINE_PHASE.SETTING_STAGE_START;
        }
    }

    // SETTING_STAGE ==========
    void Basic_Coroutine__SETTING_STAGE_START()
    {
        Basic_coroutinePhase = Basic__COROUTINE_PHASE.SETTING_STAGE_DATA_CHECK;
    }

    // STAGE_DATA_CHECK
    void Basic_Coroutine__SETTING_STAGE_DATA_CHECK()
    {
        GameObject obj = (GameObject)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
            Giggle_ScriptBridge.EVENT.DATABASE__STAGE__GET_DATA_FROM_SAVE);
        
        object isOnObj
            = Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.DATABASE__STAGE__GET_IS_OPEN);
        
        if(isOnObj != null)
        {
            if((bool)isOnObj)
            {
                Basic_coroutinePhase = Basic__COROUTINE_PHASE.SETTING_STAGE;
            }
        }
    }

    // SETTING_STAGE
    void Basic_Coroutine__SETTING_STAGE()
    {
        GameObject obj = (GameObject)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
            Giggle_ScriptBridge.EVENT.DATABASE__STAGE__GET_DATA_FROM_SAVE);

        Map_Load(obj);

        //
        Basic_coroutinePhase = Basic__COROUTINE_PHASE.SETTING_PLAYER_START;
    }

    // SETTING_PLAYER ==========
    // SETTING_PLAYER_START
    void Basic_Coroutine__SETTING_PLAYER_START()
    {
        Basic_coroutinePhase = Basic__COROUTINE_PHASE.SETTING_PLAYER_DATA_CHECK;
    }

    // PLAYER_DATA_CHECK
    void Basic_Coroutine__SETTING_PLAYER_DATA_CHECK()
    {
        object isOnObj
            = Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.DATABASE__MARIONETTE__GET_IS_OPEN);
        
        if(isOnObj != null)
        {
            if((bool)isOnObj)
            {
                if(Basic_Coroutine__PLAYER_DATA_CHECK__Player())
                {
                    Basic_coroutinePhase = Basic__COROUTINE_PHASE.SETTING_PLAYER;
                }
            }
        }
    }

    bool Basic_Coroutine__PLAYER_DATA_CHECK__Player()
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
        //
        Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
            Giggle_ScriptBridge.EVENT.DATABASE__PINOCCHIO__GET_DATA_FROM_ID,
            1111);

        //
        int whileCount = 0;
        while(whileCount < formationList[formationSelect].Basic_VarFormation.Count)
        {
            int characterId = formationList[formationSelect].Basic_VarFormation[whileCount];
            if(characterId >= 0)
            {
                //characterId -= 1;
                for(int for0 = 0; for0 < marionetteList.Count; for0++)
                {
                    if(marionetteList[for0].Basic_VarInventoryId.Equals(characterId))
                    {
                        characterId = marionetteList[for0].Basic_VarDataId;
                        break;
                    }
                }
                Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                    Giggle_ScriptBridge.EVENT.DATABASE__MARIONETTE__GET_DATA_FROM_ID,
                    characterId);
            }

            whileCount++;
        }

        //
        res =
            (bool)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.DATABASE__PINOCCHIO__GET_IS_OPEN) &&
            (bool)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.DATABASE__MARIONETTE__GET_IS_OPEN);

        //
        return res;
    }

    // PLAYER_SETTING
    void Basic_Coroutine__SETTING_PLAYER()
    {
        //
        //
        Giggle_Character.Save pinocchioData
            = (Giggle_Character.Save)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.PLAYER__PINOCCHIO__VAR_DATA);

        //
        int formationSelect
            = (int)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.PLAYER__FORMATION__GET_SELECT);

        List<Giggle_Player.Formation> formationList
            = (List<Giggle_Player.Formation>)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.PLAYER__FORMATION__GET_FORMATION_LIST);

        //
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
            int formationId = formationList[formationSelect].Basic_VarFormation[whileCount];
            if(!formationId.Equals(-1))
            {
                switch(formationId)
                {
                    case -2:
                        {
                            data = (Giggle_Character.Database)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                                Giggle_ScriptBridge.EVENT.DATABASE__PINOCCHIO__GET_DATA_FROM_ID,
                                //
                                pinocchioData.Basic_VarDataId);
                        }
                        break;
                    default:
                        {
                            data = (Giggle_Character.Database)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                                Giggle_ScriptBridge.EVENT.DATABASE__MARIONETTE__GET_DATA_FROM_ID,
                                //
                                marionetteList[formationList[formationSelect].Basic_VarFormation[whileCount] - 1].Basic_VarDataId);
                        }
                        break;
                }

                unit = GameObject.Instantiate(data.Basic_VarUnit, Formation_Ally.Basic_GetTile(whileCount));
                unit.Basic_Init(this, data);
            }

            whileCount++;
        }

        Basic_coroutinePhase = Basic__COROUTINE_PHASE.SETTING_ENEMY_START;
    }

    // ENEMY_SETTING ==========

    void Basic_Coroutine__SETTING_ENEMY_START()
    {
        Formation_Enemy.Basic_VarParent.localScale = Vector3.zero;

        //
        Basic_coroutinePhase = Basic__COROUTINE_PHASE.SETTING_ENEMY_DATA_CHECK;
    }

    // STAGE_DATA_CHECK
    void Basic_Coroutine__SETTING_ENEMY_DATA_CHECK()
    {
        Giggle_Character.Database data = (Giggle_Character.Database)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
            Giggle_ScriptBridge.EVENT.DATABASE__MARIONETTE__GET_DATA_FROM_ID,
            21001);
            
        object isOnObj
            = Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.DATABASE__MARIONETTE__GET_IS_OPEN);
        
        if(isOnObj != null)
        {
            if((bool)isOnObj)
            {
                Basic_coroutinePhase = Basic__COROUTINE_PHASE.SETTING_ENEMY;
            }
        }
    }

    //
    void Basic_Coroutine__SETTING_ENEMY()
    {
        // 캐릭터 생성
        Giggle_Character.Database data = null;
        Giggle_Unit unit = null;

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
            Giggle_ScriptBridge.EVENT.DATABASE__MARIONETTE__GET_DATA_FROM_ID,
            21001);

        unit = GameObject.Instantiate(data.Basic_VarUnit, Formation_Enemy.Basic_GetTile(0));
        unit.Basic_Init(this, data);

        // 총알 리셋
        Bullet_Reset();

        //
        if(UI_fade.gameObject.activeSelf)
        {
            Basic_coroutinePhase = Basic__COROUTINE_PHASE.FADE_IN_START;
        }
        else
        {
            Basic_coroutinePhase = Basic__COROUTINE_PHASE.SETTING_END;
        }
    }

    //
    void Basic_Coroutine__FADE_IN_START()
    {
        UI_FadeInStart();

        Basic_coroutinePhase = Basic__COROUTINE_PHASE.FADE_IN;
    }

    void Basic_Coroutine__FADE_IN(float _timer)
    {
        UI_FadeIn(_timer);

        if(UI_fadeTime <= 0.0f)
        {
            Basic_coroutinePhase = Basic__COROUTINE_PHASE.SETTING_END;
        }
    }

    //
    void Basic_Coroutine__SETTING_END()
    {
        Basic_coroutinePhase = Basic__COROUTINE_PHASE.ACTIVE_MOVE_READY;
    }

    // ACTIVE_BATTLE ==========
    void Basic_Coroutine__ACTIVE_BATTLE(float _timer)
    {
        Bullet_Coroutine(_timer);
    }

    // ACTIVE_MOVE ==========
    void Basic_Coroutine__ACTIVE_MOVE_READY()
    {
        Map_Coroutine__ACTIVE_MOVE_READY();
        Formation_Coroutine__ACTIVE_MOVE_READY();

        Formation_Enemy.Basic_VarParent.localScale = new Vector3(-1,1,1);

        Basic_coroutinePhase = Basic__COROUTINE_PHASE.ACTIVE_MOVE;
    }

    void Basic_Coroutine__ACTIVE_MOVE(float _timer)
    {
        Map_Coroutine__ACTIVE_MOVE(_timer);
        Formation_Coroutine__ACTIVE_MOVE();
    }

    void Basic_Coroutine__ACTIVE_MOVE_END()
    {
        Basic_coroutinePhase = Basic__COROUTINE_PHASE.ACTIVE_BATTLE;
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
        // Basic_parent
        public Transform    Basic_VarParent { get { return Basic_parent;    }   }
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

    // ACTIVE_MOVE
    void Formation_Coroutine__ACTIVE_MOVE_READY()
    {
        Formation_Enemy.Basic_VarParent.position = Map_ground.position;
        float x = Formation_Enemy.Basic_VarParent.localPosition.x;
        x += 2.73f;
        Formation_Enemy.Basic_VarParent.localPosition = new Vector3(x, 0.0f, 0.0f);
    }

    void Formation_Coroutine__ACTIVE_MOVE()
    {
        Formation_Enemy.Basic_VarParent.position = Map_ground.position;
        float x = Formation_Enemy.Basic_VarParent.localPosition.x;
        x += 2.73f;
        Formation_Enemy.Basic_VarParent.localPosition = new Vector3(x, 0.0f, 0.0f);
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

    #region MAP

    [Header("MAP ==================================================")]
    [SerializeField] Transform  Map_parent;
    [SerializeField] float      Map_time;

    [Header("RUNNING")]
    [SerializeField] Transform  Map_ground;
    [SerializeField] float      Map_speed;

    ////////// Getter & Setter          //////////

    ////////// Method                   //////////
    void Map_Load(GameObject _obj)
    {
        while(Map_parent.childCount > 0)
        {
            Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.MASTER__GARBAGE__REMOVE,
                Map_parent.GetChild(0));
        }

        //
        GameObject obj = GameObject.Instantiate(_obj, Map_parent);
        Map_ground = obj.transform.Find("Ground");
    }

    void Map_Coroutine__ACTIVE_MOVE_READY()
    {
        Map_ground.localPosition = new Vector3(30, 0, 0);
        Map_speed = Map_ground.localPosition.x / Map_time;
    }

    void Map_Coroutine__ACTIVE_MOVE(float _timer)
    {
        float x = Map_ground.localPosition.x;
        x -= _timer * Map_speed;
        if(x <= 0.0f)
        {
            x = 0.0f;
            Basic_coroutinePhase = Basic__COROUTINE_PHASE.ACTIVE_MOVE_END;
        }

        Map_ground.localPosition = new Vector3(x, 0.0f, 0.0f);
    }

    ////////// Constructor & Destroyer  //////////

    #endregion

    #region UI

    [Header("UI ==================================================")]
    [SerializeField] Image UI_fade;

    [Header("RUNNING")]
    [SerializeField] float  UI_fadeTime;

    ////////// Getter & Setter          //////////

    ////////// Method                   //////////
    // UI_FadeOut
    void UI_FadeOutStart()
    {
        UI_fadeTime = 0.0f;

        Color c = UI_fade.color;
        c.a = 0.0f;
        UI_fade.color = c;
        UI_fade.gameObject.SetActive(true);
    }

    void UI_FadeOut(float _timer)
    {
        UI_fadeTime += _timer;

        if(UI_fadeTime >= 1.0f)
        {
            UI_fadeTime = 1.0f;
        }

        Color c = UI_fade.color;
        c.a = UI_fadeTime;
        UI_fade.color = c;
    }

    // UI_FadeIn
    void UI_FadeInStart()
    {
        UI_fadeTime = 1.0f;

        Color c = UI_fade.color;
        c.a = 1.0f;
        UI_fade.color = c;
    }

    void UI_FadeIn(float _timer)
    {
        UI_fadeTime -= _timer;

        if(UI_fadeTime <= 0.0f)
        {
            UI_fadeTime = 0.0f;
            UI_fade.gameObject.SetActive(false);
        }

        Color c = UI_fade.color;
        c.a = UI_fadeTime;
        UI_fade.color = c;
    }

    ////////// Constructor & Destroyer  //////////

    #endregion
}
