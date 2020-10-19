using UnityEngine;

[CreateAssetMenu(fileName = "DungeonGenerationData.asset", menuName = "DungeonGenerationData/Dungeon Data")]
public class DungeonGenerationData : ScriptableObject
{
    public int numberOfCrawlers;
    public int iterationMin;
    public int iterationMax;
}
//Allows change of values for the crawler, such as how many crawlers, this is set to 2 at the moment giving two different directions.
//Also you can then set the minimum amount and maximum in the unity inspector.