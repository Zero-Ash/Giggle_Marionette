using UnityEngine;
//
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

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
        SETTING_OBJECT,

        //
        SETTING_PLAYER_START,
        SETTING_PLAYER_DATA_CHECK,
        SETTING_PLAYER,

        //
        SETTING_ENEMY_START,
        SETTING_ENEMY_DATA_CHECK,
        SETTING_ENEMY,

        //
        SETTING_POWER_SAVING,

        //
        FADE_IN_START,
        FADE_IN,

        //
        SETTING_END,

        //
        ACTIVE_BATTLE,
        ACTIVE_MOVE_READY,
        ACTIVE_MOVE,
        ACTIVE_MOVE_END,
        ACTIVE_RESULT_WIN,
        ACTIVE_RESULT_LOSE,
        ACTIVE_RESULT_LOSE_DOING,
        ACTIVE_END
    }

    [SerializeField] protected Basic__COROUTINE_PHASE   Basic_coroutinePhase;
    [SerializeField] protected GameObject               Basic_parent;

    //
    IEnumerator Basic_coroutine;

    ////////// Getter & Setter          //////////
    public Basic__COROUTINE_PHASE   Basic_VarCoroutinePhase { get { return Basic_coroutinePhase;    } set{ Basic_coroutinePhase = value;    }   }

    public bool Basic_VarParentActive   { get { return Basic_parent.activeSelf; } set { Basic_parent.SetActive(value);  }   }

    ////////// Method                   //////////
    
    public void Basic_Accel()
    {
        Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.PLAYER__STAGE__ACCEL);

        Formation_AcimatorSpeed();
    }

    public void Basic_PowerSaving()
    {
        if(Giggle_ScriptBridge.Basic_VarInstance.Basic_GetIsInMethod(Giggle_ScriptBridge.EVENT.MAIN__POWER_SAVING__GET_IS_ON))
        {
            Formation_PowerSaving();
            Bullet_PowerSaving();
            Effect_PowerSaving();
            Map_PowerSaving();
        }
    }

    // Basic_Init ====================
    public void Basic_Init()
    {
        //
        Basic_coroutinePhase = Basic__COROUTINE_PHASE.INIT;

        Basic_coroutine = Basic_Coroutine();

        Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
            Giggle_ScriptBridge.EVENT.MASTER__BASIC__START_COROUTINE, Basic_coroutine);
    }

    // Basic_Coroutine ====================
    IEnumerator Basic_Coroutine()
    {
        int phase = 0;

        float time      = Time.time;
        float lastTime  = Time.time;

        float stageSpeed = (float)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.PLAYER__STAGE__VAR_SPEED);

        while(phase != -1)
        {
            if(Basic_parent.activeSelf || Basic_coroutinePhase.Equals(Basic__COROUTINE_PHASE.INIT))
            {
                time = Time.time;

                stageSpeed = (float)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.PLAYER__STAGE__VAR_SPEED);

                switch(Basic_coroutinePhase)
                {
                    case Basic__COROUTINE_PHASE.INIT:               { Basic_Coroutine__INIT(ref phase);     }   break;

                    // SETTING
                    case Basic__COROUTINE_PHASE.RESET:  { Basic_Coroutine__RESET(); }   break;

                    case Basic__COROUTINE_PHASE.SETTING_FADE_OUT_START: { Basic_Coroutine__SETTING_FADE_OUT_START();    }   break;
                    case Basic__COROUTINE_PHASE.SETTING_FADE_OUT:       { Basic_Coroutine__SETTING_FADE_OUT();          }   break;

                    case Basic__COROUTINE_PHASE.SETTING_STAGE_START:        { Basic_Coroutine__SETTING_STAGE_START();       }   break;
                    case Basic__COROUTINE_PHASE.SETTING_STAGE_DATA_CHECK:   { Basic_Coroutine__SETTING_STAGE_DATA_CHECK();  }   break;
                    case Basic__COROUTINE_PHASE.SETTING_STAGE:              { Basic_Coroutine__SETTING_STAGE();             }   break;

                    case Basic__COROUTINE_PHASE.SETTING_OBJECT: { Basic_Coroutine__SETTING_OBJECT();    }   break;

                    case Basic__COROUTINE_PHASE.SETTING_PLAYER_START:       { Basic_Coroutine__SETTING_PLAYER_START();      }   break;
                    case Basic__COROUTINE_PHASE.SETTING_PLAYER_DATA_CHECK:  { Basic_Coroutine__SETTING_PLAYER_DATA_CHECK(); }   break;
                    case Basic__COROUTINE_PHASE.SETTING_PLAYER:             { Basic_Coroutine__SETTING_PLAYER();            }   break;

                    case Basic__COROUTINE_PHASE.SETTING_ENEMY_START:        { Basic_Coroutine__SETTING_ENEMY_START();       }   break;
                    case Basic__COROUTINE_PHASE.SETTING_ENEMY_DATA_CHECK:   { Basic_Coroutine__SETTING_ENEMY_DATA_CHECK();  }   break;
                    case Basic__COROUTINE_PHASE.SETTING_ENEMY:              { Basic_Coroutine__SETTING_ENEMY();             }   break;

                    case Basic__COROUTINE_PHASE.SETTING_POWER_SAVING:   { Basic_Coroutine__SETTING_POWER_SAVING();  }   break;

                    case Basic__COROUTINE_PHASE.FADE_IN_START:  { Basic_Coroutine__FADE_IN_START(); }   break;
                    case Basic__COROUTINE_PHASE.FADE_IN:        { Basic_Coroutine__FADE_IN();       }   break;

                    case Basic__COROUTINE_PHASE.SETTING_END:    { Basic_Coroutine__SETTING_END();   }   break;

                    // ACTIVE
                    case Basic__COROUTINE_PHASE.ACTIVE_BATTLE:  { Basic_Coroutine__ACTIVE_BATTLE((time - lastTime) * stageSpeed);  }   break;

                    case Basic__COROUTINE_PHASE.ACTIVE_MOVE_READY:  { Basic_Coroutine__ACTIVE_MOVE_READY();                         }   break;
                    case Basic__COROUTINE_PHASE.ACTIVE_MOVE:        { Basic_Coroutine__ACTIVE_MOVE((time - lastTime) * stageSpeed); }   break;
                    case Basic__COROUTINE_PHASE.ACTIVE_MOVE_END:    { Basic_Coroutine__ACTIVE_MOVE_END();                           }   break;

                    case Basic__COROUTINE_PHASE.ACTIVE_RESULT_WIN:  { Basic_Coroutine__ACTIVE_RESULT_WIN(); }   break;

                    case Basic__COROUTINE_PHASE.ACTIVE_RESULT_LOSE:         { Basic_Coroutine__ACTIVE_RESULT_LOSE();    }   break;
                    case Basic__COROUTINE_PHASE.ACTIVE_RESULT_LOSE_DOING:   { Basic_Coroutine__ACTIVE_RESULT_DOING();   }   break;
                }

                Effect_Coroutine();

                lastTime = time;
            }

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
                    bool isDoing
                        = (bool)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                            Giggle_ScriptBridge.EVENT.PLAYER__BASIC__IS_NETWORK_DATA_LOAD_DOING);
                            
                    if(!isDoing)
                    {
                        _phase = 10;
                    }
                }
                break;
            case 10:
                {
                    if(Formation_CotoutineInit0())
                    {
                        _phase = 20;
                    }
                }
                break;
            case 20:
                {
                    if(Formation_CotoutineInit1())
                    {
                        _phase = 100;
                    }
                }
                break;
            case 100:
                {
                    UI_Init();

                    //
                    _phase = 0;
                    Basic_coroutinePhase = Basic__COROUTINE_PHASE.SETTING_STAGE_START;
                }
                break;
        }
    }

    // RESET ==========
    protected virtual void Basic_Coroutine__RESET()
    {
        Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
            Giggle_ScriptBridge.EVENT.MAIN__BATTLE__VAR_LOSE_ACTIVE,
            //
            false);

        Basic_coroutinePhase = Basic__COROUTINE_PHASE.SETTING_FADE_OUT_START;
    }

    // FADE_OUT ==========
    void Basic_Coroutine__SETTING_FADE_OUT_START()
    {
        Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MAIN__BATTLE__FADE_OUT);
        Basic_coroutinePhase = Basic__COROUTINE_PHASE.SETTING_FADE_OUT;
    }

    void Basic_Coroutine__SETTING_FADE_OUT()
    {
        if( !(bool)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.PLAYER__BASIC__IS_NETWORK_DATA_LOAD_DOING) &&
            ((int)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MAIN__UI__VAR_COROUTINE_PHASE) == 10002))
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
            Giggle_ScriptBridge.EVENT.DATABASE__STAGE__GET_OBJ_FROM_SAVE);
        
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
    protected virtual void Basic_Coroutine__SETTING_STAGE()
    {
        GameObject obj = (GameObject)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
            Giggle_ScriptBridge.EVENT.DATABASE__STAGE__GET_OBJ_FROM_SAVE);

        Map_Load(obj);

        //
        Basic_coroutinePhase = Basic__COROUTINE_PHASE.SETTING_OBJECT;
    }

    // SETTING_OBJECT ==========
    void Basic_Coroutine__SETTING_OBJECT()
    {
        // 총알 리셋
        Bullet_Reset();

        // 유닛 비활성화
        Formation_Ally.Basic_VarParent.gameObject.SetActive(false);
        Formation_Enemy.Basic_VarParent.gameObject.SetActive(false);

        //
        Basic_coroutinePhase = Basic__COROUTINE_PHASE.SETTING_PLAYER_START;
    }

    // SETTING_PLAYER ==========
    // SETTING_PLAYER_START
    protected virtual void Basic_Coroutine__SETTING_PLAYER_START()
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
    protected virtual void Basic_Coroutine__SETTING_PLAYER()
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
        Giggle_Character.Save save = null;
        Giggle_Character.Database data = null;
        Giggle_Unit unit = null;

        // player
        // 기존 데이터 날리기
        Formation_Ally.Basic_Reset();

        int whileCount = 0;
        while(whileCount < formationList[formationSelect].Basic_VarFormation.Count)
        {
            //
            int formationId = formationList[formationSelect].Basic_VarFormation[whileCount];
            if(!formationId.Equals(-1))
            {
                switch(formationId)
                {
                    case -2:
                        {
                            save = pinocchioData;
                            data = (Giggle_Character.Database)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                                Giggle_ScriptBridge.EVENT.DATABASE__PINOCCHIO__GET_DATA_FROM_ID,
                                //
                                save.Basic_VarDataId);
                        }
                        break;
                    default:
                        {
                            save = marionetteList[formationList[formationSelect].Basic_VarFormation[whileCount]];
                            data = (Giggle_Character.Database)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                                Giggle_ScriptBridge.EVENT.DATABASE__MARIONETTE__GET_DATA_FROM_ID,
                                //
                                save.Basic_VarDataId);
                        }
                        break;
                }
                
                unit = GameObject.Instantiate(data.Basic_VarUnit, Formation_Ally.Basic_GetTile(whileCount));
                unit.Basic_Init(this, save);
            }

            whileCount++;
        }

        Formation_Ally.Basic_BuffStageStart();

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

    // SETTING_ENEMY
    void Basic_Coroutine__SETTING_ENEMY()
    {
        // 캐릭터 생성
        Giggle_Character.Database data = null;
        Giggle_Unit unit = null;

        // enemy
        // 기존 데이터 날리기
        Formation_Enemy.Basic_Reset();

        int stageId = (int)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.PLAYER__STAGE__VAR_SELECT);
        
        Giggle_Stage.Stage stage
            = (Giggle_Stage.Stage)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.DATABASE__STAGE__GET_STAGE_FROM_ID,
                stageId);
        
        for(int for0 = 0; for0 < stage.Basic_FormationVarCount; for0++)
        {
            data = (Giggle_Character.Database)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.DATABASE__MARIONETTE__GET_DATA_FROM_ID,
                stage.Basic_FormationGetData(for0).Basic_VarId);

            Giggle_Character.Save save = new Giggle_Character.Save(-1, data.Basic_VarId);
            save.Basic_VarLevel = stage.Basic_FormationGetData(for0).Basic_VarLv;

            unit = GameObject.Instantiate(data.Basic_VarUnit, Formation_Enemy.Basic_GetTile(stage.Basic_FormationGetData(for0).Basic_VarFormation));
            unit.Basic_Init(this, save);
        }

        Formation_Enemy.Basic_BuffStageStart();

        //
        Basic_coroutinePhase = Basic__COROUTINE_PHASE.SETTING_POWER_SAVING;
    }


    // SETTING_POWER_SAVING ==========
    protected virtual void Basic_Coroutine__SETTING_POWER_SAVING()
    {
        Basic_PowerSaving();

        // 유닛 활성화
        Formation_Ally.Basic_VarParent.gameObject.SetActive(true);
        Formation_Enemy.Basic_VarParent.gameObject.SetActive(true);

        //
        Basic_coroutinePhase = Basic__COROUTINE_PHASE.FADE_IN_START;
    }

    //
    void Basic_Coroutine__FADE_IN_START()
    {
        Image fade = (Image)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MAIN__BATTLE__VAR_FADE);
        // 페이드가 진행되었을 때
        if(fade.gameObject.activeSelf)
        {
            Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MAIN__BATTLE__FADE_IN);

            Basic_coroutinePhase = Basic__COROUTINE_PHASE.FADE_IN;
        }
        // 페이드가 진행되지 않았을 때
        else
        {
            Basic_coroutinePhase = Basic__COROUTINE_PHASE.SETTING_END;
        }
    }

    void Basic_Coroutine__FADE_IN()
    {
        if((int)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MAIN__UI__VAR_COROUTINE_PHASE) == 10102)
        {
            Basic_coroutinePhase = Basic__COROUTINE_PHASE.SETTING_END;
        }
    }

    //
    void Basic_Coroutine__SETTING_END()
    {
        //
        Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
            Giggle_ScriptBridge.EVENT.MAIN__BATTLE__VAR_WIN_ACTIVE,
            //
            false);

        //
        Basic_coroutinePhase = Basic__COROUTINE_PHASE.ACTIVE_MOVE_READY;
    }

    // ACTIVE_BATTLE ==========
    void Basic_Coroutine__ACTIVE_BATTLE(float _timer)
    {
        Bullet_Coroutine(_timer);

        if(Formation_Enemy.Basic_VarIsUnitEmpty)
        {
            Basic_coroutinePhase = Basic__COROUTINE_PHASE.ACTIVE_RESULT_WIN;
        }
        else
        {
            if(Formation_Ally.Basic_VarIsUnitEmpty)
            {
                Basic_coroutinePhase = Basic__COROUTINE_PHASE.ACTIVE_RESULT_LOSE;
            }
        }
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
        // 이동 종료 후, 패시브 체크
        Formation_Ally.Basic_BuffStageStart();

        //
        Basic_coroutinePhase = Basic__COROUTINE_PHASE.ACTIVE_BATTLE;
    }

    // ACTIVE_RESULT ==========

    //
    protected virtual void Basic_Coroutine__ACTIVE_RESULT_WIN()
    {
        //
        Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
            Giggle_ScriptBridge.EVENT.MAIN__BATTLE__VAR_WIN_ACTIVE,
            //
            true);

        //
        bool isNext = (bool)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.PLAYER__STAGE__VAR_IS_NEXT);
        
        if(isNext)
        {
            // 클리어한 스테이지의 정보 가져오기
            int stageId = (int)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.PLAYER__STAGE__VAR_SELECT);

            Giggle_Stage.Stage stage = (Giggle_Stage.Stage)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.DATABASE__STAGE__GET_STAGE_FROM_ID,
                stageId);

            // 다음 스테이지로 넘기기
            Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.PLAYER__STAGE__NEXT);

            // 스테이지가 보스라면 배경까지 셋팅한다.
            if(stage.Basic_VarIsBoss)
            {
                Basic_coroutinePhase = Giggle_Battle.Basic__COROUTINE_PHASE.RESET;
            }
            // 유닛만 셋팅한다.
            else
            {
                Basic_coroutinePhase = Basic__COROUTINE_PHASE.SETTING_OBJECT;
            }
        }
        else
        {
            Basic_coroutinePhase = Basic__COROUTINE_PHASE.SETTING_OBJECT;
        }
    }

    protected virtual void Basic_Coroutine__ACTIVE_RESULT_WIN_DOING(float _timer)
    {

    }

    //
    protected virtual void Basic_Coroutine__ACTIVE_RESULT_LOSE()
    {
        Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MAIN__BATTLE__LOSE);

        Basic_coroutinePhase = Basic__COROUTINE_PHASE.ACTIVE_RESULT_LOSE_DOING;
    }

    protected virtual void Basic_Coroutine__ACTIVE_RESULT_DOING()
    {
        if((int)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MAIN__UI__VAR_COROUTINE_PHASE) == 10202)
        {
            Basic_coroutinePhase = Giggle_Battle.Basic__COROUTINE_PHASE.RESET;
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
        [SerializeField] Giggle_Battle  Basic_manager;

        [SerializeField] Transform          Basic_parent;
        [SerializeField] List<Transform>    Basic_tiles;

        [Header("RUNNING")]
        [SerializeField] List<Giggle_Unit> Basic_units;
        
        [SerializeField] Giggle_Character.Status    Basic_buff;

        ////////// Getter & Setter          //////////
        // Basic_parent
        public Transform    Basic_VarParent { get { return Basic_parent;    }   }
        // Basic_tiles
        public Transform    Basic_GetTile(int _count)   { return Basic_tiles[_count];   }

        public bool Basic_VarIsUnitEmpty
        {
            get
            {
                bool res = true;

                //
                for(int for0 = 0; for0 < Basic_tiles.Count; for0++)
                {
                    if(Basic_tiles[for0].childCount > 0)
                    {
                        res = false;
                        break;
                    }
                }

                //
                return res;
            }
        }

        ////////// Method                   //////////
        //
        public void Basic_Reset()
        {
            for(int for0 = 0; for0 < Basic_tiles.Count; for0++)
            {
                while(Basic_tiles[for0].childCount > 0)
                {
                    Transform element = Basic_tiles[for0].GetChild(0);
                    if(element.GetComponent<Giggle_Unit>() != null)
                    {
                        element.GetComponent<Giggle_Unit>().Basic_Release();
                    }
                        
                    Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                        Giggle_ScriptBridge.EVENT.MASTER__GARBAGE__REMOVE,
                        //
                        element);
                }
            }
        }
        
        //
        public void Basic_RemoveUnit(Transform _unit)
        {
            for(int for0 = 0; for0 < Basic_tiles.Count; for0++)
            {
                if(Basic_tiles[for0].childCount > 0)
                {
                    Transform element = Basic_tiles[for0].GetChild(0);
                    if(element.Equals(_unit))
                    {
                        element.GetComponent<Giggle_Unit>().Basic_Release();
                        
                        Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                            Giggle_ScriptBridge.EVENT.MASTER__GARBAGE__REMOVE,
                            //
                            element);

                        break;
                    }
                }
            }
        }

        //
        public void Basic_UnitAnimatorSpeed()
        {
            float speed = (float)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.PLAYER__STAGE__VAR_SPEED);
            
            for(int for0 = 0; for0 < Basic_tiles.Count; for0++)
            {
                if(Basic_tiles[for0].childCount > 0)
                {
                    Basic_tiles[for0].GetChild(0).GetComponent<Giggle_Unit>().Model_AnimatorVarSpeed = speed;
                }
            }
        }

        // 버프 계산
        // Basic_BuffStageStart
        public void Basic_BuffStageStart()
        {
            // 1. 초기화
            Basic_buff.Basic_Reset();

            List<int> attributeCount = new List<int>();
            attributeCount.Clear();
            while (attributeCount.Count < (int)Giggle_Master.ATTRIBUTE.TOTAL)
            {
                attributeCount.Add(0);
            }

            // 2. 타일 전체 체크
            for (int for0 = 0; for0 < Basic_tiles.Count; for0++)
            {
                // 타일에 캐릭터가 있는 지 여부
                if (Basic_tiles[for0].childCount <= 0)
                {
                    continue;
                }

                Transform element = Basic_tiles[for0].GetChild(0);

                // 속성
                Basic_BuffStageStart__Attribute_2(element.GetComponent<Giggle_Unit>(), ref attributeCount);

                // 패시브
                Basic_BuffStageStart__Passive_2(element.GetComponent<Giggle_Unit>(), for0);
            }

            // 3. 계산 시작
            int max = 0;
            int count = 0;

            Basic_BuffStageStart__Attribute_3(
                attributeCount,
                ref max, ref count);

            // 다른 속성 체크를 위해 현 속성 갯수 소멸
            attributeCount[count] = 0;
            // 빛 속성 가중치
            max += attributeCount[(int)Giggle_Master.ATTRIBUTE.LIGHT];

            int BounsType = 0;
            switch (max)
            {
                case 4:
                    {
                        Basic_BuffStageStart__Attribute_3(
                            attributeCount,
                            ref max, ref count);

                        // i. 동일 속성 4명
                        if (max < 2) { BounsType = 1; }
                        // ii. 동일 속성 4명, 다른 속성 2명
                        else { BounsType = 2; }
                    }
                    break;
                case 5:
                    {
                        BounsType = 3;
                    }
                    break;
                case 6:
                    {
                        BounsType = 4;
                    }
                    break;
            }

            Basic_buff.Basic_FormationBounsSetting(BounsType, attributeCount[(int)Giggle_Master.ATTRIBUTE.DARK]);

            //
            //
            for (int for0 = 0; for0 < Basic_tiles.Count; for0++)
            {
                if (Basic_tiles[for0].childCount > 0)
                {
                    Transform element = Basic_tiles[for0].GetChild(0);

                    element.GetComponent<Giggle_Unit>().Status_VarBounsPercentStatus.Basic_Plus(Basic_buff);
                    element.GetComponent<Giggle_Unit>().Status_CalculateAll();
                }
            }
        }

        void Basic_BuffStageStart__Attribute_2(Giggle_Unit _unit, ref List<int> _attributeCount)
        {
            // 속성 모으기
            _attributeCount[(int)_unit.Status_VarDatabase.Basic_VarAttribute]++;
        }

        void Basic_BuffStageStart__Attribute_3(
            List<int> _attributeCount,
            ref int _max, ref int _count)
        {
            _max = 0;
            _count = 0;

            for (int for0 = 0; for0 <= (int)Giggle_Master.ATTRIBUTE.EARTH; for0++)
            {
                if (_attributeCount[for0] > _max)
                {
                    _max = _attributeCount[for0];
                    _count = for0;
                }
            }
        }

        void Basic_BuffStageStart__Passive_2(Giggle_Unit _unit, int _position)
        {
            for (int for0 = 0; for0 < _unit.Basic_VarSave.Basic_VarPassiveCount; for0++)
            {
                Giggle_Character.Save.Passive passive = _unit.Basic_VarSave.Basic_GetPassive(for0);
                Giggle_Character.Passive data
                    = (Giggle_Character.Passive)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                        Giggle_ScriptBridge.EVENT.DATABASE__MARIONETTE__GET_PASSIVE_FROM_ID,
                        //
                        passive.Basic_VarId);
                data.Basic_OnStageStart(Basic_tiles, _position);
            }
        }

        // Basic_BuffBattleStart
        public void Basic_BuffBattleStart()
        {
            for (int for0 = 0; for0 < Basic_tiles.Count; for0++)
            {
                if (Basic_tiles[for0].childCount > 0)
                {
                    Transform element = Basic_tiles[for0].GetChild(0);
                    //
                    for (int for1 = 0; for1 < element.GetComponent<Giggle_Unit>().Basic_VarSave.Basic_VarPassiveCount; for1++)
                    {
                        Giggle_Character.Save.Passive passive = element.GetComponent<Giggle_Unit>().Basic_VarSave.Basic_GetPassive(for1);
                        Giggle_Character.Passive data
                            = (Giggle_Character.Passive)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                                Giggle_ScriptBridge.EVENT.DATABASE__MARIONETTE__GET_PASSIVE_FROM_ID,
                                //
                                passive.Basic_VarId);
                        data.Basic_OnBattleStart(Basic_tiles, for0);
                    }
                }
            }
        }

        //
        public void Basic_PowerSaving()
        {
            for (int for0 = 0; for0 < Basic_tiles.Count; for0++)
            {
                for (int for1 = 0; for1 < Basic_tiles[for0].childCount; for1++)
                {
                    Giggle_Unit element = Basic_tiles[for0].GetChild(for1).GetComponent<Giggle_Unit>();
                    if (element != null)
                    {
                        element.Model_View();
                    }
                }
            }
        }

        //
        public void Basic_Init(Giggle_Battle _manager)
        {
            Basic_manager = _manager;

            for (int for0 = 0; for0 < Basic_parent.childCount; for0++)
            {
                Basic_tiles.Add(Basic_parent.Find(for0.ToString()));
            }

            Basic_buff = new Giggle_Character.Status();
            Basic_buff.Basic_Reset();
        }

        ////////// Constructor & Destroyer  //////////
        public void Dispose()
        {

        }
    }

    [Header("FORMATION ==================================================")]
    [SerializeField] protected Formation    Formation_Ally;
    [SerializeField] protected Formation    Formation_Enemy;

    ////////// Getter & Setter          //////////
    public Formation Formation_VarAlly  { get{ return Formation_Ally;   }   }

    public Formation Formation_VarEnemy { get{ return Formation_Enemy;  }   }

    ////////// Method                   //////////

    //
    void Formation_AcimatorSpeed()
    {
        Formation_Ally.Basic_UnitAnimatorSpeed();
        Formation_Enemy.Basic_UnitAnimatorSpeed();
    }

    // SLEEP
    void Formation_PowerSaving()
    {
        Formation_Ally.Basic_PowerSaving();
        Formation_Enemy.Basic_PowerSaving();
    }

    //
    bool Formation_CotoutineInit0()
    {
        bool res = false;

        if(Formation_Ally != null)
        {
            Formation_Ally.Basic_Init(this);

            res = true;
        }

        return res;
    }

    bool Formation_CotoutineInit1()
    {
        bool res = false;

        if(Formation_Enemy != null)
        {
            Formation_Enemy.Basic_Init(this);

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
        public enum TYPE
        {
            NORMAL,
            SPLIT,
            FIRE_BALL
        }

        [SerializeField] GameObject Basic_obj;

        [Header("RUNNING")]
        [SerializeField] Giggle_Unit Basic_owner;
        [SerializeField] Giggle_Unit Basic_target;

        [SerializeField] TYPE   Basic_type;
        [SerializeField] int    Basic_damage;

        [SerializeField] Vector3    Basic_startPos;
        [SerializeField] Vector3    Basic_destinationPos;

        [SerializeField] float  Basic_timer;
        [SerializeField] float  Basic_time;

        ////////// Getter & Setter          //////////
        public GameObject Basic_VarObj  { get{ return Basic_obj;    }   }

        public bool Basic_IsReady   { get{ return !Basic_obj.activeSelf;    }   }

        ////////// Method                   //////////
        // Basic_Launch
        public void Basic_Launch(
            Giggle_Unit _owner, Giggle_Unit _target,
            TYPE _type, int _damage)
        {
            Basic_owner  = _owner;
            Basic_target = _target;

            Basic_type = _type;
            // 데미지
            Basic_Launch__Damage(_damage);

            Basic_startPos       = Basic_owner.transform.Find("Target").Find("0").position;
            Basic_destinationPos = Basic_target.transform.Find("Target").Find("0").position;
            //Basic_startPos       = Basic_owner.transform.position;
            //Basic_destinationPos = Basic_target.transform.position;

            Basic_timer = 0.0f;
            float distance = Vector3.Distance(Basic_startPos, Basic_destinationPos);
            Basic_time = distance / 10.0f;

            // Bullet
            for(int for0 = 0; for0 < Basic_obj.transform.childCount; for0++)
            {
                Basic_obj.transform.GetChild(for0).gameObject.SetActive(false);
            }
            string bulletName = "";
            switch(Basic_type)
            {
                case TYPE.NORMAL:
                case TYPE.FIRE_BALL:
                    { bulletName = "0"; }   break;
            }
            Basic_obj.transform.Find(bulletName).gameObject.SetActive(true);
            //
            Basic_obj.transform.position = Basic_startPos;
            Basic_obj.SetActive(true);
        }

        void Basic_Launch__Damage(int _damage)
        {
            // 속성에 따른 데미지도 계산한다.
            bool isAdd = false;

            Giggle_Master.ATTRIBUTE ownerAttribute  = Basic_owner.Status_VarDatabase.Basic_VarAttribute;
            Giggle_Master.ATTRIBUTE targetAttribute = Basic_target.Status_VarDatabase.Basic_VarAttribute;
            
            switch (ownerAttribute)
            {
                case Giggle_Master.ATTRIBUTE.FIRE:  { if (targetAttribute.Equals(Giggle_Master.ATTRIBUTE.EARTH) ) { isAdd = true;   }   }   break;
                case Giggle_Master.ATTRIBUTE.EARTH: { if (targetAttribute.Equals(Giggle_Master.ATTRIBUTE.WIND)  ) { isAdd = true;   }   }   break;
                case Giggle_Master.ATTRIBUTE.WIND:  { if (targetAttribute.Equals(Giggle_Master.ATTRIBUTE.WATER) ) { isAdd = true;   }   }   break;
                case Giggle_Master.ATTRIBUTE.WATER: { if (targetAttribute.Equals(Giggle_Master.ATTRIBUTE.FIRE)  ) { isAdd = true;   }   }   break;
                
                case Giggle_Master.ATTRIBUTE.LIGHT: { if (targetAttribute.Equals(Giggle_Master.ATTRIBUTE.DARK)  ) { isAdd = true;   }   }   break;
                case Giggle_Master.ATTRIBUTE.DARK:  { if (targetAttribute.Equals(Giggle_Master.ATTRIBUTE.LIGHT) ) { isAdd = true;   }   }   break;
            }

            if (isAdd)
            {
                float calc = (float)_damage;
                calc *= 1.25f;
                Basic_damage = (int)calc;
            }
            else
            {
                Basic_damage = _damage;
            }
        }

        // Basic_Coroutine
        public void Basic_Coroutine(Giggle_Battle _manager, float _deltaTime)
        {
            if (Basic_obj.activeSelf)
            {
                // 시간 진행
                Basic_timer += _deltaTime;

                if (Basic_timer >= Basic_time)
                {
                    Basic_timer = Basic_time;

                    switch (Basic_type)
                    {
                        case TYPE.NORMAL: { Basic_Coroutine__Arrive_NORMAL(); } break;
                        case TYPE.FIRE_BALL: { Basic_Coroutine__Arrive_FIRE_BALL(_manager, Basic_destinationPos); } break;
                    }

                    // 비활성화
                    Basic_obj.SetActive(false);
                }

                switch (Basic_type)
                {
                    case TYPE.SPLIT: { } break;
                }

                Basic_obj.transform.position = Vector3.Lerp(Basic_startPos, Basic_destinationPos, Basic_timer / Basic_time);
            }
        }
        
        void Basic_Coroutine__Arrive_NORMAL()
        {
            if(Basic_target.gameObject.activeInHierarchy)
            {
                Basic_owner.Status_AttackSuccess(Basic_target);
                Basic_target.Status_Damage(Basic_damage);
            }
        }
        
        void Basic_Coroutine__Arrive_FIRE_BALL(Giggle_Battle _manager, Vector3 _pos)
        {
            Basic_Coroutine__Arrive_NORMAL();
            
            _manager.Effect_Play(Basic_type, _pos);
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

    void Bullet_PowerSaving()
    {
        bool isSleep = (bool)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MAIN__POWER_SAVING__GET_IS_ON);
        
        Bullet_parent.gameObject.SetActive(!isSleep);
    }

    //
    void Bullet_Coroutine(float _deltaTime)
    {
        for(int for0 = 0; for0 < Bullet_bullets.Count; for0++)
        {
            Bullet_bullets[for0].Basic_Coroutine(this, _deltaTime);
        }
    }

    public void Bullet_Launch(
        Giggle_Unit _owner, Giggle_Unit _target,
        Bullet.TYPE _type, int _damage)
    {
        bool isLaunch = false;

        int whileCount = 0;
        while(whileCount < Bullet_bullets.Count)
        {
            if(Bullet_bullets[whileCount].Basic_IsReady)
            {
                Bullet_bullets[whileCount].Basic_Launch(
                    _owner, _target,
                    _type, _damage);

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
                _type, _damage);
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

    #region EFFECT

    [Header("EFFECT ==================================================")]
    [SerializeField] Transform  Effect_parent;

    ////////// Getter & Setter          //////////

    ////////// Method                   //////////

    //
    public void Effect_Play(Bullet.TYPE _type, Vector3 _pos)
    {
        if(Effect_parent.gameObject.activeSelf)
        {
            bool isPlay = false;

            for(int for0 = 0; for0 < Effect_parent.childCount; for0++)
            {
                Transform trans0 = Effect_parent.GetChild(for0);

                if(!trans0.gameObject.activeSelf)
                {
                    for(int for1 = 0; for1 < trans0.childCount; for1++)
                    {
                        trans0.GetChild(for1).gameObject.SetActive(false);
                    }
                    // 이펙트 찾아서 활성화
                    Transform trans1 = trans0.Find(((int)_type).ToString());
                    for(int for1 = 0; for1 < trans1.childCount; for1++)
                    {
                        trans1.GetChild(for1).GetComponent<ParticleSystem>().Play();
                        trans1.GetChild(for1).gameObject.SetActive(true);
                    }
                    trans1.gameObject.SetActive(true);

                    // 이펙트 활성화
                    trans0.position = _pos;
                    trans0.gameObject.SetActive(true);

                    isPlay = true;

                    break;
                }
            }

            // 대기 중인 이펙트가 없다면 추가로 생성하고 재귀
            if(!isPlay)
            {
                for(int for0 = 0; for0 < 100; for0++)
                {
                    Transform element = GameObject.Instantiate<Transform>(Effect_parent.GetChild(for0), Effect_parent);
                    element.gameObject.SetActive(false);
                }

                Effect_Play(_type, _pos);
            }
        }
    }

    //
    void Effect_PowerSaving()
    {
        bool isSleep = (bool)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MAIN__POWER_SAVING__GET_IS_ON);

        Effect_parent.gameObject.SetActive(!isSleep);

        if(!isSleep)
        {
            for(int for0 = 0; for0 < Effect_parent.childCount; for0++)
            {
                Transform trans0 = Effect_parent.GetChild(for0);

                trans0.gameObject.SetActive(false);
            }
        }
    }

    //
    void Effect_Coroutine()
    {
        if(Effect_parent.gameObject.activeSelf)
        {
            for(int for0 = 0; for0 < Effect_parent.childCount; for0++)
            {
                Transform trans0 = Effect_parent.GetChild(for0);
                // 이펙트가 활성화되 있다면,
                // 실행여부를 체크하여, 재생이 끝나면 비활성화 시켜준다.
                if(trans0.gameObject.activeSelf)
                {
                    for(int for1 = 0; for1 < trans0.childCount; for1++)
                    {
                        Transform trans1 = trans0.GetChild(for1);
                        if(trans1.gameObject.activeSelf)
                        {
                            bool isPlay = false;

                            for(int for2 = 0; for2 < trans1.childCount; for2++)
                            {
                                if(trans1.GetChild(for2).gameObject.activeSelf)
                                {
                                    isPlay = true;
                                    break;
                                }
                            }

                            if(!isPlay)
                            {
                                trans0.gameObject.SetActive(false);
                            }
                        }
                    }
                }
            }
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
    protected void Map_Load(GameObject _obj)
    {
        while(Map_parent.childCount > 0)
        {
            Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(
                Giggle_ScriptBridge.EVENT.MASTER__GARBAGE__REMOVE,
                Map_parent.GetChild(0));
        }

        //
        GameObject obj = GameObject.Instantiate(_obj, Map_parent);
        Map_ground = obj.transform.Find("Ground").GetChild(0);

        Map_PowerSaving();
    }

    // Map_PowerSaving
    void Map_PowerSaving()
    {
        bool isSleep = (bool)Giggle_ScriptBridge.Basic_VarInstance.Basic_GetMethod(Giggle_ScriptBridge.EVENT.MAIN__POWER_SAVING__GET_IS_ON);

        Map_parent.gameObject.SetActive(!isSleep);
    }

    // Map_Coroutine
    void Map_Coroutine__ACTIVE_MOVE_READY()
    {
        Map_ground.localPosition = new Vector3(2, 0, 0);
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

    //[Header("UI ==================================================")]

    ////////// Getter & Setter          //////////

    ////////// Method                   //////////

    ////////// Constructor & Destroyer  //////////

    //
    void UI_Init()
    {
    }

    #endregion
}
