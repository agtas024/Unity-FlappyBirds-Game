using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class anamenu : MonoBehaviour
{
    public Text enbpuan;
    void Start()
    {
        enbpuan.text = "Highest Score: " + PlayerPrefs.GetInt("kayit");
    }

    public void oyunagit()
    {
        SceneManager.LoadScene("level1");
    }
    public void oyundancik()
    {
        Application.Quit();
    }
}
