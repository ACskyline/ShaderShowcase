using UnityEngine;
using System.Collections;

public class Up_Down : MonoBehaviour {
    private float speed = 0;
    public float acceleration = 10;
    public float max_speed = 10;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
        speed += acceleration;
        if(speed>max_speed || speed < -max_speed)
        {
            acceleration = -acceleration;
        }
	}
}
