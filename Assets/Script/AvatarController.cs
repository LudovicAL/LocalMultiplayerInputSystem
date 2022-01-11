using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class AvatarController : MonoBehaviour {

	[SerializeField]
	private float speed = 10f;
	private PlayerConfiguration playerConfiguration;
	private Vector2 direction;
	private bool moveToMouse = false;
	private CanvasInfoManager canvasInfoManager;

	private void Awake() {
		
	}

	// Start is called before the first frame update
	void Start() {
		canvasInfoManager = GameObject.Find("Canvas").GetComponent<CanvasInfoManager>();
	}

	// Update is called once per frame
	void Update() {
		Move();
	}

	//Called when a info action is triggered
	public void OnInfo(InputAction.CallbackContext context) {
		if (context.started) {
			canvasInfoManager.Show(true);
		} else if (context.canceled) {
			canvasInfoManager.Show(false);
		}
	}

	//Called when a move action is triggered
	public void OnMove(InputAction.CallbackContext context) {
		direction = context.ReadValue<Vector2>();
	}

	//Called when a mouse move action is triggered
	public void OnMouseMove(InputAction.CallbackContext context) {
		if (context.started) {
			moveToMouse = true;
		} else if (context.canceled) {
			moveToMouse = false;
			direction = Vector3.zero;
		}
	}

	//Moves the avatar
	private void Move() {
		if (moveToMouse) {
			direction = (Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - transform.position).normalized;
		}
		Vector2 tempVec2 = direction * speed * Time.deltaTime;
		transform.position += new Vector3(tempVec2.x, tempVec2.y);
	}

	//Sets the playerInput reference
	public void Initialize(PlayerConfiguration playerConfiguration) {
		Debug.Log("Initializing avatar for player " + (playerConfiguration.playerInput.playerIndex + 1) + " - " + playerConfiguration.playerInput.currentControlScheme);
		this.playerConfiguration = playerConfiguration;
		GetComponent<SpriteRenderer>().color = playerConfiguration.material.color;
		transform.Find("Text Title").GetComponent<TextMeshPro>().text = "Player " + (playerConfiguration.playerInput.playerIndex + 1) + " - " + playerConfiguration.playerInput.currentControlScheme;
		if (playerConfiguration.playerInput.currentControlScheme == "Mouse") {
			playerConfiguration.playerInput.actions.FindAction("MouseMove").started += OnMouseMove;
			playerConfiguration.playerInput.actions.FindAction("MouseMove").canceled += OnMouseMove;
		} else {
			playerConfiguration.playerInput.actions.FindAction("Move").started += OnMove;
			playerConfiguration.playerInput.actions.FindAction("Move").performed += OnMove;
			playerConfiguration.playerInput.actions.FindAction("Move").canceled += OnMove;
		}
		playerConfiguration.playerInput.actions.FindAction("Info").started += OnInfo;
		playerConfiguration.playerInput.actions.FindAction("Info").canceled += OnInfo;
	}
}
