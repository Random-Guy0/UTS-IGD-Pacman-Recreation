using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
	[SerializeField] private GameObject manualLevel;

	[SerializeField] private GameObject[] levelSprites;

	private int[,] levelMap =
	{
		{1,2,2,2,2,2,2,2,2,2,2,2,2,7},
		{2,5,5,5,5,5,5,5,5,5,5,5,5,4},
		{2,5,3,4,4,3,5,3,4,4,4,3,5,4},
		{2,6,4,0,0,4,5,4,0,0,0,4,5,4},
		{2,5,3,4,4,3,5,3,4,4,4,3,5,3},
		{2,5,5,5,5,5,5,5,5,5,5,5,5,5},
		{2,5,3,4,4,3,5,3,3,5,3,4,4,4},
		{2,5,3,4,4,3,5,4,4,5,3,4,4,3},
		{2,5,5,5,5,5,5,4,4,5,5,5,5,4},
		{1,2,2,2,2,1,5,4,3,4,4,3,0,4},
		{0,0,0,0,0,2,5,4,3,4,4,3,0,3},
		{0,0,0,0,0,2,5,4,4,0,0,0,0,0},
		{0,0,0,0,0,2,5,4,4,0,3,4,4,0},
		{2,2,2,2,2,1,5,3,3,0,4,0,0,0},
		{0,0,0,0,0,0,5,0,0,0,4,0,0,0},
	};

    private void Start()
    {
		Destroy(manualLevel);
    }
}
