using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerSetupMenuController : MonoBehaviour {

	public PlayerInput playerInput { get; private set; }

	[SerializeField]
	private AudioClip joinClip;
	[SerializeField]
	private AudioClip colorClip;
	[SerializeField]
	private AudioClip readyClip;
	private TextMeshProUGUI textTitle;
	private GameObject panelColor;
	private GameObject panelReadyUp;
	private Button buttonReady;
	private GameObject panelIsReady;
	private AudioSource audioSource;

	private void Awake() {
		textTitle = transform.Find("Text Title").GetComponent<TextMeshProUGUI>();
		panelColor = transform.Find("Panel Color").gameObject;
		panelReadyUp = transform.Find("Panel ReadyUp").gameObject;
		buttonReady = transform.Find("Panel ReadyUp").Find("Button ReadyUp").GetComponent<Button>();
		panelIsReady = transform.Find("Panel IsReady").gameObject;
		audioSource = this.GetComponent<AudioSource>();
	}

	// Start is called before the first frame update
	void Start() {
		audioSource.clip = joinClip;
		audioSource.Play();
	}

    // Update is called once per frame
    void Update() {

	}

	//Assigns an index number to the player
	public void initialize(PlayerInput pi) {
		playerInput = pi;
		textTitle.SetText("Player " + (pi.playerIndex + 1).ToString());
	}

	//Assigns a material to the player
	public void SelectColor(Material mat) {
		audioSource.clip = colorClip;
		audioSource.Play();
		this.GetComponent<Image>().color = mat.color;
		PlayerConfigurationManager.Instance.SetPlayerColor(playerInput.playerIndex, mat);
		panelReadyUp.SetActive(true);
		buttonReady.interactable = true;
		panelColor.SetActive(false);
		buttonReady.Select();
    }

	//Marks the player as ready
    public void ReadyPlayer() {
		audioSource.clip = readyClip;
		audioSource.Play();
		PlayerConfigurationManager.Instance.ReadyPlayer(playerInput.playerIndex);
		panelReadyUp.SetActive(false);
		panelIsReady.SetActive(true);

	}
}
