using Microsoft.VisualStudio.TestTools.UnitTesting;
using enemie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace enemie.Tests
{
    [TestClass()]
    public class JoueurTests
    {
        [TestMethod()]
        public void JouerTest()
        {
            Joueur.jouLife = 5;
        }
        [TestMethod()]
        public void MechanTest()
        {
            Enemie mechant = new();
            mechant.life = 3;
        }
    }
}