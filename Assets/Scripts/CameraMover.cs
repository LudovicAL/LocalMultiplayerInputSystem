using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour {
	[SerializeField]
	private Vector3 cameraOffset = new Vector3(0.0f, 0.0f, -20.0f);
	[SerializeField]
	private float smoothTime = 0.5f;
	[SerializeField]
	private float minZoom = 5.0f;
	[SerializeField]
	private float maxZoom = 20.0f;
	[SerializeField]
	private float zoomLimiter = 40.0f;

	private Vector3 velocity;
	private Camera cam;
	private Bounds bounds;

	void Start() {
		cam = this.GetComponentInChildren<Camera>();
	}

	void LateUpdate() {
		ComputeBounds();
		MoveCamera(bounds.center);
		ZoomCamera(Mathf.Max(bounds.size.x, bounds.size.y));
	}

	// Returns bounds encapsulating all player avatar gameobjects
	private void ComputeBounds() {
		bounds = new Bounds(PlayerConfigurationManager.Instance.GetPlayerConfigs()[0].avatar.transform.position, Vector3.zero);
		for (int i = 1, max = PlayerConfigurationManager.Instance.GetPlayerConfigs().Count; i < max; i++) {
			bounds.Encapsulate(PlayerConfigurationManager.Instance.GetPlayerConfigs()[i].avatar.transform.position);
		}
	}

	//Moves the camera
	private void MoveCamera(Vector3 targetPosition) {
		transform.position = Vector3.SmoothDamp(transform.position, targetPosition + cameraOffset, ref velocity, smoothTime);
	}

	//Zooms the camera
	private void ZoomCamera(float width) {
		float newZoom = Mathf.Lerp(minZoom, maxZoom, width / zoomLimiter);
		cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newZoom, Time.deltaTime);
	}
}
