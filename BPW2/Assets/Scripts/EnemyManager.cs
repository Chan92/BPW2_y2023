using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
	public static EnemyManager instance;

	[SerializeField]
	private DungeonGenerator dgnGen;

	[SerializeField]
	private List<Transform> enemyPool;
	public List<Transform> spawnedEnemies;

	[SerializeField]
	private Transform poolParent;
	[SerializeField]
	private Transform spawnedParent;

	[SerializeField]
	private Transform[] enemyPrefabs;

	[SerializeField]
	private Transform enemyInfoPrefab;
	[SerializeField]
	private Transform enemyInfoCanvas;

	[SerializeField]
	private int poolSize = 10;
	[SerializeField]
	private int spawnChanceTypeB = 35;

	[SerializeField]
	private float actionDelay = 0.4f;

	private void Awake() {
		instance = this;
	}

	private void Start() {
		CreateEnemyPool(poolSize);
	}

	public void EnemyAction() {
		if(Manager.instance.gameTurn == Manager.GameTurn.Enemy) {
			StartCoroutine(ActionDelay());
		}
	}

	public int AliveCounter() {
		int counter = spawnedEnemies.Count;
		return counter;
	}

	private void CreateEnemyPool(int amount) {
		for(int i = 0; i < amount; i++) {
			Transform newEnemy;
			int spawnChance = Random.Range(1, 100);

			if(spawnChance < spawnChanceTypeB) {
				newEnemy = Instantiate(enemyPrefabs[1]);
			} else {
				newEnemy = Instantiate(enemyPrefabs[0]);
			}

			enemyPool.Add(newEnemy);
			newEnemy.parent = poolParent;

			Transform newInfoWindow =  Instantiate(enemyInfoPrefab, enemyInfoCanvas);
			newInfoWindow.GetComponent<EnemyInfo>().AssignInfo(newEnemy);
			newEnemy.GetComponent<EnemyController>().enemyInfo = newInfoWindow.GetComponent<EnemyInfo>();

			newEnemy.gameObject.SetActive(false);
		}
		
	}

	public void SpawnEnemy(Vector3 position) {
		if(enemyPool.Count < 1) {
			CreateEnemyPool(1);
		}

		int random = Random.Range(0, enemyPool.Count);
		Transform newEnemy = enemyPool[random];

		enemyPool.Remove(newEnemy);
		spawnedEnemies.Add(newEnemy);

		newEnemy.position = position;
		newEnemy.parent = spawnedParent;
		newEnemy.gameObject.SetActive(true);
	}

	public void DespawnEnemy(Transform enemy) {		
		enemyPool.Add(enemy);
		spawnedEnemies.Remove(enemy);

		enemy.parent = poolParent;
		enemy.gameObject.SetActive(false);
		AliveCounter();
	}

	private IEnumerator ActionDelay() {
		yield return new WaitForSeconds(actionDelay);
		if(spawnedEnemies.Count > 0) {
			int random = Random.Range(0, spawnedEnemies.Count);
			spawnedEnemies[random].GetComponent<EnemyController>().GetAction();
		}
	}
}
