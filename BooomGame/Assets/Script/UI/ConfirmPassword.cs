using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmPassword : MonoBehaviour
{
    private string password;

    public List<TMP_Text> curPassword;
    public Button btn1;
    public Button btn2;
    public Button btn3;
    public Button btn4;
    public Button btn5;
    public Button btn6;
    public Button btn7;
    public Button btn8;
    public Button btn9;
    public Button btnBackSpace;
    public Button btn0;
    public Button btnConfirm;


    public List<AudioClip> btnAudioClips;
    public AudioSource audioSource;
    private void Awake()
    {
        btn1.onClick.AddListener(() => InputPassword("1"));
        btn2.onClick.AddListener(() => InputPassword("2"));
        btn3.onClick.AddListener(() => InputPassword("3"));
        btn4.onClick.AddListener(() => InputPassword("4"));
        btn5.onClick.AddListener(() => InputPassword("5"));
        btn6.onClick.AddListener(() => InputPassword("6"));
        btn7.onClick.AddListener(() => InputPassword("7"));
        btn8.onClick.AddListener(() => InputPassword("8"));
        btn9.onClick.AddListener(() => InputPassword("9"));
        btnBackSpace.onClick.AddListener(BackspacePassword);
        btn0.onClick.AddListener(() => InputPassword("0"));
        btnConfirm.onClick.AddListener(() => Confirm(password));

    }

    private void OnEnable()
    {
        password = "";
    }
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < curPassword.Count; i++)
        {
            if (password.Length - 1 >= i)
            {
                curPassword[i].text = password[i].ToString();
            }
            else
            {
                curPassword[i].text = "";
            }
        }
    }

    public void InputPassword(string str)
    {
        if (password.Length < 4)
        {
            password += str;
        }
        PlayAudioClip();
    }

    public void BackspacePassword()
    {
        if (password.Length > 0)
        {
            password = password.Remove(password.Length - 1);
        }
        PlayAudioClip();
    }

    public void Confirm(string password)
    {
        if (string.IsNullOrEmpty(password) || password.Length < 4)
        {
            Debug.Log("密码长度不正确");
            return;
        }
        PlayAudioClip();
    }

    private void PlayAudioClip()
    {
        int index = Random.Range(0, 4);
        audioSource.PlayOneShot(btnAudioClips[index]);
    }
}
