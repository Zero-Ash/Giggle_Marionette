using UnityEngine;

//
using System.Collections.Generic;

public class Giggle_ScriptBridge
{
    public enum EVENT
    {
        #region Giggle_Master

        /// <summary>
        /// 코루틴 시작<br/>
        /// 매개변수 : IEnumerator - 시작하고자 하는 코루틴<br/>
        /// return type : 
        /// </summary>
        MASTER__BASIC__START_COROUTINE,

        /// <summary>
        /// 코루틴 중지<br/>
        /// 매개변수 : IEnumerator - 시작하고자 하는 코루틴<br/>
        /// return type : 
        /// </summary>
        MASTER__BASIC__STOP_COROUTINE,

        /// <summary>
        /// 네트워크 초기화 여부<br/>
        /// 매개변수 : <br/>
        /// return type : int - 네트워크 초기화 여부(-1 : 진행중, 0 : 실패, 1: 성공)
        /// </summary>
        MASTER__NETWORK__VAR_INIT_STATE,

        /// <summary>
        /// 씬 전환<br/>
        /// 매개변수 : Giggle_Master.Scene_TYPE - 씬 변수<br/>
        /// return type : 
        /// </summary>
        SCENE__LOAD_SCENE,

        /// <summary>
        /// 코루틴 시작<br/>
        /// 매개변수 : Transform - 버리고자 하는 오브젝트<br/>
        /// return type : 
        /// </summary>
        MASTER__GARBAGE__REMOVE,

        /// <summary>
        /// UI 세이프 에리어 크기<br/>
        /// 매개변수 : <br/>
        /// return type : Giggle_Unit
        /// </summary>
        MASTER__UI__SAFE_AREA_VAR_SIZE_DELTA,

        /// <summary>
        /// UI 세이프 에리어 위치<br/>
        /// 매개변수 : <br/>
        /// return type : Giggle_Unit
        /// </summary>
        MASTER__UI__SAFE_AREA_VAR_POSITION,

        /// <summary>
        /// UI용 캐릭터 오브젝트 생성<br/>
        /// 매개변수 : int - 캐릭터 데이터 id, Transform - 오브젝트의 부모, float - 각도, float - 크기<br/>
        /// return type : Giggle_Unit
        /// </summary>
        MASTER__UI__PINOCCHIO_INSTANTIATE,

        /// <summary>
        /// UI용 캐릭터 오브젝트 생성<br/>
        /// 매개변수 : int - 캐릭터 데이터 id, Transform - 오브젝트의 부모, float - 각도, float - 크기<br/>
        /// return type : Giggle_Unit
        /// </summary>
        MASTER__UI__CHARACTER_INSTANTIATE,

        /// <summary>
        /// Canvas 세팅<br/>
        /// 매개변수 : Canvas - 대상<br/>
        /// return type :
        /// </summary>
        MASTER__UI__CANVAS_SETTING,

        /// <summary>
        /// UI용 캐릭터 오브젝트 생성<br/>
        /// 매개변수 : int - 캐릭터 데이터 id, Transform - 오브젝트의 부모, float - 각도, float - 크기<br/>
        /// return type : Giggle_Unit
        /// </summary>
        MASTER__UI__VALUE_TEXT,

        /// <summary>
        /// UI RawImage<br/>
        /// 매개변수 : Transform - 부모<br/>
        /// return type :
        /// </summary>
        MASTER__UI__RAW_IMAGE,

        /// <summary>
        /// 전투 진행 변경<br/>
        /// 매개변수 : Giggle_Battle.Basic__COROUTINE_PHASE - 진행시키고자 하는 단계<br/>
        /// return type : Giggle_Battle.Basic__COROUTINE_PHASE
        /// </summary>
        MASTER__BATTLE__VAR_COROUTINE_PHASE,

        /// <summary>
        /// 진행 속도 가속<br/>
        /// 매개변수 : <br/>
        /// return type : 
        /// </summary>
        MASTER__BATTLE__ACCEL,

        /// <summary>
        /// 절전 모드 실행<br/>
        /// 매개변수 : <br/>
        /// return type : 
        /// </summary>
        MASTER__BATTLE__SLEEP_ON,

        /// <summary>
        /// 절전 모드 종료<br/>
        /// 매개변수 : <br/>
        /// return type : 
        /// </summary>
        MASTER__BATTLE__SLEEP_OFF,

