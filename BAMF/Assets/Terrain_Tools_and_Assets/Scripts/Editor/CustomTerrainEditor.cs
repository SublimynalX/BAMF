using UnityEditor;
using UnityEngine;
using EditorGUITable;

[CustomEditor(typeof(CustomTerrain))]
[CanEditMultipleObjects]

public class CustomTerrainEditor : Editor
{
    Texture2D hmTexture;
    //properties------------
    SerializedProperty randomHeightRange;
    SerializedProperty heightMapScale;
    SerializedProperty heightMapImage;
    SerializedProperty perlinXScale;
    SerializedProperty perlinYScale;
    SerializedProperty perlinOffsetX;
    SerializedProperty perlinOffsetY;
    SerializedProperty perlinOctaves;
    SerializedProperty perlinPersistance;
    SerializedProperty perlinHeightScale;
    SerializedProperty resetTerrain;
    SerializedProperty MPDheightMin;
    SerializedProperty MPDheightMax;
    SerializedProperty MPDheightDampenerPower;
    SerializedProperty MPDroughness;
    SerializedProperty smoothAmount;
    SerializedProperty maxTrees;
    SerializedProperty treeSpacing;
    SerializedProperty vegetation;

    //fold out-------------
    bool showRandom = false;
    bool showLoadHeights = false;
    bool showPerlinNoise = false;
    bool showMultiplePerlin = false;
    bool showVoronoi = false;
    bool showMidPointDisplacement = false;
    bool showSmooth = false;
    bool showSplatMaps = false;
    bool showHeights = false;
    bool showVeg = false;
    bool showDetails = false;
    bool showWater = false;
    bool showErosion = false;
    bool showClouds = false;

    //Table Things-------------------
    GUITableState perlinParameterTable;
    SerializedProperty perlinParameters;


    //Voronoi stuff--------------------------
    SerializedProperty voronoiFallOff;
    SerializedProperty voronoiDropOff;
    SerializedProperty voronoiMinHeight;
    SerializedProperty voronoiMaxHeight;
    SerializedProperty voronoiPeaks;
    SerializedProperty voronoiType;


    //SplatMap-------------------------------
    GUITableState splatMapTable;
    SerializedProperty splatHeights;
    /*SerializedProperty splatOffset;
     SerializedProperty splatNoiseXScale;
     SerializedProperty splatNoiseYScale;
     SerializedProperty splatNoiseScaler;*/

    //Vegetation
    GUITableState vegMapTable;

    //Details 
    GUITableState detailMapTable;
    SerializedProperty details;
    SerializedProperty maxDetails;
    SerializedProperty detailSpacing;


    //Water---------------------------------
    SerializedProperty waterHeight;
    SerializedProperty waterGO;
    SerializedProperty shoreLineMaterial;

    //Erosion
    SerializedProperty erosionType;
    SerializedProperty erosionStrength;
    SerializedProperty springsPerRiver;
    SerializedProperty solubility;
    SerializedProperty droplets;
    SerializedProperty erosionSmoothAmount;
    SerializedProperty erosionAmount;

    //Clouds--------------------------------
    SerializedProperty numClouds;
    SerializedProperty particlesPerCloud;
    SerializedProperty cloudScaleMin;
    SerializedProperty cloudScaleMax;
    SerializedProperty cloudMaterial;
    SerializedProperty cloudShadowMaterial;
    SerializedProperty cloudStartSize;
    SerializedProperty cloudColor;
    SerializedProperty cloudLining;
    SerializedProperty cloudMinSpeed;
    SerializedProperty cloudMaxSpeed;
    SerializedProperty cloudRange;


