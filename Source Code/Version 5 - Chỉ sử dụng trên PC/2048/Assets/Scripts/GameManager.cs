using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;



public class GameManager : MonoBehaviour {

	public static GameManager Instance;

	public Text TilteText;
	public GameObject YouWonText;
	public GameObject GameOverText;
	public Text GameOverScoreText;
	public GameObject GameOverPanel;

	//Score
	public GameObject ScoreHUD;
	public GameObject HighScoreHUD;
	//Time
	public GameObject Time;
	public Text TimeText;

	//EnterName
	public InputField EnterName;
	public Text EnterNameText;
	public GameObject EnterName2;
	public GameObject TitleEnterYourName;

	//Name Top1	
	public Text NameTheBest;

	//Video
	public GameObject Video;

	private Tile[,] AllTiles = new Tile[4, 4]; //Array 2 ch 4,4
	private List<Tile[]> columns = new List<Tile[]> ();
	private List<Tile[]> rows = new List<Tile[]> ();
	private List<Tile> EmptyTiles = new List<Tile>(); //List Tiles

	//Time
	private int TimePhut=15;
	private int TimeGiay=0;

	// Use this for initialization
	void Start () 
	{	
		KiemTraCheDo ();
		Instance = this;
		Tile[] AllTilesOneDim = GameObject.FindObjectsOfType<Tile> ();
		foreach (Tile t in AllTilesOneDim) 
		{
			t.Number = 0;
			AllTiles [t.indRow, t.indCol] = t;
			EmptyTiles.Add (t);
		}
			
		columns.Add (new Tile[]{ AllTiles [0, 0], AllTiles [1, 0], AllTiles [2, 0], AllTiles [3, 0] });
		columns.Add (new Tile[]{ AllTiles [0, 1], AllTiles [1,1], AllTiles [2,1], AllTiles [3, 1] });
		columns.Add (new Tile[]{ AllTiles [0, 2], AllTiles [1, 2], AllTiles [2, 2], AllTiles [3, 2] });
		columns.Add (new Tile[]{ AllTiles [0, 3], AllTiles [1, 3], AllTiles [2, 3], AllTiles [3, 3] });

		rows.Add (new Tile[]{ AllTiles [0, 0], AllTiles [0,1], AllTiles [0,2], AllTiles [0,3 ] });
		rows.Add (new Tile[]{ AllTiles [1, 0], AllTiles [1,1], AllTiles [1,2], AllTiles [1, 3] });
		rows.Add (new Tile[]{ AllTiles [2, 0], AllTiles [2, 1], AllTiles [2, 2], AllTiles [2, 3] });
		rows.Add (new Tile[]{ AllTiles [3, 0], AllTiles [3, 1], AllTiles [3, 2], AllTiles [3, 3] });
		Generate ();
		Generate ();
		if (PlayerPrefs.GetInt ("mode") == 2)
			Generate4096 ();
	}

	private void KiemTraCheDo()
	{
		if (PlayerPrefs.GetInt ("mode") == 2) 
		{
			if (PlayerPrefs.GetString ("Video") == "Skip") 
			{
				Video.SetActive (false);
				if (PlayerPrefs.GetInt ("dokho") == 1)
					TimePhut = 30;
				else
					TimePhut = 15;
				NameTheBest.text = PlayerPrefs.GetString ("NameHighScore20");
				ScoreHUD.SetActive (false);
				HighScoreHUD.SetActive (false);
				Time.SetActive (true);
				Invoke ("XuLyTime", 1);
			}
			else 
			{
				Video.SetActive (true);
			}
		} 
		else 
		{
			NameTheBest.text = PlayerPrefs.GetString ("NameHighScore0");
			Time.SetActive (false);
			ScoreHUD.SetActive (true);
			HighScoreHUD.SetActive (true);
		}
	}
	bool tieptuc = true;
	private void XuLyTime()
	{
		if (TimePhut == 0 && TimeGiay== 0 )
		{
			GameOver();
			return;
		}
		if (tieptuc == false)
			return;
		if (TimeGiay == 0) 
		{
			TimeGiay = 59;
			TimePhut--;
		} 
		else
			TimeGiay--;
		if (TimeGiay < 10) {
			TimeText.text = TimePhut.ToString() + ":0" + TimeGiay.ToString();
		}
		else
			TimeText.text = TimePhut.ToString() + ":" + TimeGiay.ToString();
		Invoke ("XuLyTime", 1);
	}

