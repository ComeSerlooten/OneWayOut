using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class CraftArea : MonoBehaviour
{
    public bool canCraft = true;
    public bool singleUse = false;
    public bool crafted = false;
    [SerializeField] float checkRadius = 2;

    [SerializeField] List<RecipeItem> recipe;
    public List<Item> itemsInArea;
    public List<Item> currentRecipeItems;
    [SerializeField] Transform craftingCenter;
    public bool resetComponents = false;
    [Space(20)]
    [Header("Output of the Recipe")]
    [SerializeField] GameObject itemToSpawn;
    [SerializeField] Transform spawnLocation;
    [SerializeField] ParticleSystem craftingEffect;
    [Space]
    public bool runCheck = true;
    public bool checkRunning = false;
    public bool initialRunState;

    IEnumerator CheckForItems()
    {
        checkRunning = true;
        while(runCheck)
        {
            itemsInArea.Clear();
            RaycastHit[] rayHits = Physics.SphereCastAll(craftingCenter.position, checkRadius, Vector3.up, 0);
            foreach(RaycastHit r in rayHits)
            {
                if (r.transform.GetComponent<Item>()) itemsInArea.Add(r.transform.GetComponent<Item>());
            }
            UpdateCompletion();
            yield return new WaitForSeconds(.25f);
        }
        checkRunning = false;
    }

    void UpdateCompletion()
    {
        currentRecipeItems.Clear();
        foreach(RecipeItem r in recipe)
        {
            r.placed = false;
            foreach(Item i in itemsInArea)
            {
                if (i == r.item)
                {
                    r.placed = true;
                    currentRecipeItems.Add(i);
                    break;
                }
            }
        }

        if (AllItemsPlaced() && !(singleUse && crafted) && canCraft) StartCoroutine(RecipeStartDelay());
    }

     bool AllItemsPlaced()
    {
        bool allDone = true;
        if (recipe.Count == 0) allDone = false;
        else foreach (RecipeItem r in recipe)
        {
            if (!r.placed) allDone = false;
        }
        return allDone;
    }

    IEnumerator RecipeStartDelay()
    {
        StopCoroutine(CheckForItems());
        initialRunState = runCheck;
        runCheck = false;
        yield return new WaitForSeconds(1f);
        if (AllItemsPlaced()) StartCoroutine(AssembleIngredients());
        else runCheck = initialRunState;
        yield break;
    }

    IEnumerator AssembleIngredients()
    {
        foreach(Item i in currentRecipeItems)
        {
            if (i.GetComponent<Collider>()) i.GetComponent<Collider>().enabled = false;
            Vector3 toCenter = (craftingCenter.position - i.transform.position).normalized;
            toCenter.y = 0;

            i.transform.DOJump(craftingCenter.position, .5f, 1, 1).SetEase(Ease.InOutSine);
        }

        itemsInArea.Clear();
        //UpdateCompletion();

        yield return new WaitForSeconds(.95f);
        foreach (Item i in currentRecipeItems)
        {
            i.transform.DOKill();
        }

        RecipeOutput();

        yield break;
    }

    void RecipeOutput()
    {
        
        foreach (Item i in currentRecipeItems)
        {
            i.transform.DOKill();
            if (resetComponents)
            {
                i.ResetItem();
                if (i.GetComponent<Collider>()) i.GetComponent<Collider>().enabled = true;
            }
            else
            {
                i.gameObject.SetActive(false);
            }
        }


        Instantiate(itemToSpawn, spawnLocation.position, spawnLocation.rotation);

        StartCoroutine(RetryDelay());
       
        if (craftingEffect) Instantiate(craftingEffect, spawnLocation.position, spawnLocation.rotation);
        
    }

    IEnumerator RetryDelay()
    {
        currentRecipeItems.Clear();
        itemsInArea.Clear();

        yield return new WaitForSeconds(.5f);

        runCheck = initialRunState;

        yield break;
    }

    // Update is called once per frame
    void Update()
    {
        if (runCheck && !checkRunning) StartCoroutine(CheckForItems());
    }
}

[Serializable]
public class RecipeItem
{
    public Item item;
    public bool placed = false;
}