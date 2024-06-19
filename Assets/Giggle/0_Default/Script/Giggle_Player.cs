using UnityEngine;

//
using System;
using System.Collections.Generic;

public class Giggle_Player : IDisposable
{
    #region BASIC

    ////////// Getter & Setter          //////////

    ////////// Method                   //////////

    ////////// Constructor & Destroyer  //////////
    public Giggle_Player()
    {
        Power_Contructor();
    }

    public void Dispose()
    {

    }

    #endregion

    #region POWER
    [Header("POWER ==================================================")]
    [SerializeField] List<int> Power_values;

    ////////// Getter & Setter          //////////

    ////////// Method                   //////////

    public void Power_Add(int _pow)
    {
        Power_values[0] += _pow;

        for(int for0 = 0; for0 < Power_values.Count - 1; for0++)
        {
            if (Power_values[for0] > 1000)
            {
                Power_values[for0 + 1] += Power_values[for0] / 1000;
                Power_values[for0] %= 1000;
            }
            else
            {
                break;
            }
        }
    }

    ////////// Constructor & Destroyer  //////////
    void Power_Contructor()
    {
        if(Power_values == null)
        {
            Power_values = new List<int>();
        }

        while(Power_values.Count < 5)
        {
            Power_values.Add(0);
        }
    }

    #endregion
}
