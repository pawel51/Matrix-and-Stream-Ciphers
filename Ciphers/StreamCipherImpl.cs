using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace Ciphers
{
    public class StreamCipherImpl
    {
        private static T? ReadJsonFile<T>()
        {
            string json = "{}";
            try
            {
                using (StreamReader r = new StreamReader("Fi(x).json"))
                {
                    json = r.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Problem: " + e.Message);
            }
            T? structure = JsonConvert.DeserializeObject<T>(json);
            if (structure == null)
            {
                MessageBox.Show("Json deserializing problem");
                return default;
            }
            else
            {
                return structure;
            }
        }

        private int[] GenerateLFSR(int degree, int[] seed, int[] polymonial)
        {

            // Przypisanie seeda do bufora
            int[] buffer = new int[degree];
            for (int i = 0; i < degree; i++)
            {
                buffer[i] = seed[i];
            }

            // Policz jedynki na xor-ach w celu wygenerowania nowego bitu
            int sum = 0;
            for (int i = 0; i < degree; i++)
            {
                if (polymonial[i] == 1 && buffer[i] == 1)
                    sum += 1;
            }

            // shift
            for (int i = degree - 1; i > 0; i--)
            {
                buffer[i] = buffer[i - 1];
            }
            // wpisanie nowego bitu na wejście bufora
            buffer[0] = sum % 2 == 1 ? 1 : 0;

            return buffer;
        }

        /// <summary>
        /// Szyfruje inputStream za pomocą pseudolosowego generatora LFSR
        /// o długości = 'degree' zaczynając od ustawienia 'seed'
        /// </summary>
        /// <param name="inputStream"></param>
        /// <param name="seed"></param>
        /// <param name="degree"></param>
        /// <returns>Zwraca zaszyfrowany ciąg bitów tj. tablicę intów 0 lub 1 </returns>
        public int[]? StreamCipher(int[] inputStream, int[] seed, int degree)
        {
            // Wczytanie tablicy których stopni wielomianu użyć
            // do generowania liczb jeżeli stopień nieobecny w słowniku 
            // lub błąd otwarcia json'a kończy funkcję
            Dictionary<string, int[]>? degreesList = ReadJsonFile<Dictionary<string, int[]>?>();
            if (degreesList == null) return null;

            int[]? degreesToTake = null;
            degreesList?.TryGetValue(degree.ToString(), out degreesToTake);
            if (degreesToTake == null)
            {
                MessageBox.Show("Degree not present in dictionary");
                return null;
            }


            int[] polymonial = new int[degree];
            // Ustawienie jedynek na odpowiednie stopnie wielomianu
            // Odwzorowuje dodanie xor-ów 
            for (int i = 0; i < degreesToTake.Length; i++)
            {
                polymonial[degreesToTake[i] - 1] = 1;
            }
            int[] outputStream = new int[inputStream.Length];

            // pobranie pierwszej wartości bufora
            int[] buffer = GenerateLFSR(degree, seed, polymonial);
            for (int i = 0; i < inputStream.Length; i++)
            {
                // wpisanie na wyjście wyniku operacji xor na wartości z input'a i pierwszej liczby w buforze LSFR
                outputStream[i] = inputStream[i] ^ buffer[0];
                buffer = GenerateLFSR(degree, buffer, polymonial);
            }

            return outputStream;
        }
    }
}
