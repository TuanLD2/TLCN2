using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable] //look class tilestyle
public class TileStyle //number,background, textcolor Of Cell
{
	public int Number;
	public Color32 TileColor;
	public Color32 TextColor;

	public Sprite bacgroundSprite;
}


	
public class TileStyleHolder : MonoBehaviour {

	//SINGLETON
	public static TileStyleHolder Instance;

	public TileStyle[] TileStyles; 

	
	void Awake()
	{
		Instance = this;
	}
}
