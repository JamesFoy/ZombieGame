using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "Waves", order = +1)]
public class Wave : ScriptableObject {

    public int enemyCount;
    public List<AIScript> enemyTypes;

}
