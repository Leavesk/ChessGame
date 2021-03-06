﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Chessman 
{
	public override bool[,] PossibleMove()
	{
		bool[,] r = new bool [8,8];
		Chessman c, c2;

		// Движение белых фигур
		if (isWhite) 
		{
			// Левая Диагональ

			if (CurrentX != 0 && CurrentY != 7) 
			{
				c = BoardManager.Instance.Chessmans [CurrentX - 1, CurrentY + 1];
				if (c != null && !c.isWhite)
					r [CurrentX - 1, CurrentY + 1] = true;
			}

			// Правая Диагональ

			if (CurrentX != 7 && CurrentY != 7) 
			{
				c = BoardManager.Instance.Chessmans [CurrentX + 1, CurrentY + 1];
				if (c != null && !c.isWhite)
					r [CurrentX + 1, CurrentY + 1] = true;
			}

			// Центр
			if (CurrentY != 7) 
			{
				c = BoardManager.Instance.Chessmans [CurrentX, CurrentY + 1];
				if (c == null)
					r [CurrentX, CurrentY + 1] = true;
			}

			// Первое центральное движение 
			if (CurrentY == 1) 
			{
				c = BoardManager.Instance.Chessmans [CurrentX, CurrentY + 1];
				c2 = BoardManager.Instance.Chessmans [CurrentX, CurrentY + 2];
				if (c == null & c2 == null)
					r [CurrentX, CurrentY + 2] = true;
			}
		} 
		else 
		{
			//Ход черной фигуры
			// Левая Диагональ

			if (CurrentX != 0 && CurrentY != 0) 
			{
				c = BoardManager.Instance.Chessmans [CurrentX - 1, CurrentY - 1];
				if (c != null && c.isWhite)
					r [CurrentX - 1, CurrentY - 1] = true;
			}

			// Правая Диагональ
			if (CurrentX != 7 && CurrentY != 0) 
			{
				c = BoardManager.Instance.Chessmans [CurrentX + 1, CurrentY - 1];
				if (c != null && c.isWhite)
					r [CurrentX + 1, CurrentY - 1] = true;
			}

			// Центр
			if (CurrentY != 0) 
			{
				c = BoardManager.Instance.Chessmans [CurrentX, CurrentY - 1];
				if (c == null)
					r [CurrentX, CurrentY - 1] = true;
			}

			// Первое центральное движение 
			if (CurrentY == 6) 
			{
				c = BoardManager.Instance.Chessmans [CurrentX, CurrentY - 1];
				c2 = BoardManager.Instance.Chessmans [CurrentX, CurrentY - 2];
				if (c == null & c2 == null)
					r [CurrentX, CurrentY - 2] = true;
			}
		}
		return r;
	}
}