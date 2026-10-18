using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    Text MoneyText;
    public static Money instance;
    public int MyMoney;
     void Awake()
    {
        MoneyText = GetComponent<Text>();
        instance = this;
        MyData();
        if (!PlayerPrefs.HasKey("MyData2")) {

            PlayerPrefs.SetInt("Money", 0);
        }
    }


    void MyData()
    {
        PlayerPrefs.SetInt("MyData2", 1);
    }


    public void Bank(int money)
    {
        
      
            PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + money);
            PlayerPrefs.Save();
     
    }

     void LateUpdate()
    {
        MyMoney = PlayerPrefs.GetInt("Money");
        MoneyText.text = string.Format("{0:F0}", PlayerPrefs.GetInt("Money"));

    }
}
