using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void OnClickVideo()
    {
        PlayerPrefs.SetString("Media Type", "Video");
        SceneManager.LoadScene(1);
    }

    public void OnClickImage()
    {
        PlayerPrefs.SetString("Media Type", "Image");
        SceneManager.LoadScene(1);
    }
}
