﻿using UnityEngine;
using System.Collections;

public class tilemovement : MonoBehaviour {
	public int distance1_A, distance2_A, distance3_A;
	public int distance1_B, distance2_B;
	public int distance1_C, distance2_C;
	public bool phase1A, phase2A, phase3A;
	public bool phase1B, phase2B;
	public bool phase1C, phase2C;
	public bool pathA, pathB, pathC;
	public bool waitTimeA, waitTimeB, waitTimeC;
	public int delayA, delayB, delayC;
	public int directionA, directionB, directionC;
	public IsoDirection ORIENTATION_A, ORIENTATION_B, ORIENTATION_C;

	// Use this for initialization
	void Start() {
		directionA = 1;
		phase1A = true; 
		ORIENTATION_A = IsoDirection.East;

		directionB = 1;
		phase1B = true;
		ORIENTATION_B = IsoDirection.Up;

		directionC = 1;
		phase1C = true;
		ORIENTATION_C = IsoDirection.South;
	}

	// Update is called once per frame
	void Update() {
		GameObject platformA = GameObject.Find("PlatformA-B");
		GameObject platformB = GameObject.Find("PlatformB-lift");
		GameObject platformC = GameObject.Find("PlatformB-C");

		MovePlatforms(platformA);
		MovePlatforms(platformB);
		MovePlatforms(platformC);
	}

	void MovePlatforms(GameObject platform) {
		Debug.Log ("Phase 1: " + distance1_A + " Phase 2: " + distance2_A + " Phase 3: " + distance3_A);

		if (platform.name == "PlatformA-B") {
			MovePlatform_A (platform);
		} else if (platform.name == "PlatformB-lift") {
			MovePlatform_B (platform);
		} else if (platform.name == "PlatformB-C") {
			MovePlatform_C (platform);
		}
	}

	void MovePlatform_A(GameObject platform) {

		if (waitTimeA == false) {
			platform.transform.Translate (Isometric.vectorToIsoDirection (ORIENTATION_A) * directionA * Time.deltaTime);
		}

		if (waitTimeA) {
			delayA++;

			if (delayA > 50) {
				delayA = 0;
				waitTimeA = false;
			}
		}

		// full path not complete, go through first run
		if (pathA == false) {
			// executing first phase (linear path)
			if (phase1A == true) {
				if (waitTimeA == false) {
					distance1_A++;
				}

				if (distance1_A >= 200) {
					ORIENTATION_A = IsoDirection.South;
					phase1A = false;
					phase2A = true;
				}

			} else if (phase2A) {
				distance2_A++;

				if (distance2_A >= 400) {
					ORIENTATION_A = IsoDirection.West;
					phase2A = false;
					phase3A = true;
				}
			} else if (phase3A) {
				if (waitTimeA == false) {
					distance3_A++;
				}

				if (distance3_A >= 125) {
					ORIENTATION_A = IsoDirection.East;

					// completed path, do reverse
					pathA = true;
					waitTimeA = true;
				}
			}
		} else {
			if (phase1A == true) {
				if (waitTimeA == false) {
					distance1_A--;
				}

				if (distance1_A <= 0) {
					ORIENTATION_A = IsoDirection.East;

					// completed path, start over
					pathA = false;
					waitTimeA = true;
				}

			} else if (phase2A) {
				distance2_A--;

				if (distance2_A <= 0) {
					ORIENTATION_A = IsoDirection.West;
					phase2A = false;
					phase1A = true;
				}
			} else if (phase3A) {
				if (waitTimeA == false) {
					distance3_A--;
				}

				if (distance3_A <= 0) {
					ORIENTATION_A = IsoDirection.North;
					phase3A = false;
					phase2A = true;
				}
			}
		}
	}

	void MovePlatform_B(GameObject platform) {
		if (waitTimeB == false) {
			platform.transform.Translate (Isometric.vectorToIsoDirection (ORIENTATION_B) * directionB * Time.deltaTime);
		}

		if (waitTimeB) {
			delayB++;

			if (delayB > 50) {
				delayB = 0;
				waitTimeB = false;
			}
		}

		if (waitTimeB == false) {
			distance1_B++;
		}

		if (distance1_B >= 50) {
			distance1_B = 0;
			directionB *= -1;
			waitTimeB = true;
		}
	}

	void MovePlatform_C(GameObject platform) {
		if (waitTimeC == false) {
			platform.transform.Translate (Isometric.vectorToIsoDirection (ORIENTATION_C) * directionC * Time.deltaTime);
		}

		if (waitTimeC) {
			delayC++;

			if (delayC > 50) {
				delayC = 0;
				waitTimeC = false;
			}
		}

		if (pathC == false) {
			if (phase1C == true) {
				if (waitTimeC == false) {
					distance1_C++;
				}

				if (distance1_C >= 30) {
					ORIENTATION_C = IsoDirection.West;
					phase1C = false;
					phase2C = true;
				}
			} else if (phase2C) {
				if (waitTimeC == false) {
					distance2_C++;
				}

				if (distance2_C >= 7) {
					ORIENTATION_C = IsoDirection.East;

					pathC = true;
					waitTimeC = true;
				}
			}
		} else {

		}
	}
}