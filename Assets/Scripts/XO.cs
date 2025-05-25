using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class XO : MonoBehaviour
{
    [SerializeField] AudioClip[] ClipXOFatorh;

    public Sprite X;
    public Sprite O;
    public Sprite BackgroundButton;
    [SerializeField] GameObject[] NumberText;
    int button = 0;

    public void AddX()
    {
        if (button == 0)
        {
            NumberText = GameObject.FindGameObjectsWithTag("XO");
            Debug.Log(NumberText.Length);
            SoundManager.Instance.RandomSoundEffect(ClipXOFatorh);
            foreach (GameObject numText in NumberText)
            {
                Debug.Log(numText.transform.parent.name);
                Debug.Log(gameObject.transform.parent.name);
                if (numText.transform.parent.name == gameObject.transform.parent.name)
                {
                    Debug.Log("XXXXX");
                    numText.GetComponent<Image>().enabled = true;
                    numText.GetComponent<Image>().sprite = X;
                }

            }
            button++;
            return;
        }
        if (button == 1)
        {
            NumberText = GameObject.FindGameObjectsWithTag("XO");
            Debug.Log(NumberText.Length);
            SoundManager.Instance.RandomSoundEffect(ClipXOFatorh);
            foreach (GameObject numText in NumberText)
            {
                if (numText.transform.parent.name == gameObject.transform.parent.name)
                {
                    Debug.Log("OOOOO");
                    numText.GetComponent<Image>().enabled = true;
                    numText.GetComponent<Image>().sprite = O;
                }
            }
            button++;
            return;
        }
        if (button == 2)
        {
            NumberText = GameObject.FindGameObjectsWithTag("XO");
            Debug.Log(NumberText.Length);
            SoundManager.Instance.RandomSoundEffect(ClipXOFatorh);
            foreach (GameObject numText in NumberText)
            {
                if (numText.transform.parent.name == gameObject.transform.parent.name)
                {
                    Debug.Log("delete XO");
                    numText.GetComponent<Image>().enabled = false;
                }
            }
            button = 0;
            return;
        }
    }
}
