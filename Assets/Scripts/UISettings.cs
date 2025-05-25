
using UnityEngine;

[CreateAssetMenu(fileName = "UISettings", menuName = "ScriptableObjects/UISettings", order = 1)]
public class UISettings : ScriptableObject
{
   public Color _InteractableTextColor = new Color32(255, 95, 8, 255);
   public float _MenuFontSize = 35;
   public float _GeneralFontSize = 20;
}
