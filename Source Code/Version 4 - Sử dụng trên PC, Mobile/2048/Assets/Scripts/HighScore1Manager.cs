using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HighScore1Manager : MonoBehaviour {

	public static HighScore1Manager Instance;

	public Text Score1; public Text Score2; public Text Score3;public Text Score4;
	public Text Score5;public Text Score6;public Text Score7;

	public Text Name1; public Text Name2; public Text Name3;public Text Name4;
	public Text Name5;public Text Name6;public Text Name7;

	// Use this for initialization
	void Start () {
		Instance = this;
		if (PlayerPrefs.GetInt ("mode") == 2) 
		{
			LoadHighScore2 ();
		}
		else	
			LoadHighScore ();
	}

	public void OpenMenu()
	{
		Application.LoadLevel ("MenuScene");
	}

	void LoadHighScore()
	{
		Score1.text = PlayerPrefs.GetInt ("HighScore0").ToString(); 
		Score2.text = PlayerPrefs.GetInt ("HighScore1").ToString(); 
		Score3.text = PlayerPrefs.GetInt ("HighScore2").ToString(); 
		Score4.text = PlayerPrefs.GetInt ("HighScore3").ToString(); 
		Score5.text = PlayerPrefs.GetInt ("HighScore4").ToString(); 
		Score6.text = PlayerPrefs.GetInt ("HighScore5").ToString(); 
		Score7.text = PlayerPrefs.GetInt ("HighScore6").ToString(); 
		Name1.text = PlayerPrefs.GetString ("NameHighScore0");
		Name2.text = PlayerPrefs.GetString ("NameHighScore1");
		Name3.text = PlayerPrefs.GetString ("NameHighScore2");
		Name4.text = PlayerPrefs.GetString ("NameHighScore3");
		Name5.text = PlayerPrefs.GetString ("NameHighScore4");
		Name6.text = PlayerPrefs.GetString ("NameHighScore5");
		Name7.text = PlayerPrefs.GetString ("NameHighScore6");
	}
	void LoadHighScore2()
	{
		Score1.text = (PlayerPrefs.GetInt ("HighScore20") / 60) + ":" + Mod (PlayerPrefs.GetInt ("HighScore20"));
		Score2.text = (PlayerPrefs.GetInt ("HighScore21") / 60) + ":" + Mod (PlayerPrefs.GetInt ("HighScore21"));
		Score3.text = (PlayerPrefs.GetInt ("HighScore22") / 60) + ":" + Mod (PlayerPrefs.GetInt ("HighScore22"));
		Score4.text = (PlayerPrefs.GetInt ("HighScore23") / 60) + ":" + Mod (PlayerPrefs.GetInt ("HighScore23"));
		Score5.text = (PlayerPrefs.GetInt ("HighScore24") / 60) + ":" + Mod (PlayerPrefs.GetInt ("HighScore24"));
		Score6.text = (PlayerPrefs.GetInt ("HighScore25") / 60) + ":" + Mod (PlayerPrefs.GetInt ("HighScore25"));
		Score7.text = (PlayerPrefs.GetInt ("HighScore26") / 60) + ":" + Mod (PlayerPrefs.GetInt ("HighScore26"));
		Name1.text = PlayerPrefs.GetString ("NameHighScore20");
		Name2.text = PlayerPrefs.GetString ("NameHighScore21");
		Name3.text = PlayerPrefs.GetString ("NameHighScore22");
		Name4.text = PlayerPrefs.GetString ("NameHighScore23");
		Name5.text = PlayerPrefs.GetString ("NameHighScore24");
		Name6.text = PlayerPrefs.GetString ("NameHighScore25");
		Name7.text = PlayerPrefs.GetString ("NameHighScore26");
	}
	private int Mod(int x)
	{
		while(x>=60)
			x = x - 60;
		return x;
	}
	// Update is called once per frame
	void Update () {

	}
}
