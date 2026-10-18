using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Passive : MonoBehaviour
{
    public GameObject PassiveUI;
    public GameObject PassiveUIs;
    public static Passive Instance;
    public Sprite[] UI;
    public int NumberPS = 6;
    public GameObject[] Button;

    void Start()
    {
        

        for (int index=0;index < NumberPS; index++)
        {
          
            if (PlayerPrefs.GetInt("Passives_" + index) == 1)
            {


                Debug.Log(index);
                switch (index)
                {
                    case 0:
                        Mag(index);
                        break;
                    case 1:
                        Health(index);
                        break;
                    case 2:
                        Treasure(index);
                        break;
                    case 3:
                        Reaper(index);
                        break;
                    case 4:
                        Exp(index);
                        break;
                    case 5:
                        Dead(index);
                        break;

                    default:
                        break;

                }
            }
        }



    }
    void Awake()
    {
        Instance = this;
       
    }
    public void  GetPassive(int id)
    {

        

     
        switch (id)
        {
 
            case 0: ///////////// Mag

                if (Money.instance.MyMoney < 300 )
                    return;
                    Mag(id);
                Money.instance.Bank(-300);
                PlayerPrefs.SetInt("Passives_" + id, 1);
                PlayerPrefs.Save();
                break;
            case 1: ///////////// Health

                if (Money.instance.MyMoney < 450)
                    return;
                Health(id);
                Money.instance.Bank(-450);
                PlayerPrefs.SetInt("Passives_" + id, 1);
                PlayerPrefs.Save();
                break;

            case 2:  ///////////// Treasure
                if (Money.instance.MyMoney < 500)
                    return;
                Treasure(id);
                Money.instance.Bank(-500);
                PlayerPrefs.SetInt("Passives_" + id, 1);
                PlayerPrefs.Save();
                break;
            case 3:  ///////////// Reaper
                if (Money.instance.MyMoney < 450)
                    return;
                Reaper(id);
                Money.instance.Bank(-450);
                PlayerPrefs.SetInt("Passives_" + id, 1);
                PlayerPrefs.Save();
                break;
            case 4:  ///////////// Exp
                if (Money.instance.MyMoney < 300)
                    return;
                Exp(id);
                Money.instance.Bank(-300);
                PlayerPrefs.SetInt("Passives_" + id, 1);
                PlayerPrefs.Save();
                break;
            case 5:  ///////////// Dead
                if (Money.instance.MyMoney < 600)
                    return;
                Dead(id);
                Money.instance.Bank(-600);
                PlayerPrefs.SetInt("Passives_" + id, 1);
                PlayerPrefs.Save();
                break;

        }

    }

    void Mag(int id )
    {
        Button[id].GetComponent<Button>().enabled = false;
        Button[id].transform.Find("setActive_Image").gameObject.SetActive(true);
        UiPassive(id);
       
        GameManager.instance.player.transform.Find("Collection").GetComponent<CapsuleCollider2D>().size *= 1.2f;
    }
    void Health(int id)
    {
        UiPassive(id);
    
        Button[id].GetComponent<Button>().enabled = false;
        Button[id].transform.Find("setActive_Image").gameObject.SetActive(true);
        GameManager.instance.maxHealth += 50;
    }
    void Treasure(int id)
    {
        UiPassive(id);
      
        Button[id].GetComponent<Button>().enabled = false;
        Button[id].transform.Find("setActive_Image").gameObject.SetActive(true);
        Coin.instence.GetCoin *= 1.3f;
    }
    void Reaper(int id)
    {
        UiPassive(id);
      
        Button[id].GetComponent<Button>().enabled = false;
        Button[id].transform.Find("setActive_Image").gameObject.SetActive(true);
        GameManager.instance.Reaper = true;
    }
    void Exp(int id)
    {
        UiPassive(id);
       
        Button[id].GetComponent<Button>().enabled = false;
        Button[id].transform.Find("setActive_Image").gameObject.SetActive(true);
        GameManager.instance.expBost *= 2;
    }
    void Dead(int id)
    {
        UiPassive(id);
      
        Button[id].GetComponent<Button>().enabled = false;
        Button[id].transform.Find("setActive_Image").gameObject.SetActive(true);
        GameManager.instance.Dead = true;
    }

    void UiPassive(int id)
    {
        GameObject UIclone = Instantiate(PassiveUI);
        UIclone.transform.SetParent(PassiveUIs.transform,false);
        UIclone.transform.localScale = new Vector3(1,1,1);
        UIclone.transform.Find("UIimage").transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        for (int index = 0; index < UI.Length; index++)
        {
            if (index == id)
            {
                UIclone.transform.Find("UIimage").GetComponent<Image>().sprite = UI[index];
                UIclone.transform.Find("UIimage").GetComponent<Image>().SetNativeSize();
                break;
            }

        }



    }

}
