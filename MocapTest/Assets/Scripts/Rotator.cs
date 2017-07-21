using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {
	
	public float speed = 1.0f;
	private float rot = 0f;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.rotation = Quaternion.Euler(new Vector3(0, rot += Time.deltaTime * 15f * speed, 0));
	}
}
