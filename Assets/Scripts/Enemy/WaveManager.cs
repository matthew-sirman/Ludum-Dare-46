using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {
    private List<Dictionary<EnemyType, int>> waveComposition = new List<Dictionary<EnemyType, int>>() {
        new Dictionary<EnemyType, int>() {
            {EnemyType.basic, 5}
        },
        new Dictionary<EnemyType, int>() {
            {EnemyType.basic, 10}
        },
        new Dictionary<EnemyType, int>() {
            {EnemyType.basic, 15}
        },
    };
    private List<EnemyContainer> spawners;
    public float timeBetweenWaves;

    void Start() {
        spawners = new List<EnemyContainer>();
        foreach (Transform child in transform) {
            spawners.Add(
                child.gameObject.GetComponent<EnemyContainer>());
        }
    }

    void Update() {
        
    }
}
