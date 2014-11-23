using UnityEngine;
using System.Collections;

public enum ArmorSlotType {
	Head,
	Chest,
	Gauntlet,
	Leg
}

public enum ArmorKey {
	//head
	LeatherCap,
	IronHelmet,
	MagicianHat,
	//chest
	LeatherVest,
	IronPlate,
	MagicianCloak,
	//gauntlet
	LeatherGauntlet,
	IronGauntlet,
	MagicianRing,
	//leg
	LeatherBoots,
	IronBoots,
	MagicianBoots,
}

public class Armor : Item {
	public ArmorSlotType type;

	public static Armor FromKey (ArmorKey key) {
		Armor ret = new Armor();
		switch(key) {
		//head
		case ArmorKey.LeatherCap: 
			ret = new Armor() {
				type = ArmorSlotType.Head,

				alterAttackChance = 0,
				alterAttackRange = 0,
				alterDamageReduction = 1,
				alterDamageBase = 0,
				alterDamageRollSides = 0,
				alterEvade = 0,
				alterMovementPerActionPoint = 0
			};
			break;			
		case ArmorKey.IronHelmet: 
			ret = new Armor() {
				type = ArmorSlotType.Head,

				alterAttackChance = 0,
				alterAttackRange = 0,
				alterDamageReduction = 3,
				alterDamageBase = 0,
				alterDamageRollSides = 0,
				alterEvade = -0.15f,
				alterMovementPerActionPoint = -1
			};
			break;
			
		case ArmorKey.MagicianHat: 
			ret = new Armor() {
				type = ArmorSlotType.Head,

				alterAttackChance = 0,
				alterAttackRange = 0,
				alterDamageReduction = 0,
				alterDamageBase = 0,
				alterDamageRollSides = 0,
				alterEvade = 0.15f,
				alterMovementPerActionPoint = 1
			};
			break;
		//chest
		case ArmorKey.LeatherVest: 
			ret = new Armor() {
				type = ArmorSlotType.Chest,

				alterAttackChance = 0,
				alterAttackRange = 0,
				alterDamageReduction = 2,
				alterDamageBase = 0,
				alterDamageRollSides = 0,
				alterEvade = 0,
				alterMovementPerActionPoint = -1
			};
			break;
			
		case ArmorKey.IronPlate: 
			ret = new Armor() {
				type = ArmorSlotType.Chest,

				alterAttackChance = 0,
				alterAttackRange = 0,
				alterDamageReduction = 3,
				alterDamageBase = 0,
				alterDamageRollSides = 0,
				alterEvade = -0.15f,
				alterMovementPerActionPoint = -2
			};
			break;
			
		case ArmorKey.MagicianCloak: 
			ret = new Armor() {
				type = ArmorSlotType.Chest,

				alterAttackChance = 0,
				alterAttackRange = 0,
				alterDamageReduction = 0,
				alterDamageBase = 0,
				alterDamageRollSides = 0,
				alterEvade = 0.15f,
				alterMovementPerActionPoint = 2
			};
			break;
		//gauntlets	
		case ArmorKey.LeatherGauntlet: 
			ret = new Armor() {
				type = ArmorSlotType.Gauntlet,

				alterAttackChance = 0,
				alterAttackRange = 0,
				alterDamageReduction = 1,
				alterDamageBase = 0,
				alterDamageRollSides = 0,
				alterEvade = 0,
				alterMovementPerActionPoint = 0
			};
			break;
			
		case ArmorKey.IronGauntlet: 
			ret = new Armor() {
				type = ArmorSlotType.Gauntlet,

				alterAttackChance = 0,
				alterAttackRange = 0,
				alterDamageReduction = 3,
				alterDamageBase = 0,
				alterDamageRollSides = 0,
				alterEvade = -0.15f,
				alterMovementPerActionPoint = 0
			};
			break;
			
		case ArmorKey.MagicianRing: 
			ret = new Armor() {
				type = ArmorSlotType.Gauntlet,

				alterAttackChance = 0,
				alterAttackRange = 0,
				alterDamageReduction = 0,
				alterDamageBase = 0,
				alterDamageRollSides = 0,
				alterEvade = 0.15f,
				alterMovementPerActionPoint = 1
			};
			break;
		//legs
			
		case ArmorKey.LeatherBoots: 
			ret = new Armor() {
				type = ArmorSlotType.Leg,

				alterAttackChance = 0,
				alterAttackRange = 0,
				alterDamageReduction = 1,
				alterDamageBase = 0,
				alterDamageRollSides = 0,
				alterEvade = 0,
				alterMovementPerActionPoint = 0
			};
			break;
			
		case ArmorKey.IronBoots: 
			ret = new Armor() {
				type = ArmorSlotType.Leg,

				alterAttackChance = 0,
				alterAttackRange = 0,
				alterDamageReduction = 3,
				alterDamageBase = 0,
				alterDamageRollSides = 0,
				alterEvade = -0.15f,
				alterMovementPerActionPoint = -2
			};
			break;
			
		case ArmorKey.MagicianBoots: 
			ret = new Armor() {
				type = ArmorSlotType.Leg,

				alterAttackChance = 0,
				alterAttackRange = 0,
				alterDamageReduction = 0,
				alterDamageBase = 0,
				alterDamageRollSides = 0,
				alterEvade = 0.15f,
				alterMovementPerActionPoint = 2
			};
			break;
		}
		return ret;
	}
}