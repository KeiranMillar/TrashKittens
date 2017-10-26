using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchToZoom : MonoBehaviour 
{
	public float perspectiveZoomSpeed = 0.5f;        // The rate of change of the field of view in perspective mode.
	public float orthoZoomSpeed = 0.5f;        // The rate of change of the orthographic size in orthographic mode.
	Camera camera;

	// Update is called once per frame
	void Update ()
	{
		// If there are two touches on the screen
		if (Input.touchCount == 2) {
			// Store information from two touches
			Touch touchZero = Input.GetTouch (0);
			Touch touchOne = Input.GetTouch (1);

			// Find the position of the touches in the previous frame

			Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

			// Find the magnitude of the distance between fingers each frame.
			float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
			float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

			// Find the difference in the touches between frames
			float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

			if (camera.orthographic) {
				// Change the size based on the change in distance between the touches
				camera.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;

				// Make sure there is never a sie less than 0
				camera.orthographicSize = Mathf.Max (camera.orthographicSize, 0.1f);
			} else {
				// Otherwise change the field of view based on the change in distance between the touches.
				camera.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;

				// Clamp the field of view to make sure it's between 0 and 180.
				camera.fieldOfView = Mathf.Clamp (camera.fieldOfView, 0.1f, 179.9f);
			}
		}
	}
}
