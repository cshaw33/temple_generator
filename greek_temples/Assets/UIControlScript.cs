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
	private int columnFluting;
	private GameObject currentFluting;

	private float module;
	private float columnDiameter;
	private float intercolumnation;

	private bool amphi;
	private bool prostyle;
	private bool peristyle;

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
		amphi = false;
		prostyle = true;

		currentFluting = ColumnTwentyFour;
		currentCapital = CapitalCorinthian;

		numColumns = 6;
		columnDepth = 13;
		columnHeight = 6;
		columnSpacing = 2.25f;
		columnOrder = 0;
		columnFluting = 1;
		peristyle = false;

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
		destroyChildren(temple_columns);
		destroyChildren(temple_entablature);
		destroyChildren(temple_roof);
		destroyChildren(temple_walls);
		destroyChildren(temple_base);

		MakeBase(numColumns, columnDepth, columnSpacing, peristyle);
		MakeColumns(numColumns, columnDepth, columnHeight, columnSpacing, peristyle);
		MakeWalls(numColumns, columnDepth, columnHeight, columnSpacing, peristyle);

		temple_walls.transform.localScale = new Vector3(1,this.columnHeight, 1);
		temple_walls.transform.position = new Vector3(0, this.columnHeight/2, 0);

		MakeEntablature(numColumns, columnDepth, columnHeight, columnSpacing, peristyle);
		MakeRoof(numColumns, columnDepth, columnHeight, columnSpacing, peristyle);
	}

	public GameObject Make(GameObject go){
		GameObject make = (GameObject) Instantiate(go, new Vector3(0,0,0), go.transform.rotation);
		return make;
	}

	public GameObject Make(GameObject go, GameObject parent){
		GameObject make = Make(go);
		make.transform.parent = parent.transform;
		return make;
	}

	public void MakeBase(int numColumns, int columnDepth, float columnSpacing, bool peristyle){
		GameObject base1 = Make(this.TempleBase, this.temple_base);
		GameObject base2 = Make(this.TempleBase, this.temple_base);
		GameObject base3 = Make(this.TempleBase, this.temple_base);

		float horizontal  = (this.columnDiameter*(float)this.numColumns + this.columnSpacing * ((float)this.numColumns - 1.0f));
		float halfHoriz = horizontal/2.0f;

		
		float vertical = (this.columnDiameter*(float)this.columnDepth + this.columnSpacing * ((float)this.columnDepth - 1.0f));
		float halfVert = vertical / 2.0f;


		// only need horizontal and vertical here //
		base1.transform.localScale = new Vector3(horizontal, 1.0f, vertical);
		base2.transform.localScale = new Vector3(horizontal+2.0f, 1.0f, vertical+2.0f);
		base3.transform.localScale = new Vector3(horizontal+4.0f, 1.0f, vertical+4.0f);

		base1.transform.position = new Vector3(0.0f, -0.5f, 0.0f);
		base2.transform.position = new Vector3(0.0f, -1.5f, 0.0f);
		base3.transform.position = new Vector3(0.0f, -2.5f, 0.0f);

	}

	/* Create Columns */

	public void MakeColumns(int numColumns, int columnDepth, float height, float spacing, bool peristyle){

		GameObject columnType = ColumnSixteen;
		GameObject columnCapital = CapitalIonic;
		//Above two lines of code are temporary!  Fix them later!  Implement dynamic stuff.



		for(int i = 0; i<numColumns; i++){
			GameObject column = MakeColumn(currentFluting, currentCapital, temple_columns);
			rotate90(column);
			placeColumn(column, i, 0);

			if(amphi || this.peristyle){
				GameObject column2 = MakeColumn(currentFluting, currentCapital, temple_columns);
				rotate90(column2);
				placeColumn(column2, i, columnDepth - 1);
			}
		}

		if(this.peristyle){
			for(int i=1; i<columnDepth-1; i++){
				GameObject column = MakeColumn(currentFluting, currentCapital, temple_columns);
				placeColumn(column, 0, i);

				GameObject column2 = MakeColumn(currentFluting, currentCapital, temple_columns);
				placeColumn(column2, numColumns - 1, i);
			}
		}

		//add code to create columns in antis inside peristyle
	}

	public GameObject MakeColumn(GameObject shaft, GameObject capital, GameObject parent){

		GameObject column = new GameObject(); column.transform.parent = parent.transform;

		GameObject column_shaft = Make(shaft, column); sizeColumn(column_shaft);

		GameObject cap = Make(capital, column); 
		cap.transform.position = new Vector3(0, this.columnHeight/2.0f, 0); 

		//cap.transform.parent = column.transform;

		return column;
	}

	public void sizeColumn(GameObject column){
		column.transform.localScale = new Vector3(columnDiameter/2.0f, columnHeight, columnDiameter/2.0f);
	}
	
	public void placeColumn(GameObject column, int num, int depth){
		float horizontal  = (this.columnDiameter*(float)this.numColumns + this.columnSpacing * ((float)this.numColumns - 1.0f));
		float halfHoriz = horizontal/2.0f;
		float moveHoriz = this.columnDiameter*(float)num + this.columnSpacing*(float)num;
		float x = moveHoriz - halfHoriz + (this.columnDiameter/2.0f);

		float vertical = (this.columnDiameter*(float)this.columnDepth + this.columnSpacing * ((float)this.columnDepth - 1.0f));
		float halfVert = vertical / 2.0f;
		float moveVert = this.columnDiameter*(float)depth + this.columnSpacing*(float)depth;
		float z = moveVert - halfVert + (this.columnDiameter/2.0f);

		column.transform.position = new Vector3(x, this.columnHeight/2, z);
	}

	public void rotate90(GameObject obj){
		obj.transform.eulerAngles = new Vector3(transform.eulerAngles.x, 90.0f, transform.eulerAngles.z);
	}


	/* Create Walls */

	public void MakeWalls(int numColumns, int columnDepth, float columnHeight, float columnSpacing, bool peristyle){
		GameObject leftWall = Make(CellaWalls, temple_walls);
		GameObject rightWall = Make(CellaWalls, temple_walls); 
		GameObject backWall = Make(CellaWalls, temple_walls);

		float horizontal  = (this.columnDiameter*(float)this.numColumns + this.columnSpacing * ((float)this.numColumns - 1.0f));
		float halfHoriz = horizontal/2.0f;

		float vertical = (this.columnDiameter*(float)this.columnDepth + this.columnSpacing * ((float)this.columnDepth - 1.0f));
		float halfVert = vertical / 2.0f;

		float unit = this.columnDiameter+this.intercolumnation;

		if(peristyle || amphi){

			vertical = (this.columnDiameter*(float)(this.columnDepth - 2) + this.columnSpacing * ((float)this.columnDepth - 3));
			halfVert = vertical/2.0f;

			leftWall.transform.localScale = new Vector3(1.0f, 1.0f, vertical);
			rightWall.transform.localScale = new Vector3(1.0f, 1.0f, vertical);

			horizontal = (this.columnDiameter*(float)(numColumns - 2) + this.columnSpacing * ((float) this.numColumns - 3.0f));
			halfHoriz = horizontal/2.0f;

			backWall.transform.localScale = new Vector3(horizontal-3.0f, 1.0f, 1.0f);

			float move = halfHoriz - (columnDiameter/2.0f);

			leftWall.transform.position = new Vector3(-1*move, 0.0f, 0.0f);
			rightWall.transform.position = new Vector3(move, 0.0f, 0.0f);

			backWall.transform.position = new Vector3(0.0f, 0.0f, halfVert-0.5f);
		}

		else if(!amphi && prostyle){
			vertical = (this.columnDiameter * (float)(this.columnDepth -1) + this.columnSpacing *((float)this.columnDepth - 2));
			halfVert = vertical / 2.0f;

			leftWall.transform.localScale = new Vector3(1.0f, 1.0f, vertical - unit);
			rightWall.transform.localScale = new Vector3(1.0f, 1.0f, vertical - unit);
			leftWall.transform.position = new Vector3(-1*(halfHoriz - columnDiameter/2.0f), 0.0f, unit);
			rightWall.transform.position = new Vector3(halfHoriz - columnDiameter/2.0f, 0.0f, unit);

			backWall.transform.localScale = new Vector3(horizontal - 3.0f, 1.0f, 1.0f);
			backWall.transform.position = new Vector3(0.0f, 0.0f, halfVert + 0.5f);
		}
	}

	public void MakeEntablature(int numColumns, int columnDepth, float columnHeight, float columnSpacing, bool peristyle){

	}

	public void MakeRoof(int numColumns, int columnDepth, float columnHeight, float columnSpacing, bool peristyle){

	}

	public void destroyChildren(GameObject parent){
		/*var children = new List<GameObject>();
		foreach(Transform child in parent.transform){
			children.Add(child.gameObject);
		}
		children.ForEach(child => Destroy(child));*/

	}
}
