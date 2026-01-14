// R3 T04 Graf 05 Obilazak: Primer: Muzej:  https://petlja.org/sr-Latn-RS/kurs/17918/4/5358#id11 Implicitna reperzentacija
// DFS obilazak matrice iterativno (rucno) bez rekurzije i bez steka
using System;
// using System.Collections.Generic;

class R3_T04_Graf_05_Obilazak_Muzej_Matrica_Ver_003_Rekrzija
{
    public static int[,] A = new int[,]     // Matrica (graf) ORIGINAL
    {
            { 11, 12, 13, 14, 15 },         // ABCDE    { 11, 12, 13, 14, 15 },         
            { 21, -1, 23, -1, 25 },         // F G H    { 21, -1, 23, -1, 25 },
            { -1, -1, 33, -1, -1 },         // X I Y    { 31, -1, 33, -1, 35 },
            { 41, -1, 43, -1, 45 },         // J K L    { 41, -1, 43, -1, 45 },
            { 51, 52, 53, 54, 55 }          // MNOPQ    { 51, 52, 53, 54, 55 }
    };

    public static int Start_X = 2;          // X start =  2 : Pocetna pozicija, odnosno X koordinata pocetnog cvora
    public static int Start_Y = 2;          // Y start =  2 : Pocetna pozicija, odnosno Y koordinata pocetnog cvora
    public static int Stop_X = -1;          // X stop  = -1 : Krajnja pozicija, odnosno X koordinata ciljnog cvora (za npr lavirint), ako je -1 nema ciljnog cvora
    public static int Stop_Y = -1;          // Y stop  = -1 : Krajnja pozicija, odnosno Y koordinata ciljnog cvora (za npr lavirint), ako je -1 nema ciljnog cvora

    public static bool bSmer_Reset = false; // Ako je bSmer_Reset, onda svaki put smer krece od 0, pre petlje do 3 u petlji, u suprotnom nastavlja sa istim poslednjim smerom

    // Sve gore navedene globalne promenljive matrica A, StartXY i StopXY se mogu uobicajeno ucitavati sa tastature
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
        int[,] G = Graf_Matrica_G_Ini(A);                                               // G Matrica (graf) pomocna za obelezavanje posecenih cvorova
        Graf_Obilazak_DFS_Matrica_Rekurzija_Ver_003(Start_X, Start_Y, G, Stop_X, Stop_Y);   // Obilazak DFS matrice iterativno (rucno) bez rekurzije i steka
    }

    static void Graf_Obilazak_DFS_Matrica_Rekurzija_Ver_003(int X, int Y, int[,] G, int XX = -1, int YY = -1)
    {
        int smer = 0;
        int brojac_posecenih_cvorova_ulaz = 0;          // Brojac cvorova koji su prvi put poseceni
        int brojac_posecenih_cvorova_izlaz = 0;         // Brojac cvorova koji su poslednji put poseceni

        bool bObilazak_DFS_Moze = true;                 // Obilazak DFS je moguc
        while (bObilazak_DFS_Moze)                      // Sve dok je DFS obilazak moguc
        {
            if (X == XX && Y == YY) bObilazak_DFS_Moze = false;     //  Ako smo stigli do ciljnog cvora XX YY
            if (G[X, Y] == 1) DFS_Poseti_cvor_XY_i_Obelezi_da_je_posecen_Rekurzija(X, Y, G, ref brojac_posecenih_cvorova_ulaz);            // Cvor XY nije posecen

            bool bCvor_susedni_koji_nije_posecen_Postoji = DFS_Cvor_XY_Susedni_nije_posecen_Postoji(ref X, ref Y, G, smer);
            if (!bCvor_susedni_koji_nije_posecen_Postoji)
            {
                if (G[X, Y] == 2) DFS_Poseti_cvor_XY_i_Obelezi_da_je_posecen_Rekurzija(X, Y, G, ref brojac_posecenih_cvorova_izlaz, true); // Cvor XY povratak
                bool bCvor_susedni_Stanje_2_Postoji = DFS_Cvor_XY_Susedni_nije_posecen_Postoji(ref X, ref Y, G, smer, 2);
                if (!bCvor_susedni_Stanje_2_Postoji) bObilazak_DFS_Moze = false;    // Ako ne postoji ni jedan od 4 moguca susedna cvora koji je dostupan i nije posecen, 
            }                                                                       // onda je kraj
        }
    }
    static void DFS_Poseti_cvor_XY_i_Obelezi_da_je_posecen_Rekurzija(int X, int Y, int[,] G, ref int brojac_posecenih_cvorova, bool bIzlazna_obrada = false)
    {
        // Korak 1: Zapamti da si posetio cvor C
        // Korak 2: Izvrsi ulaznu obradu cvora C
        string sKomentar_Povratak = (bIzlazna_obrada) ? " : Povratak" : "";
        Console.WriteLine(A[X, Y] + " " + brojac_posecenih_cvorova + "" + sKomentar_Povratak);
        G[X, Y]++;      // G[X, Y] = 2; odnosno G[X, Y] = 3; --> ovim obelazavamo (zapisujemo) da je cvor XY posecen
        brojac_posecenih_cvorova++;     // u zavisnosti od konkretnog zadatka (npr. da li se trazi udaljenost cvorova i sl) ova promenljiva ce se drugacije obradjivati 

        // Korak 3: Za svaki cvor c' koji je susedan cvoru C
        int smer = 0;
        // Korak 4:     Ako cvor c' nije posecen
        if (DFS_Cvor_XY_Susedni_nije_posecen_Postoji(ref X, ref Y, G, smer))
            // Korak 5:         Poseti cvor c'
            DFS_Poseti_cvor_XY_i_Obelezi_da_je_posecen_Rekurzija(X, Y, G, ref brojac_posecenih_cvorova);

        // Korak 6: Izvrsi izlaznu obradu cvora C
        if (DFS_Cvor_XY_Susedni_nije_posecen_Postoji(ref X, ref Y, G, smer, 2))
            DFS_Poseti_cvor_XY_i_Obelezi_da_je_posecen_Rekurzija(X, Y, G, ref brojac_posecenih_cvorova, true);
    }

    static bool DFS_Cvor_XY_Susedni_nije_posecen_Postoji(ref int X, ref int Y, int[,] G, int smer, int STANJE = 1)
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
    static bool Graf_Matrica_Da_li_je_cvor_XY_unutar_matrice(int x, int y, int[,] G)    // Provera da li se cvor XY nalazi unutar matrice
    {
        int N = G.GetLength(0);                     // Dimenzija matrice A: Broj redova
        int M = G.GetLength(1);                     // Dimenzija matrice A: Broj kolona
        return x >= 0 && x < N && y >= 0 && y < M;  // Ova metoda moze da se izostavi ukoliko se matrici G doda okvir u odnosu na original A (N+2, M+2)
    }
}

// https://codeblog.rs/clanci/bfs_i_dfs#uvod
// https://www.youtube.com/watch?v=84jNzUOY78c
// https://petlja.org/sr-Latn-RS/biblioteka/r/Zbirka2/broj_belih_oblasti slican zadatak Broj belih oblasti
