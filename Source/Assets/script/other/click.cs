using UnityEngine;
using System.Collections;

public class click : MonoBehaviour {
    public float r_speed = 500;
    public float acceleration = 0.005f;
    private float first_acceleration;
    public Material m;
    private float amount = 0;
	// Use this for initialization
	void Start () {
        first_acceleration = acceleration;
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetMouseButton(0))
        {
            if(m!=null)
            {
                amount += acceleration * Time.deltaTime;
                transform.Rotate(Vector3.up, amount * r_speed * Time.deltaTime);
                amount = Mathf.Clamp(amount, 0, 1);
                m.SetFloat("_BurnAmount", amount); 
            }
           
        }
        else if (Input.GetMouseButton(1))
        {
            if (m != null)
            {
                amount -= acceleration * Time.deltaTime;
                transform.Rotate(Vector3.up, (amount - 1) * r_speed * Time.deltaTime);
                amount = Mathf.Clamp(amount, 0, 1);
                m.SetFloat("_BurnAmount", amount);
               
            }

        }
        else
        {
            acceleration = first_acceleration;
        }
        
	}
}
