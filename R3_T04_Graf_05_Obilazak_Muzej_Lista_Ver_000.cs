using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

class R3_T04_Graf_05_Obilazak_Muzej_Lista_Ver_000
{
    public static int[,] A = new int[,]     // Matrica (graf) ORIGINAL
    {
            { 11, 12, 13, 14, 15 },         // ABCDE    { 11, 12, 13, 14, 15 },         
            { 21, -1, 23, -1, 25 },         // F G H    { 21, -1, 23, -1, 25 },
            { 31, -1, 33, -1, 35 },         // X I Y    { 31, -1, 33, -1, 35 },
            { 41, -1, 43, -1, 45 },         // J K L    { 41, -1, 43, -1, 45 },
            { 51, 52, 53, 54, 55 }          // MNOPQ    { 51, 52, 53, 54, 55 }
    };
    static void Main()
    {
        List<int>[] G = new List<int>[]
        {
            new List<int>{ 12, 21 },        // 11
            new List<int>{ 11, 13 },        // 12
            new List<int>{ 12, 14, 23 },    // 13
            new List<int>{ 13, 15 },        // 14
            new List<int>{ 14, 25 },        // 15

            new List<int>{ 11, 31 },        // 21
            new List<int>{ 12, 21, 23 },    // 22
            new List<int>{ 13, 33 },        // 23
            new List<int>{ 14, 23, 25 },    // 24
            new List<int>{ 15, 35 },        // 25

            new List<int>{ 21, 41 },        // 31
            new List<int>{ 31, 33 },        // 32
            new List<int>{ 22, 43 },        // 33
            new List<int>{ 33, 35 },        // 34
            new List<int>{ 25, 45 },        // 35

            new List<int>{ 31, 51 },        // 41
            new List<int>{ 41, 43, 52 },    // 42
            new List<int>{ 33, 53 },        // 43
            new List<int>{ 43, 45, 54 },    // 44
            new List<int>{ 35, 55 },        // 45

            new List<int>{ 41, 52 },        // 51
            new List<int>{ 51, 53 },        // 52
            new List<int>{ 43, 52, 54 },    // 53
            new List<int>{ 53, 55 },        // 54
            new List<int>{ 54, 45 },        // 55
        };

        DFS(0, G);
        Console.WriteLine("K");
    }

    static void DFS(int cvor, List<int>[] susedi)
    {
        int n = susedi.Length;
        bool[] posecen = new bool[n];
        // return 
            DFS(cvor, susedi, posecen);
    }
    static void DFS(int cvor, List<int>[] susedi, bool[] posecen)
    {
        Console.WriteLine("Poseta cvoru {0}", cvor);
        posecen[cvor] = true;
        foreach (int sused in susedi[cvor])
            if (!posecen[sused])
                DFS(sused, susedi, posecen);
        // DFS(sused, graf, posecen);
    }
}
