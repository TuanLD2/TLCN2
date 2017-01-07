using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

	public GameObject DoKho;

	// Use this for initialization
	void Start () {
		if (!PlayerPrefs.HasKey ("mode"))
			PlayerPrefs.SetInt ("mode",1);
		if (PlayerPrefs.GetInt ("mode") == 2 && PlayerPrefs.HasKey ("MainMode2") && PlayerPrefs.GetInt ("MainMode2") == 1) 
		{
			DoKho.SetActive (true);
			PlayerPrefs.SetInt ("MainMode2", 0);
		}
	}

	public void OpenPlay(string mode)
	{
		if (mode == "MainScene") 
		{
			PlayerPrefs.SetInt ("mode",1);
			PlayerPrefs.Save ();
			Application.LoadLevel ("MainScene");
		}
		else 
		{
			PlayerPrefs.SetInt ("mode",2);
			PlayerPrefs.Save();
			DoKho.SetActive (true);
		}
	}

	public void ChonDoKho(string doKho)
	{
		if (doKho == "Easy") 
		{
			PlayerPrefs.SetInt ("dokho",1);
			PlayerPrefs.Save ();
		}
		else 
		{
			PlayerPrefs.SetInt ("dokho",2);
			PlayerPrefs.Save ();
		}
		PlayerPrefs.SetString ("Video", "MovieStart");
		Application.LoadLevel ("MainScene");
	}

	public void Back()
	{
		Application.LoadLevel ("MenuScene");
	}

	public void OpenHighScore(string mode)
	{
		if (mode == "MainScene") 
		{
			PlayerPrefs.SetInt ("mode",1);
		}
		else 
		{
			PlayerPrefs.SetInt ("mode",2);
		}
		Application.LoadLevel ("HighScoreScene");
	}

	// Update is called once per frame
	void Update () {

	}
}
