using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ScoreTracker : MonoBehaviour {

	private int score;
	public static ScoreTracker Instance;
	public Text ScoreText;
	public Text HighScoreText;
	public Text NameHighScore;

	public int Score
	{
		get
		{
			return score;
		}
		set
		{ 
			score = value;
			ScoreText.text = score.ToString();
			if (PlayerPrefs.GetInt ("HighScore") < score) 
			{
				PlayerPrefs.SetInt ("HighScore",score);
				HighScoreText.text = score.ToString ();
			}
		}
	}

	void Awake()
	{
		if (PlayerPrefs.GetInt ("mode") == 1) 
		{
			Instance = this;
			PlayerPrefs.SetInt("HighScore",0); // Set HighScore become 0 before build device
			if (!PlayerPrefs.HasKey ("HighScore"))
				PlayerPrefs.SetInt ("HighScore", 0);
			ScoreText.text = "0";
			HighScoreText.text = PlayerPrefs.GetInt ("HighScore").ToString ();
		}
	}
		
}