        /// <summary>
        /// 초기화<br/>
        /// 매개변수 : <br/>
        /// return type : 
        /// </summary>
        MASTER__BATTLE__INIT,

        #endregion

        #region Giggle_Database

        /// <summary>
        /// 캐릭터 스킬 배경<br/>
        /// 매개변수 : int - rank<br/>
        /// return type : Sprite
        /// </summary>
        DATABASE__CHARACTER__GET_SKILL_BACK_FROM_RANK,

        /// <summary>
        /// 피노키오 데이터의 개방 여부<br/>
        /// 매개변수<br/>
        /// return type : bool
        /// </summary>
        DATABASE__PINOCCHIO__GET_IS_OPEN,

        /// <summary>
        /// 피노키오 데이터<br/>
        /// 매개변수 : int - 캐릭터id<br/>
        /// return type : Giggle_Character.Database
        /// </summary>
        DATABASE__PINOCCHIO__GET_DATA_FROM_ID,

        /// <summary>
        /// 피노키오 데이터<br/>
        /// 매개변수 : int - 스킬id<br/>
        /// return type : Giggle_Character.Skill
        /// </summary>
        DATABASE__PINOCCHIO__GET_SKILL_FROM_ID,

        /// <summary>
        /// 특성 데이터 리스트<br/>
        /// 매개변수 : Giggle_Player.ATTRIBUTE_TYPE - type<br/>
        /// return type : List(Giggle_Character.Attribute)
        /// </summary>
        DATABASE__PINOCCHIO__GET_ATTRIBUTE,

        /// <summary>
        /// 특성 데이터<br/>
        /// 매개변수 : Giggle_Player.ATTRIBUTE_TYPE - type, int - 스킬id<br/>
        /// return type : Giggle_Character.Attribute
        /// </summary>
        DATABASE__PINOCCHIO__GET_ATTRIBUTE_FROM_ID,

        /// <summary>
        /// 재능 리스트<br/>
        /// 매개변수 : int - grade<br/>
        /// return type : Giggle_Character.Ability
        /// </summary>
        DATABASE__PINOCCHIO__GET_ABILITYS_FROM_GRADE,

        /// <summary>
        /// 등급 내 재능 무작위 리턴<br/>
        /// 매개변수 : int - grade<br/>
        /// return type : Giggle_Character.AbilityClass
        /// </summary>
        DATABASE__PINOCCHIO__GET_ABILITY_RANDOM_FROM_GRADE,

        /// <summary>
        /// 재능 가져오기<br/>
        /// 매개변수 : int - id, int - grade<br/>
        /// return type : Giggle_Character.AbilityClass
        /// </summary>
        DATABASE__PINOCCHIO__ABILITY_GET_ABILITY_FROM_ELEMENT,

        /// <summary>
        /// 재능 확률 리스트<br/>
        /// 매개변수 : int - level<br/>
        /// return type : Giggle_Character.Ability_Probability
        /// </summary>
        DATABASE__PINOCCHIO__ABILITY_GET_PROBABILITY_FROM_LEVEL,

        /// <summary>
        /// 재능 나무 아이콘<br/>
        /// 매개변수 : int - select<br/>
        /// return type : Sprite
        /// </summary>
        DATABASE__PINOCCHIO__ABILITY_GET_WOOD_ICON_FROM_SELECT,

        /// <summary>
        /// 유물 데이터<br/>
        /// 매개변수 : int - id<br/>
        /// return type : 
        /// </summary>
        DATABASE__PINOCCHIO__RELIC_VAR_DATA_FROM_ID,

