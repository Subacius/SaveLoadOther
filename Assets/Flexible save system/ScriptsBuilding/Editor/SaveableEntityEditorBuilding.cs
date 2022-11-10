using System.Collections.Generic;
namespace SaveLoadSystemBuilding.Editor
{
    using UnityEditor;
    using UnityEngine;
    using SaveLoadSystemBuildingName;

    [CustomEditor(typeof(SaveableEntityBuilding))]
    public class SaveableEntityEditorBuilding : SaveIDManagerEditorBuilding<SaveableEntityBuilding>
    {
       /* private void OnEnable()
        {

            base.OnEnable();
        }*/
        // Implementation for finding all sceene objects of type SaveableEntityBuilding
        override protected SaveableEntityBuilding[] GetObjects()
        {
            
            return FindObjectsOfType<SaveableEntityBuilding>();
            /*List<SaveableEntityBuilding> objectsInScene = new List<SaveableEntityBuilding>();
            return new SaveableEntityBuilding[0];
            foreach (SaveableEntityBuilding go in Resources.FindObjectsOfTypeAll(typeof(SaveableEntityBuilding)) as SaveableEntityBuilding[])
            {
                if (!EditorUtility.IsPersistent(go.transform.root.gameObject) && !(go.hideFlags == HideFlags.NotEditable || go.hideFlags == HideFlags.HideAndDontSave))
                    objectsInScene.Add(go);
            }
            return objectsInScene.ToArray();*/
        }
    }
}