	private int SaveScore = 1;

	private void YouWon()
	{	
		SaveScore = 1;
		if (PlayerPrefs.GetInt ("mode") == 1) 
		{
			GameOverScoreText.text = ScoreTracker.Instance.Score.ToString ();
			GameOverText.SetActive (false); //GameOverText.enable = false
			YouWonText.SetActive (true);
			GameOverPanel.SetActive (true);
		}
		else 
		{
			PlayerPrefs.SetString ("Video", "MovieWin");
			PlayerPrefs.Save ();
			Video.SetActive (true);
			GameOverScoreText.text = TimePhut.ToString () + ":" + TimeGiay.ToString ();
			GameOverText.SetActive (false); //GameOverText.enable = false
			YouWonText.SetActive (true);
			GameOverPanel.SetActive (true);
		}

	}

	private void GameOver()
	{
		if (PlayerPrefs.GetInt ("mode") == 1) 
		{
			GameOverScoreText.text = ScoreTracker.Instance.Score.ToString ();
			SaveScore = 1;
			GameOverPanel.SetActive (true);
		}
		else 
		{
			TitleEnterYourName.SetActive (false);
			EnterName2.SetActive (false);
			SaveScore = 0;
			GameOverScoreText.text = TimePhut.ToString () + ":" + TimeGiay.ToString ();
			GameOverPanel.SetActive (true);
			Invoke ("LoadMovieEnd",4);
		}
	}

	void LoadMovieEnd()
	{
		PlayerPrefs.SetString ("Video", "MovieEnd");
		PlayerPrefs.Save ();
		Video.SetActive (true);
	}

	public void GetInput(string guess)
	{
		Debug.Log ("You Enterd" + guess);
	}
	private int check=1;
	bool CanMove()
	{
		if (EmptyTiles.Count > 0)
			return true;
		else
		{
			//check rows: to be continue
			for (int i = 0; i < rows.Count; i++)
				for (int j = 0; j < columns.Count - 1; j++)
					if (AllTiles [i, j].Number == AllTiles [i, j+1].Number)
						return true;
			//check columns: to be continue
			for (int j = 0; j < columns.Count; j++)
				for (int i = 0; i < rows.Count - 1; i++)
					if (AllTiles [i, j].Number == AllTiles [i + 1, j].Number)
						return true;
			//check with mode Special
			if (PlayerPrefs.GetInt ("mode") == 2) 
			{
				int Duyet = 0;
				//check rows: to be continue
				for (int i = 0; i < rows.Count && Duyet == 0; i++)
					for (int j = 0; j < columns.Count - 1 && Duyet == 0; j++)
						if (AllTiles [i, j].Number == 2048) 
						{
							switch (j) 
							{
							case 0:
								if (AllTiles [i, 1].Number < 8)
									return true;
								break;
							case 1:
								if (AllTiles [i, 0].Number < 8 || AllTiles [i, 2].Number < 8)
									return true;
								break;
							case 2: 
								if (AllTiles [i, 1].Number < 8 || AllTiles [i, 3].Number < 8)
									return true;
								break;
							default:
								if (AllTiles [i, 2].Number < 8)
									return true;
								break;
							}
							Duyet = 1;
						}
				Duyet = 0;
				//check columns: to be continue
				for (int j = 0; j < columns.Count && Duyet == 0; j++)
					for (int i = 0; i < rows.Count - 1 && Duyet == 0; i++)
						if (AllTiles [i, j].Number == 2048) 
						{
							switch (i) 
							{
							case 0:
								if (AllTiles [1, j].Number < 8)
									return true;
								break;
							case 1:
								if (AllTiles [0, j].Number < 8 || AllTiles [2, j].Number < 8)
									return true;
								break;
							case 2: 
								if (AllTiles [1, j].Number < 8 || AllTiles [3, j].Number < 8)
									return true;
								break;
							default:
								if (AllTiles [2, j].Number < 8)
									return true;
								break;
							}
							Duyet = 1;
						} 
			}
		}
		if (check == 1) 
		{
			check = 0;
			return true;
		}
		return false;
	}