        /// <summary>
        /// 마리오네트 데이터의 개방 여부<br/>
        /// 매개변수<br/>
        /// return type : bool
        /// </summary>
        DATABASE__MARIONETTE__GET_IS_OPEN,
        /// <summary>
        /// 마리오네트 데이터 리스트<br/>
        /// 매개변수 : <br/>
        /// return type : List(Giggle_Character.Database)
        /// </summary>
        DATABASE__MARIONETTE__GET_DATAS,
        /// <summary>
        /// 마리오네트 데이터 리스트<br/>
        /// 매개변수 : Giggle_Character.ATTRIBUTE - 캐릭터 속성(복수 기입 가능)<br/>
        /// return type : List(Giggle_Character.Database)
        /// </summary>
        DATABASE__MARIONETTE__GET_DATAS_FROM_ATTRIBUTE,
        /// <summary>
        /// 마리오네트 데이터 리스트<br/>
        /// 매개변수 : Giggle_Character.TYPE - 캐릭터 무기(복수 기입 가능)<br/>
        /// return type : List(Giggle_Character.Database)
        /// </summary>
        DATABASE__MARIONETTE__GET_DATAS_FROM_TYPE,
        /// <summary>
        /// 마리오네트 데이터<br/>
        /// 매개변수 : int - 캐릭터id<br/>
        /// return type : Giggle_Character.Database
        /// </summary>
        DATABASE__MARIONETTE__GET_DATA_FROM_ID,
        /// <summary>
        /// 마리오네트 스킬 데이터<br/>
        /// 매개변수<br/>
        /// return type : Giggle_Character.Skill
        /// </summary>
        DATABASE__MARIONETTE__GET_SKILL_FROM_ID,
        /// <summary>
        /// 마리오네트 별자리 데이터<br/>
        /// 매개변수 : int - id<br/>
        /// return type : Giggle_Item.Constellation
        /// </summary>
        DATABASE__MARIONETTE__GET_CONSTELLATION_FROM_ID,
        /// <summary>
        /// 마리오네트 별자리 별이미지<br/>
        /// 매개변수 : Giggle_Database.Marionette_Constellation_STARS - type<br/>
        /// return type : Sprite
        /// </summary>
        DATABASE__MARIONETTE__GET_CONSTELLATION_STAR_FROM_TYPE,
        /// <summary>
        /// 마리오네트 카드 데이터<br/>
        /// 매개변수 : int - id<br/>
        /// return type : Giggle_Item.Card
        /// </summary>
        DATABASE__MARIONETTE__GET_CARD_FROM_ID,

        /// <summary>
        /// 스테이지 데이터의 개방 여부<br/>
        /// 매개변수<br/>
        /// return type : bool
        /// </summary>
        DATABASE__STAGE__GET_IS_OPEN,

        /// <summary>
        /// 스테이지 데이터 불러오기<br/>
        /// 매개변수 : int - id<br/>
        /// return type : Giggle_Stage.Stage
        /// </summary>
        DATABASE__STAGE__GET_STAGE_FROM_ID,

        /// <summary>
        /// 스테이지 데이터 불러오기<br/>
        /// 매개변수 : int - id<br/>
        /// return type : GameObject
        /// </summary>
        DATABASE__STAGE__GET_OBJ_FROM_ID,

        /// <summary>
        /// 스테이지 데이터 불러오기<br/>
        /// 매개변수<br/>
        /// return type : GameObject
        /// </summary>
        DATABASE__STAGE__GET_OBJ_FROM_SAVE,

        /// <summary>
        /// 아이템 데이터의 개방 여부<br/>
        /// 매개변수<br/>
        /// return type : bool
        /// </summary>
        DATABASE__ITEM__GET_IS_OPEN,

        /// <summary>
        /// 아이템 데이터<br/>
        /// 매개변수 : int - 캐릭터id<br/>
        /// return type : Giggle_Database.Item_Data
        /// </summary>
        DATABASE__ITEM__GET_DATAS_FROM_TYPE,

        /// <summary>
        /// 아이템 데이터<br/>
        /// 매개변수 : int - 캐릭터id<br/>
        /// return type : Giggle_Item.List
        /// </summary>
        DATABASE__ITEM__GET_DATA_FROM_ID,

        /// <summary>
        /// 아이템 스프라이트 가져오기<br/>
        /// 매개변수 : Giggle_Item.TYPE - itemt type, int - item class<br/>
        /// return type : Giggle_Item.List
        /// </summary>
        DATABASE__ITEM__GET_SPRITE_FROM_VALUE,

        /// <summary>
        /// 퀘스트 데이터<br/>
        /// 매개변수 : int - id<br/>
        /// return type : Giggle_Quest.Database
        /// </summary>
        DATABASE__QUEST__GET_DATAS_FROM_TYPE,

        /// <summary>
        /// 퀘스트 데이터<br/>
        /// 매개변수 : int - id<br/>
        /// return type : Giggle_Quest.Database
        /// </summary>
        DATABASE__QUEST__GET_DATA_FROM_ID,

