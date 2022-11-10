using System.Collections.Generic;
using UnityEngine;

namespace SaveLoadSystemBuildingName
{
    [CreateAssetMenu(fileName = "SaveablePrefabsBuilding", menuName = "ScriptableObjects/SaveLoadSystem/SingleInstance/SaveablePrefabsBuilding")]
    public class SaveablePrefabsBuilding : ScriptableObject
    {
        static SaveablePrefabsBuilding m_instance;

        [SerializeField] List<SaveableEntityBuilding> m_prefabs = new List<SaveableEntityBuilding>();
        Dictionary<string, SaveableEntityBuilding> m_dictionary = new Dictionary<string, SaveableEntityBuilding>();

        public void Setup()
        {
            Awake();
        }
        public static SaveablePrefabsBuilding instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = FindObjectOfType<SaveablePrefabsBuilding>();
                }
                if(m_instance == null)
                {
                    m_instance = Resources.Load<SaveablePrefabsBuilding>("SaveablePrefabsBuilding");
                }
                if (m_instance == null)
                {
                    Debug.LogError("No instance of type SaveablePrefabsBuilding");
                }
                return m_instance;
            }
        }

        private void Awake()
        {
            m_instance = this;
            UpdateTable();
        }
        private void OnEnable()
        {
            m_instance = this;
            UpdateTable();
        }
        public List<SaveableEntityBuilding> prefabs
        {
            get
            {
                if (m_prefabs == null)
                    m_prefabs = new List<SaveableEntityBuilding>();
                return m_prefabs;
            }
        }
        public static void UpdateTable()
        {
            if (instance == null)
                return;
            instance.m_dictionary.Clear();
            List<SaveableEntityBuilding> toRemove = new List<SaveableEntityBuilding>();
            foreach (var obj in instance.m_prefabs)
            {
                string ID = "";
                SaveableEntityBuilding sav = obj;
                if (!sav)
                {
                    toRemove.Add(obj);
                    continue;
                }
                ID = sav.GetPrefabID();
                if (ID == "")
                {
                    Debug.Log("ID of " + obj.name + " is invalid: ID is empty");
                    continue;
                }
                instance.m_dictionary[ID] = obj;
            }
        }

        public static GameObject GetPrefab(string prefabID)
        {
            if (instance == null)
                return null;
            if (instance.m_dictionary.Count == 0)
            {
                Debug.LogWarning("SaveablePrefabs list is empty");
            }
            if (instance.m_dictionary.TryGetValue(prefabID, out SaveableEntityBuilding prefab))
                return prefab.gameObject;

            Debug.LogWarning("Can't find any Prefab with ID \"" + prefabID + "\" in the SaveablePrefabs list");
            return null;
        }

    }
}