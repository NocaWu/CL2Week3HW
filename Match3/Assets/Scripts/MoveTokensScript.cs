using UnityEngine;
using System.Collections;

public class MoveTokensScript : MonoBehaviour
{
    protected GameManagerScript gameManager; // A reference to the GameManagerScript component on this script's gameObject.

    public bool move = false; //selected tokens are moving?

    public float lerpPercent; //how much % the tokens have moved from their original positions
    public float lerpSpeed; //How quickly the lerp % is increased

    bool userSwap; //Have the user tokens been swapped or not??

    [SerializeField] protected GameObject exchangeToken1; //A reference to the GameObject token that the user clicked first.
	[SerializeField] GameObject exchangeToken2; //A reference to the GameObject token that the user clicked second.

    Vector2 exchangeGridPos1; //The position of exchangeToken1 in the scene.
    Vector2 exchangeGridPos2; //The position of exchangeToken2 in the scene.

    public virtual void Start()
    {
        gameManager = GetComponent<GameManagerScript>(); //Set the gameManager variable to the GameManagerScript component on the GameObject.
        lerpPercent = 0; //Reset the lerp percentage to 0 so that tokens don't move until the right time.
    }

    public virtual void Update()
    {
		if (move){ //If the tokens are moving:
            lerpPercent += lerpSpeed; 

            if (lerpPercent >= 1) {
                lerpPercent = 1; 

				move = false;
            }

			if (exchangeToken1 != null){ //If exchangeToken1 is a valid GameObject:
            ExchangeTokens(); //run the ExchangeTokens function.
			}
        }
    }

    /// <summary>
    /// resets the lerp percentage before setting up the tokens through SetupTokenExchange.
    /// </summary>
    public void SetupTokenMove()
    {
        move = true; //The tokens are now moving.
        lerpPercent = 0; //Reset the lerp percentage to 0 so that tokens don't move until the right time.

    }

    /// <summary>
    /// This function sets up the exchange of two tokens.
    /// </summary>
    /// <param name="token1">The first token you clicked on.</param>
    /// <param name="pos1">The location of token1 in the game scene.</param>
    /// <param name="token2">The second token you clicked on.</param>
    /// <param name="pos2">The location of token2 in the game scene.</param>
    /// <param name="reversable">Can this exchange be reversed? (Useful if the tokens don't match for a match 3.)</param>
	public void SetupTokenExchange(GameObject token1, Vector2 pos1,
                                   GameObject token2, Vector2 pos2, bool reversable)
    {
        SetupTokenMove(); //Run SetupTokenMove so that the tokens will start moving.

        exchangeToken1 = token1; //Set exchangeToken1 to the first token you clicked on.
        exchangeToken2 = token2; //Set exchangeToken2 to the second token you clicked on.

		exchangeGridPos1 = pos1; //Set exchangeGridPos1 to the position of the first token you clicked on.
        exchangeGridPos2 = pos2; //Set exchangeGridPos2 to the position of the second token you clicked on.

        this.userSwap = reversable; //Let the game know that the tokens have been swapped.
    }