        /// <summary>
        /// 퀘스트 데이터의 개방 여부<br/>
        /// 매개변수<br/>
        /// return type : bool
        /// </summary>
        DATABASE__QUEST__GET_IS_OPEN,

        /// <summary>
        /// 퀘스트 설명<br/>
        /// 매개변수 : Giggle_Quest.completeCondition_TYPE - type<br/>
        /// return type : Giggle_Quest.Text
        /// </summary>
        DATABASE__QUEST__GET_TEXT,

        #endregion

        #region Giggle_Player

        /// <summary>
        /// 네트워크 데이터 불러오기 실행중 여부<br/>
        /// 매개변수 : <br/>
        /// return type : bool
        /// </summary>
        PLAYER__BASIC__IS_NETWORK_DATA_LOAD_DOING,

        /// <summary>
        /// 네트워크 데이터 불러오기<br/>
        /// 매개변수 : <br/>
        /// return type : 
        /// </summary>
        PLAYER__BASIC__NETWORK_DATA_LOAD,


        /// <summary>
        /// 선택된 스테이지 번호<br/>
        /// 매개변수 : <br/>
        /// return type : int
        /// </summary>
        PLAYER__STAGE__VAR_SELECT,

        /// <summary>
        /// 다음 스테이지 갱신 여부<br/>
        /// 매개변수 : <br/>
        /// return type : bool
        /// </summary>
        PLAYER__STAGE__VAR_IS_NEXT,

        /// <summary>
        /// 스테이지 속도 업 남은 시간<br/>
        /// 매개변수 : <br/>
        /// return type : float
        /// </summary>
        PLAYER__STAGE__VAR_SPEED_TIMER,

        /// <summary>
        /// 스테이지 속도<br/>
        /// 매개변수 : <br/>
        /// return type : float
        /// </summary>
        PLAYER__STAGE__VAR_SPEED,

        /// <summary>
        /// 다음 스테이지로<br/>
        /// 매개변수 : <br/>
        /// return type : 
        /// </summary>
        PLAYER__STAGE__NEXT,

        /// <summary>
        /// 속도업 개시<br/>
        /// 매개변수 : <br/>
        /// return type : 
        /// </summary>
        PLAYER__STAGE__ACCEL,

        /// <summary>
        /// 피노키오 데이터<br/>
        /// 매개변수 : <br/>
        /// return type : Giggle_Character.Save
        /// </summary>
        PLAYER__PINOCCHIO__VAR_DATA,

        /// <summary>
        /// 피노키오 보유 직업 리스트<br/>
        /// 매개변수 : <br/>
        /// return type : List(int)
        /// </summary>
        PLAYER__PINOCCHIO__VAR_JOBS,

        /// <summary>
        /// 피노키오 스킬 데이터<br/>
        /// 매개변수 : <br/>
        /// return type : List(Giggle_Player.Pinocchio_Skill)
        /// </summary>
        PLAYER__PINOCCHIO__VAR_SKILLS,

        /// <summary>
        /// 피노키오 스킬 데이터<br/>
        /// 매개변수 : int - id<br/>
        /// return type : Giggle_Player.Pinocchio_Skill
        /// </summary>
        PLAYER__PINOCCHIO__VAR_SKILL_FROM_ID,

        /// <summary>
        /// 피노키오 스킬 슬롯 데이터<br/>
        /// 매개변수 : <br/>
        /// return type : List(int)
        /// </summary>
        PLAYER__PINOCCHIO__SKILL_VAR_SKILL_SLOTS,

        /// <summary>
        /// 피노키오 스킬 슬롯 데이터<br/>
        /// 매개변수 : int - count, (params)int - id<br/>
        /// return type : int
        /// </summary>
        PLAYER__PINOCCHIO__SKILL_VAR_SKILL_SLOT_FROM_COUNT,

        /// <summary>
        /// 피노키오 장비 id<br/>
        /// 매개변수 : int - count<br/>
        /// return type : int
        /// </summary>
        PLAYER__PINOCCHIO__SKILL_VAR_SELECT_SKILL_SLOT,

        /// <summary>
        /// 피노키오 장비 id<br/>
        /// 매개변수 : int - count<br/>
        /// return type : int
        /// </summary>
        PLAYER__PINOCCHIO__EUIPMENT_VAR_SELECT_FROM_COUNT,

