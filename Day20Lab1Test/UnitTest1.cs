namespace Day20Lab1Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //UnixTest è fatto di fatti(Fact) fatto è un evento secco
            double fk = Day20Lab1.Utils.FakeDiv(10, 2);
            Assert.True(fk==5.0, "Wrong Division");
        }
        //Quando si corregge il bug, il test si lascia perchè poi quando si ricompila
        //il codice in futuro se per caso il bug ritorna lo riscopriamo subito
        [Fact]
        public void Test2()
        {
            //UnixTest è fatto di fatti(Fact)
            double fk = Day20Lab1.Utils.FakeDiv(1010, 2);
            Assert.True(fk == 1010/2, "Wrong Division 1010");
        }
        //la teoria rispetto al fatto viene ripetuta (Es. i dati)
        [Theory]
        [InlineData("leo","oel")]
        [InlineData("anna","anna")]
        [InlineData("Antani","inatnA")]
        public void TestReverse(string a,string b)
        {
            Assert.True(Day20Lab1.Utils.Reverse(a).Equals(b), $"Wrong Reverse {a} != {b}");
        }
    }
}