using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour {
    public int loopCount = 1000;
    System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();

    public int tableSize = 360;
    public const float PI2 = Mathf.PI * 2.0f;
    float[] cosTable;

	void Start () {
        cosTable = new float[ tableSize ];
        for( int i = 0; i < tableSize; ++i )
        {
            float theta = (float)i / tableSize * PI2;
            cosTable[i] = Mathf.Cos( theta );
        }

        Debug.Log( "getCosFromTable 30.0f : "  + getCosFromTable( 30.0f  * Mathf.Deg2Rad ) + " , " + Mathf.Cos( 30.0f  * Mathf.Deg2Rad ) );
        Debug.Log( "getCosFromTable 45.0f : "  + getCosFromTable( 45.0f  * Mathf.Deg2Rad ) + " , " + Mathf.Cos( 45.0f  * Mathf.Deg2Rad ) );
        Debug.Log( "getCosFromTable 90.0f : "  + getCosFromTable( 90.0f  * Mathf.Deg2Rad ) + " , " + Mathf.Cos( 90.0f  * Mathf.Deg2Rad ) );
        Debug.Log( "getCosFromTable 120.0f : " + getCosFromTable( 120.0f * Mathf.Deg2Rad ) + " , " + Mathf.Cos( 120.0f * Mathf.Deg2Rad ) );
        Debug.Log( "getCosFromTable 180.0f : " + getCosFromTable( 180.0f * Mathf.Deg2Rad ) + " , " + Mathf.Cos( 180.0f * Mathf.Deg2Rad ) );
        Debug.Log( "getCosFromTable 235.0f : " + getCosFromTable( 235.0f * Mathf.Deg2Rad ) + " , " + Mathf.Cos( 235.0f * Mathf.Deg2Rad ) );
        Debug.Log( "getCosFromTable 270.0f : " + getCosFromTable( 270.0f * Mathf.Deg2Rad ) + " , " + Mathf.Cos( 270.0f * Mathf.Deg2Rad ) );
        Debug.Log( "getCosFromTable 315.0f : " + getCosFromTable( 315.0f * Mathf.Deg2Rad ) + " , " + Mathf.Cos( 315.0f * Mathf.Deg2Rad ) );
        Debug.Log( "getCosFromTable 360.0f : " + getCosFromTable( 360.0f * Mathf.Deg2Rad ) + " , " + Mathf.Cos( 360.0f * Mathf.Deg2Rad ) );
        Debug.Log( "getCosFromTable 390.0f : " + getCosFromTable( 390.0f * Mathf.Deg2Rad ) + " , " + Mathf.Cos( 390.0f * Mathf.Deg2Rad ) );

        /* float tt = 270.0f * Mathf.Deg2Rad; */
        /* int index = getCosTableIndex( 270.0f * Mathf.Deg2Rad ); */ 
        /* float t = (float)index / tableSize * PI2; */
        /* Debug.Log( " index 270 : " + index + "," + t/Mathf.PI + " , " + (int)( tt / PI2 * tableSize ) % tableSize ); */
        /* index = getCosTableIndex( 90.0f * Mathf.Deg2Rad ); */ 
        /* t = (float)index / tableSize * PI2; */
        /* Debug.Log( " index 90 : " + index + "," + t/Mathf.PI ); */
    }

    [ContextMenu("Test Mathf")]
	void TestMathf () {
        stopWatch.Reset();
        stopWatch.Start();
        float sum = 0.0f;
        for( int i = 0; i < loopCount; ++i )
        {
            sum += Mathf.Cos( (float)i );
        }
		
        stopWatch.Stop();
        Debug.Log( "time : " + stopWatch.Elapsed + " sum, " + sum );
	}

    [ContextMenu("Test Cos Table")]
	void TestCosTable () {
        stopWatch.Reset();
        stopWatch.Start();
        float sum = 0.0f;
        for( int i = 0; i < loopCount; ++i )
        {
            sum += getCosFromTable( (float)i );
        }
        stopWatch.Stop();
        Debug.Log( "time : " + stopWatch.Elapsed + " sum, " + sum );
	}

	int getCosTableIndex ( float theta ) {
        return (int)( theta / PI2 * tableSize ) % tableSize;
    }

	float getCosFromTable ( float theta ) {
        int index = getCosTableIndex( theta );
        return cosTable[ index ];
    }
}
