using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]

public class PlayVideo : MonoBehaviour {

	public MovieTexture MovieStart;
	public MovieTexture MovieEnd;
	public MovieTexture MovieWin;
	private AudioSource audio;

	public GameObject MainPanel;
	public GameObject GameOverPanel;
	public GameObject Video;
	// Use this for initialization
	private string Name="";
	void Start () 
	{
		if (PlayerPrefs.HasKey ("Video"))
			Name = PlayerPrefs.GetString ("Video");
		else
		{
			Name = "MovieStart";
			PlayerPrefs.SetString ("Video", "MovieStart");
			PlayerPrefs.Save();
		}
		switch (Name) {
		case "MovieStart":
			OpenMovie (MovieStart);
			break;
		case "MovieEnd":
			OpenMovie (MovieEnd);
			break;
		case "MovieWin":
			OpenMovie (MovieWin);
			break;
		default:
			PlayerPrefs.SetString ("Video", "Skip"); //gan skip vao video
			Application.LoadLevel (Application.loadedLevel);
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		switch(Name)
		{
		case "MovieEnd":
			PausePlayMovie (MovieEnd);
			break;
		case "MovieWin":
			PausePlayMovie (MovieWin);
			break;
		default:
			PausePlayMovie (MovieStart);
			break;
		}

	}		

	void PausePlayMovie(MovieTexture MovieX)
	{
		if (Input.GetMouseButtonDown(0) && MovieX.isPlaying) 
		{
			MovieX.Pause ();
		} 
		else
			if (Input.GetMouseButtonDown(0) && !MovieX.isPlaying) 
			{
				MovieX.Play ();
			}
	}

	void OpenMovie(MovieTexture MovieOpen)
	{
		GetComponent<RawImage> ().texture = MovieOpen as MovieTexture;
		audio = GetComponent<AudioSource> ();
		audio.clip = MovieOpen.audioClip;
		MovieOpen.Play ();
		audio.Play ();
	}

	public void Skip()
	{
		
		if (PlayerPrefs.GetString ("Video") == "MovieWin") 
		{
			PlayerPrefs.SetString ("Video", "Skip"); //gan skip vao video
			PlayerPrefs.Save ();
			Video.SetActive (false);
			GameOverPanel.SetActive (true);
		} 
		else 
		{
			PlayerPrefs.SetString ("Video", "Skip"); //gan skip vao video
			PlayerPrefs.Save ();
			Application.LoadLevel ("MainScene");
		}

	}
}
