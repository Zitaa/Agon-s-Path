using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class PersistentData : Singleton
{
    public void Init()
    {
        dataPath = Application.persistentDataPath + saveFile;
        Load();

        GameBehaviour.instance.StartCoroutine(AutoSave());
    }

    [SerializeField] private string saveFile;
    [SerializeField] private int autoSaveInterval;
    [SerializeField] private bool autoSave;

    private string dataPath;
    private bool isStartUp = true;

    #region UNITY FUNCTIONS
    
    private IEnumerator AutoSave()
    {
        yield return new WaitForSecondsRealtime(autoSaveInterval);
        if (autoSave)
        {
            Debug.Log("Auto saving...");
            Save();
        }
        GameBehaviour.instance.StartCoroutine(AutoSave());
    }

    #endregion

    #region PRIVATE FUNCTIONS
    
    private void UpdatePlayerPosition(Vector3 playerPos, Vector3 cameraPos)
    {
        GetGame().GetPlayer().position = new Vector3(playerPos.x, playerPos.y, playerPos.z);
        GetGame().GetCameraSettings().GetCamera().transform.position = new Vector3(cameraPos.x, cameraPos.y, cameraPos.z);
    }

    private void UpdatePlayerSettings(EntitySettings data)
    {
        EntitySettings settings = GetGame().GetPlayer().GetComponent<PlayerEntity>().GetSettings();

        settings.name = data.name;
        settings.speed = data.speed;
        settings.health = data.health;
        settings.damage = data.damage;
        settings.meleeRange = data.meleeRange;
        settings.viewRange = data.viewRange;
    }

    #endregion

    #region PUBLIC FUNCTIONS

    public void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Create(dataPath);
        PlayerData data = new PlayerData();
        Transform player = GetGame().GetPlayer();
        Transform camera = GetGame().GetCameraSettings().GetCamera().transform;

        for (int i = 0; i < GetGame().GetInventory().GetItems().Count; i++)
        {
            Item item = GetGame().GetInventory().GetItem(i);
            if (item != null) GetGame().GetItemSerialization().CreateNewContainer(item.name, AssetDatabase.GetAssetPath(item.icon));
        }

        data.SetSettings(player.GetComponent<PlayerEntity>().GetSettings());
        Debug.Log("Saving " + GetGame().GetItemSerialization().GetContainers().Count + " items.");
        data.SetInventoryItems(GetGame().GetItemSerialization().GetContainers());
        GetGame().GetItemSerialization().NewList();
        data.SetHealth(player.GetComponent<PlayerEntity>().GetHealth());
        data.SetMana(player.GetComponent<PlayerEntity>().GetMana());
        data.SetTime(GetGame().GetDynamicEnvironment().GetTime());
        data.SetPlayerPosition(player.position.x, player.position.y, player.position.z);
        data.SetCameraPosition(camera.position.x, camera.position.y);

        formatter.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(dataPath))
        {
            Debug.Log("Loading file...");
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(dataPath, FileMode.Open);
            PlayerData data = (PlayerData)formatter.Deserialize(file);
            file.Close();

            Debug.Log("Loading " + data.GetInventoryItems().Count + " items.");
            GetGame().GetInventory().Init(data.GetInventoryItems());
            GetGame().GetDynamicEnvironment().Init(data.GetTime());
            UpdatePlayerSettings(data.GetSettings());
            UpdatePlayerPosition(data.GetPlayerPosition(), data.GetCameraPosition());
            GetGame().GetPlayer().GetComponent<PlayerEntity>().Init(data.GetHealth(), data.GetMana());
        }
        else
        {
            GetGame().GetInventory().Init();
            GetGame().GetDynamicEnvironment().Init(GetGame().GetDynamicEnvironment().GetTime());
            GetGame().GetPlayer().GetComponent<PlayerEntity>().Init();
        }
    }

    #endregion

    [System.Serializable]
    private class PlayerData
    {
        private EntitySettings settings;
        private List<ItemSerialization.Container> items;
        private int health, mana;
        private int time;
        private float px, py, pz;
        private float cx, cy, cz = -10;

        public void SetSettings(EntitySettings settings) { this.settings = settings; }

        public void SetInventoryItems(List<ItemSerialization.Container> items)
        {
            this.items = new List<ItemSerialization.Container>();
            this.items = items;
        }

        public void SetHealth(int health) { this.health = health; }

        public void SetMana(int mana) { this.mana = mana; }

        public void SetTime(int time) { this.time = time; }

        public void SetPlayerPosition(float x, float y, float z)
        {
            px = x;
            py = y;
            pz = z;
        }

        public void SetCameraPosition(float x, float y)
        {
            cx = x;
            cy = y;
        }

        public int GetHealth() { return health; }

        public int GetMana() { return mana; }

        public int GetTime() { return time; }

        public Vector3 GetPlayerPosition() { return new Vector3(px, py, pz); }

        public Vector3 GetCameraPosition() { return new Vector3(cx, cy, cz); }

        public EntitySettings GetSettings() { return settings; }

        public List<ItemSerialization.Container> GetInventoryItems() { return items; }
    }
}
