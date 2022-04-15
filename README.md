# Matrix-and-Stream-Ciphers

## Instrukcja do zadania 1.

![zadanie 1](/docs/zad1.PNG)

![zadanie 1 gui](/docs/zad1screen.PNG)

W zadaniu 1. zaimplementowano algorytm typu rail fence
1. Wybierz zad1 w lewym górnym comboboxie
2. Wybierz wysokość RailFence (height)
3. Wpisz 'Text to encrypt' i naciśnij Encrypt
4. Aby odszyfrować naciśnij copy, a następnie decrypt.



## Instrukcja do zadania 2a

![zadanie 2a](/docs/zad2a.PNG)

![zadanie 2a gui](/docs/zad2aScreen.PNG)

W zadaniu 2a zaimplementowano szyfr oparty o klucz zamieniający kolejność
odczytywania kolejnych liter z macierzy.
1. Wybierz zad2a w lewym górnym comboboxie.
2. Wprowadź klucz w polu Key np. 2-3-4-1 lub 5-2-3-1-4
3. Wpisz tekst do zaszyfrowania i naciśnij encrypt.
4. Aby odszyfrować naciśnij copy, a następnie decrypt.



## Instrukcja do zadania2b

![zadanie 2b](/docs/zad2b.PNG)

![zadanie 2b gui](/docs/zad2bScreen.PNG)

W zadaniu 2b zaimplementowano szyfr oparty o klucz, który posortowany
określa kolejność odczytywania pełnych kolumn z macierzy.
1. Wybierz zad2b w lewym górnym rogu.
2. Wprowadź dowolny klucz możliwy do alfabetycznego posortowania.
3. Wpisz tekst do zaszyfrowania i naciśnij encrypt.
4. Aby odszyfrować naciśnij copy, a następnie decrypt.

# Szyfr strumieniowy oparty o generato LFSR (linear-feedback shift register)

![encrypted](docs/stream1.PNG)

![decrypted](docs/stream2.PNG)

## Instrukcja korzystania z GUI

1. Wprowadź ciąg bitów o dowolnej długości w pole 'Input'
2. Wpisz ciąg bitów określający początkowe nastawienie generatora w pole 'seed' (długość od 1-9)
3. Ustal długość wielomianu szyfrującego (stopień odpowiada długości ciągu seed od 1 do 9) np. 10010 to 'degree' = 5
4. Kliknij 'Pipe' w celu szyfracji.
5. Skopiuj output i wklej w input. Ponownie kliknij Pipe w celu Deszyfracji

## Zasada działania
LFSR wygeneruje ten sam ciąg bitów jeżeli seed będzie jednakowy. Dla każdego bitu z input i bitu wygenerowanego stosuję operację xor. Wynik dodaje do wyjścia.
Dzięki odwrotności działania 'xor' ciąg zaszyfrowany można zdeszyfrować posiadając te same ustawienia szyfru.


