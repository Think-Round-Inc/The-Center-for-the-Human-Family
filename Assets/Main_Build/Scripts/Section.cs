using UnityEditor;
using UnityEngine;

public static class SectionColorHolder
{
    public static Color EmptyScreenColor;
}

public sealed class SectionEditorWindow : EditorWindow
{
    private Color emptyScreenColor = Color.black;

    [MenuItem("Window/Section Editor")]
    public static void OpenWindow()
    {
        SectionEditorWindow window = GetWindow<SectionEditorWindow>("Section Editor");
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Section Settings", EditorStyles.boldLabel);
        emptyScreenColor = EditorGUILayout.ColorField("Empty Screen Color", emptyScreenColor);

        if (GUILayout.Button("Apply"))
        {
            SectionColorHolder.EmptyScreenColor = emptyScreenColor;
            Debug.Log("Empty Screen Color Applied: " + emptyScreenColor);
        }
    }
}

public enum SectionType
{
    Christians,
    Jews,
    Buddhists_Jains,
    Hindus_Sikhs,
    Taoists_Confucians,
    Indigenous,
    Muslims
}

[System.Serializable]
public sealed class Section : MonoBehaviour
{
    [SerializeField] SectionType sectionType;
    public Color missingPaintingScreenColor = Color.red;

    private void Start()
    {
        SectionColorHolder.EmptyScreenColor = missingPaintingScreenColor;
        SetAllPaintingTexturesFromPaintingData();
    }

    public SectionType GetCurrentSectionType() => sectionType;

    public void SetAllPaintingTexturesFromPaintingData()
    {
        PaintingData[] paintings = GetComponentsInChildren<PaintingData>();
        for (int i = 0; i < paintings.Length; i++)
        {
            paintings[i].InitializePaintingData();
        }
    }
}
