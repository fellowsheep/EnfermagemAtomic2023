using UnityEngine;

[CreateAssetMenu(menuName = "ChoiceData")]
public class ChoiceData : ScriptableObject
{
    public Sprite ScoreboardSprite;
    public Sprite ManSprite;
    public Sprite PopUpSprite;
    public int State; 
    public float Points;
    public AudioScript Audio;
}
