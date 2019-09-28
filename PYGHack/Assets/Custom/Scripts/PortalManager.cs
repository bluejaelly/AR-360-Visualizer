using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Rendering;

public class PortalManager : MonoBehaviour
{
    [SerializeField]
    private GameObject mainCamera;

    [SerializeField]
    private GameObject sponza;

    private Material[] sponzaMaterials;

    private Material portalPlaneMaterial;

    // Start is called before the first frame update
    void Start()
    {
        sponzaMaterials = sponza.GetComponent<Renderer>().sharedMaterials;
        portalPlaneMaterial = GetComponent<Renderer>().sharedMaterial;
    }

    // Update is called once per frame
    void OnTriggerStay (Collider collider)
    {
        // In reference to the position of the main camera, not the actual position in the world.
        Vector3 cameraPositionInPortalSpace = transform.InverseTransformPoint(mainCamera.transform.position); 

        if (cameraPositionInPortalSpace.y <= 0.0f)
        {
            for (int i = 0; i < sponzaMaterials.Length; i++)
            {
                sponzaMaterials[i].SetInt("_StencilComp", (int)CompareFunction.NotEqual);
            }

            portalPlaneMaterial.SetInt("_CullMode", (int)CullMode.Front);
        }
        else if (cameraPositionInPortalSpace.y < 0.5f)
        {
            // Disable stencil test
            for (int i = 0; i < sponzaMaterials.Length; i++)
            {
                sponzaMaterials[i].SetInt("_StencilComp", (int)CompareFunction.Always);
            }

            portalPlaneMaterial.SetInt("_CullMode", (int)CullMode.Off);
        }
        else
        {
            // Enable stencil test
            for (int i = 0; i < sponzaMaterials.Length; i++)
            {
                sponzaMaterials[i].SetInt("_StencilComp", (int)CompareFunction.Equal);
            }

            portalPlaneMaterial.SetInt("_CullMode", (int)CullMode.Back);
        }
    }
}
