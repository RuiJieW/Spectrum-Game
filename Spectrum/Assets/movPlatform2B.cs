using UnityEngine;
using System.Collections;

public class movPlatform2B : MonoBehaviour {
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

        GameObject platformA = GameObject.Find("PlatformA-B");
        var player = GameObject.FindGameObjectWithTag("Player");
        var offset = 3;
        var offset2 = 8;


        float XD = transform.position.x - player.transform.position.x;
        float YD = transform.position.y - player.transform.position.y;

        if (XD < offset && XD > -offset && YD < offset && YD > -offset)
        {
            if (waitTime == false)
            {
                player.GetComponent<Rigidbody>().isKinematic = true;
                player.transform.Translate(Isometric.vectorToIsoDirection(ORIENTATION) * direction * Time.deltaTime * speed);
            }
        }
        else {
            if (waitTime == false)
            {
                if ((XD > offset2 || XD < -offset2) || (YD > offset2 || YD < -offset2))
                {
                    player.GetComponent<Rigidbody>().isKinematic = false;
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