    /// <summary>
    /// Exchange the two tokens that have been selected.
    /// </summary>
    public virtual void ExchangeTokens()
    {
		//get the swaping tokens' positions
        Vector3 startPos = gameManager.GetWorldPositionFromGridPosition((int)exchangeGridPos1.x, (int)exchangeGridPos1.y);
        Vector3 endPos = gameManager.GetWorldPositionFromGridPosition((int)exchangeGridPos2.x, (int)exchangeGridPos2.y);

 		//move tokens with smoothlerp
        Vector3 movePos1 = SmoothLerp(startPos, endPos, lerpPercent);
        Vector3 movePos2 = SmoothLerp(endPos, startPos, lerpPercent);

        exchangeToken1.transform.position = movePos1;
        exchangeToken2.transform.position = movePos2;

		//swap tokens and let flowers on them know
        if (lerpPercent == 1) 
        {
			/*
            The next two lines simply swap exchangeToken1 and exchangeToken2's positions in the grid array of the gameManager script.
            */
			gameManager.gridArray[(int)exchangeGridPos2.x, (int)exchangeGridPos2.y] = exchangeToken1;
			gameManager.gridArray[(int)exchangeGridPos1.x, (int)exchangeGridPos1.y] = exchangeToken2;

			GameObject flower1 = exchangeToken2.GetComponent<Token> ().flowerOn;
			GameObject flower2 = exchangeToken1.GetComponent<Token> ().flowerOn;

			//swap the flowers on it
			gameManager.gridArray[(int)exchangeGridPos2.x, (int)exchangeGridPos2.y].GetComponent<Token>().flowerOn = flower1; 
			gameManager.gridArray[(int)exchangeGridPos1.x, (int)exchangeGridPos1.y].GetComponent<Token>().flowerOn = flower2;

			if (flower1.name == "red(Clone)") 
			{
				gameManager.gridArray [(int)exchangeGridPos2.x, (int)exchangeGridPos2.y].GetComponent<Token> ().flowerOn.GetComponent<Red> ().tileOn = gameManager.gridArray [(int)exchangeGridPos2.x, (int)exchangeGridPos2.y];
			}
			else
				if (flower1.name == "blue(Clone)") 
				{
				gameManager.gridArray [(int)exchangeGridPos2.x, (int)exchangeGridPos2.y].GetComponent<Token> ().flowerOn.GetComponent<Blue> ().tileOn = gameManager.gridArray [(int)exchangeGridPos2.x, (int)exchangeGridPos2.y];
				}	
				else
					if (flower1.name == "yellow(Clone)") 
					{
				gameManager.gridArray [(int)exchangeGridPos2.x, (int)exchangeGridPos2.y].GetComponent<Token> ().flowerOn.GetComponent<Yellow> ().tileOn = gameManager.gridArray [(int)exchangeGridPos2.x, (int)exchangeGridPos2.y];
					}	

			if (flower2.name == "red(Clone)")
			{
				gameManager.gridArray [(int)exchangeGridPos1.x, (int)exchangeGridPos1.y].GetComponent<Token> ().flowerOn.GetComponent<Red> ().tileOn = gameManager.gridArray [(int)exchangeGridPos1.x, (int)exchangeGridPos1.y];
			}
			else
				if (flower2.name == "blue(Clone)") 
				{
					gameManager.gridArray[(int)exchangeGridPos1.x, (int)exchangeGridPos1.y].GetComponent<Token> ().flowerOn.GetComponent<Blue> ().tileOn = gameManager.gridArray[(int)exchangeGridPos1.x, (int)exchangeGridPos1.y];
				}	
				else
					if (flower2.name == "yellow(Clone)") 
					{
						gameManager.gridArray[(int)exchangeGridPos1.x, (int)exchangeGridPos1.y].GetComponent<Token> ().flowerOn.GetComponent<Yellow> ().tileOn = gameManager.gridArray[(int)exchangeGridPos1.x, (int)exchangeGridPos1.y];
					}	
        }
    }

    /// <summary>
    /// Allows a smooth linear interpolation (lerp) between two positions.
    /// </summary>
    /// <param name="startPos">Starting position to lerp from.</param>
    /// <param name="endPos">Ending position to lerp to.</param>
    /// <param name="lerpPercent">How far in the lerp (as a percentage) we need to reach.</param>
    /// <returns>Returns the position that matches the percentage that the object has moved.</returns>
    private Vector3 SmoothLerp(Vector3 startPos, Vector3 endPos, float lerpPercent)
    {
        return new Vector3(
            Mathf.SmoothStep(startPos.x, endPos.x, lerpPercent),
            Mathf.SmoothStep(startPos.y, endPos.y, lerpPercent),
            Mathf.SmoothStep(startPos.z, endPos.z, lerpPercent));
    }
}
