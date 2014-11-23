using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour {
	public static GameManager instance;
	
	public GameObject TilePrefab;
	public GameObject UserPlayerPrefab;
	public GameObject AIPlayerPrefab;
	
	public int mapSize = 22;
	Transform mapTransform;
	
	public List <List<Tile>> map = new List<List<Tile>>();
	public List <Player> players = new List<Player>();
	public int currentPlayerIndex = 0;
	
	void Awake() {
		instance = this;

		mapTransform = transform.FindChild("Map");
	}
	
	// Use this for initialization
	void Start () {		
		generateMap();
		generatePlayers();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (players[currentPlayerIndex].HP > 0) players[currentPlayerIndex].TurnUpdate();
		else nextTurn();
	}
	
	void OnGUI () {
		if (players[currentPlayerIndex].HP > 0) players[currentPlayerIndex].TurnOnGUI();
	}
	
	public void nextTurn() {
		if (currentPlayerIndex + 1 < players.Count) {
			currentPlayerIndex++;
		} else {
			currentPlayerIndex = 0;
		}
	}

	public void highlightTilesAt(Vector2 originLocation, Color highlightColor, int distance) {
		highlightTilesAt(originLocation, highlightColor, distance, true);
	}

	public void highlightTilesAt(Vector2 originLocation, Color highlightColor, int distance, bool ignorePlayers) {

		List <Tile> highlightedTiles = new List<Tile>();

		if (ignorePlayers) highlightedTiles = TileHighlight.FindHighlight(map[(int)originLocation.x][(int)originLocation.y], distance, highlightColor == Color.red);
		else highlightedTiles = TileHighlight.FindHighlight(map[(int)originLocation.x][(int)originLocation.y], distance, players.Where(x => x.gridPosition != originLocation).Select(x => x.gridPosition).ToArray(), highlightColor == Color.red);
		
		foreach (Tile t in highlightedTiles) {
			t.visual.transform.renderer.materials[0].color = highlightColor;
		}
	}
	
	public void removeTileHighlights() {
		for (int i = 0; i < mapSize; i++) {
			for (int j = 0; j < mapSize; j++) {
				if (!map[i][j].impassible) map[i][j].visual.transform.renderer.materials[0].color = Color.white;
			}
		}
	}
 	
	public void moveCurrentPlayer(Tile destTile) {
		if (destTile.visual.transform.renderer.materials[0].color != Color.white && !destTile.impassible && players[currentPlayerIndex].positionQueue.Count == 0) {
			removeTileHighlights();
			players[currentPlayerIndex].moving = false;
			foreach(Tile t in TilePathFinder.FindPath(map[(int)players[currentPlayerIndex].gridPosition.x][(int)players[currentPlayerIndex].gridPosition.y],destTile, players.Where(x => x.gridPosition != destTile.gridPosition && x.gridPosition != players[currentPlayerIndex].gridPosition).Select(x => x.gridPosition).ToArray())) {
				players[currentPlayerIndex].positionQueue.Add(map[(int)t.gridPosition.x][(int)t.gridPosition.y].transform.position + 1.5f * Vector3.up);
				Debug.Log("(" + players[currentPlayerIndex].positionQueue[players[currentPlayerIndex].positionQueue.Count - 1].x + "," + players[currentPlayerIndex].positionQueue[players[currentPlayerIndex].positionQueue.Count - 1].y + ")");
			}			
			players[currentPlayerIndex].gridPosition = destTile.gridPosition;

		} else {
			Debug.Log ("destination invalid");
			destTile.visual.transform.renderer.materials[0].color = Color.cyan;
		}
	}
	
	public void attackWithCurrentPlayer(Tile destTile) {
		if (destTile.visual.transform.renderer.materials[0].color != Color.white && !destTile.impassible) {
			
			Player target = null;
			foreach (Player p in players) {
				if (p.gridPosition == destTile.gridPosition) {
					target = p;
				}
			}
			
			if (target != null) {
								
				//Debug.Log ("p.x: " + players[currentPlayerIndex].gridPosition.x + ", p.y: " + players[currentPlayerIndex].gridPosition.y + " t.x: " + target.gridPosition.x + ", t.y: " + target.gridPosition.y);
//				if (players[currentPlayerIndex].gridPosition.x >= target.gridPosition.x - 1 && players[currentPlayerIndex].gridPosition.x <= target.gridPosition.x + 1 &&
//					players[currentPlayerIndex].gridPosition.y >= target.gridPosition.y - 1 && players[currentPlayerIndex].gridPosition.y <= target.gridPosition.y + 1) {
				
				players[currentPlayerIndex].actionPoints--;
				
				removeTileHighlights();
				players[currentPlayerIndex].attacking = false;			
				
				//attack logic
				//roll to hit
				bool hit = Random.Range(0.0f, 1.0f) <= players[currentPlayerIndex].attackChance - target.evade;
				
				if (hit) {
					//damage logic
					int amountOfDamage = Mathf.Max(0, (int)Mathf.Floor(players[currentPlayerIndex].damageBase + Random.Range(0, players[currentPlayerIndex].damageRollSides)) - target.damageReduction);
					
					target.HP -= amountOfDamage;
					
					Debug.Log(players[currentPlayerIndex].playerName + " successfuly hit " + target.playerName + " for " + amountOfDamage + " damage!");
				} else {
					Debug.Log(players[currentPlayerIndex].playerName + " missed " + target.playerName + "!");
				}
			//  }
			}
		} else {
			Debug.Log ("destination invalid");
		}
	}
	
	void generateMap() {
		loadMapFromXml();

//		map = new List<List<Tile>>();
//		for (int i = 0; i < mapSize; i++) {
//			List <Tile> row = new List<Tile>();
//			for (int j = 0; j < mapSize; j++) {
//				Tile tile = ((GameObject)Instantiate(TilePrefab, new Vector3(i - Mathf.Floor(mapSize/2),0, -j + Mathf.Floor(mapSize/2)), Quaternion.Euler(new Vector3()))).GetComponent<Tile>();
//				tile.gridPosition = new Vector2(i, j);
//				row.Add (tile);
//			}
//			map.Add(row);
//		}
	}

	void loadMapFromXml() {
		MapXmlContainer container = MapSaveLoad.Load("map.xml");
		
		mapSize = container.size;
		
		//initially remove all children
		for(int i = 0; i < mapTransform.childCount; i++) {
			Destroy (mapTransform.GetChild(i).gameObject);
		}
		
		map = new List<List<Tile>>();
		for (int i = 0; i < mapSize; i++) {
			List <Tile> row = new List<Tile>();
			for (int j = 0; j < mapSize; j++) {
				Tile tile = ((GameObject)Instantiate(PrefabHolder.instance.BASE_TILE_PREFAB, new Vector3(i - Mathf.Floor(mapSize/2),0, -j + Mathf.Floor(mapSize/2)), Quaternion.Euler(new Vector3()))).GetComponent<Tile>();
				tile.transform.parent = mapTransform;
				tile.gridPosition = new Vector2(i, j);
				tile.setType((TileType)container.tiles.Where(x => x.locX == i && x.locY == j).First().id);
				row.Add (tile);
			}
			map.Add(row);
		}
	}
	
	void generatePlayers() {
		UserPlayer player;
		
		player = ((GameObject)Instantiate(UserPlayerPrefab, new Vector3(0 - Mathf.Floor(mapSize/2),1.5f, -0 + Mathf.Floor(mapSize/2)), Quaternion.Euler(new Vector3()))).GetComponent<UserPlayer>();
		player.gridPosition = new Vector2(0,0);
		player.playerName = "Bob";
		player.headArmor = Armor.FromKey(ArmorKey.LeatherCap);
		player.chestArmor = Armor.FromKey(ArmorKey.MagicianCloak);
		player.handWeapons.Add(Weapon.FromKey(WeaponKey.LongSword));
		
		players.Add(player);
		
		player = ((GameObject)Instantiate(UserPlayerPrefab, new Vector3((mapSize-1) - Mathf.Floor(mapSize/2),1.5f, -(mapSize-1) + Mathf.Floor(mapSize/2)), Quaternion.Euler(new Vector3()))).GetComponent<UserPlayer>();
		player.gridPosition = new Vector2(mapSize-1,mapSize-1);
		player.playerName = "Kyle";
		player.chestArmor = Armor.FromKey(ArmorKey.LeatherVest);
		player.handWeapons.Add(Weapon.FromKey(WeaponKey.ShortSword));
		player.handWeapons.Add(Weapon.FromKey(WeaponKey.ShortSword));
		
		players.Add(player);
				
		player = ((GameObject)Instantiate(UserPlayerPrefab, new Vector3(4 - Mathf.Floor(mapSize/2),1.5f, -5 + Mathf.Floor(mapSize/2)), Quaternion.Euler(new Vector3()))).GetComponent<UserPlayer>();
		player.gridPosition = new Vector2(4,5);
		player.playerName = "Lars";
		player.chestArmor = Armor.FromKey(ArmorKey.IronPlate);
		player.handWeapons.Add(Weapon.FromKey(WeaponKey.Warhammer));
		
		players.Add(player);

		player = ((GameObject)Instantiate(UserPlayerPrefab, new Vector3(8 - Mathf.Floor(mapSize/2),1.5f, -8 + Mathf.Floor(mapSize/2)), Quaternion.Euler(new Vector3()))).GetComponent<UserPlayer>();
		player.gridPosition = new Vector2(8,8);
		player.playerName = "Olivia";
		player.chestArmor = Armor.FromKey(ArmorKey.MagicianCloak);
		player.handWeapons.Add(Weapon.FromKey(WeaponKey.LongBow));
		
		players.Add(player);
		
		AIPlayer aiplayer = ((GameObject)Instantiate(AIPlayerPrefab, new Vector3(6 - Mathf.Floor(mapSize/2),1.5f, -4 + Mathf.Floor(mapSize/2)), Quaternion.Euler(new Vector3()))).GetComponent<AIPlayer>();
		aiplayer.gridPosition = new Vector2(6,4);
		aiplayer.playerName = "Bot1";
		aiplayer.chestArmor = Armor.FromKey(ArmorKey.IronHelmet);
		aiplayer.handWeapons.Add(Weapon.FromKey(WeaponKey.LongSword));
		
		players.Add(aiplayer);

		aiplayer = ((GameObject)Instantiate(AIPlayerPrefab, new Vector3(8 - Mathf.Floor(mapSize/2),1.5f, -4 + Mathf.Floor(mapSize/2)), Quaternion.Euler(new Vector3()))).GetComponent<AIPlayer>();
		aiplayer.gridPosition = new Vector2(8,4);
		aiplayer.playerName = "Bot2";
		aiplayer.handWeapons.Add(Weapon.FromKey(WeaponKey.LongSword));
		
		players.Add(aiplayer);

		aiplayer = ((GameObject)Instantiate(AIPlayerPrefab, new Vector3(12 - Mathf.Floor(mapSize/2),1.5f, -1 + Mathf.Floor(mapSize/2)), Quaternion.Euler(new Vector3()))).GetComponent<AIPlayer>();
		aiplayer.gridPosition = new Vector2(12,1);
		aiplayer.playerName = "Bot3";
		aiplayer.chestArmor = Armor.FromKey(ArmorKey.LeatherVest);
		aiplayer.handWeapons.Add(Weapon.FromKey(WeaponKey.ShortBow));
		
		players.Add(aiplayer);

		aiplayer = ((GameObject)Instantiate(AIPlayerPrefab, new Vector3(18 - Mathf.Floor(mapSize/2),1.5f, -8 + Mathf.Floor(mapSize/2)), Quaternion.Euler(new Vector3()))).GetComponent<AIPlayer>();
		aiplayer.gridPosition = new Vector2(18,8);
		aiplayer.playerName = "Bot4";
		aiplayer.handWeapons.Add(Weapon.FromKey(WeaponKey.LongSword));

		players.Add(aiplayer);
	}
}
