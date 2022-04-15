using Xunit;
using Ciphers;

namespace CiphersTests
{
    public class StreamCipherTests
    {
        [Theory]
        [InlineData(
            new int[] {1, 1, 1, 0, 1, 0, 0, 1},
            new int[] {0,0,1,0},
            4,
            new int[] {1, 0, 0, 1, 0, 0, 1, 1}
        )]
        [InlineData(
            new int[] { 1,1,1,0,1,0,0,1 },
            new int[] { 1,1,0,0,1,0,0,1 },
            8,
            new int[] { 0,1,1,0,1,0,0,0 }
        )]
        public void shouldReturnExactIntArray(int[] inputStream, int[] seed, int degree, int[] expected)
        {
            StreamCipherImpl impl = new StreamCipherImpl();
            int[]? actual = impl.StreamCipher(inputStream, seed, degree);
            if (actual == null) return;
            Assert.Equal(actual.Length, expected.Length);
            for (int i = 0; i < actual.Length; i++)
            {
                Assert.Equal(expected[i], actual[i]);
            }

        }

    }
}
