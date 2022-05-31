using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;

public class SceneTransitionManager : MonoBehaviour
{
    public void GoToScene(int sceneIndex)
    {
        StartCoroutine(GoToSceneRoutine(sceneIndex));
    }
    IEnumerator GoToSceneRoutine(int sceneIndex)
    {
        SteamVR_Fade.View(Color.clear, 0);
        SteamVR_Fade.View(Color.black, .5f);
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene(sceneIndex);
    }
}
