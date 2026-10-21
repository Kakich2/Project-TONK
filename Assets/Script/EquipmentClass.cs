using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewEquipment", menuName = "Equipment Unit")]
public class Equipment : ScriptableObject
{
	//Общие параметры
	public string InternalName;
	public string DisplayName; 
	public string Description;
	public int OpenLevel; 
	//Экономические параметры
	public int SilverCost; 
	//Графика
	public Sprite Icon;
	//Список бонусов
	public Dictionary<string, float> BonusList;
}