using UnityEngine;

//
using BackEnd;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using Unity.Services.Authentication;
using System.Collections.Generic;
using System;

public class Giggle_ServerManager : MonoBehaviour
{

    ////////// Getter & Setter  //////////

    ////////// Method           //////////

    ////////// Unity            //////////

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Network_Start();
    }

    // Update is called once per frame
    void Update()
    {
        Network_timer += Time.deltaTime;

        if (Network_timer >= 29.0f)
        {
            GuildBattle_LobbyAlive();

            Network_timer = 0.0f;
        }
    }

    #region Network

    [SerializeField] int    Network_initState;
    [SerializeField] float  Network_timer;

    ////////// Getter & Setter  //////////

    ////////// Method           //////////

    ////////// Unity            //////////

    // Network_Start
    //
    void Network_Start()
    {
        Network_initState = -1;

        //
        Backend.InitializeAsync(
            callback =>
            {
                if (callback.IsSuccess())
                {
                    Network_initState = 1;
                }
                else
                {
                    Network_initState = 0;
                }
            });

        Network_Start__LobbyAuthenticate();

        //
        GuildBattle_Start();
    }

    // 로비에 인증하기
    async void Network_Start__LobbyAuthenticate()
    {
        InitializationOptions initializationOptions = new InitializationOptions();
        initializationOptions.SetProfile(Backend.UserInDate);

        await UnityServices.InitializeAsync(initializationOptions);
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    #endregion

    #region GUILD_BATTLE

    [Serializable]
    public class GuildBattle_LobbyData
    {
        [SerializeField] string Basic_guildId;

        [SerializeField] List<Lobby> Basic_lobbys;

        ////////// Getter & Setter          //////////

        ////////// Method                   //////////
        public void Basic_Ready(string _guildId)
        {
            Basic_guildId = _guildId;
            Basic_Ready__CreateLobby(_guildId);
        }

        async void Basic_Ready__CreateLobby(string _guildId)
        {
            //CreateLobbyOptions options = new CreateLobbyOptions();
            //options.IsPrivate = false;
            //
            //int maxPlayers = 3;
            //Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(_lobbyName + "_0", maxPlayers, options);
            //Basic_lobbys.Add(lobby);
            //
            //maxPlayers = 11;
            //lobby = await LobbyService.Instance.CreateLobbyAsync(_lobbyName + "_1", maxPlayers, options);
            //Basic_lobbys.Add(lobby);
        }

        //
        public void Basic_Alive()
        {
            for (int for0 = 0; for0 < Basic_lobbys.Count; for0++)
            {
                //LobbyService.Instance.SendHeartbeatPingAsync(Basic_lobbys[for0].Id);
            }
        }

        ////////// Constructor & Destroyer  //////////

        public GuildBattle_LobbyData()
        {
            if (Basic_lobbys == null)
            {
                Basic_lobbys = new List<Lobby>();
            }
        }
    }

    [SerializeField] List<GuildBattle_LobbyData>    GuildBattle_lobbyes;

    ////////// Getter & Setter  //////////

    ////////// Method           //////////
    // GuildBattle_LobbySetting
    void GuildBattle_LobbySetting()
    {
        GuildBattle_lobbyes.Clear();

        //
        Backend.GameData.Get(
            "GUILD__BATTLE_0", new Where(),
            (callback) =>
            {
                if (!callback.IsSuccess())
                {
                    // TODO:팝업이 나온다면 갱신해주세요.
                    return;
                }

                GuildBattle_LobbySetting__Success(callback.FlattenRows());
            });
    }

    void GuildBattle_LobbySetting__Success(LitJson.JsonData _datas)
    {
        for (int for0 = 0; for0 < _datas.Count; for0++)
        {
            GuildBattle_LobbyData element = new GuildBattle_LobbyData();
        }
    }

    // GuildBattle_LobbyAlive
    void GuildBattle_LobbyAlive()
    {
        for (int for0 = 0; for0 < GuildBattle_lobbyes.Count; for0++)
        {
            GuildBattle_lobbyes[for0].Basic_Alive();
        }
    }

    ////////// Unity            //////////

    void GuildBattle_Start()
    {
        if (GuildBattle_lobbyes == null)
        {
            GuildBattle_lobbyes = new List<GuildBattle_LobbyData>();
        }
    }

    #endregion
}
