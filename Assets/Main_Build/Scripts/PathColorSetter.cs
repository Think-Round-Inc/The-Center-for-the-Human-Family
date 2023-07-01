using UnityEngine;

public class PathColorSetter : MonoBehaviour
{
    [SerializeField] GameObject[] paths;
    [SerializeField] Color[] pathColors;

    private void Start()
    {
        SetPathColors();
    }

    public void SetPathColors()
    {
        for (int i = 0; i < paths.Length; i++)
        {
            if (i < pathColors.Length)
            {
                if (paths[i].TryGetComponent(out Renderer renderer))
                {
                    renderer.material.SetColor("_Color", pathColors[i]);
                }
            }
        }
    }
}
