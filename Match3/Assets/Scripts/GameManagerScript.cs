using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour {

	public int gridWidth = 8;
	public int gridHeight = 8;
	public float tokenSize = 1;

	public int randomCap;

	//protected MatchManagerScript matchManager;
	protected InputManagerScript inputManager;
	//protected RepopulateScript repopulateManager;
	protected MoveTokensScript moveTokenManager;

	public GameObject grid;
	public GameObject tokenPrefab;
	public GameObject bluePrefab;
	public GameObject redPrefab;
	public GameObject yellowPrefab;


	//declaring a MULTI-DIMENSIONAL ARRAY; it's a GRID
	public GameObject[,] gridArray;
	protected Object[] flowerTypes;

	// used in InputMangerScript
	GameObject selected;

	public virtual void Start () {
		//load the flowers
		//flowerTypes = (Object[])Resources.LoadAll("flowers/");

		//make the grid
		gridArray = new GameObject[gridWidth, gridHeight];
		MakeGrid();

		//create references to the other scripts
		//matchManager = GetComponent<MatchManagerScript>();
		inputManager = GetComponent<InputManagerScript>();
		//repopulateManager = GetComponent<RepopulateScript>();
		moveTokenManager = GetComponent<MoveTokensScript>();
	}

	public virtual void Update(){
		inputManager.SelectToken();
	}
		
	/// <summary>
	/// This creates the grid, fills it with tokens, and makes the GameObjects children of a new GameObject, TokenGrid.
	/// It looks like this should only be called once a game.
	/// </summary>
	void MakeGrid() {
		//creates a GameObject called TokenGrid, then makes all of the tokens as children of it
		//this keeps our hierarchy neat
		grid = new GameObject("TokenGrid");
		//then, fill the grid with tokens
		for(int x = 0; x < gridWidth; x++){
			for(int y = 0; y < gridHeight; y++){
				AddTokenToPosInGrid(x, y, grid);
			}
		}
	}

	//checks whether there is an empty space in the grid
	public virtual bool GridHasEmpty(){
		for(int x = 0; x < gridWidth; x++){
			for(int y = 0; y < gridHeight ; y++){
				if(gridArray[x, y] == null){
					return true;
				}
			}
		}
		return false;
	}

	/// <summary>
	/// Gets the position of a specific token
	/// </summary>
	/// 
	/// <param name="token">A token GameObject, 'cause Matt likes sports</param>
	/// <returns>The Vector2 coordinate of a token in the grid</returns>
	public Vector2 GetPositionOfTokenInGrid(GameObject token){
		for(int x = 0; x < gridWidth; x++){
			for(int y = 0; y < gridHeight ; y++){
				if(gridArray[x, y] == token){
					return(new Vector2(x, y));
				}
			}
		}
		return new Vector2();
	}
		
	/// <summary>
	/// Creates a random token into the grid as a child at the given coordinate 
	/// </summary>
	/// 
	/// <param name="x">An int x that is the x coordinate in the grid</param>
	/// <param name="y">An int y that is the y coordinate in the grid</param>
	/// <param name="parent">The parent of this token. It should be TokenGrid</param> 
	public void AddTokenToPosInGrid(int x, int y, GameObject parent){
		Vector3 position = GetWorldPositionFromGridPosition(x, y);
		GameObject token = Instantiate(tokenPrefab, position, Quaternion.identity) as GameObject;
		token.transform.parent = parent.transform;
		//put this token into the array of tokens
		gridArray[x, y] = token;

		//create a random kind of flower at the position in the grid with the same rotation
		int i = Random.Range (0,randomCap);

		if(i==0){
			GameObject flower = Instantiate(bluePrefab, position, Quaternion.identity) as GameObject;
			flower.transform.parent = parent.transform;
			//give the tile position to the object script so we can know which tile's color the flower is changing
			flower.GetComponent<Blue> ().tileOn = token;

			token.GetComponent<Token> ().flowerOn = flower;
		}

		if(i==1){
			GameObject flower = Instantiate(redPrefab, position, Quaternion.identity) as GameObject;
			flower.transform.parent = parent.transform;
			flower.GetComponent<Red> ().tileOn = token;

			token.GetComponent<Token> ().flowerOn = flower;
		}

		if(i==2){
			GameObject flower = Instantiate(yellowPrefab, position, Quaternion.identity) as GameObject;
			flower.transform.parent = parent.transform;
			flower.GetComponent<Yellow> ().tileOn = token;

			token.GetComponent<Token> ().flowerOn = flower;
		}
	}

	/// <summary>
	/// Converts a grid position into a Worldspace Position
	/// </summary>
	/// 
	/// <param name="x">An int x that is the x coordinate in the grid</param>
	/// <param name="y">An int y that is the y coordinate in the grid</param>
	/// <returns>A Vector2 coordinate in Worldspace</returns>
	public Vector2 GetWorldPositionFromGridPosition(int x, int y){
		return new Vector2(
			(x - gridWidth/2) * tokenSize,
			(y - gridHeight/2) * tokenSize);
	}


}
