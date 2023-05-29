using UnityEditor;
using UnityEngine;
using MaximovInk.NumbersSort;

[CustomEditor(typeof(LevelDatabase))]
public class YourScriptableObjClassEditor : Editor
{
    private LevelDatabase targetInfo;

    public int levelsToGen = 100;

    public void OnEnable()
    {
        if (targetInfo == null)
        {
            targetInfo = target as LevelDatabase;
        }
    }

    public override void OnInspectorGUI()
    {
        levelsToGen = EditorGUILayout.IntField(levelsToGen);
        if (GUILayout.Button("Generate"))
        {
            targetInfo.GenerateLevels(levelsToGen);
            EditorUtility.SetDirty(targetInfo);
        }

        if (GUILayout.Button("Clear"))
        {
            targetInfo.Clear();
            EditorUtility.SetDirty(targetInfo);
        }

        base.OnInspectorGUI();
    }
}