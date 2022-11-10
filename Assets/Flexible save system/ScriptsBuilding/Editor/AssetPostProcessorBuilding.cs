
namespace SaveLoadSystemBuilding.Editor
{
    using UnityEditor;
    class AssetPostProcessorBuilding : AssetPostprocessor
    {
        static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            // Update the List with prefabs
            SaveablePrefabsEditorBuilding.GenerateListWithAllPrefabs();
        }
    }
}