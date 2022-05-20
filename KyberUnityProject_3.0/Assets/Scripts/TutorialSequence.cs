using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSequence : MonoBehaviour
{
    public List<TutorialPart> tutorialParts;
    public List<GameObject> tutorialTexts;
    [SerializeField] private int currentTutorialSegment = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (tutorialParts.Count > 0)
        {
            tutorialParts[currentTutorialSegment].invokeStartEvents();
        }
    }

    public void NextSegment()
    {
        if(currentTutorialSegment < tutorialParts.Count - 1)
        {
            DisableAllTexts();
            tutorialParts[currentTutorialSegment].invokeEndEvents();
            currentTutorialSegment += 1;
            tutorialParts[currentTutorialSegment].invokeStartEvents();
            tutorialTexts[currentTutorialSegment].SetActive(true);
        }

    }

    public void PrevSegment()
    {
        if(currentTutorialSegment > 0)
        {
            DisableAllTexts();
            tutorialParts[currentTutorialSegment].invokeEndEvents();
            currentTutorialSegment -= 1;
            tutorialParts[currentTutorialSegment].invokeStartEvents();
            tutorialTexts[currentTutorialSegment].SetActive(true);
        }
    }

    private void DisableAllTexts()
    {
        foreach(GameObject g in tutorialTexts)
        {
            g.SetActive(false);
        }
    }
}
