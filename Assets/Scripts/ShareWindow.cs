using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ShareWindow : MonoBehaviour
{
    [SerializeField] RectTransform fatorahScrollContant;
    [SerializeField] GameObject ShareCanvas;
    [SerializeField] GameObject ShareWindowContant;
    [SerializeField] ScrollRect ShareWindowScrollView;
    public GameObject shareWindowObject;
    [SerializeField] GameObject SharePrefabOriginal;
    [SerializeField] GameObject SharePrefabVariant;
    [SerializeField] Camera ShareCamera;
    GameObject duplicate;

    bool stop = false;

    private void Update()
    {
        if (duplicate != null)
        {
            duplicate.transform.localPosition = new Vector3(0, 0, 0);
        }
    }



    public void ShareButton()
    {
        while (stop == false)
        {
            stop = true;

            duplicate = Instantiate(fatorahScrollContant.gameObject, new Vector3(0, 0, 0), Quaternion.identity);
            duplicate.transform.SetParent(ShareCanvas.transform);
            duplicate.transform.localScale = new Vector3(1, 1, 1);
            duplicate.transform.localPosition = new Vector3(0, 0, 0);

            RectTransform uitransform = duplicate.GetComponent<RectTransform>();

            uitransform.anchorMin = new Vector2(0f, 0f);
            uitransform.anchorMax = new Vector2(1f, 1f);
            uitransform.pivot = new Vector2(0.5f, 0.5f);

            GameObject ShareLastTrials1 = Instantiate(SharePrefabVariant, new Vector3(0, 0, 0), Quaternion.identity);
            ShareLastTrials1.transform.SetParent(duplicate.transform);
            ShareLastTrials1.transform.localScale = new Vector3(1, 1, 1);
            ShareLastTrials1.transform.localPosition = new Vector3(0, 0, 0);


            GameObject duplicate2 = Instantiate(fatorahScrollContant.gameObject, new Vector3(0, 0, 0), Quaternion.identity);
            duplicate2.transform.SetParent(ShareWindowContant.transform);
            duplicate2.transform.localScale = new Vector3(1, 1, 1);

            GameObject ShareLastTrials = Instantiate(SharePrefabOriginal, new Vector3(0, 0, 0), Quaternion.identity);
            ShareLastTrials.transform.SetParent(duplicate2.transform);
            ShareLastTrials.transform.localScale = new Vector3(1, 1, 1);

            ShareWindowScrollView.normalizedPosition = new Vector2(1, ShareWindowScrollView.normalizedPosition.y + SharePrefabOriginal.GetComponent<RectTransform>().sizeDelta.y);


            //ShareWindowContant.GetComponent<RectTransform>().sizeDelta = new Vector2(duplicate2.GetComponent<RectTransform>().sizeDelta.x + 100, duplicate2.GetComponent<RectTransform>().rect.height + 100 );





            //float w = duplicate.transform.localScale.x;
            //float h = duplicate.transform.localScale.y;
            //float x = w * 0.5f - 0.5f;
            //float y = h * 0.5f - 0.5f;

            //ShareCamera.transform.position = new Vector3(x, y, -10f);

            //ShareCamera.orthographicSize = ((w > h * ShareCamera.aspect) ? (float)w / (float)ShareCamera.pixelWidth * ShareCamera.pixelHeight : h) / 2;

            ShareCamera.targetTexture = new RenderTexture((int)fatorahScrollContant.rect.width, (int)fatorahScrollContant.rect.height + 350, 24);
            ShareCamera.usePhysicalProperties = true;

        }




    }
}
