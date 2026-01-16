using UnityEngine;
using System.IO;

[System.Serializable]
public class BrainData
{
    public double[] weights;
    public double bias;
}

public class Perceptron : MonoBehaviour
{
    public double[] weights = { 0, 0 };
    public double bias = 0;
    double learningRate = 0.1;
    private string savePath; 

    void Awake()
    {
        if (Application.isEditor)
             savePath = Path.Combine(Directory.GetParent(Application.dataPath).ToString(), "ai_weights.json");
        else
             savePath = Path.Combine(Directory.GetCurrentDirectory(), "ai_weights.json");
    }

    public void InitialiseWeights()
    {
        for (int i = 0; i < weights.Length; i++)
        {
            weights[i] = Random.Range(-1.0f, 1.0f);
        }
        bias = Random.Range(-1.0f, 1.0f);
    }

    public double CalcOutput(double i1, double i2)
    {
        double[] inputs = { i1, i2 };
        double dp = DotProductBias(weights, inputs);
        return (dp > 0) ? 1 : 0;
    }

    double DotProductBias(double[] v1, double[] v2)
    {
        if (v1 == null || v2 == null || v1.Length != v2.Length) return -1;
        double d = 0;
        for (int x = 0; x < v1.Length; x++) d += v1[x] * v2[x];
        d += bias;
        return d;
    }

    public void Train(double i1, double i2, double desiredOutput)
    {
        double currentOutput = CalcOutput(i1, i2);
        double error = desiredOutput - currentOutput;
        weights[0] += error * i1 * learningRate;
        weights[1] += error * i2 * learningRate;
        bias += error * learningRate;
    }

    public void SaveWeights()
    {
        BrainData data = new BrainData();
        data.weights = weights;
        data.bias = bias;
        string json = JsonUtility.ToJson(data);
        
        try
        {
            File.WriteAllText(savePath, json);
        }
        catch (System.Exception)
        {
        }
    }

    public bool LoadWeights(string pathFromMenu)
    {
        if (File.Exists(pathFromMenu))
        {
            try 
            {
                string json = File.ReadAllText(pathFromMenu);
                BrainData data = JsonUtility.FromJson<BrainData>(json);
                this.weights = data.weights;
                this.bias = data.bias;
                return true;
            }
            catch
            {
                return false;
            }
        }
        return false;
    }

    public bool LoadWeightsFromText(string jsonString)
    {
        try 
        {
            BrainData data = JsonUtility.FromJson<BrainData>(jsonString);
            this.weights = data.weights;
            this.bias = data.bias;
            return true;
        }
        catch
        {
            return false;
        }
    }
}