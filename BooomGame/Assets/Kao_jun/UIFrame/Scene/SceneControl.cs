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
            Debug.LogError("SceneControlʵ�岻���ڣ�");
            return instance;
        }

        return instance;
    }
    public int scene_number=1;
    public string[] string_scene;
    /// <summary>
    /// keyΪ���������ƣ�valΪ��������Ϣ
    /// </summary>
    public Dictionary<string, SceneBase> dict_scene;

    public SceneControl() 
    {
        instance = this;

        dict_scene = new Dictionary<string, SceneBase>();
        //dict_scene.Add();
    }

    /// <summary>
    /// ����һ������
    /// </summary>
    /// <param name="SceneName">����������</param>
    /// <param name="sceneBase">������Base</param>
    public void SceneLoad(string SceneName,SceneBase sceneBase) 
    {
        if (scene_number >= 2)  
        {
            foreach (string scenename in string_scene)
            {
                if (scenename == SceneName)
                {
                    Debug.Log($"����{SceneName}�����ع�");
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

        //�����³���ʱ���ϳ���ִ���˳�����
        if (scene_number>=2) 
        {
            dict_scene[SceneManager.GetActiveScene().name].ExitScene();
        }

        //�����³���ʱ���³���ִ�н��뷽��
        sceneBase.EnterScene();
        GameRoot.GetInstance().UIManager_Root.Pop(true);
        SceneManager.LoadScene(SceneName);

    }

}
