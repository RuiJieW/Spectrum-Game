﻿using UnityEngine;
using System.Collections;

public class movPlatform2C : MonoBehaviour {
	public int distance1, distance2;
	public bool phase1, phase2;
	public bool isPathComplete, waitTime;
	public int delay, direction;
	public IsoDirection ORIENTATION;
	public int speed;

	// Use this for initialization
	void Start () {
		direction = 1;
		speed = 3;
		phase1 = true;
		ORIENTATION = IsoDirection.South;
	}
	
	// Update is called once per frame
	void Update () {
		MovePlatform ();
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

		if (isPathComplete == false) {
			if (phase1 == true) {
				if (waitTime == false) {
					distance1++;
				}

				if (distance1 >= 110) {
					ORIENTATION = IsoDirection.West;
					phase1 = false;
					phase2 = true;
				}
			} else if (phase2) {
				if (waitTime == false) {
					distance2++;
				}

				if (distance2 >= 28) {
					ORIENTATION = IsoDirection.East;

					isPathComplete = true;
					waitTime = true;
				}
			}
		} else {
			if (phase2 == true) {
				if (waitTime == false) {
					distance2--;
				}

				if (distance2 <= 0) {
					ORIENTATION = IsoDirection.North;

					phase2 = false;
					phase1 = true;
				}

			} else if (phase1) {
				if (waitTime == false) {
					distance1--;
				}

				if (distance1 <= 0) {
					ORIENTATION = IsoDirection.South;

					isPathComplete = false;
					waitTime = true;
				}
			}
		}

	}
}
