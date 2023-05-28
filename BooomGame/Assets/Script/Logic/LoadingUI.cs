using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingUI : MonoBehaviour
{
    public float rotateSpeed = 50f; // 旋转速度
    public float delayTime = 2f; // 延迟时间

    private void Start()
    {
        // 2秒后加载下一个场景
        Invoke("LoadNextScene", delayTime);
    }

    private void LoadNextScene()
    {
        // 获取下一个场景的索引
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        // 如果下一个场景的索引大于场景总数，重新开始游戏
        if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        // 加载下一个场景
        SceneManager.LoadScene(nextSceneIndex);
    }
    private void Update()
    {
        // 每帧以rotateSpeed的速度绕Z轴旋转
        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
    }
}