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
		GameObject level = new GameObject("level");
		level.transform.Translate(new Vector3(-13.5f, 14.0f));

		int maxI = levelMap.GetLength(0);
		int maxJ = levelMap.GetLength(1);

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
					
					switch(spriteInt)
                    {
						case 1:
						case 3:
							AlignWall(i, j, currentSprite, maxI, maxJ);
							break;
                    }
				}
			}
        }
    }

	private void AlignWall(int i, int j, GameObject currentSprite,int maxI, int maxJ)
	{
		int previousSprite = 0;
		int nextSprite = 0;
		int aboveSprite = 0;
		int belowSprite = 0;

		if(j - 1 >= 0)
        {
			previousSprite = levelMap[i, j - 1];
        }

		if(j + 1 < maxJ)
        {
			nextSprite = levelMap[i, j + 1];
        }

		if (i - 1 >= 0)
		{
			belowSprite = levelMap[i - 1, j];
		}

		if (i + 1 < maxI)
		{
			aboveSprite = levelMap[i + 1, j];
		}

		bool spritesNearbyHorizontal = (previousSprite <= 4 && previousSprite > 0) || (nextSprite <= 4 && nextSprite > 0);
		bool spritesNearbyVertical = (aboveSprite <= 4 && aboveSprite > 0) || (belowSprite <= 4 && belowSprite > 0);

		bool twoSpritesHorizontal = (previousSprite <= 4 && previousSprite > 0) && (nextSprite <= 4 && nextSprite > 0);
		bool twoSpritesVertical = (aboveSprite <= 4 && aboveSprite > 0) && (belowSprite <= 4 && belowSprite > 0);
		
		if ((spritesNearbyHorizontal && !spritesNearbyVertical) || (!twoSpritesVertical && twoSpritesHorizontal))
        {
			Rotate(currentSprite, 90.0f);
        }
		else if((nextSprite == 2 || nextSprite == 4 || previousSprite == 2 || previousSprite ==4) && !twoSpritesVertical)
        {
			Rotate(currentSprite, 90.0f);
        }
    }

	private void Rotate(GameObject gameObject, float angle)
    {
		Vector3 rotation = gameObject.transform.eulerAngles;
		rotation.z += angle;
		gameObject.transform.rotation = Quaternion.Euler(rotation);
	}
}