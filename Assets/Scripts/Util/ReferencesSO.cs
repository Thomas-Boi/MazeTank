using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ReferencesSO")]
public class ReferencesSO : ScriptableObject
{
    [SerializeField]
    public List<GameObject> references;
}
