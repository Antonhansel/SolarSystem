using UnityEngine;
using System.Collections;

public class rotate : MonoBehaviour {
	// Use this for initialization
	void Start () {
		// Cachhe the transform link
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0,20*Time.deltaTime,0);
	}
}
