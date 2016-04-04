using UnityEngine;
using System.Collections;

public class movPlatform2A : MonoBehaviour {
	public int distance1, distance2, distance3;
	public bool phase1, phase2, phase3;
	public bool isPathComplete, waitTime;
	public int delay, direction;
	public IsoDirection ORIENTATION;
	public int speed = 5;

	// Use this for initialization
	void Start () {
		direction = 1;
		phase1 = true;
		ORIENTATION = IsoDirection.East;
	}
	
	// Update is called once per frame
	void Update () {
		MovePlatform();
	}

	void MovePlatform() {

		GameObject platformA = GameObject.Find("PlatformA-B");
		var player = GameObject.FindGameObjectWithTag("Player");
		var offset = 3;
		var offset2 = 8;


		float XD = platform.transform.position.x - player.transform.position.x;
		float YD = platform.transform.position.y - player.transform.position.y;

		if (XD < offset && XD > -offset && YD < offset && YD > -offset) {
			if (waitTimeA == false) {
				player.GetComponent<Rigidbody> ().isKinematic = true;
				player.transform.Translate (Isometric.vectorToIsoDirection (ORIENTATION_A) * directionA * Time.deltaTime);
			}
		} else {
			if (waitTimeA == false) {
				if ((XD > offset2 || XD < -offset2) || (YD > offset2 || YD < -offset2)) {
					player.GetComponent<Rigidbody> ().isKinematic = false;
				}
			}
		}




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

		// full path not complete, go through first run
		if (isPathComplete == false) {
			// executing first phase (linear path)
			if (phase1 == true) {
				if (waitTime == false) {
					distance1++;
				}

				if (distance1 >= 100) {
					ORIENTATION = IsoDirection.South;
					phase1 = false;
					phase2 = true;
				}

			} else if (phase2) {
				distance2++;

				if (distance2 >= 200) {
					ORIENTATION = IsoDirection.West;
					phase2 = false;
					phase3 = true;
				}
			} else if (phase3) {
				if (waitTime == false) {
					distance3++;
				}

				if (distance3 >= 80) {
					ORIENTATION = IsoDirection.East;

					// completed path, do reverse
					isPathComplete = true;
					waitTime = true;
				}
			}
		} else {
			if (phase1 == true) {
				if (waitTime == false) {
					distance1--;
				}

				if (distance1 <= 0) {
					ORIENTATION = IsoDirection.East;

					// completed path, start over
					isPathComplete = false;
					waitTime = true;
				}

			} else if (phase2) {
				distance2--;

				if (distance2 <= 0) {
					ORIENTATION = IsoDirection.West;
					phase2 = false;
					phase1 = true;
				}
			} else if (phase3) {
				if (waitTime == false) {
					distance3--;
				}

				if (distance3 <= 0) {
					ORIENTATION = IsoDirection.North;
					phase3 = false;
					phase2 = true;
				}
			}
		}
	}
}
