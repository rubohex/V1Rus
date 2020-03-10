using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(BEventPoint))]

public class BEventPointEditor : Editor
{
   

    BEventPoint t;
    SerializedObject GetTarget;
    SerializedProperty ThisList;
    int ListSize;

    void OnEnable()
    {
        t = (BEventPoint)target;
        GetTarget = new SerializedObject(t);
        ThisList = GetTarget.FindProperty("MyList"); // Find the List in our script and create a refrence of it
    }

    public override void OnInspectorGUI()
    {
        //Update our list

        GetTarget.Update();

        //Resize our list
        ListSize = ThisList.arraySize;

        if (ListSize != ThisList.arraySize)
        {
            while (ListSize > ThisList.arraySize)
            {
                ThisList.InsertArrayElementAtIndex(ThisList.arraySize);
            }
            while (ListSize < ThisList.arraySize)
            {
                ThisList.DeleteArrayElementAtIndex(ThisList.arraySize - 1);
            }
        }

        //Or add a new item to the List<> with a button
        EditorGUILayout.LabelField("Add a new item with a button");

        if (GUILayout.Button("Add New"))
        {
            t.MyList.Add(new BEventPoint.MyClass());
        }

        EditorGUILayout.Space();

        //Display our list to the inspector window

        for (int i = 0; i < ThisList.arraySize; i++)
        {
            SerializedProperty MyListRef = ThisList.GetArrayElementAtIndex(i);
            SerializedProperty MyArray = MyListRef.FindPropertyRelative("Path");


            // Array fields with remove at index
            EditorGUILayout.LabelField("Array Fields");

            if (GUILayout.Button("Add New Index", GUILayout.MaxWidth(130), GUILayout.MaxHeight(20)))
            {
                MyArray.InsertArrayElementAtIndex(MyArray.arraySize);
                //MyArray.GetArrayElementAtIndex(MyArray.arraySize - 1).intValue = 0;
            }

            for (int a = 0; a < MyArray.arraySize; a++)
            {
                EditorGUILayout.PropertyField(MyArray.GetArrayElementAtIndex(a));
                if (GUILayout.Button("Remove  (" + a.ToString() + ")", GUILayout.MaxWidth(100), GUILayout.MaxHeight(15)))
                {
                    MyArray.DeleteArrayElementAtIndex(a);
                }
            }



            //Remove this index from the List
            if (GUILayout.Button("Remove This Index (" + i.ToString() + ")"))
            {
                ThisList.DeleteArrayElementAtIndex(i);
            }
        }

        //Apply the changes to our list
        GetTarget.ApplyModifiedProperties();
    }
}