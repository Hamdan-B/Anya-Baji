using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MaterialChanger : MonoBehaviour
{
    [SerializeField]
    private Material newMaterial;

    private void Start()
    {
        if (newMaterial == null)
        {
            Debug.LogError("No material provided! Assign a material in the inspector.");
            return;
        }

        ApplyMaterialToChildren(transform);

#if UNITY_EDITOR
        // Mark the scene objects as dirty to save changes to the scene
        EditorUtility.SetDirty(gameObject);
        foreach (Transform child in transform)
        {
            EditorUtility.SetDirty(child.gameObject);
        }
#endif

        Debug.Log("Material applied to all child objects with MeshRenderer in the scene.");

        // Destroy the script after applying
        Destroy(this);
    }

    private void ApplyMaterialToChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            MeshRenderer meshRenderer = child.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                meshRenderer.material = newMaterial; // Use `material` to apply changes to the scene instance
            }

            // Recursively apply to children of this child
            ApplyMaterialToChildren(child);
        }
    }
}
