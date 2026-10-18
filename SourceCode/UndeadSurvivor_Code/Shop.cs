using UnityEngine;

public class Shop : MonoBehaviour
{
    bool Open = false;
    public void setActive()
    {
        if (!Open)
        {
            Open = true;
            gameObject.SetActive(true);
        }
        else
        {
            Open = false;
            gameObject.SetActive(false);
        }
    }


}
