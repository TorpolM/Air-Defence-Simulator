using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADPScreen : MonoBehaviour
{
    public GameObject TCC;
    public GameObject symbol;
    public GameObject antenna;
    public float scale;


    TCC tcc;
    void Start()
    {
        tcc = TCC.GetComponent<TCC>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void newSymbol(GameObject truck){
        GameObject echo = Instantiate(symbol);
        echo.GetComponent<SymbolControl>().truck = truck;
        echo.GetComponent<SymbolControl>().scale = scale;
        echo.GetComponent<SymbolControl>().antennaPos = antenna.transform.position;
        echo.transform.parent = transform;
    }
}
