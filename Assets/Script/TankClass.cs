using UnityEngine;
using System.Collections.Generic;

public enum ClassEnum {
	Light,
	Medium,
	Heavy,
	TankDestroyer,
	IFV,
	APC,
	SPA,
	MLRS
}
public enum NationEnum {
	USA,
	USSR,
	Germany,
	UK,
	France,
	Italy,
	Sweden,
	Japan,
	China,
	Poland,
	Czechoslovakia,
	Finland
}
public enum ShellType {
	AP, //Сплошняк
	APHE, //Каморник
	HEAT, //Кумулятив
	HE, //Фугасный
	ATGM, //ПТУР
	MLRS,

}
[CreateAssetMenu(fileName = "NewTank", menuName = "Tank")]
public class Tank : ScriptableObject
{
	[Header("Основная информация")]
	public string InternalName;
	public string DisplayName; 
	public int Level; 
	public Sprite Icon;
	public ClassEnum Class;
	public NationEnum Nation;

	[Header("Экономика")]
	public int SilverCost; 
	public int GoldCost; 
	public int XPCost; 
	public float IncomeModifier;
	public bool IsPremium;

	[Header("Живучесть")]
   	public int Health;
	public int CurrentHealth;
	public int DisplayArmorTurretFront;
	public int DisplayArmorTurretSide;
	public int DisplayArmorTurretBack;
	public int DisplayArmorHullFront;
	public int DisplayArmorHullSide;
	public int DisplayArmorHullBack;
	public int IgnitionProbability;

	[Header("Обзор и маскировка")]
	public float SpotRadius;
	public float DisguiseStill;
	public float DisguiseMoving;
	public float DisguiseShooting;

	[Header("Орудие")]
	public float Cooldown;
	public Dictionary<ShellType, float> ShellPenetration;
	public Dictionary<ShellType, float> ShellDamage;
	public Dictionary<ShellType, float> ShellSpeed;
	public float WeaponBendLimitUp;
	public float WeaponBendLimitDown;
	public float WeaponBendLimitRight;
	public float WeaponBendLimitLeft;

	[Header("Подвижность")]
	public float MaxSpeedForwards;
	public float MaxSpeedBackwards;
	public float Mass;
	public float RotationSpeedHull;
	public float RotationSpeedTurret;
	public float PassabilitySolid;
	public float PassabilitySoft;
	public float PassabilityWater;

	[Header("3D-объекты")]
	public GameObject HullModel;
}