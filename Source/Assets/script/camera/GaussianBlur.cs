using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class GaussianBlur : MonoBehaviour {
    [Range(0, 4)]
    public int iterations = 3;

    [Range(0.2f, 3.0f)]
    public float blurSpread = 0.6f;

    [Range(1,8)]
    public int downSample;

    public Shader shader;
    private Material material;
	// Use this for initialization
	void Start () {
	    if(shader&&shader.isSupported)
        {
            material = new Material(shader);
            material.hideFlags = HideFlags.DontSave;
        }
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnDestroy()
    {
        DestroyImmediate(material);
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (material != null)
        {
            int rtw = src.width/downSample;
            int rth = src.height/downSample;
            RenderTexture buffer = RenderTexture.GetTemporary(rtw, rth, 0);
            buffer.filterMode = FilterMode.Bilinear;

            Graphics.Blit(src, buffer);

            for (int i = 0; i < iterations;i++ )
            {
                material.SetFloat("_BlurSize", 1.0f + i * blurSpread);
                RenderTexture bufferTemp = RenderTexture.GetTemporary(rtw, rth, 0);
                Graphics.Blit(buffer, bufferTemp, material, 0);
                Graphics.Blit(bufferTemp, buffer, material, 1);
                RenderTexture.ReleaseTemporary(bufferTemp);
            }
            Graphics.Blit(buffer, dest);
            RenderTexture.ReleaseTemporary(buffer);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }
}
