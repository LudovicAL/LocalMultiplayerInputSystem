using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class SpawnPlayerSetupMenu : MonoBehaviour {
	[SerializeField]
	private GameObject playerSetupMenuPrefab;
	private GameObject panelGridLayout;
	
	private void Awake() {
        panelGridLayout = GameObject.Find("Canvas").transform.Find("Panel Grid Layout").gameObject;
		panelGridLayout.transform.Find("Text Instruction").gameObject.SetActive(false);
		GameObject menu = Instantiate(playerSetupMenuPrefab, panelGridLayout.transform);
		PlayerInput playerInput = this.GetComponent<PlayerInput>();
		playerInput.uiInputModule = menu.GetComponentInChildren<InputSystemUIInputModule>();
		menu.GetComponent<PlayerSetupMenuController>().initialize(playerInput);
	}
}
