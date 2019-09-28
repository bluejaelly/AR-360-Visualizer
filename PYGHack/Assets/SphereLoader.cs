using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SphereLoader : MonoBehaviour
{
    [SerializeField]
    private Material videoMat;

    [SerializeField]
    private GameObject VideoManager;

    // Start is called before the first frame update
    void Start()
    {
        VideoManager.SetActive(false);
        if (PlayerPrefs.GetString("Media Type") == "Video")
        {
            print("Ran Video");
            VideoManager.SetActive(true);   
        }
        else
        {
            print("Ran Image");
            VideoManager.SetActive(false);
            StartCoroutine(GetTexture());
        }
    }

    IEnumerator GetTexture()
    {
        WWW wwwLoader = new WWW(PlayerPrefs.GetString("URL"));
        yield return wwwLoader;

        videoMat.color = Color.white;
        videoMat.mainTexture = wwwLoader.texture;
    }
}