	public void NewGameButtonHandler()
	{
		Application.LoadLevel (Application.loadedLevel);
	}
	public void Continue()
	{	
		PlayerPrefs.SetString ("NameHighScore", EnterNameText.text);
		if(SaveScore == 1)
			HighScoreSave();	
		Application.LoadLevel (Application.loadedLevel);
	}	
	public void OpenMenu()
	{
		if (PlayerPrefs.GetInt ("mode") == 2) {
			PlayerPrefs.SetInt ("MainMode2", 1);
			PlayerPrefs.Save ();
		}
		Application.LoadLevel ("MenuScene");
	}

	// 1.down
	bool MakeOneMoveDownIndex(Tile[] LineOfTiles)
	{
		for (int i = 0; i < LineOfTiles.Length - 1; i++) 
		{
			int KiemTra = 0;
			//MOVE BLOCK 
			if (LineOfTiles[i].Number == 0 && LineOfTiles[i+1].Number != 0) 
			{
				LineOfTiles[i].Number = LineOfTiles[i+1].Number;
				LineOfTiles[i+1].Number = 0;
				return true;
			}
			//MERGE BLOCK
			if (LineOfTiles[i].Number != 0 && (LineOfTiles[i].Number == LineOfTiles[i+1].Number || LineOfTiles[i].Number == 4096 || LineOfTiles[i+1].Number == 4096)
				&& LineOfTiles[i].mergedThisTurn == false && LineOfTiles[i+1].mergedThisTurn == false ) 
			{
				if (LineOfTiles [i].Number != 4096 && LineOfTiles [i + 1].Number != 4096) 
				{
					LineOfTiles [i].Number *= 2;
					LineOfTiles [i+1].Number = 0;
					LineOfTiles [i].PlayMergeAnimation ();
					KiemTra = 1;
				} 
				else 
				{
					if (LineOfTiles [i + 1].Number < 7) //LineOfTiles [i] is 4096
					{
						if (LineOfTiles [i + 1].Number > 0 ) 
							LineOfTiles [i].PlayMergeAnimation ();
						LineOfTiles [i + 1].Number = 0;
						KiemTra = 1;
					} 
					else
						if(LineOfTiles [i].Number < 7) 
						{
							if (LineOfTiles [i].Number > 0 ) 
								LineOfTiles [i].PlayMergeAnimation ();
							LineOfTiles [i].Number = 4096;
							LineOfTiles [i + 1].Number = 0;
							KiemTra = 1;
						}
				}
				LineOfTiles [i].mergedThisTurn = true;

				if (PlayerPrefs.GetInt ("mode") == 1) 
				{
					ScoreTracker.Instance.Score += LineOfTiles [i].Number;
					if (LineOfTiles [i].Number == 4096)
						YouWon();
				}
				else 
				{
					if (LineOfTiles [i].Number == 2048) 
					{
						YouWon ();
						tieptuc = false;
					}
				}
				if(KiemTra == 1)
					return true;
			}
		}
		return false;
	}
	// 2.Up
	bool MakeOneMoveUpIndex(Tile[] LineOfTiles)
	{
		for (int i = LineOfTiles.Length-1; i>0 ; i--) 
		{
			//MOVE BLOCK 
			if (LineOfTiles[i].Number == 0 && LineOfTiles[i-1].Number != 0) 
			{
				LineOfTiles[i].Number = LineOfTiles[i-1].Number;
				LineOfTiles[i-1].Number = 0;
				return true;
			}
			//MERGE BLOCK
			if (LineOfTiles[i].Number != 0 && (LineOfTiles[i].Number == LineOfTiles[i-1].Number || LineOfTiles[i].Number == 4096|| LineOfTiles[i-1].Number == 4096)
				&& LineOfTiles[i].mergedThisTurn == false && LineOfTiles[i-1].mergedThisTurn == false) 
			{
				int KiemTra = 0;
				if (LineOfTiles [i].Number != 4096 && LineOfTiles [i - 1].Number != 4096) 
				{
					LineOfTiles [i].Number *= 2;
					LineOfTiles [i-1].Number = 0;
					LineOfTiles [i].PlayMergeAnimation ();
					KiemTra = 1;
				} 
				else 
				{
					if (LineOfTiles [i - 1].Number < 7) 
					{
						if(LineOfTiles [i - 1].Number >0)
							LineOfTiles [i].PlayMergeAnimation ();
						LineOfTiles [i -1].Number = 0;
						KiemTra = 1;
					} 
					else
						if(LineOfTiles [i].Number < 7) 
						{
							if(LineOfTiles [i].Number >0)
								LineOfTiles [i].PlayMergeAnimation ();
							LineOfTiles [i].Number = 4096;
							LineOfTiles [i - 1].Number = 0;
							KiemTra = 1;
						}
				}
				LineOfTiles [i].mergedThisTurn = true;

				if (PlayerPrefs.GetInt ("mode") == 1) 
				{
					ScoreTracker.Instance.Score += LineOfTiles [i].Number;
					if (LineOfTiles [i].Number == 4096)
						YouWon();
				}
				else 
				{
					if (LineOfTiles [i].Number == 2048)
					{
						YouWon ();
						tieptuc = false;
					}
				}
				if(KiemTra == 1)
					return true;
			}

		}
		return false;
	}

