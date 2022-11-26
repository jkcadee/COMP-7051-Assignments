using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class PlayerConfigManager : MonoBehaviour
{
    private List<PlayerConfig> playerConfigs;
// shows how many players needed to start
// different if one player is an AI
    private int MaxPlayers = 2; 

    public static PlayerConfigManager Instance {get; set;}

    private void Awake(){
        if (Instance != null && this.playerConfigs.Count > 2) {
            Destroy(gameObject);
            Debug.Log("SINGLETON - Trying to make another instance!");
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
            playerConfigs = new List<PlayerConfig>();
        }
    }

    public List<PlayerConfig> GetPlayerConfigs(){
        return playerConfigs;
    }

    public void ClearPlayerConfigs(){
        Destroy(gameObject);
    }

    public void ReadyPlayer(int index){
        playerConfigs[index].IsReady = true;
        // if all players are ready
        if(playerConfigs.Count == MaxPlayers && playerConfigs.All(p => p.IsReady == true)) {
            SceneManager.LoadScene("Player_V_Player_controller");
        }
    }

    public void HandlePlayerJoin(PlayerInput pi){
        if (playerConfigs.Count > 2) {
            playerConfigs = new List<PlayerConfig>();
        }
        Debug.Log("Player Joined: " + pi.playerIndex);
        //check if any players aren't added
        if (!playerConfigs.Any(p => p.PlayerIndex == pi.playerIndex)){
            pi.transform.SetParent(transform);
            playerConfigs.Add(new PlayerConfig(pi));
        }
    }
}

public class PlayerConfig
{
    public PlayerConfig(PlayerInput pi) {
        PlayerIndex = pi.playerIndex;
        Input = pi;
        if (pi.playerIndex != 0) {
            pi.SwitchCurrentActionMap("Player2Movement");
        }
    }
    public PlayerInput Input { get; set;}
    public int PlayerIndex { get; set; }

    public bool isAI { get; set; }
    public bool IsReady { get; set;}
}
