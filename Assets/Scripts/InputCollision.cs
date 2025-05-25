using TMPro;
using UnityEngine;

public class InputCollision : MonoBehaviour
{

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "corsier")
        {
            keyboardManager.Instance.currentInput = gameObject.GetComponent<TMP_Text>();
        }
    }


}
