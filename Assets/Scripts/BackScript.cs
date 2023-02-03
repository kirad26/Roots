using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BackScript : MonoBehaviour
{
    private List<Transform> segments = new List<Transform>(); // Хранит в себе список созданных корней
    private Transform segment; // используется для перемещения корня.
    public GameObject segmentPrefab; // префаб корня
    public GameObject rowPrefab; // префаб стрелочки над корнями
    public Vector3 xf1; // очень важен он отвечает за перемещение корня. Берет значение с segments 
    public TextMeshProUGUI tmpText; // отвечает за то, чтобы изменить кол-во воды.
    [SerializeField] private Slider uFill; // таймер
    public int Duration; // задержка
    private int remainingDuration; // помогает в задержке
    private int flag = 0; // отвечает за проверку какую клавишу мы нажали. Сохраняет направленность роста
    public int stop = 0; // когда корень подходит к камня мы перестаем добавлять новые корни, но вода убывает
    public bool st = true; // отвечает за то, чтобы изменить положение корня не относительно прошлого корня, а за обьетом, которому коснулись

    private void Start()
    {
        ResetState(); // отвечает за первое положение корня
        Being(Duration); // запускает таймер с задержкой
    }
    private void Update()
    {
        DirectinCreate(); // проверяет на нажатия клавиш
    }

    private void Being(int second) // к таймеру
    {
        remainingDuration = second;
        StartCoroutine(UpdateTimer());
    }
    private IEnumerator UpdateTimer()
    {
        while (remainingDuration >= 0)
        {
            uFill.value = Mathf.InverseLerp(0, Duration, remainingDuration);
            remainingDuration--;
            yield return new WaitForSeconds(1f);
        }
        GetElement();
    }
    public void GetElement() // Самый важный аспект. Функция, которая отрисовывает корень
    {
        int tms = int.Parse(tmpText.text); // перевод текстовое значение в инт. Наш ресурсы.
        if (tms != 0)
        {
            tms--;
            if (stop == 0)
            {
                segment = Instantiate(segmentPrefab.transform); // получает значение с префаба корня
                if (st)
                {
                    xf1 = segments[segments.Count - 1].position; // сохраняет в себя положение прошлого корня
                }
                if (flag == 0)
                {
                    segment.position = new Vector3(xf1.x, xf1.y - 0.6f, 0); // отрисовка корня если корень смотрит вниз
                    segment.transform.localEulerAngles = new Vector3(0, 0, 0); // тоже самое, но только для стрелки
                    if (segment.GetComponent<SpriteRenderer>().flipX == false) // отвечает за перерисовку (зеркалька)
                    {
                        segmentPrefab.GetComponent<SpriteRenderer>().flipX = true;
                        segment.GetComponent<SpriteRenderer>().flipX = true;
                    }
                    else
                    {
                        segmentPrefab.GetComponent<SpriteRenderer>().flipX = false;
                        segment.GetComponent<SpriteRenderer>().flipX = false;
                    }
                }
                if (flag == 1)
                {
                    segment.position = new Vector3(xf1.x + 0.33f, xf1.y - 0.5f, 0); // аналогично, но влево
                    segment.transform.localEulerAngles = new Vector3(0, 0, 35);
                    if (segment.GetComponent<SpriteRenderer>().flipX == false)
                    {
                        segmentPrefab.GetComponent<SpriteRenderer>().flipX = true;
                        segment.GetComponent<SpriteRenderer>().flipX = true;
                    }
                    else
                    {
                        segmentPrefab.GetComponent<SpriteRenderer>().flipX = false;
                        segment.GetComponent<SpriteRenderer>().flipX = false;
                    }
                }
                if (flag == 2)
                {
                    segment.position = new Vector3(xf1.x - 0.33f, xf1.y - 0.5f, 0); // аналогично, но в право
                    segment.transform.localEulerAngles = new Vector3(0, 0, -35);
                    if (segment.GetComponent<SpriteRenderer>().flipX == false)
                    {
                        segmentPrefab.GetComponent<SpriteRenderer>().flipX = true;
                        segment.GetComponent<SpriteRenderer>().flipX = true;
                    }
                    else
                    {
                        segmentPrefab.GetComponent<SpriteRenderer>().flipX = false;
                        segment.GetComponent<SpriteRenderer>().flipX = false;
                    }
                }
                rowPrefab.transform.position = segment.position; // изменяет положение созданного префаба на то, что указали выше после проверок
                segments.Add(segment); // добавляем в список созданный корень
                st = true; // отвечает за то, чтобы снять блокировку, когда обьект не касается триггера
            }
            tmpText.text = tms.ToString(); // возвращаем полученный результат нашему ресурсы
            uFill.value = 1; // вновь запускает таймер
            Being(Duration);
        }
        else
        {
            Being(Duration); // запускает таймер
        }
    }
    public void ResetState() // создает наш список
    {
        for (int i = 1; i < segments.Count; i++)
        {
            Destroy(segments[i].gameObject);
        }
        segments.Clear();
        segments.Add(transform);
    }
    private void DirectinCreate() // проверка на нажатие клавиш
    {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || flag == 0)
        {
            rowPrefab.transform.localEulerAngles = new Vector3(0, 0, 0);
            flag = 0;
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) || flag == 1)
        {
            rowPrefab.transform.localEulerAngles = new Vector3(0, 0, 35);
            flag = 1;
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || flag == 2)
        {
            rowPrefab.transform.localEulerAngles = new Vector3(0, 0, -35);
            flag = 2;
        }
    }
}
