using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Keep track of equipment. Has functions for adding and removing items. */

public class EquipmentManager : MonoBehaviour
{

	#region Singleton

	public static EquipmentManager instance;

	void Awake()
	{
		instance = this;
	}

	#endregion

	int ind;
	public Equipment[] defaultItems;
	public Equipment[] allItems;
	public OutfitManager[] defaultOutfits;
	public SkinnedMeshRenderer targetMesh;
	Equipment[] currentEquipment;   // Предметы которые у нас сейчас в инвентаре
	SkinnedMeshRenderer[] currentMeshes;
	public int itemInHand;
	public bool equiped;
	public string nameEq;
	// Обратный вызов когда предмет экипирован или неэкипирован
	public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
	public OnEquipmentChanged onEquipmentChanged;

	InventoryPl inventory;    // наш иннвентарь

	void Start()
	{
		inventory = InventoryPl.instance;

		// Инициализируем currentEquipment на основе количества слотов для экипировки
		int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
		currentEquipment = new Equipment[numSlots];
		currentMeshes = new SkinnedMeshRenderer[numSlots];
        
		LoadEquipment();
		if (equiped) 
		{
			itemInHand = 1;
        }
        else
        {
			itemInHand = 0;
        }
		EquipDefaultItems();
	}

	// экипируем новый предмет
	public void Equip(Equipment newItem)
	{
		equiped = true;

		itemInHand = 1;
		Debug.Log(equiped);
		// Находим в каком слоте находится предмет
		int slotIndex = (int)newItem.equipSlot;
		Debug.Log(slotIndex);
		Equipment oldItem = Unequip(slotIndex);
		ind = slotIndex;
		// Экипировали предмет значит делаем callback(обратный вызов)
		if (onEquipmentChanged != null)
		{
			onEquipmentChanged.Invoke(newItem, oldItem);
		}


		// Вставляем предмет в слот
		currentEquipment[slotIndex] = newItem;
		SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(newItem.mesh);
		newMesh.transform.parent = targetMesh.transform;

		newMesh.bones = targetMesh.bones;
		newMesh.rootBone = targetMesh.rootBone;
		currentMeshes[slotIndex] = newMesh;
	}

	// Снимаем предмет с определенным индексом
	public Equipment Unequip(int slotIndex)
	{
		Debug.Log(slotIndex);
		// проверка на наличие предмета
		if (currentEquipment[slotIndex] != null)
		{
			if (currentMeshes[slotIndex] != null)
			{
				Debug.Log("Removed");
				Destroy(currentMeshes[slotIndex].gameObject);
			}
			// добавляем предмет в инвентарь
			itemInHand = 0;
			equiped = false;
			Equipment oldItem = currentEquipment[slotIndex];
			inventory.AddItem(oldItem);

			// убираем из массива жкипированных вещей
			currentEquipment[slotIndex] = null;

			//тк предмет сняли опять callback
			if (onEquipmentChanged != null)
			{
				onEquipmentChanged.Invoke(null, oldItem);
			}
			Debug.Log(equiped);
			return oldItem;
		}
		
		return null;
	}

	// снимаем все предметы
	public void UnequipAll()
	{
		for (int i = 0; i < currentEquipment.Length; i++)
		{
			Unequip(i);
		}
		ChangeMaterial.instance.ChangeMatBack();
		EquipDefaultItems();
	}

	
	//экипировка дефолтной одежды 
	void EquipDefaultItems()
	{
		foreach (Equipment item in defaultItems)
		{
			Equip(item);
		}
		foreach (OutfitManager outfit in defaultOutfits) // меняем текстуру персонажа на основную
		{
			ChangeMaterial.instance.ChangeMat(outfit.ind,outfit.itemMaterial, outfit);
		}
	}

	void Update()
	{
		// при нажатии U все снимается
		if (Input.GetKeyDown(KeyCode.U))
			UnequipAll();
	}

	public void SaveEquipment() // сохраняем данные об экипировынных предметах 
    {
		Debug.Log(equiped);
		if (equiped || itemInHand == 1)
		{
			Debug.Log("ItemInHand saved");
			itemInHand = 1;
			PlayerPrefs.SetInt("bool", 1); 
			Debug.Log(currentEquipment[ind].name);
			nameEq = currentEquipment[ind].name;
			PlayerPrefs.SetString("eqName", nameEq);
			PlayerPrefs.SetInt("currentEqInd", ind);

        }
        else
        {
			itemInHand = 0;
			PlayerPrefs.SetInt("bool", 0);
		}
    }
	public void LoadEquipment() // щагружаем данные об экипированных предметах
    {

		int ifInHand = PlayerPrefs.GetInt("bool", itemInHand);
		Debug.Log(ifInHand);
        if (ifInHand==1)
        {
			int tmp = PlayerPrefs.GetInt("currentEqInd", ind);
			string eqName = PlayerPrefs.GetString("eqName", nameEq);
			Debug.Log(eqName);
			for (int i = 0; i < allItems.Length; i++)
			{
				if (allItems[i].name == eqName)
				{
					Debug.Log("Equiped");
					currentEquipment[ind] = allItems[i];
					break;
				}
			}
		    SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(currentEquipment[ind].mesh);
			newMesh.transform.parent = targetMesh.transform;

			newMesh.bones = targetMesh.bones;
			newMesh.rootBone = targetMesh.rootBone;
			currentMeshes[ind] = newMesh;
			equiped = true;
			//Equip(currentEquipment[ind]);
		}
        else 
		{
			Debug.Log("no items equiped");
		}
	}
}
