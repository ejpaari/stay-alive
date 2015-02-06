using UnityEngine;
using System.Collections;

public class StartScreenUI : MonoBehaviour 
{
    public GUISkin main;
    public Texture backgroundImage;

    void Start()
    {
        Screen.showCursor = true;
    }

	void OnGUI () 
    {
        GUI.DrawTexture(new Rect(0.0f, 0.0f, backgroundImage.width, backgroundImage.height), backgroundImage);

        GUI.skin = main;
        GUI.Label(new Rect(0.0f, 100.0f, Screen.width, 300.0f), "Stay Alive");

        if (GUI.Button(new Rect(Screen.width / 2.0f - 150.0f, Screen.height / 2.0f, 300.0f, 75.0f), "Start"))
        {
            Application.LoadLevel("Main");
        }

        if (GUI.Button(new Rect(Screen.width / 2.0f - 150.0f, Screen.height / 2.0f + 100.0f, 300.0f, 75.0f), "Quit"))
        {
            Application.Quit();
        }
	}
}
