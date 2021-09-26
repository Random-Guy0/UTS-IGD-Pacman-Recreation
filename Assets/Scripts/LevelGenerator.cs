using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour //i really hope this works
{
	//be prepared for extreme spaghetti code
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
		//levelMap = RandomLevel(Random.Range(0, 100), Random.Range(0, 100)); //only uncomment this line if you want a horrible looking mess

		Destroy(manualLevel);
		GameObject level = new GameObject("level");

		int maxI = levelMap.GetLength(0);
		int maxJ = levelMap.GetLength(1);

		level.transform.Translate(new Vector3(-maxJ + 0.5f, maxI - 1f));

		for (int i = 0; i < maxI; i++)
        {
			for(int j = 0; j < maxJ; j++)
            {
				int spriteInt = levelMap[i, j] - 1;
				Vector3 position = new Vector3(j, -i);
				if (spriteInt >= 0)
				{
					GameObject currentSprite = Instantiate(levelSprites[spriteInt], level.transform);
					currentSprite.transform.Translate(position);

					AdjacentSprites nearbySprites = GetNearbySprites(i, j, maxI, maxJ);

					switch(spriteInt)
                    {
						case 1:
						case 3:
							AlignWall(currentSprite, nearbySprites);
							break;
						case 0:
						case 2:
							AlignCorner(currentSprite, nearbySprites, maxI, maxJ, i, j);
							break;
						case 6:
							AlignTJunction(currentSprite, nearbySprites);
							break;
                    }
				}
			}
        }

		GameObject topRight = Instantiate(level);
		FlipX(topRight);

		GameObject bottomLeft = Instantiate(level);
		FlipY(bottomLeft);
		RemoveDuplicateRow(bottomLeft, maxI);

		GameObject bottomRight = Instantiate(bottomLeft);
		FlipX(bottomRight);
		RemoveDuplicateRow(bottomRight, maxI);

		GameObject[] pellets = GameObject.FindGameObjectsWithTag("Pellet");

		foreach(GameObject pellet in pellets)
        {
			if(pellet.transform.position.y < 0)
            {
				Rotate(pellet, 180.0f);
            }
        }
    }

	private void AlignWall(GameObject currentSprite, AdjacentSprites nearbySprites)
	{
		bool spritesNearbyHorizontal = (nearbySprites.left <= 4 && nearbySprites.left > 0) || (nearbySprites.right <= 4 && nearbySprites.right > 0);
		bool spritesNearbyVertical = (nearbySprites.above <= 4 && nearbySprites.above > 0) || (nearbySprites.below <= 4 && nearbySprites.below > 0);

		bool twoSpritesHorizontal = (nearbySprites.left <= 4 && nearbySprites.left > 0) && (nearbySprites.right <= 4 && nearbySprites.right > 0);
		bool twoSpritesVertical = (nearbySprites.above <= 4 && nearbySprites.above > 0) && (nearbySprites.below <= 4 && nearbySprites.below > 0);

		if ((spritesNearbyHorizontal && !spritesNearbyVertical) || (!twoSpritesVertical && twoSpritesHorizontal))
        {
			Rotate(currentSprite, 90.0f);
        }
		else if((nearbySprites.right == 2 || nearbySprites.right == 4 || nearbySprites.left == 2 || nearbySprites.left ==4) && !twoSpritesVertical)
        {
			Rotate(currentSprite, 90.0f);
        }
    }

	private void AlignCorner(GameObject currentSprite, AdjacentSprites nearbySprites, int maxI, int maxJ, int spriteI, int spriteJ)
    {
		bool[] adjacentSprites = new bool[4];
		adjacentSprites[2] = nearbySprites.left > 0 && nearbySprites.left <= 4;
		adjacentSprites[3] = nearbySprites.below > 0 && nearbySprites.below <= 4;
		adjacentSprites[0] = nearbySprites.right > 0 && nearbySprites.right <= 4;
		adjacentSprites[1] = nearbySprites.above > 0 && nearbySprites.above <= 4;

		int trueCount = 0;
		foreach(bool adjacentSprite in adjacentSprites)
        {
			if(adjacentSprite)
            {
				trueCount++;
            }
        }

		if(trueCount == 1)
        {
			for (int i = 0; i < adjacentSprites.Length; i++)
			{
				if (adjacentSprites[i])
				{
					Rotate(currentSprite, 90.0f * i);
					break;
				}
			}
        }
		else if (trueCount == 2)
        {
			if(adjacentSprites[2] && adjacentSprites[3])
            {
				Rotate(currentSprite, -90.0f);
            }
			else if(adjacentSprites[2] && adjacentSprites[1])
            {
				Rotate(currentSprite, 180.0f);
            }
			else if(adjacentSprites[0] && adjacentSprites[1])
            {
				Rotate(currentSprite, 90.0f);
            }
        }
		else if(trueCount == 3 || trueCount == 4)
        {
			int[] diagonals = new int[4];
			for(int i = 0; i < diagonals.Length; i++)
            {
				diagonals[i] = -1;
            }

			bool posI = spriteI + 1 < maxI;
			bool negI = spriteI - 1 >= 0;
			bool posJ = spriteJ + 1 < maxJ;
			bool negJ = spriteJ - 1 >= 0;


			if (posJ && posI)
			{
				diagonals[0] = levelMap[spriteI + 1, spriteJ + 1];
			}

			if(posJ && negI)
            {
				diagonals[1] = levelMap[spriteI - 1, spriteJ + 1];
            }

			if(negJ && negI)
            {
				diagonals[2] = levelMap[spriteI - 1, spriteJ - 1];
            }

			if(negJ && posI)
            {
				diagonals[3] = levelMap[spriteI + 1, spriteJ - 1];
            }

			int validRotaion = 0;

			for(int i = 0; i < diagonals.Length; i++)
            {
				if(diagonals[i] == 0 || diagonals[i] == 5 || diagonals[i] == 6)
                {
					validRotaion = i;
					break;
                }
            }

			Rotate(currentSprite, 90.0f * validRotaion);
        }
	}

	private void AlignTJunction(GameObject currentSprite, AdjacentSprites nearbySprites)
    {
		bool[] validRotations = new bool[4];
		validRotations[0] = nearbySprites.left == 2 && nearbySprites.below == 4;
		validRotations[1] = nearbySprites.below == 2 && nearbySprites.right == 4;
		validRotations[2] = nearbySprites.right == 2 && nearbySprites.above == 4;
		validRotations[3] = nearbySprites.above == 2 && nearbySprites.left == 4;

		int trueCount = 0;
		foreach(bool validRotation in validRotations)
        {
			if(validRotation)
            {
				trueCount++;
            }
        }

		if(trueCount == 1)
        {
			int correctRotation = 0;
			for(int i = 0; i < validRotations.Length; i++)
            {
				if(validRotations[i])
                {
					correctRotation = i;
                }
            }

			Rotate(currentSprite, 90.0f * correctRotation);
        }
    }

	private void Rotate(GameObject gameObject, float angle)
    {
		Vector3 rotation = gameObject.transform.eulerAngles;
		rotation.z += angle;
		gameObject.transform.rotation = Quaternion.Euler(rotation);
	}

	public AdjacentSprites GetNearbySprites(int i, int j, int maxI, int maxJ)
    {
		int previousSprite = 0;
		int nextSprite = 0;
		int aboveSprite = 0;
		int belowSprite = 0;

		if (j - 1 >= 0)
		{
			previousSprite = levelMap[i, j - 1];
		}

		if (j + 1 < maxJ)
		{
			nextSprite = levelMap[i, j + 1];
		}

		if (i + 1 < maxI)
		{
			belowSprite = levelMap[i + 1, j];
		}

		if (i - 1 >= 0)
		{
			aboveSprite = levelMap[i - 1, j];
		}

		return new AdjacentSprites(previousSprite, nextSprite, aboveSprite, belowSprite);
	}

	private void FlipX(GameObject objectToFlip)
    {
		Vector3 scale = objectToFlip.transform.localScale;
		scale.x *= -1;
		objectToFlip.transform.localScale = scale;

		Vector3 pos = objectToFlip.transform.position;
		pos.x *= -1;
		objectToFlip.transform.position = pos;
    }

	private void FlipY(GameObject objectToFlip)
	{
		Vector3 scale = objectToFlip.transform.localScale;
		scale.y *= -1;
		objectToFlip.transform.localScale = scale;

		Vector3 pos = objectToFlip.transform.position;
		pos.y *= -1;
		objectToFlip.transform.position = pos;
	}

	private void RemoveDuplicateRow(GameObject level, int maxI)
    {
		foreach (Transform child in level.transform)
		{
			if (child.localPosition.y == -maxI + 1)
			{
				Destroy(child.gameObject);
			}
		}
	}


	private int[,] RandomLevel(int maxI, int maxJ) //bad random function for testing
    {
		int[,] newLevelMap = new int[maxI, maxJ];
		for(int i = 0; i < maxI; i++)
        {
			for(int j = 0; j < maxJ; j++)
            {
				newLevelMap[i, j] = Random.Range(0, 8);
            }
        }
		return newLevelMap;
    }
}


public class AdjacentSprites
{
	public int left;
	public int right;
	public int above;
	public int below;

    public AdjacentSprites(int left, int right, int above, int below)
    {
        this.left = left;
        this.right = right;
        this.above = above;
        this.below = below;
    }

    public override string ToString()
    {
		return "Left: " + left + ", Right: " + right + ", Above: " + above + ", Below: " + below;
    }
}