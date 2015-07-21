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

	public int numColumns;
	public int columnDepth;
	public float columnHeight;
	public float columnSpacing;
	private GameObject currentCapital;
	private GameObject currentFluting;

	public float module;
	public float columnDiameter;
	public float intercolumnation;

	public bool antis;
	public bool amphi;
	public bool prostyle;
	public bool peristyle;
	public bool columnDepthIsAuto;

	private GameObject temple;
	private GameObject temple_columns;
	private GameObject temple_entablature;
	private GameObject temple_roof;
	private GameObject temple_walls;
	private GameObject temple_base;

	public float baseWidth;
	public float baseLength;
	public float columnWidth;//this will only be different if temple is in antis but not prostyle or peristyle
	public float columnLength;
	public float cellaWidth;
	public float cellaLength;
	public Vector3 cellaPosition;
	public Vector3 basePosition;


	
	// Use this for initialization
	void Start () {
		module = 1.0f;
		columnDiameter = module * 2.0f;
		columnHeight = columnDiameter * 3.0f;

		antis = true;
		amphi = false;
		prostyle = false;
		columnDepthIsAuto = true;

		currentFluting = ColumnSixteen;
		currentCapital = CapitalDoric;

		numColumns = 2;
		columnDepth = 5;
		//columnHeight = 6;
		columnSpacing = 2.25f * module;
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


	/* Functions that are hooked up to check boxes on UI. */
	public void setDistyle(bool t){numColumns = 2; ShowTemple();}
	public void setTetrastyle(bool t){numColumns = 4; ShowTemple();}
	public void setHexastyle(bool t){numColumns = 6; ShowTemple();}
	public void setOctastyle(bool t){numColumns = 8; ShowTemple();}

	public void setColumnDepthStandard(bool t){columnDepth = numColumns*2 + 1; columnDepthIsAuto = true; ShowTemple();}
	public void setColumnDepthCustom(bool t){columnDepthIsAuto = false; ShowTemple();}

	public void setDoric(bool t){currentCapital = CapitalDoric; ShowTemple();}
	public void setIonic(bool t){currentCapital = CapitalIonic; ShowTemple();}
	public void setCorinthian(bool t){currentCapital = CapitalCorinthian; ShowTemple();}

	public void setAntis(bool t){antis = t; ShowTemple();}

	public void setProstyle(bool t){
		//enable Amphiprostyle if Prostyle is checked
		prostyle = t; DivAmphiprostyle.interactable = t;
		if(!t){
			amphi = t;
			this.Frontal.SetAllTogglesOff();
		}
		ShowTemple();
	}

	public void setAmphi(bool t){amphi = t; ShowTemple();}

	public void setUnfluted(bool t){currentFluting = ColumnZero; ShowTemple();}
	public void setFlutedSixteen(bool t){currentFluting = ColumnSixteen; ShowTemple();}
	public void setFlutedTwenty(bool t){currentFluting = ColumnTwenty; ShowTemple();}
	public void setFlutedTwentyFour(bool t){currentFluting = ColumnTwentyFour; ShowTemple();}

	public void setPeristyleNone(bool t){peristyle = false; ShowTemple();}
	public void setPeristylePeripteral(bool t){peristyle = t; ShowTemple();}

	public void setPycnostyle(bool t){columnSpacing = 1.5f; ShowTemple();}
	public void setSystyle(bool t){columnSpacing = 2.0f; ShowTemple();}
	public void setEustyle(bool t){columnSpacing = 2.25f; ShowTemple();}
	public void setDiastyle(bool t){columnSpacing = 3.0f; ShowTemple();}
	public void setAraeostyle(bool t){columnSpacing = 3.5f; ShowTemple();}




	public void ShowTemple(){

		//clear existing temple, if any;
		destroyChildren(temple_columns);
		destroyChildren(temple_entablature);
		destroyChildren(temple_roof);
		destroyChildren(temple_walls);
		destroyChildren(temple_base);

		UpdateDepth();

		//process dimensions to be used in creating parts of temple
		PreprocessDimensions();

		MakeBase(numColumns, columnDepth, columnSpacing, peristyle);
		MakeColumns(numColumns, columnDepth, columnHeight, columnSpacing, peristyle);
		MakeWalls(numColumns, columnDepth, columnHeight, columnSpacing, peristyle);

		//temple_walls.transform.localScale = new Vector3(1,columnHeight, 1);
		//temple_walls.transform.position = new Vector3(0, columnHeight, 0);

		MakeEntablature(numColumns, columnDepth, columnHeight, columnSpacing, peristyle);
		MakeRoof(numColumns, columnDepth, columnHeight, columnSpacing, peristyle);
	}

	public void PreprocessDimensions(){


		baseWidth  = (this.columnDiameter*(float)this.numColumns + this.columnSpacing * ((float)this.numColumns - 1.0f));
		baseLength = (this.columnDiameter*(float)this.columnDepth + this.columnSpacing * ((float)this.columnDepth - 1.0f));
		columnWidth = (this.columnDiameter*(float)this.numColumns + this.columnSpacing * ((float)this.numColumns - 1.0f));
		columnLength = (this.columnDiameter*(float)this.columnDepth + this.columnSpacing * ((float)this.columnDepth - 1.0f));

		//cella-width is determined by 
	}

	public void UpdateDepth(){
		if (columnDepthIsAuto){
			columnDepth = numColumns * 2 + 1;
		}
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

		//float horizontal  = (this.columnDiameter*(float)this.numColumns + this.columnSpacing * ((float)this.numColumns - 1.0f));
		//float vertical = (this.columnDiameter*(float)this.columnDepth + this.columnSpacing * ((float)this.columnDepth - 1.0f));

		if(antis && !prostyle){
			baseWidth = (this.columnDiameter*(float)(this.numColumns+2.0f) + this.columnSpacing*((float)(this.numColumns+1.0f)));

		}


		// only need horizontal and vertical here //
		base1.transform.localScale = new Vector3(baseWidth, 1.0f, baseLength);
		base2.transform.localScale = new Vector3(baseWidth+2.0f, 1.0f, baseLength+2.0f);
		base3.transform.localScale = new Vector3(baseWidth+4.0f, 1.0f, baseLength+4.0f);

		base1.transform.position = new Vector3(0.0f, -0.5f, 0.0f);
		base2.transform.position = new Vector3(0.0f, -1.5f, 0.0f);
		base3.transform.position = new Vector3(0.0f, -2.5f, 0.0f);

	}

	/* Create Columns */

	public void MakeColumns(int numColumns, int columnDepth, float height, float spacing, bool peristyle){

		//GameObject columnType = ColumnSixteen;
		//GameObject columnCapital = CapitalIonic;
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
			if(numColumns ==2) return;
			for(int i=1; i<columnDepth-1; i++){
				GameObject column = MakeColumn(currentFluting, currentCapital, temple_columns);
				placeColumn(column, 0, i);

				GameObject column2 = MakeColumn(currentFluting, currentCapital, temple_columns);
				placeColumn(column2, numColumns - 1, i);
			}
		}

		if(antis){

			if(peristyle || prostyle){
				for (int i=1; i<numColumns-1; i++){
					GameObject column = MakeColumn(currentFluting, currentCapital, temple_columns);
					rotate90(column);
					placeColumn(column, i, 1);
				}
			}

			else{
				for (int i=0; i<numColumns; i++){
					GameObject column = MakeColumn(currentFluting, currentCapital, temple_columns);
					rotate90(column);
					placeColumn(column, i, 0);
				}
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

		if(antis && (!peristyle && !prostyle)){
			horizontal = (this.columnDiameter*(float)(this.numColumns+2) + this.columnSpacing * ((float)this.numColumns+1));
			vertical = (this.columnDiameter*(float)(this.columnDepth-1) + this.columnSpacing*((float)this.columnDepth-1));
			backWall.transform.localScale = new Vector3(horizontal - columnDiameter/2.0f, 1.0f, 1.0f);
			leftWall.transform.localScale = new Vector3(1.0f, 1.0f, vertical);
			rightWall.transform.localScale = new Vector3(1.0f, 1.0f, vertical);

			backWall.transform.position = new Vector3(0.0f, 0.0f, vertical/2 - 1.0f);
			leftWall.transform.position = new Vector3(-1*(horizontal/2.0f - columnDiameter/2.0f), 0.0f, -1.0f);
			rightWall.transform.position = new Vector3(horizontal/2.0f - columnDiameter/2.0f, 0.0f, -1.0f);
		}

		if(peristyle || amphi){

			vertical = (this.columnDiameter*(float)(this.columnDepth - 2) + this.columnSpacing * ((float)this.columnDepth - 3));
			halfVert = vertical/2.0f;

			leftWall.transform.localScale = new Vector3(1.0f, 1.0f, vertical);
			rightWall.transform.localScale = new Vector3(1.0f, 1.0f, vertical);

			if(peristyle){
				horizontal = (this.columnDiameter*(float)(numColumns - 2) + this.columnSpacing * ((float) this.numColumns - 3.0f));
				halfHoriz = horizontal/2.0f;
			}

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

		leftWall.transform.localScale = new Vector3(leftWall.transform.localScale.x, columnHeight + 2.0f, leftWall.transform.localScale.z);
		rightWall.transform.localScale = new Vector3(rightWall.transform.localScale.x, columnHeight + 2.0f, rightWall.transform.localScale.z);
		backWall.transform.localScale = new Vector3(backWall.transform.localScale.x, columnHeight + 2.0f, backWall.transform.localScale.z);

		leftWall.transform.position = new Vector3(leftWall.transform.position.x, columnHeight/2.0f + 1.0f, leftWall.transform.position.z);
		rightWall.transform.position = new Vector3(rightWall.transform.position.x, columnHeight/2.0f + 1.0f, rightWall.transform.position.z);
		backWall.transform.position = new Vector3(backWall.transform.position.x, columnHeight/2.0f + 1.0f, backWall.transform.position.z);
	}

	public void MakeEntablature(int numColumns, int columnDepth, float columnHeight, float columnSpacing, bool peristyle){

		//makeArchitraves//
		float horizontal = 1;
		float vertical = (columnDiameter * (float)columnDepth + columnSpacing*(float)(columnDepth - 1));

		if(!antis){
			horizontal = (columnDiameter*(float)numColumns + columnSpacing*(float)(numColumns-1));

			//make front architrave

			GameObject architraveFront = Make(CellaWalls, temple_entablature);
			GameObject architraveFrontLeft = Make(CellaWalls, temple_entablature);
			GameObject architraveFrontRight = Make(CellaWalls, temple_entablature);

			architraveFront.transform.localScale = new Vector3(horizontal - 1.0f, 2.0f, 1.0f);
			architraveFrontLeft.transform.localScale = new Vector3(1.0f, 2.0f, columnSpacing+ columnDiameter/2.0f);
			architraveFrontRight.transform.localScale = new Vector3(1.0f, 2.0f, columnSpacing + columnDiameter/2.0f);

			PlaceArchitraveFront(architraveFront, architraveFrontLeft, architraveFrontRight, 0);

			if(amphi || peristyle){
				//make rear architrave

				GameObject architraveBack = Make(CellaWalls, temple_entablature);
				GameObject architraveBackLeft = Make(CellaWalls, temple_entablature);
				GameObject architraveBackRight = Make(CellaWalls, temple_entablature);

				architraveBack.transform.localScale = new Vector3(horizontal-1.0f, 2.0f, 1.0f);
				architraveBackLeft.transform.localScale = new Vector3(1.0f, 2.0f, columnSpacing + columnDiameter/2.0f);
				architraveBackRight.transform.localScale = new Vector3(1.0f, 2.0f, columnSpacing + columnDiameter/2.0f);

				PlaceArchitraveBack(architraveBack, architraveBackLeft, architraveBackRight, columnDepth-1);

				if(peristyle){
					//make side architraves
					GameObject architraveLeft = Make(CellaWalls, temple_entablature);
					GameObject architraveRight = Make(CellaWalls, temple_entablature);

					architraveLeft.transform.localScale = new Vector3(1.0f, 2.0f, vertical - 1.0f);
					architraveRight.transform.localScale = new Vector3(1.0f, 2.0f, vertical - 1.0f);

					PlaceArchitravesSides(architraveLeft, architraveRight);

					//architraveLeft.transform.localScale = new Vector3(1.0f, 
					//////////////////////

				}
			}

		}
		else{
			//in antis

			if(prostyle || amphi){
				//make front and antis architraves

				if(amphi){
					//make rear architrave
				}
			}

			if(peristyle){
				//make front, antis, rear, and side architraves

			}
			if(!prostyle && (!amphi && ! peristyle)){

			}

		}

	}

	public void PlaceArchitrave(GameObject architrave, int row){

		//code adopted from PlaceColumn function
	
		
		float vertical = (this.columnDiameter*(float)this.columnDepth + this.columnSpacing * ((float)this.columnDepth - 1.0f));
		float halfVert = vertical / 2.0f;
		float moveVert = this.columnDiameter*(float)row + this.columnSpacing*(float)row;
		float z = moveVert - halfVert + (this.columnDiameter/2.0f);

	
		
		//column.transform.position = new Vector3(x, this.columnHeight/2, z);

		architrave.transform.localPosition = new Vector3(0.0f, columnHeight + 2.0f, z);
	}

	public void PlaceArchitraveFront(GameObject center, GameObject left, GameObject right, int row){
		PlaceArchitrave(center, row);

		float horizontal  = (this.columnDiameter*(float)this.numColumns + this.columnSpacing * ((float)this.numColumns - 1.0f));
		float halfHoriz = horizontal/2.0f;

		float moveHorizLeft = this.columnDiameter*(float)0 + this.columnSpacing*(float)0;

		float xLeft = moveHorizLeft - halfHoriz + (this.columnDiameter/2.0f);
		float xRight = -1 * xLeft;
		
		float vertical = (this.columnDiameter*(float)this.columnDepth + this.columnSpacing * ((float)this.columnDepth - 1.0f));
		float halfVert = vertical / 2.0f;
		float moveVert = this.columnDiameter*(float)(row + .5f) + this.columnSpacing*(float)(row+.5f);
		float z = moveVert - halfVert + (this.columnDiameter/2.0f);

		left.transform.localPosition = new Vector3(xLeft, columnHeight + 2.0f, z);
		right.transform.localPosition = new Vector3(xRight, columnHeight+ 2.0f, z);

	}

	public void PlaceArchitraveBack(GameObject center, GameObject left, GameObject right, int row){
		PlaceArchitrave(center, row);

		float horizontal  = (this.columnDiameter*(float)this.numColumns + this.columnSpacing * ((float)this.numColumns - 1.0f));
		float halfHoriz = horizontal/2.0f;
		
		float moveHorizLeft = this.columnDiameter*(float)0 + this.columnSpacing*(float)0;

		float xLeft = moveHorizLeft - halfHoriz + (this.columnDiameter/2.0f);
		float xRight = -1 * xLeft;
		
		float vertical = (this.columnDiameter*(float)this.columnDepth + this.columnSpacing * ((float)this.columnDepth - 1.0f));
		float halfVert = vertical / 2.0f;
		float moveVert = this.columnDiameter*(float)(row - .5f) + this.columnSpacing*(float)(row-  .5f);
		float z = moveVert - halfVert + (this.columnDiameter/2.0f);

		left.transform.localPosition = new Vector3(xLeft, columnHeight + 2.0f, z);
		right.transform.localPosition = new Vector3(xRight, columnHeight + 2.0f, z);
	}

	public void PlaceArchitravesSides(GameObject left, GameObject right){
		float horiz = (this.columnDiameter*(float)this.numColumns + this.columnSpacing * ((float)this.numColumns - 1.0f));
		float halfHoriz = horiz / 2.0f;
		float moveHorizLeft = this.columnDiameter*(float)0 + this.columnSpacing*(float)0;
		float x = moveHorizLeft - halfHoriz + (this.columnDiameter/2.0f);

		left.transform.localPosition = new Vector3(x, columnHeight + 2.0f, 0.0f);
		right.transform.localPosition = new Vector3(-x, columnHeight + 2.0f, 0.0f);
	}


	public void MakeRoof(int numColumns, int columnDepth, float columnHeight, float columnSpacing, bool peristyle){

	}

	public void destroyChildren(GameObject parent){
		/*var children = new List<GameObject>();
		foreach(Transform child in parent.transform){
			children.Add(child.gameObject);
		}
		children.ForEach(child => Destroy(child));*/
		int numChillins = parent.transform.childCount;
		for(int i=numChillins-1; i>-1; i--){
			GameObject.Destroy(parent.transform.GetChild(i).gameObject);
		}

	}
}
