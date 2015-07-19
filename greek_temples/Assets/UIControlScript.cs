using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIControlScript : MonoBehaviour {
	

	public GameObject ColumnZero;
	public GameObject ColumnSixteen;
	public GameObject ColumnTwenty;
	public GameObject ColumnTwentyFour;

	public GameObject Pediment;
	public GameObject Roof;
	public GameObject CellaWalls;
	public GameObject TempleBase;

	public GameObject CapitalDoric;
	public GameObject CapitalIonic;
	public GameObject CapitalIonicDiagonal;
	public GameObject CapitalCorinthian;

	public GameObject Metope;
	public GameObject Triglyph;

	public Button Generate;

	public ToggleGroup NumColumns;
	public Toggle Distyle;
	public Toggle Tetrastyle;
	public Toggle Hexastyle;
	public Toggle Octastyle;

	public ToggleGroup ColumnDepth;
	public Toggle CDStandard;
	public Toggle CDCustom;

	public ToggleGroup TempleOrder;
	public Toggle OrderDoric;
	public Toggle OrderIonic;
	public Toggle OrderCorinthian;

	public ToggleGroup Frontal;
	public Toggle DivAntis;
	public Toggle DivProstyle;
	public Toggle DivAmphiprostyle;

	public ToggleGroup Fluting;
	public Toggle FluteZero;
	public Toggle FluteSixteen;
	public Toggle FluteTwenty;
	public Toggle FluteTwentyFour;

	public ToggleGroup Peristyle;
	public Toggle PeriNone;
	public Toggle PeriPeripteral;
	public Toggle PeriDipteral;
	public Toggle PeriPseudoperipteral;

	public ToggleGroup ColumnSpace;
	public Toggle SpacePicno;
	public Toggle SpaceSys;
	public Toggle SpaceEu;
	public Toggle SpaceDia;
	public Toggle SpaceAraeo;

	private int numColumns;
	private int columnDepth;
	private float columnHeight;
	private float columnSpacing;
	private int columnOrder;

	private GameObject currentCapital;
	private GameObject currentFluting;

	private int columnFluting;
	private int peristyle;

	private float module;
	private float columnDiameter;
	private float intercolumnation;

	private GameObject temple;
	private GameObject temple_columns;
	private GameObject temple_entablature;
	private GameObject temple_roof;
	private GameObject temple_walls;
	private GameObject temple_base;





	//public GameObject entablature?  Have to figure that out.  

	// Use this for initialization
	void Start () {
		module = 1.0f;
		columnDiameter = 2.0f *module;
		columnHeight = 6.0f*columnDiameter;

		currentFluting = ColumnSixteen;

		numColumns = 4;
		columnDepth = 9;
		columnHeight = 6;
		columnSpacing = 2.25f;
		columnOrder = 0;
		columnFluting = 1;
		peristyle = 1;

		temple = new GameObject();
		temple_columns = new GameObject();
		temple_entablature = new GameObject();
		temple_roof = new GameObject();
		temple_walls = new GameObject();
		temple_base = new GameObject();

		temple_columns.transform.parent = temple.transform;
		temple_entablature.transform.parent = temple.transform;
		temple_roof.transform.parent = temple.transform;
		temple_walls.transform.parent = temple.transform;
		temple_base.transform.parent = temple.transform;

		ShowTemple();
		
	}
	
	// Update is called once per frame
	void Update () {
		//if stuff changed, update variables based on those changes.
	}

	public void ShowTemple(){

		//clear existing temple, if any;

		MakeBase(numColumns, columnDepth, columnSpacing, peristyle);
		MakeColumns(numColumns, columnDepth, columnHeight, columnSpacing, peristyle);
		MakeWalls(numColumns, columnDepth, columnHeight, columnSpacing, peristyle);
		MakeEntablature(numColumns, columnDepth, columnHeight, columnSpacing, peristyle);
		MakeRoof(numColumns, columnDepth, columnHeight, columnSpacing, peristyle);
	}

	public GameObject Make(GameObject go){
		GameObject make = (GameObject) Instantiate(go, new Vector3(0,0,0), go.transform.rotation);
		return make;
	}

	public void MakeBase(int numColumns, int columnDepth, float columnSpacing, int peristyle){

	}

	/* Create Columns */

	public void MakeColumns(int numColumns, int columnDepth, float height, float spacing, int peristyle){

		GameObject columnType = ColumnSixteen;
		GameObject columnCapital = CapitalIonic;
		//Above two lines of code are temporary!  Fix them later!  Implement dynamic stuff.

		for(int i = 0; i<numColumns; i++){
			GameObject column = Make(currentFluting);
			GameObject column2 = Make(currentFluting);

			sizeColumn(column);
			sizeColumn(column2);

			placeColumnFront(column, i);
			placeColumnBack(column2, i);

		}
	}

	public void sizeColumn(GameObject column){
		column.transform.localScale = new Vector3(columnDiameter, columnHeight, columnDiameter);
	}
	
	public void placeColumnFront(GameObject column, int num){
		
	}
	
	public void placeColumnBack(GameObject column, int num){
		
	}

	/* Create Walls */

	public void MakeWalls(int numColumns, int columnDepth, float columnHeight, float columnSpacing, int peristyle){

	}

	public void MakeEntablature(int numColumns, int columnDepth, float columnHeight, float columnSpacing, int peristyle){

	}

	public void MakeRoof(int numColumns, int columnDepth, float columnHeight, float columnSpacing, float peristyle){

	}
}
