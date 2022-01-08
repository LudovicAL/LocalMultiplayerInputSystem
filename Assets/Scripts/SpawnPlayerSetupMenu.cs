using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class SpawnPlayerSetupMenu : MonoBehaviour {
    public GameObject playerSetupMenuPrefab;
	public PlayerInput input;

	private GameObject rootMenu;

    private void Awake() {
        rootMenu = GameObject.Find("Canvas");
        if(rootMenu != null) {
            GameObject menu = Instantiate(playerSetupMenuPrefab, rootMenu.transform);
            input.uiInputModule = menu.GetComponentInChildren<InputSystemUIInputModule>();
            menu.GetComponent<PlayerSetupMenuController>().setPlayerIndex(input.playerIndex);
        }
    }
}
