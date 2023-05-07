using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl
{
    private static SceneControl instance;
    public static SceneControl GetInstance() 
    {
        if (instance == null) 
        {
            Debug.LogError("SceneControl实体不存在！");
            return instance;
        }

        return instance;
    }
    public int scene_number=1;
    public string[] string_scene;
    /// <summary>
    /// key为场景的名称，val为场景的信息
    /// </summary>
    public Dictionary<string, SceneBase> dict_scene;

    public SceneControl() 
    {
        instance = this;

        dict_scene = new Dictionary<string, SceneBase>();
        //dict_scene.Add();
    }

    /// <summary>
    /// 加载一个场景
    /// </summary>
    /// <param name="SceneName">场景的名称</param>
    /// <param name="sceneBase">场景的Base</param>
    public void SceneLoad(string SceneName,SceneBase sceneBase) 
    {
        if (scene_number >= 2)  
        {
            foreach (string scenename in string_scene)
            {
                if (scenename == SceneName)
                {
                    Debug.Log($"场景{SceneName}被加载过");
                    break;
                }
                scene_number++;
                string_scene[scene_number] = SceneName;
            }
        }
        
        if (!dict_scene.ContainsKey(SceneName)) 
        {
            dict_scene.Add(SceneName, sceneBase);
        }

        //加载新场景时，老场景执行退出方法
        if (scene_number>=2) 
        {
            dict_scene[SceneManager.GetActiveScene().name].ExitScene();
        }

        //加载新场景时，新场景执行进入方法
        sceneBase.EnterScene();
        GameRoot.GetInstance().UIManager_Root.Pop(true);
        SceneManager.LoadScene(SceneName);

    }

}
