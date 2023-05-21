using System;
using UnityEngine;
using UnityEngine.UI;

public class DigitalLock : MonoBehaviour
{
    private string input = "";
    public const string password = "1234";

    public void EnterDigit(string digit)
    {
        input += digit;
        if (input.Length > 4)
        {
            Debug.Log("密码超出大小");
            return;
            //input = input.Substring(1);
        }
        CheckPassword();
    }

    public void DeleteDigit()
    {
        // 从输入中删除最后位数字
        if (input.Length > 0)
        {
            input = input.Substring(0, input.Length - 1);
        }
    }

    private void CheckPassword()
    {
        if (input.Length<4)
        {
            Debug.Log("密码错误");
            return;
        }
        
        if (input == password)
        {
            Debug.Log("成功");
            input = "";
        }
        else if (input.Length == 4)
        {
            Debug.Log("密码错误");
            input = "";
        }
    }
}