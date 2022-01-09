using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour {

	[SerializeField]
	private MeshRenderer playerMesh;
	private PlayerConfiguration playerConfiguration;
    private Mover mover;
	
    void Awake() {
        mover = this.GetComponent<Mover>();
    }

	private void Start() {
		
	}

	public void InitializePlayer(PlayerConfiguration playerConfiguration) {
        this.playerConfiguration = playerConfiguration;
		playerConfiguration.playerInput.onActionTriggered += Input_onActionTriggered;
		playerMesh.material = playerConfiguration.material;
    }   

    private void Input_onActionTriggered(CallbackContext obj) {
		OnMove(obj);
    }

    public void OnMove(CallbackContext context) {
		if (mover != null) {
			mover.SetInputVector(context.ReadValue<Vector2>());
		}        
    }
}
