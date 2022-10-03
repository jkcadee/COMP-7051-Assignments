using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
public class PlayerInputHandler : MonoBehaviour
{
    private PlayerConfig playerConfig;

    private InputActions controls;

    private Mover mover;

    private void Awake(){
        mover = GetComponent<Mover>();
        controls = new InputActions();
    }

    public void InitializePlayer(PlayerConfig pc) {
        playerConfig = pc;
        playerConfig.Input.onActionTriggered += Input_onActionTriggered;
    }

    private void Input_onActionTriggered(CallbackContext obj) {
        if (obj.action.name == controls.Player.Movement.name){
            OnMove(obj);
        }
    }

    public void OnMove(CallbackContext context)
    {
        if(mover != null)
            mover.SetInputVector(context.ReadValue<Vector2>());
    }
}
