using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSetupMenuController : MonoBehaviour
{
    private int playerIndex;
    [SerializeField]
    private TextMeshProUGUI titleText;
    [SerializeField]
    private GameObject readyPanel;
   
    [SerializeField]
    private Button readyButton;

    private float ignoreInputTime = 0.5f;
    private bool inputEnabled;

    public void SetPlayerIndex(int pi)
    {
        playerIndex = pi;
        titleText.SetText("Player " + (pi + 1).ToString());
        ignoreInputTime = Time.time + ignoreInputTime;
    }

    void Update(){
        if(Time.time > ignoreInputTime) {
            inputEnabled = true;
            
        }
    }

    void Start(){
        // readyPanel.SetActive(true);
        // readyButton.interactable = true;
        // readyButton.Select();
        // Debug.Log("SELECTED");
    }

    public void ReadyPlayer(){
        Debug.Log("READY");
        if(!inputEnabled){ return; }
        PlayerConfigManager.Instance.ReadyPlayer(playerIndex);
        readyButton.gameObject.SetActive(false);
    }
}
