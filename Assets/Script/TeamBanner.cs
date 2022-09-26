using UnityEngine;

public class TeamBanner : MonoBehaviour
{
    [SerializeField] private MeshRenderer[] ColoredObjects;

    public void SetColor(Color color)
    {
        for(int i = 0; i < ColoredObjects.Length; i++)
        {
            ColoredObjects[i].material.color = color;
        }
    }
    
}
