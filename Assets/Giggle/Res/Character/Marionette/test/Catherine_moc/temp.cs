using Live2D.Cubism.Core;
using Live2D.Cubism.Framework;
using UnityEngine;

public class temp : MonoBehaviour
{
    [SerializeField] CubismModel aa;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int for0 = 0; for0 < aa.Parameters.Length; for0++)
        {
            if(aa.Parameters[for0].name.Equals("Param_weapon"))
            {
                aa.Parameters[for0].AddToValue(1.0f, 1.0f);

                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
