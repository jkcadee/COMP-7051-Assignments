using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeLevel : MonoBehaviour
{
    [SerializeField]
    private Transform[] playerSpawns;
    [SerializeField]
    private GameObject playerPaddlePrefab;
    // Start is called before the first frame update
    void Start()
    {
        var playerConfigs = PlayerConfigManager.Instance.GetPlayerConfigs().ToArray();
        //spawn players on spawn points
        for (int i = 0; i < playerConfigs.Length; i++ ){
            var player = Instantiate(playerPaddlePrefab,playerSpawns[i].position,playerSpawns[i].rotation,gameObject.transform);
            player.GetComponent<PlayerInputHandler>().InitializePlayer(playerConfigs[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
