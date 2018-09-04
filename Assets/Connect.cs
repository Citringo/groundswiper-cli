using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Connect : MonoBehaviour
{
    [SerializeField]
    Text errorText;
    [SerializeField]
    InputField ipField;
    [SerializeField]
    InputField portField;
    [SerializeField]
    Dropdown protocolDropdown;

    public void OpenWS()
    {
        try
        {
            GroundSwiper.I.Connect(protocolDropdown.options[protocolDropdown.value].text, ipField.text, portField.text);
        }
        catch(GSException e)
        {
            errorText.text = e.Message;
            return;
        }
        catch (Exception e)
        {
            errorText.text = "重大なエラー: " + e.Message;
            return;
        }

        SceneManager.LoadScene("Swiper");
    }
}