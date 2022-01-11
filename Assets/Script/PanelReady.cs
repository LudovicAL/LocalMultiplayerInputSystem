using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class PanelReady : MonoBehaviour {

	private PlayerConfiguration playerConfiguration;

	// Start is called before the first frame update
	void Start() {
		transform.localScale = Vector3.one;
	}

    // Update is called once per frame
    void Update() {
        
    }

	//Initializes the content of the prefab
	public void Initialize(PlayerConfiguration playerConfiguration) {
		this.playerConfiguration = playerConfiguration;
		//GetComponent<InputSystemUIInputModule>().actionsAsset = playerConfiguration.playerInput.currentActionMap.asset;
		playerConfiguration.playerInput.uiInputModule = GetComponent<InputSystemUIInputModule>();
		//playerConfiguration.playerInput.uiInputModule.ActivateModule();
		transform.GetComponent<Image>().color = playerConfiguration.material.color;
		transform.Find("Text Title").GetComponent<TextMeshProUGUI>().text = "Player " + (playerConfiguration.playerInput.playerIndex + 1) + " - " + playerConfiguration.playerInput.currentControlScheme;
	}

	//Called when a ready button is triggered
	public void OnReady() {
		transform.Find("Button Ready").gameObject.SetActive(false);
		transform.Find("Text IsReady").gameObject.SetActive(true);
		PlayerConfigurationManager.Instance.PlayerIsReady(playerConfiguration);
	}
}
