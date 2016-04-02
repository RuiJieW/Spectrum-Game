using UnityEngine;
using System.Collections;

public class onPlatform : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		var platform = GameObject.FindWithTag("orange");



		var player = GameObject.FindGameObjectWithTag("Player");
		float XD = platform.transform.position.x - player.transform.position.x;
		float YD = platform.transform.position.y - player.transform.position.y;
		if (XD < 2 && XD > -2 && YD < 2 && YD > -2) {

		}
	}
}
