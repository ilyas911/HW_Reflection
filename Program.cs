using HW_Reflection.Classes;
using HW_Reflection.Interfaces;
using System;
using System.Diagnostics;
using Newtonsoft.Json;
class Program
{
    static void Main()
    {
        ICsvSerializer csvSerializer = new MyCsvSerializer();
        IOutputInfo outputInfo = new MyOutputInfoConsole();
        IInputData inputData = new MyInputDataConsole();
        var someObject = new F() 
        { 
            i1 = 1, 
            i2 = 2, 
            i3 = 3,
            i4 = 4, 
            i5 = 5 
        };
        List<F> objects = new List<F>();
        objects.Add(someObject);
        inputData.InitializeInput_IterationsCount();
        int iterations = inputData.InputIterationsCount_WithCheck();
        
        var stopWatcher_CustomSerialize = Stopwatch.StartNew();
        string serialize = string.Empty;
        for (int i = 0; i < iterations; i++)
        {
            serialize = csvSerializer.SerializeToCsv<F>(objects);
        }
        stopWatcher_CustomSerialize.Stop();

        var stopWatcher_CustomDeserialize = Stopwatch.StartNew();
        List<F> deserializedF = new List<F>();
        for (int i = 0; i < iterations; i++)
        {
            deserializedF = csvSerializer.DeserializeFromCsv<F>(serialize);
        }
        stopWatcher_CustomDeserialize.Stop();

        var stopWatcher_SerializeJSONbyDefault = Stopwatch.StartNew();
        serialize = string.Empty;
        for (int i = 0; i < iterations; i++)
        {
            serialize = JsonConvert.SerializeObject(objects);
        }
        stopWatcher_SerializeJSONbyDefault.Stop();

        var stopWatcher_DeserializeJSONbyDefault = Stopwatch.StartNew();
        deserializedF = new List<F>();
        for (int i = 0; i < iterations; i++)
        {
            deserializedF = JsonConvert.DeserializeObject<List<F>>(serialize); 
        }
        stopWatcher_DeserializeJSONbyDefault.Stop();
        outputInfo.OutputResearchInfo(stopWatcher_CustomSerialize.Elapsed.ToString(),
                                      stopWatcher_SerializeJSONbyDefault.Elapsed.ToString(),
                                      stopWatcher_CustomDeserialize.Elapsed.ToString(),
                                      stopWatcher_DeserializeJSONbyDefault.Elapsed.ToString(),
                                      "NewtonSoft.Json");
    }
}