	void Generate()
	{
		if (EmptyTiles.Count > 0) 
		{
			int indexForNewNumber = Random.Range (0, EmptyTiles.Count);
			int randomNum = Random.Range (0, 10);
			if (randomNum == 0)
				EmptyTiles [indexForNewNumber].Number = 4;
			else
				EmptyTiles [indexForNewNumber].Number = 2;

			EmptyTiles [indexForNewNumber].PlayApperAnimation ();

			EmptyTiles.RemoveAt (indexForNewNumber);
		}
	}
	void Generate4096()
	{
		if (EmptyTiles.Count > 0) {
			int indexForNewNumber = Random.Range (0, EmptyTiles.Count);
			EmptyTiles [indexForNewNumber].Number = 4096;
			EmptyTiles [indexForNewNumber].PlayApperAnimation ();
			EmptyTiles.RemoveAt (indexForNewNumber);
		}
	}
	// Update is called once per frame
	/*void Update () 
	{
		if (Input.GetKeyDown (KeyCode.G))
			Generate ();
	}*/

	private void ResetMergerFlags()
	{
		foreach (Tile t in AllTiles)
			t.mergedThisTurn = false;
	}

	private void UpdateEmptyTiles()
	{
		EmptyTiles.Clear ();
		foreach (Tile t in AllTiles) 
		{
			if (t.Number == 0)
				EmptyTiles.Add (t);
		}
	}

