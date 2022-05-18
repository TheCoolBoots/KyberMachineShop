using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSequence : MonoBehaviour
{
    public List<TutorialPart> tutorialParts;
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
        tutorialParts[currentTutorialSegment].invokeEndEvents();
        currentTutorialSegment += 1;
        tutorialParts[currentTutorialSegment].invokeStartEvents();
    }

    public void PrevSegment()
    {
        tutorialParts[currentTutorialSegment].invokeEndEvents();
        currentTutorialSegment -= 1;
        tutorialParts[currentTutorialSegment].invokeStartEvents();
    }
}
