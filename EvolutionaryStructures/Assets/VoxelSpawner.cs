using System.Collections;
using System.Collections.Generic;
using SharpNeat.Phenomes;
using UnityEngine;


//https://medium.com/@HolographicInterfaces/learn-how-to-implement-neat-ai-in-unity-157168eeae7e
public class VoxelSpawner : UnitController {
    private IBlackBox box;

    // Start is called before the first frame update
    void Start() {
        ISignalArray inputArr = box.InputSignalArray;
        ISignalArray outputArr = box.OutputSignalArray;

        inputArr[0] = 3;
        Debug.Log(outputArr[0]);
    }

    public override void Activate(IBlackBox box) {
        this.box = box;
        Debug.Log("Activate called");
    }

    // Update is called once per frame
    void Update() {

    }

    public override float GetFitness() {
        return 0;
    }

    public override void Stop() {
        Debug.Log("Stopped but not really");
    }
}
