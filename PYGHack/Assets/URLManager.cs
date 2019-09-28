using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class URLManager : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField field;

    private void Start()
    {
        print(PlayerPrefs.GetString("Media Type"));
    }

    public void onClickURL()
    {
        PlayerPrefs.SetString("URL", field.text);
        SceneManager.LoadScene(2);
    }
}
