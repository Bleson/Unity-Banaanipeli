using UnityEngine;
using System.Collections;

public class NameDisplay : LerpTransparency
{

    internal void ShowName(string name)
    {
        text.text = name;
        Refresh();
    }
}
