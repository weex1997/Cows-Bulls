using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Totorilas : MonoBehaviour
{
    public List<GameObject> TutorialsGameObjects = new List<GameObject>();
    string[] tutorial_string = new string[] {
        "Here's a guess.",
        "Here's the guessing number.",
        "Here are the numbers that have been attempted to find the hidden number.",
        "Here are the results showing how many cows and bulls have been found in the guess. We have 1 <sprite index=2> and 1 <sprite index=3>.",
        "Cow <sprite index=2> means that there is a number in the guess that is present in the hidden numbers, but its position is incorrect.",
        "Bull <sprite index=3> means that there is a number present in the hidden numbers, and its position is correct.",
        "You win if you get bulls equal to the number of hidden numbers.",
        "These are additional features that help you with analysis and focus. If you press a number from the previous guesses, it will be marked on the invoice.",
        "The first press gives us the symbol <sprite index=4> and you can use it if you think that this number is not among the hidden numbers.",
        "Another press gives us the symbol <sprite index=5> and this means that you are sure about the number.",
        "A third press will return the number to its original state.",
        "Note: You can use these features in any way you prefer; it's up to you.",
        "Here's a dice tool that gives you random numbers.",
        "This is the bull search tool that gives you only one bull.",
        "This is the deletion tool that removes one number not present in the hidden numbers.",
        "Here are the number input buttons.",
        "Here's the button to submit your guess.",
        "Here's the button to delete the guess.",
        "Here's the device screen where the chosen numbers for the guess will appear.",
        "These boxes represent the number of hidden numbers.",
        "Here's the stage number you have reached.",
        "Here's the time, it will start counting from the beginning of the stage, and it will affect your points.",
        "You must finish the stage before your number of attempts runs out, otherwise you will lose, and this will also affect your points. The fewer attempts you have, the more points you will get.",
        "We have finished the explanation. Let's begin!",
        };
    public GameObject Tutorial;
    public GameObject Tutorial_circle;
    public GameObject Tutorial_square;
    public TMP_Text Tutorial_text;
    public GameObject finger;
    public GameObject dark_background;
    public TimerManager timerManager;
    public GameObject Attempt;

    int clickNum = 0;

    void OnEnable()
    {
        Time.timeScale = 0;
        timerManager.StopStopwatch();
        Attempt.SetActive(true);
        Attempt.GetComponent<RectTransform>().SetAsLastSibling();
        finger.SetActive(false);
        Tutorial_circle.SetActive(false);
        Tutorial_square.SetActive(false);
        dark_background.SetActive(true);
        Tutorial_text.text = "Try to find the hidden number using the cow and bull method by making a random guess, and based on the results of the guess, you will get closer to the solution.";
    }
    void OnDisable()
    {
        Attempt.SetActive(false);
        Time.timeScale = 1;
        timerManager.StartStopwatch();
        clickNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if there is a touch
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            dark_background.SetActive(false);
            finger.SetActive(true);
            if (TutorialsGameObjects.Count > clickNum)
            {
                if (TutorialsGameObjects[clickNum].GetComponent<RectTransform>().sizeDelta.x - TutorialsGameObjects[clickNum].GetComponent<RectTransform>().sizeDelta.y < 10)
                {

                    Tutorial_circle.SetActive(true);
                    Tutorial_square.SetActive(false);
                    Tutorial_text.text = tutorial_string[clickNum];

                    Tutorial_circle.GetComponent<RectTransform>().sizeDelta = TutorialsGameObjects[clickNum].GetComponent<RectTransform>().sizeDelta * 1.15f;
                    Tutorial.transform.position = TutorialsGameObjects[clickNum].transform.position;
                    clickNum++;
                }
                else
                {
                    Tutorial_circle.SetActive(false);
                    Tutorial_square.SetActive(true);
                    Tutorial_text.text = tutorial_string[clickNum];

                    Tutorial_square.GetComponent<RectTransform>().sizeDelta = TutorialsGameObjects[clickNum].GetComponent<RectTransform>().sizeDelta * 1.15f;
                    Tutorial.transform.position = TutorialsGameObjects[clickNum].transform.position;
                    clickNum++;
                }
            }
            else if (tutorial_string.Length > clickNum)
            {
                finger.SetActive(false);
                Tutorial_circle.SetActive(false);
                Tutorial_square.SetActive(false);
                dark_background.SetActive(true);
                Tutorial_text.text = tutorial_string[clickNum];
                clickNum++;
            }
            else
            {
                gameObject.SetActive(false);
            }


        }
    }
}
