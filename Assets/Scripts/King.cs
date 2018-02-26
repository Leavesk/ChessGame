using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Chessman {

	public override bool[,] PossibleMove()
	{
		bool[,] r = new bool[8, 8];

		KingMove(CurrentX + 1, CurrentY, ref r); // Вверх
		KingMove(CurrentX - 1, CurrentY, ref r); // Вниз
		KingMove(CurrentX, CurrentY - 1, ref r); // Влево
		KingMove(CurrentX, CurrentY + 1, ref r); // Вправо
		KingMove(CurrentX + 1, CurrentY -1, ref r); // Вверх влево
		KingMove(CurrentX - 1, CurrentY -1, ref r); // Вниз влево
		KingMove(CurrentX +1, CurrentY + 1, ref r); // Вверх вправо
		KingMove(CurrentX - 1, CurrentY + 1, ref r); // Вниз вправо

		return r;
	}

	public void KingMove(int x, int y, ref bool[,] r)
	{
		Chessman c;
		if (x >= 0 && x < 8 && y >= 0 && y < 8)
		{
			c = BoardManager.Instance.Chessmans[x, y];
			if (c == null)
				r[x, y] = true;
			else if (isWhite != c.isWhite)
				r[x, y] = true;
		}
	}
}﻿