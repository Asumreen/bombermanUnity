using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class menuScript : MonoBehaviour {
	//the game object hold the GameController GameObject
	public GameObject game;
	//the mainMenu object hold the RawImage that have the buttons of the menu as it's child
	public RawImage mainMenu;
	//this function will be called when the player prass the start button on the main menu , it disactivat the main menu and active the GameController 
	public void startGame()
	{
		mainMenu.gameObject.SetActive(false);
		game.gameObject.SetActive(true);
	}
	//this function will be called when the player prass the quit button on the main menu , it will step the game
	public void quit(){
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying=false;
		#else
		Application.Quit();
		#endif
	}
}
