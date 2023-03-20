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
	private int lastEnemyId = -1;

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


		if(AliveCounter() <= 0) {
			Manager.instance.dungeonManager.ToggleHallWalls(true);
			Manager.instance.Invoke("ChangeTurn", 0f);
		}
	}

	

	private IEnumerator ActionDelay() {
		yield return new WaitForSeconds(actionDelay);
		if(spawnedEnemies.Count > 0) {
			int random = Random.Range(0, spawnedEnemies.Count);
			StartCoroutine(SetActiveMark(random));

			spawnedEnemies[random].GetComponent<EnemyController>().GetAction();
		}
	}

	public IEnumerator SetActiveMark(int currentId) {
		//currentID is -1 when its not the enemies turn
		if(currentId < 0) {
			for(int i = 0; i < spawnedEnemies.Count; i++) {
				spawnedEnemies[i].GetComponent<EnemyController>().enemyInfo.SetActiveTurn(false);
			}	
		} else {
			yield return new WaitForSeconds(0);

			if(lastEnemyId == currentId) {
				spawnedEnemies[currentId].GetComponent<EnemyController>().enemyInfo.SetActiveTurn(true);
			} else {
				if(lastEnemyId >= 0 && lastEnemyId < spawnedEnemies.Count) {
					spawnedEnemies[lastEnemyId].GetComponent<EnemyController>().enemyInfo.SetActiveTurn(false);
				}

				spawnedEnemies[currentId].GetComponent<EnemyController>().enemyInfo.SetActiveTurn(true);
				lastEnemyId = currentId;
			}
		}
	}
}
