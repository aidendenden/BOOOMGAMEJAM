//好的，以下是只包含代码部分的脚本：

//csharp
using UnityEngine;

public class RotateObject_f : MonoBehaviour
{
    public float rotationSpeed = 50f; // 旋转速度

    void Update()
    {
        // 每帧旋转物体
        transform.Rotate(Vector3.left, rotationSpeed * Time.deltaTime);
    }
}


//将这个代码保存为一个名为`RotateObject.cs`的文件，并将其放置在Unity项目中的任何位置。然后将这个脚本组件添加到您想要旋转的物体上，就可以使物体旋转了。