	public void Move(MoveDirection md)
	{
		Debug.Log (md.ToString() + " move.");
		bool moveMade = false;
		ResetMergerFlags ();
		for (int i = 0; i < rows.Count; i++) 
		{
			switch (md) 
			{
			case MoveDirection.Down:
				while (MakeOneMoveUpIndex (columns[i]))
				{
					moveMade = true;
				}
				break;

			case MoveDirection.Left:
				while (MakeOneMoveDownIndex (rows[i]))
				{
					moveMade = true;
				}
				break;

			case MoveDirection.Right:
				while (MakeOneMoveUpIndex (rows[i]))
				{
					moveMade = true;
				}
				break;

			case MoveDirection.Up:
				while (MakeOneMoveDownIndex (columns[i]))
				{
					moveMade = true;
				}
				break;
			}
		}
		if (moveMade)
		{
			UpdateEmptyTiles ();
			Generate ();
			if (!CanMove ()) 
			{
				GameOver ();
			}
			if (check == 0) 
			{
				Invoke ("XuLyGameOverTre", 3);
			}
		}
	}

	void XuLyGameOverTre()
	{
		if (!CanMove ()) 
		{
			GameOver ();
		}
	}

	void HighScoreSave()
	{
		PlayerPrefs.SetString ("NameHighScore", EnterNameText.text);
		string NameHighScoreX;
		string HighScoreX;
		int Score;
		if (PlayerPrefs.GetInt ("mode") == 1) 
		{
			 NameHighScoreX = "NameHighScore";
			 HighScoreX = "HighScore";
			 Score = ScoreTracker.Instance.Score;
		} 
		else 
		{
			 HighScoreX = "HighScore2";
			 NameHighScoreX = "NameHighScore2";
			 Score = TimePhut * 60 + TimeGiay;
			if (PlayerPrefs.GetInt ("dokho") == 2) 
			{
				Score = Score + 15 * 60; //Hard < Easy + 15 minutes
			}
		}
		//check score in top 7.
		if (Score < PlayerPrefs.GetInt (HighScoreX + 6))
			return;

		//insert from PlayerPrefs to Array
		int[] ArrayHighScore = new int[7];
		for (int i = 0; i < 7; i++) 
		{
			if (PlayerPrefs.HasKey (HighScoreX + i))
				ArrayHighScore [i] = PlayerPrefs.GetInt (HighScoreX + i);
			else 
			{
				ArrayHighScore [i] = 0;
				PlayerPrefs.SetInt (HighScoreX + i,0);
			}
		}
		string[] ArrayNameHighScore = new string[7];
		for (int i = 0; i < 7; i++) 
		{
			if (PlayerPrefs.HasKey (NameHighScoreX + i))
				ArrayNameHighScore [i] = PlayerPrefs.GetString (NameHighScoreX + i);
			else 
			{
				ArrayNameHighScore [i] = "NoName";
				PlayerPrefs.SetString (NameHighScoreX + i,"NoName");
			}
		}

		//insert score into ArraySort;
		for (int i = 0; i < 7; i++) 
		{
			if (Score > ArrayHighScore [i]) 
			{
				for(int j=6; j>i; j--)
				{
					ArrayHighScore [j] = ArrayHighScore [j - 1];	
					ArrayNameHighScore [j]= string.Copy(ArrayNameHighScore [j - 1]);
				}
				ArrayHighScore [i] = Score;
				ArrayNameHighScore [i] = PlayerPrefs.GetString ("NameHighScore");
				break;
			} 
		}
		//insert value from array to HighScoreX 
		for (int i = 0; i < 7; i++) 
		{
			PlayerPrefs.SetInt (HighScoreX + i, ArrayHighScore [i]);
			PlayerPrefs.SetString (NameHighScoreX + i, ArrayNameHighScore [i]);
		}
		PlayerPrefs.Save();
	}

	void ResetScoreName()
	{
		for (int i = 0; i < 7; i++)
		{
			PlayerPrefs.SetInt ("HighScore" + i, 0);
			PlayerPrefs.SetInt ("HighScore2" + i, 0);
			PlayerPrefs.SetString ("NameHighScore" + i, "NoName");
			PlayerPrefs.SetString("NameHighScore2"+i,"NoName");
		}
		PlayerPrefs.Save();
	}
}
