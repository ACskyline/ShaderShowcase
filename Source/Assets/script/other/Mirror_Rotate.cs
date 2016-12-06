using UnityEngine;
using System.Collections;

public class Mirror_Rotate : MonoBehaviour {
    public float r_speed = 10;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.up, r_speed * Time.fixedDeltaTime);
	}
}