        /// <summary>
        /// 피노키오 장비 장착<br/>
        /// 매개변수 : int - id<br/>
        /// return type : 
        /// </summary>
        PLAYER__PINOCCHIO__EUIPMENT_SAVE_SELECT,

        /// <summary>
        /// 피노키오 장비 장착<br/>
        /// 매개변수 : string - 소켓 이름, int - 인벤토리 id<br/>
        /// return type : 
        /// </summary>
        PLAYER__PINOCCHIO__EUIPMENT_ITEM,

        /// <summary>
        /// 특성 가져오기<br/>
        /// 매개변수 : Giggle_Player.ATTRIBUTE_TYPE - type<br/>
        /// return type : Giggle_Player.Pinocchio_Skill
        /// </summary>
        PLAYER__PINOCCHIO__ATTRIBUTE_VAR_FROM_TYPE,

        /// <summary>
        /// 특성 가져오기<br/>
        /// 매개변수 : Giggle_Player.ATTRIBUTE_TYPE - type, int - id<br/>
        /// return type : Giggle_Player.Pinocchio_Skill
        /// </summary>
        PLAYER__PINOCCHIO__ATTRIBUTE_VAR_FROM_TYPE_AND_ID,

        /// <summary>
        /// 특성 레벨업<br/>
        /// 매개변수 : Giggle_Player.ATTRIBUTE_TYPE - type, int - id<br/>
        /// return type : 
        /// </summary>
        PLAYER__PINOCCHIO__ATTRIBUTE_LEVEL_UP,

        /// <summary>
        /// 플레이어 재능 가져오기<br/>
        /// 매개변수 : int - 카운트<br/>
        /// return type : Giggle_Player.Pinocchio_Ability
        /// </summary>
        PLAYER__PINOCCHIO__ABILITY_GET_ABILITY_FROM_COUNT,

        /// <summary>
        /// 플레이어 재능 갯수<br/>
        /// 매개변수 : <br/>
        /// return type : int
        /// </summary>
        PLAYER__PINOCCHIO__ABILITY_GET_ABILITYS_COUNT,

        /// <summary>
        /// 플레이어 재능 레벨<br/>
        /// 매개변수 : <br/>
        /// return type : int
        /// </summary>
        PLAYER__PINOCCHIO__ABILITY_GET_LEVEL,

        /// <summary>
        /// 플레이어 재능 변경<br/>
        /// 매개변수 : <br/>
        /// return type : 
        /// </summary>
        PLAYER__PINOCCHIO__ABILITY_CHANGE,

        /// <summary>
        /// 플레이어 재능 잠그기<br/>
        /// 매개변수 : int - count<br/>
        /// return type : 
        /// </summary>
        PLAYER__PINOCCHIO__ABILITY_LOCK,

        /// <summary>
        /// 목공소 데이터 가져오기<br/>
        /// 매개변수 : int - count<br/>
        /// return type : 
        /// </summary>
        PLAYER__PINOCCHIO__ABILITY_WOOD_VAR_DATA_FROM_COUNT,

        /// <summary>
        /// 목공소 작업 시작하기<br/>
        /// 매개변수 : int - count, int - select<br/>
        /// return type : 
        /// </summary>
        PLAYER__PINOCCHIO__ABILITY_WOOD_WORK,

        /// <summary>
        /// 목공소 마리오네트 배정<br/>
        /// 매개변수 : int - count, int - id<br/>
        /// return type : 
        /// </summary>
        PLAYER__PINOCCHIO__ABILITY_WOOD_WORKER,

        /// <summary>
        /// 목공소 작업 종료<br/>
        /// 매개변수 : int - count<br/>
        /// return type : 
        /// </summary>
        PLAYER__PINOCCHIO__ABILITY_WOOD_WORKER_END,

        /// <summary>
        /// 유물 리스트<br/>
        /// 매개변수 : <br/>
        /// return type : List(Giggle_Player.Pinocchio_Relic)
        /// </summary>
        PLAYER__PINOCCHIO__RELIC_VAR_RELICS,

        /// <summary>
        /// 유물 리스트<br/>
        /// 매개변수 : <br/>
        /// return type : List(Giggle_Player.Pinocchio_Relic)
        /// </summary>
        PLAYER__PINOCCHIO__RELIC_GET_RELIC_FROM_INVENTORY_ID,

