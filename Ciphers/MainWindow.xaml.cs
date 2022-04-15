using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;

namespace Ciphers
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private StreamCipherImpl _impl = new StreamCipherImpl();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        /// <summary>
        /// Rail Fance
        /// </summary>
        /// <param name="message">wiadomość do zaszyfrowania</param>
        /// <param name="height">wysokość macierzy</param>
        /// <returns>zaszyfrowaną wiadomość z pominięciem spacji</returns>
        public static string zad1_encrypt(string message, int height)
        {
            // usunięcie spacji
            message = message.Replace(" ", "");
            // szerokość to długość wiadomości
            int width = message.Length;
            if (height < 1) return message;

            // inicjuję macierz
            char[][] matrix = new char[height][];
            for (int i = 0; i < height; i++)
            {
                matrix[i] = new char[width];
            }
            
            // przechodzę po tablicy zygzakiem odbijając się od górnej i dolnej krawędzi
            for (int j = 0, n = 0; j < width; )
            {
                // idę w dół
                for (int i = 0; i < height & j < width; i++, j++, n++)
                {
                    matrix[i][j] = message[n];
                }
                // idę w górę
                for (int i = height - 2; i > 0 && j < width; i--, j++, n++)
                {
                    matrix[i][j] = message[n];
                }
            }
            // tablica charów na ciąg końcowy
            char[] encrypted = new char[message.Length];

            // przechodzę po tablicy jeżeli napotykam '\0' pomijam dane pole
            for (int i = 0, b = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (matrix[i][j] != '\0')
                        encrypted[b++] = matrix[i][j];
                }
            }

            return new string(encrypted).Trim();

        }

        /// <summary>
        /// Deszyfruje Railfence
        /// </summary>
        /// <param name="height">wysokosc kodowanej macierzy</param>
        /// <param name="message">zaszyfrowana wiadomość</param>
        /// <returns></returns>
        public static string zad1_decrypt(int height, string message)
        {
            int width = message.Length;

            char[][] snakeTrace = new char[height][];
            for (int i = 0; i < height; i++)
            {
                snakeTrace[i] = new char[width];
            }

            // przechodzę po tablicy zygzakiem odbijając się od górnej i dolnej krawędzi
            // ustalam dzięki temu koordynatu w które muszę wpisywać szyfr
            for (int j = 0, n = 0; j < width;)
            {
                // idę w dół
                for (int i = 0; i < height & j < width; i++, j++, n++)
                {
                    snakeTrace[i][j] = '1';
                }
                // idę w górę
                for (int i = height - 2; i > 0 && j < width; i--, j++, n++)
                {
                    snakeTrace[i][j] = '1';
                }
            }

            // przechodzę po tablicy (zwykłe czytanie)
            // wspisuję po kolei zaszyfrowaną wiadomość w pola oznaczone 1 
            for (int i = 0, n = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (snakeTrace[i][j] == '1')
                    {
                        snakeTrace[i][j] = message[n++];
                    }
                }
            }

            char[] decrypted = new char[message.Length];

            // przechodzę jeszcze raz zygzakiem by odczytać odszyfrowaną wiadomość
            for (int j = 0, n = 0; j < width;)
            {
                // idę w dół
                for (int i = 0; i < height & j < width; i++, j++, n++)
                {
                    decrypted[n] = snakeTrace[i][j];
                }
                // idę w górę
                for (int i = height - 2; i > 0 && j < width; i--, j++, n++)
                {
                    decrypted[n] = snakeTrace[i][j];
                }
            }

            return new string(decrypted).Trim();

        }

        /// <summary>
        /// Przestawienia macierzowe
        /// </summary>
        /// <param name="key">dotyczy kolejności odczytywania znaków z macierzy, określa równierz szerokość macierzy</param>
        /// <param name="message">szyfrowana wiadomość</param>
        /// <returns>zaszyfrowaną wiadomość z pominięciem spacji</returns>
        public static string zad2a_encrypt(string key, string message)
        {
            // Wyrzucam wszystkie '-' z klucza
            string[] colNumbers = key.Split('-');

            // tablica z kolejnością czytania kolumn
            int[] keyArray = new int[colNumbers.Length];
            // konwersja na int-y
            for (int i = 0; i < colNumbers.Length; i++)
            {
                keyArray[i] = Convert.ToInt32(colNumbers[i]);
            }

            // szerokość do długość klucza
            int width = keyArray.Length;
            // wysokość to długość wiadomości przez długość klucza + 1
            int height = message.Length / keyArray.Length + 1;

            // inicjuję macierz char-ów
            char[][] matrix = new char[height][];
            for (int i = 0; i < height; i++)
            {
                matrix[i] = new char[width];
            }

            // wpisuje do macierzy wiadomość po kolei
            for (int i = 0, n = 0; i < height; i++)
            {
                for (int j = 0; j < width && n < message.Length; j++, n++)
                {
                    matrix[i][j] = message[n];
                }
            }
            // tablic do zaszyfrowania
            char[] encrypted = new char[width * height];
            // przechodzę po tablicy przeskakując po kolumnach zgodnie z kluczem
            for (int i = 0, n = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    int m = keyArray[j] - 1;
                    if (matrix[i][m] == '\0')
                        encrypted[n++] = ' ';
                    else
                        encrypted[n++] = matrix[i][m];
                }
            }

            return new string(encrypted).Trim();
                
        }

        public static string zad2a_decrypt(string key, string message)
        {
            // Wyrzucam wszystkie '-' z klucza
            string[] colNumbers = key.Split('-');
            // tablica z kolejnością czytania kolumn
            int[] keyArray = new int[colNumbers.Length];
            // konwersja na int-y
            for (int i = 0; i < colNumbers.Length; i++)
            {
                keyArray[i] = Convert.ToInt32(colNumbers[i]);
            }

            // szerokość do długość klucza
            int width = keyArray.Length;
            // wysokość to długość wiadomości przez długość klucza + 1
            int height = message.Length / keyArray.Length + 1;

            // inicjuję macierz char-ów
            char[][] matrix = new char[height][];
            for (int i = 0; i < height; i++)
            {
                matrix[i] = new char[width];
            }

            // przechodzę po tablicy przeskakując po kolumnach zgodnie z kluczem
            for (int i = 0, n = 0; i < height; i++)
            {
                for (int j = 0; j < width && n < message.Length; j++)
                {
                    int m = keyArray[j] - 1;
                    matrix[i][m] = message[n++];
                }
            }

            char[] decrypted = new char[message.Length];

            // odczytuje z macierzy wiadomość po kolei
            for (int i = 0, n = 0; i < height; i++)
            {
                for (int j = 0; j < width && n < message.Length; j++)
                {
                    decrypted[n++] = matrix[i][j];
                }
            }

            return new string(decrypted).Trim();

        }

        /// <summary>
        /// Przestawienia macierzowe + plus zasada kolejności alfabetycznej
        /// </summary>
        /// <param name="key">Określa kolejność odczytywania kolumn z macierzy</param>
        /// <param name="message">wiadomość do zaszyfrowania</param>
        /// <returns>zaszyfrowaną wiadomość z pominięciem spacji</returns>
        public static string zad2b_encrypt(string key, string message)
        {

            message = message.Replace(" ", "");

            int width = key.Length; // szerokość tablicy początkowej
            int height = message.Length / key.Length + 1;

            // stworzenie macierzy
            char[][] matrix = new char[height][];
            for (int i = 0; i < height; i++)
            {
                matrix[i] = new char[width];
            }

            // wypełnienie macierzy
            for (int i = 0, k = 0; i < height; i++)
            {
                for (int j = 0; j < width && k < message.Length; j++, k++)
                {
                    matrix[i][j] = message[k];
                }
            }

            // ustalenie kolejności kolumn według klucza
            List<(char, int)> extendedKey = new List<(char, int)>(); // klucz - char klucza oraz jego początkowa pozycja, value - index w posortowanej tablicy


            // przekształcam string na listę krotek(tupli) (znak , index)
            for (int i = 0; i < key.Length; i++)
            {
                extendedKey.Add((key[i], i));
            }
            //sortowanie listy alfabetycznie po znaku czyli Item1 
            extendedKey
                .Sort((x, y) => {
                    if (x.Item1 > y.Item1)
                        return 1;
                    else if (x.Item1 < y.Item1)
                        return -1;
                    else
                        return 0;
                });

            char[] encrypted = new char[width * height];


            // ustawiam j (kolumnę) na index początkowy znaku z posortowanej tablicy
            // czyli biąrę pierwszy element z posortowanej tablicy i patrzę jaki miał indeks w początkowym kluczu
            // czytam całą kolumnę 
            // biorę następny element z posortowanej tablicy i patrzę jaki miał indeks w początkowym kluczu ...
            
            for (int n = 0, b = 0; n < width; n++)
            {
                int j = extendedKey[n].Item2;
                for (int i = 0; i < height; i++)
                {
                    if (matrix[i][j] == '\0')
                        encrypted[b++] = ' ';
                    else
                        encrypted[b++] = matrix[i][j];
                }
            }

            return new string(encrypted).Trim();

            // Aby odszyfrować wiadomośc należałoby narysować siatkę długość klucz na (długość wiadomości przez długość klucza + 1)
            // i wpisywać do odpowiednich kolumn alfabetycznie kolejne znaki z wiadomości
        }

        /// <summary>
        /// Odszyfrowuje przestawienie macierzowe z zad 2b
        /// </summary>
        /// <param name="key">klucz</param>
        /// <param name="message">zaszyfrowana wiadomość</param>
        /// <returns></returns>
        public static string zad2b_decrypt(string key, string message)
        {
            int condensedMessageLength = message.Replace(" ", "").Length;
            int width = key.Length; // szerokość tablicy początkowej
            int tmpHeight = message.Length / key.Length;
            int height = tmpHeight * width < message.Length ?
                         tmpHeight + 1 :
                         tmpHeight;
                

            // stworzenie macierzy
            char[][] matrix = new char[height][];
            for (int i = 0; i < height; i++)
            {
                matrix[i] = new char[width];
            }

            // ustalenie kolejności kolumn według klucza
            List<(char, int)> extendedKey = new List<(char, int)>(); // klucz - char klucza oraz jego początkowa pozycja, value - index w posortowanej tablicy


            // przekształcam string na listę krotek(tupli) (znak , index)
            for (int i = 0; i < key.Length; i++)
            {
                extendedKey.Add((key[i], i));
            }
            //sortowanie listy alfabetycznie po znaku czyli Item1 
            extendedKey
                .Sort((x, y) => {
                    if (x.Item1 > y.Item1)
                        return 1;
                    else if (x.Item1 < y.Item1)
                        return -1;
                    else
                        return 0;
                });


            // ustawiam j (kolumnę) na index początkowy znaku z posortowanej tablicy
            // czyli biąrę pierwszy element z posortowanej tablicy i patrzę jaki miał indeks w początkowym kluczu
            // wpisuję całą kolumne 
            // biorę następny element z posortowanej tablicy i patrzę jaki miał indeks w początkowym kluczu ...

            for (int n = 0, b = 0; n < width; n++)
            {
                int j = extendedKey[n].Item2;
                for (int i = 0; i < height && b < message.Length; i++)
                {
                    matrix[i][j] = message[b++];
                }
            }

            char[] decrypted = new char[condensedMessageLength];

            for (int i = 0, b = 0; i < height; i++)
            {
                for (int j = 0; j < width && b < condensedMessageLength; j++, b++)
                {
                    decrypted[b] = matrix[i][j];
                }
            }

            return new string(decrypted).Trim();
        }

        

        


        private void Encrypt_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel dc = (MainViewModel)DataContext;

            string key = dc.Key;
            string message = dc.Message;
            string n = dc.n;

            switch (dc.SelectedAlgorithm)
            {
                case AlgorithmsEnum.zad1:
                    dc.Encrypted = zad1_encrypt(message, Convert.ToInt32(n));
                    break;
                case AlgorithmsEnum.zad2a:
                    dc.Encrypted = zad2a_encrypt(key, message);
                    break;
                case AlgorithmsEnum.zad2b:
                    dc.Encrypted = zad2b_encrypt(key, message);
                    break;
                default:
                    return;

            }
        }

        private void Decrypt_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel dc = (MainViewModel)DataContext;

            string key = dc.Key;
            string message = dc.ToDecrypt;
            string n = dc.n;

            switch (dc.SelectedAlgorithm)
            {
                case AlgorithmsEnum.zad1:
                    dc.Decrypted = zad1_decrypt(Convert.ToInt32(n), message);
                    break;
                case AlgorithmsEnum.zad2a:
                    dc.Decrypted = zad2a_decrypt(key, message);
                    break;
                case AlgorithmsEnum.zad2b:
                    dc.Decrypted = zad2b_decrypt(key, message);
                    break;
                default:
                    return;

            }
        }

        private void CopyEncrypted_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(EncryptedTextBox.Text);
            TextToDecryptTxtBox.Text = EncryptedTextBox.Text;
        }

        private void CopyDecrypted_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(DecryptedTextBox.Text);
        }

        private void BitInputPreview(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-1]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void IntegerInputPreview(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void StreamCipherButton_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel dc = ((MainViewModel)DataContext);

            // init
            string inputStr = dc.Input;
            string seedStr = dc.Seed;
            string degreeStr = dc.SelectedDegree;
            int degree = Int32.Parse(degreeStr);

            // validate
            if (String.IsNullOrEmpty(inputStr))
            {
                MessageBox.Show("input is empty !!!");
                return;
            }

            if (degree != seedStr.Length)
            {
                MessageBox.Show("degree != seed.length !!!");
                return;
            }

            if (degree > 9 || degree < 1)
            {
                MessageBox.Show("degree not supported");
                return;
            }
                

            // convert
            int[] input = new int[inputStr.Length];
            int[] seed = new int[seedStr.Length];

            for (int i = 0; i < inputStr.Length; i++)
            {
                input[i] = Int32.Parse(inputStr[i].ToString());
            }

            for (int i = 0; i < degree; i++)
            {
                seed[i] = Int32.Parse(seedStr[i].ToString());
            }

            // encrypt
            int[]? output = _impl.StreamCipher(input, seed, degree);

            // validate
            if (output == null)
            {
                dc.Output = "error";
                return;
            }

            // convert
            string outputStr = "";
            for (int i = 0; i < output.Length; i++)
            {
                outputStr += output[i];
            }

            // display
            dc.Output = outputStr;
        }
    }
}
