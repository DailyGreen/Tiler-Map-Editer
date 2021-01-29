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
        if (Mng.I.nCount.Equals(24))
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
                    if (Mng.I.mapTile[y, x]._code >= (int)TILE.GRASS_START) { sw.Write((char)Mng.I.mapTile[y, x]._code); }
                    else { sw.Write(Mng.I.mapTile[y, x]._code); }
                }
                sw.WriteLine();
            }
            sw.Close();
            Savetext.text = "저장 완료.";
            StartCoroutine("SaveTxt");
        }
        else
        {
            Savetext.text = "시작지점 24개 미만이에요.";
            StartCoroutine("SaveTxt");
        }
    }

    IEnumerator SaveTxt()
    {
        Savetext.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        Savetext.gameObject.SetActive(false);
    }
}
