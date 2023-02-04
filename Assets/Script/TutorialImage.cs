using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tutorial Image", menuName = "ScriptableObject/Tutorial")]
public class TutorialImage : ScriptableObject
{
    public List<Sprite> tutorialImages;
}
