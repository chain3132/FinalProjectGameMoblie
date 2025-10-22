using UnityEngine;

public class HalloweenSetup : MonoBehaviour
{
    public SpriteRenderer referenceBackground;
    public Sprite BackGroundHalloween;
    
    public void SetUpHalloween(bool isHalloween)
    {
        if (isHalloween)
        {
            referenceBackground.sprite = BackGroundHalloween;
        }
        
    }
}
