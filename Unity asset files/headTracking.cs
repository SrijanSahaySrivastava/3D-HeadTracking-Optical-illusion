using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class headTracking : MonoBehaviour
{
    public UDPReceive udpReceive;
    public GameObject cameraObject;
    List<float> zList = new List<float>();
    List<float> yList = new List<float>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        string data = udpReceive.data;
        data = data.Remove(0,1);
        data = data.Remove(data.Length-1, 1);

        string[] points= data.Split(',');

        float z = (float.Parse(points[0])-320)/100;
        float y = (float.Parse(points[1])-240)/100;

        zList.Add(z);
        yList.Add(y);
        if(zList.Count>25) {zList.RemoveAt(0);}
        if(yList.Count>25) {yList.RemoveAt(0);}

        float zAverage = Queryable.Average(zList.AsQueryable());
        float yAverage = Queryable.Average(yList.AsQueryable());

        Vector3 cameraPos = cameraObject.transform.localPosition;
        Vector3 cameraRot = cameraObject.transform.eulerAngles;

        cameraObject.transform.localPosition = new Vector3(cameraPos.x, 1.51f-yAverage, 4f-zAverage);
        cameraObject.transform.eulerAngles = new Vector3(10.551f-yAverage*10, -90+zAverage*10, 0);
    }
}