        /// <summary>
        /// 유물 장착 리스트<br/>
        /// 매개변수 : <br/>
        /// return type : List(Giggle_Player.Pinocchio_RelicSlot)
        /// </summary>
        PLAYER__PINOCCHIO__RELIC_VAR_RELIC_SLOTS,

        /// <summary>
        /// 유물 색상 체크<br/>
        /// 매개변수 : int - 슬롯 번호, int - 슬롯 번호<br/>
        /// return type : 
        /// </summary>
        PLAYER__PINOCCHIO__RELIC_VAR_SLOTS_COLOR_IS_SAME,

        /// <summary>
        /// 유물 색상 체크<br/>
        /// 매개변수 : int - <br/>
        /// return type : bool
        /// </summary>
        PLAYER__PINOCCHIO__RELIC_VAR_RELIC_SLOT_BUFF_FROM_COUNT,

        /// <summary>
        /// 유물 변경<br/>
        /// 매개변수 : int - 인벤토리 번호, int - 슬롯 번호<br/>
        /// return type : 
        /// </summary>
        PLAYER__PINOCCHIO__RELIC_SLOT_CHANGE,

        /// <summary>
        /// 마리오네트 리스트 가져오기<br/>
        /// 매개변수 : <br/>
        /// return type : List(Giggle_Character.Save)
        /// </summary>
        PLAYER__MARIONETTE__GET_LIST,

        /// <summary>
        /// 마리오네트 데이터 가져오기<br/>
        /// 매개변수 : int - inventoryId <br/>
        /// return type : Giggle_Character.Save
        /// </summary>
        PLAYER__MARIONETTE__GET_DATA_FROM_INVENTORYID,

        /// <summary>
        /// 마리오네트 도감 가져오기<br/>
        /// 매개변수 : <br/>
        /// return type : List(int)
        /// </summary>
        PLAYER__MARIONETTE__GET_DOCUMENT,

        /// <summary>
        /// 마리오네트 추가<br/>
        /// 매개변수 : int - 캐릭터 id<br/>
        /// return type : 
        /// </summary>
        PLAYER__MARIONETTE__ADD,

        /// <summary>
        /// 마리오네트 카드 장비<br/>
        /// 매개변수 : int - 캐릭터 인벤토리 id, int - 슬롯, int - 카드 data id<br/>
        /// return type : 
        /// </summary>
        PLAYER__MARIONETTE__CARD_EQUIP,

        /// <summary>
        /// 마리오네트 리스트 가져오기<br/>
        /// 매개변수 : <br/>
        /// return type : int
        /// </summary>
        PLAYER__FORMATION__GET_SELECT,

        /// <summary>
        /// 마리오네트 리스트 가져오기<br/>
        /// 매개변수 : <br/>
        /// return type : List(Giggle_Player.Formation)
        /// </summary>
        PLAYER__FORMATION__GET_FORMATION_LIST,

        /// <summary>
        /// 선택한 진영 가져오기<br/>
        /// 매개변수 :<br/>
        /// return type : List(int)
        /// </summary>
        PLAYER__FORMATION__GET_SELECT_FORMATION,

        /// <summary>
        /// 진영 위치 변경<br/>
        /// 매개변수 : int - 마리오네트 인벤토르 id, int - 배치할 자리<br/>
        /// return type :
        /// </summary>
        PLAYER__FORMATION__FORMATION_SETTING,

        /// <summary>
        /// 아이템 리스트 가져오기<br/>
        /// 매개변수 : <br/>
        /// return type : List(Giggle_Item.Inventory)
        /// </summary>
        PLAYER__ITEM__GET_ITEM_LIST,

        /// <summary>
        /// 아이템 가져오기<br/>
        /// 매개변수 : int - id<br/>
        /// return type : Giggle_Item.Inventory
        /// </summary>
        PLAYER__ITEM__GET_ITEM_FROM_INVENTORY_ID,

        /// <summary>
        /// 카드 리스트 가져오기<br/>
        /// 매개변수 : <br/>
        /// return type : List(Giggle_Item.Inventory)
        /// </summary>
        PLAYER__ITEM__GET_CARD_LIST,

        /// <summary>
        /// 카드 리스트 가져오기<br/>
        /// 매개변수 : int - 카드 데이터 id<br/>
        /// return type : Giggle_Item.Inventory
        /// </summary>
        PLAYER__CARD__GET_DATA_FROM_DATA_ID,

