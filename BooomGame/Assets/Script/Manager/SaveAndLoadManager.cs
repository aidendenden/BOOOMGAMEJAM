using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Vector3 = UnityEngine.Vector3;


public class SaveAndLoadManager : MonoBehaviour
{
    // 存档数据结构
    [System.Serializable]
    public struct SaveData
    {
        public string playerName;
        public StuffList playerBackpack;
        public Vector3 playerPosition;
    }
    
    // 存档文件路径
    private string _saveFilePath;

    private void Start()
    {
        // 初始化存档文件路径
        _saveFilePath = Path.Combine(Application.persistentDataPath, "save_data.dat");
    }

    // 存档
    public void SaveGame()
    {
        // 创建存档数据对象
        var saveData = new SaveData
        {
            playerName = "Player1",
            playerBackpack = GameManager.StuffList,
            playerPosition = GameInputManage.Instance.playerLocation
        };

        // 序列化存档数据为二进制格式
        var formatter = new BinaryFormatter();
        using (FileStream fileStream = File.Create(_saveFilePath))
        {
            formatter.Serialize(fileStream, saveData);
        }
    }

    // 加载存档
    public void LoadGame()
    {
        if (File.Exists(_saveFilePath))
        {
            // 反序列化二进制数据为存档数据对象
            var formatter = new BinaryFormatter();
            using (FileStream fileStream = File.Open(_saveFilePath, FileMode.Open))
            {
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
}

