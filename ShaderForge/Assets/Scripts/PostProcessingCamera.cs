using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcessingCamera : MonoBehaviour {

    public Material postPrecessingMat;

    public void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, postPrecessingMat);
    }
}
