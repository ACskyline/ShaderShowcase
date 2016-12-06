using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class Fog : MonoBehaviour
{
    [Range(0.0f, 3.0f)]
    public float fogDensity = 1.0f;

    public Color fogColor = Color.white;

    public float fogStart = 0.0f;
    public float fogEnd = 2.0f;

    public Shader shader;
    private Material material;
    private Camera hCamera;

    // Use this for initialization
    void OnEnable()
    {
        if (shader && shader.isSupported)
        {
            material = new Material(shader);
        }
        hCamera = gameObject.GetComponent<Camera>();
        hCamera.depthTextureMode |= DepthTextureMode.Depth;
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (material != null)
        {
            Matrix4x4 frustumConrners = Matrix4x4.identity;

            float fov = hCamera.fieldOfView;
            float near = hCamera.nearClipPlane;
            float aspect = hCamera.aspect;

            float halfHeight = near * Mathf.Tan(fov * 0.5f * Mathf.Deg2Rad);
            Vector3 toRight = hCamera.transform.right * halfHeight * aspect;
            Vector3 toTop = hCamera.transform.up * halfHeight;

            Vector3 topLeft = hCamera.transform.forward * near + toTop - toRight;
            topLeft = topLeft / near;
            //float scale = topLeft.magnitude / near;

            //topLeft.Normalize();
            //topLeft *= scale;

            Vector3 topRight = hCamera.transform.forward * near + toTop + toRight;
            topRight = topRight / near;
            //topRight.Normalize();
            //topRight *= scale;

            Vector3 bottomLeft = hCamera.transform.forward * near - toTop - toRight;
            bottomLeft = bottomLeft / near;
            //bottomLeft.Normalize();
            //bottomLeft *= scale;

            Vector3 bottomRight = hCamera.transform.forward * near - toTop + toRight;
            bottomRight = bottomRight / near;
            //bottomRight.Normalize();
            //bottomRight *= scale;

            frustumConrners.SetRow(0, bottomLeft);
            frustumConrners.SetRow(1, bottomRight);
            frustumConrners.SetRow(2, topRight);
            frustumConrners.SetRow(3, topLeft);

            material.SetMatrix("_FrustumCornersRay", frustumConrners);
            material.SetMatrix("_ViewProjectionInverseMatrix", (hCamera.projectionMatrix * hCamera.worldToCameraMatrix).inverse);
            material.SetFloat("_FogDensity", fogDensity);
            material.SetColor("_FogColor", fogColor);
            material.SetFloat("_FogStart", fogStart);
            material.SetFloat("_FogEnd", fogEnd);

            Graphics.Blit(src, dest, material);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }
}
