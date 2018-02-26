using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour { 

	public int window;

	public int sw;

	public Camera Cam0;

	public Camera Cam1;

	void Start () 
	{
		window = 1;
       if (window == 1)
        {
            var obj1 = GameObject.Find("ChessBoard");
            obj1.active = false;
        }
     }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            window = 4;
        }
    }
        void OnGUI () 
	{
		GUI.Box(new Rect(Screen.width/2-150, Screen.height/2-150, 300, 300), "3D Шахматы");
		if(window == 1) 
		{ 
			if(GUI.Button(new Rect(Screen.width/2-100, Screen.height/2-100, 200, 40), "Новая игра"))
			{ 
				window = 2;
                Application.LoadLevel("ChessGame");

			} 
			if(GUI.Button(new Rect(Screen.width/2-100, Screen.height/2-25, 200, 40), "О приложении"))
			{ 
				window = 3; 
			} 
			if(GUI.Button(new Rect(Screen.width/2-100, Screen.height/2+50, 200, 40), "Выход")) 
			{ 
				window = 4; 
			} 
		} 
		if(window == 3) 
		{    
			GUI.Label(new Rect(Screen.width/2-40, Screen.height/2-100, 300, 150), 
				"Разработчик: \n" +
				"\nстудент группы 4-09-П                               " +
                "Комаров Богдан"); 
			if(GUI.Button (new Rect(Screen.width/2-100, Screen.height/2+50, 200, 40), "Назад")) 
			{ 
				window = 1; 
			}   
		} 
		if(window == 4) 
		{ 
			GUI.Label(new Rect(Screen.width/2-50, Screen.height/2-100, 200, 200), "Вы уже выходите?");   
			if(GUI.Button (new Rect(Screen.width/2-100, Screen.height/2-25, 200, 40), "Да")) 
			{ 
				Application.Quit(); 
			} 
			if(GUI.Button (new Rect(Screen.width/2-100, Screen.height/2+50, 200, 40), "Нет")) 
			{ 
				window = 1; 
			} 
		}  
	} 
} 