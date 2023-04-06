using UnityEngine;
using System.Collections.Generic;

//[CreateAssetMenu()]
// we comment this because we only need one list to be made
public class RecipeListSO : ScriptableObject {
	// We make this an SO so we can have one gameobject containing 
	// In DeliveryManager, instead of having a list of all recipe, we can reference only one object
	// This also make it so that when there are other gameobjects that need a list of recipes, we dont do double reference.
	   // Having two gameobject referencing two different list means we have to update the list twice if there is a change.
	public List<RecipeSO> recipeSOList;
}
