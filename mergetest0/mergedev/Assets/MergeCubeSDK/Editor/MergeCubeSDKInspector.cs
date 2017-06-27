using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Merge.MergeCubeSDK))]
public class MergeCubeSDKInspector : Editor
{
	private static readonly string[] _dontIncludeMe = new string[]{ "m_Script" };

	SerializedProperty viewMode;

	void OnEnable()
	{
		viewMode = serializedObject.FindProperty("viewMode");
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update ();
		EditorGUILayout.Space();
		EditorGUILayout.PropertyField(viewMode);
		EditorGUILayout.Space();
		serializedObject.ApplyModifiedProperties ();
	}
}
