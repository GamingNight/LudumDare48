using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class PostProcessingFX : MonoBehaviour
{

    public Volume volume;

    Bloom bloom;
    public float bloomIntensity;

    Vignette vignette;
    public float vignetteIntensity;
    public Color vignetteColor;

    ChromaticAberration chromAb;
    public float chromAbIntensity;

    LensDistortion lensDist;
    public float lensDistIntensity;


    void Start()
    {
        volume.profile.TryGet<Bloom>(out bloom);
        volume.profile.TryGet<Vignette>(out vignette);
        volume.profile.TryGet<ChromaticAberration>(out chromAb);
        volume.profile.TryGet<LensDistortion>(out lensDist);
    }


    void FixedUpdate()
    {
        bloom.intensity.value = bloomIntensity;
        vignette.intensity.value = vignetteIntensity;
        vignette.color.value = vignetteColor;
        chromAb.intensity.value = chromAbIntensity;
        lensDist.intensity.value = lensDistIntensity;
    }
}
