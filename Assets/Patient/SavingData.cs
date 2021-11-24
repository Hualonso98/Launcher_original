using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SavingData
{
    public static bool leapHeadMode = true; //TRUE: Head Mounted; FALSE: Desktop

    public static bool modeGesture = true; //True musica se autocompleta, false musica de fondo
    public static bool modeTimeGesture = true; //True modo largo, false modo corto
    public static bool progressive = false;
    public static int numTheme = 0;
    public static float timeGeneration = 0.75f;
    public static float timeFalling = 0.75f;
    public static float linePosition = -150f; //Nueva posición por defecto, ajustada al 3d e hijo del Canvas

    public static int[] gest_Gestures = new int[5] { 0, 1, 2, 3, 4 }; //Gesto1, Gesto2, Gesto3, Gesto4, Gesto5


    public static bool length = true, arkMode = true, speedBall = true;
    public static float speedPongValue = 0.75f;

    public static int[] ark_Gestures = new int[2] { 0, 1 }; //Izquierda, Derecha
    

    public static int lives = 3;
    public static float timeAttacks = 0.75f;
    public static float speedShots = 0.75f;
    public static float speedCraft = 0.75f;
    public static bool heatTracking = false;

    public static int[] spa_Gestures = new int[3] { 0, 1, 2 }; //Izquierda, Derecha, Disparar

    public static List<int> cook_Gestures = new List<int>();
    public static List<int> cook_Gestures_no_repeated = new List<int>();


    public static int time_holding = 3;
    public static bool difficult_mode = false;
    public static int[] tres_Gestures = new int[5] { 0, 1, 2, 3, 4 }; //Izquierda, Derecha, Arriba, Abajo, Confirmar

    public static int ammo_selected = 13;
    public static int flota_holding = 3;   
    public static int[] flota_Gestures = new int[5] { 0, 1, 2, 3, 4 }; //Izquierda, Derecha, Arriba, Abajo, Confirmar
    /////////////////////////////////////////////////////////////
    public static float left_minIndexPinchDistance = 0.02f;
    public static float left_minMiddlePinchDistance = 0.02f;
    public static float left_minRingPinchDistance = 0.02f;
    public static float left_minPinkyPinchDistance = 0.02f;
                        
    public static float left_maxThumbExtension = 0.08f;
    public static float left_maxIndexExtension = 0.08f;
    public static float left_maxMiddleExtension = 0.08f;
    public static float left_maxRingExtension = 0.08f;
    public static float left_maxPinkyExtension = 0.08f;

    public static float left_maxVerticalAngleDown = 40f;
    public static float left_maxVerticalAngleUp = 40f;
    public static float left_maxHorizontalAngleLeft = 15f;
    public static float left_maxHorizontalAngleRight = 10f;

    public static float left_maxPron = 0f;
    public static float left_maxSup = 165f;
    /////////////////////////////////////////////////////////////    
    public static float right_minIndexPinchDistance = 0.02f;
    public static float right_minMiddlePinchDistance = 0.02f;
    public static float right_minRingPinchDistance = 0.02f;
    public static float right_minPinkyPinchDistance = 0.02f;
                        
    public static float right_maxThumbExtension = 0.08f;
    public static float right_maxIndexExtension = 0.08f;
    public static float right_maxMiddleExtension = 0.08f;
    public static float right_maxRingExtension = 0.08f;
    public static float right_maxPinkyExtension = 0.08f;
                        
    public static float right_maxVerticalAngleDown = 40f;
    public static float right_maxVerticalAngleUp = 40f;
    public static float right_maxHorizontalAngleLeft = 10f;
    public static float right_maxHorizontalAngleRight = 15f;

    public static float right_maxPron = 0f;
    public static float right_maxSup = -165f;
    /////////////////////////////////////////////////////////////

    public static float default_minIndexPinchDistance = 0.02f;
    public static float default_minMiddlePinchDistance = 0.02f;
    public static float default_minRingPinchDistance = 0.02f;
    public static float default_minPinkyPinchDistance = 0.02f;
                        
    public static float default_maxThumbExtension = 0.08f;
    public static float default_maxIndexExtension = 0.08f;
    public static float default_maxMiddleExtension = 0.08f;
    public static float default_maxRingExtension = 0.08f;
    public static float default_maxPinkyExtension = 0.08f;
                        
    public static float default_maxVerticalAngleDown = 40f;
    public static float default_maxVerticalAngleUp = 40f;
    public static float default_maxHorizontalAngleLeft = 15f;
    public static float default_maxHorizontalAngleRight = 10f;

    public static float default_pron = 0f;
    public static float default_left_sup = 165f;
    public static float default_right_sup = -165f;
    ////////////////////////////////////////////////////////////
    public static bool manoIzquierda = false;

    public static float workSpaceX_x = 0.20275f;
    public static float workSpaceX_y = 0.03039f; //El workSpace exterior tiene aquí 0.04039f
    public static float workSpaceY_x = 0.08999999f; //El workSpace exterior tiene aquí 0.09999f
    public static float workSpaceY_y = 0.29f;

    //OJO. ESTOS UMBRALES LOS PODRÉ AMPLIAR, DIFERENCIANDO PARA CADA MANO, CADA PINZA, CADA DEDO EXTENDIDO O CADA DESVIACIÓN

    public static float pinch_tolerance = 0.01f;
    public static float extension_tolerance = 0.005f;
    public static float desv_vert_tolerance = 5;
    public static float desv_hor_tolerance = 5;
    public static float pronosup_tolerance = 5;

    public static bool[] typeGesturesRequired = new bool[4] { false, false, false, false }; //Array para controlar qué tipo de getsos usará el juego, y en función de ello activaré o no sus scripts
                                                            //pinzas, extensiones, desviaciones, pronosupinaciones    
    /*//Futuros umbrales individuales
    public static float left_indexPinch_tolerance = 0.01f;
    public static float left_middlePinch_tolerance = 0.01f;
    public static float left_ringPinch_tolerance = 0.01f;
    public static float left_pinkyPinch_tolerance = 0.01f;

    public static float right_indexPinch_tolerance = 0.01f;
    public static float right_middlePinch_tolerance = 0.01f;
    public static float right_ringPinch_tolerance = 0.01f;
    public static float right_pinkyPinch_tolerance = 0.01f;

    public static float left_extension_tolerance = 0.005f;
    public static float right_extension_tolerance = 0.005f;

    public static float left_flex_tolerance = 5;
    public static float left_exten_tolerance = 5;
    public static float left_desvRad_tolerance = 5;
    public static float left_desvCubit_tolerance = 5;

    public static float right_flex_tolerance = 5;
    public static float right_exten_tolerance = 5;
    public static float right_desvRad_tolerance = 5;
    public static float right_desvCubit_tolerance = 5;
    */

    public static int finalCountFrames = 30;
    public static int indexMedianValue = 15;

    //Gestos en orden: Pinza índice, Pinza medio, Pinza anular, Pinza meñique, Puño cerrado, Mano extendida, Flexión, Extensión, Desviación radial, Desviación cubital
    //                      0             1            2             3             4             5              6         7              8                   9

    /*public static void ResetOptions()
    {
        length = true;
        arkMode = true;
        speedBall = true;
        speedPongValue = 0.75f; //Si es 1f, la velocidad de la raqueta es la máxima posible (más fácil)
        ark_Gestures = new int[2] { 3, 4 };

        lives = 3;
        timeAttacks = 0.75f; //Si es 1f, el tiempo entre los ataques es el máxima posible (más fácil)
        speedShots = 0.75f; //Si es 1f, la velocidad de los disparos es la máxima posible (más fácil)
        speedCraft = 0.75f; //Si es 1f, la velocidad de la nave es la máxima posible (más fácil)
        heatTracking = false;
        spa_Gestures = new int[3] { 3, 4, 1 };

        modeGesture = true; //True musica se autocompleta, false musica de fondo
        modeTimeGesture = true; //True modo largo, false modo corto
        progressive = false;
        numTheme = 0;
        timeGeneration = 0.75f; //Si es 1f, el tiempo entre generaciones es el máximo posible (más fácil)
        timeFalling = 0.75f; //Si es 1f, el tiempo de caída es el máximo posible (más fácil)
        linePosition = -80f;
        gest_Gestures = new int[5] { 0, 1, 2, 3, 4 };       
    }*/

    public static void ResetGestureOptions()
    {
        modeGesture = true; //True musica se autocompleta, false musica de fondo
        modeTimeGesture = true; //True modo largo, false modo corto
        progressive = false;
        numTheme = 0;
        timeGeneration = 0.75f;
        timeFalling = 0.75f;
        linePosition = -150f; //Nueva posición por defecto, ajustada al 3d e hijo del Canvas
    }
    public static void ResetArkanoidOptions()
    {
        length = true; arkMode = true; speedBall = true;
        speedPongValue = 0.75f;
    }
    public static void ResetSpaceOptions()
    {
        lives = 3;
        timeAttacks = 0.75f;
        speedShots = 0.75f;
        speedCraft = 0.75f;
        heatTracking = false;
    }
    public static void ResetTresOptions()
    {
        difficult_mode = false;
        time_holding = 3;
    }
    public static void ResetFlotaOptions()
    {
        ammo_selected = 13;
        flota_holding = 3;
    }

    public static void ResetGestureGestures()
    {
        gest_Gestures = new int[5] { 0, 1, 2, 3, 4 };
    }
    public static void ResetArkanoidGestures()
    {
        ark_Gestures = new int[2] { 0, 1 };
    }
    public static void ResetSpaceGestures()
    {
        spa_Gestures = new int[3] { 0, 1, 2 };
    }   
    public static void ResetTresGestures()
    {
        tres_Gestures = new int[5] { 0, 1, 2, 3, 4 };
    }
    public static void ResetFlotaGestures()
    {
        flota_Gestures = new int[5] { 0, 1, 2, 3, 4 };
    }
    public static void ResetRequiredGestures()
    {
        typeGesturesRequired[0] = false;
        typeGesturesRequired[1] = false;
        typeGesturesRequired[2] = false;
        typeGesturesRequired[3] = false;
    }
}