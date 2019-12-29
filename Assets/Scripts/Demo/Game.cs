using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{

    private Transform _orcTrans;
    private Transform _goblinTrans;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            //将屏幕坐标转换为世界坐标
            Vector3 mousePos = Input.mousePosition;
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(mousePos);
            mousePosition.z = 0;

            //点击鼠标左键添加哥布林，点击鼠标右键添加兽人
            if (Input.GetMouseButtonDown(0))
            {
                if (_goblinTrans == null)
                {
                    _goblinTrans = (Instantiate(Resources.Load("Goblin")) as GameObject).transform;
                }
                _goblinTrans.name = "Goblin";
                _goblinTrans.transform.position = mousePosition;
            }
            else if (Input.GetMouseButtonDown(1))
            {
                if (_orcTrans == null)
                {
                    _orcTrans = (Instantiate(Resources.Load("Orc")) as GameObject).transform;
                }
                _orcTrans.name = "Orc";
                _orcTrans.transform.position = mousePosition;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("main");
        }

    }
}
