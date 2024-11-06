using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour {

	public Transform[] backgrounds;				// Array of all back- and foregrounds to be parallaxed
	private float[] parallaxScales; 			// Amount of camera movement to move background
	public float smoothing = 1; 				// Smoothness of parallax

	private Transform cam;						// Reference to main camera
	private Vector3 previousCamPos;				// Position of the camera in previous frame

	void Awake () {
		cam = Camera.main.transform;
	}

	// For initialization
	void Start () {
		previousCamPos = cam.position;

		parallaxScales = new float[backgrounds.Length];

		for (int i = 0; i < backgrounds.Length; i++) {
			parallaxScales [i] = backgrounds [i].position.z * -1; // Determins the "depth" of the scene
		}
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < backgrounds.Length; i++) {
			float parallax = (previousCamPos.x - cam.position.x) * parallaxScales [i]; // How much the background/foreground has to move

			float backgroundTargetPosX = backgrounds [i].position.x + parallax;

			Vector3 backgroundTargetPos = new Vector3 (backgroundTargetPosX, backgrounds [i].position.y, backgrounds [i].position.z);

			backgrounds [i].position = Vector3.Lerp (backgrounds [i].position, backgroundTargetPos, smoothing * Time.deltaTime); // Smoothes the parallaxing
		}

		previousCamPos = cam.position;
	}
}
