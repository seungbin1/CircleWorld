using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameMenu))]
[CanEditMultipleObjects]
class CustomInspecter : Editor
{
    public GameMenu selected;

    private void OnEnable()
    {
        // target은 Editor에 있는 변수로 선택한 오브젝트를 받아옴.
        if (AssetDatabase.Contains(target))
        {
            selected = null;
        }
        else
        {
            // target은 Object형이므로 Enemy로 형변환
            selected = (GameMenu)target;
        }
    }

    SerializedProperty kindProp;
    SerializedProperty sceneNameProp;
    SerializedProperty settingProp;
    SerializedProperty eacapeProp;
    SerializedProperty eacpaeSceneNameProp;
    SerializedProperty timerProp;
    SerializedProperty gameObjectTrueProp;
    SerializedProperty gameObjectFalseProp;

    private void Awake()
    {
        kindProp = serializedObject.FindProperty("kind");
        settingProp = serializedObject.FindProperty("settingMenu");
        sceneNameProp = serializedObject.FindProperty("sceneName");
        eacapeProp = serializedObject.FindProperty("eacape");
        eacpaeSceneNameProp = serializedObject.FindProperty("eacpaeSceneName");
        timerProp = serializedObject.FindProperty("timer");
        gameObjectTrueProp = serializedObject.FindProperty("gameObjectTrue");
        gameObjectFalseProp = serializedObject.FindProperty("gameObjectFalse");
    }

    public override void OnInspectorGUI()
    {
        if (selected == null)
        {
            return;
        }

        EditorGUILayout.PropertyField(kindProp);
        EditorGUILayout.PropertyField(eacapeProp);

        switch (selected.kind)
        {
            case GameMenu.Kind.SETTING:
                EditorGUILayout.PropertyField(settingProp);
                break;

            case GameMenu.Kind.SETTINGEXIT:
                EditorGUILayout.PropertyField(settingProp);
                break;

            case GameMenu.Kind.SCENEMOVE:
                EditorGUILayout.PropertyField(sceneNameProp);
                break;

            case GameMenu.Kind.STOPUI:
                EditorGUILayout.PropertyField(gameObjectTrueProp);
                EditorGUILayout.PropertyField(gameObjectFalseProp);
                break;

            case GameMenu.Kind.RESUME:
                EditorGUILayout.PropertyField(gameObjectTrueProp);
                EditorGUILayout.PropertyField(gameObjectFalseProp);
                break;

            case GameMenu.Kind.SETUI:

                break;
            case GameMenu.Kind.BACKMENU:
                EditorGUILayout.PropertyField(gameObjectTrueProp);
                EditorGUILayout.PropertyField(gameObjectFalseProp);
                break;

            case GameMenu.Kind.REWARD:
                EditorGUILayout.PropertyField(gameObjectTrueProp);
                EditorGUILayout.PropertyField(gameObjectFalseProp);
                EditorGUILayout.PropertyField(timerProp);
                break;

            case GameMenu.Kind.NULL:
                break;

            default:
                break;
        }
        if (eacapeProp.boolValue)
        {
            EditorGUILayout.PropertyField(eacpaeSceneNameProp);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
