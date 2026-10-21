using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankManager : MonoBehaviour
{
	[SerializeField] private List<Tank> AllTanks = new List<Tank>();
	[SerializeField] private List<Tank> ResearchedTanks = new List<Tank>();	
	[SerializeField] private List<Tank> PurchasedTanks = new List<Tank>();	
	[SerializeField] private Transform ContentContainer;
	[SerializeField] private GameObject TankIconPrefab; //Иконка которая сгенерируется
	[SerializeField] private GameObject TankPrefab;	//Префаб самого танка
		
    // Start is called before the first frame update
	void CreateTestTank() {
		var TestTank = Instantiate(TankPrefab);
		var TestTankIcon = Instantiate(TankIconPrefab);
	
	}
	
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
