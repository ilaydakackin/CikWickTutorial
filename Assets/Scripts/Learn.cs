using System.Collections.Generic;
using System.Globalization;
using NUnit.Framework;
using UnityEngine;

//MonoBehavior, oyun dünyası ile iletişim kurmamızı sağlayan köprü, Unity ile bağlantı aracı
public class Learn : MonoBehaviour
{
    //private, özel
    //public,halka açık
    //eğer önlerine ne public ne de private yazmazsak private olarak kabul edilir

    //OYUNLARDA KULLANILAN UNİTY TARAFINDAN HAZIR BULUNDURULAN FONKSİYONLAR(Sırasıyla):

    //1.Awake fonksiyonu, oyun başlamadan önce çalışan fonksiyondur.
    //Sadece 1 kez çalışır
    private void Awake()
    {
            
    }

//SerializeField değerin unity üzerinde değiştirilebilmesini ve görülebilmesini sağlar 
    [SerializeField] private int varible =6;
    //2.Start, oyun başlarken çalışan fonksiyondur
    //Sadece 1 kez çalışır
     List<int> numbersList = new List<int>();
    private void Start() {

    //Array'lerde index 0'dan başlar
    //Array'ler sadece aynı değişken tipi olan değişkenlerle çalışır.Tüm elemanlar ya int ya string ya da float olur.
        int[] numbers ={1, 2, 3, 10};
        //i değeri 0'dır bu değeri listenin kaç elemanı var ise arttıra arttıra döndür
        for(int i = 0; i < numbers.Length; i++)
        {
            
        }

    //List'ler
       

        numbersList.Add(11);
        numbersList.Add(17);
        PrintList();

        numbersList.Remove(10);
        PrintList();

        numbersList.Clear();
        PrintList();

    }

    public void PrintList()
    {
        for(int i = 0; i < numbersList.Count; i++)
        {
           
        }

    //myNumber isminde değişken belirle ve bu değişkeni numbersList isimli değişkenin içinde gezecek şekilde ayarla, arttırma işlemini ise otomatik olarak yapar üstteki for ile aynıdır.
        foreach(int myNumber in numbersList)
        {
            
        }
       

    }
    //3.Update, Sürekli çalışan bir fonksiyon
    //Çok dikkat et!! Çok fazla şey yazarsan Update patlar ve performansu düşürür
    //ne zaman olacağını bilmediğimiz şeyler için kullanılır
    private void Update() {
         
    }

    //4.FixedUpdate, fiziksel objeleri yönetirken kullanılır. 
    // Fbs açısından sorun çıkmaması adına fix ayarlar yapabilmek için kullanılır 
    private void FixedUpdate() {
        
    }
    //5.LateUpdate, Ubdate ve FixedUpdate'den sonra kullanılır 
    private void LateUpdate() {
        
    }

    //C# KONULARI

    //sosuz kez else if yazılabilir
    //sadece  birer tane else ve if yazılabilir
    //swich Bazı şeyleri koşul olarak kontrol eder ve döndürür
 
    //FOR döngüsü
    private void forLoopFunction()
    {
         for (int i = 0; i<10; i++)
        {
            Debug.Log(i);
        }
    }

  
  
    int number = 5;


    //küsüratlı sayılar, değer verirken mutlaka yanına küçük f yazmak zorundayız.
    float anotherNumber = 5.5f;
    string text = "Hello World!";
    bool isTrue = false;

    //void, hiçbirşey döndürmeyen fonksiyon tipi
    private void TestFunction()
    {
        number = 7;
        anotherNumber = 7.7f;
        text = "Selam";
        isTrue = true;
        Debug.Log("TextFunction");
    }
}
