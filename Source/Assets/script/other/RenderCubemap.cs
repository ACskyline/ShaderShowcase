using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class RenderCubemap : MonoBehaviour {
    public Cubemap cubeMap;
    private Camera cubeCamera;
	// Use this for initialization
	void Start () {
        cubeCamera = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        if (cubeCamera != null && cubeMap != null)
        {
            cubeCamera.RenderToCubemap(cubeMap);
        }
	}
}
