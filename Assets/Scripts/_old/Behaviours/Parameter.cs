using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Parameter
{
    public string name;
    public TypeSO type;

    public string GetInspectorName() {

        List<char> charList = new List<char>(name.ToCharArray());

        List<char> inspectorCharList = new List<char>();
        for (int i = 0; i < charList.Count; i++) {
            if (i == 0) {
                inspectorCharList.Add(char.ToUpper(charList[i]));
            } else {
                if (char.IsUpper(charList[i])) {
                    inspectorCharList.Add(' ');
                }
                inspectorCharList.Add(charList[i]);
            }
        }

        return new string(inspectorCharList.ToArray());
    }
}
