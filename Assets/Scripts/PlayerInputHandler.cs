using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour {

	[SerializeField]
	private MeshRenderer playerMesh;
	private PlayerConfiguration playerConfig;
    private Mover mover;
	
    private void Awake() {
        mover = this.GetComponent<Mover>();
    }

    public void InitializePlayer(PlayerConfiguration config) {
        playerConfig = config;
        config.Input.onActionTriggered += Input_onActionTriggered;
		playerMesh.material = config.color;
    }   

    private void Input_onActionTriggered(CallbackContext obj) {
        //if (obj.action.name == playerConfig.Input.) {
            OnMove(obj);
        //}
    }

    public void OnMove(CallbackContext context) {
        if(mover != null)
            mover.SetInputVector(context.ReadValue<Vector2>());
    }
}
