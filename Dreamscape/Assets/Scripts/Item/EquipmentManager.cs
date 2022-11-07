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

	public Equipment[] defaultItems;
	public SkinnedMeshRenderer targetMesh;
	Equipment[] currentEquipment;   // Предметы которые у нас сейчас в инвентаре
	SkinnedMeshRenderer[] currentMeshes;

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

		EquipDefaultItems();
	}

	// экипируем новый предмет
	public void Equip(Equipment newItem)
	{
		// Находим в какос слоте находится предмет
		int slotIndex = (int)newItem.equipSlot;
		Equipment oldItem = Unequip(slotIndex);

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
		// проверка на наличие предмета
		if (currentEquipment[slotIndex] != null)
		{
			if (currentMeshes[slotIndex] != null)
			{
				Destroy(currentMeshes[slotIndex].gameObject);
			}
			// добавляем предмет в инвентарь
			Equipment oldItem = currentEquipment[slotIndex];
			inventory.AddItem(oldItem);

			// убираем из массива жкипированных вещей
			currentEquipment[slotIndex] = null;

			//тк предмет сняли опять callback
			if (onEquipmentChanged != null)
			{
				onEquipmentChanged.Invoke(null, oldItem);
			}
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
		EquipDefaultItems();
	}

	
	//экипировка дефолтной одежды 
	void EquipDefaultItems()
	{
		foreach (Equipment item in defaultItems)
		{
			Equip(item);
		}
	}

	void Update()
	{
		// при нажатии U все снимается
		if (Input.GetKeyDown(KeyCode.U))
			UnequipAll();
	}

	public void SaveEquipment()
    {

    }

}
