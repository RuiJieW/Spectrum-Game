using UnityEngine;
using System.Collections;

public class movPlatform3A : MonoBehaviour {
	public int distance;
	public bool waitTime;
	public int delay, direction;
	public IsoDirection ORIENTATION;
	public int speed = 100;

	// Use this for initialization
	void Start () {
		direction = 1;
		ORIENTATION = IsoDirection.Up;
	}

	// Update is called once per frame
	void Update () {
		MovePlatform();
	}

	void MovePlatform() {
		if (waitTime == false) {
			transform.Translate (Isometric.vectorToIsoDirection (ORIENTATION) * direction * Time.deltaTime * speed);
		}

		if (waitTime) {
			delay++;

			if (delay > 50) {
				delay = 0;
				waitTime = false;
			}
		}

		if (waitTime == false) {
			distance++;
		}

		if (distance >= 240) {
			distance = 0;
			direction *= -1;
			waitTime = true;
		}

	}
}