        /// <summary>
        /// 아이템 제작.<br/>
        /// 매개변수 : int - 데이터 id, int - 획득 량<br/>
        /// return type : bool
        /// </summary>
        PLAYER__ITEM__CRAFTING,

        /// <summary>
        /// 아이템 획득.<br/>
        /// 매개변수 : int - 데이터 id, int - 획득 량<br/>
        /// return type : bool
        /// </summary>
        PLAYER__ITEM__LOOTING,

        /// <summary>
        /// 아이템 갯수 줄이기. 0이면 리스트에서 삭제<br/>
        /// 매개변수 : int - 데이터 id, int - 줄어들 갯수<br/>
        /// return type : bool
        /// </summary>
        PLAYER__ITEM__DIS_COUNT,

        /// <summary>
        /// 선택한 진영 가져오기<br/>
        /// 매개변수 :<br/>
        /// return type : List(int)
        /// </summary>
        PLAYER__DUNGEON__GET_FORMATION,

        /// <summary>
        /// 진영 위치 변경<br/>
        /// 매개변수 : int - 마리오네트 인벤토르 id, int - 배치할 자리<br/>
        /// return type :
        /// </summary>
        PLAYER__DUNGEON__FORMATION_SETTING,

        #endregion

        #region MAIN

        /// <summary>
        /// UI 페이즈<br/>
        /// 매개변수 : <br/>
        /// return type : int
        /// </summary>
        MAIN__UI__VAR_COROUTINE_PHASE,

        /// <summary>
        /// 절전 모드 가동 여부<br/>
        /// 매개변수 : <br/>
        /// return type : bool
        /// </summary>
        MAIN__POWER_SAVING__GET_IS_ON,

        /// <summary>
        /// 절전 모드 가동<br/>
        /// 매개변수 : bool<br/>
        /// return type : 
        /// </summary>
        MAIN__POWER_SAVING__ON_OFF,

        /// <summary>
        /// 페이브 오브젝트<br/>
        /// 매개변수 : <br/>
        /// return type : Image
        /// </summary>
        MAIN__BATTLE__VAR_FADE,

        /// <summary>
        /// 승리UI 출력<br/>
        /// 매개변수 : bool<br/>
        /// return type : bool
        /// </summary>
        MAIN__BATTLE__VAR_WIN_ACTIVE,

        /// <summary>
        /// 패배UI 출력<br/>
        /// 매개변수 : bool<br/>
        /// return type : bool
        /// </summary>
        MAIN__BATTLE__VAR_LOSE_ACTIVE,

        /// <summary>
        /// 절전 모드 가동 여부<br/>
        /// 매개변수 : <br/>
        /// return type : 
        /// </summary>
        MAIN__BATTLE__FADE_OUT,

        /// <summary>
        /// 절전 모드 가동 여부<br/>
        /// 매개변수 : <br/>
        /// return type : 
        /// </summary>
        MAIN__BATTLE__FADE_IN,

        /// <summary>
        /// 절전 모드 가동 여부<br/>
        /// 매개변수 : <br/>
        /// return type : 
        /// </summary>
        MAIN__BATTLE__LOSE


        #endregion
    }

    #region BASIC
    public delegate object Basic_Method(params object[] _args);

    static Giggle_ScriptBridge Basic_instance;

    Dictionary<EVENT, Basic_Method> Basic_methods;

    ////////// Getter & Setter          //////////
    public static Giggle_ScriptBridge Basic_VarInstance
    {
        get
        {
            if (Basic_instance == null)
            {
                Basic_instance = new Giggle_ScriptBridge();
            }

            return Basic_instance;
        }
    }

    ////////// Method                   //////////
    public object   Basic_GetMethod(EVENT _event, params object[] _args)
    {
        return Basic_methods[_event](_args);
    }

    public void     Basic_SetMethod(EVENT _event, Basic_Method _method)
    {
        if(Basic_methods.ContainsKey(_event))
        {
            Basic_methods[_event] = _method;
        }
        else
        {
            Basic_methods.Add(_event, _method);
        }
    }

    //
    public bool Basic_GetIsInMethod(EVENT _event)
    {
        return Basic_methods.ContainsKey(_event);
    }


    ////////// Constructor & Destroyer  //////////
    public Giggle_ScriptBridge()
    {
        if(Basic_methods == null)
        {
            Basic_methods = new Dictionary<EVENT, Basic_Method>();
        }
    }

    #endregion
}