    //allows to setup anything and anything new will be re-ran
    private void OnEnable()
    {
        //link and serialize the vector 2 variable from the other script to keep all serialized
        randomHeightRange = serializedObject.FindProperty("randomHeightRange");
        heightMapScale = serializedObject.FindProperty("heightMapScale");
        heightMapImage = serializedObject.FindProperty("heightMapImage");
        perlinXScale = serializedObject.FindProperty("perlinXScale");
        perlinYScale = serializedObject.FindProperty("perlinYScale");
        perlinOffsetX = serializedObject.FindProperty("perlinOffsetX");
        perlinOffsetY = serializedObject.FindProperty("perlinOffsetY");
        perlinOctaves = serializedObject.FindProperty("perlinOctaves");
        perlinPersistance = serializedObject.FindProperty("perlinPersistance");
        perlinHeightScale = serializedObject.FindProperty("perlinHeightScale");
        resetTerrain = serializedObject.FindProperty("resetTerrain");
        perlinParameterTable = new GUITableState("perlinParameterTable");
        perlinParameters = serializedObject.FindProperty("perlinParameters");
        voronoiFallOff = serializedObject.FindProperty("voronoiFallOff");
        voronoiDropOff = serializedObject.FindProperty("voronoiDropOff");
        voronoiMinHeight = serializedObject.FindProperty("voronoiMinHeight");
        voronoiMaxHeight = serializedObject.FindProperty("voronoiMaxHeight");
        voronoiPeaks = serializedObject.FindProperty("voronoiPeaks");
        voronoiType = serializedObject.FindProperty("voronoiType");
        MPDheightMin = serializedObject.FindProperty("MPDheightMin");
        MPDheightMax = serializedObject.FindProperty("MPDheightMax");
        MPDroughness = serializedObject.FindProperty("MPDroughness");
        MPDheightDampenerPower = serializedObject.FindProperty("MPDheightDampenerPower");
        smoothAmount = serializedObject.FindProperty("smoothAmount");
        splatHeights = serializedObject.FindProperty("splatHeights");
        splatMapTable = new GUITableState("splatMapTable");
        /* splatOffset = serializedObject.FindProperty("splatOffset");
         splatNoiseXScale = serializedObject.FindProperty("splatNoiseXScale");
         splatNoiseYScale = serializedObject.FindProperty("splatNoiseYScale");
         splatNoiseScaler = serializedObject.FindProperty("splatNoiseScaler");*/

        maxTrees = serializedObject.FindProperty("maxTrees");
        treeSpacing = serializedObject.FindProperty("treeSpacing");
        vegMapTable = new GUITableState("vegMapTable");
        vegetation = serializedObject.FindProperty("vegetation");
        maxDetails = serializedObject.FindProperty("maxDetails");
        detailSpacing = serializedObject.FindProperty("detailSpacing");
        detailMapTable = new GUITableState("detailMapTable");
        details = serializedObject.FindProperty("details");
        waterHeight = serializedObject.FindProperty("waterHeight");
        waterGO = serializedObject.FindProperty("waterGO");
        shoreLineMaterial = serializedObject.FindProperty("shoreLineMaterial");

        erosionType = serializedObject.FindProperty("erosionType");
        erosionStrength = serializedObject.FindProperty("erosionStrength");
        erosionSmoothAmount = serializedObject.FindProperty("erosionSmoothAmount");
        springsPerRiver = serializedObject.FindProperty("springsPerRiver");
        solubility = serializedObject.FindProperty("solubility");
        droplets = serializedObject.FindProperty("droplets");
        erosionAmount = serializedObject.FindProperty("erosionAmount");
        hmTexture = new Texture2D(513, 513, TextureFormat.ARGB32, false);

        numClouds = serializedObject.FindProperty("numClouds");
        particlesPerCloud = serializedObject.FindProperty("particlesPerCloud");
        cloudStartSize = serializedObject.FindProperty("cloudStartSize");
        cloudScaleMin = serializedObject.FindProperty("cloudScaleMin");
        cloudScaleMax = serializedObject.FindProperty("cloudScaleMax");
        cloudMaterial = serializedObject.FindProperty("cloudMaterial");
        cloudShadowMaterial = serializedObject.FindProperty("cloudShadowMaterial");
        cloudColor = serializedObject.FindProperty("cloudColor");
        cloudLining = serializedObject.FindProperty("cloudLining");
        cloudMinSpeed = serializedObject.FindProperty("cloudMinSpeed");
        cloudMaxSpeed = serializedObject.FindProperty("cloudMaxSpeed");
        cloudRange = serializedObject.FindProperty("cloudRange");
    }
    Vector2 scrollPos;
    //Does all the magic
    public override void OnInspectorGUI()
    {
        //Always use at beginning, updates all serilaized values between the two inheriting scripts
        serializedObject.Update();

        //link to script on the terrain
        CustomTerrain terrain = (CustomTerrain)target;
        //ScrollBar Start
        Rect r = EditorGUILayout.BeginVertical();
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(r.width), GUILayout.Height(r.height));
        EditorGUI.indentLevel++;
        //for reset terrain to 0
        EditorGUILayout.PropertyField(resetTerrain);
        //drives fold out display in Inspector
        showRandom = EditorGUILayout.Foldout(showRandom, "Random");

