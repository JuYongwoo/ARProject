using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitlePanel : MonoBehaviour
{

    enum TitleObjs
    {
        StartBtn,
        HowtoBtn,
        HowtoImg,
        HowtoExitBtn
    }

    private Dictionary<TitleObjs, GameObject> titleMap;

    private void Awake()
    {
        titleMap = Util.cacheDictionaryUIObjs<TitleObjs>(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        titleMap[TitleObjs.HowtoImg].SetActive(false);

        titleMap[TitleObjs.StartBtn].GetComponent<Button>().onClick.AddListener( () => { SceneManager.LoadScene("InGame"); } );
        titleMap[TitleObjs.HowtoBtn].GetComponent<Button>().onClick.AddListener( () => { titleMap[TitleObjs.HowtoImg].SetActive(true); } );
        titleMap[TitleObjs.HowtoExitBtn].GetComponent<Button>().onClick.AddListener( () => { titleMap[TitleObjs.HowtoImg].SetActive(false); } );


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
