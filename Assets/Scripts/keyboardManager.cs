using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class keyboardManager : MonoBehaviour
{
    public List<TMP_Text> input = new List<TMP_Text>();
    public List<string> numberBlockedPrev = new List<string>();
    public List<string> numberBlockedcurr = new List<string>();

    public GameObject[] Buttons;

    public string number;
    public TMP_Text currentInput;
    public GameObject Corsier;

    [SerializeField] AudioClip[] ClipMadaButtons;
    [SerializeField] AudioClip[] ClipDeleteMadaButton;
    bool stopStartCorsierPostion = false;
    // Singleton instance.
    public static keyboardManager Instance = null;

    // Initialize the singleton instance.
    private void Awake()
    {
        // If there is not already an instance , set it to this.
        if (Instance == null)
        {
            Instance = this;
        }
        //If an instance already exists, destroy whatever this object is to enforce the singleton.
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        Buttons = GameObject.FindGameObjectsWithTag("Button");
    }
    public void Update()
    {
        while (input.Count != 0 && !stopStartCorsierPostion)
        {
            stopStartCorsierPostion = true;
            Invoke("onStartCorserPostion", 0.1f);
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
        {

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);


            if (hit.collider != null)
            {

                if (hit.transform.gameObject.tag == ("Input"))
                {
                    Corsier.transform.position = new Vector2(hit.transform.position.x, hit.transform.position.y);
                }
            }
        }
    }

    private void onStartCorserPostion()
    {
        Corsier.transform.position = new Vector2(input[0].gameObject.transform.position.x, input[0].gameObject.transform.position.y);

    }
    public void pressButton(string num)
    {
        numberBlockedPrev = new List<string>();
        numberBlockedcurr = new List<string>();

        foreach (TMP_Text inputText in input)
        {
            if (inputText.text != "")
                numberBlockedPrev.Add(inputText.text);

            if (numberBlockedPrev.Contains(num) && inputText.text == num)
                inputText.text = "";
            if (inputText == currentInput)
                currentInput.text = num;

            if (inputText.text != "")
                numberBlockedcurr.Add(inputText.text);
        }
        for (int i = 0; i < input.Count; i++)
        {

            foreach (GameObject button in Buttons)
            {
                if (numberBlockedcurr.Contains(button.name))
                    button.GetComponent<Image>().color = new Color(0.93f, 0.33f, 0.23f, 0.5f);
                else
                    button.GetComponent<Image>().color = Color.white;
            }
        }

        if (num != "")
            for (int i = 0; i < input.Count; i++)
            {
                if (input[i].text == "")
                {
                    Corsier.transform.position = new Vector2(input[i].gameObject.transform.position.x, input[i].gameObject.transform.position.y);
                    break;
                }
            }
        else
            for (int i = input.Count - 1; i >= 0; i--)
            {
                if (input[i].text != "")
                {
                    Corsier.transform.position = new Vector2(input[i].gameObject.transform.position.x, input[i].gameObject.transform.position.y);
                    break;
                }
            }
    }
    public void DeleteALLButton()
    {
        numberBlockedPrev = new List<string>();
        numberBlockedcurr = new List<string>();

        SoundManager.Instance.RandomSoundEffect(ClipDeleteMadaButton);

        foreach (TMP_Text inputText in input)
        {
            inputText.text = "";
        }

        foreach (GameObject button in Buttons)
        {
            button.GetComponent<Image>().color = Color.white;
        }
    }

}
