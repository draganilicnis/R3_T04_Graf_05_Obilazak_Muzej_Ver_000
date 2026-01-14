// R3 T04 Graf 05 Obilazak: Primer: Muzej:  https://petlja.org/sr-Latn-RS/kurs/17918/4/5358#id11 Implicitna reperzentacija
// DFS obilazak matrice iterativno (rucno) bez rekurzije i bez steka
using System;
// using System.Collections.Generic;

class R3_T04_Graf_05_Obilazak_Muzej_Matrica_2_Ver_002
{
    public static int[,] A = new int[,]     // Matrica (graf) ORIGINAL
    {
            { 11, 12, 13, 14, 15 },         // ABCDE    { 11, 12, 13, 14, 15 },         
            { 21, -1, 23, -1, 25 },         // F G H    { 21, -1, 23, -1, 25 },
            { -1, -1, 33, -1, -1 },         // X I Y    { 31, -1, 33, -1, 35 },
            { 41, -1, 43, -1, 45 },         // J K L    { 41, -1, 43, -1, 45 },
            { 51, 52, 53, 54, 55 }          // MNOPQ    { 51, 52, 53, 54, 55 }
    };
    public static int Start_X = 2;          // X start = 2 : Pocetna pozicija, odnosno X koordinata pocetnog cvora
    public static int Start_Y = 2;          // Y start = 2 : Pocetna pozicija, odnosno Y koordinata pocetnog cvora
    static int[,] Graf_Matrica_G_Ini(int[,] A)  // Matrica pomocna (graf) za obelezavanje posecenih cvorova (0: prepreka, 1: neposecen cvor, 2: posecen prvi put, 3: povratak
    {
        int N = A.GetLength(0);                 // Dimenzija matrice A: Broj redova
        int M = A.GetLength(1);                 // Dimenzija matrice A: Broj kolona
        int[,] G = new int[N, M];               // Elegantno resenje je da matrica G bude dimenzija int[,] G = new int[N + 2, M + 2]; odnosno da se doda okvir
        for (int i = 0; i < N; i++) for (int j = 0; j < M; j++) G[i, j] = (A[i, j] > 0) ? 1 : 0;    // Umesto 1 : 0; moze da bude 0 : -1;
        return G;
    }
    static void Main()
    {
        int[,] G = Graf_Matrica_G_Ini(A);                                   // Matrica (graf) pomocna obelezavanje posecenih cvorova
        Graf_Obilazak_DFS_Matrica_Rucno_Ver_002(Start_X, Start_Y, G, A);    // Obilazak DFS matrice iterativno (rucno) bez rekurzije i steka
    }

    static void Graf_Obilazak_DFS_Matrica_Rucno_Ver_002(int X, int Y, int[,] G, int[,] A)
    {
        int smer = 0;
        int brojac_posecenih_cvorova_ulaz = 0;          // Brojac cvorova koji su prvi put poseceni
        int brojac_posecenih_cvorova_izlaz = 0;         // Brojac cvorova koji su poslednji put poseceni

        bool bObilazak_DFS_Kraj = false;                // Obilazak DFS vise nije moguc
        while (!bObilazak_DFS_Kraj)
        {
            if (G[X, Y] == 1) DFS_Poseti_cvor_XY_i_Obelezi_da_je_posecen(X, Y, G, A, ref brojac_posecenih_cvorova_ulaz);            // Cvor XY nije posecen

            bool bCvor_susedni_koji_nije_posecen_Postoji = DFS_Cvor_XY_Susedni_nije_posecen_Postoji(ref X, ref Y, G, smer);
            if (!bCvor_susedni_koji_nije_posecen_Postoji)
            {
                if (G[X, Y] == 2) DFS_Poseti_cvor_XY_i_Obelezi_da_je_posecen(X, Y, G, A, ref brojac_posecenih_cvorova_izlaz, true); // Cvor XY povratak
                bool bCvor_susedni_Stanje_2_Postoji = DFS_Cvor_XY_Susedni_nije_posecen_Postoji(ref X, ref Y, G, smer, 2);
                if (!bCvor_susedni_Stanje_2_Postoji) bObilazak_DFS_Kraj = true; // Ako ne postoji ni jedan od 4 moguca susedna cvora koji je dostupan i nije posecen, 
            }                                                                   // onda je kraj
        }
    }
    static void DFS_Poseti_cvor_XY_i_Obelezi_da_je_posecen(int X, int Y, int[,] G, int[,] A, ref int brojac_posecenih_cvorova, bool bIzlazna_obrada = false)
    {
        string sKomentar_Povratak = (bIzlazna_obrada) ? " : Povratak" : "";
        Console.WriteLine(A[X, Y] + " " + brojac_posecenih_cvorova + "" + sKomentar_Povratak);
        G[X, Y]++;      // G[X, Y] = 2; odnosno G[X, Y] = 3; --> ovim obelazavamo (zapisujemo) da je cvor XY posecen
        brojac_posecenih_cvorova++;     // u zavisnosti od konkretnog zadatka (npr. da li se trazi udaljenost cvorova i sl) ova promenljiva ce se drugacije obradjivati 
    }

