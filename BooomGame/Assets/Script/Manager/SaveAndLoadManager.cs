using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Debug = UnityEngine.Debug;
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
        public DateTime worldTime;
        public float gameTime;
    }

    // 存档文件夹路径
    private static string DataPath => Application.persistentDataPath;

    // 存档列表，key为编号，value为地址
    private Dictionary<uint, string> _saveList => LoadSaveList();

    //外部获取存档字典
    public Dictionary<uint, string> SaveList => _saveList;

    // 游戏开始时间
    private DateTime _gameStartTime;

    // 游戏暂停状态
    private bool _gamePaused;

    // 游戏暂停时间
    private TimeSpan _gamePausedTime;

    // 获取当前游戏时间
    public float CurrentGameTime
    {
        get
        {
            TimeSpan timeSinceStart = DateTime.Now - _gameStartTime;
            if (_gamePaused)
            {
                return (float)(_gamePausedTime.TotalSeconds);
            }
            else
            {
                return (float)(timeSinceStart.TotalSeconds - _gamePausedTime.TotalSeconds);
            }
        }
    }

    // 保存存档
    public void SaveGame(uint saveID)
    {
        // 创建存档数据对象
        SaveData saveData = new SaveData
        {
            playerName = SystemInfo.deviceName, //设备名字
            playerBackpack = BackpackManager.Instance.BackpackStuff,
            playerPosition = GameInputManage.Instance.playerLocation,
            worldTime = DateTime.Now,
            gameTime = CurrentGameTime // 保存当前游戏时间
        };

        // 生成存档文件名和路径
        string fileName = "Save_" + saveID + ".dat";
        string filePath = Path.Combine(DataPath, fileName);

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
                Debug.Log("World Time"+saveData.worldTime);
                Debug.Log("Game Time: " + saveData.gameTime);

                // 更新游戏时间
                _gameStartTime = saveData.worldTime;
                _gamePaused = false;
                _gamePausedTime = TimeSpan.FromSeconds(0);
                float elapsedTime = saveData.gameTime;
                if (elapsedTime > 0)
                {
                    TimeSpan timeSinceStart = DateTime.Now - _gameStartTime;
                    if (elapsedTime > timeSinceStart.TotalSeconds)
                    {
                        _gamePausedTime = TimeSpan.FromSeconds(elapsedTime - timeSinceStart.TotalSeconds);
                        _gamePaused = true;
                    }
                }
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
        string[] fileNames = Directory.GetFiles(DataPath);

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

    // 游戏暂停
    public void PauseGame()
    {
        if (!_gamePaused)
        {
            _gamePaused = true;
            _gamePausedTime = TimeSpan.FromSeconds(CurrentGameTime);
        }
    }

    // 游戏继续
    public void ResumeGame()
    {
        if (_gamePaused)
        {
            _gamePaused = false;
            TimeSpan timeSinceStart = DateTime.Now - _gameStartTime;
            _gamePausedTime = TimeSpan.FromSeconds(timeSinceStart.TotalSeconds - _gamePausedTime.TotalSeconds);
        }
    }

    // 游戏重置
    public void ResetGameTime()
    {
        _gameStartTime = DateTime.Now;
        _gamePaused = false;
        _gamePausedTime = TimeSpan.FromSeconds(0);
    }
}

#region TestCode

// using System.Diagnostics;
// using System.Collections;

// 高精度计时器
// public class GameTimer
// {
//     private Stopwatch stopwatch;
//
//     public GameTimer()
//     {
//         stopwatch = new Stopwatch();
//         stopwatch.Start();
//     }
//
//     public float GetElapsedTime()
//     {
//         
//         return (float)stopwatch.Elapsed.TotalSeconds;
//     }
// }

// 低精度计时器
// public class GameTimer
// {
//     private DateTime startTime;
//
//     public GameTimer()
//     {
//         startTime = DateTime.Now;
//     }
//
//     public float GetElapsedTime()
//     {
//         TimeSpan elapsedTime = DateTime.Now - startTime;
//         return (float)elapsedTime.TotalSeconds;
//     }
// }

// System.Diagnostics.Stopwatch stopwatch = new Stopwatch();
// stopwatch.Start(); //  开始监视代码运行时间
// stopwatch.Stop(); //  停止监视
// TimeSpan timespan = stopwatch.Elapsed; //  获取当前实例测量得出的总时间
// double hours = timespan.TotalHours; // 总小时
// double minutes = timespan.TotalMinutes;  // 总分钟
// double seconds = timespan.TotalSeconds;  //  总秒数
// double milliseconds = timespan.TotalMilliseconds;  //  总毫秒数
// stopwatch.Reset(); 

#endregion