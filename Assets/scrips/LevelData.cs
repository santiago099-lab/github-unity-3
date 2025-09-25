using UnityEngine;

[CreateAssetMenu(fileName = "New Level Data", menuName = "Game/Level Data", order = 1)]
public class LevelData : ScriptableObject
{
    [Header("Level Settings")]
    public string levelName = "Level 1";
    public int fruitsNeeded = 30;
    public float timeLimit = 120f;

    [Header("Player Settings")]
    public float playerSpeed = 5f;
    public float jumpForce = 10f;

    [Header("Visual")]
    public Color backgroundColor = Color.white;
}