    static bool Graf_Matrica_Da_li_je_cvor_XY_unutar_matrice(int x, int y, int[,] G)    // Provera da li se cvor XY nalazi unutar matrice
    {
        int N = G.GetLength(0);                     // Dimenzija matrice A: Broj redova
        int M = G.GetLength(1);                     // Dimenzija matrice A: Broj kolona
        return x >= 0 && x < N && y >= 0 && y < M;  // Ova metoda moze da se izostavi ukoliko se matrici G doda okvir u odnosu na original A (N+2, M+2)
    }
    static bool DFS_Cvor_XY_Susedni_nije_posecen_Postoji(ref int X, ref int Y, int[,] G, int smer, int STANJE = 1, bool bSmer_Reset = false)
    {
        int Smer_Mx = 4;                            // Ukupan broj smerova
        int[] DX = new int[] { -1, +0, +1, +0 };    // S Z J I  // TLBR: Gore, Levo, Dole, Desno    // Pomeraji (susedni) po X (red:    gore, isti, dole, isti )
        int[] DY = new int[] { +0, -1, +0, +1 };    // S Z J I  // TLBR: Gore, Levo, Dole, Desno    // Pomeraji (susedni) po Y (kolona: ista, levo, ista, desno)

        if (bSmer_Reset) smer = 0;                  // Ako je bSmer_Reset, onda svaki put smer krece od 0, pre petlje do 3 u petlji, u suprotnom nastavlja sa istim poslednjim smerom
        bool bCvor_Susedni_koji_nije_posecen_Postoji = false;
        for (int cvor_susedni = 0; cvor_susedni < Smer_Mx; cvor_susedni++)    // Obilazak svih susednih cvorova u odnosu na trenutni XY i provera da li su poseceni
        {
            int x = X + DX[smer];                   // x koordinata susednog cvora od cvora X (moze biti X - 1, X ili X + 1)
            int y = Y + DY[smer];                   // y koordinata susednog cvora od cvora Y (moze biti Y - 1, Y ili Y + 1)
            if (Graf_Matrica_Da_li_je_cvor_XY_unutar_matrice(x, y, G) && G[x, y] == STANJE)     // STANJE = 1 : Cvor je dostupan i nije posecen, ako je = 2 posecen je.
            {
                X = x;      // Ako je susedni cvor xy od trenutnog cvora XY dostupan (i nije posecen) onda
                Y = y;      // taj cvor xy postaje sledeci trenutni cvor XY koji ce se obraditi
                bCvor_Susedni_koji_nije_posecen_Postoji = true;
                break;
            }
            smer = (smer + 1) % Smer_Mx;    // Ako susedni xy cvor od cvora XY nije dostupan (ili je posecen) pomeramo smer za 1.
        }
        return bCvor_Susedni_koji_nije_posecen_Postoji;
    }
}

// https://codeblog.rs/clanci/bfs_i_dfs#uvod
// https://www.youtube.com/watch?v=84jNzUOY78c
// https://petlja.org/sr-Latn-RS/biblioteka/r/Zbirka2/broj_belih_oblasti slican zadatak Broj belih oblasti
