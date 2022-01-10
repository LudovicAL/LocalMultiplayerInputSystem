using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour {
    [SerializeField]
    private float walkSpeed = 5f;
	[SerializeField]
	private float runSpeed = 12f;
	private float speed;
    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;
    private Vector2 inputVector = Vector2.zero;
	private float yAxisLocation;

	void Awake() {
        controller = GetComponent<CharacterController>();
    }

	private void Start() {
		SetSpeed(false);
		yAxisLocation = this.GetComponent<MeshRenderer>().bounds.size.y / 2f;
	}

	void Update() {
        moveDirection = new Vector3(inputVector.x, 0, inputVector.y);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;
		if (transform.position.y != yAxisLocation) {
			moveDirection.y = yAxisLocation - transform.position.y;
		}
		controller.Move(moveDirection * Time.deltaTime);
	}

	public void SetInputVector(Vector2 direction) {
		inputVector = direction;
	}

	public void SetSpeed(bool isRunning) {
		speed = isRunning ? runSpeed : walkSpeed;
	}
}
