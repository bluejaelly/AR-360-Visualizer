using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    [SerializeField]
    private GameObject videoSphere;

    private VideoPlayer videoPlayer;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = videoSphere.GetComponent<VideoPlayer>();

        // Video clip from Url
        videoPlayer.source = VideoSource.Url;
        videoPlayer.url = PlayerPrefs.GetString("URL");

        //Play Video
        videoPlayer.Play();
    }
}
