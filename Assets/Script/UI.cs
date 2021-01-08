using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Text Savetext;
    public void MapfileSave()
    {
        FileInfo fi = new FileInfo(@"Assets/" + Mng.I._filename + ".txt");
        if (!fi.Exists)
        {
            FileStream file = File.Create(@"Assets/" + Mng.I._filename + ".txt");
            file.Close();
        }
        StreamWriter sw = new StreamWriter(@"Assets/" + Mng.I._filename + ".txt");
        for (int y = 0; y < Mng.I.getMapHeight; y++)
        {
            for (int x = 0; x < Mng.I.getMapwidth; x++)
            {
                sw.Write(Mng.I.mapTile[y, x]._code);
            }
            sw.WriteLine();
        }
        sw.Close();
        StartCoroutine("SaveTxt");
    }

    IEnumerator SaveTxt()
    {
        Savetext.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        Savetext.gameObject.SetActive(false);
    }
}
