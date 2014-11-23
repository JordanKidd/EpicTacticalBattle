using UnityEngine;
using System.Collections;

public enum WeaponSlotType {
	OneHanded,
	TwoHanded
}

public enum WeaponKey {
	ShortSword,
	Shield,

	LongSword,
	Warhammer,

	ShortBow,
	LongBow,
}

public class Weapon : Item {
	public WeaponSlotType type;

	public static Weapon FromKey (WeaponKey key) {
		Weapon ret = new Weapon();
		switch (key) {
		case WeaponKey.ShortSword:
			ret = new Weapon() {
				type = WeaponSlotType.OneHanded,

				alterAttackChance = 0.15f,
				alterAttackRange = 1,
				alterDamageReduction = 0,
				alterDamageBase = 3,
				alterDamageRollSides = 5,
				alterEvade = 0,
				alterMovementPerActionPoint = 0
			};
			break;
		case WeaponKey.Shield:
			ret = new Weapon() {
				type = WeaponSlotType.OneHanded,
				
				alterAttackChance = -0.15f,
				alterAttackRange = 0,
				alterDamageReduction = 2,
				alterDamageBase = 0,
				alterDamageRollSides = 0,
				alterEvade = 0,
				alterMovementPerActionPoint = 0
			};
			break;
		case WeaponKey.LongSword:
			ret = new Weapon() {
				type = WeaponSlotType.TwoHanded,
				
				alterAttackChance = 0.15f,
				alterAttackRange = 1,
				alterDamageReduction = 0,
				alterDamageBase = 6,
				alterDamageRollSides = 7,
				alterEvade = -0.05f,
				alterMovementPerActionPoint = 0
			};
			break;			
		case WeaponKey.Warhammer:
			ret = new Weapon() {
				type = WeaponSlotType.TwoHanded,
				
				alterAttackChance = 0.0f,
				alterAttackRange = 1,
				alterDamageReduction = 0,
				alterDamageBase = 3,
				alterDamageRollSides = 15,
				alterEvade = -0.15f,
				alterMovementPerActionPoint = -1
			};
			break;			
		case WeaponKey.ShortBow:
			ret = new Weapon() {
				type = WeaponSlotType.TwoHanded,
				
				alterAttackChance = 0.15f,
				alterAttackRange = 5,
				alterDamageReduction = 0,
				alterDamageBase = 3,
				alterDamageRollSides = 4,
				alterEvade = -0.0f,
				alterMovementPerActionPoint = 0
			};
			break;		
		case WeaponKey.LongBow:
			ret = new Weapon() {
				type = WeaponSlotType.TwoHanded,
				
				alterAttackChance = 0.2f,
				alterAttackRange = 10,
				alterDamageReduction = 0,
				alterDamageBase = 6,
				alterDamageRollSides = 7,
				alterEvade = -0.0f,
				alterMovementPerActionPoint = 0
			};
			break;
		}
		return ret;
	}
}
