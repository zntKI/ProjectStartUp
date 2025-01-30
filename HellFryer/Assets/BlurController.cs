using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class BlurController : MonoBehaviour
{
    public PostProcessVolume postProcessVolume; // Reference to your PostProcessVolume
    private DepthOfField depthOfField;          // Reference to Depth of Field effect
    private bool isBlurEnabled = false;         // Tracks blur state

    void Start()
    {
        // Try to get the Depth of Field effect from the volume
        if (postProcessVolume.profile.TryGetSettings(out DepthOfField dof))
        {
            depthOfField = dof;
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.O))
        {
            depthOfField.focusDistance.value = 0.1f;
        }
        else if (Input.GetKeyUp(KeyCode.O))
        {
            depthOfField.focusDistance.value = 10f;
        }

        if (Input.GetKey(KeyCode.P))
        {
            depthOfField.focusDistance.value = 0.1f;

        }
        else if (Input.GetKeyUp(KeyCode.P))
        {
            depthOfField.focusDistance.value = 10f;
        }

 
    }
}
