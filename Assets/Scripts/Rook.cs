﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : Chessman 
{
	public override bool [,] PossibleMove()
	{
		bool[,] r = new bool[8, 8];

		Chessman c;
		int i;

		// Право
		i = CurrentX;
		while (true) 
		{
			i++;
			if (i >= 8)
				break;

			c = BoardManager.Instance.Chessmans [i, CurrentY];
			if (c == null)
				r [i, CurrentY] = true;
			else 
			{
				if (c.isWhite != isWhite)
					r [i, CurrentY] = true;

				break;
			}
		}

		// Влево
		i = CurrentX;
		while (true) 
		{
			i--;
			if (i < 0)
				break;

			c = BoardManager.Instance.Chessmans [i, CurrentY];
			if (c == null)
				r [i, CurrentY] = true;
			else 
			{
				if (c.isWhite != isWhite)
					r [i, CurrentY] = true;

				break;
			}
		}

		// Вверх
		i = CurrentY;
		while (true) 
		{
			i++;
			if (i >= 8)
				break;

			c = BoardManager.Instance.Chessmans [CurrentX, i];
			if (c == null)
				r [CurrentX, i] = true;
			else 
			{
				if (c.isWhite != isWhite)
					r [CurrentX, i] = true;

				break;
			}
		}

		// Вниз
		i = CurrentY;
		while (true) 
		{
			i--;
			if (i < 0)
				break;

			c = BoardManager.Instance.Chessmans [CurrentX, i];
			if (c == null)
				r [CurrentX, i] = true;
			else 
			{
				if (c.isWhite != isWhite)
					r [CurrentX, i] = true;

				break;
			}
		}

		return r;
	}
}