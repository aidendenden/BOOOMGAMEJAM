using System;
using UnityEngine;
using UnityEngine.UI;

public class DigitalLock : MonoBehaviour
{
    private string _input = "";
    private const string Password = "1234";

    public void EnterDigit(string digit)
    {
        _input += digit;
        if (_input.Length > 4)
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
        if (_input.Length > 0)
        {
            _input = _input.Substring(0, _input.Length - 1);
        }
    }

    private void CheckPassword()
    {
        if (_input.Length<4)
        {
            Debug.Log("密码错误");
            return;
        }
        
        if (_input == Password)
        {
            Debug.Log("成功");
            _input = "";
        }
        else if (_input.Length == 4)
        {
            Debug.Log("密码错误");
            _input = "";
        }
    }
}