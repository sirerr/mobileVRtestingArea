using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Merge.MergeCubeSDK))]
public class MergeCubeSDKInspector : Editor
{
	private static readonly string[] _dontIncludeMe = new string[]{ "m_Script" };

	SerializedProperty viewMode;
	SerializedProperty useMergeUserAccount;
	SerializedProperty userAccountDebugMode;
	SerializedProperty cubeConfiguration;

	void OnEnable()
	{
		//viewMode = serializedObject.FindProperty("viewMode");	
		useMergeUserAccount = serializedObject.FindProperty("useMergeUserAccount");	
		userAccountDebugMode = serializedObject.FindProperty("userAccountDebugMode");	
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update ();
		EditorGUILayout.Space();
//		EditorGUILayout.PropertyField(viewMode);
		EditorGUILayout.PropertyField(useMergeUserAccount);
		EditorGUILayout.PropertyField(userAccountDebugMode);
		EditorGUILayout.Space();
		serializedObject.ApplyModifiedProperties ();
	}
}
