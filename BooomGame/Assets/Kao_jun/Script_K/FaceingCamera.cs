using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceingCamera : MonoBehaviour
{
    //可以把子物体都朝向摄像机的神奇脚本
    Transform[] childs;
    void Start()
    {
        childs = new Transform[transform.childCount];
        for(int i = 0; i<transform.childCount; i++)
        {
            childs[i] = transform.GetChild(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < childs.Length; i++)
        {
            childs[i].rotation = Camera.main.transform.rotation;//这里只是粗暴的把物体的旋转角度扭的和相机一样，实际上应该使得角度限制在一定的区间之中，不过具体是多少还有待调试
        }
    }
}
