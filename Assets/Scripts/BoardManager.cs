using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour
{
	public static BoardManager Instance{set;get;}
	private bool[,] allowedMoves{set; get;}

	public Chessman[,] Chessmans {set;get;}
	private Chessman selectedChessman;

    private const float TILE_SIZE = 1.0f;
    private const float TILE_OFFSET = 0.5f;

    private int selectionX = -1;
    private int selectionY = -1;

	public List <GameObject> chessmanPrefabs;
	private List<GameObject> activeChessman = new List<GameObject>();

	private Material previousMat;
	public Material selectedMat;

	private Quaternion orientation = Quaternion.Euler(0, 180, 0);  // Разворот чёрных фигур в сторону белых на 180 градусов

	public bool isWhiteTurn = true;

    public int window;

    private void Start()
	{

        if (window == 1)
        {
            var obj1 = GameObject.Find("ChessBoard");
            obj1.active = false;
        }
        if (window == 2)
        {
            var obj1 = GameObject.Find("ChessBoard");
            obj1.active = true;
        }
        Instance = this;
		SpawnAllChessmans();
   	}
    
    private void Update() 
    {
        
        UpdateSelection();
        DrawChessboard();
		if(Input.GetMouseButtonDown(0))
		{
			if (selectionX >= 0 && selectionY >= 0) 
			{
				if (selectedChessman == null)
				{
					// Выбор шахматной фигуры
					SelectChessman(selectionX,selectionY);
				}
				else
				{
					// Движение фигуры по шахматной доске
					MoveChessman(selectionX, selectionY);
				}
			}
		}
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.LoadLevel("Menu");
        }
    }


	private void SelectChessman(int x, int y)
	{
		if (Chessmans [x, y] == null)
			return;

		if (Chessmans [x, y].isWhite != isWhiteTurn)
			return;

		bool hasAtleastOneMove = false;
		allowedMoves = Chessmans [x, y].PossibleMove ();
		for (int i = 0; i < 8; i++)
			for (int j = 0; j < 8; j++)
				if (allowedMoves [i, j])
					hasAtleastOneMove = true;

		if (!hasAtleastOneMove)
			return;
		
		//Показать возможные ходы 
		selectedChessman = Chessmans [x, y];
		previousMat = selectedChessman.GetComponent<MeshRenderer> ().material;
		selectedMat.mainTexture = previousMat.mainTexture;
		selectedChessman.GetComponent<MeshRenderer> ().material = selectedMat;
		BoardHighLights.Instance.HighlightAllowedMoves (allowedMoves);
	}

	private void MoveChessman (int x, int y)
	{
		if (allowedMoves[x,y]) 
		{
			Chessman c = Chessmans [x, y];

			if (c != null && c.isWhite != isWhiteTurn)
			{
				if (c.GetType () == typeof(King)) 
				{
					    EndGame ();
						return;
					}
					
					activeChessman.Remove (c.gameObject);
					Destroy (c.gameObject);

			}

			// Выбор шахматной фигуры

			Chessmans [selectedChessman.CurrentX, selectedChessman.CurrentY] = null;
			selectedChessman.transform.position = GetTileCenter (x, y);
			selectedChessman.SetPosition (x, y);
			Chessmans [x, y] = selectedChessman;
			isWhiteTurn = !isWhiteTurn;
		}


		selectedChessman.GetComponent<MeshRenderer> ().material = previousMat;
		BoardHighLights.Instance.Hidehighlights ();
		selectedChessman = null;
	}

    private void UpdateSelection()
    {
        if (!Camera.main)
        return;

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25.0f, LayerMask.GetMask("ChessPlane")))
        {
			selectionX = (int)hit.point.x;
			selectionY = (int)hit.point.z;
        }
        else
        {
            selectionX = -1;
            selectionY = -1;
        }
    }

	private void SpawnChessmans(int index, int x, int y)
	{
		GameObject go = Instantiate (chessmanPrefabs [index], GetTileCenter(x,y), Quaternion.identity) as GameObject;
		go.transform.SetParent (transform);
		Chessmans [x, y] = go.GetComponent<Chessman> ();
		Chessmans [x, y].SetPosition (x, y);
		activeChessman.Add (go);
	}

	private void SpawnChessmans180(int index, int x, int y)
	{
		GameObject go = Instantiate (chessmanPrefabs [index], GetTileCenter(x,y), orientation) as GameObject;
		go.transform.SetParent (transform);
		Chessmans [x, y] = go.GetComponent<Chessman> ();
		Chessmans [x, y].SetPosition (x, y);
		activeChessman.Add (go);
	}

    private void SpawnAllChessmans()
    {
        activeChessman = new List<GameObject>();
		Chessmans = new Chessman[8, 8];

        // Спавн белых фигур

        //Король
		SpawnChessmans(0, 3, 0);

        //Ферзь
		SpawnChessmans(1, 4, 0);

        //Слон
		SpawnChessmans(2, 0, 0);
		SpawnChessmans(2, 7, 0);

        //Ладья
		SpawnChessmans(3, 2, 0);
		SpawnChessmans(3, 5, 0);

        //Конь
		SpawnChessmans(4, 1, 0);
		SpawnChessmans(4, 6, 0);

        //Пешка
        for (int i = 0 ; i < 8; i++)
			SpawnChessmans(5, i, 1);

        // Спавн чёрных фигур

        //Король
		SpawnChessmans180(6, 4, 7);

        //Ферзь
		SpawnChessmans180(7, 3, 7);

        //Слон
		SpawnChessmans180(8, 0, 7);
		SpawnChessmans180(8, 7, 7);

        //Ладья
		SpawnChessmans180(9, 2, 7);
		SpawnChessmans180(9, 5, 7);

        //Конь
		SpawnChessmans180(10, 1, 7);
		SpawnChessmans180(10, 6, 7);

        //Пешка
		for (int i = 0; i < 8; i++)
			SpawnChessmans180(11, i, 6);
}

	private Vector3 GetTileCenter(int x, int y)
	{
		Vector3 origin = Vector3.zero;
		origin.x += (TILE_SIZE * x) + TILE_OFFSET;
		origin.z += (TILE_SIZE * y) + TILE_OFFSET;
        return origin;
	}

	private void DrawChessboard()
    {
        Vector3 widthLine = Vector3.right * 8;
        Vector3 heigthLine = Vector3.forward * 8;

        for (int i = 0; i <= 8; i++)
        {
            Vector3 start = Vector3.forward * i;
            Debug.DrawLine(start, start + widthLine);
            for (int j = 0; j <= 8; j++)
            {
                start = Vector3.right * j;
                Debug.DrawLine(start, start + heigthLine);
            }
        } 

        // Перекрестие хода
        if (selectionX >= 0 && selectionY >= 0)
        {
            Debug.DrawLine (
                Vector3.forward * selectionY + Vector3.right * selectionX,
                Vector3.forward * (selectionY + 1) + Vector3.right * (selectionX + 1));

            Debug.DrawLine (
                Vector3.forward * (selectionY + 1) + Vector3.right * selectionX,
                Vector3.forward * selectionY + Vector3.right * (selectionX + 1));
        }
    }

	private void EndGame()
	{
		if (isWhiteTurn)
			Debug.Log("White team wins");
          else
			Debug.Log("Black team wins");

		foreach (GameObject go in activeChessman)
			Destroy (go);

		isWhiteTurn = true;
		BoardHighLights.Instance.Hidehighlights ();
		SpawnAllChessmans ();
	}
}