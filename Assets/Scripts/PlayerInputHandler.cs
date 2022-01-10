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
	private PlayerMover playerMover;
	private GameObject panelControlsDisplay;

	void Awake() {
		playerMover = this.GetComponent<PlayerMover>();
	}

	private void Start() {
		panelControlsDisplay = GameObject.Find("Canvas").transform.Find("Panel Controls Display").gameObject;
	}

	//Initializes the input handler
	public void InitializePlayer(PlayerConfiguration playerConfiguration) {
		this.playerConfiguration = playerConfiguration;
		playerMesh.material = playerConfiguration.material;
		playerConfiguration.playerInput.actions.FindAction("Move").performed += OnMove;
		playerConfiguration.playerInput.actions.FindAction("Move").canceled += OnMove;
		playerConfiguration.playerInput.actions.FindAction("Run").performed += OnRun;
		playerConfiguration.playerInput.actions.FindAction("Run").canceled += OnRun;
		playerConfiguration.playerInput.actions.FindAction("Controls").performed += OnControls;
		playerConfiguration.playerInput.actions.FindAction("Controls").canceled += OnControls;
	}

	//Called when a move command is received
	public void OnMove(CallbackContext context) {
		playerMover.SetInputVector(context.ReadValue<Vector2>());
		Debug.Log(Gamepad.current);
	}

	//Called when a run command is received
	public void OnRun(CallbackContext context) {
		playerMover.SetSpeed(context.ReadValueAsButton());
	}

	//Called when a controls display command is received
	public void OnControls(CallbackContext context) {
		//If the panel is already active
		if (panelControlsDisplay.activeSelf) {
			//And if there is still a device pressing the controls display button
			foreach (PlayerConfiguration pc in PlayerConfigurationManager.Instance.GetPlayerConfigs()) {
				if (pc.playerInput.actions.FindAction(context.action.name).IsPressed()) {
					return;
				}
			}
		}
		panelControlsDisplay.SetActive(!panelControlsDisplay.activeSelf);
	}
}