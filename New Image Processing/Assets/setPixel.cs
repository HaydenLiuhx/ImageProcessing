using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class setPixel : MonoBehaviour
{
    //button
    public bool startB = false;
    public bool fillB = false;
    public bool exitB = false;
    //save as txt
    StringBuilder sb = new StringBuilder();
    StreamWriter writer;
    //polygon的顶点
    public List<Vector2> fingerPositions;
    //texture所有像素点
    public List<Vector2> allPoints;
    //polygon区域
    public List<Vector2> areaPoints;
    //test
    private bool t = true;
    public Texture2D tmpTexture;
    public int pointNumber = 0;

    //From move脚本
    public List<GameObject> objs;

    // Start is called before the first frame update
    private void Awake()
    {
        Texture2D texture = (Texture2D)GetComponent<UnityEngine.UI.RawImage>().texture;
        tmpTexture = new Texture2D(texture.width, texture.height);
        for (int y = 0; y < tmpTexture.height; y++)
        {
            for (int x = 0; x < tmpTexture.width; x++)
            {
                tmpTexture.SetPixel(x, y, texture.GetPixel(x, y));
            }
        }
        tmpTexture.Apply();
    }
    void Start()
    {
        
        //addAllPoints();
        GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<UnityEngine.UI.RawImage>().texture.width, GetComponent<UnityEngine.UI.RawImage>().texture.height);
        StartCoroutine(addPoint());


        //
        FileInfo file = new FileInfo(Application.dataPath + "/mytxt.txt");
        if (file.Exists)
        {
            file.Delete();
            file.Refresh();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log(objs.Count);
        }
        //if (t == true)
        //{
        //    Test();
        //}
        //if (pointNumber >1)
        //{
        //    int lineNumber = fingerPositions.Count-1;
        //    float _x = (GetComponent<RectTransform>().sizeDelta.x + 2 * GetComponent<RectTransform>().anchoredPosition.x) * transform.localScale.x;
        //    float _y = (GetComponent<RectTransform>().sizeDelta.y + 2 * GetComponent<RectTransform>().anchoredPosition.y) * transform.localScale.y;
        //    int X_0 = Convert.ToInt32((fingerPositions[lineNumber-1].x - (transform.position.x - _x / 2)) / transform.localScale.x);
        //    int X_1 = Convert.ToInt32((fingerPositions[lineNumber].x - (transform.position.x - _x / 2)) / transform.localScale.x);
        //    int Y_0 = Convert.ToInt32((fingerPositions[lineNumber - 1].y - (transform.position.y - _y / 2)) / transform.localScale.y);
        //    int Y_1 = Convert.ToInt32((fingerPositions[lineNumber].y - (transform.position.y - _y / 2)) / transform.localScale.y);
        //    DrawLine(tmpTexture, X_0, Y_0, X_1, Y_1, Color.red);
        //    GetComponent<UnityEngine.UI.RawImage>().texture = tmpTexture;
        //    pointNumber--;
        //}
        //if (startB == true)
        //{
        //    if (Input.GetMouseButtonUp(0))
        //    {
        //        Debug.Log(Input.mousePosition);
        //        fingerPositions.Add(Input.mousePosition);
        //        Debug.Log(fingerPositions.Count );
        //        float _x = (GetComponent<RectTransform>().sizeDelta.x + 2 * GetComponent<RectTransform>().anchoredPosition.x) * transform.localScale.x;
        //        float _y = (GetComponent<RectTransform>().sizeDelta.y + 2 * GetComponent<RectTransform>().anchoredPosition.y) * transform.localScale.y;
        //        int _X = Convert.ToInt32((Input.mousePosition.x - (transform.position.x - _x / 2)) / transform.localScale.x);
        //        int _Y = Convert.ToInt32((Input.mousePosition.y - (transform.position.y - _y / 2)) / transform.localScale.y);
        //        fillColor(tmpTexture, _X, _Y);
        //        GetComponent<UnityEngine.UI.RawImage>().texture = tmpTexture;
        //        pointNumber++;
        //    }
        //}

        if (fillB == true)
        {
            Vector2[] polygon = new Vector2[objs.Count];
            for (int i = 0; i < objs.Count; i++)
            {
                polygon[i].x = objs[i].transform.position.x;
                polygon[i].y = objs[i].transform.position.y;
            }
            foreach (var p in allPoints)
            {
                if (IsPointInPolygon(p, polygon) == true)
                {

                    sb.Append(p);
                    areaPoints.Add(p);
                }
            }
            //SaveAsTxt("/save.txt");

            fillB = false;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
           

            
            //foreach (var o in areaPoints)
            
            //{
                
            //    Color imageColor = texture.GetPixel(Mathf.FloorToInt(o.x), Mathf.FloorToInt(o.y));
            //    WriteIntoTxt(imageColor.ToString());
            //}
           


            //SaveAsTxt("/savePixel.txt");
            Debug.Log("red");
            //anchorP影响sizeDelta
            float _x = (GetComponent<RectTransform>().sizeDelta.x + 2 * GetComponent<RectTransform>().anchoredPosition.x) * transform.localScale.x;
            float _y = (GetComponent<RectTransform>().sizeDelta.y + 2 * GetComponent<RectTransform>().anchoredPosition.y) * transform.localScale.y;
            Texture2D texture = (Texture2D)GetComponent<UnityEngine.UI.RawImage>().texture;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                for (int i = 0; i < areaPoints.Count; i++)
                {
                    //anchor position -- relative position
                    int X = Convert.ToInt32((areaPoints[i].x - (transform.position.x - _x / 2)) / transform.localScale.x);
                    int Y = Convert.ToInt32((areaPoints[i].y - (transform.position.y - _y / 2)) / transform.localScale.y);
                    //Debug.Log(X + " " + Y);
                    Color imageColor = texture.GetPixel(X,Y);
                    WriteIntoTxt(imageColor.ToString());
                    tmpTexture.SetPixel(X, Y, Color.red);
                }
                tmpTexture.Apply(true);
                //GetComponent<Image>().sprite = Sprite.Create(tmpTexture, new Rect(0.0f, 0.0f, tmpTexture.width, tmpTexture.height), new Vector2(0, 0), 1);
                GetComponent<UnityEngine.UI.RawImage>().texture = tmpTexture;
                Debug.Log("over");
                
            }
        }
    }
    void Test()
    {
        t = false;
        Texture texture = GetComponent<UnityEngine.UI.RawImage>().texture;
        //2D坐标
        Debug.Log("anchoredPosition" + GetComponent<RectTransform>().anchoredPosition);
        //世界坐标系的3D坐标
        Debug.Log("GetComponent<RectTransform>().position" + GetComponent<RectTransform>().position);
        //世界坐标
        Debug.Log("transform.position" + transform.position);
        Debug.Log(transform.localScale.x);
        Debug.Log(GetComponent<RectTransform>().sizeDelta.x +","+ GetComponent<RectTransform>().sizeDelta.y);
        //单位长度
        Debug.Log("GetComponent<RectTransform>().up" + GetComponent<RectTransform>().up);

        Debug.Log(texture.width + "," + texture.height);
        Texture2D tex = (Texture2D)GameObject.Find("Up").GetComponent<UnityEngine.UI.RawImage>().texture;
        for (int i = 600; i < 800; i++)
        {
            for(int j = 100; j < 400; j++)
            {
               
                tex.SetPixel(i,j, Color.red);
               
            }
        }
        tex.Apply();
        GameObject.Find("Up").GetComponent<UnityEngine.UI.RawImage>().texture = tex;

    }
    IEnumerator addPoint()
    {
        float _x = (GetComponent<RectTransform>().sizeDelta.x + 2 * GetComponent<RectTransform>().anchoredPosition.x) * transform.localScale.x;
        float _y = (GetComponent<RectTransform>().sizeDelta.y + 2 * GetComponent<RectTransform>().anchoredPosition.y) * transform.localScale.y;
        for (float x = transform.position.x - _x / 2; x < transform.position.x + _x / 2; x += 0.5f)
        {
            for (float y = transform.position.y - _y / 2; y < transform.position.y + _y / 2; y += 0.5f)
            {
                Vector2 point = new Vector2(x, y);
                allPoints.Add(point);
            }
        }
        yield return 0;
    }
    List<Vector2> addAllPoints()
    {
        float _x = (GetComponent<RectTransform>().sizeDelta.x + 2 * GetComponent<RectTransform>().anchoredPosition.x) * transform.localScale.x;
        float _y = (GetComponent<RectTransform>().sizeDelta.y + 2 * GetComponent<RectTransform>().anchoredPosition.y) * transform.localScale.y;
        for (float x = transform.position.x - _x / 2; x < transform.position.x + _x / 2; x += 0.5f)
        {
            for (float y = transform.position.y - _y / 2; y < transform.position.y + _y / 2; y += 0.5f)
            {
                Vector2 point = new Vector2(x, y);
                allPoints.Add(point);
            }
        }
        return allPoints;
    }
    bool IsPointInPolygon(Vector2 point, Vector2[] polygon)
    {
        int polygonLength = polygon.Length, i = 0;
        bool inside = false;
        // x, y for tested point.
        float pointX = point.x, pointY = point.y;
        // start / end point for the current polygon segment.
        float startX, startY, endX, endY;
        Vector2 endPoint = polygon[polygonLength - 1];
        endX = endPoint.x;
        endY = endPoint.y;
        while (i < polygonLength)
        {
            startX = endX; startY = endY;
            endPoint = polygon[i++];
            endX = endPoint.x; endY = endPoint.y;
            //
            inside ^= (endY > pointY ^ startY > pointY) /* ? pointY inside [startY;endY] segment ? */
                      && /* if so, test if it is under the segment */
                      ((pointX - endX) < (pointY - endY) * (startX - endX) / (startY - endY));
        }
        return inside;
    }
    public void SaveAsTxt(String fileName)
    {
        //写文件 文件名为save.text
        //这里的FileMode.create是创建这个文件,如果文件名存在则覆盖重新创建
        //String fileName = 
        FileStream fs = new FileStream(Application.dataPath + fileName, FileMode.Create);
        //存储时时二进制,所以这里需要把我们的字符串转成二进制
        byte[] bytes = new UTF8Encoding().GetBytes(sb.ToString());
        fs.Write(bytes, 0, bytes.Length);
        //每次读取文件后都要记得关闭文件
        fs.Close();
        //PlayerPrefs.SetString("color", getImageColor().ToString("F5"));

    }
    public void startButton()
    {
        StartCoroutine(setPixel.DelayToInvokeDo(() =>
        {
            startB = true; 
        }, 0.5f));

        //startB = true;
        
    }
    public void fillButton()
    {
        fillB = true;
        startB = false;
    }
    public void exitButton()
    {
        exitB = true;

    }
    public void fillColor(Texture2D t, int x , int y)
    {
        for(int i = (int)(x - 3); i < x + 4; i++)
        {
            for(int j = (int)(y - 3); j < y + 4; j++)
            {
                t.SetPixel(i, j, Color.red);
            }
        }
        t.Apply();
    }
    public static IEnumerator DelayToInvokeDo(Action action, float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
        action();
    }
    void DrawLine(Texture2D tex, int x0, int y0, int x1, int y1, Color col)
    {
        int dy = (int)(y1 - y0);
        int dx = (int)(x1 - x0);
        int stepx, stepy;

        if (dy < 0) { dy = -dy; stepy = -1; }
        else { stepy = 1; }
        if (dx < 0) { dx = -dx; stepx = -1; }
        else { stepx = 1; }
        dy <<= 1;
        dx <<= 1;

        float fraction = 0;

        tex.SetPixel(x0, y0, col);
        if (dx > dy)
        {
            fraction = dy - (dx >> 1);
            while (Mathf.Abs(x0 - x1) > 1)
            {
                if (fraction >= 0)
                {
                    y0 += stepy;
                    fraction -= dx;
                }
                x0 += stepx;
                fraction += dy;
                tex.SetPixel(x0, y0, col);
            }
        }
        else
        {
            fraction = dx - (dy >> 1);
            while (Mathf.Abs(y0 - y1) > 1)
            {
                if (fraction >= 0)
                {
                    x0 += stepx;
                    fraction -= dy;
                }
                y0 += stepy;
                fraction += dx;
                tex.SetPixel(x0, y0, col);
            }
        }
        tex.Apply();
    }

    public void WriteIntoTxt(string message)
    {
        FileInfo file = new FileInfo(Application.dataPath + "/mytxt.txt");
        if (!file.Exists)
        {
            writer = file.CreateText();
        }
        else
        {
            writer = file.AppendText();
        }
        writer.WriteLine(message);
        writer.Flush();
        writer.Dispose();
        writer.Close();
    }


}
