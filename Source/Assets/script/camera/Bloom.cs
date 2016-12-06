using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class Bloom : MonoBehaviour
{
    [Range(0, 4)]
    public int iterations = 3;

    [Range(0.2f, 3.0f)]
    public float blurSpread = 0.6f;

    [Range(1, 8)]
    public int downSample = 1;

    [Range(0.0f, 4.0f)]
    public float luminanceThreshold = 0.6f;

    public Shader shader;
    private Material material;
    // Use this for initialization
    void OnEnable()
    {
        if (shader && shader.isSupported)
        {
            material = new Material(shader);
        }
    }
    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (material != null)
        {
            material.SetFloat("_LuminanceThreshold", luminanceThreshold);
            int rtw = src.width / downSample;
            int rth = src.height / downSample;
            RenderTexture buffer = RenderTexture.GetTemporary(rtw, rth, 0);
            buffer.filterMode = FilterMode.Bilinear;

            Graphics.Blit(src, buffer, material, 0);//获取高光部分

            for (int i = 0; i < iterations; i++)
            {
                material.SetFloat("_BlurSize", 1.0f + i * blurSpread);
                RenderTexture bufferTemp = RenderTexture.GetTemporary(rtw, rth, 0);
                Graphics.Blit(buffer, bufferTemp, material, 1);//高斯模糊pass1
                Graphics.Blit(bufferTemp, buffer, material, 2);//高斯模糊pass2
                RenderTexture.ReleaseTemporary(bufferTemp);
            }
            material.SetTexture("_Bloom", buffer);

            Graphics.Blit(src, dest, material, 3);//混合

            RenderTexture.ReleaseTemporary(buffer);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }
}
