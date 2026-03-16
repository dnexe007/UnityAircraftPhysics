using UnityEngine;
using TMPro;

public class MarksGenerator : MonoBehaviour
{
    public GameObject HorizonMark;
    public GameObject ShortMark;

    [ContextMenu("Generate")]
    public void Generate()
    {
        if (HorizonMark == null || ShortMark == null) return;
        for(int i = 90; i >= -90; i -= 5)
        {
            if(i % 10 != 0)
            {
                Instantiate(ShortMark, transform);
                continue;
            }

            GameObject cur = Instantiate(HorizonMark, transform);
            cur.transform.Find("Left").GetComponent<TMP_Text>().text = $"{Mathf.Abs(i)}";
            cur.transform.Find("Right").GetComponent<TMP_Text>().text = $"{Mathf.Abs(i)}";
        }
    }
}
