using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AphexCreator : Editor {
    [MenuItem("EDITORS/ProjectIcons/Enable")]
    static void EnableIcons()
    {
        EditorApplication.projectWindowItemOnGUI -= AphexCreator.MyCallback();
        EditorApplication.projectWindowItemOnGUI += AphexCreator.MyCallback();
    }

    [MenuItem("EDITORS/ProjectIcons/Disable")]
    static void DisableIcons()
    {
        EditorApplication.projectWindowItemOnGUI -= AphexCreator.MyCallback();
    }

    static EditorApplication.ProjectWindowItemCallback MyCallback()
    {
        EditorApplication.ProjectWindowItemCallback myCallback = new EditorApplication.ProjectWindowItemCallback(IconGUI);
        return myCallback;
    }

    static void IconGUI(string s, Rect r){
        string[] fileNameSplit = AssetDatabase.GUIDToAssetPath(s).Split('/');
        string fileName = fileNameSplit[fileNameSplit.Length - 1];

        string[] fileType;
        if ((fileType = fileName.Split('.')).Length < 3)
            return;

        string type = fileType[1] + "." + fileType[2];
        Debug.Log(type);

        r.width = r.height = r.height * 0.35f;
        switch (type)
        {
            case "aphexschema.cs":
                //Put your icon images somewhere in the project, and refer to them with a string here
                GUI.DrawTexture(r, (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/gizmos/icon.png", typeof(Texture2D)));
                break;
        }
    }
}
