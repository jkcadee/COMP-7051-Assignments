using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class GameplayMenu : MonoBehaviour
{
    public GameObject menu_card;
    // Start is called before the first frame update
    void Start()
    {
        Disable_Menu();
    }

    public void Enable_Menu() {
        menu_card.SetActive(true);
    }
    public void Disable_Menu() {
        menu_card.SetActive(false);
    }
}