        if (showRandom)
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            GUILayout.Label("Set Heights Between Random Values", EditorStyles.boldLabel);
            //Used to display values
            EditorGUILayout.PropertyField(randomHeightRange);
            if(GUILayout.Button("Random Heights"))
            {
                terrain.RandomTerrain();
            }
        }

        showLoadHeights = EditorGUILayout.Foldout(showLoadHeights, "Load Heights");
        if (showLoadHeights)
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            GUILayout.Label("Load Heights From Texture", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(heightMapImage);
            EditorGUILayout.PropertyField(heightMapScale);
            if(GUILayout.Button("Load Texture"))
            {
                terrain.LoadTexture();
            }
        }
        showPerlinNoise = EditorGUILayout.Foldout(showPerlinNoise, "Single Perlin Noise");
        if (showPerlinNoise)
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            GUILayout.Label("Perlin Noise", EditorStyles.boldLabel);
            //Set Slider valuse for both x and y scale
            EditorGUILayout.Slider(perlinXScale, 0, 1, new GUIContent("X Scale"));
            EditorGUILayout.Slider(perlinYScale, 0, 1, new GUIContent("Y Scale"));
            EditorGUILayout.IntSlider(perlinOffsetX, 0, 10000, new GUIContent("Offset X"));
            EditorGUILayout.IntSlider(perlinOffsetY, 0, 10000, new GUIContent("Offset Y"));
            EditorGUILayout.IntSlider(perlinOctaves, 1, 10, new GUIContent("Octaves"));
            EditorGUILayout.Slider(perlinPersistance, 1, 10, new GUIContent("Persistance"));
            EditorGUILayout.Slider(perlinHeightScale, 0.1f, 1, new GUIContent("Height Scale"));
            if (GUILayout.Button("Perlin"))
            {
                terrain.Perlin();
            }

        }

        showMultiplePerlin = EditorGUILayout.Foldout(showMultiplePerlin, "Multiple Perlin Noise");
        if (showMultiplePerlin)
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            GUILayout.Label("Multiple Perlin Noise", EditorStyles.boldLabel);
            perlinParameterTable = GUITableLayout.DrawTable(perlinParameterTable, serializedObject.FindProperty("perlinParameters"));

            GUILayout.Space(20);
            EditorGUILayout.BeginHorizontal();
            if(GUILayout.Button("+"))
            {
                terrain.AddNewPerlin();
            }
            if (GUILayout.Button("-"))
            {
                terrain.RemovePerlin();
            }
            EditorGUILayout.EndHorizontal();
            if(GUILayout.Button("Apply Multple Perlin"))
            {
                terrain.MultiplePerlinTerrain();
            }
        }

        showVoronoi = EditorGUILayout.Foldout(showVoronoi, "Voronoi");
        if (showVoronoi)
        {
            EditorGUILayout.IntSlider(voronoiPeaks, 1, 10, new GUIContent("Peak Count"));
            EditorGUILayout.Slider(voronoiFallOff, 0, 10, new GUIContent("Falloff"));
            EditorGUILayout.Slider(voronoiDropOff, 0, 10, new GUIContent("Dropoff"));
            EditorGUILayout.Slider(voronoiMinHeight,0, 1, new GUIContent("Min Height"));
            EditorGUILayout.Slider(voronoiMaxHeight, 0, 1, new GUIContent("Max Height"));
            EditorGUILayout.PropertyField(voronoiType);
            if (GUILayout.Button("Voronoi"))
            {
                terrain.Voronoi();
            }
        }
        showMidPointDisplacement = EditorGUILayout.Foldout(showMidPointDisplacement, "MidPointDisplacement");
        if (showMidPointDisplacement)
        {
            EditorGUILayout.PropertyField(MPDheightMin);
            EditorGUILayout.PropertyField(MPDheightMax);
            EditorGUILayout.PropertyField(MPDroughness);
            EditorGUILayout.PropertyField(MPDheightDampenerPower);
            if (GUILayout.Button("MPD"))
            {
                terrain.MidPointDisplacement();
            }
        }
        showErosion = EditorGUILayout.Foldout(showErosion, "Erosion");
        if (showErosion)
        {
           
                EditorGUILayout.PropertyField(erosionType);
                EditorGUILayout.Slider(erosionStrength, 0, 1, new GUIContent("Erosion Strength"));
                EditorGUILayout.Slider(erosionAmount, 0, 1, new GUIContent("Erosion Amount"));
                EditorGUILayout.IntSlider(droplets,0,500,new GUIContent("Droplets"));
                EditorGUILayout.Slider(solubility, 0.001f, 1, new GUIContent("Solubility"));
                EditorGUILayout.IntSlider(springsPerRiver, 0, 20, new GUIContent("Springs Per River"));
                EditorGUILayout.IntSlider(erosionSmoothAmount, 0, 10, new GUIContent("Smooth Amount"));
            if (GUILayout.Button("Erode"))
            {
                terrain.Errode();
            }

        }
        showClouds = EditorGUILayout.Foldout(showClouds, "Clouds");
        if (showClouds)
        {
            EditorGUILayout.PropertyField(numClouds, new GUIContent("Number of Clouds"));
            EditorGUILayout.PropertyField(particlesPerCloud, new GUIContent("Particles Per Clouds"));
            EditorGUILayout.PropertyField(cloudStartSize, new GUIContent("Cloud Particle Size"));
            EditorGUILayout.PropertyField(cloudScaleMin, new GUIContent("Min Size"));
            EditorGUILayout.PropertyField(cloudScaleMax, new GUIContent("Max Size"));
            EditorGUILayout.PropertyField(cloudMaterial, true);
            EditorGUILayout.PropertyField(cloudShadowMaterial, true);
            EditorGUILayout.PropertyField(cloudColor, new GUIContent("Color"));
            EditorGUILayout.PropertyField(cloudLining, new GUIContent("Lining"));
            EditorGUILayout.PropertyField(cloudMinSpeed, new GUIContent("Min Speed"));
            EditorGUILayout.PropertyField(cloudMaxSpeed, new GUIContent("Max Speed"));
            EditorGUILayout.PropertyField(cloudRange, new GUIContent("Distance Travelled"));
            if(GUILayout.Button("Generate Clouds"))
            {
                terrain.GenerateClouds();
            }


        }
        showSmooth = EditorGUILayout.Foldout(showSmooth, "Smooth");
        if (showSmooth)
        {
            EditorGUILayout.IntSlider(smoothAmount, 0, 10, new GUIContent("Smooth Amount"));
            if (GUILayout.Button("Smooth"))
            {
                terrain.Smooth();
            }
        }
        showSplatMaps = EditorGUILayout.Foldout(showSplatMaps, "Splat Maps");
        if (showSplatMaps)
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            GUILayout.Label("Splat Maps", EditorStyles.boldLabel);
           /*EditorGUILayout.Slider(splatOffset, 0, 0.1f, new GUIContent("Offset"));
            EditorGUILayout.Slider(splatNoiseXScale, 0.001f, 1, new GUIContent("Noise X Scale"));
            EditorGUILayout.Slider(splatNoiseYScale, 0.001f, 1, new GUIContent("Noise Y Scale"));
            EditorGUILayout.Slider(splatNoiseScaler, 0, 1, new GUIContent("Noise Scaler"));*/
            splatMapTable = GUITableLayout.DrawTable(splatMapTable, serializedObject.FindProperty("splatHeights"));
            GUILayout.Space(20);
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("+"))
            {
                terrain.AddNewSplatHeight();
            }
            if (GUILayout.Button("-"))
            {
                terrain.RemoveSplatHeights();
            }
            EditorGUILayout.EndHorizontal();
            if(GUILayout.Button("Apply SplatMaps"))
            {
                terrain.SplatMaps();
            }

        }
        showVeg = EditorGUILayout.Foldout(showVeg, "Vegetation");
        if (showVeg)
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            GUILayout.Label("Vegetation", EditorStyles.boldLabel);
            EditorGUILayout.IntSlider(maxTrees, 0, 10000, new GUIContent("Maximum Trees"));
            EditorGUILayout.IntSlider(treeSpacing, 2, 20, new GUIContent("Tree Spacing"));
            vegMapTable = GUITableLayout.DrawTable(vegMapTable, serializedObject.FindProperty("vegetation"));

            GUILayout.Space(20);
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("+"))
            {
                terrain.AddNewVegetation();
            }
            if (GUILayout.Button("-"))
            {
                terrain.RemoveVegetation();
            }
            EditorGUILayout.EndHorizontal();
            if(GUILayout.Button("Apply Vegetation"))
            {
                terrain.PlantVegetation();
            }
        }
        showDetails = EditorGUILayout.Foldout(showDetails, "Details");
        if (showDetails)
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            GUILayout.Label("Details", EditorStyles.boldLabel);
            EditorGUILayout.IntSlider(maxDetails, 0, 10000, new GUIContent("Maximum Details"));
            EditorGUILayout.IntSlider(detailSpacing, 1, 20, new GUIContent("Detail Spacing"));
            detailMapTable = GUITableLayout.DrawTable(detailMapTable, serializedObject.FindProperty("details"));


            terrain.GetComponent<Terrain>().detailObjectDistance = maxDetails.intValue;
            GUILayout.Space(20);
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("+"))
            {
                terrain.AddNewDetails();
            }
            if (GUILayout.Button("-"))
            {
                terrain.RemoveDetails();
            }
            EditorGUILayout.EndHorizontal();
            if(GUILayout.Button("Apply Details"))
            {
                terrain.AddDetails();
            }
        }
        showWater = EditorGUILayout.Foldout(showWater, "Water");
        if (showWater)
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            GUILayout.Label("Water", EditorStyles.boldLabel);
            EditorGUILayout.Slider(waterHeight, 0, 1, new GUIContent("Water Height"));
            EditorGUILayout.PropertyField(waterGO);

            if(GUILayout.Button("Add Water"))
            {
                terrain.AddWater();
            }
        }
        EditorGUILayout.PropertyField(shoreLineMaterial);
        if(GUILayout.Button("Add Shoreline"))
        {
            terrain.DrawShoreline();
        }
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        if(GUILayout.Button("Reset Terrain"))
        {
            terrain.ResetTerrain();
        }
        showHeights = EditorGUILayout.Foldout(showHeights, "Height Map");
        if (showHeights)
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            int hmtSize = (int)(EditorGUIUtility.currentViewWidth - 100);
            GUILayout.Label(hmTexture, GUILayout.Width(hmtSize), GUILayout.Height(hmtSize));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Refresh", GUILayout.Width(hmtSize)))
            {
                float[,] heightMap = terrain.terrainData.GetHeights(0, 0, terrain.terrainData.heightmapWidth, terrain.terrainData.heightmapHeight);

                for(int y = 0; y < terrain.terrainData.heightmapHeight; y++)
                {
                    for(int x = 0; x < terrain.terrainData.heightmapWidth; x++)
                    {
                        hmTexture.SetPixel(x, y, new Color(heightMap[x, y], heightMap[x, y], heightMap[x, y], 1));
                    }
                }
                hmTexture.Apply();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }


        //End ScrollBar
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
        //Always use at end and applies the properties
        serializedObject.ApplyModifiedProperties();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
