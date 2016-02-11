using UnityEngine;
using System.Collections;

public class Visibility : MonoBehaviour {
    public bool Visib = false;
    public Renderer rend;
	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
        rend.enabled = false;
     // SpriteRenderer.Sprite = None;
	}
	
	// Update is called once per frame
	void Update () {
        //GetComponent(MeshRenderer).enabled = Visib;
    }
}
