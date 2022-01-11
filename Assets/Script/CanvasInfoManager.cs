using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CanvasInfoManager : MonoBehaviour {

	public GameObject panelControlListPrefab;
	public GameObject panelControlPrefab;

	private GameObject panelBackground;

	private void Awake() {
		panelBackground = transform.Find("Panel Background").gameObject;
	}

	// Start is called before the first frame update
	void Start() {
        
    }

    // Update is called once per frame
    void Update() {

	}

	//Creates a player's controls info panel
	public void CreatePanelControl(PlayerConfiguration playerConfiguration) {
		Debug.Log("Creating panel for player " + (playerConfiguration.playerInput.playerIndex + 1) + " - " + playerConfiguration.playerInput.currentControlScheme);
		GameObject panelControlList = Instantiate(panelControlListPrefab, panelBackground.transform.Find("Panel Horizontallayout"));
		panelControlList.GetComponent<Image>().color = playerConfiguration.material.color;
		panelControlList.transform.Find("Text Title").GetComponent<TextMeshProUGUI>().text = "Player " + (playerConfiguration.playerInput.playerIndex + 1) + " - " + playerConfiguration.playerInput.currentControlScheme;
		//Adds every controls of the player in the vertical layout group
		Transform verticalLayoutPanel = panelControlList.transform.Find("Panel Verticallayout");
		foreach (InputAction inputAction in playerConfiguration.playerInput.currentActionMap) {
			//Find the binding
			string button = GetActionBindingName(inputAction, playerConfiguration);
			//Create the panel only if a binding has been found
			if (button.Length > 0) {
				GameObject playerControlContainer = Instantiate(panelControlPrefab, verticalLayoutPanel);
				playerControlContainer.transform.Find("Text Name").gameObject.GetComponent<TextMeshProUGUI>().text = inputAction.name;
				playerControlContainer.transform.Find("Text Button").gameObject.GetComponent<TextMeshProUGUI>().text = button;
			}
		}
	}

	//Shows or hide the info panel
	public void Show(bool value) {
		if (!value && panelBackground.activeSelf) {
			//Returns if there is still a device pressing the info button
			foreach (PlayerConfiguration pc in PlayerConfigurationManager.Instance.GetPlayerConfiguationsList()) {
				if (pc.playerInput.actions.FindAction("Info").IsPressed()) {
					return;
				}
			}
		}
		panelBackground.SetActive(value);
	}

	private string GetActionBindingName(InputAction inputAction, PlayerConfiguration playerConfiguration) {
		if (inputAction.GetBindingDisplayString() != null && inputAction.GetBindingDisplayString() != "") {
			return inputAction.GetBindingDisplayString();
		} else {
			int bindingIndex = inputAction.GetBindingIndex(group: playerConfiguration.playerInput.currentControlScheme) - 1;
			if (bindingIndex >= 0) {
				if (inputAction.bindings[bindingIndex].isComposite) {
					return inputAction.bindings[bindingIndex].name;
				}
				return inputAction.GetBindingDisplayString(bindingIndex);
			}
		}
		Debug.LogWarning("The binding for the action " + inputAction.name + " could not be found.");
		return "";
	}
}
