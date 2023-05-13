using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Vector3 = UnityEngine.Vector3;


public class SaveAndLoadManager : MonoBehaviour
{
    private static readonly Lazy<SaveAndLoadManager>
        Lazy = new Lazy<SaveAndLoadManager>(() => new SaveAndLoadManager());

    private SaveAndLoadManager()
    {
    }

    public static SaveAndLoadManager Instance => Lazy.Value;


    // 存档数据结构
    [System.Serializable]
    public struct SaveData
    {
        public string playerName;
        public List<StuffEnum> playerBackpack;
        public Vector3 playerPosition;
    }

    // 存档文件夹路径
    private string _dataPath;

    // 存档列表，key为编号，value为地址
    private Dictionary<uint, string> _saveList = new Dictionary<uint, string>();

    private void Awake()
    {
        // 获取存档文件夹路径
        _dataPath = Application.persistentDataPath;

        // 加载存档列表
        _saveList = LoadSaveList();
    }

    // 保存存档
    public void SaveGame(uint saveID)
    {
        // 创建存档数据对象
        SaveData saveData = new SaveData
        {
            playerName = SystemInfo.deviceName,//设备名字
            playerBackpack = BackpackManager.Instance.BackpackStuff,
            playerPosition = GameInputManage.Instance.playerLocation
        };

        // 生成存档文件名和路径
        string fileName = "Save_" + saveID + ".dat";
        string filePath = Path.Combine(_dataPath, fileName);

        // 序列化存档数据为二进制格式
        using (FileStream fileStream = File.Create(filePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fileStream, saveData);
        }

        // 更新存档列表
        if (_saveList.ContainsKey(saveID))
        {
            _saveList[saveID] = filePath;
        }
        else
        {
            _saveList.Add(saveID, filePath);
        }
    }

    // 加载存档
    public void LoadGame(uint saveID)
    {
        // 获取存档文件路径
        if (_saveList.TryGetValue(saveID, out string filePath))
        {
            // 反序列化二进制数据为存档数据对象
            using (FileStream fileStream = File.Open(filePath, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                SaveData saveData = (SaveData)formatter.Deserialize(fileStream);

                // 加载存档数据
                Debug.Log("Player Name: " + saveData.playerName);
                Debug.Log("Player Level: " + saveData.playerBackpack);
                Debug.Log("Player Position: " + saveData.playerPosition);
            }
        }
        else
        {
            Debug.Log("Save file does not exist!");
        }
    }

    // 删除存档
    public void DeleteSave(uint saveID)
    {
        if (_saveList.TryGetValue(saveID, out string filePath))
        {
            // 删除存档文件
            File.Delete(filePath);

            // 更新存档列表
            _saveList.Remove(saveID);
        }
        else
        {
            Debug.Log("Save file does not exist!");
        }
    }

    // 加载存档列表
    private Dictionary<uint, string> LoadSaveList()
    {
        Dictionary<uint, string> saveList = new Dictionary<uint, string>();

        // 获取存档文件夹下所有文件的完整路径
        string[] fileNames = Directory.GetFiles(_dataPath);

        // 遍历文件列表，筛选存档文件并添加到存档列表中
        foreach (string fileName in fileNames)
        {
            if (Path.GetExtension(fileName) == ".dat" && fileName.StartsWith("Save_"))
            {
                uint saveID = uint.Parse(Path.GetFileNameWithoutExtension(fileName).Substring(5));
                saveList.Add(saveID, fileName);
            }
        }

        return saveList;
    }
}