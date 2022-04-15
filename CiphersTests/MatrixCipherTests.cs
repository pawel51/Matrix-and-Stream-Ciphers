using Ciphers;
using Xunit;

namespace CiphersTests
{
    public class MatrixCipherTests
    {
        [Theory]
        [InlineData(
            "CRYPTOGRAPHY"
            , 3
            , "CTARPORPYYGH")]
        [InlineData(
            "W tym szyfrze zostaly uzyte polskie znaki i cyfry 420"
            , 5
            , "Wrloaytfzayplnkr4yyetueszif2mzzsztkeiy0soyic")]
        [InlineData(
            "short"
            , 2
            , "sothr")]
        public void zad1_encrypt_shouldReturnExpectedString(string message, int height, string expected)
        {

            string actual = MainWindow.zad1_encrypt(message, height);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(
            "CTARPORPYYGH"
            , 3
            , "CRYPTOGRAPHY")]
        [InlineData(
            "Wrloaytfzayplnkr4yyetueszif2mzzsztkeiy0soyic"
            , 5
            , "Wtymszyfrzezostalyuzytepolskieznakiicyfry420")]
        [InlineData(
            "sothr"
            , 2
            , "short")]
        public void zad1_decrypt_shouldReturnExpectedString(string message, int height, string expected)
        {

            string actual = MainWindow.zad1_decrypt(height, message);

            Assert.Equal(expected, actual);
        }


        [Theory]
        [InlineData("6-3-4-1-5-2"
            , "CRYPTOGRAPHY"
            , "OYPCTRYAPGHR")]
        [InlineData("6-3-4-1-5-2"
            , "cryptography is awesome"
            , "oypctryapghrws  ai omees")]
        [InlineData("3-2-1"
            , "short"
            , "ohs tr")]
        [InlineData("6-3-4-1-5-2"
        , "tibiajuz"
        , "jbitai   u z"
        )]
        public void zad2a_encrypt_shouldReturnExpectedString(string key, string message, string expected)
        {

            string actual = MainWindow.zad2a_encrypt(key, message);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("6-3-4-1-5-2"
            , "OYPCTRYAPGHR"
            , "CRYPTOGRAPHY")]
        [InlineData("6-3-4-1-5-2"
            , "oypctryapghrws  ai omees"
            , "cryptography is awesome"
            )]
        [InlineData("3-2-1"
            , "ohs tr"
            , "short"
            )]
        [InlineData("6-3-4-1-5-2"
        , "jbitai   u z"
        , "tibiajuz"
        )]
        public void zad2a_decrypt_shouldReturnExpectedString(string key, string message, string expected)
        {

            string actual = MainWindow.zad2a_decrypt(key, message);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("CONVENIENCE"
            , "CRYPTOGRAPHY"
            , "CYP T R H G Y O A R P")]
        [InlineData("CONVENIENCE"
            , "W tym szyfrze zostały użyte polskie znaki i cyfry 420"
            , "Wzek zyn2 sasy fuey eta0 yyir ysoi złkf rżz4 topi mtlc")]
        [InlineData("CONVENIENCE"
            , "short"
            , "s t   o  hr")]
        public void zad2b_encrypt_shouldReturnExpectedString(string key, string message, string expected)
        {

            string actual = MainWindow.zad2b_encrypt(key, message);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("CONVENIENCE"
            , "CYP T R H G Y O A R P"
            , "CRYPTOGRAPHY")]
        [InlineData("CONVENIENCE"
            , "Wzek zyn2 sasy fuey eta0 yyir ysoi złkf rżz4 topi mtlc"
            , "Wtymszyfrzezostałyużytepolskieznakiicyfry420")]
        [InlineData("CONVENIENCE"
            , "s t   o  hr"
            , "short")]
        public void zad2b_decrypt_shouldReturnExpectedString(string key, string message, string expected)
        {
            string actual = MainWindow.zad2b_decrypt(key, message);
            Assert.Equal(expected, actual);
        }

    }
}