using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hyperlink : MonoBehaviour
{

    public void YaraqahDiscord() {
        Application.OpenURL("https://discord.gg/tWHHzGjmU7");

    }
   
    public void OpenURL(string link)
    {
        Application.OpenURL(link);
    }
}
