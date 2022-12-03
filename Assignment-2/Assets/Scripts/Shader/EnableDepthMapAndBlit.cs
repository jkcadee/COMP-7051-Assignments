using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class EnableDepthMapAndBlit : MonoBehaviour {
    public Material mat;
    public Camera targetCamera;

	void Start () {
        targetCamera.depthTextureMode = DepthTextureMode.Depth;
	}

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (mat != null)
            Graphics.Blit(source, destination, mat);
        else
            Graphics.Blit(source, destination);
    }
}