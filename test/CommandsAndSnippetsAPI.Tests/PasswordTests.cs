using System;
using UsersServer.Cryptography;
using Xunit;

namespace CommandsAndSnippetsAPI.Tests
{
    public class PasswordTests : IDisposable
    {
        private readonly Hasher _hasher;

        public PasswordTests()
        {
            _hasher = new Hasher();
        }


        public void Dispose()
        {
        }

        [Fact]
        public void SHA3_512_Password_Gets_Verified()
        {
            var password = _hasher.CreateHash("password", BaseCryptoItem.HashAlgorithm.SHA3_512);

            // Act
            var result = _hasher.MatchesHash("password", password);

            // Assert

            Assert.True(result);
        }

        [Fact]
        public void SHA2_512_Password_Gets_Verified()
        {
            // Arrange
            var password = _hasher.CreateHash("password", BaseCryptoItem.HashAlgorithm.SHA2_512);

            // Act
            var result = _hasher.MatchesHash("password", password);

            // Assert

            Assert.True(result);
        }

        [Fact]
        public void SHA3_512_Password_With_Symbols_and_Numbers_Gets_Success()
        {
            // Arrange

            var password = _hasher.CreateHash("Uw&wtUxo912=27%$//dUwuq", BaseCryptoItem.HashAlgorithm.SHA3_512);

            // Act
            var result = _hasher.MatchesHash("Uw&wtUxo912=27%$//dUwuq", password);

            // Assert

            Assert.True(result);
        }


        [Fact]
        public void SHA3_512_Password_With_Different_Symbols_and_Numbers_Gets_Unsuccess()
        {
            // Arrange

            var password = _hasher.CreateHash("Uw&wtUxo912=27%$//dUwuq", BaseCryptoItem.HashAlgorithm.SHA3_512);

            // Act
            var result = _hasher.MatchesHash("Uw&wtU1291212191dUwuq", password);

            // Assert

            Assert.False(result);
        }
    }
}