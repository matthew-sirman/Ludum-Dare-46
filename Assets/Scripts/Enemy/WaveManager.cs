using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private float waveEndTime;
    private int nActiveSpawners;
    private int wave;
    private bool waveActive;
    private Text nextWaveText;

    void Start() {
        waveEndTime = -timeBetweenWaves - 5;
        wave = 0;
        waveActive = true;
        nextWaveText = PlayerUIController.instance.nextWaveText;
        spawners = new List<EnemyContainer>();
        foreach (Transform child in transform) {
            spawners.Add(
                child.gameObject.GetComponent<EnemyContainer>());
        }
        startWave();
    }

    public void startWave() {
        nActiveSpawners = spawners.Count;
        foreach (EnemyContainer c in spawners) {
            c.setSpawnQueue(generateEnemySequence(wave));
        }
    }

    public void notifyWaveFinished() {
        nActiveSpawners--;
        if (nActiveSpawners == 0) {
            startBetweenWaveDialog();
        }
    }

    private void startBetweenWaveDialog() {
        waveEndTime = Time.time;
        waveActive = false;
    }

    private Queue<EnemyType> generateEnemySequence(int wave) {
        Dictionary<EnemyType, int> comp = new Dictionary<EnemyType, int>(
            waveComposition[wave]);
        List<EnemyType> types = new List<EnemyType>(comp.Keys);
        Queue<EnemyType> q = new Queue<EnemyType>();
        while (types.Count > 0) {
            EnemyType type = types[Random.Range(0, types.Count)];
            q.Enqueue(type);
            comp[type]--;
            if (comp[type] == 0) {
                types.Remove(type);
            }
        }
        
        return q;
    }

    void Update() {
        if (!waveActive) {
            int t = Time.time - waveEndTime;
            nextWaveText.text = "Wave " + wave + "\n" + t + " Seconds";
        }
    }
}
