using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{
    #region Singleton

    public static ChangeMaterial instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    public OutfitManager[] outfits;
    public SkinnedMeshRenderer body;
    public SkinnedMeshRenderer hat;
    public SkinnedMeshRenderer head;
    public SkinnedMeshRenderer dress;
    InventoryPl inventory;
    OutfitManager oldOutfit;
    bool outfitOn;
    int index;
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.DeleteAll();
        inventory = InventoryPl.instance;
    }

    private void Update() //при нажатии U меняем тектуру на обычную
    {
        if (Input.GetKeyDown(KeyCode.U) && outfitOn)
        {
            Debug.Log(index);
            outfitOn = false;
            ChangeMatBack();
        }
    }

    // Update is called once per frame
    public void ChangeMat(int i, Material mat, OutfitManager outfit) // меняем материал персонажа на новый
    {
        //Debug.Log(i);
        index = i;
        if (outfitOn && i != 0)
        {
            inventory.AddItem(oldOutfit);

        }
        body.material = outfits[i].itemMaterial;
        head.material = outfits[i].itemMaterial;
        hat.material = outfits[i].itemMaterial;
        dress.material = outfits[i].itemMaterial;
        oldOutfit = outfits[index];
        Debug.Log(oldOutfit.name);
        outfitOn = true;

    }
    public void ChangeMatBack() // меняем материал на обычный
    {
        index = 0;
        body.material = outfits[0].itemMaterial;
        head.material = outfits[0].itemMaterial;
        hat.material = outfits[0].itemMaterial;
        dress.material = outfits[0].itemMaterial;
        inventory.AddItem(oldOutfit);
    }
    public void SaveClothes() //сохраняем данные об одежде
    {
        PlayerPrefs.SetInt("matId", index);
        PlayerPrefs.SetString("outfitName", outfits[index].name);
        Debug.Log(index);
    }
    public void LoadClothes() // загружаем данные об одежде
    {
        int tmpInd = PlayerPrefs.GetInt("matId", index);
        string nameObj =PlayerPrefs.GetString("outfitName", outfits[index].name);
        if (tmpInd == 0)
        {
            index = 0;
            outfitOn = false;
            body.material = outfits[0].itemMaterial;
            head.material = outfits[0].itemMaterial;
            hat.material = outfits[0].itemMaterial;
            dress.material = outfits[0].itemMaterial;
        }
        else
        {
            outfitOn = true;
            index = tmpInd;
            oldOutfit = outfits[index];
            Debug.Log(tmpInd);
            body.material = outfits[tmpInd].itemMaterial;
            head.material = outfits[tmpInd].itemMaterial;
            hat.material = outfits[tmpInd].itemMaterial;
            dress.material = outfits[tmpInd].itemMaterial;
        }
    }
}
