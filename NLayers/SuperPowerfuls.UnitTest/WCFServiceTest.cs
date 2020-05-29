namespace SuperPowerfuls.UnitTest
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SuperPowerfuls.Core.Contracts;
    using Moq;
    using SuperPowerfuls.Core.Services;
    using SuperPowerfuls.Services;
    using SuperPowerfuls.Core.Models;
    using System.Collections.Generic;

    [TestClass]
    public class WCFServiceTest
    {
        private Mock<IHeroRepository> _mockHeroRepository;
        private IHeroService _heroService;
        List<Hero> heroes;

        HeroSvc wcfService;

        [TestInitialize]
        public void Initialize()
        {
            _mockHeroRepository = new Mock<IHeroRepository>();
            HeroService heroService = new HeroService(_mockHeroRepository.Object);
            _heroService = heroService;
            wcfService = new HeroSvc(_heroService);
            heroes = new List<Hero>() { new Hero("Batman"), new Hero("Superman"), new Hero("Mujer Maravilla"), new Hero("Spiderman") };
        }

        [TestMethod]
        public void Get_All_Heroes_From_WCF_In_Xml()
        {
            //Arrange
            _mockHeroRepository.Setup(x => x.GetAll()).Returns(heroes);
            //Act
            int result = wcfService.GetSuperHeroesXml().Count;
            //Assert
            Assert.AreEqual(4, result);
        }

        [TestMethod]
        public void Get_All_Heroes_From_WCF_In_Json()
        {
            //Arrange
            _mockHeroRepository.Setup(x => x.GetAll()).Returns(heroes);
            //Act
            int result = wcfService.GetSuperHeroesJson().Count;
            //Assert
            Assert.AreEqual(4, result);
        }
    }
}
